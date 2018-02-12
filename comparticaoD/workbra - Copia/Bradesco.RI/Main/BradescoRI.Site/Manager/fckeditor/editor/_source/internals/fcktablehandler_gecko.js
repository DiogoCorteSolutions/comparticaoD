FCKTableHandler.GetSelectedCells = function()
{
	var aCells = new Array() ;

	var oSelection = FCKSelection.GetSelection() ;


	if ( oSelection.rangeCount == 1 && oSelection.anchorNode.nodeType == 3 )
	{
		var oParent = FCKTools.GetElementAscensor( oSelection.anchorNode, 'TD,TH' ) ;

		if ( oParent )
			aCells[0] = oParent ;

		return aCells ;
	}

	for ( var i = 0 ; i < oSelection.rangeCount ; i++ )
	{
		var oRange = oSelection.getRangeAt(i) ;
		var oCell ;

		if ( oRange.startContainer.tagName.Equals( 'TD', 'TH' ) )
			oCell = oRange.startContainer ;
		else
			oCell = oRange.startContainer.childNodes[ oRange.startOffset ] ;

		if ( oCell.nodeName.Equals( 'TD', 'TH' ) )
			aCells[aCells.length] = oCell ;
	}

	return aCells ;
}