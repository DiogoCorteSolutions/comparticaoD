var FCKEnterKey = function( targetWindow, enterMode, shiftEnterMode, tabSpaces )
{
	this.Window			= targetWindow ;
	this.EnterMode		= enterMode || 'p' ;
	this.ShiftEnterMode	= shiftEnterMode || 'br' ;

	var oKeystrokeHandler = new FCKKeystrokeHandler( false ) ;
	oKeystrokeHandler._EnterKey = this ;
	oKeystrokeHandler.OnKeystroke = FCKEnterKey_OnKeystroke ;

	oKeystrokeHandler.SetKeystrokes( [
		[ 13		, 'Enter' ],
		[ SHIFT + 13, 'ShiftEnter' ],
		[ 8			, 'Backspace' ],
		[ CTRL + 8	, 'CtrlBackspace' ],
		[ 46		, 'Delete' ]
	] ) ;

	this.TabText = '' ;

	if ( tabSpaces > 0 || FCKBrowserInfo.IsSafari )
	{
		while ( tabSpaces-- )
			this.TabText += '\xa0' ;

		oKeystrokeHandler.SetKeystrokes( [ 9, 'Tab' ] );
	}

	oKeystrokeHandler.AttachToElement( targetWindow.document ) ;
}


function FCKEnterKey_OnKeystroke(  keyCombination, keystrokeValue )
{
	var oEnterKey = this._EnterKey ;

	try
	{
		switch ( keystrokeValue )
		{
			case 'Enter' :
				return oEnterKey.DoEnter() ;
				break ;
			case 'ShiftEnter' :
				return oEnterKey.DoShiftEnter() ;
				break ;
			case 'Backspace' :
				return oEnterKey.DoBackspace() ;
				break ;
			case 'Delete' :
				return oEnterKey.DoDelete() ;
				break ;
			case 'Tab' :
				return oEnterKey.DoTab() ;
				break ;
			case 'CtrlBackspace' :
				return oEnterKey.DoCtrlBackspace() ;
				break ;
		}
	}
	catch (e)
	{
	}

	return false ;
}

FCKEnterKey.prototype.DoEnter = function( mode, hasShift )
{
	FCKUndo.SaveUndoStep() ;

	this._HasShift = ( hasShift === true ) ;

	var parentElement = FCKSelection.GetParentElement() ;
	var parentPath = new FCKElementPath( parentElement ) ;
	var sMode = mode || this.EnterMode ;

	if ( sMode == 'br' || parentPath.Block && parentPath.Block.tagName.toLowerCase() == 'pre' )
		return this._ExecuteEnterBr() ;
	else
		return this._ExecuteEnterBlock( sMode ) ;
}

FCKEnterKey.prototype.DoShiftEnter = function()
{
	return this.DoEnter( this.ShiftEnterMode, true ) ;
}

