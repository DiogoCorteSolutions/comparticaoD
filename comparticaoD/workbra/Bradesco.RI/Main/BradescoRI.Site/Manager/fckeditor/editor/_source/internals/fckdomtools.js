var FCKDomTools =
{
	MoveChildren : function( source, target, toTargetStart )
	{
		if ( source == target )
			return ;

		var eChild ;

		if ( toTargetStart )
		{
			while ( (eChild = source.lastChild) )
				target.insertBefore( source.removeChild( eChild ), target.firstChild ) ;
		}
		else
		{
			while ( (eChild = source.firstChild) )
				target.appendChild( source.removeChild( eChild ) ) ;
		}
	},

	MoveNode : function( source, target, toTargetStart )
	{
		if ( toTargetStart )
			target.insertBefore( FCKDomTools.RemoveNode( source ), target.firstChild ) ;
		else
			target.appendChild( FCKDomTools.RemoveNode( source ) ) ;
	},


	TrimNode : function( node )
	{
		this.LTrimNode( node ) ;
		this.RTrimNode( node ) ;
	},

	LTrimNode : function( node )
	{
		var eChildNode ;

		while ( (eChildNode = node.firstChild) )
		{
			if ( eChildNode.nodeType == 3 )
			{
				var sTrimmed = eChildNode.nodeValue.LTrim() ;
				var iOriginalLength = eChildNode.nodeValue.length ;

				if ( sTrimmed.length == 0 )
				{
					node.removeChild( eChildNode ) ;
					continue ;
				}
				else if ( sTrimmed.length < iOriginalLength )
				{
					eChildNode.splitText( iOriginalLength - sTrimmed.length ) ;
					node.removeChild( node.firstChild ) ;
				}
			}
			break ;
		}
	},

	RTrimNode : function( node )
	{
		var eChildNode ;

		while ( (eChildNode = node.lastChild) )
		{
			if ( eChildNode.nodeType == 3 )
			{
				var sTrimmed = eChildNode.nodeValue.RTrim() ;
				var iOriginalLength = eChildNode.nodeValue.length ;

				if ( sTrimmed.length == 0 )
				{



					eChildNode.parentNode.removeChild( eChildNode ) ;
					continue ;
				}
				else if ( sTrimmed.length < iOriginalLength )
				{




					eChildNode.splitText( sTrimmed.length ) ;

					node.lastChild.parentNode.removeChild( node.lastChild ) ;
				}
			}
			break ;
		}

		if ( !FCKBrowserInfo.IsIE && !FCKBrowserInfo.IsOpera )
		{
			eChildNode = node.lastChild ;

			if ( eChildNode && eChildNode.nodeType == 1 && eChildNode.nodeName.toLowerCase() == 'br' )
			{

				eChildNode.parentNode.removeChild( eChildNode ) ;
			}
		}
	},

	RemoveNode : function( node, excludeChildren )
	{
		if ( excludeChildren )
		{

			var eChild ;
			while ( (eChild = node.firstChild) )
				node.parentNode.insertBefore( node.removeChild( eChild ), node ) ;
		}

		return node.parentNode.removeChild( node ) ;
	},

	GetFirstChild : function( node, childNames )
	{

		if ( typeof ( childNames ) == 'string' )
			childNames = [ childNames ] ;

		var eChild = node.firstChild ;
		while( eChild )
		{
			if ( eChild.nodeType == 1 && eChild.tagName.Equals.apply( eChild.tagName, childNames ) )
				return eChild ;

			eChild = eChild.nextSibling ;
		}

		return null ;
	},

	GetLastChild : function( node, childNames )
	{

		if ( typeof ( childNames ) == 'string' )
			childNames = [ childNames ] ;

		var eChild = node.lastChild ;
		while( eChild )
		{
			if ( eChild.nodeType == 1 && ( !childNames || eChild.tagName.Equals( childNames ) ) )
				return eChild ;

			eChild = eChild.previousSibling ;
		}

		return null ;
	},

    GetPreviousSourceElement : function( currentNode, ignoreSpaceTextOnly, stopSearchElements, ignoreElements )
	{
		if ( !currentNode )
			return null ;

		if ( stopSearchElements && currentNode.nodeType == 1 && currentNode.nodeName.IEquals( stopSearchElements ) )
			return null ;

		if ( currentNode.previousSibling )
			currentNode = currentNode.previousSibling ;
		else
			return this.GetPreviousSourceElement( currentNode.parentNode, ignoreSpaceTextOnly, stopSearchElements, ignoreElements ) ;

		while ( currentNode )
		{
			if ( currentNode.nodeType == 1 )
			{
				if ( stopSearchElements && currentNode.nodeName.IEquals( stopSearchElements ) )
					break ;

				if ( !ignoreElements || !currentNode.nodeName.IEquals( ignoreElements ) )
					return currentNode ;
			}
			else if ( ignoreSpaceTextOnly && currentNode.nodeType == 3 && currentNode.nodeValue.RTrim().length > 0 )
				break ;

			if ( currentNode.lastChild )
				currentNode = currentNode.lastChild ;
			else
				return this.GetPreviousSourceElement( currentNode, ignoreSpaceTextOnly, stopSearchElements, ignoreElements ) ;
		}

		return null ;
	},

	GetNextSourceElement : function( currentNode, ignoreSpaceTextOnly, stopSearchElements, ignoreElements, startFromSibling )
	{
		while( ( currentNode = this.GetNextSourceNode( currentNode, startFromSibling ) ) )	// Only one "=".
		{
			if ( currentNode.nodeType == 1 )
			{
				if ( stopSearchElements && currentNode.nodeName.IEquals( stopSearchElements ) )
					break ;

				if ( ignoreElements && currentNode.nodeName.IEquals( ignoreElements ) )
					return this.GetNextSourceElement( currentNode, ignoreSpaceTextOnly, stopSearchElements, ignoreElements ) ;

				return currentNode ;
			}
			else if ( ignoreSpaceTextOnly && currentNode.nodeType == 3 && currentNode.nodeValue.RTrim().length > 0 )
				break ;
		}

		return null ;
	},

	GetNextSourceNode : function( currentNode, startFromSibling, nodeType, stopSearchNode )
	{
		if ( !currentNode )
			return null ;

		var node ;

		if ( !startFromSibling && currentNode.firstChild )
			node = currentNode.firstChild ;
		else
		{
			if ( stopSearchNode && currentNode == stopSearchNode )
				return null ;

			node = currentNode.nextSibling ;

			if ( !node && ( !stopSearchNode || stopSearchNode != currentNode.parentNode ) )
				return this.GetNextSourceNode( currentNode.parentNode, true, nodeType, stopSearchNode ) ;
		}

		if ( nodeType && node && node.nodeType != nodeType )
			return this.GetNextSourceNode( node, false, nodeType, stopSearchNode ) ;

		return node ;
	},

	GetPreviousSourceNode : function( currentNode, startFromSibling, nodeType, stopSearchNode )
	{
		if ( !currentNode )
			return null ;

		var node ;

		if ( !startFromSibling && currentNode.lastChild )
			node = currentNode.lastChild ;
		else
		{
			if ( stopSearchNode && currentNode == stopSearchNode )
				return null ;

			node = currentNode.previousSibling ;

			if ( !node && ( !stopSearchNode || stopSearchNode != currentNode.parentNode ) )
				return this.GetPreviousSourceNode( currentNode.parentNode, true, nodeType, stopSearchNode ) ;
		}

		if ( nodeType && node && node.nodeType != nodeType )
			return this.GetPreviousSourceNode( node, false, nodeType, stopSearchNode ) ;

		return node ;
	},


	InsertAfterNode : function( existingNode, newNode )
	{
		return existingNode.parentNode.insertBefore( newNode, existingNode.nextSibling ) ;
	},

	GetParents : function( node )
	{
		var parents = new Array() ;

		while ( node )
		{
			parents.unshift( node ) ;
			node = node.parentNode ;
		}

		return parents ;
	},

	GetCommonParents : function( node1, node2 )
	{
		var p1 = this.GetParents( node1 ) ;
		var p2 = this.GetParents( node2 ) ;
		var retval = [] ;
		for ( var i = 0 ; i < p1.length ; i++ )
		{
			if ( p1[i] == p2[i] )
				retval.push( p1[i] ) ;
		}
		return retval ;
	},

	GetCommonParentNode : function( node1, node2, tagList )
	{
		var tagMap = {} ;
		if ( ! tagList.pop )
			tagList = [ tagList ] ;
		while ( tagList.length > 0 )
			tagMap[tagList.pop().toLowerCase()] = 1 ;

		var commonParents = this.GetCommonParents( node1, node2 ) ;
		var currentParent = null ;
		while ( ( currentParent = commonParents.pop() ) )
		{
			if ( tagMap[currentParent.nodeName.toLowerCase()] )
				return currentParent ;
		}
		return null ;
	},

	GetIndexOf : function( node )
	{
		var currentNode = node.parentNode ? node.parentNode.firstChild : null ;
		var currentIndex = -1 ;

		while ( currentNode )
		{
			currentIndex++ ;

			if ( currentNode == node )
				return currentIndex ;

			currentNode = currentNode.nextSibling ;
		}

		return -1 ;
	},

	PaddingNode : null,

	EnforcePaddingNode : function( doc, tagName )
	{

		try
		{
			if ( !doc || !doc.body )
				return ;
		}
		catch (e)
		{
			return ;
		}

		this.CheckAndRemovePaddingNode( doc, tagName, true ) ;
		try
		{
			if ( doc.body.lastChild && ( doc.body.lastChild.nodeType != 1
					|| doc.body.lastChild.tagName.toLowerCase() == tagName.toLowerCase() ) )
				return ;
		}
		catch (e)
		{
			return ;
		}

		var node = doc.createElement( tagName ) ;
		if ( FCKBrowserInfo.IsGecko && FCKListsLib.NonEmptyBlockElements[ tagName ] )
			FCKTools.AppendBogusBr( node ) ;
		this.PaddingNode = node ;
		if ( doc.body.childNodes.length == 1
				&& doc.body.firstChild.nodeType == 1
				&& doc.body.firstChild.tagName.toLowerCase() == 'br'
				&& ( doc.body.firstChild.getAttribute( '_moz_dirty' ) != null
					|| doc.body.firstChild.getAttribute( 'type' ) == '_moz' ) )
			doc.body.replaceChild( node, doc.body.firstChild ) ;
		else
			doc.body.appendChild( node ) ;
	},

	CheckAndRemovePaddingNode : function( doc, tagName, dontRemove )
	{
		var paddingNode = this.PaddingNode ;
		if ( ! paddingNode )
			return ;


		try
		{
			if ( paddingNode.parentNode != doc.body
				|| paddingNode.tagName.toLowerCase() != tagName
				|| ( paddingNode.childNodes.length > 1 )
				|| ( paddingNode.firstChild && paddingNode.firstChild.nodeValue != '\xa0'
					&& String(paddingNode.firstChild.tagName).toLowerCase() != 'br' ) )
			{
				this.PaddingNode = null ;
				return ;
			}
		}
		catch (e)
		{
				this.PaddingNode = null ;
				return ;
		}



		if ( !dontRemove )
		{
			if ( paddingNode.parentNode.childNodes.length > 1 )
				paddingNode.parentNode.removeChild( paddingNode ) ;
			this.PaddingNode = null ;
		}
	},

	HasAttribute : function( element, attributeName )
	{
		if ( element.hasAttribute )
			return element.hasAttribute( attributeName ) ;
		else
		{
			var att = element.attributes[ attributeName ] ;
			return ( att != undefined && att.specified ) ;
		}
	},

	HasAttributes : function( element )
	{
		var attributes = element.attributes ;

		for ( var i = 0 ; i < attributes.length ; i++ )
		{
			if ( FCKBrowserInfo.IsIE && attributes[i].nodeName == 'class' )
			{






				if ( element.className.length > 0 )
					return true ;
			}
			else if ( attributes[i].specified )
				return true ;
		}

		return false ;
	},

	RemoveAttribute : function( element, attributeName )
	{
		if ( FCKBrowserInfo.IsIE && attributeName.toLowerCase() == 'class' )
			attributeName = 'className' ;

		return element.removeAttribute( attributeName, 0 ) ;
	},

	RemoveAttributes : function (element, aAttributes )
	{
		for ( var i = 0 ; i < aAttributes.length ; i++ )
			this.RemoveAttribute( element, aAttributes[i] );
	},

	GetAttributeValue : function( element, att )
	{
		var attName = att ;

		if ( typeof att == 'string' )
			att = element.attributes[ att ] ;
		else
			attName = att.nodeName ;

		if ( att && att.specified )
		{

			if ( attName == 'style' )
				return element.style.cssText ;



			else if ( attName == 'class' || attName.indexOf('on') == 0 )
				return att.nodeValue ;
			else
			{


				return element.getAttribute( attName, 2 ) ;
			}
		}
		return null ;
	},

	Contains : function( mainElement, otherElement )
	{

		if ( mainElement.contains && otherElement.nodeType == 1 )
			return mainElement.contains( otherElement ) ;

		while ( ( otherElement = otherElement.parentNode ) )	// Only one "="
		{
			if ( otherElement == mainElement )
				return true ;
		}
		return false ;
	},

	BreakParent : function( element, parent, reusableRange )
	{
		var range = reusableRange || new FCKDomRange( FCKTools.GetElementWindow( element ) ) ;



		range.SetStart( element, 4 ) ;
		range.SetEnd( parent, 4 ) ;


		var docFrag = range.ExtractContents() ;


		range.InsertNode( element.parentNode.removeChild( element ) ) ;


		docFrag.InsertAfterNode( element ) ;

		range.Release( !!reusableRange ) ;
	},

	GetNodeAddress : function( node, normalized )
	{
		var retval = [] ;
		while ( node && node != FCKTools.GetElementDocument( node ).documentElement )
		{
			var parentNode = node.parentNode ;
			var currentIndex = -1 ;
			for( var i = 0 ; i < parentNode.childNodes.length ; i++ )
			{
				var candidate = parentNode.childNodes[i] ;
				if ( normalized === true &&
						candidate.nodeType == 3 &&
						candidate.previousSibling &&
						candidate.previousSibling.nodeType == 3 )
					continue;
				currentIndex++ ;
				if ( parentNode.childNodes[i] == node )
					break ;
			}
			retval.unshift( currentIndex ) ;
			node = node.parentNode ;
		}
		return retval ;
	},

	GetNodeFromAddress : function( doc, addr, normalized )
	{
		var cursor = doc.documentElement ;
		for ( var i = 0 ; i < addr.length ; i++ )
		{
			var target = addr[i] ;
			if ( ! normalized )
			{
				cursor = cursor.childNodes[target] ;
				continue ;
			}

			var currentIndex = -1 ;
			for (var j = 0 ; j < cursor.childNodes.length ; j++ )
			{
				var candidate = cursor.childNodes[j] ;
				if ( normalized === true &&
						candidate.nodeType == 3 &&
						candidate.previousSibling &&
						candidate.previousSibling.nodeType == 3 )
					continue ;
				currentIndex++ ;
				if ( currentIndex == target )
				{
					cursor = candidate ;
					break ;
				}
			}
		}
		return cursor ;
	},

	CloneElement : function( element )
	{
		element = element.cloneNode( false ) ;


		element.removeAttribute( 'id', false ) ;

		return element ;
	},

	ClearElementJSProperty : function( element, attrName )
	{
		if ( FCKBrowserInfo.IsIE )
			element.removeAttribute( attrName ) ;
		else
			delete element[attrName] ;
	},

	SetElementMarker : function ( markerObj, element, attrName, value)
	{
		var id = String( parseInt( Math.random() * 0xffffffff, 10 ) ) ;
		element._FCKMarkerId = id ;
		element[attrName] = value ;
		if ( ! markerObj[id] )
			markerObj[id] = { 'element' : element, 'markers' : {} } ;
		markerObj[id]['markers'][attrName] = value ;
	},

	ClearElementMarkers : function( markerObj, element, clearMarkerObj )
	{
		var id = element._FCKMarkerId ;
		if ( ! id )
			return ;
		this.ClearElementJSProperty( element, '_FCKMarkerId' ) ;
		for ( var j in markerObj[id]['markers'] )
			this.ClearElementJSProperty( element, j ) ;
		if ( clearMarkerObj )
			delete markerObj[id] ;
	},

	ClearAllMarkers : function( markerObj )
	{
		for ( var i in markerObj )
			this.ClearElementMarkers( markerObj, markerObj[i]['element'], true ) ;
	},

	ListToArray : function( listNode, markerObj, baseArray, baseIndentLevel, grandparentNode )
	{
		if ( ! listNode.nodeName.IEquals( ['ul', 'ol'] ) )
			return [] ;

		if ( ! baseIndentLevel )
			baseIndentLevel = 0 ;
		if ( ! baseArray )
			baseArray = [] ;

		for ( var i = 0 ; i < listNode.childNodes.length ; i++ )
		{
			var listItem = listNode.childNodes[i] ;
			if ( ! listItem.nodeName.IEquals( 'li' ) )
				continue ;
			var itemObj = { 'parent' : listNode, 'indent' : baseIndentLevel, 'contents' : [] } ;
			if ( ! grandparentNode )
			{
				itemObj.grandparent = listNode.parentNode ;
				if ( itemObj.grandparent && itemObj.grandparent.nodeName.IEquals( 'li' ) )
					itemObj.grandparent = itemObj.grandparent.parentNode ;
			}
			else
				itemObj.grandparent = grandparentNode ;
			if ( markerObj )
				this.SetElementMarker( markerObj, listItem, '_FCK_ListArray_Index', baseArray.length ) ;
			baseArray.push( itemObj ) ;
			for ( var j = 0 ; j < listItem.childNodes.length ; j++ )
			{
				var child = listItem.childNodes[j] ;
				if ( child.nodeName.IEquals( ['ul', 'ol'] ) )


					this.ListToArray( child, markerObj, baseArray, baseIndentLevel + 1, itemObj.grandparent ) ;
				else
					itemObj.contents.push( child ) ;
			}
		}
		return baseArray ;
	},


	ArrayToList : function( listArray, markerObj, baseIndex )
	{
		if ( baseIndex == undefined )
			baseIndex = 0 ;
		if ( ! listArray || listArray.length < baseIndex + 1 )
			return null ;
		var doc = FCKTools.GetElementDocument( listArray[baseIndex].parent ) ;
		var retval = doc.createDocumentFragment() ;
		var rootNode = null ;
		var currentIndex = baseIndex ;
		var indentLevel = Math.max( listArray[baseIndex].indent, 0 ) ;
		var currentListItem = null ;
		while ( true )
		{
			var item = listArray[currentIndex] ;
			if ( item.indent == indentLevel )
			{
				if ( ! rootNode || listArray[currentIndex].parent.nodeName != rootNode.nodeName )
				{
					rootNode = listArray[currentIndex].parent.cloneNode( false ) ;
					retval.appendChild( rootNode ) ;
				}
				currentListItem = doc.createElement( 'li' ) ;
				rootNode.appendChild( currentListItem ) ;
				for ( var i = 0 ; i < item.contents.length ; i++ )
					currentListItem.appendChild( item.contents[i].cloneNode( true ) ) ;
				currentIndex++ ;
			}
			else if ( item.indent == Math.max( indentLevel, 0 ) + 1 )
			{
				var listData = this.ArrayToList( listArray, null, currentIndex ) ;
				currentListItem.appendChild( listData.listNode ) ;
				currentIndex = listData.nextIndex ;
			}
			else if ( item.indent == -1 && baseIndex == 0 && item.grandparent )
			{
				var currentListItem ;
				if ( item.grandparent.nodeName.IEquals( ['ul', 'ol'] ) )
					currentListItem = doc.createElement( 'li' ) ;
				else
				{
					if ( FCKConfig.EnterMode.IEquals( ['div', 'p'] ) && ! item.grandparent.nodeName.IEquals( 'td' ) )
						currentListItem = doc.createElement( FCKConfig.EnterMode ) ;
					else
						currentListItem = doc.createDocumentFragment() ;
				}
				for ( var i = 0 ; i < item.contents.length ; i++ )
					currentListItem.appendChild( item.contents[i].cloneNode( true ) ) ;
				if ( currentListItem.nodeType == 11 )
				{
					if ( currentListItem.lastChild &&
							currentListItem.lastChild.getAttribute &&
							currentListItem.lastChild.getAttribute( 'type' ) == '_moz' )
						currentListItem.removeChild( currentListItem.lastChild );
					currentListItem.appendChild( doc.createElement( 'br' ) ) ;
				}
				if ( currentListItem.nodeName.IEquals( FCKConfig.EnterMode ) && currentListItem.firstChild )
				{
					this.TrimNode( currentListItem ) ;
					if ( FCKListsLib.BlockBoundaries[currentListItem.firstChild.nodeName.toLowerCase()] )
					{
						var tmp = doc.createDocumentFragment() ;
						while ( currentListItem.firstChild )
							tmp.appendChild( currentListItem.removeChild( currentListItem.firstChild ) ) ;
						currentListItem = tmp ;
					}
				}
				if ( FCKBrowserInfo.IsGeckoLike && currentListItem.nodeName.IEquals( ['div', 'p'] ) )
					FCKTools.AppendBogusBr( currentListItem ) ;
				retval.appendChild( currentListItem ) ;
				rootNode = null ;
				currentIndex++ ;
			}
			else
				return null ;

			if ( listArray.length <= currentIndex || Math.max( listArray[currentIndex].indent, 0 ) < indentLevel )
			{
				break ;
			}
		}


		if ( markerObj )
		{
			var currentNode = retval.firstChild ;
			while ( currentNode )
			{
				if ( currentNode.nodeType == 1 )
					this.ClearElementMarkers( markerObj, currentNode ) ;
				currentNode = this.GetNextSourceNode( currentNode ) ;
			}
		}

		return { 'listNode' : retval, 'nextIndex' : currentIndex } ;
	},

	GetNextSibling : function( node, includeEmpties )
	{
		node = node.nextSibling ;

		while ( node && !includeEmpties && node.nodeType != 1 && ( node.nodeType != 3 || node.nodeValue.length == 0 ) )
			node = node.nextSibling ;

		return node ;
	},

	GetPreviousSibling : function( node, includeEmpties )
	{
		node = node.previousSibling ;

		while ( node && !includeEmpties && node.nodeType != 1 && ( node.nodeType != 3 || node.nodeValue.length == 0 ) )
			node = node.previousSibling ;

		return node ;
	},

	CheckIsEmptyElement : function( element, elementCheckCallback )
	{
		var child = element.firstChild ;
		var elementChild ;

		while ( child )
		{
			if ( child.nodeType == 1 )
			{
				if ( elementChild || !FCKListsLib.InlineNonEmptyElements[ child.nodeName.toLowerCase() ] )
					return false ;

				if ( !elementCheckCallback || elementCheckCallback( child ) === true )
					elementChild = child ;
			}
			else if ( child.nodeType == 3 && child.nodeValue.length > 0 )
				return false ;

			child = child.nextSibling ;
		}

		return elementChild ? this.CheckIsEmptyElement( elementChild, elementCheckCallback ) : true ;
	},

	SetElementStyles : function( element, styleDict )
	{
		var style = element.style ;
		for ( var styleName in styleDict )
			style[ styleName ] = styleDict[ styleName ] ;
	},

	SetOpacity : function( element, opacity )
	{
		if ( FCKBrowserInfo.IsIE )
		{
			opacity = Math.round( opacity * 100 ) ;
			element.style.filter = ( opacity > 100 ? '' : 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')' ) ;
		}
		else
			element.style.opacity = opacity ;
	},

	GetCurrentElementStyle : function( element, propertyName )
	{
		if ( FCKBrowserInfo.IsIE )
			return element.currentStyle[ propertyName ] ;
		else
			return element.ownerDocument.defaultView.getComputedStyle( element, '' ).getPropertyValue( propertyName ) ;
	},

	GetPositionedAncestor : function( element )
	{
		var currentElement = element ;

		while ( currentElement != FCKTools.GetElementDocument( currentElement ).documentElement )
		{
			if ( this.GetCurrentElementStyle( currentElement, 'position' ) != 'static' )
				return currentElement ;

			if ( currentElement == FCKTools.GetElementDocument( currentElement ).documentElement
					&& currentWindow != w )
				currentElement = currentWindow.frameElement ;
			else
				currentElement = currentElement.parentNode ;
		}

		return null ;
	},

	ScrollIntoView : function( element, alignTop )
	{

		var window = FCKTools.GetElementWindow( element ) ;
		var windowHeight = FCKTools.GetViewPaneSize( window ).Height ;



		var offset = windowHeight * -1 ;


		if ( alignTop === false )
		{
			offset += element.offsetHeight || 0 ;




			offset += parseInt( this.GetCurrentElementStyle( element, 'marginBottom' ) || 0, 10 ) || 0 ;
		}


		var elementPosition = FCKTools.GetDocumentPosition( window, element ) ;
		offset += elementPosition.y ;


		var currentScroll = FCKTools.GetScrollPosition( window ).Y ;
		if ( offset > 0 && ( offset > currentScroll || offset < currentScroll - windowHeight ) )
			window.scrollTo( 0, offset ) ;
	},

	CheckIsEditable : function( element )
	{

		var nodeName = element.nodeName.toLowerCase() ;


		var childDTD = FCK.DTD[ nodeName ] || FCK.DTD.span ;


		return ( childDTD['#'] && !FCKListsLib.NonEditableElements[ nodeName ] ) ;
	},

	GetSelectedDivContainers : function()
	{
		var currentBlocks = [] ;
		var range = new FCKDomRange( FCK.EditorWindow ) ;
		range.MoveToSelection() ;

		var startNode = range.GetTouchedStartNode() ;
		var endNode = range.GetTouchedEndNode() ;
		var currentNode = startNode ;

		if ( startNode == endNode )
		{
			while ( endNode.nodeType == 1 && endNode.lastChild )
				endNode = endNode.lastChild ;
			endNode = FCKDomTools.GetNextSourceNode( endNode ) ;
		}

		while ( currentNode && currentNode != endNode )
		{
			if ( currentNode.nodeType != 3 || !/^[ \t\n]*$/.test( currentNode.nodeValue ) )
			{
				var path = new FCKElementPath( currentNode ) ;
				var blockLimit = path.BlockLimit ;
				if ( blockLimit && blockLimit.nodeName.IEquals( 'div' ) && currentBlocks.IndexOf( blockLimit ) == -1 )
					currentBlocks.push( blockLimit ) ;
			}

			currentNode = FCKDomTools.GetNextSourceNode( currentNode ) ;
		}

		return currentBlocks ;
	}
} ;