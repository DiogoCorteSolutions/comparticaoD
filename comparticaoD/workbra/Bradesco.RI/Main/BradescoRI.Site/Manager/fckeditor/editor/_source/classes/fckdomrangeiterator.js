var FCKDomRangeIterator = function( range )
{
	this.Range = range ;
	this.ForceBrBreak = false ;
	this.EnforceRealBlocks = false ;
}

FCKDomRangeIterator.CreateFromSelection = function( targetWindow )
{
	var range = new FCKDomRange( targetWindow ) ;
	range.MoveToSelection() ;
	return new FCKDomRangeIterator( range ) ;
}

FCKDomRangeIterator.prototype =
{
	GetNextParagraph : function()
	{
		var block ;

		var range ;

		var isLast ;

		var removePreviousBr ;
		var removeLastBr ;

		var boundarySet = this.ForceBrBreak ? FCKListsLib.ListBoundaries : FCKListsLib.BlockBoundaries ;

		if ( !this._LastNode )
		{
			var range = this.Range.Clone() ;
			range.Expand( this.ForceBrBreak ? 'list_contents' : 'block_contents' ) ;

			this._NextNode = range.GetTouchedStartNode() ;
			this._LastNode = range.GetTouchedEndNode() ;

			range = null ;
		}

		var currentNode = this._NextNode ;
		var lastNode = this._LastNode ;

		this._NextNode = null ;

		while ( currentNode )
		{
			var closeRange = false ;

			var includeNode = ( currentNode.nodeType != 1 ) ;

			var continueFromSibling = false ;

			if ( !includeNode )
			{
				var nodeName = currentNode.nodeName.toLowerCase() ;

				if ( boundarySet[ nodeName ] && ( !FCKBrowserInfo.IsIE || currentNode.scopeName == 'HTML' ) )
				{
					if ( nodeName == 'br' )
						includeNode = true ;
					else if ( !range && currentNode.childNodes.length == 0 && nodeName != 'hr' )
					{
						block = currentNode ;
						isLast = currentNode == lastNode ;
						break ;
					}

					if ( range )
					{
						range.SetEnd( currentNode, 3, true ) ;

						if ( nodeName != 'br' )
							this._NextNode = FCKDomTools.GetNextSourceNode( currentNode, true, null, lastNode ) || currentNode ;
					}

					closeRange = true ;
				}
				else
				{
					if ( currentNode.firstChild )
					{
						if ( !range )
						{
							range = new FCKDomRange( this.Range.Window ) ;
							range.SetStart( currentNode, 3, true ) ;
						}

						currentNode = currentNode.firstChild ;
						continue ;
					}
					includeNode = true ;
				}
			}
			else if ( currentNode.nodeType == 3 )
			{
				if ( /^[\r\n\t ]+$/.test( currentNode.nodeValue ) )
					includeNode = false ;
			}

			if ( includeNode && !range )
			{
				range = new FCKDomRange( this.Range.Window ) ;
				range.SetStart( currentNode, 3, true ) ;
			}

			isLast = ( ( !closeRange || includeNode ) && currentNode == lastNode ) ;

			if ( range && !closeRange )
			{
				while ( !currentNode.nextSibling && !isLast )
				{
					var parentNode = currentNode.parentNode ;

					if ( boundarySet[ parentNode.nodeName.toLowerCase() ] )
					{
						closeRange = true ;
						isLast = isLast || ( parentNode == lastNode ) ;
						break ;
					}

					currentNode = parentNode ;
					includeNode = true ;
					isLast = ( currentNode == lastNode ) ;
					continueFromSibling = true ;
				}
			}

			if ( includeNode )
				range.SetEnd( currentNode, 4, true ) ;

			if ( ( closeRange || isLast ) && range )
			{
				range._UpdateElementInfo() ;

				if ( range.StartNode == range.EndNode
						&& range.StartNode.parentNode == range.StartBlockLimit
						&& range.StartNode.getAttribute && range.StartNode.getAttribute( '_fck_bookmark' ) )
					range = null ;
				else
					break ;
			}

			if ( isLast )
				break ;

			currentNode = FCKDomTools.GetNextSourceNode( currentNode, continueFromSibling, null, lastNode ) ;
		}

		if ( !block )
		{
			if ( !range )
			{
				this._NextNode = null ;
				return null ;
			}

			block = range.StartBlock ;

			if ( !block
				&& !this.EnforceRealBlocks
				&& range.StartBlockLimit.nodeName.IEquals( 'DIV', 'TH', 'TD' )
				&& range.CheckStartOfBlock()
				&& range.CheckEndOfBlock() )
			{
				block = range.StartBlockLimit ;
			}
			else if ( !block || ( this.EnforceRealBlocks && block.nodeName.toLowerCase() == 'li' ) )
			{
				block = this.Range.Window.document.createElement( FCKConfig.EnterMode == 'p' ? 'p' : 'div' ) ;

				range.ExtractContents().AppendTo( block ) ;
				FCKDomTools.TrimNode( block ) ;

				range.InsertNode( block ) ;

				removePreviousBr = true ;
				removeLastBr = true ;
			}
			else if ( block.nodeName.toLowerCase() != 'li' )
			{
				if ( !range.CheckStartOfBlock() || !range.CheckEndOfBlock() )
				{
					block = block.cloneNode( false ) ;

					range.ExtractContents().AppendTo( block ) ;
					FCKDomTools.TrimNode( block ) ;

					var splitInfo = range.SplitBlock() ;

					removePreviousBr = !splitInfo.WasStartOfBlock ;
					removeLastBr = !splitInfo.WasEndOfBlock ;

					range.InsertNode( block ) ;
				}
			}
			else if ( !isLast )
			{
				this._NextNode = block == lastNode ? null : FCKDomTools.GetNextSourceNode( range.EndNode, true, null, lastNode ) ;
				return block ;
			}
		}

		if ( removePreviousBr )
		{
			var previousSibling = block.previousSibling ;
			if ( previousSibling && previousSibling.nodeType == 1 )
			{
				if ( previousSibling.nodeName.toLowerCase() == 'br' )
					previousSibling.parentNode.removeChild( previousSibling ) ;
				else if ( previousSibling.lastChild && previousSibling.lastChild.nodeName.IEquals( 'br' ) )
					previousSibling.removeChild( previousSibling.lastChild ) ;
			}
		}

		if ( removeLastBr )
		{
			var lastChild = block.lastChild ;
			if ( lastChild && lastChild.nodeType == 1 && lastChild.nodeName.toLowerCase() == 'br' )
				block.removeChild( lastChild ) ;
		}

		if ( !this._NextNode )
			this._NextNode = ( isLast || block == lastNode ) ? null : FCKDomTools.GetNextSourceNode( block, true, null, lastNode ) ;

		return block ;
	}
} ;