var FCKPanel = function( parentWindow )
{
	this.IsRTL			= ( FCKLang.Dir == 'rtl' ) ;
	this.IsContextMenu	= false ;
	this._LockCounter	= 0 ;

	this._Window = parentWindow || window ;

	var oDocument ;

	if ( FCKBrowserInfo.IsIE )
	{
		this._Popup	= this._Window.createPopup() ;

		var pDoc = this._Window.document ;

		if ( FCK_IS_CUSTOM_DOMAIN && !FCKBrowserInfo.IsIE7 )
		{
			pDoc.domain = FCK_ORIGINAL_DOMAIN ;
			document.domain = FCK_ORIGINAL_DOMAIN ;
		}

		oDocument = this.Document = this._Popup.document ;

		if ( FCK_IS_CUSTOM_DOMAIN )
		{
			oDocument.domain = FCK_RUNTIME_DOMAIN ;
			pDoc.domain = FCK_RUNTIME_DOMAIN ;
			document.domain = FCK_RUNTIME_DOMAIN ;
		}

		FCK.IECleanup.AddItem( this, FCKPanel_Cleanup ) ;
	}
	else
	{
		var oIFrame = this._IFrame = this._Window.document.createElement('iframe') ;
		FCKTools.ResetStyles( oIFrame );
		oIFrame.src					= 'javascript:void(0)' ;
		oIFrame.allowTransparency	= true ;
		oIFrame.frameBorder			= '0' ;
		oIFrame.scrolling			= 'no' ;
		oIFrame.style.width = oIFrame.style.height = '0px' ;
		FCKDomTools.SetElementStyles( oIFrame,
			{
				position	: 'absolute',
				zIndex		: FCKConfig.FloatingPanelsZIndex
			} ) ;

		this._Window.document.body.appendChild( oIFrame ) ;

		var oIFrameWindow = oIFrame.contentWindow ;

		oDocument = this.Document = oIFrameWindow.document ;

		var sBase = '' ;
		if ( FCKBrowserInfo.IsSafari )
			sBase = '<base href="' + window.document.location + '">' ;

		oDocument.open() ;
		oDocument.write( '<html><head>' + sBase + '<\/head><body style="margin:0px;padding:0px;"><\/body><\/html>' ) ;
		oDocument.close() ;

		if( FCKBrowserInfo.IsAIR )
			FCKAdobeAIR.Panel_Contructor( oDocument, window.document.location ) ;

		FCKTools.AddEventListenerEx( oIFrameWindow, 'focus', FCKPanel_Window_OnFocus, this ) ;
		FCKTools.AddEventListenerEx( oIFrameWindow, 'blur', FCKPanel_Window_OnBlur, this ) ;
	}

	oDocument.dir = FCKLang.Dir ;

	FCKTools.AddEventListener( oDocument, 'contextmenu', FCKTools.CancelEvent ) ;

	this.MainNode = oDocument.body.appendChild( oDocument.createElement('DIV') ) ;

	this.MainNode.style.cssFloat = this.IsRTL ? 'right' : 'left' ;
}


FCKPanel.prototype.AppendStyleSheet = function( styleSheet )
{
	FCKTools.AppendStyleSheet( this.Document, styleSheet ) ;
}

FCKPanel.prototype.Preload = function( x, y, relElement )
{
	if ( this._Popup )
		this._Popup.show( x, y, 0, 0, relElement ) ;
}

FCKPanel.prototype.ResizeForSubpanel = function( panel, width, height )
{
	if ( !FCKBrowserInfo.IsIE7 )
		return false ;

	if ( !this._Popup.isOpen )
	{
		this.Subpanel = null ;
		return false ;
	}

	if ( width == 0 && height == 0 )
	{
		if (this.Subpanel !== panel)
			return false ;

		this.Subpanel = null ;
		this.IncreasedX = 0 ;
	}
	else
	{
		this.Subpanel = panel ;

		if ( ( this.IncreasedX >= width ) && ( this.IncreasedY >= height ) )
			return false ;

		this.IncreasedX = Math.max( this.IncreasedX, width ) ;
		this.IncreasedY = Math.max( this.IncreasedY, height ) ;
	}

	var x = this.ShowRect.x ;
	var w = this.IncreasedX ;
	if ( this.IsRTL )
		x  = x - w ;

	var finalWidth = this.ShowRect.w + w ;
	var finalHeight = Math.max( this.ShowRect.h, this.IncreasedY ) ;
	if ( this.ParentPanel )
		this.ParentPanel.ResizeForSubpanel( this, finalWidth, finalHeight ) ;
	this._Popup.show( x, this.ShowRect.y, finalWidth, finalHeight, this.RelativeElement ) ;

	return this.IsRTL ;
}

