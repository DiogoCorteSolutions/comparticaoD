var FCKDomRange = function( sourceWindow )
{
	this.Window = sourceWindow ;
	this._Cache = {} ;
}

FCKDomRange.prototype =
{

	_UpdateElementInfo : function()
	{
		var innerRange = this._Range ;

		if ( !innerRange )
			this.Release( true ) ;
		else
		{
			var eStart	= innerRange.startContainer ;

			var oElementPath = new FCKElementPath( eStart ) ;
			this.StartNode			= eStart.nodeType == 3 ? eStart : eStart.childNodes[ innerRange.startOffset ] ;
			this.StartContainer		= eStart ;
			this.StartBlock			= oElementPath.Block ;
			this.StartBlockLimit	= oElementPath.BlockLimit ;

			if ( innerRange.collapsed )
			{
				this.EndNode		= this.StartNode ;
				this.EndContainer	= this.StartContainer ;
				this.EndBlock		= this.StartBlock ;
				this.EndBlockLimit	= this.StartBlockLimit ;
			}
			else
			{
				var eEnd	= innerRange.endContainer ;

				if ( eStart != eEnd )
					oElementPath = new FCKElementPath( eEnd ) ;

				var eEndNode = eEnd ;
				if ( innerRange.endOffset == 0 )
				{
					while ( eEndNode && !eEndNode.previousSibling )
						eEndNode = eEndNode.parentNode ;

					if ( eEndNode )
						eEndNode = eEndNode.previousSibling ;
				}
				else if ( eEndNode.nodeType == 1 )
					eEndNode = eEndNode.childNodes[ innerRange.endOffset - 1 ] ;

				this.EndNode			= eEndNode ;
				this.EndContainer		= eEnd ;
				this.EndBlock			= oElementPath.Block ;
				this.EndBlockLimit		= oElementPath.BlockLimit ;
			}
		}

		this._Cache = {} ;
	},

	CreateRange : function()
	{
		return new FCKW3CRange( this.Window.document ) ;
	},

	DeleteContents : function()
	{
		if ( this._Range )
		{
			this._Range.deleteContents() ;
			this._UpdateElementInfo() ;
		}
	},

	ExtractContents : function()
	{
		if ( this._Range )
		{
			var docFrag = this._Range.extractContents() ;
			this._UpdateElementInfo() ;
			return docFrag ;
		}
		return null ;
	},

	CheckIsCollapsed : function()
	{
		if ( this._Range )
			return this._Range.collapsed ;

		return false ;
	},

	Collapse : function( toStart )
	{
		if ( this._Range )
			this._Range.collapse( toStart ) ;

		this._UpdateElementInfo() ;
	},

	Clone : function()
	{
		var oClone = FCKTools.CloneObject( this ) ;

		if ( this._Range )
			oClone._Range = this._Range.cloneRange() ;

		return oClone ;
	},

	MoveToNodeContents : function( targetNode )
	{
		if ( !this._Range )
			this._Range = this.CreateRange() ;

		this._Range.selectNodeContents( targetNode ) ;

		this._UpdateElementInfo() ;
	},

	MoveToElementStart : function( targetElement )
	{
		this.SetStart(targetElement,1) ;
		this.SetEnd(targetElement,1) ;
	},

	MoveToElementEditStart : function( targetElement )
	{
		var editableElement ;

		while ( targetElement && targetElement.nodeType == 1 )
		{
			if ( FCKDomTools.CheckIsEditable( targetElement ) )
				editableElement = targetElement ;
			else if ( editableElement )
				break ;

			targetElement = targetElement.firstChild ;
		}

		if ( editableElement )
			this.MoveToElementStart( editableElement ) ;
	},

	InsertNode : function( node )
	{
		if ( this._Range )
			this._Range.insertNode( node ) ;
	},

	CheckIsEmpty : function()
	{
		if ( this.CheckIsCollapsed() )
			return true ;

		var eToolDiv = this.Window.document.createElement( 'div' ) ;
		this._Range.cloneContents().AppendTo( eToolDiv ) ;

		FCKDomTools.TrimNode( eToolDiv ) ;

		return ( eToolDiv.innerHTML.length == 0 ) ;
	},

	CheckStartOfBlock : function()
	{
		var cache = this._Cache ;
		var bIsStartOfBlock = cache.IsStartOfBlock ;

		if ( bIsStartOfBlock != undefined )
			return bIsStartOfBlock ;

		var block = this.StartBlock || this.StartBlockLimit ;

		var container	= this._Range.startContainer ;
		var offset		= this._Range.startOffset ;
		var currentNode ;

		if ( offset > 0 )
		{
			if ( container.nodeType == 3 )
			{
				var textValue = container.nodeValue.substr( 0, offset ).Trim() ;

				if ( textValue.length != 0 )
					return cache.IsStartOfBlock = false ;
			}
			else
				currentNode = container.childNodes[ offset - 1 ] ;
		}

		if ( !currentNode )
			currentNode = FCKDomTools.GetPreviousSourceNode( container, true, null, block ) ;

		while ( currentNode )
		{
			switch ( currentNode.nodeType )
			{
				case 1 :
					if ( !FCKListsLib.InlineChildReqElements[ currentNode.nodeName.toLowerCase() ] )
						return cache.IsStartOfBlock = false ;

					break ;

				case 3 :
					if ( currentNode.nodeValue.Trim().length > 0 )
						return cache.IsStartOfBlock = false ;
			}

			currentNode = FCKDomTools.GetPreviousSourceNode( currentNode, false, null, block ) ;
		}

		return cache.IsStartOfBlock = true ;
	},

	CheckEndOfBlock : function( refreshSelection )
	{
		var isEndOfBlock = this._Cache.IsEndOfBlock ;

		if ( isEndOfBlock != undefined )
			return isEndOfBlock ;

		var block = this.EndBlock || this.EndBlockLimit ;

		var container	= this._Range.endContainer ;
		var offset			= this._Range.endOffset ;
		var currentNode ;

		if ( container.nodeType == 3 )
		{
			var textValue = container.nodeValue ;
			if ( offset < textValue.length )
			{
				textValue = textValue.substr( offset ) ;

				if ( textValue.Trim().length != 0 )
					return this._Cache.IsEndOfBlock = false ;
			}
		}
		else
			currentNode = container.childNodes[ offset ] ;

		if ( !currentNode )
			currentNode = FCKDomTools.GetNextSourceNode( container, true, null, block ) ;

		var hadBr = false ;

		while ( currentNode )
		{
			switch ( currentNode.nodeType )
			{
				case 1 :
					var nodeName = currentNode.nodeName.toLowerCase() ;

					if ( FCKListsLib.InlineChildReqElements[ nodeName ] )
						break ;

					if ( nodeName == 'br' && !hadBr )
					{
						hadBr = true ;
						break ;
					}

					return this._Cache.IsEndOfBlock = false ;

				case 3 :
					if ( currentNode.nodeValue.Trim().length > 0 )
						return this._Cache.IsEndOfBlock = false ;
			}

			currentNode = FCKDomTools.GetNextSourceNode( currentNode, false, null, block ) ;
		}

		if ( refreshSelection )
			this.Select() ;

		return this._Cache.IsEndOfBlock = true ;
	},

	CreateBookmark : function( includeNodes )
	{
		var oBookmark =
		{
			StartId	: (new Date()).valueOf() + Math.floor(Math.random()*1000) + 'S',
			EndId	: (new Date()).valueOf() + Math.floor(Math.random()*1000) + 'E'
		} ;

		var oDoc = this.Window.document ;
		var eStartSpan ;
		var eEndSpan ;
		var oClone ;

		if ( !this.CheckIsCollapsed() )
		{
			eEndSpan = oDoc.createElement( 'span' ) ;
			eEndSpan.style.display = 'none' ;
			eEndSpan.id = oBookmark.EndId ;
			eEndSpan.setAttribute( '_fck_bookmark', true ) ;

				eEndSpan.innerHTML = '&nbsp;' ;

			oClone = this.Clone() ;
			oClone.Collapse( false ) ;
			oClone.InsertNode( eEndSpan ) ;
		}

		eStartSpan = oDoc.createElement( 'span' ) ;
		eStartSpan.style.display = 'none' ;
		eStartSpan.id = oBookmark.StartId ;
		eStartSpan.setAttribute( '_fck_bookmark', true ) ;

			eStartSpan.innerHTML = '&nbsp;' ;

		oClone = this.Clone() ;
		oClone.Collapse( true ) ;
		oClone.InsertNode( eStartSpan ) ;

		if ( includeNodes )
		{
			oBookmark.StartNode = eStartSpan ;
			oBookmark.EndNode = eEndSpan ;
		}

		if ( eEndSpan )
		{
			this.SetStart( eStartSpan, 4 ) ;
			this.SetEnd( eEndSpan, 3 ) ;
		}
		else
			this.MoveToPosition( eStartSpan, 4 ) ;

		return oBookmark ;
	},

	GetBookmarkNode : function( bookmark, start )
	{
		var doc = this.Window.document ;

		if ( start )
			return bookmark.StartNode || doc.getElementById( bookmark.StartId ) ;
		else
			return bookmark.EndNode || doc.getElementById( bookmark.EndId ) ;
	},

	MoveToBookmark : function( bookmark, preserveBookmark )
	{
		var eStartSpan	= this.GetBookmarkNode( bookmark, true ) ;
		var eEndSpan	= this.GetBookmarkNode( bookmark, false ) ;

		this.SetStart( eStartSpan, 3 ) ;

		if ( !preserveBookmark )
			FCKDomTools.RemoveNode( eStartSpan ) ;

		if ( eEndSpan )
		{
			this.SetEnd( eEndSpan, 3 ) ;

			if ( !preserveBookmark )
				FCKDomTools.RemoveNode( eEndSpan ) ;
		}
		else
			this.Collapse( true ) ;

		this._UpdateElementInfo() ;
	},

	CreateBookmark2 : function()
	{
		if ( ! this._Range )
			return { "Start" : 0, "End" : 0 } ;

		var bookmark =
		{
			"Start" : [ this._Range.startOffset ],
			"End" : [ this._Range.endOffset ]
		} ;

		var curStart = this._Range.startContainer.previousSibling ;
		var curEnd = this._Range.endContainer.previousSibling ;

		var addrStart = this._Range.startContainer ;
		var addrEnd = this._Range.endContainer ;
		while ( curStart && curStart.nodeType == 3 && addrStart.nodeType == 3 )
		{
			bookmark.Start[0] += curStart.length ;
			addrStart = curStart ;
			curStart = curStart.previousSibling ;
		}
		while ( curEnd && curEnd.nodeType == 3 && addrEnd.nodeType == 3 )
		{
			bookmark.End[0] += curEnd.length ;
			addrEnd = curEnd ;
			curEnd = curEnd.previousSibling ;
		}

		if ( addrStart.nodeType == 1 && addrStart.childNodes[bookmark.Start[0]] && addrStart.childNodes[bookmark.Start[0]].nodeType == 3 )
		{
			var curNode = addrStart.childNodes[bookmark.Start[0]] ;
			var offset = 0 ;
			while ( curNode.previousSibling && curNode.previousSibling.nodeType == 3 )
			{
				curNode = curNode.previousSibling ;
				offset += curNode.length ;
			}
			addrStart = curNode ;
			bookmark.Start[0] = offset ;
		}
		if ( addrEnd.nodeType == 1 && addrEnd.childNodes[bookmark.End[0]] && addrEnd.childNodes[bookmark.End[0]].nodeType == 3 )
		{
			var curNode = addrEnd.childNodes[bookmark.End[0]] ;
			var offset = 0 ;
			while ( curNode.previousSibling && curNode.previousSibling.nodeType == 3 )
			{
				curNode = curNode.previousSibling ;
				offset += curNode.length ;
			}
			addrEnd = curNode ;
			bookmark.End[0] = offset ;
		}

		bookmark.Start = FCKDomTools.GetNodeAddress( addrStart, true ).concat( bookmark.Start ) ;
		bookmark.End = FCKDomTools.GetNodeAddress( addrEnd, true ).concat( bookmark.End ) ;
		return bookmark;
	},

	MoveToBookmark2 : function( bookmark )
	{
		var curStart = FCKDomTools.GetNodeFromAddress( this.Window.document, bookmark.Start.slice( 0, -1 ), true ) ;
		var curEnd = FCKDomTools.GetNodeFromAddress( this.Window.document, bookmark.End.slice( 0, -1 ), true ) ;

		this.Release( true ) ;
		this._Range = new FCKW3CRange( this.Window.document ) ;
		var startOffset = bookmark.Start[ bookmark.Start.length - 1 ] ;
		var endOffset = bookmark.End[ bookmark.End.length - 1 ] ;
		while ( curStart.nodeType == 3 && startOffset > curStart.length )
		{
			if ( ! curStart.nextSibling || curStart.nextSibling.nodeType != 3 )
				break ;
			startOffset -= curStart.length ;
			curStart = curStart.nextSibling ;
		}
		while ( curEnd.nodeType == 3 && endOffset > curEnd.length )
		{
			if ( ! curEnd.nextSibling || curEnd.nextSibling.nodeType != 3 )
				break ;
			endOffset -= curEnd.length ;
			curEnd = curEnd.nextSibling ;
		}
		this._Range.setStart( curStart, startOffset ) ;
		this._Range.setEnd( curEnd, endOffset ) ;
		this._UpdateElementInfo() ;
	},

	MoveToPosition : function( targetElement, position )
	{
		this.SetStart( targetElement, position ) ;
		this.Collapse( true ) ;
	},

	SetStart : function( targetElement, position, noInfoUpdate )
	{
		var oRange = this._Range ;
		if ( !oRange )
			oRange = this._Range = this.CreateRange() ;

		switch( position )
		{
			case 1 :
				oRange.setStart( targetElement, 0 ) ;
				break ;

			case 2 :
				oRange.setStart( targetElement, targetElement.childNodes.length ) ;
				break ;

			case 3 :
				oRange.setStartBefore( targetElement ) ;
				break ;

			case 4 :
				oRange.setStartAfter( targetElement ) ;
		}

		if ( !noInfoUpdate )
			this._UpdateElementInfo() ;
	},

	SetEnd : function( targetElement, position, noInfoUpdate )
	{
		var oRange = this._Range ;
		if ( !oRange )
			oRange = this._Range = this.CreateRange() ;

		switch( position )
		{
			case 1 :
				oRange.setEnd( targetElement, 0 ) ;
				break ;

			case 2 :
				oRange.setEnd( targetElement, targetElement.childNodes.length ) ;
				break ;

			case 3 :
				oRange.setEndBefore( targetElement ) ;
				break ;

			case 4 :
				oRange.setEndAfter( targetElement ) ;
		}

		if ( !noInfoUpdate )
			this._UpdateElementInfo() ;
	},

	Expand : function( unit )
	{
		var oNode, oSibling ;

		switch ( unit )
		{
			case 'inline_elements' :
				if ( this._Range.startOffset == 0 )
				{
					oNode = this._Range.startContainer ;

					if ( oNode.nodeType != 1 )
						oNode = oNode.previousSibling ? null : oNode.parentNode ;

					if ( oNode )
					{
						while ( FCKListsLib.InlineNonEmptyElements[ oNode.nodeName.toLowerCase() ] )
						{
							this._Range.setStartBefore( oNode ) ;

							if ( oNode != oNode.parentNode.firstChild )
								break ;

							oNode = oNode.parentNode ;
						}
					}
				}

				oNode = this._Range.endContainer ;
				var offset = this._Range.endOffset ;

				if ( ( oNode.nodeType == 3 && offset >= oNode.nodeValue.length ) || ( oNode.nodeType == 1 && offset >= oNode.childNodes.length ) || ( oNode.nodeType != 1 && oNode.nodeType != 3 ) )
				{
					if ( oNode.nodeType != 1 )
						oNode = oNode.nextSibling ? null : oNode.parentNode ;

					if ( oNode )
					{
						while ( FCKListsLib.InlineNonEmptyElements[ oNode.nodeName.toLowerCase() ] )
						{
							this._Range.setEndAfter( oNode ) ;

							if ( oNode != oNode.parentNode.lastChild )
								break ;

							oNode = oNode.parentNode ;
						}
					}
				}

				break ;

			case 'block_contents' :
			case 'list_contents' :
				var boundarySet = FCKListsLib.BlockBoundaries ;
				if ( unit == 'list_contents' || FCKConfig.EnterMode == 'br' )
					boundarySet = FCKListsLib.ListBoundaries ;

				if ( this.StartBlock && FCKConfig.EnterMode != 'br' && unit == 'block_contents' )
					this.SetStart( this.StartBlock, 1 ) ;
				else
				{
					oNode = this._Range.startContainer ;

					if ( oNode.nodeType == 1 )
					{
						var lastNode = oNode.childNodes[ this._Range.startOffset ] ;
						if ( lastNode )
							oNode = FCKDomTools.GetPreviousSourceNode( lastNode, true ) ;
						else
							oNode = oNode.lastChild || oNode ;
					}

					while ( oNode
							&& ( oNode.nodeType != 1
								|| ( oNode != this.StartBlockLimit
									&& !boundarySet[ oNode.nodeName.toLowerCase() ] ) ) )
					{
						this._Range.setStartBefore( oNode ) ;
						oNode = oNode.previousSibling || oNode.parentNode ;
					}
				}

				if ( this.EndBlock && FCKConfig.EnterMode != 'br' && unit == 'block_contents' && this.EndBlock.nodeName.toLowerCase() != 'li' )
					this.SetEnd( this.EndBlock, 2 ) ;
				else
				{
					oNode = this._Range.endContainer ;
					if ( oNode.nodeType == 1 )
						oNode = oNode.childNodes[ this._Range.endOffset ] || oNode.lastChild ;

					while ( oNode
							&& ( oNode.nodeType != 1
								|| ( oNode != this.StartBlockLimit
									&& !boundarySet[ oNode.nodeName.toLowerCase() ] ) ) )
					{
						this._Range.setEndAfter( oNode ) ;
						oNode = oNode.nextSibling || oNode.parentNode ;
					}

					if ( oNode && oNode.nodeName.toLowerCase() == 'br' )
						this._Range.setEndAfter( oNode ) ;
				}

				this._UpdateElementInfo() ;
		}
	},

	SplitBlock : function( forceBlockTag )
	{
		var blockTag = forceBlockTag || FCKConfig.EnterMode ;

		if ( !this._Range )
			this.MoveToSelection() ;

		if ( this.StartBlockLimit == this.EndBlockLimit )
		{
			var eStartBlock		= this.StartBlock ;
			var eEndBlock		= this.EndBlock ;
			var oElementPath	= null ;

			if ( blockTag != 'br' )
			{
				if ( !eStartBlock )
				{
					eStartBlock = this.FixBlock( true, blockTag ) ;
					eEndBlock	= this.EndBlock ;
				}

				if ( !eEndBlock )
					eEndBlock = this.FixBlock( false, blockTag ) ;
			}

			var bIsStartOfBlock	= ( eStartBlock != null && this.CheckStartOfBlock() ) ;
			var bIsEndOfBlock	= ( eEndBlock != null && this.CheckEndOfBlock() ) ;

			if ( !this.CheckIsEmpty() )
				this.DeleteContents() ;

			if ( eStartBlock && eEndBlock && eStartBlock == eEndBlock )
			{
				if ( bIsEndOfBlock )
				{
					oElementPath = new FCKElementPath( this.StartContainer ) ;
					this.MoveToPosition( eEndBlock, 4 ) ;
					eEndBlock = null ;
				}
				else if ( bIsStartOfBlock )
				{
					oElementPath = new FCKElementPath( this.StartContainer ) ;
					this.MoveToPosition( eStartBlock, 3 ) ;
					eStartBlock = null ;
				}
				else
				{
					this.SetEnd( eStartBlock, 2 ) ;
					var eDocFrag = this.ExtractContents() ;

					eEndBlock = eStartBlock.cloneNode( false ) ;
					eEndBlock.removeAttribute( 'id', false ) ;

					eDocFrag.AppendTo( eEndBlock ) ;

					FCKDomTools.InsertAfterNode( eStartBlock, eEndBlock ) ;

					this.MoveToPosition( eStartBlock, 4 ) ;

					if ( FCKBrowserInfo.IsGecko &&
							! eStartBlock.nodeName.IEquals( ['ul', 'ol'] ) )
						FCKTools.AppendBogusBr( eStartBlock ) ;
				}
			}

			return {
				PreviousBlock	: eStartBlock,
				NextBlock		: eEndBlock,
				WasStartOfBlock : bIsStartOfBlock,
				WasEndOfBlock	: bIsEndOfBlock,
				ElementPath		: oElementPath
			} ;
		}

		return null ;
	},

	FixBlock : function( isStart, blockTag )
	{
		var oBookmark = this.CreateBookmark() ;

		this.Collapse( isStart ) ;

		this.Expand( 'block_contents' ) ;

		var oFixedBlock = this.Window.document.createElement( blockTag ) ;

		this.ExtractContents().AppendTo( oFixedBlock ) ;
		FCKDomTools.TrimNode( oFixedBlock ) ;

		if ( FCKDomTools.CheckIsEmptyElement(oFixedBlock, function( element ) { return element.getAttribute('_fck_bookmark') != 'true' ; } )
				&& FCKBrowserInfo.IsGeckoLike )
				FCKTools.AppendBogusBr( oFixedBlock ) ;

		this.InsertNode( oFixedBlock ) ;

		this.MoveToBookmark( oBookmark ) ;

		return oFixedBlock ;
	},

	Release : function( preserveWindow )
	{
		if ( !preserveWindow )
			this.Window = null ;

		this.StartNode = null ;
		this.StartContainer = null ;
		this.StartBlock = null ;
		this.StartBlockLimit = null ;
		this.EndNode = null ;
		this.EndContainer = null ;
		this.EndBlock = null ;
		this.EndBlockLimit = null ;
		this._Range = null ;
		this._Cache = null ;
	},

	CheckHasRange : function()
	{
		return !!this._Range ;
	},

	GetTouchedStartNode : function()
	{
		var range = this._Range ;
		var container = range.startContainer ;

		if ( range.collapsed || container.nodeType != 1 )
			return container ;

		return container.childNodes[ range.startOffset ] || container ;
	},

	GetTouchedEndNode : function()
	{
		var range = this._Range ;
		var container = range.endContainer ;

		if ( range.collapsed || container.nodeType != 1 )
			return container ;

		return container.childNodes[ range.endOffset - 1 ] || container ;
	}
} ;