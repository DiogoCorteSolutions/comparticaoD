var oEditor		= window.parent.InnerDialogLoaded() ;
var FCK			= oEditor.FCK ;
var FCKLang		= oEditor.FCKLang ;
var FCKConfig	= oEditor.FCKConfig ;




window.parent.AddTab( 'General'		, FCKLang.DlgDocGeneralTab ) ;
window.parent.AddTab( 'Background'	, FCKLang.DlgDocBackTab ) ;
window.parent.AddTab( 'Colors'		, FCKLang.DlgDocColorsTab ) ;
window.parent.AddTab( 'Meta'		, FCKLang.DlgDocMetaTab ) ;


function OnDialogTabChange( tabCode )
{
	ShowE( 'divGeneral'		, ( tabCode == 'General' ) ) ;
	ShowE( 'divBackground'	, ( tabCode == 'Background' ) ) ;
	ShowE( 'divColors'		, ( tabCode == 'Colors' ) ) ;
	ShowE( 'divMeta'		, ( tabCode == 'Meta' ) ) ;

	ShowE( 'ePreview'		, ( tabCode == 'Background' || tabCode == 'Colors' ) ) ;
}




var oHTML = FCK.EditorDocument.getElementsByTagName('html')[0] ;


var oHead = oHTML.getElementsByTagName('head')[0] ;

var oBody = FCK.EditorDocument.body ;


var oMetaTags = new Object() ;


AppendMetaCollection( oMetaTags, oHead.getElementsByTagName('meta') ) ;
AppendMetaCollection( oMetaTags, oHead.getElementsByTagName('fck:meta') ) ;

function AppendMetaCollection( targetObject, metaCollection )
{

	for ( var i = 0 ; i < metaCollection.length ; i++ )
	{

		var sName = GetAttribute( metaCollection[i], 'name', GetAttribute( metaCollection[i], '___fcktoreplace:name', '' ) ) ;


		if ( sName.length == 0 )
		{
			if ( oEditor.FCKBrowserInfo.IsIE )
			{

				var oHttpEquivMatch = metaCollection[i].outerHTML.match( oEditor.FCKRegexLib.MetaHttpEquiv ) ;
				if ( oHttpEquivMatch )
					sName = oHttpEquivMatch[1] ;
			}
			else
				sName = GetAttribute( metaCollection[i], 'http-equiv', '' ) ;
		}

		if ( sName.length > 0 )
			targetObject[ sName.toLowerCase() ] = metaCollection[i] ;
	}
}




function SetMetadata( name, content, isHttp )
{
	if ( content.length == 0 )
	{
		RemoveMetadata( name ) ;
		return ;
	}

	var oMeta = oMetaTags[ name.toLowerCase() ] ;

	if ( !oMeta )
	{
		oMeta = oHead.appendChild( FCK.EditorDocument.createElement('META') ) ;

		if ( isHttp )
			SetAttribute( oMeta, 'http-equiv', name ) ;
		else
		{



			if ( oEditor.FCKBrowserInfo.IsIE )
				SetAttribute( oMeta, '___fcktoreplace:name', name ) ;
			else
				SetAttribute( oMeta, 'name', name ) ;
		}

		oMetaTags[ name.toLowerCase() ] = oMeta ;
	}

	SetAttribute( oMeta, 'content', content ) ;

}

function RemoveMetadata( name )
{
	var oMeta = oMetaTags[ name.toLowerCase() ] ;

	if ( oMeta && oMeta != null )
	{
		oMeta.parentNode.removeChild( oMeta ) ;
		oMetaTags[ name.toLowerCase() ] = null ;
	}
}

function GetMetadata( name )
{
	var oMeta = oMetaTags[ name.toLowerCase() ] ;

	if ( oMeta && oMeta != null )
		return oMeta.getAttribute( 'content', 2 ) ;
	else
		return '' ;
}

window.onload = function ()
{

	GetE('tdBrowse').style.display = oEditor.FCKConfig.ImageBrowser ? "" : "none";


	oEditor.FCKLanguageManager.TranslatePage( document ) ;

	FillFields() ;

	UpdatePreview() ;


	window.parent.SetOkButton( true ) ;

	window.parent.SetAutoSize( true ) ;
}

