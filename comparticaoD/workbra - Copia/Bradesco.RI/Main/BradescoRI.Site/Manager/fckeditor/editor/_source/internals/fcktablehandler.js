var FCKTableHandler = new Object() ;

FCKTableHandler.InsertRow = function( insertBefore )
{

	var oRow = FCKSelection.MoveToAncestorNode( 'TR' ) ;
	if ( !oRow ) return ;


	var oNewRow = oRow.cloneNode( true ) ;


	oRow.parentNode.insertBefore( oNewRow, oRow ) ;


	FCKTableHandler.ClearRow( insertBefore ? oNewRow : oRow ) ;
}

FCKTableHandler.DeleteRows = function( row )
{




	if ( ! row )
	{
		var aCells = FCKTableHandler.GetSelectedCells() ;
		var aRowsToDelete = new Array() ;

		for ( var i = 0; i < aCells.length; i++ )
		{
			var oRow = aCells[i].parentNode ;
			aRowsToDelete[oRow.rowIndex] = oRow ;
		}
		for ( var i = aRowsToDelete.length; i >= 0; i-- )
		{
			if ( aRowsToDelete[i] )
				FCKTableHandler.DeleteRows( aRowsToDelete[i] );
		}
		return ;
	}


	var oTable = FCKTools.GetElementAscensor( row, 'TABLE' ) ;


	if ( oTable.rows.length == 1 )
	{
		FCKTableHandler.DeleteTable( oTable ) ;
		return ;
	}


	row.parentNode.removeChild( row ) ;
}

FCKTableHandler.DeleteTable = function( table )
{


	if ( !table )
	{
		table = FCKSelection.GetSelectedElement() ;
		if ( !table || table.tagName != 'TABLE' )
			table = FCKSelection.MoveToAncestorNode( 'TABLE' ) ;
	}
	if ( !table ) return ;


	FCKSelection.SelectNode( table ) ;
	FCKSelection.Collapse();



	if ( table.parentNode.childNodes.length == 1 )
		table.parentNode.parentNode.removeChild( table.parentNode );
	else
		table.parentNode.removeChild( table  ) ;
}

FCKTableHandler.InsertColumn = function( insertBefore )
{

	var oCell = null ;
	var nodes = this.GetSelectedCells() ;

	if ( nodes && nodes.length )
		oCell = nodes[ insertBefore ? 0 : ( nodes.length - 1 ) ] ;

	if ( ! oCell )
		return ;


	var oTable = FCKTools.GetElementAscensor( oCell, 'TABLE' ) ;

	var iIndex = oCell.cellIndex ;


	for ( var i = 0 ; i < oTable.rows.length ; i++ )
	{

		var oRow = oTable.rows[i] ;


		if ( oRow.cells.length < ( iIndex + 1 ) )
			continue ;

		oCell = oRow.cells[iIndex].cloneNode(false) ;

		if ( FCKBrowserInfo.IsGeckoLike )
			FCKTools.AppendBogusBr( oCell ) ;


		var oBaseCell = oRow.cells[iIndex] ;

		oRow.insertBefore( oCell, ( insertBefore ? oBaseCell : oBaseCell.nextSibling ) ) ;
	}
}

FCKTableHandler.DeleteColumns = function( oCell )
{


	if ( !oCell  )
	{
		var aColsToDelete = FCKTableHandler.GetSelectedCells();
		for ( var i = aColsToDelete.length; i >= 0; i--  )
		{
			if ( aColsToDelete[i]  )
				FCKTableHandler.DeleteColumns( aColsToDelete[i]  );
		}
		return;
	}

	if ( !oCell ) return ;


	var oTable = FCKTools.GetElementAscensor( oCell, 'TABLE' ) ;


	var iIndex = oCell.cellIndex ;



	for ( var i = oTable.rows.length - 1 ; i >= 0 ; i-- )
	{

		var oRow = oTable.rows[i] ;


		if ( iIndex == 0 && oRow.cells.length == 1 )
		{

			FCKTableHandler.DeleteRows( oRow ) ;
			continue ;
		}


		if ( oRow.cells[iIndex] )
			oRow.removeChild( oRow.cells[iIndex] ) ;
	}
}

