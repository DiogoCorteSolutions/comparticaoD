var dialog	= window.parent ;
var oEditor	= dialog.InnerDialogLoaded() ;

var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelectedElement() ;

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl && oActiveEl.tagName == 'INPUT' && oActiveEl.type == 'checkbox' )
	{
		GetE('txtName').value		= oActiveEl.name ;
		GetE('txtValue').value		= oEditor.FCKBrowserInfo.IsIE ? oActiveEl.value : GetAttribute( oActiveEl, 'value' ) ;
		GetE('txtSelected').checked	= oActiveEl.checked ;
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

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'INPUT', {name: GetE('txtName').value, type: 'checkbox' } ) ;

	if ( oEditor.FCKBrowserInfo.IsIE )
		oActiveEl.value = GetE('txtValue').value ;
	else
		SetAttribute( oActiveEl, 'value', GetE('txtValue').value ) ;

	var bIsChecked = GetE('txtSelected').checked ;
	SetAttribute( oActiveEl, 'checked', bIsChecked ? 'checked' : null ) ;
	oActiveEl.checked = bIsChecked ;

	return true ;
}