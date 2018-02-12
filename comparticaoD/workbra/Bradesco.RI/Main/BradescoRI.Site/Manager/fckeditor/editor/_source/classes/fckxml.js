var FCKXml = function()
{
	this.Error = false ;
}

FCKXml.GetAttribute = function( node, attName, defaultValue )
{
	var attNode = node.attributes.getNamedItem( attName ) ;
	return attNode ? attNode.value : defaultValue ;
}

FCKXml.TransformToObject = function( element )
{
	if ( !element )
		return null ;

	var obj = {} ;

	var attributes = element.attributes ;
	for ( var i = 0 ; i < attributes.length ; i++ )
	{
		var att = attributes[i] ;
		obj[ att.name ] = att.value ;
	}

	var childNodes = element.childNodes ;
	for ( i = 0 ; i < childNodes.length ; i++ )
	{
		var child = childNodes[i] ;

		if ( child.nodeType == 1 )
		{
			var childName = '$' + child.nodeName ;
			var childList = obj[ childName ] ;
			if ( !childList )
				childList = obj[ childName ] = [] ;

			childList.push( this.TransformToObject( child ) ) ;
		}
	}

	return obj ;
}