FCKTableHandler.InsertCell = function( cell, insertBefore )
{

	var oCell = null ;
	var nodes = this.GetSelectedCells() ;
	if ( nodes && nodes.length )
		oCell = nodes[ insertBefore ? 0 : ( nodes.length - 1 ) ] ;
	if ( ! oCell )
		return null ;


	var oNewCell = FCK.EditorDocument.createElement( 'TD' ) ;
	if ( FCKBrowserInfo.IsGeckoLike )
		FCKTools.AppendBogusBr( oNewCell ) ;

	if ( !insertBefore && oCell.cellIndex == oCell.parentNode.cells.length - 1 )
		oCell.parentNode.appendChild( oNewCell ) ;
	else
		oCell.parentNode.insertBefore( oNewCell, insertBefore ? oCell : oCell.nextSibling ) ;

	return oNewCell ;
}

FCKTableHandler.DeleteCell = function( cell )
{

	if ( cell.parentNode.cells.length == 1 )
	{

		FCKTableHandler.DeleteRows( cell.parentNode ) ;
		return ;
	}


	cell.parentNode.removeChild( cell ) ;
}

FCKTableHandler.DeleteCells = function()
{
	var aCells = FCKTableHandler.GetSelectedCells() ;

	for ( var i = aCells.length - 1 ; i >= 0  ; i-- )
	{
		FCKTableHandler.DeleteCell( aCells[i] ) ;
	}
}

FCKTableHandler._MarkCells = function( cells, label )
{
	for ( var i = 0 ; i < cells.length ; i++ )
		cells[i][label] = true ;
}

FCKTableHandler._UnmarkCells = function( cells, label )
{
	for ( var i = 0 ; i < cells.length ; i++ )
	{
		FCKDomTools.ClearElementJSProperty(cells[i], label ) ;
	}
}

FCKTableHandler._ReplaceCellsByMarker = function( tableMap, marker, substitute )
{
	for ( var i = 0 ; i < tableMap.length ; i++ )
	{
		for ( var j = 0 ; j < tableMap[i].length ; j++ )
		{
			if ( tableMap[i][j][marker] )
				tableMap[i][j] = substitute ;
		}
	}
}

FCKTableHandler._GetMarkerGeometry = function( tableMap, rowIdx, colIdx, markerName )
{
	var selectionWidth = 0 ;
	var selectionHeight = 0 ;
	var cellsLeft = 0 ;
	var cellsUp = 0 ;
	for ( var i = colIdx ; tableMap[rowIdx][i] && tableMap[rowIdx][i][markerName] ; i++ )
		selectionWidth++ ;
	for ( var i = colIdx - 1 ; tableMap[rowIdx][i] && tableMap[rowIdx][i][markerName] ; i-- )
	{
		selectionWidth++ ;
		cellsLeft++ ;
	}
	for ( var i = rowIdx ; tableMap[i] && tableMap[i][colIdx] && tableMap[i][colIdx][markerName] ; i++ )
		selectionHeight++ ;
	for ( var i = rowIdx - 1 ; tableMap[i] && tableMap[i][colIdx] && tableMap[i][colIdx][markerName] ; i-- )
	{
		selectionHeight++ ;
		cellsUp++ ;
	}
	return { 'width' : selectionWidth, 'height' : selectionHeight, 'x' : cellsLeft, 'y' : cellsUp } ;
}

