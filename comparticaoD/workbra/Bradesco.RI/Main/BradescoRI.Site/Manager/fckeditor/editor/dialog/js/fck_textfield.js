var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;


var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelectedElement() ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl && oActiveEl.tagName == 'INPUT' && ( oActiveEl.type == 'text' || oActiveEl.type == 'password' ) )
	{
		GetE('txtName').value	= oActiveEl.name ;
		GetE('txtValue').value	= oActiveEl.value ;
		GetE('txtSize').value	= GetAttribute( oActiveEl, 'size' ) ;
		GetE('txtMax').value	= GetAttribute( oActiveEl, 'maxLength' ) ;
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
	if ( isNaN( GetE('txtMax').value ) || GetE('txtMax').value < 0 )
	{
		alert( "Maximum characters must be a positive number." ) ;
		GetE('txtMax').focus() ;
		return false ;
	}
	else if( isNaN( GetE('txtSize').value ) || GetE('txtSize').value < 0 )
	{
		alert( "Width must be a positive number." ) ;
		GetE('txtSize').focus() ;
		return false ;
	}

	oEditor.FCKUndo.SaveUndoStep() ;

	oActiveEl = CreateNamedElement( oEditor, oActiveEl, 'INPUT', {name: GetE('txtName').value, type: GetE('txtType').value } ) ;

	SetAttribute( oActiveEl, 'value'	, GetE('txtValue').value ) ;
	SetAttribute( oActiveEl, 'size'		, GetE('txtSize').value ) ;
	SetAttribute( oActiveEl, 'maxlength', GetE('txtMax').value ) ;

	return true ;
}