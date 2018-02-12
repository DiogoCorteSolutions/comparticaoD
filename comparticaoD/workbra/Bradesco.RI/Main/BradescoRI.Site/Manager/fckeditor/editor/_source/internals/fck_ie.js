FCK.Description = "FCKeditor for Internet Explorer 5.5+" ;

FCK._GetBehaviorsStyle = function()
{
	if ( !FCK._BehaviorsStyle )
	{
		var sBasePath = FCKConfig.BasePath ;
		var sTableBehavior = '' ;
		var sStyle ;



		sStyle = '<style type="text/css" _fcktemp="true">' ;

		if ( FCKConfig.ShowBorders )
			sTableBehavior = 'url(' + sBasePath + 'css/behaviors/showtableborders.htc)' ;


		sStyle += 'INPUT,TEXTAREA,SELECT,.FCK__Anchor,.FCK__PageBreak,.FCK__InputHidden' ;

		if ( FCKConfig.DisableObjectResizing )
		{
			sStyle += ',IMG' ;
			sTableBehavior += ' url(' + sBasePath + 'css/behaviors/disablehandles.htc)' ;
		}

		sStyle += ' { behavior: url(' + sBasePath + 'css/behaviors/disablehandles.htc) ; }' ;

		if ( sTableBehavior.length > 0 )
			sStyle += 'TABLE { behavior: ' + sTableBehavior + ' ; }' ;

		sStyle += '</style>' ;
		FCK._BehaviorsStyle = sStyle ;
	}

	return FCK._BehaviorsStyle ;
}

function Doc_OnMouseUp()
{
	if ( FCK.EditorWindow.event.srcElement.tagName == 'HTML' )
	{
		FCK.Focus() ;
		FCK.EditorWindow.event.cancelBubble	= true ;
		FCK.EditorWindow.event.returnValue	= false ;
	}
}

function Doc_OnPaste()
{
	var body = FCK.EditorDocument.body ;

	body.detachEvent( 'onpaste', Doc_OnPaste ) ;

	var ret = FCK.Paste( !FCKConfig.ForcePasteAsPlainText && !FCKConfig.AutoDetectPasteFromWord ) ;

	body.attachEvent( 'onpaste', Doc_OnPaste ) ;

	return ret ;
}

function Doc_OnDblClick()
{
	FCK.OnDoubleClick( FCK.EditorWindow.event.srcElement ) ;
	FCK.EditorWindow.event.cancelBubble = true ;
}

function Doc_OnSelectionChange()
{

	if ( !FCK.IsSelectionChangeLocked && FCK.EditorDocument )
		FCK.Events.FireEvent( "OnSelectionChange" ) ;
}

function Doc_OnDrop()
{
	if ( FCK.MouseDownFlag )
	{
		FCK.MouseDownFlag = false ;
		return ;
	}

	if ( FCKConfig.ForcePasteAsPlainText )
	{
		var evt = FCK.EditorWindow.event ;

		if ( FCK._CheckIsPastingEnabled() || FCKConfig.ShowDropDialog )
			FCK.PasteAsPlainText( evt.dataTransfer.getData( 'Text' ) ) ;

		evt.returnValue = false ;
		evt.cancelBubble = true ;
	}
}

FCK.InitializeBehaviors = function( dontReturn )
{


	this.EditorDocument.attachEvent( 'onmouseup', Doc_OnMouseUp ) ;


	this.EditorDocument.body.attachEvent( 'onpaste', Doc_OnPaste ) ;


	this.EditorDocument.body.attachEvent( 'ondrop', Doc_OnDrop ) ;


	FCK.ContextMenu._InnerContextMenu.AttachToElement( FCK.EditorDocument.body ) ;

	this.EditorDocument.attachEvent("onkeydown", FCK._KeyDownListener ) ;

	this.EditorDocument.attachEvent("ondblclick", Doc_OnDblClick ) ;

	this.EditorDocument.attachEvent("onbeforedeactivate", function(){ FCKSelection.Save() ; } ) ;


	this.EditorDocument.attachEvent("onselectionchange", Doc_OnSelectionChange ) ;

	FCKTools.AddEventListener( FCK.EditorDocument, 'mousedown', Doc_OnMouseDown ) ;
}