FCKTableHandler.CheckIsSelectionRectangular = function()
{


	var cells = FCKTableHandler.GetSelectedCells() ;
	if ( cells.length < 1 )
		return false ;


	for (var i = 0; i < cells.length; i++)
	{
		if ( cells[i].parentNode.parentNode != cells[0].parentNode.parentNode )
			return false ;
	}

	this._MarkCells( cells, '_CellSelected' ) ;

	var tableMap = this._CreateTableMap( cells[0] ) ;
	var rowIdx = cells[0].parentNode.rowIndex ;
	var colIdx = this._GetCellIndexSpan( tableMap, rowIdx, cells[0] ) ;

	var geometry = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_CellSelected' ) ;
	var baseColIdx = colIdx - geometry.x ;
	var baseRowIdx = rowIdx - geometry.y ;

	if ( geometry.width >= geometry.height )
	{
		for ( colIdx = baseColIdx ; colIdx < baseColIdx + geometry.width ; colIdx++ )
		{
			rowIdx = baseRowIdx + ( colIdx - baseColIdx ) % geometry.height ;
			if ( ! tableMap[rowIdx] || ! tableMap[rowIdx][colIdx] )
			{
				this._UnmarkCells( cells, '_CellSelected' ) ;
				return false ;
			}
			var g = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_CellSelected' ) ;
			if ( g.width != geometry.width || g.height != geometry.height )
			{
				this._UnmarkCells( cells, '_CellSelected' ) ;
				return false ;
			}
		}
	}
	else
	{
		for ( rowIdx = baseRowIdx ; rowIdx < baseRowIdx + geometry.height ; rowIdx++ )
		{
			colIdx = baseColIdx + ( rowIdx - baseRowIdx ) % geometry.width ;
			if ( ! tableMap[rowIdx] || ! tableMap[rowIdx][colIdx] )
			{
				this._UnmarkCells( cells, '_CellSelected' ) ;
				return false ;
			}
			var g = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_CellSelected' ) ;
			if ( g.width != geometry.width || g.height != geometry.height )
			{
				this._UnmarkCells( cells, '_CellSelected' ) ;
				return false ;
			}
		}
	}

	this._UnmarkCells( cells, '_CellSelected' ) ;
	return true ;
}

FCKTableHandler.MergeCells = function()
{

	var cells = this.GetSelectedCells() ;
	if ( cells.length < 2 )
		return ;



	var refCell = cells[0] ;
	var tableMap = this._CreateTableMap( refCell ) ;
	var rowIdx = refCell.parentNode.rowIndex ;
	var colIdx = this._GetCellIndexSpan( tableMap, rowIdx, refCell ) ;

	this._MarkCells( cells, '_SelectedCells' ) ;
	var selectionGeometry = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_SelectedCells' ) ;

	var baseColIdx = colIdx - selectionGeometry.x ;
	var baseRowIdx = rowIdx - selectionGeometry.y ;
	var cellContents = FCKTools.GetElementDocument( refCell ).createDocumentFragment() ;
	for ( var i = 0 ; i < selectionGeometry.height ; i++ )
	{
		var rowChildNodesCount = 0 ;
		for ( var j = 0 ; j < selectionGeometry.width ; j++ )
		{
			var currentCell = tableMap[baseRowIdx + i][baseColIdx + j] ;
			while ( currentCell.childNodes.length > 0 )
			{
				var node = currentCell.removeChild( currentCell.firstChild ) ;
				if ( node.nodeType != 1
					|| ( node.getAttribute( 'type', 2 ) != '_moz' && node.getAttribute( '_moz_dirty' ) != null ) )
				{
					cellContents.appendChild( node ) ;
					rowChildNodesCount++ ;
				}
			}
		}
		if ( rowChildNodesCount > 0 )
			cellContents.appendChild( FCK.EditorDocument.createElement( 'br' ) ) ;
	}

	this._ReplaceCellsByMarker( tableMap, '_SelectedCells', refCell ) ;
	this._UnmarkCells( cells, '_SelectedCells' ) ;
	this._InstallTableMap( tableMap, refCell.parentNode.parentNode.parentNode ) ;
	refCell.appendChild( cellContents ) ;

	if ( FCKBrowserInfo.IsGeckoLike && ( ! refCell.firstChild ) )
		FCKTools.AppendBogusBr( refCell ) ;

	this._MoveCaretToCell( refCell, false ) ;
}

