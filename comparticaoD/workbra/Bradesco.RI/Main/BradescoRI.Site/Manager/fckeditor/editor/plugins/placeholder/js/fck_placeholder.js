var dialog = window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;
var FCKLang = oEditor.FCKLang ;
var FCKPlaceholders = oEditor.FCKPlaceholders ;

window.onload = function ()
{

	oEditor.FCKLanguageManager.TranslatePage( document ) ;

	LoadSelected() ;


	dialog.SetOkButton( true ) ;


	SelectField( 'txtName' ) ;
}

var eSelected = dialog.Selection.GetSelectedElement() ;

function LoadSelected()
{
	if ( !eSelected )
		return ;

	if ( eSelected.tagName == 'SPAN' && eSelected._fckplaceholder )
		document.getElementById('txtName').value = eSelected._fckplaceholder ;
	else
		eSelected == null ;
}

function Ok()
{
	var sValue = document.getElementById('txtName').value ;

	if ( eSelected && eSelected._fckplaceholder == sValue )
		return true ;

	if ( sValue.length == 0 )
	{
		alert( FCKLang.PlaceholderErrNoName ) ;
		return false ;
	}

	if ( FCKPlaceholders.Exist( sValue ) )
	{
		alert( FCKLang.PlaceholderErrNameInUse ) ;
		return false ;
	}

	FCKPlaceholders.Add( sValue ) ;
	return true ;
}