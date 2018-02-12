var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;

var FCKDomTools = oEditor.FCKDomTools ;


var aCells = oEditor.FCKTableHandler.GetSelectedCells() ;

window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage( document ) ;

	SetStartupValue() ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
	SelectField( 'txtWidth' ) ;
}

function SetStartupValue()
{
	if ( aCells.length > 0 )
	{
		var oCell = aCells[0] ;
		var iWidth = GetAttribute( oCell, 'width' ) ;

		if ( iWidth.indexOf && iWidth.indexOf( '%' ) >= 0 )
		{
			iWidth = iWidth.substr( 0, iWidth.length - 1 ) ;
			GetE('selWidthType').value = 'percent' ;
		}

		if ( oCell.attributes['noWrap'] != null && oCell.attributes['noWrap'].specified )
			GetE('selWordWrap').value = !oCell.noWrap ;

		GetE('txtWidth').value			= iWidth ;
		GetE('txtHeight').value			= GetAttribute( oCell, 'height' ) ;
		GetE('selHAlign').value			= GetAttribute( oCell, 'align' ) ;
		GetE('selVAlign').value			= GetAttribute( oCell, 'vAlign' ) ;
		GetE('txtRowSpan').value		= GetAttribute( oCell, 'rowSpan' ) ;
		GetE('txtCollSpan').value		= GetAttribute( oCell, 'colSpan' ) ;
		GetE('txtBackColor').value		= GetAttribute( oCell, 'bgColor' ) ;
		GetE('txtBorderColor').value	= GetAttribute( oCell, 'borderColor' ) ;
		GetE('selCellType').value     = oCell.nodeName.toLowerCase() ;
	}
}


function Ok()
{
	oEditor.FCKUndo.SaveUndoStep() ;

	for( i = 0 ; i < aCells.length ; i++ )
	{
		if ( GetE('txtWidth').value.length > 0 )
			aCells[i].width	= GetE('txtWidth').value + ( GetE('selWidthType').value == 'percent' ? '%' : '') ;
		else
			aCells[i].removeAttribute( 'width', 0 ) ;

		if ( GetE('selWordWrap').value == 'false' )
			SetAttribute( aCells[i], 'noWrap', 'nowrap' ) ;
		else
			aCells[i].removeAttribute( 'noWrap' ) ;

		SetAttribute( aCells[i], 'height'		, GetE('txtHeight').value ) ;
		SetAttribute( aCells[i], 'align'		, GetE('selHAlign').value ) ;
		SetAttribute( aCells[i], 'vAlign'		, GetE('selVAlign').value ) ;
		SetAttribute( aCells[i], 'rowSpan'		, GetE('txtRowSpan').value ) ;
		SetAttribute( aCells[i], 'colSpan'		, GetE('txtCollSpan').value ) ;
		SetAttribute( aCells[i], 'bgColor'		, GetE('txtBackColor').value ) ;
		SetAttribute( aCells[i], 'borderColor'	, GetE('txtBorderColor').value ) ;

		var cellType = GetE('selCellType').value ;
		if ( aCells[i].nodeName.toLowerCase() != cellType )
			aCells[i] = RenameNode( aCells[i], cellType ) ;
	}




	if ( !oEditor.FCKBrowserInfo.IsIE )
	{
		var selection = oEditor.FCK.EditorWindow.getSelection() ;
		selection.removeAllRanges() ;
		for ( var i = 0 ; i < aCells.length ; i++ )
		{
			var range = oEditor.FCK.EditorDocument.createRange() ;
			range.selectNode( aCells[i] ) ;
			selection.addRange( range ) ;
		}
	}

	return true ;
}

function SelectBackColor( color )
{
	if ( color && color.length > 0 )
		GetE('txtBackColor').value = color ;
}

function SelectBorderColor( color )
{
	if ( color && color.length > 0 )
		GetE('txtBorderColor').value = color ;
}

function SelectColor( wich )
{
	oEditor.FCKDialog.OpenDialog( 'FCKDialog_Color', oEditor.FCKLang.DlgColorTitle, 'dialog/fck_colorselector.html', 410, 320, wich == 'Back' ? SelectBackColor : SelectBorderColor, window ) ;
}