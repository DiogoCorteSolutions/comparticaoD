FCK.Description = "FCKeditor for Gecko Browsers" ;

FCK.InitializeBehaviors = function()
{

	if ( window.onresize )
		window.onresize() ;

	FCKFocusManager.AddWindow( this.EditorWindow ) ;

	this.ExecOnSelectionChange = function()
	{
		FCK.Events.FireEvent( "OnSelectionChange" ) ;
	}

	this._ExecDrop = function( evt )
	{
		if ( FCK.MouseDownFlag )
		{
			FCK.MouseDownFlag = false ;
			return ;
		}

		if ( FCKConfig.ForcePasteAsPlainText )
		{
			if ( evt.dataTransfer )
			{
				var text = evt.dataTransfer.getData( 'Text' ) ;
				text = FCKTools.HTMLEncode( text ) ;
				text = FCKTools.ProcessLineBreaks( window, FCKConfig, text ) ;
				FCK.InsertHtml( text ) ;
			}
			else if ( FCKConfig.ShowDropDialog )
				FCK.PasteAsPlainText() ;

			evt.preventDefault() ;
			evt.stopPropagation() ;
		}
	}

	this._ExecCheckCaret = function( evt )
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return ;

		if ( evt.type == 'keypress' )
		{
			var keyCode = evt.keyCode ;



			if ( keyCode < 33 || keyCode > 40 )
				return ;
		}

		var blockEmptyStop = function( node )
		{
			if ( node.nodeType != 1 )
				return false ;
			var tag = node.tagName.toLowerCase() ;
			return ( FCKListsLib.BlockElements[tag] || FCKListsLib.EmptyElements[tag] ) ;
		}

		var moveCursor = function()
		{
			var selection = FCKSelection.GetSelection() ;
			var range = selection.getRangeAt(0) ;
			if ( ! range || ! range.collapsed )
				return ;

			var node = range.endContainer ;


			if ( node.nodeType != 3 )
				return ;

			if ( node.nodeValue.length != range.endOffset )
				return ;


			var parentTag = node.parentNode.tagName.toLowerCase() ;
			if ( ! (  parentTag == 'a' || ( !FCKBrowserInfo.IsOpera && String(node.parentNode.contentEditable) == 'false' ) ||
					( ! ( FCKListsLib.BlockElements[parentTag] || FCKListsLib.NonEmptyBlockElements[parentTag] )
					  && keyCode == 35 ) ) )
				return ;





			var nextTextNode = FCKTools.GetNextTextNode( node, node.parentNode, blockEmptyStop ) ;
			if ( nextTextNode )
				return ;


			range = FCK.EditorDocument.createRange() ;

			nextTextNode = FCKTools.GetNextTextNode( node, node.parentNode.parentNode, blockEmptyStop ) ;
			if ( nextTextNode )
			{



				if ( FCKBrowserInfo.IsOpera && keyCode == 37 )
					return ;




				range.setStart( nextTextNode, 0 ) ;
				range.setEnd( nextTextNode, 0 ) ;
			}
			else
			{

				while ( node.parentNode
					&& node.parentNode != FCK.EditorDocument.body
					&& node.parentNode != FCK.EditorDocument.documentElement
					&& node == node.parentNode.lastChild
					&& ( ! FCKListsLib.BlockElements[node.parentNode.tagName.toLowerCase()]
					  && ! FCKListsLib.NonEmptyBlockElements[node.parentNode.tagName.toLowerCase()] ) )
					node = node.parentNode ;


				if ( FCKListsLib.BlockElements[ parentTag ]
						|| FCKListsLib.EmptyElements[ parentTag ]
						|| node == FCK.EditorDocument.body )
				{

					range.setStart( node, node.childNodes.length ) ;
					range.setEnd( node, node.childNodes.length ) ;
				}
				else
				{


					var stopNode = node.nextSibling ;



					while ( stopNode )
					{
						if ( stopNode.nodeType != 1 )
						{
							stopNode = stopNode.nextSibling ;
							continue ;
						}

						var stopTag = stopNode.tagName.toLowerCase() ;
						if ( FCKListsLib.BlockElements[stopTag] || FCKListsLib.EmptyElements[stopTag]
							|| FCKListsLib.NonEmptyBlockElements[stopTag] )
							break ;
						stopNode = stopNode.nextSibling ;
					}



					var marker = FCK.EditorDocument.createTextNode( '' ) ;
					if ( stopNode )
						node.parentNode.insertBefore( marker, stopNode ) ;
					else
						node.parentNode.appendChild( marker ) ;
					range.setStart( marker, 0 ) ;
					range.setEnd( marker, 0 ) ;
				}
			}

			selection.removeAllRanges() ;
			selection.addRange( range ) ;
			FCK.Events.FireEvent( "OnSelectionChange" ) ;
		}

		setTimeout( moveCursor, 1 ) ;
	}

	this.ExecOnSelectionChangeTimer = function()
	{
		if ( FCK.LastOnChangeTimer )
			window.clearTimeout( FCK.LastOnChangeTimer ) ;

		FCK.LastOnChangeTimer = window.setTimeout( FCK.ExecOnSelectionChange, 100 ) ;
	}

	this.EditorDocument.addEventListener( 'mouseup', this.ExecOnSelectionChange, false ) ;



	this.EditorDocument.addEventListener( 'keyup', this.ExecOnSelectionChangeTimer, false ) ;

	this._DblClickListener = function( e )
	{
		FCK.OnDoubleClick( e.target ) ;
		e.stopPropagation() ;
	}
	this.EditorDocument.addEventListener( 'dblclick', this._DblClickListener, true ) ;


	this.EditorDocument.addEventListener( 'keydown', this._KeyDownListener, false ) ;


	if ( FCKBrowserInfo.IsGecko )
	{
		this.EditorWindow.addEventListener( 'dragdrop', this._ExecDrop, true ) ;
	}
	else if ( FCKBrowserInfo.IsSafari )
	{
		this.EditorDocument.addEventListener( 'dragover', function ( evt )
				{ if ( !FCK.MouseDownFlag && FCK.Config.ForcePasteAsPlainText ) evt.returnValue = false ; }, true ) ;
		this.EditorDocument.addEventListener( 'drop', this._ExecDrop, true ) ;
		this.EditorDocument.addEventListener( 'mousedown',
			function( ev )
			{
				var element = ev.srcElement ;

				if ( element.nodeName.IEquals( 'IMG', 'HR', 'INPUT', 'TEXTAREA', 'SELECT' ) )
				{
					FCKSelection.SelectNode( element ) ;
				}
			}, true ) ;

		this.EditorDocument.addEventListener( 'mouseup',
			function( ev )
			{
				if ( ev.srcElement.nodeName.IEquals( 'INPUT', 'TEXTAREA', 'SELECT' ) )
					ev.preventDefault()
			}, true ) ;

		this.EditorDocument.addEventListener( 'click',
			function( ev )
			{
				if ( ev.srcElement.nodeName.IEquals( 'INPUT', 'TEXTAREA', 'SELECT' ) )
					ev.preventDefault()
			}, true ) ;
	}


	if ( FCKBrowserInfo.IsGecko || FCKBrowserInfo.IsOpera )
	{
		this.EditorDocument.addEventListener( 'keypress', this._ExecCheckCaret, false ) ;
		this.EditorDocument.addEventListener( 'click', this._ExecCheckCaret, false ) ;
	}


	FCK.ContextMenu._InnerContextMenu.SetMouseClickWindow( FCK.EditorWindow ) ;
	FCK.ContextMenu._InnerContextMenu.AttachToElement( FCK.EditorDocument ) ;
}

