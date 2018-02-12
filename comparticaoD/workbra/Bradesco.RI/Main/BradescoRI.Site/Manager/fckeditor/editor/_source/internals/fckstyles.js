var FCKStyles = FCK.Styles =
{
	_Callbacks : {},
	_ObjectStyles : {},

	ApplyStyle : function( style )
	{
		if ( typeof style == 'string' )
			style = this.GetStyles()[ style ] ;

		if ( style )
		{
			if ( style.GetType() == FCK_STYLE_OBJECT )
				style.ApplyToObject( FCKSelection.GetSelectedElement() ) ;
			else
				style.ApplyToSelection( FCK.EditorWindow ) ;

			FCK.Events.FireEvent( 'OnSelectionChange' ) ;
		}
	},

	RemoveStyle : function( style )
	{
		if ( typeof style == 'string' )
			style = this.GetStyles()[ style ] ;

		if ( style )
		{
			style.RemoveFromSelection( FCK.EditorWindow ) ;
			FCK.Events.FireEvent( 'OnSelectionChange' ) ;
		}
	},

	AttachStyleStateChange : function( styleName, callback, callbackOwner )
	{
		var callbacks = this._Callbacks[ styleName ] ;

		if ( !callbacks )
			callbacks = this._Callbacks[ styleName ] = [] ;

		callbacks.push( [ callback, callbackOwner ] ) ;
	},

	CheckSelectionChanges : function()
	{
		var startElement = FCKSelection.GetBoundaryParentElement( true ) ;

		if ( !startElement )
			return ;


		var path = new FCKElementPath( startElement ) ;
		var styles = this.GetStyles() ;

		for ( var styleName in styles )
		{
			var callbacks = this._Callbacks[ styleName ] ;

			if ( callbacks )
			{
				var style = styles[ styleName ] ;
				var state = style.CheckActive( path ) ;

				if ( state != ( style._LastState || null ) )
				{
					style._LastState = state ;

					for ( var i = 0 ; i < callbacks.length ; i++ )
					{
						var callback = callbacks[i][0] ;
						var callbackOwner = callbacks[i][1] ;

						callback.call( callbackOwner || window, styleName, state ) ;
					}
				}
			}
		}
	},

	CheckStyleInSelection : function( styleName )
	{
		return false ;
	},

	_GetRemoveFormatTagsRegex : function ()
	{
		var regex = new RegExp( '^(?:' + FCKConfig.RemoveFormatTags.replace( /,/g,'|' ) + ')$', 'i' ) ;

		return (this._GetRemoveFormatTagsRegex = function()
		{
			return regex ;
		})
		&& regex  ;
	},
	
	RemoveAll : function()
	{
		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;

		if ( range.CheckIsCollapsed() )
			return ;


		range.Expand( 'inline_elements' ) ;



		var bookmark = range.CreateBookmark( true ) ;


		var startNode	= range.GetBookmarkNode( bookmark, true ) ;
		var endNode		= range.GetBookmarkNode( bookmark, false ) ;

		range.Release( true ) ;

		var tagsRegex = this._GetRemoveFormatTagsRegex() ;












		var path = new FCKElementPath( startNode ) ;
		var pathElements = path.Elements ;
		var pathElement ;

		for ( var i = 1 ; i < pathElements.length ; i++ )
		{
			pathElement = pathElements[i] ;

			if ( pathElement == path.Block || pathElement == path.BlockLimit )
				break ;


			if ( tagsRegex.test( pathElement.nodeName ) )
				FCKDomTools.BreakParent( startNode, pathElement, range ) ;
		}


		path = new FCKElementPath( endNode ) ;
		pathElements = path.Elements ;

		for ( var i = 1 ; i < pathElements.length ; i++ )
		{
			pathElement = pathElements[i] ;

			if ( pathElement == path.Block || pathElement == path.BlockLimit )
				break ;

			elementName = pathElement.nodeName.toLowerCase() ;


			if ( tagsRegex.test( pathElement.nodeName ) )
				FCKDomTools.BreakParent( endNode, pathElement, range ) ;
		}


		var currentNode = FCKDomTools.GetNextSourceNode( startNode, true, 1 ) ;

		while ( currentNode )
		{

			if ( currentNode == endNode )
				break ;



			var nextNode = FCKDomTools.GetNextSourceNode( currentNode, false, 1 ) ;


			if ( tagsRegex.test( currentNode.nodeName ) )
				FCKDomTools.RemoveNode( currentNode, true ) ;
			else
				FCKDomTools.RemoveAttributes( currentNode, FCKConfig.RemoveAttributesArray );

			currentNode = nextNode ;
		}

		range.SelectBookmark( bookmark ) ;

		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
	},

	GetStyle : function( styleName )
	{
		return this.GetStyles()[ styleName ] ;
	},

	GetStyles : function()
	{
		var styles = this._GetStyles ;
		if ( !styles )
		{
			styles = this._GetStyles = FCKTools.Merge(
				this._LoadStylesCore(),
				this._LoadStylesCustom(),
				this._LoadStylesXml() ) ;
		}
		return styles ;
	},

	CheckHasObjectStyle : function( elementName )
	{
		return !!this._ObjectStyles[ elementName ] ;
	},

	_LoadStylesCore : function()
	{
		var styles = {};
		var styleDefs = FCKConfig.CoreStyles ;

		for ( var styleName in styleDefs )
		{

			var style = styles[ '_FCK_' + styleName ] = new FCKStyle( styleDefs[ styleName ] ) ;
			style.IsCore = true ;
		}
		return styles ;
	},

	_LoadStylesCustom : function()
	{
		var styles = {};
		var styleDefs = FCKConfig.CustomStyles ;

		if ( styleDefs )
		{
			for ( var styleName in styleDefs )
			{
				var style = styles[ styleName ] = new FCKStyle( styleDefs[ styleName ] ) ;
				style.Name = styleName ;
			}
		}

		return styles ;
	},

	_LoadStylesXml : function()
	{
		var styles = {};

		var stylesXmlPath = FCKConfig.StylesXmlPath ;

		if ( !stylesXmlPath || stylesXmlPath.length == 0 )
			return styles ;


		var xml = new FCKXml() ;
		xml.LoadUrl( stylesXmlPath ) ;

		var stylesXmlObj = FCKXml.TransformToObject( xml.SelectSingleNode( 'Styles' ) ) ;


		var styleNodes = stylesXmlObj.$Style ;


		if ( !styleNodes )
			return styles ;


		for ( var i = 0 ; i < styleNodes.length ; i++ )
		{
			var styleNode = styleNodes[i] ;

			var element = ( styleNode.element || '' ).toLowerCase() ;

			if ( element.length == 0 )
				throw( 'The element name is required. Error loading "' + stylesXmlPath + '"' ) ;

			var styleDef = {
				Element : element,
				Attributes : {},
				Styles : {},
				Overrides : []
			} ;


			var attNodes = styleNode.$Attribute || [] ;


			for ( var j = 0 ; j < attNodes.length ; j++ )
			{
				styleDef.Attributes[ attNodes[j].name ] = attNodes[j].value ;
			}


			var cssStyleNodes = styleNode.$Style || [] ;


			for ( j = 0 ; j < cssStyleNodes.length ; j++ )
			{
				styleDef.Styles[ cssStyleNodes[j].name ] = cssStyleNodes[j].value ;
			}


			var cssStyleOverrideNodes = styleNode.$Override ;
			if ( cssStyleOverrideNodes )
			{
				for ( j = 0 ; j < cssStyleOverrideNodes.length ; j++ )
				{
					var overrideNode = cssStyleOverrideNodes[j] ;
					var overrideDef =
					{
						Element : overrideNode.element
					} ;

					var overrideAttNode = overrideNode.$Attribute ;
					if ( overrideAttNode )
					{
						overrideDef.Attributes = {} ;
						for ( var k = 0 ; k < overrideAttNode.length ; k++ )
						{
							var overrideAttValue = overrideAttNode[k].value || null ;
							if ( overrideAttValue )
							{

								var regexMatch = overrideAttValue && FCKRegexLib.RegExp.exec( overrideAttValue ) ;
								if ( regexMatch )
									overrideAttValue = new RegExp( regexMatch[1], regexMatch[2] || '' ) ;
							}
							overrideDef.Attributes[ overrideAttNode[k].name ] = overrideAttValue ;
						}
					}

					styleDef.Overrides.push( overrideDef ) ;
				}
			}

			var style = new FCKStyle( styleDef ) ;
			style.Name = styleNode.name || element ;

			if ( style.GetType() == FCK_STYLE_OBJECT )
				this._ObjectStyles[ element ] = true ;


			styles[ style.Name ] = style ;
		}

		return styles ;
	}
} ;