FCKPanel.prototype.Show = function( x, y, relElement, width, height )
{
	var iMainWidth ;
	var eMainNode = this.MainNode ;

	if ( this._Popup )
	{
		this._Popup.show( x, y, 0, 0, relElement ) ;

		FCKDomTools.SetElementStyles( eMainNode,
			{
				width	: width ? width + 'px' : '',
				height	: height ? height + 'px' : ''
			} ) ;

		iMainWidth = eMainNode.offsetWidth ;

		if ( FCKBrowserInfo.IsIE7 )
		{
			if (this.ParentPanel && this.ParentPanel.ResizeForSubpanel(this, iMainWidth, eMainNode.offsetHeight) )
			{
				FCKTools.RunFunction( this.Show, this, [x, y, relElement] ) ;
				return ;
			}
		}

		if ( this.IsRTL )
		{
			if ( this.IsContextMenu )
				x  = x - iMainWidth + 1 ;
			else if ( relElement )
				x  = ( x * -1 ) + relElement.offsetWidth - iMainWidth ;
		}

		if ( FCKBrowserInfo.IsIE7 )
		{
			this.ShowRect = {x:x, y:y, w:iMainWidth, h:eMainNode.offsetHeight} ;
			this.IncreasedX = 0 ;
			this.IncreasedY = 0 ;
			this.RelativeElement = relElement ;
		}

		this._Popup.show( x, y, iMainWidth, eMainNode.offsetHeight, relElement ) ;

		if ( this.OnHide )
		{
			if ( this._Timer )
				CheckPopupOnHide.call( this, true ) ;

			this._Timer = FCKTools.SetInterval( CheckPopupOnHide, 100, this ) ;
		}
	}
	else
	{
		if ( typeof( FCK.ToolbarSet.CurrentInstance.FocusManager ) != 'undefined' )
			FCK.ToolbarSet.CurrentInstance.FocusManager.Lock() ;

		if ( this.ParentPanel )
		{
			this.ParentPanel.Lock() ;

			FCKPanel_Window_OnBlur( null, this.ParentPanel ) ;
		}

		if ( FCKBrowserInfo.IsGecko && FCKBrowserInfo.IsMac )
		{
			this._IFrame.scrolling = '' ;
			FCKTools.RunFunction( function(){ this._IFrame.scrolling = 'no'; }, this ) ;
		}

		if ( FCK.ToolbarSet.CurrentInstance.GetInstanceObject( 'FCKPanel' )._OpenedPanel &&
				FCK.ToolbarSet.CurrentInstance.GetInstanceObject( 'FCKPanel' )._OpenedPanel != this )
			FCK.ToolbarSet.CurrentInstance.GetInstanceObject( 'FCKPanel' )._OpenedPanel.Hide( false, true ) ;

		FCKDomTools.SetElementStyles( eMainNode,
			{
				width	: width ? width + 'px' : '',
				height	: height ? height + 'px' : ''
			} ) ;

		iMainWidth = eMainNode.offsetWidth ;

		if ( !width )	this._IFrame.width	= 1 ;
		if ( !height )	this._IFrame.height	= 1 ;

		iMainWidth = eMainNode.offsetWidth || eMainNode.firstChild.offsetWidth ;

		var oPos = FCKTools.GetDocumentPosition( this._Window,
			relElement.nodeType == 9 ?
				( FCKTools.IsStrictMode( relElement ) ? relElement.documentElement : relElement.body ) :
				relElement ) ;

		var positionedAncestor = FCKDomTools.GetPositionedAncestor( this._IFrame.parentNode ) ;
		if ( positionedAncestor )
		{
			var nPos = FCKTools.GetDocumentPosition( FCKTools.GetElementWindow( positionedAncestor ), positionedAncestor ) ;
			oPos.x -= nPos.x ;
			oPos.y -= nPos.y ;
		}

		if ( this.IsRTL && !this.IsContextMenu )
			x = ( x * -1 ) ;

		x += oPos.x ;
		y += oPos.y ;

		if ( this.IsRTL )
		{
			if ( this.IsContextMenu )
				x  = x - iMainWidth + 1 ;
			else if ( relElement )
				x  = x + relElement.offsetWidth - iMainWidth ;
		}
		else
		{
			var oViewPaneSize = FCKTools.GetViewPaneSize( this._Window ) ;
			var oScrollPosition = FCKTools.GetScrollPosition( this._Window ) ;

			var iViewPaneHeight	= oViewPaneSize.Height + oScrollPosition.Y ;
			var iViewPaneWidth	= oViewPaneSize.Width + oScrollPosition.X ;

			if ( ( x + iMainWidth ) > iViewPaneWidth )
				x -= x + iMainWidth - iViewPaneWidth ;

			if ( ( y + eMainNode.offsetHeight ) > iViewPaneHeight )
				y -= y + eMainNode.offsetHeight - iViewPaneHeight ;
		}

		FCKDomTools.SetElementStyles( this._IFrame,
			{
				left	: x + 'px',
				top		: y + 'px'
			} ) ;

		this._IFrame.contentWindow.focus() ;
		this._IsOpened = true ;

		var me = this ;
		this._resizeTimer = setTimeout( function()
			{
				var iWidth = eMainNode.offsetWidth || eMainNode.firstChild.offsetWidth ;
				var iHeight = eMainNode.offsetHeight ;
				me._IFrame.style.width = iWidth + 'px' ;
				me._IFrame.style.height = iHeight + 'px' ;

			}, 0 ) ;

		FCK.ToolbarSet.CurrentInstance.GetInstanceObject( 'FCKPanel' )._OpenedPanel = this ;
	}

	FCKTools.RunFunction( this.OnShow, this ) ;
}