FCKTableHandler.MergeRight = function()
{
	var target = this.GetMergeRightTarget() ;
	if ( target == null )
		return ;
	var refCell = target.refCell ;
	var tableMap = target.tableMap ;
	var nextCell = target.nextCell ;

	var cellContents = FCK.EditorDocument.createDocumentFragment() ;
	while ( nextCell && nextCell.childNodes && nextCell.childNodes.length > 0 )
		cellContents.appendChild( nextCell.removeChild( nextCell.firstChild ) ) ;

	nextCell.parentNode.removeChild( nextCell ) ;
	refCell.appendChild( cellContents ) ;
	this._MarkCells( [nextCell], '_Replace' ) ;
	this._ReplaceCellsByMarker( tableMap, '_Replace', refCell ) ;
	this._InstallTableMap( tableMap, refCell.parentNode.parentNode.parentNode ) ;

	this._MoveCaretToCell( refCell, false ) ;
}

FCKTableHandler.MergeDown = function()
{
	var target = this.GetMergeDownTarget() ;
	if ( target == null )
		return ;
	var refCell = target.refCell ;
	var tableMap = target.tableMap ;
	var nextCell = target.nextCell ;

	var cellContents = FCKTools.GetElementDocument( refCell ).createDocumentFragment() ;
	while ( nextCell && nextCell.childNodes && nextCell.childNodes.length > 0 )
		cellContents.appendChild( nextCell.removeChild( nextCell.firstChild ) ) ;
	if ( cellContents.firstChild )
		cellContents.insertBefore( FCK.EditorDocument.createElement( 'br' ), cellContents.firstChild ) ;
	refCell.appendChild( cellContents ) ;
	this._MarkCells( [nextCell], '_Replace' ) ;
	this._ReplaceCellsByMarker( tableMap, '_Replace', refCell ) ;
	this._InstallTableMap( tableMap, refCell.parentNode.parentNode.parentNode ) ;

	this._MoveCaretToCell( refCell, false ) ;
}

FCKTableHandler.HorizontalSplitCell = function()
{
	var cells = FCKTableHandler.GetSelectedCells() ;
	if ( cells.length != 1 )
		return ;

	var refCell = cells[0] ;
	var tableMap = this._CreateTableMap( refCell ) ;
	var rowIdx = refCell.parentNode.rowIndex ;
	var colIdx = FCKTableHandler._GetCellIndexSpan( tableMap, rowIdx, refCell ) ;
	var cellSpan = isNaN( refCell.colSpan ) ? 1 : refCell.colSpan ;

	if ( cellSpan > 1 )
	{


		var newCellSpan = Math.ceil( cellSpan / 2 ) ;
		var newCell = FCK.EditorDocument.createElement( refCell.nodeName ) ;
		if ( FCKBrowserInfo.IsGeckoLike )
			FCKTools.AppendBogusBr( newCell ) ;
		var startIdx = colIdx + newCellSpan ;
		var endIdx = colIdx + cellSpan ;
		var rowSpan = isNaN( refCell.rowSpan ) ? 1 : refCell.rowSpan ;
		for ( var r = rowIdx ; r < rowIdx + rowSpan ; r++ )
		{
			for ( var i = startIdx ; i < endIdx ; i++ )
				tableMap[r][i] = newCell ;
		}
	}
	else
	{


		var newTableMap = [] ;
		for ( var i = 0 ; i < tableMap.length ; i++ )
		{
			var newRow = tableMap[i].slice( 0, colIdx ) ;
			if ( tableMap[i].length <= colIdx )
			{
				newTableMap.push( newRow ) ;
				continue ;
			}
			if ( tableMap[i][colIdx] == refCell )
			{
				newRow.push( refCell ) ;
				newRow.push( FCK.EditorDocument.createElement( refCell.nodeName ) ) ;
				if ( FCKBrowserInfo.IsGeckoLike )
					FCKTools.AppendBogusBr( newRow[newRow.length - 1] ) ;
			}
			else
			{
				newRow.push( tableMap[i][colIdx] ) ;
				newRow.push( tableMap[i][colIdx] ) ;
			}
			for ( var j = colIdx + 1 ; j < tableMap[i].length ; j++ )
				newRow.push( tableMap[i][j] ) ;
			newTableMap.push( newRow ) ;
		}
		tableMap = newTableMap ;
	}

	this._InstallTableMap( tableMap, refCell.parentNode.parentNode.parentNode ) ;
}

