var FCKToolbarBreak = function()
{}

FCKToolbarBreak.prototype.Create = function( targetElement )
{
	var oBreakDiv = FCKTools.GetElementDocument( targetElement ).createElement( 'div' ) ;

	oBreakDiv.className = 'TB_Break' ;

	oBreakDiv.style.clear = FCKLang.Dir == 'rtl' ? 'left' : 'right' ;

	targetElement.appendChild( oBreakDiv ) ;
}