var FCKXHtml = new Object() ;

FCKXHtml.CurrentJobNum = 0 ;

FCKXHtml.GetXHTML = function( node, includeNode, format )
{
	FCKDomTools.CheckAndRemovePaddingNode( FCKTools.GetElementDocument( node ), FCKConfig.EnterMode ) ;
	FCKXHtmlEntities.Initialize() ;


	this._NbspEntity = ( FCKConfig.ProcessHTMLEntities? 'nbsp' : '#160' ) ;



	var bIsDirty = FCK.IsDirty() ;



	FCKXHtml.SpecialBlocks = new Array() ;


	this.XML = FCKTools.CreateXmlObject( 'DOMDocument' ) ;


	this.MainNode = this.XML.appendChild( this.XML.createElement( 'xhtml' ) ) ;

	FCKXHtml.CurrentJobNum++ ;



	if ( includeNode )
		this._AppendNode( this.MainNode, node ) ;
	else
		this._AppendChildNodes( this.MainNode, node, false ) ;


	var sXHTML = this._GetMainXmlString() ;



	this.XML = null ;


	if ( FCKBrowserInfo.IsSafari )
		sXHTML = sXHTML.replace( /^<xhtml.*?>/, '<xhtml>' ) ;


	sXHTML = sXHTML.substr( 7, sXHTML.length - 15 ).Trim() ;




	if (FCKConfig.DocType.length > 0 && FCKRegexLib.HtmlDocType.test( FCKConfig.DocType ) )
		sXHTML = sXHTML.replace( FCKRegexLib.SpaceNoClose, '>');
	else
		sXHTML = sXHTML.replace( FCKRegexLib.SpaceNoClose, ' />');

	if ( FCKConfig.ForceSimpleAmpersand )
		sXHTML = sXHTML.replace( FCKRegexLib.ForceSimpleAmpersand, '&' ) ;

	if ( format )
		sXHTML = FCKCodeFormatter.Format( sXHTML ) ;


	for ( var i = 0 ; i < FCKXHtml.SpecialBlocks.length ; i++ )
	{
		var oRegex = new RegExp( '___FCKsi___' + i ) ;
		sXHTML = sXHTML.replace( oRegex, FCKXHtml.SpecialBlocks[i] ) ;
	}


	sXHTML = sXHTML.replace( FCKRegexLib.GeckoEntitiesMarker, '&' ) ;


	if ( !bIsDirty )
		FCK.ResetIsDirty() ;

	FCKDomTools.EnforcePaddingNode( FCKTools.GetElementDocument( node ), FCKConfig.EnterMode ) ;
	return sXHTML ;
}

FCKXHtml._AppendAttribute = function( xmlNode, attributeName, attributeValue )
{
	try
	{
		if ( attributeValue == undefined || attributeValue == null )
			attributeValue = '' ;
		else if ( attributeValue.replace )
		{
			if ( FCKConfig.ForceSimpleAmpersand )
				attributeValue = attributeValue.replace( /&/g, '___FCKAmp___' ) ;


			attributeValue = attributeValue.replace( FCKXHtmlEntities.EntitiesRegex, FCKXHtml_GetEntity ) ;
		}


		var oXmlAtt = this.XML.createAttribute( attributeName ) ;
		oXmlAtt.value = attributeValue ;


		xmlNode.attributes.setNamedItem( oXmlAtt ) ;
	}
	catch (e)
	{}
}