FCKPanel.prototype.Hide = function( ignoreOnHide, ignoreFocusManagerUnlock )
{
	if ( this._Popup )
		this._Popup.hide() ;
	else
	{
		if ( !this._IsOpened || this._LockCounter > 0 )
			return ;

		if ( typeof( FCKFocusManager ) != 'undefined' && !ignoreFocusManagerUnlock )
			FCKFocusManager.Unlock() ;

		this._IFrame.style.width = this._IFrame.style.height = '0px' ;

		this._IsOpened = false ;

		if ( this._resizeTimer )
		{
			clearTimeout( this._resizeTimer ) ;
			this._resizeTimer = null ;
		}

		if ( this.ParentPanel )
			this.ParentPanel.Unlock() ;

		if ( !ignoreOnHide )
			FCKTools.RunFunction( this.OnHide, this ) ;
	}
}

FCKPanel.prototype.CheckIsOpened = function()
{
	if ( this._Popup )
		return this._Popup.isOpen ;
	else
		return this._IsOpened ;
}

FCKPanel.prototype.CreateChildPanel = function()
{
	var oWindow = this._Popup ? FCKTools.GetDocumentWindow( this.Document ) : this._Window ;

	var oChildPanel = new FCKPanel( oWindow ) ;
	oChildPanel.ParentPanel = this ;

	return oChildPanel ;
}

FCKPanel.prototype.Lock = function()
{
	this._LockCounter++ ;
}

FCKPanel.prototype.Unlock = function()
{
	if ( --this._LockCounter == 0 && !this.HasFocus )
		this.Hide() ;
}

function FCKPanel_Window_OnFocus( e, panel )
{
	panel.HasFocus = true ;
}

function FCKPanel_Window_OnBlur( e, panel )
{
	panel.HasFocus = false ;

	if ( panel._LockCounter == 0 )
		FCKTools.RunFunction( panel.Hide, panel ) ;
}

function CheckPopupOnHide( forceHide )
{
	if ( forceHide || !this._Popup.isOpen )
	{
		window.clearInterval( this._Timer ) ;
		this._Timer = null ;

		if (this._Popup && this.ParentPanel && !forceHide)
			this.ParentPanel.ResizeForSubpanel(this, 0, 0) ;

		FCKTools.RunFunction( this.OnHide, this ) ;
	}
}

function FCKPanel_Cleanup()
{
	this._Popup = null ;
	this._Window = null ;
	this.Document = null ;
	this.MainNode = null ;
	this.RelativeElement = null ;
}