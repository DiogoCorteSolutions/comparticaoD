var FCKToolbarBreak = function()
{}

FCKToolbarBreak.prototype.Create = function( targetElement )
{
	var oBreakDiv = targetElement.ownerDocument.createElement( 'div' ) ;

	oBreakDiv.style.clear = oBreakDiv.style.cssFloat = FCKLang.Dir == 'rtl' ? 'right' : 'left' ;

	targetElement.appendChild( oBreakDiv ) ;
}