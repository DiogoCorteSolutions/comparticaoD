var dialog	= window.parent ;
var oEditor	= dialog.InnerDialogLoaded() ;

var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelectedElement() ;

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl && oActiveEl.tagName.toUpperCase() == "INPUT" && ( oActiveEl.type == "button" || oActiveEl.type == "submit" || oActiveEl.type == "reset" ) )
	{
		GetE('txtName').value	= oActiveEl.name ;
		GetE('txtValue').value	= oActiveEl.value ;
		GetE('txtType').value	= oActiveEl.type ;
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

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'INPUT', {name: GetE('txtName').value, type: GetE('txtType').value } ) ;

	SetAttribute( oActiveEl, 'value', GetE('txtValue').value ) ;

	return true ;
}