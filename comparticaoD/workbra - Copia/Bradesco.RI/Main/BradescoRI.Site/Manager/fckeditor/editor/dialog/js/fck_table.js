var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;

var FCKDomTools = oEditor.FCKDomTools ;


var table ;
var e = dialog.Selection.GetSelectedElement() ;
var hasColumnHeaders ;

if ( ( !e && document.location.search.substr(1) == 'Parent' ) || ( e && e.tagName != 'TABLE' ) )
	e = oEditor.FCKSelection.MoveToAncestorNode( 'TABLE' ) ;

if ( e && e.tagName == "TABLE" )
	table = e ;



window.onload = function()
{

	oEditor.FCKLanguageManager.TranslatePage(document) ;

	if (table)
	{
		document.getElementById('txtRows').value    = table.rows.length ;
		document.getElementById('txtColumns').value = table.rows[0].cells.length ;


		var iWidth  = (table.style.width  ? table.style.width  : table.width ) ;
		var iHeight = (table.style.height ? table.style.height : table.height ) ;

		if (iWidth.indexOf('%') >= 0)
		{
			iWidth = parseInt( iWidth.substr(0,iWidth.length - 1), 10 ) ;
			document.getElementById('selWidthType').value = "percent" ;
		}
		else if (iWidth.indexOf('px') >= 0)
		{
			iWidth = iWidth.substr(0,iWidth.length - 2);
			document.getElementById('selWidthType').value = "pixels" ;
		}

		if (iHeight && iHeight.indexOf('px') >= 0)
			iHeight = iHeight.substr(0,iHeight.length - 2);

		document.getElementById('txtWidth').value		= iWidth || '' ;
		document.getElementById('txtHeight').value		= iHeight || '' ;
		document.getElementById('txtBorder').value		= GetAttribute( table, 'border', '' ) ;
		document.getElementById('selAlignment').value	= GetAttribute( table, 'align', '' ) ;
		document.getElementById('txtCellPadding').value	= GetAttribute( table, 'cellPadding', '' ) ;
		document.getElementById('txtCellSpacing').value	= GetAttribute( table, 'cellSpacing', '' ) ;
		document.getElementById('txtSummary').value     = GetAttribute( table, 'summary', '' ) ;


		var eCaption = oEditor.FCKDomTools.GetFirstChild( table, 'CAPTION' ) ;
		if ( eCaption ) document.getElementById('txtCaption').value = eCaption.innerHTML ;

		hasColumnHeaders = true ;

		for (var row=0; row<table.rows.length; row++)
		{

			if ( table.rows[row].cells[0].nodeName != 'TH' )
			{
				hasColumnHeaders = false ;

				break;
			}
		}


		if ((table.tHead !== null) )
		{
			if (hasColumnHeaders)
				GetE('selHeaders').value = 'both' ;
			else
				GetE('selHeaders').value = 'row' ;
		}
		else
		{
			if (hasColumnHeaders)
				GetE('selHeaders').value = 'col' ;
			else
				GetE('selHeaders').value = '' ;
		}


		document.getElementById('txtRows').disabled    = true ;
		document.getElementById('txtColumns').disabled = true ;
		SelectField( 'txtWidth' ) ;
	}
	else
		SelectField( 'txtRows' ) ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;
}


