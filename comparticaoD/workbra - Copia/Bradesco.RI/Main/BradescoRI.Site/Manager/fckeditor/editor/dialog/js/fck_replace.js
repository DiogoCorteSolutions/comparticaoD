﻿var dialog	= window.parent ;
var oEditor = dialog.InnerDialogLoaded() ;
var dialogArguments = dialog.Args() ;

var FCKLang = oEditor.FCKLang ;
var FCKDomTools = oEditor.FCKDomTools ;
var FCKDomRange = oEditor.FCKDomRange ;
var FCKListsLib = oEditor.FCKListsLib ;
var FCKTools = oEditor.FCKTools ;
var EditorDocument = oEditor.FCK.EditorDocument ;
var HighlightStyle = oEditor.FCKStyles.GetStyle( '_FCK_SelectionHighlight' )  ;

dialog.AddTab( 'Find', FCKLang.DlgFindTitle ) ;
dialog.AddTab( 'Replace', FCKLang.DlgReplaceTitle ) ;
var idMap = {} ;

function OnDialogTabChange( tabCode )
{
	ShowE( 'divFind', ( tabCode == 'Find' ) ) ;
	ShowE( 'divReplace', ( tabCode == 'Replace' ) ) ;
	idMap['FindText'] = 'txtFind' + tabCode ;
	idMap['CheckCase'] = 'chkCase' + tabCode ;
	idMap['CheckWord'] = 'chkWord' + tabCode ;

	if ( tabCode == 'Replace' )
		dialog.SetAutoSize( true ) ;
}

GetNextNonEmptyTextNode = function( node, stopNode )
{
	while ( ( node = FCKDomTools.GetNextSourceNode( node, false, 3, stopNode ) ) && node && node.length < 1 )
		1 ;
	return node ;
}

CharacterCursor = function( arg )
{
	if ( arg.nodeType && arg.nodeType == 9 )
	{
		this._textNode = GetNextNonEmptyTextNode( arg.body, arg.documentElement ) ;
		this._offset = 0 ;
		this._doc = arg ;
	}
	else
	{
		this._textNode = arguments[0] ;
		this._offset = arguments[1] ;
		this._doc = FCKTools.GetElementDocument( arguments[0] ) ;
	}
}
CharacterCursor.prototype =
{
	GetCharacter : function()
	{
		return ( this._textNode && this._textNode.nodeValue.charAt( this._offset ) ) || null ;
	},


	GetTextNode : function()
	{
		return this._textNode ;
	},


	GetIndex : function()
	{
		return this._offset ;
	},


	MoveNext : function()
	{
		if ( this._offset < this._textNode.length - 1 )
		{
			this._offset++ ;
			return false ;
		}

		var crossed = false ;
		var curNode = this._textNode ;
		while ( ( curNode = FCKDomTools.GetNextSourceNode( curNode ) )
				&& curNode && ( curNode.nodeType != 3 || curNode.length < 1 ) )
		{
			var tag = curNode.nodeName.toLowerCase() ;
			if ( FCKListsLib.BlockElements[tag] || tag == 'br' )
				crossed = true ;
		}

		this._textNode = curNode ;
		this._offset = 0 ;
		return crossed ;
	},


	MoveBack : function()
	{
		if ( this._offset > 0 && this._textNode.length > 0 )
		{
			this._offset = Math.min( this._offset - 1, this._textNode.length - 1 ) ;
			return false ;
		}

		var crossed = false ;
		var curNode = this._textNode ;
		while ( ( curNode = FCKDomTools.GetPreviousSourceNode( curNode ) )
				&& curNode && ( curNode.nodeType != 3 || curNode.length < 1 ) )
		{
			var tag = curNode.nodeName.toLowerCase() ;
			if ( FCKListsLib.BlockElements[tag] || tag == 'br' )
				crossed = true ;
		}

		this._textNode = curNode ;
		this._offset = curNode && curNode.length - 1 ;
		return crossed ;
	},

	Clone : function()
	{
		return new CharacterCursor( this._textNode, this._offset ) ;
	}
} ;

