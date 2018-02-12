var FCKEditingArea = function( targetElement )
{
	this.TargetElement = targetElement ;
	this.Mode = FCK_EDITMODE_WYSIWYG ;

	if ( FCK.IECleanup )
		FCK.IECleanup.AddItem( this, FCKEditingArea_Cleanup ) ;
}

FCKEditingArea.prototype.Start = function( html, secondCall )
{
	var eTargetElement	= this.TargetElement ;
	var oTargetDocument	= FCKTools.GetElementDocument( eTargetElement ) ;

	while( eTargetElement.firstChild )
		eTargetElement.removeChild( eTargetElement.firstChild ) ;

	if ( this.Mode == FCK_EDITMODE_WYSIWYG )
	{
		if ( FCK_IS_CUSTOM_DOMAIN )
			html = '<script>document.domain="' + FCK_RUNTIME_DOMAIN + '";</script>' + html ;

		if ( FCKBrowserInfo.IsIE )
			html = html.replace( /(<base[^>]*?)\s*\/?>(?!\s*<\/base>)/gi, '$1></base>' ) ;
		else if ( !secondCall )
		{
			var oMatchBefore = html.match( FCKRegexLib.BeforeBody ) ;
			var oMatchAfter = html.match( FCKRegexLib.AfterBody ) ;

			if ( oMatchBefore && oMatchAfter )
			{
				var sBody = html.substr( oMatchBefore[1].length,
					       html.length - oMatchBefore[1].length - oMatchAfter[1].length ) ;

				html =
					oMatchBefore[1] +
					'&nbsp;' +
					oMatchAfter[1] ;

				if ( FCKBrowserInfo.IsGecko && ( sBody.length == 0 || FCKRegexLib.EmptyParagraph.test( sBody ) ) )
					sBody = '<br type="_moz">' ;

				this._BodyHTML = sBody ;

			}
			else
				this._BodyHTML = html ;
		}

		var oIFrame = this.IFrame = oTargetDocument.createElement( 'iframe' ) ;

		var sOverrideError = '<script type="text/javascript" _fcktemp="true">window.onerror=function(){return true;};</script>' ;

		oIFrame.frameBorder = 0 ;
		oIFrame.style.width = oIFrame.style.height = '100%' ;

		if ( FCK_IS_CUSTOM_DOMAIN && FCKBrowserInfo.IsIE )
		{
			window._FCKHtmlToLoad = html.replace( /<head>/i, '<head>' + sOverrideError ) ;
			oIFrame.src = 'javascript:void( (function(){' +
				'document.open() ;' +
				'document.domain="' + document.domain + '" ;' +
				'document.write( window.parent._FCKHtmlToLoad );' +
				'document.close() ;' +
				'window.parent._FCKHtmlToLoad = null ;' +
				'})() )' ;
		}
		else if ( !FCKBrowserInfo.IsGecko )
		{
			oIFrame.src = 'javascript:void(0)' ;
		}

		eTargetElement.appendChild( oIFrame ) ;

		this.Window = oIFrame.contentWindow ;

		if ( !FCK_IS_CUSTOM_DOMAIN || !FCKBrowserInfo.IsIE )
		{
			var oDoc = this.Window.document ;

			oDoc.open() ;
			oDoc.write( html.replace( /<head>/i, '<head>' + sOverrideError ) ) ;
			oDoc.close() ;
		}

		if ( FCKBrowserInfo.IsAIR )
			FCKAdobeAIR.EditingArea_Start( oDoc, html ) ;

		if ( FCKBrowserInfo.IsGecko10 && !secondCall )
		{
			this.Start( html, true ) ;
			return ;
		}

		if ( oIFrame.readyState && oIFrame.readyState != 'completed' )
		{
			var editArea = this ;

			setTimeout( function()
					{
						try
						{
							editArea.Window.document.documentElement.doScroll("left") ;
						}
						catch(e)
						{
							setTimeout( arguments.callee, 0 ) ;
							return ;
						}
						editArea.Window._FCKEditingArea = editArea ;
						FCKEditingArea_CompleteStart.call( editArea.Window ) ;
					}, 0 ) ;
		}
		else
		{
			this.Window._FCKEditingArea = this ;

			if ( FCKBrowserInfo.IsGecko10 )
				this.Window.setTimeout( FCKEditingArea_CompleteStart, 500 ) ;
			else
				FCKEditingArea_CompleteStart.call( this.Window ) ;
		}
	}
	else
	{
		var eTextarea = this.Textarea = oTargetDocument.createElement( 'textarea' ) ;
		eTextarea.className = 'SourceField' ;
		eTextarea.dir = 'ltr' ;
		FCKDomTools.SetElementStyles( eTextarea,
			{
				width	: '100%',
				height	: '100%',
				border	: 'none',
				resize	: 'none',
				outline	: 'none'
			} ) ;
		eTargetElement.appendChild( eTextarea ) ;

		eTextarea.value = html  ;

		FCKTools.RunFunction( this.OnLoad ) ;
	}
}

