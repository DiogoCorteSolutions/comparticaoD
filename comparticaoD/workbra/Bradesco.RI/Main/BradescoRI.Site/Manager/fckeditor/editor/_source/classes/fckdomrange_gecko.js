FCKDomRange.prototype.MoveToSelection = function()
{
	this.Release( true ) ;

	var oSel = this.Window.getSelection() ;

	if ( oSel && oSel.rangeCount > 0 )
	{
		this._Range = FCKW3CRange.CreateFromRange( this.Window.document, oSel.getRangeAt(0) ) ;
		this._UpdateElementInfo() ;
	}
	else
		if ( this.Window.document )
			this.MoveToElementStart( this.Window.document.body ) ;
}

FCKDomRange.prototype.Select = function()
{
	var oRange = this._Range ;
	if ( oRange )
	{
		var startContainer = oRange.startContainer ;

		if ( oRange.collapsed && startContainer.nodeType == 1 && startContainer.childNodes.length == 0 )
			startContainer.appendChild( oRange._Document.createTextNode('') ) ;

		var oDocRange = this.Window.document.createRange() ;
		oDocRange.setStart( startContainer, oRange.startOffset ) ;

		try
		{
			oDocRange.setEnd( oRange.endContainer, oRange.endOffset ) ;
		}
		catch ( e )
		{
			if ( e.toString().Contains( 'NS_ERROR_ILLEGAL_VALUE' ) )
			{
				oRange.collapse( true ) ;
				oDocRange.setEnd( oRange.endContainer, oRange.endOffset ) ;
			}
			else
				throw( e ) ;
		}

		var oSel = this.Window.getSelection() ;
		oSel.removeAllRanges() ;

		oSel.addRange( oDocRange ) ;
	}
}

FCKDomRange.prototype.SelectBookmark = function( bookmark )
{
	var domRange = this.Window.document.createRange() ;

	var startNode	= this.GetBookmarkNode( bookmark, true ) ;
	var endNode		= this.GetBookmarkNode( bookmark, false ) ;

	domRange.setStart( startNode.parentNode, FCKDomTools.GetIndexOf( startNode ) ) ;
	FCKDomTools.RemoveNode( startNode ) ;

	if ( endNode )
	{
		domRange.setEnd( endNode.parentNode, FCKDomTools.GetIndexOf( endNode ) ) ;
		FCKDomTools.RemoveNode( endNode ) ;
	}

	var selection = this.Window.getSelection() ;
	selection.removeAllRanges() ;
	selection.addRange( domRange ) ;
}