function FillFields()
{

	GetE('txtPageTitle').value = FCK.EditorDocument.title ;

	GetE('selDirection').value	= GetAttribute( oHTML, 'dir', '' ) ;
	GetE('txtLang').value		= GetAttribute( oHTML, 'xml:lang', GetAttribute( oHTML, 'lang', '' ) ) ;





		var sCharSet = GetMetadata( 'Content-Type' ) ;

	if ( sCharSet != null && sCharSet.length > 0 )
	{

			sCharSet = sCharSet.match( /[^=]*$/ ) ;

		GetE('selCharSet').value = sCharSet ;

		if ( GetE('selCharSet').selectedIndex == -1 )
		{
			GetE('selCharSet').value = '...' ;
			GetE('txtCustomCharSet').value = sCharSet ;

			CheckOther( GetE('selCharSet'), 'txtCustomCharSet' ) ;
		}
	}


	if ( FCK.DocTypeDeclaration && FCK.DocTypeDeclaration.length > 0 )
	{
		GetE('selDocType').value = FCK.DocTypeDeclaration ;

		if ( GetE('selDocType').selectedIndex == -1 )
		{
			GetE('selDocType').value = '...' ;
			GetE('txtDocType').value = FCK.DocTypeDeclaration ;

			CheckOther( GetE('selDocType'), 'txtDocType' ) ;
		}
	}


	GetE('chkIncXHTMLDecl').checked = ( FCK.XmlDeclaration && FCK.XmlDeclaration.length > 0 ) ;


	GetE('txtBackColor').value = GetAttribute( oBody, 'bgColor'		, '' ) ;
	GetE('txtBackImage').value = GetAttribute( oBody, 'background'	, '' ) ;
	GetE('chkBackNoScroll').checked = ( GetAttribute( oBody, 'bgProperties', '' ).toLowerCase() == 'fixed' ) ;


	GetE('txtColorText').value		= GetAttribute( oBody, 'text'	, '' ) ;
	GetE('txtColorLink').value		= GetAttribute( oBody, 'link'	, '' ) ;
	GetE('txtColorVisited').value	= GetAttribute( oBody, 'vLink'	, '' ) ;
	GetE('txtColorActive').value	= GetAttribute( oBody, 'aLink'	, '' ) ;


	GetE('txtMarginTop').value		= GetAttribute( oBody, 'topMargin'		, '' ) ;
	GetE('txtMarginLeft').value		= GetAttribute( oBody, 'leftMargin'		, '' ) ;
	GetE('txtMarginRight').value	= GetAttribute( oBody, 'rightMargin'	, '' ) ;
	GetE('txtMarginBottom').value	= GetAttribute( oBody, 'bottomMargin'	, '' ) ;


	GetE('txtMetaKeywords').value		= GetMetadata( 'keywords' ) ;
	GetE('txtMetaDescription').value	= GetMetadata( 'description' ) ;
	GetE('txtMetaAuthor').value			= GetMetadata( 'author' ) ;
	GetE('txtMetaCopyright').value		= GetMetadata( 'copyright' ) ;
}