FCKEnterKey.prototype.DoBackspace = function()
{
	var bCustom = false ;

	var oRange = new FCKDomRange( this.Window ) ;
	oRange.MoveToSelection() ;

	if ( FCKBrowserInfo.IsIE && this._CheckIsAllContentsIncluded( oRange, this.Window.document.body ) )
	{
		this._FixIESelectAllBug( oRange ) ;
		return true ;
	}

	var isCollapsed = oRange.CheckIsCollapsed() ;

	if ( !isCollapsed )
	{
		if ( FCKBrowserInfo.IsIE && this.Window.document.selection.type.toLowerCase() == "control" )
		{
			var controls = this.Window.document.selection.createRange() ;
			for ( var i = controls.length - 1 ; i >= 0 ; i-- )
			{
				var el = controls.item( i ) ;
				el.parentNode.removeChild( el ) ;
			}
			return true ;
		}

		return false ;
	}

	if ( FCKBrowserInfo.IsIE )
	{
		var previousElement = FCKDomTools.GetPreviousSourceElement( oRange.StartNode, true ) ;

		if ( previousElement && previousElement.nodeName.toLowerCase() == 'br' )
		{
			var testRange = oRange.Clone() ;
			testRange.SetStart( previousElement, 4 ) ;

			if ( testRange.CheckIsEmpty() )
			{
				previousElement.parentNode.removeChild( previousElement ) ;
				return true ;
			}
		}
	}

	var oStartBlock = oRange.StartBlock ;
	var oEndBlock = oRange.EndBlock ;

	if ( oRange.StartBlockLimit == oRange.EndBlockLimit && oStartBlock && oEndBlock )
	{
		if ( !isCollapsed )
		{
			var bEndOfBlock = oRange.CheckEndOfBlock() ;

			oRange.DeleteContents() ;

			if ( oStartBlock != oEndBlock )
			{
				oRange.SetStart(oEndBlock,1) ;
				oRange.SetEnd(oEndBlock,1) ;
			}

			oRange.Select() ;

			bCustom = ( oStartBlock == oEndBlock ) ;
		}

		if ( oRange.CheckStartOfBlock() )
		{
			var oCurrentBlock = oRange.StartBlock ;

			var ePrevious = FCKDomTools.GetPreviousSourceElement( oCurrentBlock, true, [ 'BODY', oRange.StartBlockLimit.nodeName ], ['UL','OL'] ) ;

			bCustom = this._ExecuteBackspace( oRange, ePrevious, oCurrentBlock ) ;
		}
		else if ( FCKBrowserInfo.IsGeckoLike )
		{
			oRange.Select() ;
		}
	}

	oRange.Release() ;
	return bCustom ;
}

FCKEnterKey.prototype.DoCtrlBackspace = function()
{
	FCKUndo.SaveUndoStep() ;
	var oRange = new FCKDomRange( this.Window ) ;
	oRange.MoveToSelection() ;
	if ( FCKBrowserInfo.IsIE && this._CheckIsAllContentsIncluded( oRange, this.Window.document.body ) )
	{
		this._FixIESelectAllBug( oRange ) ;
		return true ;
	}
	return false ;
}

FCKEnterKey.prototype._ExecuteBackspace = function( range, previous, currentBlock )
{
	var bCustom = false ;

	if ( !previous && currentBlock && currentBlock.nodeName.IEquals( 'LI' ) && currentBlock.parentNode.parentNode.nodeName.IEquals( 'LI' ) )
	{
		this._OutdentWithSelection( currentBlock, range ) ;
		return true ;
	}

	if ( previous && previous.nodeName.IEquals( 'LI' ) )
	{
		var oNestedList = FCKDomTools.GetLastChild( previous, ['UL','OL'] ) ;

		while ( oNestedList )
		{
			previous = FCKDomTools.GetLastChild( oNestedList, 'LI' ) ;
			oNestedList = FCKDomTools.GetLastChild( previous, ['UL','OL'] ) ;
		}
	}

	if ( previous && currentBlock )
	{
		if ( currentBlock.nodeName.IEquals( 'LI' ) && !previous.nodeName.IEquals( 'LI' ) )
		{
			this._OutdentWithSelection( currentBlock, range ) ;
			return true ;
		}

		var oCurrentParent = currentBlock.parentNode ;

		var sPreviousName = previous.nodeName.toLowerCase() ;
		if ( FCKListsLib.EmptyElements[ sPreviousName ] != null || sPreviousName == 'table' )
		{
			FCKDomTools.RemoveNode( previous ) ;
			bCustom = true ;
		}
		else
		{
			FCKDomTools.RemoveNode( currentBlock ) ;

			while ( oCurrentParent.innerHTML.Trim().length == 0 )
			{
				var oParent = oCurrentParent.parentNode ;
				oParent.removeChild( oCurrentParent ) ;
				oCurrentParent = oParent ;
			}

			FCKDomTools.LTrimNode( currentBlock ) ;
			FCKDomTools.RTrimNode( previous ) ;

			range.SetStart( previous, 2, true ) ;
			range.Collapse( true ) ;
			var oBookmark = range.CreateBookmark( true ) ;

			if ( ! currentBlock.tagName.IEquals( [ 'TABLE' ] ) )
				FCKDomTools.MoveChildren( currentBlock, previous ) ;

			range.SelectBookmark( oBookmark ) ;

			bCustom = true ;
		}
	}

	return bCustom ;
}

