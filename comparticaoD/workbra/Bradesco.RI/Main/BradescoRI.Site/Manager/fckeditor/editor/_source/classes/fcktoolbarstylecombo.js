var FCKToolbarStyleCombo = function( tooltip, style )
{
	if ( tooltip === false )
		return ;

	this.CommandName = 'Style' ;
	this.Label		= this.GetLabel() ;
	this.Tooltip	= tooltip ? tooltip : this.Label ;
	this.Style		= style ? style : FCK_TOOLBARITEM_ICONTEXT ;

	this.DefaultLabel = FCKConfig.DefaultStyleLabel || '' ;
}


FCKToolbarStyleCombo.prototype = new FCKToolbarSpecialCombo ;

FCKToolbarStyleCombo.prototype.GetLabel = function()
{
	return FCKLang.Style ;
}

FCKToolbarStyleCombo.prototype.GetStyles = function()
{
	var styles = {} ;
	var allStyles = FCK.ToolbarSet.CurrentInstance.Styles.GetStyles() ;

	for ( var styleName in allStyles )
	{
		var style = allStyles[ styleName ] ;
		if ( !style.IsCore )
			styles[ styleName ] = style ;
	}
	return styles ;
}

FCKToolbarStyleCombo.prototype.CreateItems = function( targetSpecialCombo )
{
	var targetDoc = targetSpecialCombo._Panel.Document ;


	FCKTools.AppendStyleSheet( targetDoc, FCKConfig.ToolbarComboPreviewCSS ) ;
	FCKTools.AppendStyleString( targetDoc, FCKConfig.EditorAreaStyles ) ;
	targetDoc.body.className += ' ForceBaseFont' ;


	FCKConfig.ApplyBodyAttributes( targetDoc.body ) ;


	var styles = this.GetStyles() ;

	for ( var styleName in styles )
	{
		var style = styles[ styleName ] ;


		var caption = style.GetType() == FCK_STYLE_OBJECT ?
			styleName :
			FCKToolbarStyleCombo_BuildPreview( style, style.Label || styleName ) ;

		var item = targetSpecialCombo.AddItem( styleName, caption ) ;

		item.Style = style ;
	}


	targetSpecialCombo.OnBeforeClick = this.StyleCombo_OnBeforeClick ;
}

FCKToolbarStyleCombo.prototype.RefreshActiveItems = function( targetSpecialCombo )
{
	var startElement = FCK.ToolbarSet.CurrentInstance.Selection.GetBoundaryParentElement( true ) ;

	if ( startElement )
	{
		var path = new FCKElementPath( startElement ) ;
		var elements = path.Elements ;

		for ( var e = 0 ; e < elements.length ; e++ )
		{
			for ( var i in targetSpecialCombo.Items )
			{
				var item = targetSpecialCombo.Items[i] ;
				var style = item.Style ;

				if ( style.CheckElementRemovable( elements[ e ], true ) )
				{
					targetSpecialCombo.SetLabel( style.Label || style.Name ) ;
					return ;
				}
			}
		}
	}

	targetSpecialCombo.SetLabel( this.DefaultLabel ) ;
}

FCKToolbarStyleCombo.prototype.StyleCombo_OnBeforeClick = function( targetSpecialCombo )
{






	targetSpecialCombo.DeselectAll() ;

	var startElement ;
	var path ;
	var tagName ;

	var selection = FCK.ToolbarSet.CurrentInstance.Selection ;

	if ( selection.GetType() == 'Control' )
	{
		startElement = selection.GetSelectedElement() ;
		tagName = startElement.nodeName.toLowerCase() ;
	}
	else
	{
		startElement = selection.GetBoundaryParentElement( true ) ;
		path = new FCKElementPath( startElement ) ;
	}

	for ( var i in targetSpecialCombo.Items )
	{
		var item = targetSpecialCombo.Items[i] ;
		var style = item.Style ;

		if ( ( tagName && style.Element == tagName ) || ( !tagName && style.GetType() != FCK_STYLE_OBJECT ) )
		{
			item.style.display = '' ;

			if ( ( path && style.CheckActive( path ) ) || ( !path && style.CheckElementRemovable( startElement, true ) ) )
				targetSpecialCombo.SelectItem( style.Name ) ;
		}
		else
			item.style.display = 'none' ;
	}
}

function FCKToolbarStyleCombo_BuildPreview( style, caption )
{
	var styleType = style.GetType() ;
	var html = [] ;

	if ( styleType == FCK_STYLE_BLOCK )
		html.push( '<div class="BaseFont">' ) ;

	var elementName = style.Element ;


	if ( elementName == 'bdo' )
		elementName = 'span' ;

	html = [ '<', elementName ] ;


	var attribs	= style._StyleDesc.Attributes ;
	if ( attribs )
	{
		for ( var att in attribs )
		{
			html.push( ' ', att, '="', style.GetFinalAttributeValue( att ), '"' ) ;
		}
	}


	if ( style._GetStyleText().length > 0 )
		html.push( ' style="', style.GetFinalStyleValue(), '"' ) ;

	html.push( '>', caption, '</', elementName, '>' ) ;

	if ( styleType == FCK_STYLE_BLOCK )
		html.push( '</div>' ) ;

	return html.join( '' ) ;
}