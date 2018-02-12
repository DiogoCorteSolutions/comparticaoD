var FCKTools	= window.parent.FCKTools ;
var FCKConfig	= window.parent.FCKConfig ;

document.write( FCKTools.GetStyleHtml( FCKConfig.EditorAreaCSS ) ) ;
document.write( FCKTools.GetStyleHtml( FCKConfig.EditorAreaStyles ) ) ;

if ( window.parent.FCKConfig.BaseHref.length > 0 )
	document.write( '<base href="' + window.parent.FCKConfig.BaseHref + '">' ) ;

window.onload = function()
{
	window.parent.SetPreviewElements(
		document.getElementById( 'imgPreview' ),
		document.getElementById( 'lnkPreview' ) ) ;
}