FCKTableHandler.VerticalSplitCell = function()
{
	var cells = FCKTableHandler.GetSelectedCells() ;
	if ( cells.length != 1 )
		return ;

	var currentCell = cells[0] ;
	var tableMap = this._CreateTableMap( currentCell ) ;
	var currentRowIndex = currentCell.parentNode.rowIndex ;
	var cellIndex = FCKTableHandler._GetCellIndexSpan( tableMap, currentRowIndex, currentCell ) ;

	var currentColSpan = isNaN( currentCell.colSpan ) ? 1 : currentCell.colSpan ;
	var currentRowSpan = currentCell.rowSpan ;
	if ( isNaN( currentRowSpan ) )
		currentRowSpan = 1 ;

	if ( currentRowSpan > 1 )
	{

		currentCell.rowSpan = Math.ceil( currentRowSpan / 2 ) ;


		var newCellRowIndex = currentRowIndex + Math.ceil( currentRowSpan / 2 ) ;
		var oRow = tableMap[newCellRowIndex] ;
		var insertMarker = null ;
		for ( var i = cellIndex+1 ; i < oRow.length ; i++ )
		{
			if ( oRow[i].parentNode.rowIndex == newCellRowIndex )
			{
				insertMarker = oRow[i] ;
				break ;
			}
		}


		var newCell = FCK.EditorDocument.createElement( currentCell.nodeName ) ;
		newCell.rowSpan = Math.floor( currentRowSpan / 2 ) ;
		if ( currentColSpan > 1 )
			newCell.colSpan = currentColSpan ;
		if ( FCKBrowserInfo.IsGeckoLike )
			FCKTools.AppendBogusBr( newCell ) ;
		currentCell.parentNode.parentNode.parentNode.rows[newCellRowIndex].insertBefore( newCell, insertMarker ) ;
	}
	else
	{

		var newSectionRowIdx = currentCell.parentNode.sectionRowIndex + 1 ;
		var newRow = FCK.EditorDocument.createElement( 'tr' ) ;
		var tSection = currentCell.parentNode.parentNode ;
		if ( tSection.rows.length > newSectionRowIdx )
			tSection.insertBefore( newRow, tSection.rows[newSectionRowIdx] ) ;
		else
			tSection.appendChild( newRow ) ;


		for ( var i = 0 ; i < tableMap[currentRowIndex].length ; )
		{
			var colSpan = tableMap[currentRowIndex][i].colSpan ;
			if ( isNaN( colSpan ) || colSpan < 1 )
				colSpan = 1 ;
			if ( i == cellIndex )
			{
				i += colSpan ;
				continue ;
			}
			var rowSpan = tableMap[currentRowIndex][i].rowSpan ;
			if ( isNaN( rowSpan ) )
				rowSpan = 1 ;
			tableMap[currentRowIndex][i].rowSpan = rowSpan + 1 ;
			i += colSpan ;
		}


		var newCell = FCK.EditorDocument.createElement( currentCell.nodeName ) ;
		if ( currentColSpan > 1 )
			newCell.colSpan = currentColSpan ;
		if ( FCKBrowserInfo.IsGeckoLike )
			FCKTools.AppendBogusBr( newCell	) ;
		newRow.appendChild( newCell ) ;
	}
}


FCKTableHandler._GetCellIndexSpan = function( tableMap, rowIndex, cell )
{
	if ( tableMap.length < rowIndex + 1 )
		return null ;

	var oRow = tableMap[ rowIndex ] ;

	for ( var c = 0 ; c < oRow.length ; c++ )
	{
		if ( oRow[c] == cell )
			return c ;
	}

	return null ;
}


FCKTableHandler._GetCellLocation = function( tableMap, cell  )
{
	for ( var i = 0 ; i < tableMap.length; i++ )
	{
		for ( var c = 0 ; c < tableMap[i].length ; c++  )
		{
			if ( tableMap[i][c] == cell  ) return [i,c];
		}
	}
	return null ;
}

