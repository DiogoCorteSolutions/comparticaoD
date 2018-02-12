var oEditor = window.parent.InnerDialogLoaded() ;

function OnLoad()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	CreateColorTable() ;

	window.parent.SetOkButton( true ) ;
	window.parent.SetAutoSize( true ) ;
}

function CreateColorTable()
{
	var oTable = document.getElementById('ColorTable') ;

	var aColors = ['00','33','66','99','cc','ff'] ;

	function AppendColorRow( rangeA, rangeB )
	{
		for ( var i = rangeA ; i < rangeA + 3 ; i++ )
		{
			var oRow = oTable.insertRow(-1) ;

			for ( var j = rangeB ; j < rangeB + 3 ; j++ )
			{
				for ( var n = 0 ; n < 6 ; n++ )
				{
					AppendColorCell( oRow, '#' + aColors[j] + aColors[n] + aColors[i] ) ;
				}
			}
		}
	}

	function AppendColorCell( targetRow, color )
	{
		var oCell = targetRow.insertCell(-1) ;
		oCell.className = 'ColorCell' ;
		oCell.bgColor = color ;

		oCell.onmouseover = function()
		{
			document.getElementById('hicolor').style.backgroundColor = this.bgColor ;
			document.getElementById('hicolortext').innerHTML = this.bgColor ;
		}

		oCell.onclick = function()
		{
			document.getElementById('selhicolor').style.backgroundColor = this.bgColor ;
			document.getElementById('selcolor').value = this.bgColor ;
		}
	}

	AppendColorRow( 0, 0 ) ;
	AppendColorRow( 3, 0 ) ;
	AppendColorRow( 0, 3 ) ;
	AppendColorRow( 3, 3 ) ;

	var oRow = oTable.insertRow(-1) ;

	for ( var n = 0 ; n < 6 ; n++ )
	{
		AppendColorCell( oRow, '#' + aColors[n] + aColors[n] + aColors[n] ) ;
	}

	for ( var i = 0 ; i < 12 ; i++ )
	{
		AppendColorCell( oRow, '#000000' ) ;
	}
}

function Clear()
{
	document.getElementById('selhicolor').style.backgroundColor = '' ;
	document.getElementById('selcolor').value = '' ;
}

function ClearActual()
{
	document.getElementById('hicolor').style.backgroundColor = '' ;
	document.getElementById('hicolortext').innerHTML = '&nbsp;' ;
}

function UpdateColor()
{
	try		  { document.getElementById('selhicolor').style.backgroundColor = document.getElementById('selcolor').value ; }
	catch (e) { Clear() ; }
}

function Ok()
{
	if ( typeof(window.parent.Args().CustomValue) == 'function' )
		window.parent.Args().CustomValue( document.getElementById('selcolor').value ) ;

	return true ;
}