FCK.MakeEditable = function()
{
	this.EditingArea.MakeEditable() ;
}


function Document_OnContextMenu( e )
{
	if ( !e.target._FCKShowContextMenu )
		e.preventDefault() ;
}
document.oncontextmenu = Document_OnContextMenu ;


FCK._BaseGetNamedCommandState = FCK.GetNamedCommandState ;
FCK.GetNamedCommandState = function( commandName )
{
	switch ( commandName )
	{
		case 'Unlink' :
			return FCKSelection.HasAncestorNode('A') ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
		default :
			return FCK._BaseGetNamedCommandState( commandName ) ;
	}
}


FCK.RedirectNamedCommands =
{
	Print	: true,
	Paste	: true
} ;


FCK.ExecuteRedirectedNamedCommand = function( commandName, commandParameter )
{
	switch ( commandName )
	{
		case 'Print' :
			FCK.EditorWindow.print() ;
			break ;
		case 'Paste' :
			try
			{

				if ( FCKBrowserInfo.IsSafari )
					throw '' ;

				if ( FCK.Paste() )
					FCK.ExecuteNamedCommand( 'Paste', null, true ) ;
			}
			catch (e)	{
				if ( FCKConfig.ForcePasteAsPlainText )
					FCK.PasteAsPlainText() ;
				else
					FCKDialog.OpenDialog( 'FCKDialog_Paste', FCKLang.Paste, 'dialog/fck_paste.html', 400, 330, 'Security' ) ;
			}
			break ;
		default :
			FCK.ExecuteNamedCommand( commandName, commandParameter ) ;
	}
}

