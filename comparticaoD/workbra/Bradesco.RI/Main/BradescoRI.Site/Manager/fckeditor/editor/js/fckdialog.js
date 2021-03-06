﻿(function()
{
	var d = document.domain ;

	while ( true )
	{

		try
		{
			var parentDomain = ( Args().TopWindow || E ).document.domain ;

			if ( document.domain != parentDomain )
				document.domain = parentDomain ;

			break ;
		}
		catch( e ) {}


		d = d.replace( /.*?(?:\.|$)/, '' ) ;

		if ( d.length == 0 )
			break ;

		document.domain = d ;
	}
})() ;

var E = frameElement._DialogArguments.Editor ;



function Args()
{
	return frameElement._DialogArguments ;
}

function ParentDialog( dialog )
{
	return dialog ? dialog._ParentDialog : frameElement._ParentDialog ;
}

var FCK				= E.FCK ;
var FCKTools		= E.FCKTools ;
var FCKDomTools		= E.FCKDomTools ;
var FCKDialog		= E.FCKDialog ;
var FCKBrowserInfo	= E.FCKBrowserInfo ;
var FCKConfig		= E.FCKConfig ;


window.focus() ;


document.write( FCKTools.GetStyleHtml( FCKConfig.SkinDialogCSS ) ) ;


var langDir = E.FCKLang.Dir ;


if ( FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsIE7 )
	document.write( '<' + 'script type="text/javascript" src="' + FCKConfig.SkinPath + 'fck_dialog_ie6.js"><' + '\/script>' ) ;

FCKTools.RegisterDollarFunction( window ) ;


var Sizer = function()
{
	var bAutoSize = false ;

	var retval = {

		SetAutoSize : function( autoSize )
		{
			bAutoSize = autoSize ;
			this.RefreshSize() ;
		},


		RefreshContainerSize : function()
		{
			var frmMain = $( 'frmMain' ) ;

			if ( frmMain )
			{

				var height = $( 'contents' ).offsetHeight ;


				height -= $( 'TitleArea' ).offsetHeight ;
				height -= $( 'TabsRow' ).offsetHeight ;
				height -= $( 'PopupButtons' ).offsetHeight ;

				frmMain.style.height = Math.max( height, 0 ) + 'px' ;
			}
		},



		ResizeDialog : function( width, height )
		{
			FCKDomTools.SetElementStyles( window.frameElement,
					{
						'width' : width + 'px',
						'height' : height + 'px'
					} ) ;


			if ( typeof window.DoResizeFixes == 'function' )
				window.DoResizeFixes() ;
		},





		RefreshSize : function()
		{
			if ( bAutoSize )
			{
				var frmMain		= $( 'frmMain' ) ;
				var innerDoc	= frmMain.contentWindow.document ;
				var isStrict	= FCKTools.IsStrictMode( innerDoc ) ;


				var innerWidth	= isStrict ? innerDoc.documentElement.scrollWidth : innerDoc.body.scrollWidth ;
				var innerHeight	= isStrict ? innerDoc.documentElement.scrollHeight : innerDoc.body.scrollHeight ;


				var frameSize = FCKTools.GetViewPaneSize( frmMain.contentWindow ) ;

				var deltaWidth	= innerWidth - frameSize.Width ;
				var deltaHeight	= innerHeight - frameSize.Height ;


				if ( deltaWidth <= 0 && deltaHeight <= 0 )
					return ;

				var dialogWidth		= frameElement.offsetWidth + Math.max( deltaWidth, 0 ) ;
				var dialogHeight	= frameElement.offsetHeight + Math.max( deltaHeight, 0 ) ;

				this.ResizeDialog( dialogWidth, dialogHeight ) ;
			}
			this.RefreshContainerSize() ;
		}
	}

	if ( FCKBrowserInfo.IsSafari )
	{
		var originalRefreshSize = retval.RefreshSize ;

		retval.RefreshSize = function()
		{
			FCKTools.SetTimeout( originalRefreshSize, 1, retval ) ;
		}
	}

	if ( FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsIE7 )
	{
		var originalRefreshContainerSize = retval.RefreshContainerSize ;
		retval.RefreshContainerSize = function()
		{
			FCKTools.SetTimeout( originalRefreshContainerSize, 1, retval ) ;
		}
	}

	window.onresize = function()
	{
		retval.RefreshContainerSize() ;
	}

	window.SetAutoSize = FCKTools.Bind( retval, retval.SetAutoSize ) ;

	return retval ;
}() ;