FCK.InsertHtml = function( html )
{
	html = FCKConfig.ProtectedSource.Protect( html ) ;
	html = FCK.ProtectEvents( html ) ;
	html = FCK.ProtectUrls( html ) ;
	html = FCK.ProtectTags( html ) ;


	FCKSelection.Restore() ;
	FCK.EditorWindow.focus() ;

	FCKUndo.SaveUndoStep() ;


	var oSel = FCKSelection.GetSelection() ;


	if ( oSel.type.toLowerCase() == 'control' )
		oSel.clear() ;



	html = '<span id="__fakeFCKRemove__" style="display:none;">fakeFCKRemove</span>' + html ;


	oSel.createRange().pasteHTML( html ) ;


	FCK.EditorDocument.getElementById('__fakeFCKRemove__').removeNode( true ) ;

	FCKDocumentProcessor.Process( FCK.EditorDocument ) ;


	this.Events.FireEvent( "OnSelectionChange" ) ;
}

FCK.SetInnerHtml = function( html )
{
	var oDoc = FCK.EditorDocument ;


	oDoc.body.innerHTML = '<div id="__fakeFCKRemove__">&nbsp;</div>' + html ;
	oDoc.getElementById('__fakeFCKRemove__').removeNode( true ) ;
}

function FCK_PreloadImages()
{
	var oPreloader = new FCKImagePreloader() ;


	oPreloader.AddImages( FCKConfig.PreloadImages ) ;


	oPreloader.AddImages( FCKConfig.SkinPath + 'fck_strip.gif' ) ;

	oPreloader.OnComplete = LoadToolbarSetup ;
	oPreloader.Start() ;
}


function Document_OnContextMenu()
{
	return ( event.srcElement._FCKShowContextMenu == true ) ;
}
document.oncontextmenu = Document_OnContextMenu ;

function FCK_Cleanup()
{
	this.LinkedField = null ;
	this.EditorWindow = null ;
	this.EditorDocument = null ;
}

