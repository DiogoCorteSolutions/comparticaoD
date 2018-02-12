var dialog = window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;
var FCK = oEditor.FCK;
var FCKTools	= oEditor.FCKTools ;
var FCKConfig	= oEditor.FCKConfig ;
var FCKBrowserInfo = oEditor.FCKBrowserInfo ;

window.onload = function ()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	var sPastingType = dialog.Args().CustomValue ;

	if ( sPastingType == 'Word' || sPastingType == 'Security' )
	{
		if ( sPastingType == 'Security' )
			document.getElementById( 'xSecurityMsg' ).style.display = '' ;



		var sFrameUrl = !oEditor.FCK_IS_CUSTOM_DOMAIN || !FCKBrowserInfo.IsIE ?
			'javascript:void(0)' :
			'javascript:void( (function(){' +
				'document.open() ;' +
				'document.domain=\'' + document.domain + '\' ;' +
				'document.write(\'<html><head><script>window.onerror = function() { return true ; };<\/script><\/head><body><\/body><\/html>\') ;' +
				'document.close() ;' +
				'document.body.contentEditable = true ;' +
				'window.focus() ;' +
				'})() )' ;

		var eFrameSpace = document.getElementById( 'xFrameSpace' ) ;
		eFrameSpace.innerHTML = '<iframe id="frmData" src="' + sFrameUrl + '" ' +
					'height="98%" width="99%" frameborder="0" style="border: #000000 1px; background-color: #ffffff"><\/iframe>' ;

		var oFrame = eFrameSpace.firstChild ;

		if ( !oEditor.FCK_IS_CUSTOM_DOMAIN || !FCKBrowserInfo.IsIE )
		{

			var oDoc = oFrame.contentWindow.document ;
			oDoc.open() ;
			oDoc.write('<html><head><script>window.onerror = function() { return true ; };<\/script><\/head><body><\/body><\/html>') ;
			oDoc.close() ;

			if ( FCKBrowserInfo.IsIE )
				oDoc.body.contentEditable = true ;
			else
				oDoc.designMode = 'on' ;

			oFrame.contentWindow.focus();
		}
	}
	else
	{
		document.getElementById('txtData').style.display = '' ;
		SelectField( 'txtData' ) ;
	}

	if ( sPastingType != 'Word' )
		document.getElementById('oWordCommands').style.display = 'none' ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
}

function Ok()
{

	oEditor.FCKUndo.SaveUndoStep() ;

	var sHtml ;

	var sPastingType = dialog.Args().CustomValue ;

	if ( sPastingType == 'Word' || sPastingType == 'Security' )
	{
		var oFrame = document.getElementById('frmData') ;
		var oBody ;

		if ( oFrame.contentDocument )
			oBody = oFrame.contentDocument.body ;
		else
			oBody = oFrame.contentWindow.document.body ;

		if ( sPastingType == 'Word' )
		{

			if ( typeof( FCK.CustomCleanWord ) == 'function' )
				sHtml = FCK.CustomCleanWord( oBody, document.getElementById('chkRemoveFont').checked, document.getElementById('chkRemoveStyles').checked ) ;
			else
				sHtml = CleanWord( oBody, document.getElementById('chkRemoveFont').checked, document.getElementById('chkRemoveStyles').checked ) ;
		}
		else
			sHtml = oBody.innerHTML ;


		var re = new RegExp( window.location + "#", "g" ) ;
		sHtml = sHtml.replace( re, '#') ;
	}
	else
	{
		sHtml = oEditor.FCKTools.HTMLEncode( document.getElementById('txtData').value )  ;
		sHtml = FCKTools.ProcessLineBreaks( oEditor, FCKConfig, sHtml ) ;



		var range = new oEditor.FCKDomRange( oEditor.FCK.EditorWindow ) ;
		var oDoc = oEditor.FCK.EditorDocument ;
		dialog.Selection.EnsureSelection() ;
		range.MoveToSelection() ;
		range.DeleteContents() ;
		var marker = [] ;
		for ( var i = 0 ; i < 5 ; i++ )
			marker.push( parseInt(Math.random() * 100000, 10 ) ) ;
		marker = marker.join( "" ) ;
		range.InsertNode ( oDoc.createTextNode( marker ) ) ;
		var bookmark = range.CreateBookmark() ;



		var htmlString = oDoc.body.innerHTML ;
		var index = htmlString.indexOf( marker ) ;


		var htmlList = [] ;
		htmlList.push( htmlString.substr( 0, index ) ) ;
		htmlList.push( sHtml ) ;
		htmlList.push( htmlString.substr( index + marker.length ) ) ;
		htmlString = htmlList.join( "" ) ;

		if ( oEditor.FCKBrowserInfo.IsIE )
			oEditor.FCK.SetInnerHtml( htmlString ) ;
		else
			oDoc.body.innerHTML = htmlString ;

		range.MoveToBookmark( bookmark ) ;
		range.Collapse( false ) ;
		range.Select() ;
		range.Release() ;
		return true ;
	}

	oEditor.FCK.InsertHtml( sHtml ) ;

	return true ;
}





