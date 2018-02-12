var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;
var FCK = oEditor.FCK ;
var FCKLang = oEditor.FCKLang ;
var FCKBrowserInfo = oEditor.FCKBrowserInfo ;
var FCKStyles = oEditor.FCKStyles ;
var FCKElementPath = oEditor.FCKElementPath ;
var FCKDomRange = oEditor.FCKDomRange ;
var FCKDomTools = oEditor.FCKDomTools ;
var FCKDomRangeIterator = oEditor.FCKDomRangeIterator ;
var FCKListsLib = oEditor.FCKListsLib ;
var AlwaysCreate = dialog.Args().CustomValue ;

String.prototype.IEquals = function()
{
	var thisUpper = this.toUpperCase() ;

	var aArgs = arguments ;

	if ( aArgs.length == 1 && aArgs[0].pop )
		aArgs = aArgs[0] ;

	for ( var i = 0 ; i < aArgs.length ; i++ )
	{
		if ( thisUpper == aArgs[i].toUpperCase() )
			return true ;
	}
	return false ;
}

var CurrentContainers = [] ;
if ( !AlwaysCreate )
{
	dialog.Selection.EnsureSelection() ;
	CurrentContainers = FCKDomTools.GetSelectedDivContainers() ;
}

dialog.AddTab( 'General', FCKLang.DlgDivGeneralTab );
dialog.AddTab( 'Advanced', FCKLang.DlgDivAdvancedTab ) ;

function AddStyleOption( styleName )
{
	var el = GetE( 'selStyle' ) ;
	var opt = document.createElement( 'option' ) ;
	opt.text = opt.value = styleName ;

	if ( FCKBrowserInfo.IsIE )
		el.add( opt ) ;
	else
		el.add( opt, null ) ;
}

function OnDialogTabChange( tabCode )
{
	ShowE( 'divGeneral', tabCode == 'General' ) ;
	ShowE( 'divAdvanced', tabCode == 'Advanced' ) ;
	dialog.SetAutoSize( true ) ;
}

function GetNearestAncestorDirection( node )
{
	var dir = 'ltr' ;
	while ( ( node = node.parentNode ) )
	{
		if ( node.dir )
			dir = node.dir ;
	}
	return dir ;
}

window.onload = function()
{
	oEditor.FCKLanguageManager.TranslatePage(document) ;

	dialog.SetOkButton( true ) ;
	dialog.SetAutoSize( true ) ;

	var styles = FCKStyles.GetStyles() ;
	var selectableStyles = {} ;
	for ( var i in styles )
	{
		if ( ! /^_FCK_/.test( i ) && styles[i].Element == 'div' )
			selectableStyles[i] = styles[i] ;
	}
	if ( CurrentContainers.length <= 1 )
	{
		var target = CurrentContainers[0] ;
		var match = null ;
		for ( var i in selectableStyles )
		{
			if ( target && styles[i].CheckElementRemovable( target, true ) )
				match = i ;
		}
		if ( !match )
			AddStyleOption( "" ) ;
		for ( var i in selectableStyles )
			AddStyleOption( i ) ;
		if ( match )
			GetE( 'selStyle' ).value = match ;

		if ( target )
		{
			GetE( 'txtClass' ).value = target.className ;
			GetE( 'txtId' ).value = target.id ;
			GetE( 'txtLang' ).value = target.lang ;
			GetE( 'txtInlineStyle').value = target.style.cssText ;
			GetE( 'txtTitle' ).value = target.title ;
			GetE( 'selLangDir').value = target.dir || GetNearestAncestorDirection( target ) ;
		}
	}
	else
	{
		GetE( 'txtId' ).disabled = true ;
		AddStyleOption( "" ) ;
		for ( var i in selectableStyles )
			AddStyleOption( i ) ;
	}
}