FCK._ExecPaste = function()
{

	FCKUndo.SaveUndoStep() ;

	if ( FCKConfig.ForcePasteAsPlainText )
	{
		FCK.PasteAsPlainText() ;
		return false ;
	}

	return true ;
}




FCK.InsertHtml = function( html )
{
	var doc = FCK.EditorDocument,
		range;

	html = FCKConfig.ProtectedSource.Protect( html ) ;
	html = FCK.ProtectEvents( html ) ;
	html = FCK.ProtectUrls( html ) ;
	html = FCK.ProtectTags( html ) ;


	FCKUndo.SaveUndoStep() ;

	if ( FCKBrowserInfo.IsGecko )
	{
		html = html.replace( /&nbsp;$/, '$&<span _fcktemp="1"/>' ) ;

		var docFrag = new FCKDocumentFragment( this.EditorDocument ) ;
		docFrag.AppendHtml( html ) ;

		var lastNode = docFrag.RootNode.lastChild ;

		range = new FCKDomRange( this.EditorWindow ) ;
		range.MoveToSelection() ;
		range.DeleteContents() ;
		range.InsertNode( docFrag.RootNode ) ;

		range.MoveToPosition( lastNode, 4 ) ;
	}
	else
		doc.execCommand( 'inserthtml', false, html ) ;

	this.Focus() ;


	if ( !range )
	{
		range = new FCKDomRange( this.EditorWindow ) ;
		range.MoveToSelection() ;
	}
	var bookmark = range.CreateBookmark() ;

	FCKDocumentProcessor.Process( doc ) ;



	try
	{
		range.MoveToBookmark( bookmark ) ;
		range.Select() ;
	}
	catch ( e ) {}


	this.Events.FireEvent( "OnSelectionChange" ) ;
}

FCK.PasteAsPlainText = function()
{
	FCKTools.RunFunction( FCKDialog.OpenDialog, FCKDialog, ['FCKDialog_Paste', FCKLang.PasteAsText, 'dialog/fck_paste.html', 400, 330, 'PlainText'] ) ;
}

FCK.GetClipboardHTML = function()
{
	return '' ;
}

FCK.CreateLink = function( url, noUndo )
{

	var aCreatedMenuCircular = new Array() ;




	if ( FCKSelection.GetSelection().isCollapsed )
		return aCreatedMenuCircular ;

	FCK.ExecuteNamedCommand( 'Unlink', null, false, !!noUndo ) ;

	if ( url.length > 0 )
	{

		var sTempUrl = 'javascript:void(0);/*' + ( new Date().getTime() ) + '*/' ;


		FCK.ExecuteNamedCommand( 'CreateLink', sTempUrl, false, !!noUndo ) ;


		var oMenuCircularInteractor = this.EditorDocument.evaluate("//a[@href='" + sTempUrl + "']", this.EditorDocument.body, null, XPathResult.UNORDERED_NODE_SNAPSHOT_TYPE, null) ;


		for ( var i = 0 ; i < oMenuCircularInteractor.snapshotLength ; i++ )
		{
			var oLink = oMenuCircularInteractor.snapshotItem( i ) ;
			oLink.href = url ;

			aCreatedMenuCircular.push( oLink ) ;
		}
	}

	return aCreatedMenuCircular ;
}

FCK._FillEmptyBlock = function( emptyBlockNode )
{
	if ( ! emptyBlockNode || emptyBlockNode.nodeType != 1 )
		return ;
	var nodeTag = emptyBlockNode.tagName.toLowerCase() ;
	if ( nodeTag != 'p' && nodeTag != 'div' )
		return ;
	if ( emptyBlockNode.firstChild )
		return ;
	FCKTools.AppendBogusBr( emptyBlockNode ) ;
}

FCK._ExecCheckEmptyBlock = function()
{
	FCK._FillEmptyBlock( FCK.EditorDocument.body.firstChild ) ;
	var sel = FCKSelection.GetSelection() ;
	if ( !sel || sel.rangeCount < 1 )
		return ;
	var range = sel.getRangeAt( 0 );
	FCK._FillEmptyBlock( range.startContainer ) ;
}