function CleanWord( oNode, bIgnoreFont, bRemoveStyles )
{
	var html = oNode.innerHTML ;

	html = html.replace(/<o:p>\s*<\/o:p>/g, '') ;
	html = html.replace(/<o:p>[\s\S]*?<\/o:p>/g, '&nbsp;') ;


	html = html.replace( /\s*mso-[^:]+:[^;"]+;?/gi, '' ) ;


	html = html.replace( /\s*MARGIN: 0cm 0cm 0pt\s*;/gi, '' ) ;
	html = html.replace( /\s*MARGIN: 0cm 0cm 0pt\s*"/gi, "\"" ) ;

	html = html.replace( /\s*TEXT-INDENT: 0cm\s*;/gi, '' ) ;
	html = html.replace( /\s*TEXT-INDENT: 0cm\s*"/gi, "\"" ) ;

	html = html.replace( /\s*TEXT-ALIGN: [^\s;]+;?"/gi, "\"" ) ;

	html = html.replace( /\s*PAGE-BREAK-BEFORE: [^\s;]+;?"/gi, "\"" ) ;

	html = html.replace( /\s*FONT-VARIANT: [^\s;]+;?"/gi, "\"" ) ;

	html = html.replace( /\s*tab-stops:[^;"]*;?/gi, '' ) ;
	html = html.replace( /\s*tab-stops:[^"]*/gi, '' ) ;


	if ( bIgnoreFont )
	{
		html = html.replace( /\s*face="[^"]*"/gi, '' ) ;
		html = html.replace( /\s*face=[^ >]*/gi, '' ) ;

		html = html.replace( /\s*FONT-FAMILY:[^;"]*;?/gi, '' ) ;
	}


	html = html.replace(/<(\w[^>]*) class=([^ |>]*)([^>]*)/gi, "<$1$3") ;


	if ( bRemoveStyles )
		html = html.replace( /<(\w[^>]*) style="([^\"]*)"([^>]*)/gi, "<$1$3" ) ;


	html = html.replace( /<STYLE[^>]*>[\s\S]*?<\/STYLE[^>]*>/gi, '' ) ;
	html = html.replace( /<(?:META|LINK)[^>]*>\s*/gi, '' ) ;


	html =  html.replace( /\s*style="\s*"/gi, '' ) ;

	html = html.replace( /<SPAN\s*[^>]*>\s*&nbsp;\s*<\/SPAN>/gi, '&nbsp;' ) ;

	html = html.replace( /<SPAN\s*[^>]*><\/SPAN>/gi, '' ) ;


	html = html.replace(/<(\w[^>]*) lang=([^ |>]*)([^>]*)/gi, "<$1$3") ;

	html = html.replace( /<SPAN\s*>([\s\S]*?)<\/SPAN>/gi, '$1' ) ;

	html = html.replace( /<FONT\s*>([\s\S]*?)<\/FONT>/gi, '$1' ) ;


	html = html.replace(/<\\?\?xml[^>]*>/gi, '' ) ;


	html = html.replace( /<w:[^>]*>[\s\S]*?<\/w:[^>]*>/gi, '' ) ;


	html = html.replace(/<\/?\w+:[^>]*>/gi, '' ) ;


	html = html.replace(/<\!--[\s\S]*?-->/g, '' ) ;

	html = html.replace( /<(U|I|STRIKE)>&nbsp;<\/\1>/g, '&nbsp;' ) ;

	html = html.replace( /<H\d>\s*<\/H\d>/gi, '' ) ;


	html = html.replace( /<(\w+)[^>]*\sstyle="[^"]*DISPLAY\s?:\s?none[\s\S]*?<\/\1>/ig, '' ) ;


	html = html.replace( /<(\w[^>]*) language=([^ |>]*)([^>]*)/gi, "<$1$3") ;


	html = html.replace( /<(\w[^>]*) onmouseover="([^\"]*)"([^>]*)/gi, "<$1$3") ;
	html = html.replace( /<(\w[^>]*) onmouseout="([^\"]*)"([^>]*)/gi, "<$1$3") ;

	if ( FCKConfig.CleanWordKeepsStructure )
	{

		html = html.replace( /<H(\d)([^>]*)>/gi, '<h$1>' ) ;


		html = html.replace( /<(H\d)><FONT[^>]*>([\s\S]*?)<\/FONT><\/\1>/gi, '<$1>$2<\/$1>' );
		html = html.replace( /<(H\d)><EM>([\s\S]*?)<\/EM><\/\1>/gi, '<$1>$2<\/$1>' );
	}
	else
	{
		html = html.replace( /<H1([^>]*)>/gi, '<div$1><b><font size="6">' ) ;
		html = html.replace( /<H2([^>]*)>/gi, '<div$1><b><font size="5">' ) ;
		html = html.replace( /<H3([^>]*)>/gi, '<div$1><b><font size="4">' ) ;
		html = html.replace( /<H4([^>]*)>/gi, '<div$1><b><font size="3">' ) ;
		html = html.replace( /<H5([^>]*)>/gi, '<div$1><b><font size="2">' ) ;
		html = html.replace( /<H6([^>]*)>/gi, '<div$1><b><font size="1">' ) ;

		html = html.replace( /<\/H\d>/gi, '<\/font><\/b><\/div>' ) ;


		var re = new RegExp( '(<P)([^>]*>[\\s\\S]*?)(<\/P>)', 'gi' ) ;
		html = html.replace( re, '<div$2<\/div>' ) ;



		html = html.replace( /<([^\s>]+)(\s[^>]*)?>\s*<\/\1>/g, '' ) ;
		html = html.replace( /<([^\s>]+)(\s[^>]*)?>\s*<\/\1>/g, '' ) ;
		html = html.replace( /<([^\s>]+)(\s[^>]*)?>\s*<\/\1>/g, '' ) ;
	}

	return html ;
}