FCKTableHandler._CreateTableMap = function( refCell )
{
	var table = (refCell.nodeName == 'TABLE' ? refCell : refCell.parentNode.parentNode.parentNode ) ;

	var aRows = table.rows ;


	var r = -1 ;

	var aMap = new Array() ;

	for ( var i = 0 ; i < aRows.length ; i++ )
	{
		r++ ;
		if ( !aMap[r] )
			aMap[r] = new Array() ;

		var c = -1 ;

		for ( var j = 0 ; j < aRows[i].cells.length ; j++ )
		{
			var oCell = aRows[i].cells[j] ;

			c++ ;
			while ( aMap[r][c] )
				c++ ;

			var iColSpan = isNaN( oCell.colSpan ) ? 1 : oCell.colSpan ;
			var iRowSpan = isNaN( oCell.rowSpan ) ? 1 : oCell.rowSpan ;

			for ( var rs = 0 ; rs < iRowSpan ; rs++ )
			{
				if ( !aMap[r + rs] )
					aMap[r + rs] = new Array() ;

				for ( var cs = 0 ; cs < iColSpan ; cs++ )
				{
					aMap[r + rs][c + cs] = aRows[i].cells[j] ;
				}
			}

			c += iColSpan - 1 ;
		}
	}
	return aMap ;
}


FCKTableHandler._InstallTableMap = function( tableMap, table )
{



	var rowSpanAttr = FCKBrowserInfo.IsIE ? "_fckrowspan" : "rowSpan" ;


	for ( var i = 0 ; i < tableMap.length ; i++ )
	{
		for ( var j = 0 ; j < tableMap[i].length ; j++ )
		{
			var cell = tableMap[i][j] ;
			if ( cell.parentNode )
				cell.parentNode.removeChild( cell ) ;
			cell.colSpan = cell[rowSpanAttr] = 1 ;
		}
	}


	var maxCol = 0 ;
	for ( var i = 0 ; i < tableMap.length ; i++ )
	{
		for ( var j = 0 ; j < tableMap[i].length ; j++ )
		{
			var cell = tableMap[i][j] ;
			if ( ! cell)
				continue ;
			if ( j > maxCol )
				maxCol = j ;
			if ( cell._colScanned === true )
				continue ;
			if ( tableMap[i][j-1] == cell )
				cell.colSpan++ ;
			if ( tableMap[i][j+1] != cell )
				cell._colScanned = true ;
		}
	}


	for ( var i = 0 ; i <= maxCol ; i++ )
	{
		for ( var j = 0 ; j < tableMap.length ; j++ )
		{
			if ( ! tableMap[j] )
				continue ;
			var cell = tableMap[j][i] ;
			if ( ! cell || cell._rowScanned === true )
				continue ;
			if ( tableMap[j-1] && tableMap[j-1][i] == cell )
				cell[rowSpanAttr]++ ;
			if ( ! tableMap[j+1] || tableMap[j+1][i] != cell )
				cell._rowScanned = true ;
		}
	}


	for ( var i = 0 ; i < tableMap.length ; i++ )
	{
		for ( var j = 0 ; j < tableMap[i].length ; j++)
		{
			var cell = tableMap[i][j] ;
			FCKDomTools.ClearElementJSProperty(cell, '_colScanned' ) ;
			FCKDomTools.ClearElementJSProperty(cell, '_rowScanned' ) ;
		}
	}


	for ( var i = 0 ; i < tableMap.length ; i++ )
	{
		var rowObj = FCK.EditorDocument.createElement( 'tr' ) ;
		for ( var j = 0 ; j < tableMap[i].length ; )
		{
			var cell = tableMap[i][j] ;
			if ( tableMap[i-1] && tableMap[i-1][j] == cell )
			{
				j += cell.colSpan ;
				continue ;
			}
			rowObj.appendChild( cell ) ;
			if ( rowSpanAttr != 'rowSpan' )
			{
				cell.rowSpan = cell[rowSpanAttr] ;
				cell.removeAttribute( rowSpanAttr ) ;
			}
			j += cell.colSpan ;
			if ( cell.colSpan == 1 )
				cell.removeAttribute( 'colspan' ) ;
			if ( cell.rowSpan == 1 )
				cell.removeAttribute( 'rowspan' ) ;
		}
		if ( FCKBrowserInfo.IsIE )
		{
			table.rows[i].replaceNode( rowObj ) ;
		}
		else
		{
			table.rows[i].innerHTML = '' ;
			FCKDomTools.MoveChildren( rowObj, table.rows[i] ) ;
		}
	}
}

