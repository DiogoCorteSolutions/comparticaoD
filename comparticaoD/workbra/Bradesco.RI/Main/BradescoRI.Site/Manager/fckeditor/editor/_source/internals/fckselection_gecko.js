FCKSelection.GetType = function()
{

	var type = 'Text' ;



	var sel ;
	try { sel = this.GetSelection() ; } catch (e) {}

	if ( sel && sel.rangeCount == 1 )
	{
		var range = sel.getRangeAt(0) ;
		if ( range.startContainer == range.endContainer
			&& ( range.endOffset - range.startOffset ) == 1
			&& range.startContainer.nodeType == 1
			&& FCKListsLib.StyleObjectElements[ range.startContainer.childNodes[ range.startOffset ].nodeName.toLowerCase() ] )
		{
			type = 'Control' ;
		}
	}

	return type ;
}



FCKSelection.GetSelectedElement = function()
{
	var selection = !!FCK.EditorWindow && this.GetSelection() ;
	if ( !selection || selection.rangeCount < 1 )
		return null ;

	var range = selection.getRangeAt( 0 ) ;
	if ( range.startContainer != range.endContainer || range.startContainer.nodeType != 1 || range.startOffset != range.endOffset - 1 )
		return null ;

	var node = range.startContainer.childNodes[ range.startOffset ] ;
	if ( node.nodeType != 1 )
		return null ;

	return node ;
}

FCKSelection.GetParentElement = function()
{
	if ( this.GetType() == 'Control' )
		return FCKSelection.GetSelectedElement().parentNode ;
	else
	{
		var oSel = this.GetSelection() ;
		if ( oSel )
		{



			if ( oSel.anchorNode && oSel.anchorNode == oSel.focusNode )
			{
				var oRange = oSel.getRangeAt( 0 ) ;
				if ( oRange.collapsed || oRange.startContainer.nodeType == 3 )
					return oSel.anchorNode.parentNode ;
				else
					return oSel.anchorNode ;
			}




			var anchorPath = new FCKElementPath( oSel.anchorNode ) ;
			var focusPath = new FCKElementPath( oSel.focusNode ) ;
			var deepPath = null ;
			var shallowPath = null ;
			if ( anchorPath.Elements.length > focusPath.Elements.length )
			{
				deepPath = anchorPath.Elements ;
				shallowPath = focusPath.Elements ;
			}
			else
			{
				deepPath = focusPath.Elements ;
				shallowPath = anchorPath.Elements ;
			}

			var deepPathBase = deepPath.length - shallowPath.length ;
			for( var i = 0 ; i < shallowPath.length ; i++)
			{
				if ( deepPath[deepPathBase + i] == shallowPath[i])
					return shallowPath[i];
			}
			return null ;
		}
	}
	return null ;
}

FCKSelection.GetBoundaryParentElement = function( startBoundary )
{
	if ( ! FCK.EditorWindow )
		return null ;
	if ( this.GetType() == 'Control' )
		return FCKSelection.GetSelectedElement().parentNode ;
	else
	{
		var oSel = this.GetSelection() ;
		if ( oSel && oSel.rangeCount > 0 )
		{
			var range = oSel.getRangeAt( startBoundary ? 0 : ( oSel.rangeCount - 1 ) ) ;

			var element = startBoundary ? range.startContainer : range.endContainer ;

			return ( element.nodeType == 1 ? element : element.parentNode ) ;
		}
	}
	return null ;
}

FCKSelection.SelectNode = function( element )
{
	var oRange = FCK.EditorDocument.createRange() ;
	oRange.selectNode( element ) ;

	var oSel = this.GetSelection() ;
	oSel.removeAllRanges() ;
	oSel.addRange( oRange ) ;
}

FCKSelection.Collapse = function( toStart )
{
	var oSel = this.GetSelection() ;

	if ( toStart == null || toStart === true )
		oSel.collapseToStart() ;
	else
		oSel.collapseToEnd() ;
}


FCKSelection.HasAncestorNode = function( nodeTagName )
{
	var oContainer = this.GetSelectedElement() ;
	if ( ! oContainer && FCK.EditorWindow )
	{
		try		{ oContainer = this.GetSelection().getRangeAt(0).startContainer ; }
		catch(e){}
	}

	while ( oContainer )
	{
		if ( oContainer.nodeType == 1 && oContainer.nodeName.IEquals( nodeTagName ) ) return true ;
		oContainer = oContainer.parentNode ;
	}

	return false ;
}


FCKSelection.MoveToAncestorNode = function( nodeTagName )
{
	var oNode ;

	var oContainer = this.GetSelectedElement() ;
	if ( ! oContainer )
		oContainer = this.GetSelection().getRangeAt(0).startContainer ;

	while ( oContainer )
	{
		if ( oContainer.nodeName.IEquals( nodeTagName ) )
			return oContainer ;

		oContainer = oContainer.parentNode ;
	}
	return null ;
}

FCKSelection.Delete = function()
{

	var oSel = this.GetSelection() ;


	for ( var i = 0 ; i < oSel.rangeCount ; i++ )
	{
		oSel.getRangeAt(i).deleteContents() ;
	}

	return oSel ;
}

FCKSelection.GetSelection = function()
{
	return FCK.EditorWindow.getSelection() ;
}



FCKSelection.Save = function()
{}
FCKSelection.Restore = function()
{}
FCKSelection.Release = function()
{}