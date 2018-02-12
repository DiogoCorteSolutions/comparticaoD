FCKXHtml._GetMainXmlString = function()
{
	return ( new XMLSerializer() ).serializeToString( this.MainNode ) ;
}

FCKXHtml._AppendAttributes = function( xmlNode, htmlNode, node )
{
	var aAttributes = htmlNode.attributes ;

	for ( var n = 0 ; n < aAttributes.length ; n++ )
	{
		var oAttribute = aAttributes[n] ;

		if ( oAttribute.specified )
		{
			var sAttName = oAttribute.nodeName.toLowerCase() ;
			var sAttValue ;


			if ( sAttName.StartsWith( '_fck' ) )
				continue ;

			else if ( sAttName.indexOf( '_moz' ) == 0 )
				continue ;


			else if ( sAttName == 'class' )
			{
				sAttValue = oAttribute.nodeValue.replace( FCKRegexLib.FCK_Class, '' ) ;
				if ( sAttValue.length == 0 )
					continue ;
			}

			else if ( oAttribute.nodeValue === true )
				sAttValue = sAttName ;
			else
				sAttValue = htmlNode.getAttribute( sAttName, 2 ) ;

			this._AppendAttribute( node, sAttName, sAttValue ) ;
		}
	}
}

if ( FCKBrowserInfo.IsOpera )
{




	FCKXHtml.TagProcessors['head'] = function( node, htmlNode )
	{
		FCKXHtml.XML._HeadElement = node ;

		node = FCKXHtml._AppendChildNodes( node, htmlNode, true ) ;

		return node ;
	}



	FCKXHtml.TagProcessors['meta'] = function( node, htmlNode, xmlNode )
	{
		if ( htmlNode.parentNode.nodeName.toLowerCase() != 'head' )
		{
			var headElement = FCKXHtml.XML._HeadElement ;

			if ( headElement && xmlNode != headElement )
			{
				delete htmlNode._fckxhtmljob ;
				FCKXHtml._AppendNode( headElement, htmlNode ) ;
				return null ;
			}
		}

		return node ;
	}
}

if ( FCKBrowserInfo.IsGecko )
{

	FCKXHtml.TagProcessors['link'] = function( node, htmlNode )
	{
		if ( htmlNode.href.substr(0, 9).toLowerCase() == 'chrome://' )
			return false ;

		return node ;
	}

}