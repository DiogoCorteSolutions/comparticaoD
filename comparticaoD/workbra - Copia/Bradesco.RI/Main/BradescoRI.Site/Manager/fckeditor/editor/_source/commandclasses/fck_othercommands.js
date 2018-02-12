var FCKDialogCommand = function( name, title, url, width, height, getStateFunction, getStateParam, customValue )
{
	this.Name	= name ;
	this.Title	= title ;
	this.Url	= url ;
	this.Width	= width ;
	this.Height	= height ;
	this.CustomValue = customValue ;

	this.GetStateFunction	= getStateFunction ;
	this.GetStateParam		= getStateParam ;

	this.Resizable = false ;
}

FCKDialogCommand.prototype.Execute = function()
{
	FCKDialog.OpenDialog( 'FCKDialog_' + this.Name , this.Title, this.Url, this.Width, this.Height, this.CustomValue, null, this.Resizable ) ;
}

FCKDialogCommand.prototype.GetState = function()
{
	if ( this.GetStateFunction )
		return this.GetStateFunction( this.GetStateParam ) ;
	else
		return FCK.EditMode == FCK_EDITMODE_WYSIWYG ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
}


var FCKUndefinedCommand = function()
{
	this.Name = 'Undefined' ;
}

FCKUndefinedCommand.prototype.Execute = function()
{
	alert( FCKLang.NotImplemented ) ;
}

FCKUndefinedCommand.prototype.GetState = function()
{
	return FCK_TRISTATE_OFF ;
}



var FCKFormatBlockCommand = function()
{}

FCKFormatBlockCommand.prototype =
{
	Name : 'FormatBlock',

	Execute : FCKStyleCommand.prototype.Execute,

	GetState : function()
	{
		return FCK.EditorDocument ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
	}
};



var FCKFontNameCommand = function()
{}

FCKFontNameCommand.prototype =
{
	Name		: 'FontName',
	Execute		: FCKStyleCommand.prototype.Execute,
	GetState	: FCKFormatBlockCommand.prototype.GetState
};


var FCKFontSizeCommand = function()
{}

FCKFontSizeCommand.prototype =
{
	Name		: 'FontSize',
	Execute		: FCKStyleCommand.prototype.Execute,
	GetState	: FCKFormatBlockCommand.prototype.GetState
};


var FCKPreviewCommand = function()
{
	this.Name = 'Preview' ;
}

FCKPreviewCommand.prototype.Execute = function()
{
     FCK.Preview() ;
}

FCKPreviewCommand.prototype.GetState = function()
{
	return FCK_TRISTATE_OFF ;
}


var FCKSaveCommand = function()
{
	this.Name = 'Save' ;
}

FCKSaveCommand.prototype.Execute = function()
{

	var oForm = FCK.GetParentForm() ;

	if ( typeof( oForm.onsubmit ) == 'function' )
	{
		var bRet = oForm.onsubmit() ;
		if ( bRet != null && bRet === false )
			return ;
	}




	if ( typeof( oForm.submit ) == 'function' )
		oForm.submit() ;
	else
		oForm.submit.click() ;
}

FCKSaveCommand.prototype.GetState = function()
{
	return FCK_TRISTATE_OFF ;
}


var FCKNewPageCommand = function()
{
	this.Name = 'NewPage' ;
}

FCKNewPageCommand.prototype.Execute = function()
{
	FCKUndo.SaveUndoStep() ;
	FCK.SetData( '' ) ;
	FCKUndo.Typing = true ;
	FCK.Focus() ;
}

FCKNewPageCommand.prototype.GetState = function()
{
	return FCK_TRISTATE_OFF ;
}


var FCKSourceCommand = function()
{
	this.Name = 'Source' ;
}

FCKSourceCommand.prototype.Execute = function()
{
	if ( FCKConfig.SourcePopup )	// Until v2.2, it was mandatory for FCKBrowserInfo.IsGecko.
	{
		var iWidth	= FCKConfig.ScreenWidth * 0.65 ;
		var iHeight	= FCKConfig.ScreenHeight * 0.65 ;
		FCKDialog.OpenDialog( 'FCKDialog_Source', FCKLang.Source, 'dialog/fck_source.html', iWidth, iHeight, null, null, true ) ;
	}
	else
	    FCK.SwitchEditMode() ;
}

FCKSourceCommand.prototype.GetState = function()
{
	return ( FCK.EditMode == FCK_EDITMODE_WYSIWYG ? FCK_TRISTATE_OFF : FCK_TRISTATE_ON ) ;
}


var FCKUndoCommand = function()
{
	this.Name = 'Undo' ;
}

FCKUndoCommand.prototype.Execute = function()
{
	FCKUndo.Undo() ;
}

FCKUndoCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	return ( FCKUndo.CheckUndoState() ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ) ;
}


var FCKRedoCommand = function()
{
	this.Name = 'Redo' ;
}

FCKRedoCommand.prototype.Execute = function()
{
	FCKUndo.Redo() ;
}

FCKRedoCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	return ( FCKUndo.CheckRedoState() ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ) ;
}


var FCKPageBreakCommand = function()
{
	this.Name = 'PageBreak' ;
}

FCKPageBreakCommand.prototype.Execute = function()
{

	FCKUndo.SaveUndoStep() ;






	var e = FCK.EditorDocument.createElement( 'DIV' ) ;
	e.style.pageBreakAfter = 'always' ;
	e.innerHTML = '<span style="DISPLAY:none">&nbsp;</span>' ;

	var oFakeImage = FCKDocumentProcessor_CreateFakeImage( 'FCK__PageBreak', e ) ;
	var oRange = new FCKDomRange( FCK.EditorWindow ) ;
	oRange.MoveToSelection() ;
	var oSplitInfo = oRange.SplitBlock() ;
	oRange.InsertNode( oFakeImage ) ;

	FCK.Events.FireEvent( 'OnSelectionChange' ) ;
}

FCKPageBreakCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	return 0 ;
}


var FCKUnlinkCommand = function()
{
	this.Name = 'Unlink' ;
}

FCKUnlinkCommand.prototype.Execute = function()
{

	FCKUndo.SaveUndoStep() ;

	if ( FCKBrowserInfo.IsGeckoLike )
	{
		var oLink = FCK.Selection.MoveToAncestorNode( 'A' ) ;

		if ( oLink )
			FCKTools.RemoveOuterTags( oLink ) ;

		return ;
	}

	FCK.ExecuteNamedCommand( this.Name ) ;
}

FCKUnlinkCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	var state = FCK.GetNamedCommandState( this.Name ) ;


	if ( state == FCK_TRISTATE_OFF && FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{
		var oLink = FCKSelection.MoveToAncestorNode( 'A' ) ;
		var bIsAnchor = ( oLink && oLink.name.length > 0 && oLink.href.length == 0 ) ;
		if ( bIsAnchor )
			state = FCK_TRISTATE_DISABLED ;
	}

	return state ;
}

var FCKVisitLinkCommand = function()
{
	this.Name = 'VisitLink';
}
FCKVisitLinkCommand.prototype =
{
	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;
		var state = FCK.GetNamedCommandState( 'Unlink' ) ;

		if ( state == FCK_TRISTATE_OFF )
		{
			var el = FCKSelection.MoveToAncestorNode( 'A' ) ;
			if ( !el.href )
				state = FCK_TRISTATE_DISABLED ;
		}

		return state ;
	},

	Execute : function()
	{
		var el = FCKSelection.MoveToAncestorNode( 'A' ) ;
		var url = el.getAttribute( '_fcksavedurl' ) || el.getAttribute( 'href', 2 ) ;



		if ( ! /:\/\//.test( url ) )
		{
			var baseHref = FCKConfig.BaseHref ;
			var parentWindow = FCK.GetInstanceObject( 'parent' ) ;
			if ( !baseHref )
			{
				baseHref = parentWindow.document.location.href ;
				baseHref = baseHref.substring( 0, baseHref.lastIndexOf( '/' ) + 1 ) ;
			}

			if ( /^\//.test( url ) )
			{
				try
				{
					baseHref = baseHref.match( /^.*:\/\/+[^\/]+/ )[0] ;
				}
				catch ( e )
				{
					baseHref = parentWindow.document.location.protocol + '://' + parentWindow.parent.document.location.host ;
				}
			}

			url = baseHref + url ;
		}

		if ( !window.open( url, '_blank' ) )
			alert( FCKLang.VisitLinkBlocked ) ;
	}
} ;


var FCKSelectAllCommand = function()
{
	this.Name = 'SelectAll' ;
}

FCKSelectAllCommand.prototype.Execute = function()
{
	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{
		FCK.ExecuteNamedCommand( 'SelectAll' ) ;
	}
	else
	{

		var textarea = FCK.EditingArea.Textarea ;
		if ( FCKBrowserInfo.IsIE )
		{
			textarea.createTextRange().execCommand( 'SelectAll' ) ;
		}
		else
		{
			textarea.selectionStart = 0 ;
			textarea.selectionEnd = textarea.value.length ;
		}
		textarea.focus() ;
	}
}

FCKSelectAllCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	return FCK_TRISTATE_OFF ;
}


var FCKPasteCommand = function()
{
	this.Name = 'Paste' ;
}