FCK._ExecPaste = function()
{

	if ( FCK._PasteIsRunning )
		return true ;

	if ( FCKConfig.ForcePasteAsPlainText )
	{
		FCK.PasteAsPlainText() ;
		return false ;
	}

	var sHTML = FCK._CheckIsPastingEnabled( true ) ;

	if ( sHTML === false )
		FCKTools.RunFunction( FCKDialog.OpenDialog, FCKDialog, ['FCKDialog_Paste', FCKLang.Paste, 'dialog/fck_paste.html', 400, 330, 'Security'] ) ;
	else
	{
		if ( FCKConfig.AutoDetectPasteFromWord && sHTML.length > 0 )
		{
			var re = /<\w[^>]*(( class="?MsoNormal"?)|(="mso-))/gi ;
			if ( re.test( sHTML ) )
			{
				if ( confirm( FCKLang.PasteWordConfirm ) )
				{
					FCK.PasteFromWord() ;
					return false ;
				}
			}
		}

		FCK._PasteIsRunning = true ;

		FCK.ExecuteNamedCommand( 'Paste' ) ;


		delete FCK._PasteIsRunning ;
	}

    return false ;
}

FCK.PasteAsPlainText = function( forceText )
{
	if ( !FCK._CheckIsPastingEnabled() )
	{
		FCKDialog.OpenDialog( 'FCKDialog_Paste', FCKLang.PasteAsText, 'dialog/fck_paste.html', 400, 330, 'PlainText' ) ;
		return ;
	}


	var sText = null ;
	if ( ! forceText )
		sText = clipboardData.getData("Text") ;
	else
		sText = forceText ;

	if ( sText && sText.length > 0 )
	{

		sText = FCKTools.HTMLEncode( sText ) ;
		sText = FCKTools.ProcessLineBreaks( window, FCKConfig, sText ) ;

		var closeTagIndex = sText.search( '</p>' ) ;
		var startTagIndex = sText.search( '<p>' ) ;

		if ( ( closeTagIndex != -1 && startTagIndex != -1 && closeTagIndex < startTagIndex )
				|| ( closeTagIndex != -1 && startTagIndex == -1 ) )
		{
			var prefix = sText.substr( 0, closeTagIndex ) ;
			sText = sText.substr( closeTagIndex + 4 ) ;
			this.InsertHtml( prefix ) ;
		}


		FCKUndo.SaveLocked = true ;
		this.InsertHtml( sText ) ;
		FCKUndo.SaveLocked = false ;
	}
}

FCK._CheckIsPastingEnabled = function( returnContents )
{





	FCK._PasteIsEnabled = false ;

	document.body.attachEvent( 'onpaste', FCK_CheckPasting_Listener ) ;



	var oReturn = FCK.GetClipboardHTML() ;

	document.body.detachEvent( 'onpaste', FCK_CheckPasting_Listener ) ;

	if ( FCK._PasteIsEnabled )
	{
		if ( !returnContents )
			oReturn = true ;
	}
	else
		oReturn = false ;

	delete FCK._PasteIsEnabled ;

	return oReturn ;
}

function FCK_CheckPasting_Listener()
{
	FCK._PasteIsEnabled = true ;
}

FCK.GetClipboardHTML = function()
{
	var oDiv = document.getElementById( '___FCKHiddenDiv' ) ;

	if ( !oDiv )
	{
		oDiv = document.createElement( 'DIV' ) ;
		oDiv.id = '___FCKHiddenDiv' ;

		var oDivStyle = oDiv.style ;
		oDivStyle.position		= 'absolute' ;
		oDivStyle.visibility	= oDivStyle.overflow	= 'hidden' ;
		oDivStyle.width			= oDivStyle.height		= 1 ;

		document.body.appendChild( oDiv ) ;
	}

	oDiv.innerHTML = '' ;

	var oTextRange = document.body.createTextRange() ;
	oTextRange.moveToElementText( oDiv ) ;
	oTextRange.execCommand( 'Paste' ) ;

	var sData = oDiv.innerHTML ;
	oDiv.innerHTML = '' ;

	return sData ;
}

FCK.CreateLink = function( url, noUndo )
{

	var aCreatedMenuCircular = new Array() ;


	FCK.ExecuteNamedCommand( 'Unlink', null, false, !!noUndo ) ;

	if ( url.length > 0 )
	{


		if (FCKSelection.GetType() == 'Control')
		{

			var oLink = this.EditorDocument.createElement( 'A' ) ;
			oLink.href = url ;


			var oControl = FCKSelection.GetSelectedElement() ;

			oControl.parentNode.insertBefore(oLink, oControl) ;

			oControl.parentNode.removeChild( oControl ) ;
			oLink.appendChild( oControl ) ;

			return [ oLink ] ;
		}


		var sTempUrl = 'javascript:void(0);/*' + ( new Date().getTime() ) + '*/' ;


		FCK.ExecuteNamedCommand( 'CreateLink', sTempUrl, false, !!noUndo ) ;


		var oMenuCircular = this.EditorDocument.MenuCircular ;

		for ( i = 0 ; i < oMenuCircular.length ; i++ )
		{
			var oLink = oMenuCircular[i] ;



			if ( oLink.getAttribute( 'href', 2 ) == sTempUrl )
			{
				var sInnerHtml = oLink.innerHTML ;
				oLink.href = url ;
				oLink.innerHTML = sInnerHtml ;



				var oLastChild = oLink.lastChild ;
				if ( oLastChild && oLastChild.nodeName == 'BR' )
				{

					FCKDomTools.InsertAfterNode( oLink, oLink.removeChild( oLastChild ) ) ;
				}

				aCreatedMenuCircular.push( oLink ) ;
			}
		}
	}

	return aCreatedMenuCircular ;
}

function _FCK_RemoveDisabledAtt()
{
	this.removeAttribute( 'disabled' ) ;
}

function Doc_OnMouseDown( evt )
{
	var e = evt.srcElement ;




	if ( e.nodeName.IEquals( 'input' ) && e.type.IEquals( ['radio', 'checkbox'] ) && !e.disabled )
	{
		e.disabled = true ;
		FCKTools.SetTimeout( _FCK_RemoveDisabledAtt, 1, e ) ;
	}
}