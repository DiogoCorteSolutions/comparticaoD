var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;

var FCK = oEditor.FCK ;


var oDOM = FCK.EditorDocument ;


var oFakeImage = dialog.Selection.GetSelectedElement() ;
var oActiveEl ;

if ( oFakeImage )
{
	if ( oFakeImage.tagName == 'IMG' && oFakeImage.getAttribute('_fckinputhidden') )
		oActiveEl = FCK.GetRealElement( oFakeImage ) ;
	else
		oFakeImage = null ;
}

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl )
	{
		GetE('txtName').value		= oActiveEl.name ;
		GetE('txtValue').value		= oActiveEl.value ;
	}

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
	SelectField( 'txtName' ) ;
}


function Ok()
{
	oEditor.FCKUndo.SaveUndoStep() ;

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'INPUT', {name: GetE('txtName').value, type: 'hidden' } ) ;

	SetAttribute( oActiveEl, 'value', GetE('txtValue').value ) ;

	if ( !oFakeImage )
	{
		oFakeImage	= oEditor.FCKDocumentProcessor_CreateFakeImage( 'FCK__InputHidden', oActiveEl ) ;
		oFakeImage.setAttribute( '_fckinputhidden', 'true', 0 ) ;

		oActiveEl.parentNode.insertBefore( oFakeImage, oActiveEl ) ;
		oActiveEl.parentNode.removeChild( oActiveEl ) ;
	}
	else
		oEditor.FCKUndo.SaveUndoStep() ;

	return true ;
}