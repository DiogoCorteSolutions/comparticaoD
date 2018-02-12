var FCKW3CRange = function( parentDocument )
{
	this._Document = parentDocument ;

	this.startContainer	= null ;
	this.startOffset	= null ;
	this.endContainer	= null ;
	this.endOffset		= null ;
	this.collapsed		= true ;
}

FCKW3CRange.CreateRange = function( parentDocument )
{
	return new FCKW3CRange( parentDocument ) ;
}

FCKW3CRange.CreateFromRange = function( parentDocument, sourceRange )
{
	var range = FCKW3CRange.CreateRange( parentDocument ) ;
	range.setStart( sourceRange.startContainer, sourceRange.startOffset ) ;
	range.setEnd( sourceRange.endContainer, sourceRange.endOffset ) ;
	return range ;
}

FCKW3CRange.prototype =
{

	_UpdateCollapsed : function()
	{
      this.collapsed = ( this.startContainer == this.endContainer && this.startOffset == this.endOffset ) ;
	},




	setStart : function( refNode, offset )
	{
		this.startContainer	= refNode ;
		this.startOffset	= offset ;

		if ( !this.endContainer )
		{
			this.endContainer	= refNode ;
			this.endOffset		= offset ;
		}

		this._UpdateCollapsed() ;
	},




	setEnd : function( refNode, offset )
	{
		this.endContainer	= refNode ;
		this.endOffset		= offset ;

		if ( !this.startContainer )
		{
			this.startContainer	= refNode ;
			this.startOffset	= offset ;
		}

		this._UpdateCollapsed() ;
	},

	setStartAfter : function( refNode )
	{
		this.setStart( refNode.parentNode, FCKDomTools.GetIndexOf( refNode ) + 1 ) ;
	},

	setStartBefore : function( refNode )
	{
		this.setStart( refNode.parentNode, FCKDomTools.GetIndexOf( refNode ) ) ;
	},

	setEndAfter : function( refNode )
	{
		this.setEnd( refNode.parentNode, FCKDomTools.GetIndexOf( refNode ) + 1 ) ;
	},

	setEndBefore : function( refNode )
	{
		this.setEnd( refNode.parentNode, FCKDomTools.GetIndexOf( refNode ) ) ;
	},

	collapse : function( toStart )
	{
		if ( toStart )
		{
			this.endContainer	= this.startContainer ;
			this.endOffset		= this.startOffset ;
		}
		else
		{
			this.startContainer	= this.endContainer ;
			this.startOffset	= this.endOffset ;
		}

		this.collapsed = true ;
	},

	selectNodeContents : function( refNode )
	{
		this.setStart( refNode, 0 ) ;
		this.setEnd( refNode, refNode.nodeType == 3 ? refNode.data.length : refNode.childNodes.length ) ;
	},

	insertNode : function( newNode )
	{
		var startContainer = this.startContainer ;
		var startOffset = this.startOffset ;


		if ( startContainer.nodeType == 3 )
		{
			startContainer.splitText( startOffset ) ;


			if ( startContainer == this.endContainer )
				this.setEnd( startContainer.nextSibling, this.endOffset - this.startOffset ) ;


			FCKDomTools.InsertAfterNode( startContainer, newNode ) ;

			return ;
		}
		else
		{

			startContainer.insertBefore( newNode, startContainer.childNodes[ startOffset ] || null ) ;


			if ( startContainer == this.endContainer )
			{
				this.endOffset++ ;
				this.collapsed = false ;
			}
		}
	},

	deleteContents : function()
	{
		if ( this.collapsed )
			return ;

		this._ExecContentsAction( 0 ) ;
	},

	extractContents : function()
	{
		var docFrag = new FCKDocumentFragment( this._Document ) ;

		if ( !this.collapsed )
			this._ExecContentsAction( 1, docFrag ) ;

		return docFrag ;
	},


	cloneContents : function()
	{
		var docFrag = new FCKDocumentFragment( this._Document ) ;

		if ( !this.collapsed )
			this._ExecContentsAction( 2, docFrag ) ;

		return docFrag ;
	},

	_ExecContentsAction : function( action, docFrag )
	{
		var startNode	= this.startContainer ;
		var endNode		= this.endContainer ;

		var startOffset	= this.startOffset ;
		var endOffset	= this.endOffset ;

		var removeStartNode	= false ;
		var removeEndNode	= false ;








		if ( endNode.nodeType == 3 )
			endNode = endNode.splitText( endOffset ) ;
		else
		{


			if ( endNode.childNodes.length > 0 )
			{

				if ( endOffset > endNode.childNodes.length - 1 )
				{

					endNode = FCKDomTools.InsertAfterNode( endNode.lastChild, this._Document.createTextNode('') ) ;
					removeEndNode = true ;
				}
				else
					endNode = endNode.childNodes[ endOffset ] ;
			}
		}



		if ( startNode.nodeType == 3 )
		{
			startNode.splitText( startOffset ) ;




			if ( startNode == endNode )
				endNode = startNode.nextSibling ;
		}
		else
		{





			if ( startOffset == 0 )
			{

				startNode = startNode.insertBefore( this._Document.createTextNode(''), startNode.firstChild ) ;
				removeStartNode = true ;
			}
			else if ( startOffset > startNode.childNodes.length - 1 )
			{

				startNode = startNode.appendChild( this._Document.createTextNode('') ) ;
				removeStartNode = true ;
			}
			else
				startNode = startNode.childNodes[ startOffset ].previousSibling ;
		}


		var startParents	= FCKDomTools.GetParents( startNode ) ;
		var endParents		= FCKDomTools.GetParents( endNode ) ;


		var i, topStart, topEnd ;

		for ( i = 0 ; i < startParents.length ; i++ )
		{
			topStart	= startParents[i] ;
			topEnd		= endParents[i] ;





			if ( topStart != topEnd )
				break ;
		}

		var clone, levelStartNode, levelClone, currentNode, currentSibling ;

		if ( docFrag )
			clone = docFrag.RootNode ;



		for ( var j = i ; j < startParents.length ; j++ )
		{
			levelStartNode = startParents[j] ;


			if ( clone && levelStartNode != startNode )		// 0 = Delete
				levelClone = clone.appendChild( levelStartNode.cloneNode( levelStartNode == startNode ) ) ;

			currentNode = levelStartNode.nextSibling ;

			while( currentNode )
			{


				if ( currentNode == endParents[j] || currentNode == endNode )
					break ;


				currentSibling = currentNode.nextSibling ;


				if ( action == 2 )	// 2 = Clone
					clone.appendChild( currentNode.cloneNode( true ) ) ;
				else
				{

					currentNode.parentNode.removeChild( currentNode ) ;


					if ( action == 1 )	// 1 = Extract
						clone.appendChild( currentNode ) ;
				}

				currentNode = currentSibling ;
			}

			if ( clone )
				clone = levelClone ;
		}

		if ( docFrag )
			clone = docFrag.RootNode ;



		for ( var k = i ; k < endParents.length ; k++ )
		{
			levelStartNode = endParents[k] ;


			if ( action > 0 && levelStartNode != endNode )		// 0 = Delete
				levelClone = clone.appendChild( levelStartNode.cloneNode( levelStartNode == endNode ) ) ;


			if ( !startParents[k] || levelStartNode.parentNode != startParents[k].parentNode )
			{
				currentNode = levelStartNode.previousSibling ;

				while( currentNode )
				{


					if ( currentNode == startParents[k] || currentNode == startNode )
						break ;


					currentSibling = currentNode.previousSibling ;


					if ( action == 2 )	// 2 = Clone
						clone.insertBefore( currentNode.cloneNode( true ), clone.firstChild ) ;
					else
					{

						currentNode.parentNode.removeChild( currentNode ) ;


						if ( action == 1 )	// 1 = Extract
							clone.insertBefore( currentNode, clone.firstChild ) ;
					}

					currentNode = currentSibling ;
				}
			}

			if ( clone )
				clone = levelClone ;
		}

		if ( action == 2 )		// 2 = Clone.
		{


			var startTextNode = this.startContainer ;
			if ( startTextNode.nodeType == 3 )
			{
				startTextNode.data += startTextNode.nextSibling.data ;
				startTextNode.parentNode.removeChild( startTextNode.nextSibling ) ;
			}

			var endTextNode = this.endContainer ;
			if ( endTextNode.nodeType == 3 && endTextNode.nextSibling )
			{
				endTextNode.data += endTextNode.nextSibling.data ;
				endTextNode.parentNode.removeChild( endTextNode.nextSibling ) ;
			}
		}
		else
		{




			if ( topStart && topEnd && ( startNode.parentNode != topStart.parentNode || endNode.parentNode != topEnd.parentNode ) )
			{
				var endIndex = FCKDomTools.GetIndexOf( topEnd ) ;



				if ( removeStartNode && topEnd.parentNode == startNode.parentNode )
					endIndex-- ;

				this.setStart( topEnd.parentNode, endIndex ) ;
			}


			this.collapse( true ) ;
		}


		if( removeStartNode )
			startNode.parentNode.removeChild( startNode ) ;

		if( removeEndNode && endNode.parentNode )
			endNode.parentNode.removeChild( endNode ) ;
	},

	cloneRange : function()
	{
		return FCKW3CRange.CreateFromRange( this._Document, this ) ;
	}
} ;