function Ok()
{
	var bExists = ( table != null ) ;

	var oDoc = oEditor.FCK.EditorDocument ;
	oEditor.FCKUndo.SaveUndoStep() ;

	if ( ! bExists )
		table = oDoc.createElement( "TABLE" ) ;


	if ( bExists && table.style.width )		table.style.width = null ;
	if ( bExists && table.style.height )	table.style.height = null ;

	var sWidth = GetE('txtWidth').value ;
	if ( sWidth.length > 0 && GetE('selWidthType').value == 'percent' )
		sWidth += '%' ;

    SetAttribute( table, 'class'        ,'tbFck');
	SetAttribute( table, 'width'		, sWidth ) ;
	SetAttribute( table, 'height'		, GetE('txtHeight').value ) ;
	SetAttribute( table, 'border'		, GetE('txtBorder').value ) ;
	SetAttribute( table, 'align'		, GetE('selAlignment').value ) ;
	SetAttribute( table, 'cellPadding'	, GetE('txtCellPadding').value ) ;
	SetAttribute( table, 'cellSpacing'	, GetE('txtCellSpacing').value ) ;
	SetAttribute( table, 'summary'		, GetE('txtSummary').value ) ;

	var eCaption = oEditor.FCKDomTools.GetFirstChild( table, 'CAPTION' ) ;

	if ( document.getElementById('txtCaption').value != '')
	{
		if ( !eCaption )
		{
			eCaption = oDoc.createElement( 'CAPTION' ) ;
			table.insertBefore( eCaption, table.firstChild ) ;
		}

		eCaption.innerHTML = document.getElementById('txtCaption').value ;
	}
	else if ( bExists && eCaption )
	{


		if ( oEditor.FCKBrowserInfo.IsIE )
			eCaption.innerHTML = '' ;
		else
			eCaption.parentNode.removeChild( eCaption ) ;
	}

	var headers = GetE('selHeaders').value ;
	if ( bExists )
	{

		if ( table.tHead==null && (headers=='row' || headers=='both') )
		{
			var oThead = table.createTHead() ;
			var tbody = FCKDomTools.GetFirstChild( table, 'TBODY' ) ;
			var theRow= FCKDomTools.GetFirstChild( tbody, 'TR' ) ;


			for (var i = 0; i<theRow.childNodes.length ; i++)
			{
				var th = RenameNode(theRow.childNodes[i], 'TH') ;
				if (th != null)
					th.scope='col' ;
			}
			oThead.appendChild( theRow ) ;
		}

		if ( table.tHead!==null && !(headers=='row' || headers=='both') )
		{

			var tHead = table.tHead ;
			var tbody = FCKDomTools.GetFirstChild( table, 'TBODY' ) ;

			var previousFirstRow = tbody.firstChild ;
			while ( tHead.firstChild )
			{
				var theRow = tHead.firstChild ;
				for (var i = 0; i < theRow.childNodes.length ; i++ )
				{
					var newCell = RenameNode( theRow.childNodes[i], 'TD' ) ;
					if ( newCell != null )
						newCell.removeAttribute( 'scope' ) ;
				}
				tbody.insertBefore( theRow, previousFirstRow ) ;
			}
			table.removeChild( tHead ) ;
		}


		if ( (!hasColumnHeaders)  && (headers=='col' || headers=='both') )
		{
			for( var row=0 ; row < table.rows.length ; row++ )
			{
				var newCell = RenameNode(table.rows[row].cells[0], 'TH') ;
				if ( newCell != null )
					newCell.scope = 'row' ;
			}
		}


		if ( (hasColumnHeaders)  && !(headers=='col' || headers=='both') )
		{
			for( var row=0 ; row < table.rows.length ; row++ )
			{
				var oRow = table.rows[row] ;
				if ( oRow.parentNode.nodeName == 'TBODY' )
				{
					var newCell = RenameNode(oRow.cells[0], 'TD') ;
					if (newCell != null)
						newCell.removeAttribute( 'scope' ) ;
				}
			}
		}
	}

	if (! bExists)
	{
		var iRows = GetE('txtRows').value ;
		var iCols = GetE('txtColumns').value ;

		var startRow = 0 ;

		if (headers=='row' || headers=='both')
		{
			startRow++ ;
			var oThead = table.createTHead() ;
			var oRow = table.insertRow(-1) ;
			oThead.appendChild(oRow);

			for ( var c = 0 ; c < iCols ; c++ )
			{
				var oThcell = oDoc.createElement( 'TH' ) ;
				oThcell.scope = 'col' ;
				oRow.appendChild( oThcell ) ;
				if ( oEditor.FCKBrowserInfo.IsGeckoLike )
					oEditor.FCKTools.AppendBogusBr( oThcell ) ;
			}
		}


		var oTbody = FCKDomTools.GetFirstChild( table, 'TBODY' ) ;
		if ( !oTbody )
		{

			oTbody = oDoc.createElement( 'TBODY' ) ;
			table.appendChild( oTbody ) ;
		}
		for ( var r = startRow ; r < iRows; r++ )
		{
			var oRow = oDoc.createElement( 'TR' ) ;
			oTbody.appendChild(oRow) ;

			var startCol = 0 ;

			if (headers=='col' || headers=='both')
			{
				var oThcell = oDoc.createElement( 'TH' ) ;
				oThcell.scope = 'row' ;
				oRow.appendChild( oThcell ) ;
				if ( oEditor.FCKBrowserInfo.IsGeckoLike )
					oEditor.FCKTools.AppendBogusBr( oThcell ) ;

				startCol++ ;
			}
			for ( var c = startCol ; c < iCols ; c++ )
			{

				var oCell = oDoc.createElement( 'TD' ) ;
				oRow.appendChild( oCell ) ;
				if ( oEditor.FCKBrowserInfo.IsGeckoLike )
					oEditor.FCKTools.AppendBogusBr( oCell ) ;
			}
		}

		oEditor.FCK.InsertElement( table ) ;
	}

	return true ;
}