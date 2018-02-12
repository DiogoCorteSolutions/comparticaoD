FCKSelection.GetType = function()
{


	try
	{
		var ieType = FCKSelection.GetSelection().type ;
		if ( ieType == 'Control' || ieType == 'Text' )
			return ieType ;

		if ( this.GetSelection().createRange().parentElement )
			return 'Text' ;
	}
	catch(e)
	{

	}

	return 'None' ;
} ;



FCKSelection.GetSelectedElement = function()
{
	if ( this.GetType() == 'Control' )
	{
		var oRange = this.GetSelection().createRange() ;

		if ( oRange && oRange.item )
			return this.GetSelection().createRange().item(0) ;
	}
	return null ;
} ;

FCKSelection.GetParentElement = function()
{
	switch ( this.GetType() )
	{
		case 'Control' :
			var el = FCKSelection.GetSelectedElement() ;
			return el ? el.parentElement : null ;

		case 'None' :
			return null ;

		default :
			return this.GetSelection().createRange().parentElement() ;
	}
} ;

FCKSelection.GetBoundaryParentElement = function( startBoundary )
{
	switch ( this.GetType() )
	{
		case 'Control' :
			var el = FCKSelection.GetSelectedElement() ;
			return el ? el.parentElement : null ;

		case 'None' :
			return null ;

		default :
			var doc = FCK.EditorDocument ;

			var range = doc.selection.createRange() ;
			range.collapse( startBoundary !== false ) ;

			var el = range.parentElement() ;



			return FCKTools.GetElementDocument( el ) == doc ? el : null ;
	}
} ;

FCKSelection.SelectNode = function( node )
{
	FCK.Focus() ;
	this.GetSelection().empty() ;
	var oRange ;
	try
	{

		oRange = FCK.EditorDocument.body.createControlRange() ;
		oRange.addElement( node ) ;
	}
	catch(e)
	{

		oRange = FCK.EditorDocument.body.createTextRange() ;
		oRange.moveToElementText( node ) ;
	}

	oRange.select() ;
} ;

FCKSelection.Collapse = function( toStart )
{
	FCK.Focus() ;
	if ( this.GetType() == 'Text' )
	{
		var oRange = this.GetSelection().createRange() ;
		oRange.collapse( toStart == null || toStart === true ) ;
		oRange.select() ;
	}
} ;


FCKSelection.HasAncestorNode = function( nodeTagName )
{
	var oContainer ;

	if ( this.GetSelection().type == "Control" )
	{
		oContainer = this.GetSelectedElement() ;
	}
	else
	{
		var oRange  = this.GetSelection().createRange() ;
		oContainer = oRange.parentElement() ;
	}

	while ( oContainer )
	{
		if ( oContainer.nodeName.IEquals( nodeTagName ) ) return true ;
		oContainer = oContainer.parentNode ;
	}

	return false ;
} ;



FCKSelection.MoveToAncestorNode = function( nodeTagName )
{
	var oNode, oRange ;

	if ( ! FCK.EditorDocument )
		return null ;

	if ( this.GetSelection().type == "Control" )
	{
		oRange = this.GetSelection().createRange() ;
		for ( i = 0 ; i < oRange.length ; i++ )
		{
			if (oRange(i).parentNode)
			{
				oNode = oRange(i).parentNode ;
				break ;
			}
		}
	}
	else
	{
		oRange  = this.GetSelection().createRange() ;
		oNode = oRange.parentElement() ;
	}

	while ( oNode && !oNode.nodeName.Equals( nodeTagName ) )
		oNode = oNode.parentNode ;

	return oNode ;
} ;

FCKSelection.Delete = function()
{

	var oSel = this.GetSelection() ;


	if ( oSel.type.toLowerCase() != "none" )
	{
		oSel.clear() ;
	}

	return oSel ;
} ;

FCKSelection.GetSelection = function()
{
	this.Restore() ;
	return FCK.EditorDocument.selection ;
}

FCKSelection.Save = function( lock )
{
	var editorDocument = FCK.EditorDocument ;

	if ( !editorDocument )
		return ;


	if ( this.locked )
		return ;
	this.locked = !!lock ;

	var selection = editorDocument.selection ;
	var range ;

	if ( selection )
	{


		try {
			range = selection.createRange() ;
		}
		catch(e) {}


		if ( range )
		{
			if ( range.parentElement && FCKTools.GetElementDocument( range.parentElement() ) != editorDocument )
				range = null ;
			else if ( range.item && FCKTools.GetElementDocument( range.item(0) )!= editorDocument )
				range = null ;
		}
	}

	this.SelectionData = range ;
}

FCKSelection._GetSelectionDocument = function( selection )
{
	var range = selection.createRange() ;
	if ( !range )
		return null;
	else if ( range.item )
		return FCKTools.GetElementDocument( range.item( 0 ) ) ;
	else
		return FCKTools.GetElementDocument( range.parentElement() ) ;
}

FCKSelection.Restore = function()
{
	if ( this.SelectionData )
	{
		FCK.IsSelectionChangeLocked = true ;

		try
		{

			if ( String( this._GetSelectionDocument( FCK.EditorDocument.selection ).body.contentEditable ) == 'true' )
			{
				FCK.IsSelectionChangeLocked = false ;
				return ;
			}
			this.SelectionData.select() ;
		}
		catch ( e ) {}

		FCK.IsSelectionChangeLocked = false ;
	}
}

FCKSelection.Release = function()
{
	this.locked = false ;
	delete this.SelectionData ;
}