FCKXHtml._AppendChildNodes = function( xmlNode, htmlNode, isBlockElement )
{
	var oNode = htmlNode.firstChild ;

	while ( oNode )
	{
		this._AppendNode( xmlNode, oNode ) ;
		oNode = oNode.nextSibling ;
	}



	if ( isBlockElement && htmlNode.tagName && htmlNode.tagName.toLowerCase() != 'pre' )
	{
		FCKDomTools.TrimNode( xmlNode ) ;

		if ( FCKConfig.FillEmptyBlocks )
		{
			var lastChild = xmlNode.lastChild ;
			if ( lastChild && lastChild.nodeType == 1 && lastChild.nodeName == 'br' )
				this._AppendEntity( xmlNode, this._NbspEntity ) ;
		}
	}


	if ( xmlNode.childNodes.length == 0 )
	{
		if ( isBlockElement && FCKConfig.FillEmptyBlocks )
		{
			this._AppendEntity( xmlNode, this._NbspEntity ) ;
			return xmlNode ;
		}

		var sNodeName = xmlNode.nodeName ;


		if ( FCKListsLib.InlineChildReqElements[ sNodeName ] )
			return null ;



		if ( !FCKListsLib.EmptyElements[ sNodeName ] )
			xmlNode.appendChild( this.XML.createTextNode('') ) ;
	}

	return xmlNode ;
}

FCKXHtml._AppendNode = function( xmlNode, htmlNode )
{
	if ( !htmlNode )
		return false ;

	switch ( htmlNode.nodeType )
	{

		case 1 :


			if ( FCKBrowserInfo.IsGecko
					&& htmlNode.tagName.toLowerCase() == 'br'
					&& htmlNode.parentNode.tagName.toLowerCase() == 'pre' )
			{
				var val = '\r' ;
				if ( htmlNode == htmlNode.parentNode.firstChild )
					val += '\r' ;
				return FCKXHtml._AppendNode( xmlNode, this.XML.createTextNode( val ) ) ;
			}



			if ( htmlNode.getAttribute('_fckfakelement') )
				return FCKXHtml._AppendNode( xmlNode, FCK.GetRealElement( htmlNode ) ) ;


			if ( FCKBrowserInfo.IsGecko &&
					( htmlNode.hasAttribute('_moz_editor_bogus_node') || htmlNode.getAttribute( 'type' ) == '_moz' ) )
			{
				if ( htmlNode.nextSibling )
					return false ;
				else
				{
					htmlNode.removeAttribute( '_moz_editor_bogus_node' ) ;
					htmlNode.removeAttribute( 'type' ) ;
				}
			}



			if ( htmlNode.getAttribute('_fcktemp') )
				return false ;


			var sNodeName = htmlNode.tagName.toLowerCase()  ;

			if ( FCKBrowserInfo.IsIE )
			{

				if ( htmlNode.scopeName && htmlNode.scopeName != 'HTML' && htmlNode.scopeName != 'FCK' )
					sNodeName = htmlNode.scopeName.toLowerCase() + ':' + sNodeName ;
			}
			else
			{
				if ( sNodeName.StartsWith( 'fck:' ) )
					sNodeName = sNodeName.Remove( 0,4 ) ;
			}




			if ( !FCKRegexLib.ElementName.test( sNodeName ) )
				return false ;



			if ( htmlNode._fckxhtmljob && htmlNode._fckxhtmljob == FCKXHtml.CurrentJobNum )
				return false ;

			var oNode = this.XML.createElement( sNodeName ) ;


			FCKXHtml._AppendAttributes( xmlNode, htmlNode, oNode, sNodeName ) ;

			htmlNode._fckxhtmljob = FCKXHtml.CurrentJobNum ;


			var oTagProcessor = FCKXHtml.TagProcessors[ sNodeName ] ;

			if ( oTagProcessor )
				oNode = oTagProcessor( oNode, htmlNode, xmlNode ) ;
			else
				oNode = this._AppendChildNodes( oNode, htmlNode, Boolean( FCKListsLib.NonEmptyBlockElements[ sNodeName ] ) ) ;

			if ( !oNode )
				return false ;

			xmlNode.appendChild( oNode ) ;

			break ;


		case 3 :
			if ( htmlNode.parentNode && htmlNode.parentNode.nodeName.IEquals( 'pre' ) )
				return this._AppendTextNode( xmlNode, htmlNode.nodeValue ) ;
			return this._AppendTextNode( xmlNode, htmlNode.nodeValue.ReplaceNewLineChars(' ') ) ;


		case 8 :


			if ( FCKBrowserInfo.IsIE && !htmlNode.innerHTML )
				break ;

			try { xmlNode.appendChild( this.XML.createComment( htmlNode.nodeValue ) ) ; }
			catch (e) { }
			break ;


		default :
			xmlNode.appendChild( this.XML.createComment( "Element not supported - Type: " + htmlNode.nodeType + " Name: " + htmlNode.nodeName ) ) ;
			break ;
	}
	return true ;
}