FCKTableHandler._MoveCaretToCell = function ( refCell, toStart )
{
	var range = new FCKDomRange( FCK.EditorWindow ) ;
	range.MoveToNodeContents( refCell ) ;
	range.Collapse( toStart ) ;
	range.Select() ;
}

FCKTableHandler.ClearRow = function( tr )
{

	var aCells = tr.cells ;


	for ( var i = 0 ; i < aCells.length ; i++ )
	{
		aCells[i].innerHTML = '' ;

		if ( FCKBrowserInfo.IsGeckoLike )
			FCKTools.AppendBogusBr( aCells[i] ) ;
	}
}

FCKTableHandler.GetMergeRightTarget = function()
{
	var cells = this.GetSelectedCells() ;
	if ( cells.length != 1 )
		return null ;

	var refCell = cells[0] ;
	var tableMap = this._CreateTableMap( refCell ) ;
	var rowIdx = refCell.parentNode.rowIndex ;
	var colIdx = this._GetCellIndexSpan( tableMap, rowIdx, refCell ) ;
	var nextColIdx = colIdx + ( isNaN( refCell.colSpan ) ? 1 : refCell.colSpan ) ;
	var nextCell = tableMap[rowIdx][nextColIdx] ;

	if ( ! nextCell )
		return null ;


	this._MarkCells( [refCell, nextCell], '_SizeTest' ) ;
	var refGeometry = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_SizeTest' ) ;
	var nextGeometry = this._GetMarkerGeometry( tableMap, rowIdx, nextColIdx, '_SizeTest' ) ;
	this._UnmarkCells( [refCell, nextCell], '_SizeTest' ) ;

	if ( refGeometry.height != nextGeometry.height || refGeometry.y != nextGeometry.y )
		return null ;

	return { 'refCell' : refCell, 'nextCell' : nextCell, 'tableMap' : tableMap } ;
}

FCKTableHandler.GetMergeDownTarget = function()
{
	var cells = this.GetSelectedCells() ;
	if ( cells.length != 1 )
		return null ;

	var refCell = cells[0] ;
	var tableMap = this._CreateTableMap( refCell ) ;
	var rowIdx = refCell.parentNode.rowIndex ;
	var colIdx = this._GetCellIndexSpan( tableMap, rowIdx, refCell ) ;
	var newRowIdx = rowIdx + ( isNaN( refCell.rowSpan ) ? 1 : refCell.rowSpan ) ;
	if ( ! tableMap[newRowIdx] )
		return null ;

	var nextCell = tableMap[newRowIdx][colIdx] ;

	if ( ! nextCell )
		return null ;


	if ( refCell.parentNode.parentNode != nextCell.parentNode.parentNode )
		return null ;


	this._MarkCells( [refCell, nextCell], '_SizeTest' ) ;
	var refGeometry = this._GetMarkerGeometry( tableMap, rowIdx, colIdx, '_SizeTest' ) ;
	var nextGeometry = this._GetMarkerGeometry( tableMap, newRowIdx, colIdx, '_SizeTest' ) ;
	this._UnmarkCells( [refCell, nextCell], '_SizeTest' ) ;

	if ( refGeometry.width != nextGeometry.width || refGeometry.x != nextGeometry.x )
		return null ;

	return { 'refCell' : refCell, 'nextCell' : nextCell, 'tableMap' : tableMap } ;
}