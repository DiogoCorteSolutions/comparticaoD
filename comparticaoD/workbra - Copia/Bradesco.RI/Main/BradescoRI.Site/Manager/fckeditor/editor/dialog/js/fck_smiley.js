var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;

window.onload = function ()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	dialog.SetAutoSize( true ) ;
}

function InsertSmiley( url )
{
	oEditor.FCKUndo.SaveUndoStep() ;

	var oImg = oEditor.FCK.InsertElement( 'img' ) ;
	oImg.src = url ;
	oImg.setAttribute( '_fcksavedurl', url ) ;




	document.body.innerHTML = '' ;

	dialog.Cancel() ;
}

function over(td)
{
	td.className = 'LightBackground Hand' ;
}

function out(td)
{
	td.className = 'DarkBackground Hand' ;
}