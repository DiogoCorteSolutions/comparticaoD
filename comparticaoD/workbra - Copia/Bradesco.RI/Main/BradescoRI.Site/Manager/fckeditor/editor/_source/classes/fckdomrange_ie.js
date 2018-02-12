FCKDomRange.prototype.MoveToSelection = function()
{
	this.Release( true ) ;

	this._Range = new FCKW3CRange( this.Window.document ) ;

	var oSel = this.Window.document.selection ;

	if ( oSel.type != 'Control' )
	{
		var eMarkerStart	= this._GetSelectionMarkerTag( true ) ;
		var eMarkerEnd		= this._GetSelectionMarkerTag( false ) ;

		if ( !eMarkerStart && !eMarkerEnd )
		{
			this._Range.setStart( this.Window.document.body, 0 ) ;
			this._UpdateElementInfo() ;
			return ;
		}

		this._Range.setStart( eMarkerStart.parentNode, FCKDomTools.GetIndexOf( eMarkerStart ) ) ;
		eMarkerStart.parentNode.removeChild( eMarkerStart ) ;

		this._Range.setEnd( eMarkerEnd.parentNode, FCKDomTools.GetIndexOf( eMarkerEnd ) ) ;
		eMarkerEnd.parentNode.removeChild( eMarkerEnd ) ;

		this._UpdateElementInfo() ;
	}
	else
	{
		var oControl = oSel.createRange().item(0) ;

		if ( oControl )
		{
			this._Range.setStartBefore( oControl ) ;
			this._Range.setEndAfter( oControl ) ;
			this._UpdateElementInfo() ;
		}
	}
}

FCKDomRange.prototype.Select = function( forceExpand )
{
	if ( this._Range )
		this.SelectBookmark( this.CreateBookmark( true ), forceExpand ) ;
}

FCKDomRange.prototype.SelectBookmark = function( bookmark, forceExpand )
{
	var bIsCollapsed = this.CheckIsCollapsed() ;
	var bIsStartMakerAlone ;
	var dummySpan ;

	var eStartMarker = this.GetBookmarkNode( bookmark, true ) ;

	if ( !eStartMarker )
		return ;

	var eEndMarker ;
	if ( !bIsCollapsed )
		eEndMarker = this.GetBookmarkNode( bookmark, false ) ;

	var oIERange = this.Window.document.body.createTextRange() ;

	oIERange.moveToElementText( eStartMarker ) ;
	oIERange.moveStart( 'character', 1 ) ;

	if ( eEndMarker )
	{
		var oIERangeEnd = this.Window.document.body.createTextRange() ;

		oIERangeEnd.moveToElementText( eEndMarker ) ;

		oIERange.setEndPoint( 'EndToEnd', oIERangeEnd ) ;
		oIERange.moveEnd( 'character', -1 ) ;
	}
	else
	{
		bIsStartMakerAlone = ( forceExpand || !eStartMarker.previousSibling || eStartMarker.previousSibling.nodeName.toLowerCase() == 'br' ) && !eStartMarker.nextSibing ;

		dummySpan = this.Window.document.createElement( 'span' ) ;
		dummySpan.innerHTML = '&#65279;' ;
		eStartMarker.parentNode.insertBefore( dummySpan, eStartMarker ) ;

		if ( bIsStartMakerAlone )
		{
			eStartMarker.parentNode.insertBefore( this.Window.document.createTextNode( '\ufeff' ), eStartMarker ) ;
		}
	}

	if ( !this._Range )
		this._Range = this.CreateRange() ;

	this._Range.setStartBefore( eStartMarker ) ;
	eStartMarker.parentNode.removeChild( eStartMarker ) ;

	if ( bIsCollapsed )
	{
		if ( bIsStartMakerAlone )
		{
			oIERange.moveStart( 'character', -1 ) ;

			oIERange.select() ;

			this.Window.document.selection.clear() ;
		}
		else
			oIERange.select() ;

		FCKDomTools.RemoveNode( dummySpan ) ;
	}
	else
	{
		this._Range.setEndBefore( eEndMarker ) ;
		eEndMarker.parentNode.removeChild( eEndMarker ) ;
		oIERange.select() ;
	}
}

FCKDomRange.prototype._GetSelectionMarkerTag = function( toStart )
{
	var doc = this.Window.document ;
	var selection = doc.selection ;

	var oRange ;

	try
	{
		oRange = selection.createRange() ;
	}
	catch (e)
	{
		return null ;
	}

	if ( oRange.parentElement().document != doc )
		return null ;

	oRange.collapse( toStart === true ) ;

	var sMarkerId = 'fck_dom_range_temp_' + (new Date()).valueOf() + '_' + Math.floor(Math.random()*1000) ;
	oRange.pasteHTML( '<span id="' + sMarkerId + '"></span>' ) ;

	return doc.getElementById( sMarkerId ) ;
}