function Ok()
{

	FCK.EditorDocument.title = GetE('txtPageTitle').value ;

	var oHTML = FCK.EditorDocument.getElementsByTagName('html')[0] ;

	SetAttribute( oHTML, 'dir'		, GetE('selDirection').value ) ;
	SetAttribute( oHTML, 'lang'		, GetE('txtLang').value ) ;
	SetAttribute( oHTML, 'xml:lang'	, GetE('txtLang').value ) ;


	var sCharSet = GetE('selCharSet').value ;
	if ( sCharSet == '...' )
		sCharSet = GetE('txtCustomCharSet').value ;

	if ( sCharSet.length > 0 )
			sCharSet = 'text/html; charset=' + sCharSet ;




		SetMetadata( 'Content-Type', sCharSet, true ) ;


	var sDocType = GetE('selDocType').value ;
	if ( sDocType == '...' )
		sDocType = GetE('txtDocType').value ;

	FCK.DocTypeDeclaration = sDocType ;


	if ( GetE('chkIncXHTMLDecl').checked )
	{
		if ( sCharSet.length == 0 )
			sCharSet = 'utf-8' ;

		FCK.XmlDeclaration = '<' + '?xml version="1.0" encoding="' + sCharSet + '"?>' ;

		SetAttribute( oHTML, 'xmlns', 'http:
	}
	else
	{
		FCK.XmlDeclaration = null ;
		oHTML.removeAttribute( 'xmlns', 0 ) ;
	}


	SetAttribute( oBody, 'bgcolor'		, GetE('txtBackColor').value ) ;
	SetAttribute( oBody, 'background'	, GetE('txtBackImage').value ) ;
	SetAttribute( oBody, 'bgproperties'	, GetE('chkBackNoScroll').checked ? 'fixed' : '' ) ;


	SetAttribute( oBody, 'text'	, GetE('txtColorText').value ) ;
	SetAttribute( oBody, 'link'	, GetE('txtColorLink').value ) ;
	SetAttribute( oBody, 'vlink', GetE('txtColorVisited').value ) ;
	SetAttribute( oBody, 'alink', GetE('txtColorActive').value ) ;


	SetAttribute( oBody, 'topmargin'	, GetE('txtMarginTop').value ) ;
	SetAttribute( oBody, 'leftmargin'	, GetE('txtMarginLeft').value ) ;
	SetAttribute( oBody, 'rightmargin'	, GetE('txtMarginRight').value ) ;
	SetAttribute( oBody, 'bottommargin'	, GetE('txtMarginBottom').value ) ;


	SetMetadata( 'keywords'		, GetE('txtMetaKeywords').value ) ;
	SetMetadata( 'description'	, GetE('txtMetaDescription').value ) ;
	SetMetadata( 'author'		, GetE('txtMetaAuthor').value ) ;
	SetMetadata( 'copyright'	, GetE('txtMetaCopyright').value ) ;

	return true ;
}

var bPreviewIsLoaded = false ;
var oPreviewWindow ;
var oPreviewBody ;


function OnPreviewLoad( previewWindow, previewBody )
{
	oPreviewWindow	= previewWindow ;
	oPreviewBody	= previewBody ;

	bPreviewIsLoaded = true ;
	UpdatePreview() ;
}

function UpdatePreview()
{
	if ( !bPreviewIsLoaded )
		return ;


	SetAttribute( oPreviewBody, 'bgcolor'		, GetE('txtBackColor').value ) ;
	SetAttribute( oPreviewBody, 'background'	, GetE('txtBackImage').value ) ;
	SetAttribute( oPreviewBody, 'bgproperties'	, GetE('chkBackNoScroll').checked ? 'fixed' : '' ) ;


	SetAttribute( oPreviewBody, 'text', GetE('txtColorText').value ) ;

	oPreviewWindow.SetLinkColor( GetE('txtColorLink').value ) ;
	oPreviewWindow.SetVisitedColor( GetE('txtColorVisited').value ) ;
	oPreviewWindow.SetActiveColor( GetE('txtColorActive').value ) ;
}

function CheckOther( combo, txtField )
{
	var bNotOther = ( combo.value != '...' ) ;

	GetE(txtField).style.backgroundColor = ( bNotOther ? '#cccccc' : '' ) ;
	GetE(txtField).disabled = bNotOther ;
}

function SetColor( inputId, color )
{
	GetE( inputId ).value = color + '' ;
	UpdatePreview() ;
}

function SelectBackColor( color )		{ SetColor('txtBackColor', color ) ; }
function SelectColorText( color )		{ SetColor('txtColorText', color ) ; }
function SelectColorLink( color )		{ SetColor('txtColorLink', color ) ; }
function SelectColorVisited( color )	{ SetColor('txtColorVisited', color ) ; }
function SelectColorActive( color )		{ SetColor('txtColorActive', color ) ; }

function SelectColor( wich )
{
	switch ( wich )
	{
		case 'Back'			: oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, SelectBackColor, window ) ; return ;
		case 'ColorText'	: oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, SelectColorText, window ) ; return ;
		case 'ColorLink'	: oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, SelectColorLink, window ) ; return ;
		case 'ColorVisited'	: oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, SelectColorVisited, window ) ; return ;
		case 'ColorActive'	: oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, SelectColorActive, window ) ; return ;
	}
}

function BrowseServerBack()
{
	OpenFileBrowser( FCKConfig.ImageBrowserURL, FCKConfig.ImageBrowserWindowWidth, FCKConfig.ImageBrowserWindowHeight ) ;
}

function SetUrl( url )
{
	GetE('txtBackImage').value = url ;
	UpdatePreview() ;
}