FCKPasteCommand.prototype =
{
	Execute : function()
	{
		if ( FCKBrowserInfo.IsIE )
			FCK.Paste() ;
		else
			FCK.ExecuteNamedCommand( 'Paste' ) ;
	},

	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;
		return FCK.GetNamedCommandState( 'Paste' ) ;
	}
} ;


var FCKRuleCommand = function()
{
	this.Name = 'Rule' ;
}

FCKRuleCommand.prototype =
{
	Execute : function()
	{
		FCKUndo.SaveUndoStep() ;
		FCK.InsertElement( 'hr' ) ;
	},

	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;
		return FCK.GetNamedCommandState( 'InsertHorizontalRule' ) ;
	}
} ;


var FCKCutCopyCommand = function( isCut )
{
	this.Name = isCut ? 'Cut' : 'Copy' ;
}

FCKCutCopyCommand.prototype =
{
	Execute : function()
	{
		var enabled = false ;

		if ( FCKBrowserInfo.IsIE )
		{




			var onEvent = function()
			{
				enabled = true ;
			} ;

			var eventName = 'on' + this.Name.toLowerCase() ;

			FCK.EditorDocument.body.attachEvent( eventName, onEvent ) ;
			FCK.ExecuteNamedCommand( this.Name ) ;
			FCK.EditorDocument.body.detachEvent( eventName, onEvent ) ;
		}
		else
		{
			try
			{

				FCK.ExecuteNamedCommand( this.Name ) ;
				enabled = true ;
			}
			catch(e){}
		}

		if ( !enabled )
			alert( FCKLang[ 'PasteError' + this.Name ] ) ;
	},

	GetState : function()
	{


		return FCK.EditMode != FCK_EDITMODE_WYSIWYG ?
				FCK_TRISTATE_DISABLED :
				FCK.GetNamedCommandState( 'Cut' ) ;
	}
};

var FCKAnchorDeleteCommand = function()
{
	this.Name = 'AnchorDelete' ;
}

FCKAnchorDeleteCommand.prototype =
{
	Execute : function()
	{
		if (FCK.Selection.GetType() == 'Control')
		{
			FCK.Selection.Delete();
		}
		else
		{
			var oFakeImage = FCK.Selection.GetSelectedElement() ;
			if ( oFakeImage )
			{
				if ( oFakeImage.tagName == 'IMG' && oFakeImage.getAttribute('_fckanchor') )
					oAnchor = FCK.GetRealElement( oFakeImage ) ;
				else
					oFakeImage = null ;
			}


			if ( !oFakeImage )
			{
				oAnchor = FCK.Selection.MoveToAncestorNode( 'A' ) ;
				if ( oAnchor )
					FCK.Selection.SelectNode( oAnchor ) ;
			}


			if ( oAnchor.href.length != 0 )
			{
				oAnchor.removeAttribute( 'name' ) ;

				if ( FCKBrowserInfo.IsIE )
					oAnchor.className = oAnchor.className.replace( FCKRegexLib.FCK_Class, '' ) ;
				return ;
			}



			if ( oFakeImage )
			{
				oFakeImage.parentNode.removeChild( oFakeImage ) ;
				return ;
			}

			if ( oAnchor.innerHTML.length == 0 )
			{
				oAnchor.parentNode.removeChild( oAnchor ) ;
				return ;
			}

			FCKTools.RemoveOuterTags( oAnchor ) ;
		}
		if ( FCKBrowserInfo.IsGecko )
			FCK.Selection.Collapse( true ) ;
	},

	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;
		return FCK.GetNamedCommandState( 'Unlink') ;
	}
};

var FCKDeleteDivCommand = function()
{
}
FCKDeleteDivCommand.prototype =
{
	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;

		var node = FCKSelection.GetParentElement() ;
		var path = new FCKElementPath( node ) ;
		return path.BlockLimit && path.BlockLimit.nodeName.IEquals( 'div' ) ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
	},

	Execute : function()
	{

		FCKUndo.SaveUndoStep() ;


		var nodes = FCKDomTools.GetSelectedDivContainers() ;


		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;
		var bookmark = range.CreateBookmark() ;


		for ( var i = 0 ; i < nodes.length ; i++)
			FCKDomTools.RemoveNode( nodes[i], true ) ;


		range.MoveToBookmark( bookmark ) ;
		range.Select() ;
	}
} ;


var FCKNbsp = function()
{
	this.Name = 'Non Breaking Space' ;
}

FCKNbsp.prototype =
{
	Execute : function()
	{
		FCK.InsertHtml( '&nbsp;' ) ;
	},

	GetState : function()
	{
		return ( FCK.EditMode != FCK_EDITMODE_WYSIWYG ? FCK_TRISTATE_DISABLED : FCK_TRISTATE_OFF ) ;
	}
} ;