var Throbber = function()
{
	var timer ;

	var updateThrobber = function()
	{
		var throbberParent = $( 'throbberBlock' ) ;
		var throbberBlocks = throbberParent.childNodes ;
		var lastClass = throbberParent.lastChild.className ;


		for ( var i = throbberBlocks.length - 1 ; i > 0 ; i-- )
			throbberBlocks[i].className = throbberBlocks[i-1].className ;


		throbberBlocks[0].className = lastClass ;
	}

	return {
		Show : function( waitMilliseconds )
		{


			if ( waitMilliseconds && waitMilliseconds > 0 )
			{
				timer = FCKTools.SetTimeout( this.Show, waitMilliseconds, this, null, window ) ;
				return ;
			}

			var throbberParent = $( 'throbberBlock' ) ;

			if (throbberParent.childNodes.length == 0)
			{

				var classIds = [ 1,2,3,4,5,4,3,2 ] ;
				while ( classIds.length > 0 )
					throbberParent.appendChild( document.createElement( 'div' ) ).className = ' throbber_' + classIds.shift() ;
			}


			var frm = $( 'contents' ) ;
			var frmCoords = FCKTools.GetDocumentPosition( window, frm ) ;
			var x = frmCoords.x + ( frm.offsetWidth - throbberParent.offsetWidth ) / 2 ;
			var y = frmCoords.y + ( frm.offsetHeight - throbberParent.offsetHeight ) / 2 ;
			throbberParent.style.left = parseInt( x, 10 ) + 'px' ;
			throbberParent.style.top = parseInt( y, 10 ) + 'px' ;


			throbberParent.style.visibility = ''  ;


			$( 'Tabs' ).style.visibility = 'hidden' ;
			$( 'PopupButtons' ).style.visibility = 'hidden' ;


			timer = setInterval( updateThrobber, 100 ) ;
		},

		Hide : function()
		{
			if ( timer )
			{
				clearInterval( timer ) ;
				timer = null ;
			}

			$( 'throbberBlock' ).style.visibility = 'hidden' ;


			$( 'Tabs' ).style.visibility = '' ;
			$( 'PopupButtons' ).style.visibility = '' ;
		}
	} ;
}() ;


var DragAndDrop = function()
{
	var registeredWindows = [] ;
	var lastCoords ;
	var currentPos ;

	var cleanUpHandlers = function()
	{
		for ( var i = 0 ; i < registeredWindows.length ; i++ )
		{
			FCKTools.RemoveEventListener( registeredWindows[i].document, 'mousemove', dragMouseMoveHandler ) ;
			FCKTools.RemoveEventListener( registeredWindows[i].document, 'mouseup', dragMouseUpHandler ) ;
		}
	}

	var dragMouseMoveHandler = function( evt )
	{
		if ( !lastCoords )
			return ;

		if ( !evt )
			evt = FCKTools.GetElementDocument( this ).parentWindow.event ;


		var currentCoords =
		{
			x : evt.screenX,
			y : evt.screenY
		} ;

		currentPos =
		{
			x : currentPos.x + ( currentCoords.x - lastCoords.x ),
			y : currentPos.y + ( currentCoords.y - lastCoords.y )
		} ;

		lastCoords = currentCoords ;

		frameElement.style.left	= currentPos.x + 'px' ;
		frameElement.style.top	= currentPos.y + 'px' ;

		if ( evt.preventDefault )
			evt.preventDefault() ;
		else
			evt.returnValue = false ;
	}

	var dragMouseUpHandler = function( evt )
	{
		if ( !lastCoords )
			return ;
		if ( !evt )
			evt = FCKTools.GetElementDocument( this ).parentWindow.event ;
		cleanUpHandlers() ;
		lastCoords = null ;
	}

	return {

		MouseDownHandler : function( evt )
		{
			var view = null ;
			if ( !evt )
			{
				view = FCKTools.GetElementDocument( this ).parentWindow ;
				evt = view.event ;
			}
			else
				view = evt.view ;

			var target = evt.srcElement || evt.target ;
			if ( target.id == 'closeButton' || target.className == 'PopupTab' || target.className == 'PopupTabSelected' )
				return ;

			lastCoords =
			{
				x : evt.screenX,
				y : evt.screenY
			} ;


			currentPos =
			{
				x : parseInt( FCKDomTools.GetCurrentElementStyle( frameElement, 'left' ), 10 ),
				y : parseInt( FCKDomTools.GetCurrentElementStyle( frameElement, 'top' ), 10 )
			} ;

			for ( var i = 0 ; i < registeredWindows.length ; i++ )
			{
				FCKTools.AddEventListener( registeredWindows[i].document, 'mousemove', dragMouseMoveHandler ) ;
				FCKTools.AddEventListener( registeredWindows[i].document, 'mouseup', dragMouseUpHandler ) ;
			}

			if ( evt.preventDefault )
				evt.preventDefault() ;
			else
				evt.returnValue = false ;
		},

		RegisterHandlers : function( w )
		{
			registeredWindows.push( w ) ;
		}
	}
}() ;