function CreateDiv()
{
	var newBlocks = [] ;
	var range = new FCKDomRange( FCK.EditorWindow ) ;
	range.MoveToSelection() ;

	var bookmark = range.CreateBookmark() ;

	if ( FCKBrowserInfo.IsIE )
	{
		var bStart	= range.GetBookmarkNode( bookmark, true ) ;
		var bEnd	= range.GetBookmarkNode( bookmark, false ) ;

		var cursor ;

		if ( bStart
				&& bStart.parentNode.nodeName.IEquals( 'div' )
				&& !bStart.previousSibling )
		{
			cursor = bStart ;
			while ( ( cursor = cursor.nextSibling ) )
			{
				if ( FCKListsLib.BlockElements[ cursor.nodeName.toLowerCase() ] )
					FCKDomTools.MoveNode( bStart, cursor, true ) ;
			}
		}

		if ( bEnd
				&& bEnd.parentNode.nodeName.IEquals( 'div' )
				&& !bEnd.previousSibling )
		{
			cursor = bEnd ;
			while ( ( cursor = cursor.nextSibling ) )
			{
				if ( FCKListsLib.BlockElements[ cursor.nodeName.toLowerCase() ] )
				{
					if ( cursor.firstChild == bStart )
						FCKDomTools.InsertAfterNode( bStart, bEnd ) ;
					else
						FCKDomTools.MoveNode( bEnd, cursor, true ) ;
				}
			}
		}
	}

	var iterator = new FCKDomRangeIterator( range ) ;
	var block ;

	var paragraphs = [] ;
	while ( ( block = iterator.GetNextParagraph() ) )
		paragraphs.push( block ) ;

	var commonParent = paragraphs[0].parentNode ;
	var tmp = [] ;
	for ( var i = 0 ; i < paragraphs.length ; i++ )
	{
		block = paragraphs[i] ;
		commonParent = FCKDomTools.GetCommonParents( block.parentNode, commonParent ).pop() ;
	}

	while ( commonParent.nodeName.IEquals( 'table', 'tbody', 'tr', 'ol', 'ul' ) )
		commonParent = commonParent.parentNode ;

	var lastBlock = null ;
	while ( paragraphs.length > 0 )
	{
		block = paragraphs.shift() ;
		while ( block.parentNode != commonParent )
			block = block.parentNode ;
		if ( block != lastBlock )
			tmp.push( block ) ;
		lastBlock = block ;
	}
	paragraphs = tmp ;

	var groups = [] ;
	var lastBlockLimit = null ;
	for ( var i = 0 ; i < paragraphs.length ; i++ )
	{
		block = paragraphs[i] ;
		var elementPath = new FCKElementPath( block ) ;
		if ( elementPath.BlockLimit != lastBlockLimit )
		{
			groups.push( [] ) ;
			lastBlockLimit = elementPath.BlockLimit ;
		}
		groups[groups.length - 1].push( block ) ;
	}

	for ( var i = 0 ; i < groups.length ; i++ )
	{
		var divNode = FCK.EditorDocument.createElement( 'div' ) ;
		groups[i][0].parentNode.insertBefore( divNode, groups[i][0] ) ;
		for ( var j = 0 ; j < groups[i].length ; j++ )
			FCKDomTools.MoveNode( groups[i][j], divNode ) ;
		newBlocks.push( divNode ) ;
	}

	range.MoveToBookmark( bookmark ) ;
	range.Select() ;

	FCK.Focus() ;
	FCK.Events.FireEvent( 'OnSelectionChange' ) ;

	return newBlocks ;
}

function Ok()
{
	oEditor.FCKUndo.SaveUndoStep() ;

	if ( CurrentContainers.length < 1 )
		CurrentContainers = CreateDiv();

	var setValue = function( attrName, inputName )
	{
		var val = GetE( inputName ).value ;
		for ( var i = 0 ; i < CurrentContainers.length ; i++ )
		{
			if ( val == '' )
				CurrentContainers[i].removeAttribute( attrName ) ;
			else
				CurrentContainers[i].setAttribute( attrName, val ) ;
		}
	}

	if ( CurrentContainers.length == 1 )
	{
		setValue( 'class', 'txtClass' ) ;
		setValue( 'id', 'txtId' ) ;
	}
	setValue( 'lang', 'txtLang' ) ;
	if ( FCKBrowserInfo.IsIE )
	{
		for ( var i = 0 ; i < CurrentContainers.length ; i++ )
			CurrentContainers[i].style.cssText = GetE( 'txtInlineStyle' ).value ;
	}
	else
		setValue( 'style', 'txtInlineStyle' ) ;
	setValue( 'title', 'txtTitle' ) ;
	for ( var i = 0 ; i < CurrentContainers.length ; i++ )
	{
		var dir = GetE( 'selLangDir' ).value ;
		var styleName = GetE( 'selStyle' ).value ;
		if ( GetNearestAncestorDirection( CurrentContainers[i] ) != dir )
			CurrentContainers[i].dir = dir ;
		else
			CurrentContainers[i].removeAttribute( 'dir' ) ;

		if ( styleName )
			FCKStyles.GetStyle( styleName ).ApplyToObject( CurrentContainers[i] ) ;
	}

	return true ;
}