FCKEnterKey.prototype.DoDelete = function()
{
	FCKUndo.SaveUndoStep() ;

	var bCustom = false ;

	var oRange = new FCKDomRange( this.Window ) ;
	oRange.MoveToSelection() ;

	if ( FCKBrowserInfo.IsIE && this._CheckIsAllContentsIncluded( oRange, this.Window.document.body ) )
	{
		this._FixIESelectAllBug( oRange ) ;
		return true ;
	}

	if ( oRange.CheckIsCollapsed() && oRange.CheckEndOfBlock( FCKBrowserInfo.IsGeckoLike ) )
	{
		var oCurrentBlock = oRange.StartBlock ;
		var eCurrentCell = FCKTools.GetElementAscensor( oCurrentBlock, 'td' );

		var eNext = FCKDomTools.GetNextSourceElement( oCurrentBlock, true, [ oRange.StartBlockLimit.nodeName ],
				['UL','OL','TR'], true ) ;

		if ( eCurrentCell )
		{
			var eNextCell = FCKTools.GetElementAscensor( eNext, 'td' );
			if ( eNextCell != eCurrentCell )
				return true ;
		}

		bCustom = this._ExecuteBackspace( oRange, oCurrentBlock, eNext ) ;
	}

	oRange.Release() ;
	return bCustom ;
}

FCKEnterKey.prototype.DoTab = function()
{
	var oRange = new FCKDomRange( this.Window );
	oRange.MoveToSelection() ;

	var node = oRange._Range.startContainer ;
	while ( node )
	{
		if ( node.nodeType == 1 )
		{
			var tagName = node.tagName.toLowerCase() ;
			if ( tagName == "tr" || tagName == "td" || tagName == "th" || tagName == "tbody" || tagName == "table" )
				return false ;
			else
				break ;
		}
		node = node.parentNode ;
	}

	if ( this.TabText )
	{
		oRange.DeleteContents() ;
		oRange.InsertNode( this.Window.document.createTextNode( this.TabText ) ) ;
		oRange.Collapse( false ) ;
		oRange.Select() ;
	}
	return true ;
}

