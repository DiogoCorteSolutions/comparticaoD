var FCKBlockQuoteCommand = function()
{
}

FCKBlockQuoteCommand.prototype =
{
	Execute : function()
	{
		FCKUndo.SaveUndoStep() ;

		var state = this.GetState() ;

		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;

		var bookmark = range.CreateBookmark() ;




		if ( FCKBrowserInfo.IsIE )
		{
			var bStart	= range.GetBookmarkNode( bookmark, true ) ;
			var bEnd	= range.GetBookmarkNode( bookmark, false ) ;

			var cursor ;

			if ( bStart
					&& bStart.parentNode.nodeName.IEquals( 'blockquote' )
					&& !bStart.previousSibling )
			{
				cursor = bStart ;
				while ( ( cursor = cursor.nextSibling ) )
				{
					if ( FCKListsLib.BlockElements[ cursor.nodeName.toLowerCase() ] )
						FCKDomTools.MoveNode( bStart, cursor, true ) ;
				}
			}

			if ( bEnd
					&& bEnd.parentNode.nodeName.IEquals( 'blockquote' )
					&& !bEnd.previousSibling )
			{
				cursor = bEnd ;
				while ( ( cursor = cursor.nextSibling ) )
				{
					if ( FCKListsLib.BlockElements[ cursor.nodeName.toLowerCase() ] )
					{
						if ( cursor.firstChild == bStart )
							FCKDomTools.InsertAfterNode( bStart, bEnd ) ;
						else
							FCKDomTools.MoveNode( bEnd, cursor, true ) ;
					}
				}
			}
		}

		var iterator = new FCKDomRangeIterator( range ) ;
		var block ;

		if ( state == FCK_TRISTATE_OFF )
		{
			var paragraphs = [] ;
			while ( ( block = iterator.GetNextParagraph() ) )
				paragraphs.push( block ) ;


			if ( paragraphs.length < 1 )
			{
				para = range.Window.document.createElement( FCKConfig.EnterMode.IEquals( 'p' ) ? 'p' : 'div' ) ;
				range.InsertNode( para ) ;
				para.appendChild( range.Window.document.createTextNode( '\ufeff' ) ) ;
				range.MoveToBookmark( bookmark ) ;
				range.MoveToNodeContents( para ) ;
				range.Collapse( true ) ;
				bookmark = range.CreateBookmark() ;
				paragraphs.push( para ) ;
			}


			var commonParent = paragraphs[0].parentNode ;
			var tmp = [] ;
			for ( var i = 0 ; i < paragraphs.length ; i++ )
			{
				block = paragraphs[i] ;
				commonParent = FCKDomTools.GetCommonParents( block.parentNode, commonParent ).pop() ;
			}


			while ( commonParent.nodeName.IEquals( 'table', 'tbody', 'tr', 'ol', 'ul' ) )
				commonParent = commonParent.parentNode ;



			var lastBlock = null ;
			while ( paragraphs.length > 0 )
			{
				block = paragraphs.shift() ;
				while ( block.parentNode != commonParent )
					block = block.parentNode ;
				if ( block != lastBlock )
					tmp.push( block ) ;
				lastBlock = block ;
			}


			while ( tmp.length > 0 )
			{
				block = tmp.shift() ;
				if ( block.nodeName.IEquals( 'blockquote' ) )
				{
					var docFrag = FCKTools.GetElementDocument( block ).createDocumentFragment() ;
					while ( block.firstChild )
					{
						docFrag.appendChild( block.removeChild( block.firstChild ) ) ;
						paragraphs.push( docFrag.lastChild ) ;
					}
					block.parentNode.replaceChild( docFrag, block ) ;
				}
				else
					paragraphs.push( block ) ;
			}


			var bqBlock = range.Window.document.createElement( 'blockquote' ) ;
			commonParent.insertBefore( bqBlock, paragraphs[0] ) ;
			while ( paragraphs.length > 0 )
			{
				block = paragraphs.shift() ;
				bqBlock.appendChild( block ) ;
			}
		}
		else if ( state == FCK_TRISTATE_ON )
		{
			var moveOutNodes = [] ;
			var elementMarkers = {} ;
			while ( ( block = iterator.GetNextParagraph() ) )
			{
				var bqParent = null ;
				var bqChild = null ;
				while ( block.parentNode )
				{
					if ( block.parentNode.nodeName.IEquals( 'blockquote' ) )
					{
						bqParent = block.parentNode ;
						bqChild = block ;
						break ;
					}
					block = block.parentNode ;
				}



				if ( bqParent && bqChild && !bqChild._fckblockquotemoveout )
				{
					moveOutNodes.push( bqChild ) ;
					FCKDomTools.SetElementMarker( elementMarkers, bqChild, '_fckblockquotemoveout', true ) ;
				}
			}
			FCKDomTools.ClearAllMarkers( elementMarkers ) ;

			var movedNodes = [] ;
			var processedBlockquoteBlocks = [], elementMarkers = {} ;
			var noBlockLeft = function( bqBlock )
			{
				for ( var i = 0 ; i < bqBlock.childNodes.length ; i++ )
				{
					if ( FCKListsLib.BlockElements[ bqBlock.childNodes[i].nodeName.toLowerCase() ] )
						return false ;
				}
				return true ;
			} ;
			while ( moveOutNodes.length > 0 )
			{
				var node = moveOutNodes.shift() ;
				var bqBlock = node.parentNode ;



				if ( node == node.parentNode.firstChild )
					bqBlock.parentNode.insertBefore( bqBlock.removeChild( node ), bqBlock ) ;
				else if ( node == node.parentNode.lastChild )
					bqBlock.parentNode.insertBefore( bqBlock.removeChild( node ), bqBlock.nextSibling ) ;
				else
					FCKDomTools.BreakParent( node, node.parentNode, range ) ;


				if ( !bqBlock._fckbqprocessed )
				{
					processedBlockquoteBlocks.push( bqBlock ) ;
					FCKDomTools.SetElementMarker( elementMarkers, bqBlock, '_fckbqprocessed', true );
				}

				movedNodes.push( node ) ;
			}


			for ( var i = processedBlockquoteBlocks.length - 1 ; i >= 0 ; i-- )
			{
				var bqBlock = processedBlockquoteBlocks[i] ;
				if ( noBlockLeft( bqBlock ) )
					FCKDomTools.RemoveNode( bqBlock ) ;
			}
			FCKDomTools.ClearAllMarkers( elementMarkers ) ;

			if ( FCKConfig.EnterMode.IEquals( 'br' ) )
			{
				while ( movedNodes.length )
				{
					var node = movedNodes.shift() ;
					var firstTime = true ;
					if ( node.nodeName.IEquals( 'div' ) )
					{
						var docFrag = FCKTools.GetElementDocument( node ).createDocumentFragment() ;
						var needBeginBr = firstTime && node.previousSibling &&
							!FCKListsLib.BlockBoundaries[node.previousSibling.nodeName.toLowerCase()] ;
						if ( firstTime && needBeginBr )
							docFrag.appendChild( FCKTools.GetElementDocument( node ).createElement( 'br' ) ) ;
						var needEndBr = node.nextSibling &&
							!FCKListsLib.BlockBoundaries[node.nextSibling.nodeName.toLowerCase()] ;
						while ( node.firstChild )
							docFrag.appendChild( node.removeChild( node.firstChild ) ) ;
						if ( needEndBr )
							docFrag.appendChild( FCKTools.GetElementDocument( node ).createElement( 'br' ) ) ;
						node.parentNode.replaceChild( docFrag, node ) ;
						firstTime = false ;
					}
				}
			}
		}
		range.MoveToBookmark( bookmark ) ;
		range.Select() ;

		FCK.Focus() ;
		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
	},

	GetState : function()
	{

		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG || ! FCK.EditorWindow )
			return FCK_TRISTATE_DISABLED ;

		var path = new FCKElementPath( FCKSelection.GetBoundaryParentElement( true ) ) ;
		var firstBlock = path.Block || path.BlockLimit ;

		if ( !firstBlock || firstBlock.nodeName.toLowerCase() == 'body' )
			return FCK_TRISTATE_OFF ;


		for ( var i = 0 ; i < path.Elements.length ; i++ )
		{
			if ( path.Elements[i].nodeName.IEquals( 'blockquote' ) )
				return FCK_TRISTATE_ON ;
		}
		return FCK_TRISTATE_OFF ;
	}
} ;