var Selection =
{
	EnsureSelection : function()
	{


		window.focus() ;
		$( 'btnCancel' ).focus() ;

		FCK.Selection.Restore() ;
	},

	GetSelection : function()
	{
		return FCK.Selection ;
	},
	
	GetSelectedElement : function()
	{
		return FCK.Selection.GetSelectedElement() ;
	}
}


var Tabs = function()
{


	var oTabs = new Object() ;

	var setSelectedTab = function( tabCode )
	{
		for ( var sCode in oTabs )
		{
			if ( sCode == tabCode )
				$( oTabs[sCode] ).className = 'PopupTabSelected' ;
			else
				$( oTabs[sCode] ).className = 'PopupTab' ;
		}

		if ( typeof( window.frames["frmMain"].OnDialogTabChange ) == 'function' )
			window.frames["frmMain"].OnDialogTabChange( tabCode ) ;
	}

	function TabDiv_OnClick()
	{
		setSelectedTab( this.TabCode ) ;
	}

	window.AddTab = function( tabCode, tabText, startHidden )
	{
		if ( typeof( oTabs[ tabCode ] ) != 'undefined' )
			return ;

		var eTabsRow = $( 'Tabs' ) ;

		var oCell = eTabsRow.insertCell(  eTabsRow.cells.length - 1 ) ;
		oCell.noWrap = true ;

		var oDiv = document.createElement( 'DIV' ) ;
		oDiv.className = 'PopupTab' ;
		oDiv.innerHTML = tabText ;
		oDiv.TabCode = tabCode ;
		oDiv.onclick = TabDiv_OnClick ;
		oDiv.id = Math.random() ;

		if ( startHidden )
			oDiv.style.display = 'none' ;

		eTabsRow = $( 'TabsRow' ) ;

		oCell.appendChild( oDiv ) ;

		if ( eTabsRow.style.display == 'none' )
		{
			var eTitleArea = $( 'TitleArea' ) ;
			eTitleArea.className = 'PopupTitle' ;

			oDiv.className = 'PopupTabSelected' ;
			eTabsRow.style.display = '' ;

			if ( window.onresize )
				window.onresize() ;
		}

		oTabs[ tabCode ] = oDiv.id ;

		FCKTools.DisableSelection( oDiv ) ;
	} ;

	window.SetSelectedTab = setSelectedTab ;

	window.SetTabVisibility = function( tabCode, isVisible )
	{
		var oTab = $( oTabs[ tabCode ] ) ;
		oTab.style.display = isVisible ? '' : 'none' ;

		if ( ! isVisible && oTab.className == 'PopupTabSelected' )
		{
			for ( var sCode in oTabs )
			{
				if ( $( oTabs[sCode] ).style.display != 'none' )
				{
					setSelectedTab( sCode ) ;
					break ;
				}
			}
		}
	} ;
}() ;






var onReadyRegister = function()
{
	if ( this.readyState != 'complete' )
		return ;
	DragAndDrop.RegisterHandlers( this.contentWindow ) ;
} ;