FCKEnterKey.prototype._ExecuteEnterBlock = function( blockTag, range )
{
	var oRange = range || new FCKDomRange( this.Window ) ;

	var oSplitInfo = oRange.SplitBlock( blockTag ) ;

	if ( oSplitInfo )
	{
		var ePreviousBlock	= oSplitInfo.PreviousBlock ;
		var eNextBlock		= oSplitInfo.NextBlock ;

		var bIsStartOfBlock	= oSplitInfo.WasStartOfBlock ;
		var bIsEndOfBlock	= oSplitInfo.WasEndOfBlock ;

		if ( eNextBlock )
		{
			if ( eNextBlock.parentNode.nodeName.IEquals( 'li' ) )
			{
				FCKDomTools.BreakParent( eNextBlock, eNextBlock.parentNode ) ;
				FCKDomTools.MoveNode( eNextBlock, eNextBlock.nextSibling, true ) ;
			}
		}
		else if ( ePreviousBlock && ePreviousBlock.parentNode.nodeName.IEquals( 'li' ) )
		{
			FCKDomTools.BreakParent( ePreviousBlock, ePreviousBlock.parentNode ) ;
			oRange.MoveToElementEditStart( ePreviousBlock.nextSibling );
			FCKDomTools.MoveNode( ePreviousBlock, ePreviousBlock.previousSibling ) ;
		}

		if ( !bIsStartOfBlock && !bIsEndOfBlock )
		{
			if ( eNextBlock.nodeName.IEquals( 'li' ) && eNextBlock.firstChild
					&& eNextBlock.firstChild.nodeName.IEquals( ['ul', 'ol'] ) )
				eNextBlock.insertBefore( FCKTools.GetElementDocument( eNextBlock ).createTextNode( '\xa0' ), eNextBlock.firstChild ) ;

			if ( eNextBlock )
				oRange.MoveToElementEditStart( eNextBlock ) ;
		}
		else
		{
			if ( bIsStartOfBlock && bIsEndOfBlock && ePreviousBlock.tagName.toUpperCase() == 'LI' )
			{
				oRange.MoveToElementStart( ePreviousBlock ) ;
				this._OutdentWithSelection( ePreviousBlock, oRange ) ;
				oRange.Release() ;
				return true ;
			}

			var eNewBlock ;

			if ( ePreviousBlock )
			{
				var sPreviousBlockTag = ePreviousBlock.tagName.toUpperCase() ;

				if ( !this._HasShift && !(/^H[1-6]$/).test( sPreviousBlockTag ) )
				{
					eNewBlock = FCKDomTools.CloneElement( ePreviousBlock ) ;
				}
			}
			else if ( eNextBlock )
				eNewBlock = FCKDomTools.CloneElement( eNextBlock ) ;

			if ( !eNewBlock )
				eNewBlock = this.Window.document.createElement( blockTag ) ;

			var elementPath = oSplitInfo.ElementPath ;
			if ( elementPath )
			{
				for ( var i = 0, len = elementPath.Elements.length ; i < len ; i++ )
				{
					var element = elementPath.Elements[i] ;

					if ( element == elementPath.Block || element == elementPath.BlockLimit )
						break ;

					if ( FCKListsLib.InlineChildReqElements[ element.nodeName.toLowerCase() ] )
					{
						element = FCKDomTools.CloneElement( element ) ;
						FCKDomTools.MoveChildren( eNewBlock, element ) ;
						eNewBlock.appendChild( element ) ;
					}
				}
			}

			if ( FCKBrowserInfo.IsGeckoLike )
				FCKTools.AppendBogusBr( eNewBlock ) ;

			oRange.InsertNode( eNewBlock ) ;

			if ( FCKBrowserInfo.IsIE )
			{
				oRange.MoveToElementEditStart( eNewBlock ) ;
				oRange.Select() ;
			}

			oRange.MoveToElementEditStart( bIsStartOfBlock && !bIsEndOfBlock ? eNextBlock : eNewBlock ) ;
		}

		if ( FCKBrowserInfo.IsGeckoLike )
		{
			if ( eNextBlock )
			{
				var tmpNode = this.Window.document.createElement( 'span' ) ;

				tmpNode.innerHTML = '&nbsp;';

				oRange.InsertNode( tmpNode ) ;
				FCKDomTools.ScrollIntoView( tmpNode, false ) ;
				oRange.DeleteContents() ;
			}
			else
			{
				FCKDomTools.ScrollIntoView( eNextBlock || eNewBlock, false ) ;
			}
		}

		oRange.Select() ;
	}

	oRange.Release() ;

	return true ;
}

