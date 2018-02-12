var eBase = parent.FCK.EditorDocument.getElementsByTagName( 'BASE' ) ;
if ( eBase.length > 0 && eBase[0].href.length > 0 )
{
	document.write( '<base href="' + eBase[0].href + '">' ) ;
}

window.onload = function()
{
	if ( typeof( parent.OnPreviewLoad ) == 'function' )
		parent.OnPreviewLoad( window, document.body ) ;
}

function SetBaseHRef( baseHref )
{
	var eBase = document.createElement( 'BASE' ) ;
	eBase.href = baseHref ;

	var eHead = document.getElementsByTagName( 'HEAD' )[0] ;
	eHead.appendChild( eBase ) ;
}

function SetLinkColor( color )
{
	if ( color && color.length > 0 )
		document.getElementById('eLink').style.color = color ;
	else
		document.getElementById('eLink').style.color = window.document.linkColor ;
}

function SetVisitedColor( color )
{
	if ( color && color.length > 0 )
		document.getElementById('eVisited').style.color = color ;
	else
		document.getElementById('eVisited').style.color = window.document.vlinkColor ;
}

function SetActiveColor( color )
{
	if ( color && color.length > 0 )
		document.getElementById('eActive').style.color = color ;
	else
		document.getElementById('eActive').style.color = window.document.alinkColor ;
}