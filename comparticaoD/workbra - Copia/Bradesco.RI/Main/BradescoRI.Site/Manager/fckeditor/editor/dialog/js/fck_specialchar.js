var oEditor = window.parent.InnerDialogLoaded() ;

var oSample ;

function insertChar(charValue)
{
	oEditor.FCKUndo.SaveUndoStep() ;
	oEditor.FCK.InsertHtml( charValue || "" ) ;
	window.parent.Cancel() ;
}

function over(td)
{
	if ( ! oSample )
		return ;
	oSample.innerHTML = td.innerHTML ;
	td.className = 'LightBackground SpecialCharsOver Hand' ;
}

function out(td)
{
	if ( ! oSample )
		return ;
	oSample.innerHTML = "&nbsp;" ;
	td.className = 'DarkBackground SpecialCharsOut Hand' ;
}

function setDefaults()
{

	oSample = document.getElementById("SampleTD") ;


	oEditor.FCKLanguageManager.TranslatePage(document) ;

	window.parent.SetAutoSize( true ) ;
}