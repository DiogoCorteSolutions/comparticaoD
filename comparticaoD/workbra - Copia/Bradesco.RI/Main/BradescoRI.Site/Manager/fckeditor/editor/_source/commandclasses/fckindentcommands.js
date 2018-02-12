var FCKIndentCommand = function( name, offset )
{
	this.Name = name ;
	this.Offset = offset ;
	this.IndentCSSProperty = FCKConfig.ContentLangDirection.IEquals( 'ltr' ) ? 'marginLeft' : 'marginRight' ;
}

FCKIndentCommand._InitIndentModeParameters = function()
{
	if ( FCKConfig.IndentClasses && FCKConfig.IndentClasses.length > 0 )
	{
		this._UseIndentClasses = true ;
		this._IndentClassMap = {} ;
		for ( var i = 0 ; i < FCKConfig.IndentClasses.length ;i++ )
			this._IndentClassMap[FCKConfig.IndentClasses[i]] = i + 1 ;
		this._ClassNameRegex = new RegExp( '(?:^|\\s+)(' + FCKConfig.IndentClasses.join( '|' ) + ')(?=$|\\s)' ) ;
	}
	else
		this._UseIndentClasses = false ;
}


FCKIndentCommand.prototype =
{
	Execute : function()
	{

		FCKUndo.SaveUndoStep() ;

		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;
		var bookmark = range.CreateBookmark() ;




		var nearestListBlock = FCKDomTools.GetCommonParentNode( range.StartNode || range.StartContainer ,
				range.EndNode || range.EndContainer,
				['ul', 'ol'] ) ;
		if ( nearestListBlock )
			this._IndentList( range, nearestListBlock ) ;
		else
			this._IndentBlock( range ) ;

		range.MoveToBookmark( bookmark ) ;
		range.Select() ;

		FCK.Focus() ;
		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
	},

	GetState : function()
	{

		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG || ! FCK.EditorWindow )
			return FCK_TRISTATE_DISABLED ;


		if ( FCKIndentCommand._UseIndentClasses == undefined )
			FCKIndentCommand._InitIndentModeParameters() ;



		var startContainer = FCKSelection.GetBoundaryParentElement( true ) ;
		var endContainer = FCKSelection.GetBoundaryParentElement( false ) ;
		var listNode = FCKDomTools.GetCommonParentNode( startContainer, endContainer, ['ul','ol'] ) ;

		if ( listNode )
		{
			if ( this.Name.IEquals( 'outdent' ) )
				return FCK_TRISTATE_OFF ;
			var firstItem = FCKTools.GetElementAscensor( startContainer, 'li' ) ;
			if ( !firstItem || !firstItem.previousSibling )
				return FCK_TRISTATE_DISABLED ;
			return FCK_TRISTATE_OFF ;
		}
		if ( ! FCKIndentCommand._UseIndentClasses && this.Name.IEquals( 'indent' ) )
			return FCK_TRISTATE_OFF;

		var path = new FCKElementPath( startContainer ) ;
		var firstBlock = path.Block || path.BlockLimit ;
		if ( !firstBlock )
			return FCK_TRISTATE_DISABLED ;

		if ( FCKIndentCommand._UseIndentClasses )
		{
			var indentClass = firstBlock.className.match( FCKIndentCommand._ClassNameRegex ) ;
			var indentStep = 0 ;
			if ( indentClass != null )
			{
				indentClass = indentClass[1] ;
				indentStep = FCKIndentCommand._IndentClassMap[indentClass] ;
			}
			if ( ( this.Name == 'outdent' && indentStep == 0 ) ||
					( this.Name == 'indent' && indentStep == FCKConfig.IndentClasses.length ) )
				return FCK_TRISTATE_DISABLED ;
			return FCK_TRISTATE_OFF ;
		}
		else
		{
			var indent = parseInt( firstBlock.style[this.IndentCSSProperty], 10 ) ;
			if ( isNaN( indent ) )
				indent = 0 ;
			if ( indent <= 0 )
				return FCK_TRISTATE_DISABLED ;
			return FCK_TRISTATE_OFF ;
		}
	},

	_IndentBlock : function( range )
	{
		var iterator = new FCKDomRangeIterator( range ) ;
		iterator.EnforceRealBlocks = true ;

		range.Expand( 'block_contents' ) ;
		var commonParents = FCKDomTools.GetCommonParents( range.StartContainer, range.EndContainer ) ;
		var nearestParent = commonParents[commonParents.length - 1] ;
		var block ;

		while ( ( block = iterator.GetNextParagraph() ) )
		{


			if ( ! ( block == nearestParent || block.parentNode == nearestParent ) )
				continue ;

			if ( FCKIndentCommand._UseIndentClasses )
			{

				var indentClass = block.className.match( FCKIndentCommand._ClassNameRegex ) ;
				var indentStep = 0 ;
				if ( indentClass != null )
				{
					indentClass = indentClass[1] ;
					indentStep = FCKIndentCommand._IndentClassMap[indentClass] ;
				}


				if ( this.Name.IEquals( 'outdent' ) )
					indentStep-- ;
				else if ( this.Name.IEquals( 'indent' ) )
					indentStep++ ;
				indentStep = Math.min( indentStep, FCKConfig.IndentClasses.length ) ;
				indentStep = Math.max( indentStep, 0 ) ;
				var className = block.className.replace( FCKIndentCommand._ClassNameRegex, '' ) ;
				if ( indentStep < 1 )
					block.className = className ;
				else
					block.className = ( className.length > 0 ? className + ' ' : '' ) +
						FCKConfig.IndentClasses[indentStep - 1] ;
			}
			else
			{

				var currentOffset = parseInt( block.style[this.IndentCSSProperty], 10 ) ;
				if ( isNaN( currentOffset ) )
					currentOffset = 0 ;
				currentOffset += this.Offset ;
				currentOffset = Math.max( currentOffset, 0 ) ;
				currentOffset = Math.ceil( currentOffset / this.Offset ) * this.Offset ;
				block.style[this.IndentCSSProperty] = currentOffset ? currentOffset + FCKConfig.IndentUnit : '' ;
				if ( block.getAttribute( 'style' ) == '' )
					block.removeAttribute( 'style' ) ;
			}
		}
	},

	_IndentList : function( range, listNode )
	{


		var startContainer = range.StartContainer ;
		var endContainer = range.EndContainer ;
		while ( startContainer && startContainer.parentNode != listNode )
			startContainer = startContainer.parentNode ;
		while ( endContainer && endContainer.parentNode != listNode )
			endContainer = endContainer.parentNode ;

		if ( ! startContainer || ! endContainer )
			return ;


		var block = startContainer ;
		var itemsToMove = [] ;
		var stopFlag = false ;
		while ( stopFlag == false )
		{
			if ( block == endContainer )
				stopFlag = true ;
			itemsToMove.push( block ) ;
			block = block.nextSibling ;
		}
		if ( itemsToMove.length < 1 )
			return ;




		var listParents = FCKDomTools.GetParents( listNode ) ;
		for ( var i = 0 ; i < listParents.length ; i++ )
		{
			if ( listParents[i].nodeName.IEquals( ['ul', 'ol'] ) )
			{
				listNode = listParents[i] ;
				break ;
			}
		}
		var indentOffset = this.Name.IEquals( 'indent' ) ? 1 : -1 ;
		var startItem = itemsToMove[0] ;
		var lastItem = itemsToMove[ itemsToMove.length - 1 ] ;
		var markerObj = {} ;


		var listArray = FCKDomTools.ListToArray( listNode, markerObj ) ;


		var baseIndent = listArray[lastItem._FCK_ListArray_Index].indent ;
		for ( var i = startItem._FCK_ListArray_Index ; i <= lastItem._FCK_ListArray_Index ; i++ )
			listArray[i].indent += indentOffset ;
		for ( var i = lastItem._FCK_ListArray_Index + 1 ; i < listArray.length && listArray[i].indent > baseIndent ; i++ )
			listArray[i].indent += indentOffset ;

		var newList = FCKDomTools.ArrayToList( listArray ) ;
		if ( newList )
			listNode.parentNode.replaceChild( newList.listNode, listNode ) ;


		FCKDomTools.ClearAllMarkers( markerObj ) ;
	}
} ;