function FCKEditingArea_CompleteStart()
{
	if ( !this.document.body )
	{
		this.setTimeout( FCKEditingArea_CompleteStart, 50 ) ;
		return ;
	}

	var oEditorArea = this._FCKEditingArea ;

	oEditorArea.Document = oEditorArea.Window.document ;

	oEditorArea.MakeEditable() ;

	FCKTools.RunFunction( oEditorArea.OnLoad ) ;
}

FCKEditingArea.prototype.MakeEditable = function()
{
	var oDoc = this.Document ;

	if ( FCKBrowserInfo.IsIE )
	{
		oDoc.body.disabled = true ;
		oDoc.body.contentEditable = true ;
		oDoc.body.removeAttribute( "disabled" ) ;
	}
	else
	{
		try
		{
			oDoc.body.spellcheck = ( this.FFSpellChecker !== false ) ;

			if ( this._BodyHTML )
			{
				oDoc.body.innerHTML = this._BodyHTML ;
				oDoc.body.offsetLeft ;
				this._BodyHTML = null ;
			}

			oDoc.designMode = 'on' ;

			oDoc.execCommand( 'enableObjectResizing', false, !FCKConfig.DisableObjectResizing ) ;

			oDoc.execCommand( 'enableInlineTableEditing', false, !FCKConfig.DisableFFTableHandles ) ;
		}
		catch (e)
		{
			FCKTools.AddEventListener( this.Window.frameElement, 'DOMAttrModified', FCKEditingArea_Document_AttributeNodeModified ) ;
		}

	}
}

function FCKEditingArea_Document_AttributeNodeModified( evt )
{
	var editingArea = evt.currentTarget.contentWindow._FCKEditingArea ;

	if ( editingArea._timer )
		window.clearTimeout( editingArea._timer ) ;

	editingArea._timer = FCKTools.SetTimeout( FCKEditingArea_MakeEditableByMutation, 1000, editingArea ) ;
}

function FCKEditingArea_MakeEditableByMutation()
{
	delete this._timer ;
	FCKTools.RemoveEventListener( this.Window.frameElement, 'DOMAttrModified', FCKEditingArea_Document_AttributeNodeModified ) ;
	this.MakeEditable() ;
}

FCKEditingArea.prototype.Focus = function()
{
	try
	{
		if ( this.Mode == FCK_EDITMODE_WYSIWYG )
		{
			if ( FCKBrowserInfo.IsIE )
				this._FocusIE() ;
			else
				this.Window.focus() ;
		}
		else
		{
			var oDoc = FCKTools.GetElementDocument( this.Textarea ) ;
			if ( (!oDoc.hasFocus || oDoc.hasFocus() ) && oDoc.activeElement == this.Textarea )
				return ;

			this.Textarea.focus() ;
		}
	}
	catch(e) {}
}

FCKEditingArea.prototype._FocusIE = function()
{
	this.Document.body.setActive() ;

	this.Window.focus() ;

	var range = this.Document.selection.createRange() ;

	var parentNode = range.parentElement() ;
	var parentTag = parentNode.nodeName.toLowerCase() ;

	if ( parentNode.childNodes.length > 0 ||
		 !( FCKListsLib.BlockElements[parentTag] ||
		    FCKListsLib.NonEmptyBlockElements[parentTag] ) )
	{
		return ;
	}

	range = new FCKDomRange( this.Window ) ;
	range.MoveToElementEditStart( parentNode ) ;
	range.Select() ;
}

function FCKEditingArea_Cleanup()
{
	if ( this.Document )
		this.Document.body.innerHTML = "" ;
	this.TargetElement = null ;
	this.IFrame = null ;
	this.Document = null ;
	this.Textarea = null ;

	if ( this.Window )
	{
		this.Window._FCKEditingArea = null ;
		this.Window = null ;
	}
}