FCKEnterKey.prototype._ExecuteEnterBr = function( blockTag )
{
	var oRange = new FCKDomRange( this.Window ) ;
	oRange.MoveToSelection() ;

	if ( oRange.StartBlockLimit == oRange.EndBlockLimit )
	{
		oRange.DeleteContents() ;

		oRange.MoveToSelection() ;

		var bIsStartOfBlock	= oRange.CheckStartOfBlock() ;
		var bIsEndOfBlock	= oRange.CheckEndOfBlock() ;

		var sStartBlockTag = oRange.StartBlock ? oRange.StartBlock.tagName.toUpperCase() : '' ;

		var bHasShift = this._HasShift ;
		var bIsPre = false ;

		if ( !bHasShift && sStartBlockTag == 'LI' )
			return this._ExecuteEnterBlock( null, oRange ) ;

		if ( !bHasShift && bIsEndOfBlock && (/^H[1-6]$/).test( sStartBlockTag ) )
		{
			FCKDomTools.InsertAfterNode( oRange.StartBlock, this.Window.document.createElement( 'br' ) ) ;

			if ( FCKBrowserInfo.IsGecko )
				FCKDomTools.InsertAfterNode( oRange.StartBlock, this.Window.document.createTextNode( '' ) ) ;

			oRange.SetStart( oRange.StartBlock.nextSibling, FCKBrowserInfo.IsIE ? 3 : 1 ) ;
		}
		else
		{
			var eLineBreak ;
			bIsPre = sStartBlockTag.IEquals( 'pre' ) ;
			if ( bIsPre )
				eLineBreak = this.Window.document.createTextNode( FCKBrowserInfo.IsIE ? '\r' : '\n' ) ;
			else
				eLineBreak = this.Window.document.createElement( 'br' ) ;

			oRange.InsertNode( eLineBreak ) ;

			if ( FCKBrowserInfo.IsGecko )
				FCKDomTools.InsertAfterNode( eLineBreak, this.Window.document.createTextNode( '' ) ) ;

			if ( bIsEndOfBlock && FCKBrowserInfo.IsGeckoLike )
				FCKTools.AppendBogusBr( eLineBreak.parentNode ) ;

			if ( FCKBrowserInfo.IsIE )
				oRange.SetStart( eLineBreak, 4 ) ;
			else
				oRange.SetStart( eLineBreak.nextSibling, 1 ) ;

			if ( ! FCKBrowserInfo.IsIE )
			{
				var dummy = null ;
				if ( FCKBrowserInfo.IsOpera )
					dummy = this.Window.document.createElement( 'span' ) ;
				else
					dummy = this.Window.document.createElement( 'br' ) ;

				eLineBreak.parentNode.insertBefore( dummy, eLineBreak.nextSibling ) ;

				FCKDomTools.ScrollIntoView( dummy, false ) ;

				dummy.parentNode.removeChild( dummy ) ;
			}
		}

		oRange.Collapse( true ) ;

		oRange.Select( bIsPre ) ;
	}

	oRange.Release() ;

	return true ;
}

FCKEnterKey.prototype._OutdentWithSelection = function( li, range )
{
	var oBookmark = range.CreateBookmark() ;

	FCKListHandler.OutdentListItem( li ) ;

	range.MoveToBookmark( oBookmark ) ;
	range.Select() ;
}

FCKEnterKey.prototype._CheckIsAllContentsIncluded = function( range, node )
{
	var startOk = false ;
	var endOk = false ;

	if ( range.StartContainer == node || range.StartContainer == node.firstChild )
		startOk = ( range._Range.startOffset == 0 ) ;

	if ( range.EndContainer == node || range.EndContainer == node.lastChild )
	{
		var nodeLength = range.EndContainer.nodeType == 3 ? range.EndContainer.length : range.EndContainer.childNodes.length ;
		endOk = ( range._Range.endOffset == nodeLength ) ;
	}

	return startOk && endOk ;
}

FCKEnterKey.prototype._FixIESelectAllBug = function( range )
{
	var doc = this.Window.document ;
	doc.body.innerHTML = '' ;
	var editBlock ;
	if ( FCKConfig.EnterMode.IEquals( ['div', 'p'] ) )
	{
		editBlock = doc.createElement( FCKConfig.EnterMode ) ;
		doc.body.appendChild( editBlock ) ;
	}
	else
		editBlock = doc.body ;

	range.MoveToNodeContents( editBlock ) ;
	range.Collapse( true ) ;
	range.Select() ;
	range.Release() ;
}