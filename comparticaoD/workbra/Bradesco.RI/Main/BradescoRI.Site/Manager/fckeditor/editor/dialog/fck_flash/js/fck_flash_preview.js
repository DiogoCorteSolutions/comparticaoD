var FCKTools	= window.parent.FCKTools ;
var FCKConfig	= window.parent.FCKConfig ;

document.write( FCKTools.GetStyleHtml( FCKConfig.SkinDialogCSS ) ) ;
document.write( FCKTools.GetStyleHtml( GetCommonDialogCss( '../' ) ) ) ;

if ( window.parent.FCKConfig.BaseHref.length > 0 )
	document.write( '<base href="' + window.parent.FCKConfig.BaseHref + '">' ) ;

window.onload = function()
{
	window.parent.SetPreviewElement( document.body ) ;
}