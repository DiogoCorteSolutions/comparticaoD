var FCKFitWindow = function()
{
	this.Name = 'FitWindow' ;
}

FCKFitWindow.prototype.Execute = function()
{
	var eEditorFrame		= window.frameElement ;
	var eEditorFrameStyle	= eEditorFrame.style ;

	var eMainWindow			= parent ;
	var eDocEl				= eMainWindow.document.documentElement ;
	var eBody				= eMainWindow.document.body ;
	var eBodyStyle			= eBody.style ;
	var eParent ;


	var oRange, oEditorScrollPos ;
	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{
		oRange = new FCKDomRange( FCK.EditorWindow ) ;
		oRange.MoveToSelection() ;
		oEditorScrollPos = FCKTools.GetScrollPosition( FCK.EditorWindow ) ;
	}
	else
	{
		var eTextarea = FCK.EditingArea.Textarea ;
		oRange = !FCKBrowserInfo.IsIE && [ eTextarea.selectionStart, eTextarea.selectionEnd ] ;
		oEditorScrollPos = [ eTextarea.scrollLeft, eTextarea.scrollTop ] ;
	}


	if ( !this.IsMaximized )
	{

		if( FCKBrowserInfo.IsIE )
			eMainWindow.attachEvent( 'onresize', FCKFitWindow_Resize ) ;
		else
			eMainWindow.addEventListener( 'resize', FCKFitWindow_Resize, true ) ;


		this._ScrollPos = FCKTools.GetScrollPosition( eMainWindow ) ;


		eParent = eEditorFrame ;

		while( (eParent = eParent.parentNode) )
		{
			if ( eParent.nodeType == 1 )
			{
				eParent._fckSavedStyles = FCKTools.SaveStyles( eParent ) ;
				eParent.style.zIndex = FCKConfig.FloatingPanelsZIndex - 1 ;
			}
		}


		if ( FCKBrowserInfo.IsIE )
		{
			this.documentElementOverflow = eDocEl.style.overflow ;
			eDocEl.style.overflow	= 'hidden' ;
			eBodyStyle.overflow		= 'hidden' ;
		}
		else
		{

			eBodyStyle.overflow = 'hidden' ;
			eBodyStyle.width = '0px' ;
			eBodyStyle.height = '0px' ;
		}


		this._EditorFrameStyles = FCKTools.SaveStyles( eEditorFrame ) ;


		var oViewPaneSize = FCKTools.GetViewPaneSize( eMainWindow ) ;

		eEditorFrameStyle.position	= "absolute";
		eEditorFrame.offsetLeft ;
		eEditorFrameStyle.zIndex	= FCKConfig.FloatingPanelsZIndex - 1;
		eEditorFrameStyle.left		= "0px";
		eEditorFrameStyle.top		= "0px";
		eEditorFrameStyle.width		= oViewPaneSize.Width + "px";
		eEditorFrameStyle.height	= oViewPaneSize.Height + "px";








		if ( !FCKBrowserInfo.IsIE )
		{
			eEditorFrameStyle.borderRight = eEditorFrameStyle.borderBottom = "9999px solid white" ;
			eEditorFrameStyle.backgroundColor		= "white";
		}


		eMainWindow.scrollTo(0, 0);


		var editorPos = FCKTools.GetWindowPosition( eMainWindow, eEditorFrame ) ;
		if ( editorPos.x != 0 )
			eEditorFrameStyle.left = ( -1 * editorPos.x ) + "px" ;
		if ( editorPos.y != 0 )
			eEditorFrameStyle.top = ( -1 * editorPos.y ) + "px" ;

		this.IsMaximized = true ;
	}
	else
	{

		if( FCKBrowserInfo.IsIE )
			eMainWindow.detachEvent( "onresize", FCKFitWindow_Resize ) ;
		else
			eMainWindow.removeEventListener( "resize", FCKFitWindow_Resize, true ) ;


		eParent = eEditorFrame ;

		while( (eParent = eParent.parentNode) )
		{
			if ( eParent._fckSavedStyles )
			{
				FCKTools.RestoreStyles( eParent, eParent._fckSavedStyles ) ;
				eParent._fckSavedStyles = null ;
			}
		}


		if ( FCKBrowserInfo.IsIE )
			eDocEl.style.overflow = this.documentElementOverflow ;


		FCKTools.RestoreStyles( eEditorFrame, this._EditorFrameStyles ) ;


		eMainWindow.scrollTo( this._ScrollPos.X, this._ScrollPos.Y ) ;

		this.IsMaximized = false ;
	}

	FCKToolbarItems.GetItem('FitWindow').RefreshState() ;







	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
		FCK.EditingArea.MakeEditable() ;

	FCK.Focus() ;


	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{
		oRange.Select() ;
		FCK.EditorWindow.scrollTo( oEditorScrollPos.X, oEditorScrollPos.Y ) ;
	}
	else
	{
		if ( !FCKBrowserInfo.IsIE )
		{
			eTextarea.selectionStart = oRange[0] ;
			eTextarea.selectionEnd = oRange[1] ;
		}
		eTextarea.scrollLeft = oEditorScrollPos[0] ;
		eTextarea.scrollTop = oEditorScrollPos[1] ;
	}
}

FCKFitWindow.prototype.GetState = function()
{
	if ( FCKConfig.ToolbarLocation != 'In' )
		return FCK_TRISTATE_DISABLED ;
	else
		return ( this.IsMaximized ? FCK_TRISTATE_ON : FCK_TRISTATE_OFF );
}

function FCKFitWindow_Resize()
{
	var oViewPaneSize = FCKTools.GetViewPaneSize( parent ) ;

	var eEditorFrameStyle = window.frameElement.style ;

	eEditorFrameStyle.width		= oViewPaneSize.Width + 'px' ;
	eEditorFrameStyle.height	= oViewPaneSize.Height + 'px' ;
}