(function()
{
	var setOnKeyDown = function( targetDocument )
	{
		targetDocument.onkeydown = function ( e )
		{
			e = e || event || this.parentWindow.event ;
			switch ( e.keyCode )
			{
				case 13 :
					var oTarget = e.srcElement || e.target ;
					if ( oTarget.tagName == 'TEXTAREA' )
						return true ;
					Ok() ;
					return false ;

				case 27 :
					Cancel() ;
					return false ;
			}
			return true ;
		}
	} ;

	var contextMenuBlocker = function( e )
	{
		var sTagName = e.target.tagName ;
		if ( ! ( ( sTagName == "INPUT" && e.target.type == "text" ) || sTagName == "TEXTAREA" ) )
			e.preventDefault() ;
	} ;

	var disableContextMenu = function( targetDocument )
	{
		if ( FCKBrowserInfo.IsIE )
			return ;

		targetDocument.addEventListener( 'contextmenu', contextMenuBlocker, true ) ;
	} ;


	window.Init = function()
	{
		$( 'contents' ).dir = langDir;


		Throbber.Show( 1000 ) ;

		Sizer.RefreshContainerSize() ;
		LoadInnerDialog() ;

		FCKTools.DisableSelection( document.body ) ;


		var titleElement = $( 'header' ) ;
		titleElement.onmousedown = DragAndDrop.MouseDownHandler ;


		DragAndDrop.RegisterHandlers( window ) ;
		DragAndDrop.RegisterHandlers( Args().TopWindow ) ;


		if ( ParentDialog() )
		{
			ParentDialog().contentWindow.SetEnabled( false ) ;
			if ( FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsIE7 )
			{
				var currentParent = ParentDialog() ;
				while ( currentParent )
				{
					var blockerFrame = currentParent.contentWindow.$( 'blocker' ) ;
					if ( blockerFrame.readyState == 'complete' )
						DragAndDrop.RegisterHandlers( blockerFrame.contentWindow ) ;
					else
						blockerFrame.onreadystatechange = onReadyRegister ;
					currentParent = ParentDialog( currentParent ) ;
				}
			}
			else
			{
				var currentParent = ParentDialog() ;
				while ( currentParent )
				{
					DragAndDrop.RegisterHandlers( currentParent.contentWindow ) ;
					currentParent = ParentDialog( currentParent ) ;
				}
			}
		}


		if ( FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsIE7 )
		{
			var blockerFrame = FCKDialog.GetCover().firstChild ;
			if ( blockerFrame.readyState == 'complete' )
				DragAndDrop.RegisterHandlers( blockerFrame.contentWindow ) ;
			else
				blockerFrame.onreadystatechange = onReadyRegister;
		}


		setOnKeyDown( document ) ;
		disableContextMenu( document ) ;
	} ;

	window.LoadInnerDialog = function()
	{
		if ( window.onresize )
			window.onresize() ;


		E.FCKLanguageManager.TranslatePage( document ) ;


		$( 'innerContents' ).innerHTML = '<iframe id="frmMain" src="' + Args().Page + '" name="frmMain" frameborder="0" width="100%" height="100%" scrolling="auto" style="visibility: hidden;" allowtransparency="true"><\/iframe>' ;
	} ;

	window.InnerDialogLoaded = function()
	{

		if ( !frameElement.parentNode )
			return null ;

		Throbber.Hide() ;

		var frmMain = $('frmMain') ;
		var innerWindow = frmMain.contentWindow ;
		var innerDoc = innerWindow.document ;


		frmMain.style.visibility = '' ;


		innerDoc.documentElement.dir = langDir ;


		innerDoc.write( FCKTools.GetStyleHtml( FCKConfig.SkinDialogCSS ) ) ;

		setOnKeyDown( innerDoc ) ;
		disableContextMenu( innerDoc ) ;

		Sizer.RefreshContainerSize();

		DragAndDrop.RegisterHandlers( innerWindow ) ;

		innerWindow.focus() ;

		return E ;
	} ;

	window.SetOkButton = function( showIt )
	{
		$('btnOk').style.visibility = ( showIt ? '' : 'hidden' ) ;
	} ;

	window.Ok = function()
	{
		Selection.EnsureSelection() ;

		var frmMain = window.frames["frmMain"] ;

		if ( frmMain.Ok && frmMain.Ok() )
			CloseDialog() ;
		else
			frmMain.focus() ;
	} ;

	window.Cancel = function( dontFireChange )
	{
		Selection.EnsureSelection() ;
		return CloseDialog( dontFireChange ) ;
	} ;

	window.CloseDialog = function( dontFireChange )
	{
		Throbber.Hide() ;



		if ( $( 'frmMain' ) )
			$( 'frmMain' ).src = FCKTools.GetVoidUrl() ;

		if ( !dontFireChange && !FCK.EditMode )
		{





			setTimeout( function()
				{
					FCK.Events.FireEvent( 'OnSelectionChange' ) ;
				}, 0 ) ;
		}

		FCKDialog.OnDialogClose( window ) ;
	} ;

	window.SetEnabled = function( isEnabled )
	{
		var cover = $( 'cover' ) ;
		cover.style.display = isEnabled ? 'none' : '' ;

		if ( FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsIE7 )
		{
			if ( !isEnabled )
			{

				var blocker = document.createElement( 'iframe' ) ;
				blocker.src = FCKTools.GetVoidUrl() ;
				blocker.hideFocus = true ;
				blocker.frameBorder = 0 ;
				blocker.id = blocker.className = 'blocker' ;
				cover.appendChild( blocker ) ;
			}
			else
			{
				var blocker = $( 'blocker' ) ;
				if ( blocker && blocker.parentNode )
					blocker.parentNode.removeChild( blocker ) ;
			}
		}
	} ;
})() ;