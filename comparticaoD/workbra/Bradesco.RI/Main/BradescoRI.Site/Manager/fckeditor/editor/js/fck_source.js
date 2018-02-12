var oEditor		= window.parent.InnerDialogLoaded() ;
var FCK			= oEditor.FCK ;
var FCKConfig	= oEditor.FCKConfig ;
var FCKTools	= oEditor.FCKTools ;

document.write( FCKTools.GetStyleHtml( GetCommonDialogCss() ) ) ;

window.onload = function()
{
	document.getElementById('txtSource').value = FCK.GetXHTML( FCKConfig.FormatSource ) ;
	
	window.parent.SetOkButton( true ) ;
}

function Ok()
{
	if ( oEditor.FCKBrowserInfo.IsIE )
		oEditor.FCKUndo.SaveUndoStep() ;

	FCK.SetData( document.getElementById('txtSource').value, false ) ;

	return true ;
}