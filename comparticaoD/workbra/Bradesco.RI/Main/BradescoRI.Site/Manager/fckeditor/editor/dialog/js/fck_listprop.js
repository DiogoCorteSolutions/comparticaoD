var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;


var oDOM = oEditor.FCK.EditorDocument ;
var sListType = ( location.search == '?OL' ? 'OL' : 'UL' ) ;

var oActiveEl = dialog.Selection.GetSelection().MoveToAncestorNode( sListType ) ;
var oActiveSel ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if ( sListType == 'UL' )
		oActiveSel = GetE('selBulleted') ;
	else
	{
		if ( oActiveEl )
		{
			oActiveSel = GetE('selNumbered') ;
			GetE('eStart').style.display = '' ;
			GetE('txtStartPosition').value	= GetAttribute( oActiveEl, 'start' ) ;
		}
	}

	oActiveSel.style.display = '' ;

	if ( oActiveEl )
	{
		if ( oActiveEl.getAttribute('type') )
			oActiveSel.value = oActiveEl.getAttribute('type') ;
	}

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;

	SelectField( sListType == 'OL' ? 'txtStartPosition' : 'selBulleted' ) ;
}

function Ok()
{
	if ( oActiveEl ){
		SetAttribute( oActiveEl, 'type'	, oActiveSel.value ) ;
		if(oActiveEl.tagName == 'OL')
			SetAttribute( oActiveEl, 'start', GetE('txtStartPosition').value ) ;
	}

	return true ;
}