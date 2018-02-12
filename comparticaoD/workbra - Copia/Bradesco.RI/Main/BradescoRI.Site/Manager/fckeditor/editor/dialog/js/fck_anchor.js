var dialog			= window.parent ;
var oEditor			= dialog.InnerDialogLoaded() ;

var FCK				= oEditor.FCK ;
var FCKBrowserInfo	= oEditor.FCKBrowserInfo ;
var FCKTools		= oEditor.FCKTools ;
var FCKRegexLib		= oEditor.FCKRegexLib ;

var oDOM			= FCK.EditorDocument ;

var oFakeImage = dialog.Selection.GetSelectedElement() ;

var oAnchor ;

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

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oAnchor )
		GetE('txtName').value = oAnchor.name ;
	else
		oAnchor = null ;

	window.parent.SetOkButton( true ) ;
	window.parent.SetAutoSize( true ) ;

	SelectField( 'txtName' ) ;
}

function Ok()
{
	var sNewName = GetE('txtName').value ;

	sNewName = sNewName.replace( /[^\w-_\.:]/g, '_' ) ;

	if ( sNewName.length == 0 )
	{
		if ( oAnchor )
		{
			FCK.Commands.GetCommand( 'AnchorDelete' ).Execute() ;
			return true ;
		}

		alert( oEditor.FCKLang.DlgAnchorErrorName ) ;
		return false ;
	}

	oEditor.FCKUndo.SaveUndoStep() ;

	if ( oAnchor )
	{
		ReadjustMenuCircularToAnchor( oAnchor.name, sNewName );

		oAnchor.removeAttribute( 'name' ) ;

		oAnchor.name = sNewName ;

		return true ;
	}

	var aNewAnchors = oEditor.FCK.CreateLink( '#' ) ;

	if ( aNewAnchors.length == 0 )
			aNewAnchors.push( oEditor.FCK.InsertElement( 'a' ) ) ;
	else
	{
		for ( var i = 0 ; i < aNewAnchors.length ; i++ )
			aNewAnchors[i].removeAttribute( 'href' ) ;
	}

	for ( var i = 0 ; i < aNewAnchors.length ; i++ )
	{
		oAnchor = aNewAnchors[i] ;

		if ( FCKBrowserInfo.IsIE )
		{
			var replaceAnchor = oEditor.FCK.EditorDocument.createElement( '<a name="' +
					FCKTools.HTMLEncode( sNewName ).replace( '"', '&quot;' ) + '">' ) ;
			oEditor.FCKDomTools.MoveChildren( oAnchor, replaceAnchor ) ;
			oAnchor.parentNode.replaceChild( replaceAnchor, oAnchor ) ;
			oAnchor = replaceAnchor ;
		}
		else
			oAnchor.name = sNewName ;

		if ( FCKBrowserInfo.IsIE || FCKBrowserInfo.IsOpera )
		{
			if ( oAnchor.innerHTML != '' )
			{
				if ( FCKBrowserInfo.IsIE )
					oAnchor.className += ' FCK__AnchorC' ;
			}
			else
			{
				var oImg = oEditor.FCKDocumentProcessor_CreateFakeImage( 'FCK__Anchor', oAnchor.cloneNode(true) ) ;
				oImg.setAttribute( '_fckanchor', 'true', 0 ) ;

				oAnchor.parentNode.insertBefore( oImg, oAnchor ) ;
				oAnchor.parentNode.removeChild( oAnchor ) ;
			}

		}
	}

	return true ;
}

function ReadjustMenuCircularToAnchor( sCurrent, sNew )
{
	var oDoc = FCK.EditorDocument ;

	var aMenuCircular = oDoc.getElementsByTagName( 'A' ) ;

	var sReference = '#' + sCurrent ;

	var sFullReference = oDoc.location.href.replace( /(#.*$)/, '') ;
	sFullReference += sReference ;

	var oLink ;
	var i = aMenuCircular.length - 1 ;
	while ( i >= 0 && ( oLink = aMenuCircular[i--] ) )
	{
		var sHRef = oLink.getAttribute( '_fcksavedurl' ) ;
		if ( sHRef == null )
			sHRef = oLink.getAttribute( 'href' , 2 ) || '' ;

		if ( sHRef == sReference || sHRef == sFullReference )
		{
			oLink.href = '#' + sNew ;
			SetAttribute( oLink, '_fcksavedurl', '#' + sNew ) ;
		}
	}
}