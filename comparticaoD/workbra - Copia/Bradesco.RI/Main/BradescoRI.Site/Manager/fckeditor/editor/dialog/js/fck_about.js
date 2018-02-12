var oEditor = window.parent.InnerDialogLoaded() ;
var FCKLang	= oEditor.FCKLang ;

window.parent.AddTab( 'About', FCKLang.DlgAboutAboutTab ) ;
window.parent.AddTab( 'License', FCKLang.DlgAboutLicenseTab ) ;
window.parent.AddTab( 'BrowserInfo', FCKLang.DlgAboutBrowserInfoTab ) ;

function OnDialogTabChange( tabCode )
{
	ShowE('divAbout', ( tabCode == 'About' ) ) ;
	ShowE('divLicense', ( tabCode == 'License' ) ) ;
	ShowE('divInfo'	, ( tabCode == 'BrowserInfo' ) ) ;
}

function SendEMail()
{
	var eMail = 'mailto:' ;
	eMail += 'fredck' ;
	eMail += '@' ;
	eMail += 'fckeditor' ;
	eMail += '.' ;
	eMail += 'net' ;

	window.location = eMail ;
}

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	window.parent.SetAutoSize( true ) ;
}