CharacterRange = function( initCursor, maxLength )
{
	this._cursors = initCursor.push ? initCursor : [initCursor] ;
	this._maxLength = maxLength ;
	this._highlightRange = null ;
}
CharacterRange.prototype =
{
	ToDomRange : function()
	{
		var firstCursor = this._cursors[0] ;
		var lastCursor = this._cursors[ this._cursors.length - 1 ] ;
		var domRange = new FCKDomRange( FCKTools.GetElementWindow( firstCursor.GetTextNode() ) ) ;
		var w3cRange = domRange._Range = domRange.CreateRange() ;
		w3cRange.setStart( firstCursor.GetTextNode(), firstCursor.GetIndex() ) ;
		w3cRange.setEnd( lastCursor.GetTextNode(), lastCursor.GetIndex() + 1 ) ;
		domRange._UpdateElementInfo() ;
		return domRange ;
	},

	Highlight : function()
	{
		if ( this._cursors.length < 1 )
			return ;

		var domRange = this.ToDomRange() ;
		HighlightStyle.ApplyToRange( domRange, false, true ) ;
		this._highlightRange = domRange ;

		var charRange = CharacterRange.CreateFromDomRange( domRange ) ;
		var focusNode = domRange.StartNode ;
		if ( focusNode.nodeType != 1 )
			focusNode = focusNode.parentNode ;
		FCKDomTools.ScrollIntoView( focusNode, false ) ;
		this._cursors = charRange._cursors ;
	},

	RemoveHighlight : function()
	{
		if ( this._highlightRange )
		{
			HighlightStyle.RemoveFromRange( this._highlightRange, false, true ) ;
			var charRange = CharacterRange.CreateFromDomRange( this._highlightRange ) ;
			this._cursors = charRange._cursors ;
			this._highlightRange = null ;
		}
	},

	GetHighlightDomRange : function()
	{
		return this._highlightRange;
	},

	MoveNext : function()
	{
		var next = this._cursors[ this._cursors.length - 1 ].Clone() ;
		var retval = next.MoveNext() ;
		if ( retval )
			this._cursors = [] ;
		this._cursors.push( next ) ;
		if ( this._cursors.length > this._maxLength )
			this._cursors.shift() ;
		return retval ;
	},

	MoveBack : function()
	{
		var prev = this._cursors[0].Clone() ;
		var retval = prev.MoveBack() ;
		if ( retval )
			this._cursors = [] ;
		this._cursors.unshift( prev ) ;
		if ( this._cursors.length > this._maxLength )
			this._cursors.pop() ;
		return retval ;
	},

	GetEndCharacter : function()
	{
		if ( this._cursors.length < 1 )
			return null ;
		var retval = this._cursors[ this._cursors.length - 1 ].GetCharacter() ;
		return retval ;
	},

	GetNextRange : function( len )
	{
		if ( this._cursors.length == 0 )
			return null ;
		var cur = this._cursors[ this._cursors.length - 1 ].Clone() ;
		cur.MoveNext() ;
		return new CharacterRange( cur, len ) ;
	},

	GetCursors : function()
	{
		return this._cursors ;
	}
} ;

