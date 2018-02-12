FCKXHtml._GetMainXmlString = function()
{
	return this.MainNode.xml ;
}

FCKXHtml._AppendAttributes = function( xmlNode, htmlNode, node, nodeName )
{
	var aAttributes = htmlNode.attributes,
		bHasStyle ;

	for ( var n = 0 ; n < aAttributes.length ; n++ )
	{
		var oAttribute = aAttributes[n] ;

		if ( oAttribute.specified )
		{
			var sAttName = oAttribute.nodeName.toLowerCase() ;
			var sAttValue ;


			if ( sAttName.StartsWith( '_fck' ) )
				continue ;


			else if ( sAttName == 'style' )
			{

				bHasStyle = true ;
				continue ;
			}



			else if ( sAttName == 'class' )
			{
				sAttValue = oAttribute.nodeValue.replace( FCKRegexLib.FCK_Class, '' ) ;
				if ( sAttValue.length == 0 )
					continue ;
			}
			else if ( sAttName.indexOf('on') == 0 )
				sAttValue = oAttribute.nodeValue ;
			else if ( nodeName == 'body' && sAttName == 'contenteditable' )
				continue ;

			else if ( oAttribute.nodeValue === true )
				sAttValue = sAttName ;
			else
			{


				try
				{
					sAttValue = htmlNode.getAttribute( sAttName, 2 ) ;
				}
				catch (e) {}
			}
			this._AppendAttribute( node, sAttName, sAttValue || oAttribute.nodeValue ) ;
		}
	}


	if ( bHasStyle || htmlNode.style.cssText.length > 0 )
	{
		var data = FCKTools.ProtectFormStyles( htmlNode ) ;
		var sStyleValue = htmlNode.style.cssText.replace( FCKRegexLib.StyleProperties, FCKTools.ToLowerCase ) ;
		FCKTools.RestoreFormStyles( htmlNode, data ) ;
		this._AppendAttribute( node, 'style', sStyleValue ) ;
	}
}


FCKXHtml.TagProcessors['div'] = function( node, htmlNode )
{
	if ( htmlNode.align.length > 0 )
		FCKXHtml._AppendAttribute( node, 'align', htmlNode.align ) ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

	return node ;
}


FCKXHtml.TagProcessors['font'] = function( node, htmlNode )
{
	if ( node.attributes.length == 0 )
		node = FCKXHtml.XML.createDocumentFragment() ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode ) ;

	return node ;
}

FCKXHtml.TagProcessors['form'] = function( node, htmlNode )
{
	if ( htmlNode.acceptCharset && htmlNode.acceptCharset.length > 0 && htmlNode.acceptCharset != 'UNKNOWN' )
		FCKXHtml._AppendAttribute( node, 'accept-charset', htmlNode.acceptCharset ) ;



	var nameAtt = htmlNode.attributes['name'] ;

	if ( nameAtt && nameAtt.value.length > 0 )
		FCKXHtml._AppendAttribute( node, 'name', nameAtt.value ) ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

	return node ;
}


FCKXHtml.TagProcessors['input'] = function( node, htmlNode )
{
	if ( htmlNode.name )
		FCKXHtml._AppendAttribute( node, 'name', htmlNode.name ) ;

	if ( htmlNode.value && !node.attributes.getNamedItem( 'value' ) )
		FCKXHtml._AppendAttribute( node, 'value', htmlNode.value ) ;

	if ( !node.attributes.getNamedItem( 'type' ) )
		FCKXHtml._AppendAttribute( node, 'type', 'text' ) ;

	return node ;
}

FCKXHtml.TagProcessors['label'] = function( node, htmlNode )
{
	if ( htmlNode.htmlFor.length > 0 )
		FCKXHtml._AppendAttribute( node, 'for', htmlNode.htmlFor ) ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode ) ;

	return node ;
}


FCKXHtml.TagProcessors['map'] = function( node, htmlNode )
{
	if ( ! node.attributes.getNamedItem( 'name' ) )
	{
		var name = htmlNode.name ;
		if ( name )
			FCKXHtml._AppendAttribute( node, 'name', name ) ;
	}

	node = FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

	return node ;
}

FCKXHtml.TagProcessors['meta'] = function( node, htmlNode )
{
	var oHttpEquiv = node.attributes.getNamedItem( 'http-equiv' ) ;

	if ( oHttpEquiv == null || oHttpEquiv.value.length == 0 )
	{

		var sHttpEquiv = htmlNode.outerHTML.match( FCKRegexLib.MetaHttpEquiv ) ;

		if ( sHttpEquiv )
		{
			sHttpEquiv = sHttpEquiv[1] ;
			FCKXHtml._AppendAttribute( node, 'http-equiv', sHttpEquiv ) ;
		}
	}

	return node ;
}


FCKXHtml.TagProcessors['option'] = function( node, htmlNode )
{
	if ( htmlNode.selected && !node.attributes.getNamedItem( 'selected' ) )
		FCKXHtml._AppendAttribute( node, 'selected', 'selected' ) ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode ) ;

	return node ;
}


FCKXHtml.TagProcessors['textarea'] = FCKXHtml.TagProcessors['select'] = function( node, htmlNode )
{
	if ( htmlNode.name )
		FCKXHtml._AppendAttribute( node, 'name', htmlNode.name ) ;

	node = FCKXHtml._AppendChildNodes( node, htmlNode ) ;

	return node ;
}