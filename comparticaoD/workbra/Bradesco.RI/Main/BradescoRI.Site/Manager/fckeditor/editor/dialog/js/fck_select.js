var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;


var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelectedElement() ;

var oListText ;
var oListValue ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	oListText	= document.getElementById( 'cmbText' ) ;
	oListValue	= document.getElementById( 'cmbValue' ) ;


	oListText.style.width = oListText.offsetWidth ;
	oListValue.style.width = oListValue.offsetWidth ;

	if ( oActiveEl && oActiveEl.tagName == 'SELECT' )
	{
		GetE('txtName').value		= oActiveEl.name ;
		GetE('txtSelValue').value	= oActiveEl.value ;
		GetE('txtLines').value		= GetAttribute( oActiveEl, 'size' ) ;
		GetE('chkMultiple').checked	= oActiveEl.multiple ;


		for ( var i = 0 ; i < oActiveEl.options.length ; i++ )
		{
			var sText	= HTMLDecode( oActiveEl.options[i].innerHTML ) ;
			var sValue	= oActiveEl.options[i].value ;

			AddComboOption( oListText, sText, sText ) ;
			AddComboOption( oListValue, sValue, sValue ) ;
		}
	}
	else
		oActiveEl = null ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
	SelectField( 'txtName' ) ;
}

function Ok()
{
	oEditor.FCKUndo.SaveUndoStep() ;

	var sSize = GetE('txtLines').value ;
	if ( sSize == null || isNaN( sSize ) || sSize <= 1 )
		sSize = '' ;

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'SELECT', {name: GetE('txtName').value} ) ;

	SetAttribute( oActiveEl, 'size'	, sSize ) ;
	oActiveEl.multiple = ( sSize.length > 0 && GetE('chkMultiple').checked ) ;


	while ( oActiveEl.options.length > 0 )
		oActiveEl.remove(0) ;


	for ( var i = 0 ; i < oListText.options.length ; i++ )
	{
		var sText	= oListText.options[i].value ;
		var sValue	= oListValue.options[i].value ;
		if ( sValue.length == 0 ) sValue = sText ;

		var oOption = AddComboOption( oActiveEl, sText, sValue, oDOM ) ;

		if ( sValue == GetE('txtSelValue').value )
		{
			SetAttribute( oOption, 'selected', 'selected' ) ;
			oOption.selected = true ;
		}
	}

	return true ;
}