CharacterRange.CreateFromDomRange = function( domRange )
{
	var w3cRange = domRange._Range ;
	var startContainer = w3cRange.startContainer ;
	var endContainer = w3cRange.endContainer ;
	var startTextNode, startIndex, endTextNode, endIndex ;

	if ( startContainer.nodeType == 3 )
	{
		startTextNode = startContainer ;
		startIndex = w3cRange.startOffset ;
	}
	else if ( domRange.StartNode.nodeType == 3 )
	{
		startTextNode = domRange.StartNode ;
		startIndex = 0 ;
	}
	else
	{
		startTextNode = GetNextNonEmptyTextNode( domRange.StartNode, domRange.StartNode.parentNode ) ;
		if ( !startTextNode )
			return null ;
		startIndex = 0 ;
	}

	if ( endContainer.nodeType == 3 && w3cRange.endOffset > 0 )
	{
		endTextNode = endContainer ;
		endIndex = w3cRange.endOffset - 1 ;
	}
	else
	{
		endTextNode = domRange.EndNode ;
		while ( endTextNode.nodeType != 3 )
			endTextNode = endTextNode.lastChild ;
		endIndex = endTextNode.length - 1 ;
	}

	var cursors = [] ;
	var current = new CharacterCursor( startTextNode, startIndex ) ;
	cursors.push( current ) ;
	if ( !( current.GetTextNode() == endTextNode && current.GetIndex() == endIndex ) && !domRange.CheckIsEmpty() )
	{
		do
		{
			current = current.Clone() ;
			current.MoveNext() ;
			cursors.push( current ) ;
		}
		while ( !( current.GetTextNode() == endTextNode && current.GetIndex() == endIndex ) ) ;
	}

	return new CharacterRange( cursors, cursors.length ) ;
}


KMP_NOMATCH = 0 ;
KMP_ADVANCED = 1 ;
KMP_MATCHED = 2 ;
KmpMatch = function( pattern, ignoreCase )
{
	var overlap = [ -1 ] ;
	for ( var i = 0 ; i < pattern.length ; i++ )
	{
		overlap.push( overlap[i] + 1 ) ;
		while ( overlap[ i + 1 ] > 0 && pattern.charAt( i ) != pattern.charAt( overlap[ i + 1 ] - 1 ) )
			overlap[ i + 1 ] = overlap[ overlap[ i + 1 ] - 1 ] + 1 ;
	}
	this._Overlap = overlap ;
	this._State = 0 ;
	this._IgnoreCase = ( ignoreCase === true ) ;
	if ( ignoreCase )
		this.Pattern = pattern.toLowerCase();
	else
		this.Pattern = pattern ;
}
KmpMatch.prototype = {
	FeedCharacter : function( c )
	{
		if ( this._IgnoreCase )
			c = c.toLowerCase();

		while ( true )
		{
			if ( c == this.Pattern.charAt( this._State ) )
			{
				this._State++ ;
				if ( this._State == this.Pattern.length )
				{

					this._State = 0;
					return KMP_MATCHED;
				}
				return KMP_ADVANCED ;
			}
			else if ( this._State == 0 )
				return KMP_NOMATCH;
			else
				this._State = this._Overlap[ this._State ];
		}

		return null ;
	},

	Reset : function()
	{
		this._State = 0 ;
	}
};


function OnLoad()
{

	oEditor.FCKLanguageManager.TranslatePage( document ) ;


	if ( dialogArguments.CustomValue == 'Find' )
	{
		dialog.SetSelectedTab( 'Find' ) ;
		dialog.SetAutoSize( true ) ;
	}
	else
		dialog.SetSelectedTab( 'Replace' ) ;

	SelectField( 'txtFind' + dialogArguments.CustomValue ) ;
}

function btnStat()
{
	GetE('btnReplace').disabled =
		GetE('btnReplaceAll').disabled =
			GetE('btnFind').disabled =
				( GetE(idMap["FindText"]).value.length == 0 ) ;
}

function btnStatDelayed()
{
	setTimeout( btnStat, 1 ) ;
}

function GetSearchString()
{
	return GetE(idMap['FindText']).value ;
}

function GetReplaceString()
{
	return GetE("txtReplace").value ;
}

function GetCheckCase()
{
	return !! ( GetE(idMap['CheckCase']).checked ) ;
}

function GetMatchWord()
{
	return !! ( GetE(idMap['CheckWord']).checked ) ;
}