FCKXHtml._AppendSpecialItem = function( item )
{
	return '___FCKsi___' + ( FCKXHtml.SpecialBlocks.push( item ) - 1 ) ;
}

FCKXHtml._AppendEntity = function( xmlNode, entity )
{
	xmlNode.appendChild( this.XML.createTextNode( '#?-:' + entity + ';' ) ) ;
}

FCKXHtml._AppendTextNode = function( targetNode, textValue )
{
	var bHadText = textValue.length > 0 ;
	if ( bHadText )
		targetNode.appendChild( this.XML.createTextNode( textValue.replace( FCKXHtmlEntities.EntitiesRegex, FCKXHtml_GetEntity ) ) ) ;
	return bHadText ;
}


function FCKXHtml_GetEntity( character )
{



	var sEntity = FCKXHtmlEntities.Entities[ character ] || ( '#' + character.charCodeAt(0) ) ;
	return '#?-:' + sEntity + ';' ;
}


FCKXHtml.TagProcessors =
{
	a : function( node, htmlNode )
	{

		if ( htmlNode.innerHTML.Trim().length == 0 && !htmlNode.name )
			return false ;

		var sSavedUrl = htmlNode.getAttribute( '_fcksavedurl' ) ;
		if ( sSavedUrl != null )
			FCKXHtml._AppendAttribute( node, 'href', sSavedUrl ) ;



		if ( FCKBrowserInfo.IsIE )
		{

			if ( htmlNode.name )
				FCKXHtml._AppendAttribute( node, 'name', htmlNode.name ) ;
		}

		node = FCKXHtml._AppendChildNodes( node, htmlNode, false ) ;

		return node ;
	},

	area : function( node, htmlNode )
	{
		var sSavedUrl = htmlNode.getAttribute( '_fcksavedurl' ) ;
		if ( sSavedUrl != null )
			FCKXHtml._AppendAttribute( node, 'href', sSavedUrl ) ;


		if ( FCKBrowserInfo.IsIE )
		{
			if ( ! node.attributes.getNamedItem( 'coords' ) )
			{
				var sCoords = htmlNode.getAttribute( 'coords', 2 ) ;
				if ( sCoords && sCoords != '0,0,0' )
					FCKXHtml._AppendAttribute( node, 'coords', sCoords ) ;
			}

			if ( ! node.attributes.getNamedItem( 'shape' ) )
			{
				var sShape = htmlNode.getAttribute( 'shape', 2 ) ;
				if ( sShape && sShape.length > 0 )
					FCKXHtml._AppendAttribute( node, 'shape', sShape.toLowerCase() ) ;
			}
		}

		return node ;
	},

	body : function( node, htmlNode )
	{
		node = FCKXHtml._AppendChildNodes( node, htmlNode, false ) ;

		node.removeAttribute( 'spellcheck' ) ;
		return node ;
	},



	iframe : function( node, htmlNode )
	{
		var sHtml = htmlNode.innerHTML ;


		if ( FCKBrowserInfo.IsGecko )
			sHtml = FCKTools.HTMLDecode( sHtml );


		sHtml = sHtml.replace( /\s_fcksavedurl="[^"]*"/g, '' ) ;

		node.appendChild( FCKXHtml.XML.createTextNode( FCKXHtml._AppendSpecialItem( sHtml ) ) ) ;

		return node ;
	},

	img : function( node, htmlNode )
	{

		if ( ! node.attributes.getNamedItem( 'alt' ) )
			FCKXHtml._AppendAttribute( node, 'alt', '' ) ;

		var sSavedUrl = htmlNode.getAttribute( '_fcksavedurl' ) ;
		if ( sSavedUrl != null )
			FCKXHtml._AppendAttribute( node, 'src', sSavedUrl ) ;



		if ( htmlNode.style.width )
			node.removeAttribute( 'width' ) ;
		if ( htmlNode.style.height )
			node.removeAttribute( 'height' ) ;

		return node ;
	},


	li : function( node, htmlNode, targetNode )
	{

		if ( targetNode.nodeName.IEquals( ['ul', 'ol'] ) )
			return FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

		var newTarget = FCKXHtml.XML.createElement( 'ul' ) ;


		htmlNode._fckxhtmljob = null ;


		do
		{
			FCKXHtml._AppendNode( newTarget, htmlNode ) ;


			do
			{
				htmlNode = FCKDomTools.GetNextSibling( htmlNode ) ;

			} while ( htmlNode && htmlNode.nodeType == 3 && htmlNode.nodeValue.Trim().length == 0 )

		}	while ( htmlNode && htmlNode.nodeName.toLowerCase() == 'li' )

		return newTarget ;
	},


	ol : function( node, htmlNode, targetNode )
	{
		if ( htmlNode.innerHTML.Trim().length == 0 )
			return false ;

		var ePSibling = targetNode.lastChild ;

		if ( ePSibling && ePSibling.nodeType == 3 )
			ePSibling = ePSibling.previousSibling ;

		if ( ePSibling && ePSibling.nodeName.toUpperCase() == 'LI' )
		{
			htmlNode._fckxhtmljob = null ;
			FCKXHtml._AppendNode( ePSibling, htmlNode ) ;
			return false ;
		}

		node = FCKXHtml._AppendChildNodes( node, htmlNode ) ;

		return node ;
	},

	pre : function ( node, htmlNode )
	{
		var firstChild = htmlNode.firstChild ;

		if ( firstChild && firstChild.nodeType == 3 )
			node.appendChild( FCKXHtml.XML.createTextNode( FCKXHtml._AppendSpecialItem( '\r\n' ) ) ) ;

		FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

		return node ;
	},

	script : function( node, htmlNode )
	{

		if ( ! node.attributes.getNamedItem( 'type' ) )
			FCKXHtml._AppendAttribute( node, 'type', 'text/javascript' ) ;

		node.appendChild( FCKXHtml.XML.createTextNode( FCKXHtml._AppendSpecialItem( htmlNode.text ) ) ) ;

		return node ;
	},

	span : function( node, htmlNode )
	{

		if ( htmlNode.innerHTML.length == 0 )
			return false ;

		node = FCKXHtml._AppendChildNodes( node, htmlNode, false ) ;

		return node ;
	},

	style : function( node, htmlNode )
	{

		if ( ! node.attributes.getNamedItem( 'type' ) )
			FCKXHtml._AppendAttribute( node, 'type', 'text/css' ) ;

		var cssText = htmlNode.innerHTML ;
		if ( FCKBrowserInfo.IsIE )	// Bug #403 : IE always appends a \r\n to the beginning of StyleNode.innerHTML
			cssText = cssText.replace( /^(\r\n|\n|\r)/, '' ) ;

		node.appendChild( FCKXHtml.XML.createTextNode( FCKXHtml._AppendSpecialItem( cssText ) ) ) ;

		return node ;
	},

	title : function( node, htmlNode )
	{
		node.appendChild( FCKXHtml.XML.createTextNode( FCK.EditorDocument.title ) ) ;

		return node ;
	}
} ;

FCKXHtml.TagProcessors.ul = FCKXHtml.TagProcessors.ol ;