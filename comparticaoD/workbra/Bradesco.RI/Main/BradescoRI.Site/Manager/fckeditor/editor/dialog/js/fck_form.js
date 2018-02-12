var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;


var oDOM = oEditor.FCK.EditorDocument ;

var oActiveEl = dialog.Selection.GetSelection().MoveToAncestorNode( 'FORM' ) ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( oActiveEl )
	{
		GetE('txtName').value	= oActiveEl.name ;
		GetE('txtAction').value	= oActiveEl.getAttribute( 'action', 2 ) ;
		GetE('txtMethod').value	= oActiveEl.method ;
	}
	else
		oActiveEl = null ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
	SelectField( 'txtName' ) ;
}

function Ok()
{
	if ( !oActiveEl )
	{
		oActiveEl = oEditor.FCK.InsertElement( 'form' ) ;

		if ( oEditor.FCKBrowserInfo.IsGeckoLike )
			oEditor.FCKTools.AppendBogusBr( oActiveEl ) ;
	}

	oActiveEl.name = GetE('txtName').value ;
	SetAttribute( oActiveEl, 'action', GetE('txtAction').value ) ;
	oActiveEl.method = GetE('txtMethod').value ;

	return true ;
}