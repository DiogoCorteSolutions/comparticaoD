var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;


var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelectedElement() ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl && oActiveEl.tagName == 'TEXTAREA' )
	{
		GetE('txtName').value		= oActiveEl.name ;
		GetE('txtCols').value		= GetAttribute( oActiveEl, 'cols' ) ;
		GetE('txtRows').value		= GetAttribute( oActiveEl, 'rows' ) ;
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

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'TEXTAREA', {name: GetE('txtName').value} ) ;

	SetAttribute( oActiveEl, 'cols', GetE('txtCols').value ) ;
	SetAttribute( oActiveEl, 'rows', GetE('txtRows').value ) ;

	return true ;
}