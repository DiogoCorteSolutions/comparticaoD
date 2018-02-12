var FCKJustifyCommand = function( alignValue )
{
	this.AlignValue = alignValue ;


	var contentDir = FCKConfig.ContentLangDirection.toLowerCase() ;
	this.IsDefaultAlign = ( alignValue == 'left' && contentDir == 'ltr' ) ||
						  ( alignValue == 'right' && contentDir == 'rtl' ) ;


	var cssClassName = this._CssClassName = ( function()
	{
		var classes = FCKConfig.JustifyClasses ;
		if ( classes )
		{
			switch ( alignValue )
			{
				case 'left' :
					return classes[0] || null ;
				case 'center' :
					return classes[1] || null ;
				case 'right' :
					return classes[2] || null ;
				case 'justify' :
					return classes[3] || null ;
			}
		}
		return null ;
	} )() ;

	if ( cssClassName && cssClassName.length > 0 )
		this._CssClassRegex = new RegExp( '(?:^|\\s+)' + cssClassName + '(?=$|\\s)' ) ;
}

FCKJustifyCommand._GetClassNameRegex = function()
{
	var regex = FCKJustifyCommand._ClassRegex ;
	if ( regex != undefined )
		return regex ;

	var names = [] ;

	var classes = FCKConfig.JustifyClasses ;
	if ( classes )
	{
		for ( var i = 0 ; i < 4 ; i++ )
		{
			var className = classes[i] ;
			if ( className && className.length > 0 )
				names.push( className ) ;
		}
	}

	if ( names.length > 0 )
		regex = new RegExp( '(?:^|\\s+)(?:' + names.join( '|' ) + ')(?=$|\\s)' ) ;
	else
		regex = null ;

	return FCKJustifyCommand._ClassRegex = regex ;
}

FCKJustifyCommand.prototype =
{
	Execute : function()
	{

		FCKUndo.SaveUndoStep() ;

		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;

		var currentState = this.GetState() ;
		if ( currentState == FCK_TRISTATE_DISABLED )
			return ;



		var bookmark = range.CreateBookmark() ;

		var cssClassName = this._CssClassName ;


		var iterator = new FCKDomRangeIterator( range ) ;
		var block ;
		while ( ( block = iterator.GetNextParagraph() ) )
		{
			block.removeAttribute( 'align' ) ;

			if ( cssClassName )
			{

				var className = block.className.replace( FCKJustifyCommand._GetClassNameRegex(), '' ) ;


				if ( currentState == FCK_TRISTATE_OFF )
				{
					if ( className.length > 0 )
						className += ' ' ;
					block.className = className + cssClassName ;
				}
				else if ( className.length == 0 )
					FCKDomTools.RemoveAttribute( block, 'class' ) ;
			}
			else
			{
				var style = block.style ;
				if ( currentState == FCK_TRISTATE_OFF )
					style.textAlign = this.AlignValue ;
				else
				{
					style.textAlign = '' ;
					if ( style.cssText.length == 0 )
						block.removeAttribute( 'style' ) ;
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


		var currentAlign ;
		if ( FCKBrowserInfo.IsIE )
			currentAlign = firstBlock.currentStyle.textAlign ;
		else
			currentAlign = FCK.EditorWindow.getComputedStyle( firstBlock, '' ).getPropertyValue( 'text-align' );
		currentAlign = currentAlign.replace( /(-moz-|-webkit-|start|auto)/i, '' );
		if ( ( !currentAlign && this.IsDefaultAlign ) || currentAlign == this.AlignValue )
			return FCK_TRISTATE_ON ;
		return FCK_TRISTATE_OFF ;
	}
} ;