function CheckIsWordSeparator( c )
{
	if ( !c )
		return true;
	var code = c.charCodeAt( 0 );
	if ( code >= 9 && code <= 0xd )
		return true;
	if ( code >= 0x2000 && code <= 0x200a )
		return true;
	switch ( code )
	{
		case 0x20:
		case 0x85:
		case 0xa0:
		case 0x1680:
		case 0x180e:
		case 0x2028:
		case 0x2029:
		case 0x202f:
		case 0x205f:
		case 0x3000:
			return true;
		default:
	}
	return /[.,"'?!;:]/.test( c ) ;
}

FindRange = null ;
function _Find()
{
	var searchString = GetSearchString() ;
	if ( !FindRange )
		FindRange = new CharacterRange( new CharacterCursor( EditorDocument ), searchString.length ) ;
	else
	{
		FindRange.RemoveHighlight() ;
		FindRange = FindRange.GetNextRange( searchString.length ) ;
	}
	var matcher = new KmpMatch( searchString, ! GetCheckCase() ) ;
	var matchState = KMP_NOMATCH ;
	var character = '%' ;

	while ( character != null )
	{
		while ( ( character = FindRange.GetEndCharacter() ) )
		{
			matchState = matcher.FeedCharacter( character ) ;
			if ( matchState == KMP_MATCHED )
				break ;
			if ( FindRange.MoveNext() )
				matcher.Reset() ;
		}

		if ( matchState == KMP_MATCHED )
		{
			if ( GetMatchWord() )
			{
				var cursors = FindRange.GetCursors() ;
				var head = cursors[ cursors.length - 1 ].Clone() ;
				var tail = cursors[0].Clone() ;
				if ( !head.MoveNext() && !CheckIsWordSeparator( head.GetCharacter() ) )
					continue ;
				if ( !tail.MoveBack() && !CheckIsWordSeparator( tail.GetCharacter() ) )
					continue ;
			}

			FindRange.Highlight() ;
			return true ;
		}
	}

	FindRange = null ;
	return false ;
}

function Find()
{
	if ( ! _Find() )
		alert( FCKLang.DlgFindNotFoundMsg ) ;
}

function Replace()
{
	var saveUndoStep = function( selectRange )
	{
		var ieRange ;
		if ( oEditor.FCKBrowserInfo.IsIE )
			ieRange = document.selection.createRange() ;

		selectRange.Select() ;
		oEditor.FCKUndo.SaveUndoStep() ;
		var cloneRange = selectRange.Clone() ;
		cloneRange.Collapse( false ) ;
		cloneRange.Select() ;

		if ( ieRange )
			setTimeout( function(){ ieRange.select() ; }, 1 ) ;
	}

	if ( FindRange && FindRange.GetHighlightDomRange() )
	{
		var range = FindRange.GetHighlightDomRange() ;
		var bookmark = range.CreateBookmark() ;
		FindRange.RemoveHighlight() ;
		range.MoveToBookmark( bookmark ) ;

		saveUndoStep( range ) ;
		range.DeleteContents() ;
		range.InsertNode( EditorDocument.createTextNode( GetReplaceString() ) ) ;
		range._UpdateElementInfo() ;

		FindRange = CharacterRange.CreateFromDomRange( range ) ;
	}
	else
	{
		if ( ! _Find() )
		{
			FindRange && FindRange.RemoveHighlight() ;
			alert( FCKLang.DlgFindNotFoundMsg ) ;
		}
	}
}

function ReplaceAll()
{
	oEditor.FCKUndo.SaveUndoStep() ;
	var replaceCount = 0 ;

	while ( _Find() )
	{
		var range = FindRange.GetHighlightDomRange() ;
		var bookmark = range.CreateBookmark() ;
		FindRange.RemoveHighlight() ;
		range.MoveToBookmark( bookmark) ;

		range.DeleteContents() ;
		range.InsertNode( EditorDocument.createTextNode( GetReplaceString() ) ) ;
		range._UpdateElementInfo() ;

		FindRange = CharacterRange.CreateFromDomRange( range ) ;
		replaceCount++ ;
	}
	if ( replaceCount == 0 )
	{
		FindRange && FindRange.RemoveHighlight() ;
		alert( FCKLang.DlgFindNotFoundMsg ) ;
	}
	dialog.Cancel() ;
}

window.onunload = function()
{
	if ( FindRange )
	{
		FindRange.RemoveHighlight() ;
		FindRange.ToDomRange().Select() ;
	}
}