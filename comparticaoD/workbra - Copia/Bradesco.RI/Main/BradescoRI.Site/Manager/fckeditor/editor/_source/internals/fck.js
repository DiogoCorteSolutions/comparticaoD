var FCK =
{
	Name			: FCKURLParams[ 'InstanceName' ],
	Status			: FCK_STATUS_NOTLOADED,
	EditMode		: FCK_EDITMODE_WYSIWYG,
	Toolbar			: null,
	HasFocus		: false,
	DataProcessor	: new FCKDataProcessor(),

	GetInstanceObject	: (function()
	{
		var w = window ;
		return function( name )
		{
			return w[name] ;
		}
	})(),

	AttachToOnSelectionChange : function( functionPointer )
	{
		this.Events.AttachEvent( 'OnSelectionChange', functionPointer ) ;
	},

	GetLinkedFieldValue : function()
	{
		return this.LinkedField.value ;
	},

	GetParentForm : function()
	{
		return this.LinkedField.form ;
	} ,



	StartupValue : '',

	IsDirty : function()
	{
		if ( this.EditMode == FCK_EDITMODE_SOURCE )
			return ( this.StartupValue != this.EditingArea.Textarea.value ) ;
		else
		{

			if ( ! this.EditorDocument )
				return false ;

			return ( this.StartupValue != this.EditorDocument.body.innerHTML ) ;
		}
	},

	ResetIsDirty : function()
	{
		if ( this.EditMode == FCK_EDITMODE_SOURCE )
			this.StartupValue = this.EditingArea.Textarea.value ;
		else if ( this.EditorDocument.body )
			this.StartupValue = this.EditorDocument.body.innerHTML ;
	},



	StartEditor : function()
	{
		this.TempBaseTag = FCKConfig.BaseHref.length > 0 ? '<base href="' + FCKConfig.BaseHref + '" _fcktemp="true"></base>' : '' ;


		var oKeystrokeHandler = FCK.KeystrokeHandler = new FCKKeystrokeHandler() ;
		oKeystrokeHandler.OnKeystroke = _FCK_KeystrokeHandler_OnKeystroke ;


		oKeystrokeHandler.SetKeystrokes( FCKConfig.Keystrokes ) ;





		if ( FCKBrowserInfo.IsIE7 )
		{
			if ( ( CTRL + 86 /*V*/ ) in oKeystrokeHandler.Keystrokes )
				oKeystrokeHandler.SetKeystrokes( [ CTRL + 86, true ] ) ;

			if ( ( SHIFT + 45 /*INS*/ ) in oKeystrokeHandler.Keystrokes )
				oKeystrokeHandler.SetKeystrokes( [ SHIFT + 45, true ] ) ;
		}


		oKeystrokeHandler.SetKeystrokes( [ CTRL + 8, true ] ) ;

		this.EditingArea = new FCKEditingArea( document.getElementById( 'xEditingArea' ) ) ;
		this.EditingArea.FFSpellChecker = FCKConfig.FirefoxSpellChecker ;


		this.SetData( this.GetLinkedFieldValue(), true ) ;


		FCKTools.AddEventListener( document, "keydown", this._TabKeyHandler ) ;


		this.AttachToOnSelectionChange( _FCK_PaddingNodeListener ) ;
		if ( FCKBrowserInfo.IsGecko )
			this.AttachToOnSelectionChange( this._ExecCheckEmptyBlock ) ;

	},

	Focus : function()
	{
		FCK.EditingArea.Focus() ;
	},

	SetStatus : function( newStatus )
	{
		this.Status = newStatus ;

		if ( newStatus == FCK_STATUS_ACTIVE )
		{
			FCKFocusManager.AddWindow( window, true ) ;

			if ( FCKBrowserInfo.IsIE )
				FCKFocusManager.AddWindow( window.frameElement, true ) ;


			if ( FCKConfig.StartupFocus )
				FCK.Focus() ;
		}

		this.Events.FireEvent( 'OnStatusChange', newStatus ) ;

	},



	FixBody : function()
	{
		var sBlockTag = FCKConfig.EnterMode ;


		if ( sBlockTag != 'p' && sBlockTag != 'div' )
			return ;

		var oDocument = this.EditorDocument ;

		if ( !oDocument )
			return ;

		var oBody = oDocument.body ;

		if ( !oBody )
			return ;

		FCKDomTools.TrimNode( oBody ) ;

		var oNode = oBody.firstChild ;
		var oNewBlock ;

		while ( oNode )
		{
			var bMoveNode = false ;

			switch ( oNode.nodeType )
			{

				case 1 :
					var nodeName = oNode.nodeName.toLowerCase() ;
					if ( !FCKListsLib.BlockElements[ nodeName ] &&
							nodeName != 'li' &&
							!oNode.getAttribute('_fckfakelement') &&
							oNode.getAttribute('_moz_dirty') == null )
						bMoveNode = true ;
					break ;


				case 3 :

					if ( oNewBlock || oNode.nodeValue.Trim().length > 0 )
						bMoveNode = true ;
					break;


				case 8 :
					if ( oNewBlock )
						bMoveNode = true ;
					break;
			}

			if ( bMoveNode )
			{
				var oParent = oNode.parentNode ;

				if ( !oNewBlock )
					oNewBlock = oParent.insertBefore( oDocument.createElement( sBlockTag ), oNode ) ;

				oNewBlock.appendChild( oParent.removeChild( oNode ) ) ;

				oNode = oNewBlock.nextSibling ;
			}
			else
			{
				if ( oNewBlock )
				{
					FCKDomTools.TrimNode( oNewBlock ) ;
					oNewBlock = null ;
				}
				oNode = oNode.nextSibling ;
			}
		}

		if ( oNewBlock )
			FCKDomTools.TrimNode( oNewBlock ) ;
	},

	GetData : function( format )
	{


		if ( FCK.EditMode == FCK_EDITMODE_SOURCE )
				return FCK.EditingArea.Textarea.value ;

		this.FixBody() ;

		var oDoc = FCK.EditorDocument ;
		if ( !oDoc )
			return null ;

		var isFullPage = FCKConfig.FullPage ;


		var data = FCK.DataProcessor.ConvertToDataFormat(
			isFullPage ? oDoc.documentElement : oDoc.body,
			!isFullPage,
			FCKConfig.IgnoreEmptyParagraphValue,
			format ) ;


		data = FCK.ProtectEventsRestore( data ) ;

		if ( FCKBrowserInfo.IsIE )
			data = data.replace( FCKRegexLib.ToReplace, '$1' ) ;

		if ( isFullPage )
		{
			if ( FCK.DocTypeDeclaration && FCK.DocTypeDeclaration.length > 0 )
				data = FCK.DocTypeDeclaration + '\n' + data ;

			if ( FCK.XmlDeclaration && FCK.XmlDeclaration.length > 0 )
				data = FCK.XmlDeclaration + '\n' + data ;
		}

		return FCKConfig.ProtectedSource.Revert( data ) ;
	},

	UpdateLinkedField : function()
	{
		var value = FCK.GetXHTML( FCKConfig.FormatOutput ) ;

		if ( FCKConfig.HtmlEncodeOutput )
			value = FCKTools.HTMLEncode( value ) ;

		FCK.LinkedField.value = value ;
		FCK.Events.FireEvent( 'OnAfterLinkedFieldUpdate' ) ;
	},

	RegisteredDoubleClickHandlers : new Object(),

	OnDoubleClick : function( element )
	{
		var oCalls = FCK.RegisteredDoubleClickHandlers[ element.tagName.toUpperCase() ] ;

		if ( oCalls )
		{
			for ( var i = 0 ; i < oCalls.length ; i++ )
				oCalls[ i ]( element ) ;
		}


		oCalls = FCK.RegisteredDoubleClickHandlers[ '*' ] ;

		if ( oCalls )
		{
			for ( var i = 0 ; i < oCalls.length ; i++ )
				oCalls[ i ]( element ) ;
		}

	},


	RegisterDoubleClickHandler : function( handlerFunction, tag )
	{
		var nodeName = tag || '*' ;
		nodeName = nodeName.toUpperCase() ;

		var aTargets ;

		if ( !( aTargets = FCK.RegisteredDoubleClickHandlers[ nodeName ] ) )
			FCK.RegisteredDoubleClickHandlers[ nodeName ] = [ handlerFunction ] ;
		else
		{


			if ( aTargets.IndexOf( handlerFunction ) == -1 )
				aTargets.push( handlerFunction ) ;
		}

	},

	OnAfterSetHTML : function()
	{
		FCKDocumentProcessor.Process( FCK.EditorDocument ) ;
		FCKUndo.SaveUndoStep() ;

		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
		FCK.Events.FireEvent( 'OnAfterSetHTML' ) ;
	},



	ProtectUrls : function( html )
	{

		html = html.replace( FCKRegexLib.ProtectUrlsA	, '$& _fcksavedurl=$1' ) ;


		html = html.replace( FCKRegexLib.ProtectUrlsImg	, '$& _fcksavedurl=$1' ) ;


		html = html.replace( FCKRegexLib.ProtectUrlsArea	, '$& _fcksavedurl=$1' ) ;

		return html ;
	},



	ProtectEvents : function( html )
	{
		return html.replace( FCKRegexLib.TagsWithEvent, _FCK_ProtectEvents_ReplaceTags ) ;
	},

	ProtectEventsRestore : function( html )
	{
		return html.replace( FCKRegexLib.ProtectedEvents, _FCK_ProtectEvents_RestoreEvents ) ;
	},

	ProtectTags : function( html )
	{
		var sTags = FCKConfig.ProtectedTags ;


		if ( FCKBrowserInfo.IsIE )
			sTags += sTags.length > 0 ? '|ABBR|XML|EMBED|OBJECT' : 'ABBR|XML|EMBED|OBJECT' ;

		var oRegex ;
		if ( sTags.length > 0 )
		{
			oRegex = new RegExp( '<(' + sTags + ')(?!\w|:)', 'gi' ) ;
			html = html.replace( oRegex, '<FCK:$1' ) ;

			oRegex = new RegExp( '<\/(' + sTags + ')>', 'gi' ) ;
			html = html.replace( oRegex, '<\/FCK:$1>' ) ;
		}








		sTags = 'META' ;
		if ( FCKBrowserInfo.IsIE )
			sTags += '|HR' ;

		oRegex = new RegExp( '<((' + sTags + ')(?=\\s|>|/)[\\s\\S]*?)/?>', 'gi' ) ;
		html = html.replace( oRegex, '<FCK:$1 />' ) ;

		return html ;
	},

	SetData : function( data, resetIsDirty )
	{
		this.EditingArea.Mode = FCK.EditMode ;


		if ( FCKBrowserInfo.IsIE && FCK.EditorDocument )
		{
			FCK.EditorDocument.detachEvent("onselectionchange", Doc_OnSelectionChange ) ;
		}

		FCKTempBin.Reset() ;



		FCK.Selection.Release() ;

		if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
		{

			this._ForceResetIsDirty = ( resetIsDirty === true ) ;



			data = FCKConfig.ProtectedSource.Protect( data ) ;


			data = FCK.DataProcessor.ConvertToHtml( data ) ;


			data = data.replace( FCKRegexLib.InvalidSelfCloseTags, '$1></$2>' ) ;


			data = FCK.ProtectEvents( data ) ;


			data = FCK.ProtectUrls( data ) ;
			data = FCK.ProtectTags( data ) ;




			if ( FCK.TempBaseTag.length > 0 && !FCKRegexLib.HasBaseTag.test( data ) )
				data = data.replace( FCKRegexLib.HeadOpener, '$&' + FCK.TempBaseTag ) ;


			var sHeadExtra = '' ;

			if ( !FCKConfig.FullPage )
				sHeadExtra += _FCK_GetEditorAreaStyleTags() ;

			if ( FCKBrowserInfo.IsIE )
				sHeadExtra += FCK._GetBehaviorsStyle() ;
			else if ( FCKConfig.ShowBorders )
				sHeadExtra += FCKTools.GetStyleHtml( FCK_ShowTableBordersCSS, true ) ;

			sHeadExtra += FCKTools.GetStyleHtml( FCK_InternalCSS, true ) ;





			data = data.replace( FCKRegexLib.HeadCloser, sHeadExtra + '$&' ) ;


			this.EditingArea.OnLoad = _FCK_EditingArea_OnLoad ;
			this.EditingArea.Start( data ) ;
		}
		else
		{


			FCK.EditorWindow	= null ;
			FCK.EditorDocument	= null ;
			FCKDomTools.PaddingNode = null ;

			this.EditingArea.OnLoad = null ;
			this.EditingArea.Start( data ) ;


			this.EditingArea.Textarea._FCKShowContextMenu = true ;


			FCK.EnterKeyHandler = null ;

			if ( resetIsDirty )
				this.ResetIsDirty() ;


			FCK.KeystrokeHandler.AttachToElement( this.EditingArea.Textarea ) ;

			this.EditingArea.Textarea.focus() ;

			FCK.Events.FireEvent( 'OnAfterSetHTML' ) ;
		}

		if ( FCKBrowserInfo.IsGecko )
			window.onresize() ;
	},



	RedirectNamedCommands : new Object(),

	ExecuteNamedCommand : function( commandName, commandParameter, noRedirect, noSaveUndo )
	{
		if ( !noSaveUndo )
			FCKUndo.SaveUndoStep() ;

		if ( !noRedirect && FCK.RedirectNamedCommands[ commandName ] != null )
			FCK.ExecuteRedirectedNamedCommand( commandName, commandParameter ) ;
		else
		{
			FCK.Focus() ;
			FCK.EditorDocument.execCommand( commandName, false, commandParameter ) ;
			FCK.Events.FireEvent( 'OnSelectionChange' ) ;
		}

		if ( !noSaveUndo )
		FCKUndo.SaveUndoStep() ;
	},

	GetNamedCommandState : function( commandName )
	{
		try
		{


			if ( FCKBrowserInfo.IsSafari && FCK.EditorWindow && commandName.IEquals( 'Paste' ) )
				return FCK_TRISTATE_OFF ;

			if ( !FCK.EditorDocument.queryCommandEnabled( commandName ) )
				return FCK_TRISTATE_DISABLED ;
			else
			{
				return FCK.EditorDocument.queryCommandState( commandName ) ? FCK_TRISTATE_ON : FCK_TRISTATE_OFF ;
			}
		}
		catch ( e )
		{
			return FCK_TRISTATE_OFF ;
		}
	},

	GetNamedCommandValue : function( commandName )
	{
		var sValue = '' ;
		var eState = FCK.GetNamedCommandState( commandName ) ;

		if ( eState == FCK_TRISTATE_DISABLED )
			return null ;

		try
		{
			sValue = this.EditorDocument.queryCommandValue( commandName ) ;
		}
		catch(e) {}

		return sValue ? sValue : '' ;
	},

	Paste : function( _callListenersOnly )
	{

		if ( FCK.Status != FCK_STATUS_COMPLETE || !FCK.Events.FireEvent( 'OnPaste' ) )
			return false ;


		return _callListenersOnly || FCK._ExecPaste() ;
	},

	PasteFromWord : function()
	{
		FCKDialog.OpenDialog( 'FCKDialog_Paste', FCKLang.PasteFromWord, 'dialog/fck_paste.html', 400, 330, 'Word' ) ;
	},

	Preview : function()
	{
		var sHTML ;

		if ( FCKConfig.FullPage )
		{
			if ( FCK.TempBaseTag.length > 0 )
				sHTML = FCK.TempBaseTag + FCK.GetXHTML() ;
			else
				sHTML = FCK.GetXHTML() ;
		}
		else
		{
			sHTML =
				FCKConfig.DocType +
				'<html dir="' + FCKConfig.ContentLangDirection + '">' +
				'<head>' +
				FCK.TempBaseTag +
				'<title>' + FCKLang.Preview + '</title>' +
				_FCK_GetEditorAreaStyleTags() +
				'</head><body' + FCKConfig.GetBodyAttributes() + '>' +
				FCK.GetXHTML() +
				'</body></html>' ;
		}

		var iWidth	= FCKConfig.ScreenWidth * 0.8 ;
		var iHeight	= FCKConfig.ScreenHeight * 0.7 ;
		var iLeft	= ( FCKConfig.ScreenWidth - iWidth ) / 2 ;

		var sOpenUrl = '' ;
		if ( FCK_IS_CUSTOM_DOMAIN && FCKBrowserInfo.IsIE)
		{
			window._FCKHtmlToLoad = sHTML ;
			sOpenUrl = 'javascript:void( (function(){' +
				'document.open() ;' +
				'document.domain="' + document.domain + '" ;' +
				'document.write( window.opener._FCKHtmlToLoad );' +
				'document.close() ;' +
				'window.opener._FCKHtmlToLoad = null ;' +
				'})() )' ;
		}

		var oWindow = window.open( sOpenUrl, null, 'toolbar=yes,location=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,width=' + iWidth + ',height=' + iHeight + ',left=' + iLeft ) ;

		if ( !FCK_IS_CUSTOM_DOMAIN || !FCKBrowserInfo.IsIE)
		{
			oWindow.document.write( sHTML );
			oWindow.document.close();
		}

	},

	SwitchEditMode : function( noUndo )
	{
		var bIsWysiwyg = ( FCK.EditMode == FCK_EDITMODE_WYSIWYG ) ;


		var bIsDirty = FCK.IsDirty() ;

		var sHtml ;



		if ( bIsWysiwyg )
		{
			FCKCommands.GetCommand( 'ShowBlocks' ).SaveState() ;
			if ( !noUndo && FCKBrowserInfo.IsIE )
				FCKUndo.SaveUndoStep() ;

			sHtml = FCK.GetXHTML( FCKConfig.FormatSource ) ;

			if ( FCKBrowserInfo.IsIE )
				FCKTempBin.ToHtml() ;

			if ( sHtml == null )
				return false ;
		}
		else
			sHtml = this.EditingArea.Textarea.value ;

		FCK.EditMode = bIsWysiwyg ? FCK_EDITMODE_SOURCE : FCK_EDITMODE_WYSIWYG ;

		FCK.SetData( sHtml, !bIsDirty ) ;


		FCK.Focus() ;


		FCKTools.RunFunction( FCK.ToolbarSet.RefreshModeState, FCK.ToolbarSet ) ;

		return true ;
	},

	InsertElement : function( element )
	{

		if ( typeof element == 'string' )
			element = this.EditorDocument.createElement( element ) ;

		var elementName = element.nodeName.toLowerCase() ;

		FCKSelection.Restore() ;



		var range = new FCKDomRange( this.EditorWindow ) ;


		range.MoveToSelection() ;
		range.DeleteContents() ;

		if ( FCKListsLib.BlockElements[ elementName ] != null )
		{
			if ( range.StartBlock )
			{
				if ( range.CheckStartOfBlock() )
					range.MoveToPosition( range.StartBlock, 3 ) ;
				else if ( range.CheckEndOfBlock() )
					range.MoveToPosition( range.StartBlock, 4 ) ;
				else
					range.SplitBlock() ;
			}

			range.InsertNode( element ) ;

			var next = FCKDomTools.GetNextSourceElement( element, false, null, [ 'hr','br','param','img','area','input' ], true ) ;


			if ( !next && FCKConfig.EnterMode != 'br')
			{
				next = this.EditorDocument.body.appendChild( this.EditorDocument.createElement( FCKConfig.EnterMode ) ) ;

				if ( FCKBrowserInfo.IsGeckoLike )
					FCKTools.AppendBogusBr( next ) ;
			}

			if ( FCKListsLib.EmptyElements[ elementName ] == null )
				range.MoveToElementEditStart( element ) ;
			else if ( next )
				range.MoveToElementEditStart( next ) ;
			else
				range.MoveToPosition( element, 4 ) ;

			if ( FCKBrowserInfo.IsGeckoLike )
			{
				if ( next )
					FCKDomTools.ScrollIntoView( next, false );
				FCKDomTools.ScrollIntoView( element, false );
			}
		}
		else
		{

			range.InsertNode( element ) ;



			range.SetStart( element, 4 ) ;
			range.SetEnd( element, 4 ) ;
		}

		range.Select() ;
		range.Release() ;



		this.Focus() ;

		return element ;
	},

	_InsertBlockElement : function( blockElement )
	{
	},

	_IsFunctionKey : function( keyCode )
	{

		if ( keyCode >= 16 && keyCode <= 20 )

			return true ;
		if ( keyCode == 27 || ( keyCode >= 33 && keyCode <= 40 ) )

			return true ;
		if ( keyCode == 45 )

			return true ;
		return false ;
	},

	_KeyDownListener : function( evt )
	{
		if (! evt)
			evt = FCK.EditorWindow.event ;
		if ( FCK.EditorWindow )
		{
			if ( !FCK._IsFunctionKey(evt.keyCode)
					&& !(evt.ctrlKey || evt.metaKey)
					&& !(evt.keyCode == 46) )
				FCK._KeyDownUndo() ;
		}
		return true ;
	},

	_KeyDownUndo : function()
	{
		if ( !FCKUndo.Typing )
		{
			FCKUndo.SaveUndoStep() ;
			FCKUndo.Typing = true ;
			FCK.Events.FireEvent( "OnSelectionChange" ) ;
		}

		FCKUndo.TypesCount++ ;
		FCKUndo.Changed = 1 ;

		if ( FCKUndo.TypesCount > FCKUndo.MaxTypes )
		{
			FCKUndo.TypesCount = 0 ;
			FCKUndo.SaveUndoStep() ;
		}
	},

	_TabKeyHandler : function( evt )
	{
		if ( ! evt )
			evt = window.event ;

		var keystrokeValue = evt.keyCode ;



		if ( keystrokeValue == 9 && FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		{
			if ( FCKBrowserInfo.IsIE )
			{
				var range = document.selection.createRange() ;
				if ( range.parentElement() != FCK.EditingArea.Textarea )
					return true ;
				range.text = '\t' ;
				range.select() ;
			}
			else
			{
				var a = [] ;
				var el = FCK.EditingArea.Textarea ;
				var selStart = el.selectionStart ;
				var selEnd = el.selectionEnd ;
				a.push( el.value.substr(0, selStart ) ) ;
				a.push( '\t' ) ;
				a.push( el.value.substr( selEnd ) ) ;
				el.value = a.join( '' ) ;
				el.setSelectionRange( selStart + 1, selStart + 1 ) ;
			}

			if ( evt.preventDefault )
				return evt.preventDefault() ;

			return evt.returnValue = false ;
		}

		return true ;
	}
} ;

FCK.Events = new FCKEvents( FCK ) ;


FCK.GetHTML	= FCK.GetXHTML = FCK.GetData ;


FCK.SetHTML = FCK.SetData ;


FCK.InsertElementAndGetIt = FCK.CreateElement = FCK.InsertElement ;


function _FCK_ProtectEvents_ReplaceTags( tagMatch )
{
	return tagMatch.replace( FCKRegexLib.EventAttributes, _FCK_ProtectEvents_ReplaceEvents ) ;
}




function _FCK_ProtectEvents_ReplaceEvents( eventMatch, attName )
{
	return ' ' + attName + '_fckprotectedatt="' + encodeURIComponent( eventMatch ) + '"' ;
}

function _FCK_ProtectEvents_RestoreEvents( match, encodedOriginal )
{
	return decodeURIComponent( encodedOriginal ) ;
}

function _FCK_MouseEventsListener( evt )
{
	if ( ! evt )
		evt = window.event ;
	if ( evt.type == 'mousedown' )
		FCK.MouseDownFlag = true ;
	else if ( evt.type == 'mouseup' )
		FCK.MouseDownFlag = false ;
	else if ( evt.type == 'mousemove' )
		FCK.Events.FireEvent( 'OnMouseMove', evt ) ;
}

function _FCK_PaddingNodeListener()
{
	if ( FCKConfig.EnterMode.IEquals( 'br' ) )
		return ;
	FCKDomTools.EnforcePaddingNode( FCK.EditorDocument, FCKConfig.EnterMode ) ;

	if ( ! FCKBrowserInfo.IsIE && FCKDomTools.PaddingNode )
	{


		var sel = FCKSelection.GetSelection() ;
		if ( sel && sel.rangeCount == 1 )
		{
			var range = sel.getRangeAt( 0 ) ;
			if ( range.collapsed && range.startContainer == FCK.EditorDocument.body && range.startOffset == 0 )
			{
				range.selectNodeContents( FCKDomTools.PaddingNode ) ;
				range.collapse( true ) ;
				sel.removeAllRanges() ;
				sel.addRange( range ) ;
			}
		}
	}
	else if ( FCKDomTools.PaddingNode )
	{


		var parentElement = FCKSelection.GetParentElement() ;
		var paddingNode = FCKDomTools.PaddingNode ;
		if ( parentElement && parentElement.nodeName.IEquals( 'body' ) )
		{
			if ( FCK.EditorDocument.body.childNodes.length == 1
					&& FCK.EditorDocument.body.firstChild == paddingNode )
			{
				if ( FCKSelection._GetSelectionDocument( FCK.EditorDocument.selection ) != FCK.EditorDocument )
					return ;

				var range = FCK.EditorDocument.body.createTextRange() ;
				var clearContents = false ;
				if ( !paddingNode.childNodes.firstChild )
				{
					paddingNode.appendChild( FCKTools.GetElementDocument( paddingNode ).createTextNode( '\ufeff' ) ) ;
					clearContents = true ;
				}
				range.moveToElementText( paddingNode ) ;
				range.select() ;
				if ( clearContents )
					range.pasteHTML( '' ) ;
			}
		}
	}
}

function _FCK_EditingArea_OnLoad()
{

	FCK.EditorWindow	= FCK.EditingArea.Window ;
	FCK.EditorDocument	= FCK.EditingArea.Document ;

	if ( FCKBrowserInfo.IsIE )
		FCKTempBin.ToElements() ;

	FCK.InitializeBehaviors() ;


	FCK.MouseDownFlag = false ;
	FCKTools.AddEventListener( FCK.EditorDocument, 'mousemove', _FCK_MouseEventsListener ) ;
	FCKTools.AddEventListener( FCK.EditorDocument, 'mousedown', _FCK_MouseEventsListener ) ;
	FCKTools.AddEventListener( FCK.EditorDocument, 'mouseup', _FCK_MouseEventsListener ) ;



	if ( FCKBrowserInfo.IsSafari )
	{
		var undoFunc = function( evt )
		{
			if ( ! ( evt.ctrlKey || evt.metaKey ) )
				return ;
			if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
				return ;
			switch ( evt.keyCode )
			{
				case 89:
					FCKUndo.Redo() ;
					break ;
				case 90:
					FCKUndo.Undo() ;
					break ;
			}
		}

		FCKTools.AddEventListener( FCK.EditorDocument, 'keyup', undoFunc ) ;
	}


	FCK.EnterKeyHandler = new FCKEnterKey( FCK.EditorWindow, FCKConfig.EnterMode, FCKConfig.ShiftEnterMode, FCKConfig.TabSpaces ) ;


	FCK.KeystrokeHandler.AttachToElement( FCK.EditorDocument ) ;

	if ( FCK._ForceResetIsDirty )
		FCK.ResetIsDirty() ;




	if ( FCKBrowserInfo.IsIE && FCK.HasFocus )
		FCK.EditorDocument.body.setActive() ;

	FCK.OnAfterSetHTML() ;


	FCKCommands.GetCommand( 'ShowBlocks' ).RestoreState() ;


	if ( FCK.Status != FCK_STATUS_NOTLOADED )
		return ;

	FCK.SetStatus( FCK_STATUS_ACTIVE ) ;
}

function _FCK_GetEditorAreaStyleTags()
{
	return FCKTools.GetStyleHtml( FCKConfig.EditorAreaCSS ) +
		FCKTools.GetStyleHtml( FCKConfig.EditorAreaStyles ) ;
}

function _FCK_KeystrokeHandler_OnKeystroke( keystroke, keystrokeValue )
{
	if ( FCK.Status != FCK_STATUS_COMPLETE )
		return false ;

	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{
		switch ( keystrokeValue )
		{
			case 'Paste' :
				return !FCK.Paste() ;

			case 'Cut' :
				FCKUndo.SaveUndoStep() ;
				return false ;
		}
	}
	else
	{

		if ( keystrokeValue.Equals( 'Paste', 'Undo', 'Redo', 'SelectAll', 'Cut' ) )
			return false ;
	}



	var oCommand = FCK.Commands.GetCommand( keystrokeValue ) ;


	if ( oCommand.GetState() == FCK_TRISTATE_DISABLED )
		return false ;

	return ( oCommand.Execute.apply( oCommand, FCKTools.ArgumentsToArray( arguments, 2 ) ) !== false ) ;
}



(function()
{






	var oDocument = window.parent.document ;


	var eLinkedField = oDocument.getElementById( FCK.Name ) ;

	var i = 0;
	while ( eLinkedField || i == 0 )
	{
		if ( eLinkedField && eLinkedField.tagName.toLowerCase().Equals( 'input', 'textarea' ) )
		{
			FCK.LinkedField = eLinkedField ;
			break ;
		}

		eLinkedField = oDocument.getElementsByName( FCK.Name )[i++] ;
	}
})() ;

var FCKTempBin =
{
	Elements : new Array(),

	AddElement : function( element )
	{
		var iIndex = this.Elements.length ;
		this.Elements[ iIndex ] = element ;
		return iIndex ;
	},

	RemoveElement : function( index )
	{
		var e = this.Elements[ index ] ;
		this.Elements[ index ] = null ;
		return e ;
	},

	Reset : function()
	{
		var i = 0 ;
		while ( i < this.Elements.length )
			this.Elements[ i++ ] = null ;
		this.Elements.length = 0 ;
	},

	ToHtml : function()
	{
		for ( var i = 0 ; i < this.Elements.length ; i++ )
		{
			this.Elements[i] = '<div>&nbsp;' + this.Elements[i].outerHTML + '</div>' ;
			this.Elements[i].isHtml = true ;
		}
	},

	ToElements : function()
	{
		var node = FCK.EditorDocument.createElement( 'div' ) ;
		for ( var i = 0 ; i < this.Elements.length ; i++ )
		{
			if ( this.Elements[i].isHtml )
			{
				node.innerHTML = this.Elements[i] ;
				this.Elements[i] = node.firstChild.removeChild( node.firstChild.lastChild ) ;
			}
		}
	}
} ;




var FCKFocusManager = FCK.FocusManager =
{
	IsLocked : false,

	AddWindow : function( win, sendToEditingArea )
	{
		var oTarget ;

		if ( FCKBrowserInfo.IsIE )
			oTarget = win.nodeType == 1 ? win : win.frameElement ? win.frameElement : win.document ;
		else if ( FCKBrowserInfo.IsSafari )
			oTarget = win ;
		else
			oTarget = win.document ;

		FCKTools.AddEventListener( oTarget, 'blur', FCKFocusManager_Win_OnBlur ) ;
		FCKTools.AddEventListener( oTarget, 'focus', sendToEditingArea ? FCKFocusManager_Win_OnFocus_Area : FCKFocusManager_Win_OnFocus ) ;
	},

	RemoveWindow : function( win )
	{
		if ( FCKBrowserInfo.IsIE )
			oTarget = win.nodeType == 1 ? win : win.frameElement ? win.frameElement : win.document ;
		else
			oTarget = win.document ;

		FCKTools.RemoveEventListener( oTarget, 'blur', FCKFocusManager_Win_OnBlur ) ;
		FCKTools.RemoveEventListener( oTarget, 'focus', FCKFocusManager_Win_OnFocus_Area ) ;
		FCKTools.RemoveEventListener( oTarget, 'focus', FCKFocusManager_Win_OnFocus ) ;
	},

	Lock : function()
	{
		this.IsLocked = true ;
	},

	Unlock : function()
	{
		if ( this._HasPendingBlur )
			FCKFocusManager._Timer = window.setTimeout( FCKFocusManager_FireOnBlur, 100 ) ;

		this.IsLocked = false ;
	},

	_ResetTimer : function()
	{
		this._HasPendingBlur = false ;

		if ( this._Timer )
		{
			window.clearTimeout( this._Timer ) ;
			delete this._Timer ;
		}
	}
} ;

function FCKFocusManager_Win_OnBlur()
{
	if ( typeof(FCK) != 'undefined' && FCK.HasFocus )
	{
		FCKFocusManager._ResetTimer() ;
		FCKFocusManager._Timer = window.setTimeout( FCKFocusManager_FireOnBlur, 100 ) ;
	}
}

function FCKFocusManager_FireOnBlur()
{
	if ( FCKFocusManager.IsLocked )
		FCKFocusManager._HasPendingBlur = true ;
	else
	{
		FCK.HasFocus = false ;
		FCK.Events.FireEvent( "OnBlur" ) ;
	}
}

function FCKFocusManager_Win_OnFocus_Area()
{

	if ( FCKFocusManager._IsFocusing )
		return ;

	FCKFocusManager._IsFocusing = true ;

	FCK.Focus() ;
	FCKFocusManager_Win_OnFocus() ;




	FCKTools.RunFunction( function()
		{
			delete FCKFocusManager._IsFocusing ;
		} ) ;
}

function FCKFocusManager_Win_OnFocus()
{
	FCKFocusManager._ResetTimer() ;

	if ( !FCK.HasFocus && !FCKFocusManager.IsLocked )
	{
		FCK.HasFocus = true ;
		FCK.Events.FireEvent( "OnFocus" ) ;
	}
}

(function()
{
	var el = window.frameElement ;
	var width = el.width ;
	var height = el.height ;
	if ( /^\d+$/.test( width ) ) width += 'px' ;
	if ( /^\d+$/.test( height ) ) height += 'px' ;
	var style = el.style ;
	style.border = style.padding = style.margin = 0 ;
	style.backgroundColor = 'transparent';
	style.backgroundImage = 'none';
	style.width = width ;
	style.height = height ;
})() ;