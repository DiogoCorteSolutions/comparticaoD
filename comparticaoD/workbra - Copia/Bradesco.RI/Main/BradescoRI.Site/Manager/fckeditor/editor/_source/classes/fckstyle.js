var FCKStyle = function( styleDesc )
{
	this.Element = ( styleDesc.Element || 'span' ).toLowerCase() ;
	this._StyleDesc = styleDesc ;
}

FCKStyle.prototype =
{
	GetType : function()
	{
		var type = this.GetType_$ ;

		if ( type != undefined )
			return type ;

		var elementName = this.Element ;

		if ( elementName == '#' || FCKListsLib.StyleBlockElements[ elementName ] )
			type = FCK_STYLE_BLOCK ;
		else if ( FCKListsLib.StyleObjectElements[ elementName ] )
			type = FCK_STYLE_OBJECT ;
		else
			type = FCK_STYLE_INLINE ;

		return ( this.GetType_$ = type ) ;
	},

	ApplyToSelection : function( targetWindow )
	{
		var range = new FCKDomRange( targetWindow ) ;
		range.MoveToSelection() ;

		this.ApplyToRange( range, true ) ;
	},

	ApplyToRange : function( range, selectIt, updateRange )
	{
		switch ( this.GetType() )
		{
			case FCK_STYLE_BLOCK :
				this.ApplyToRange = this._ApplyBlockStyle ;
				break ;
			case FCK_STYLE_INLINE :
				this.ApplyToRange = this._ApplyInlineStyle ;
				break ;
			default :
				return ;
		}

		this.ApplyToRange( range, selectIt, updateRange ) ;
	},

	ApplyToObject : function( objectElement )
	{
		if ( !objectElement )
			return ;

		this.BuildElement( null, objectElement ) ;
	},

	RemoveFromSelection : function( targetWindow )
	{
		var range = new FCKDomRange( targetWindow ) ;
		range.MoveToSelection() ;

		this.RemoveFromRange( range, true ) ;
	},

	RemoveFromRange : function( range, selectIt, updateRange )
	{
		var bookmark ;

		var styleAttribs = this._GetAttribsForComparison() ;
		var styleOverrides = this._GetOverridesForComparison() ;

		if ( range.CheckIsCollapsed() )
		{
			var bookmark = range.CreateBookmark( true ) ;

			var bookmarkStart = range.GetBookmarkNode( bookmark, true ) ;

			var path = new FCKElementPath( bookmarkStart.parentNode ) ;

			var boundaryElements = [] ;

			var isBoundaryRight = !FCKDomTools.GetNextSibling( bookmarkStart ) ;
			var isBoundary = isBoundaryRight || !FCKDomTools.GetPreviousSibling( bookmarkStart ) ;

			var lastBoundaryElement ;
			var boundaryLimitIndex = -1 ;

			for ( var i = 0 ; i < path.Elements.length ; i++ )
			{
				var pathElement = path.Elements[i] ;
				if ( this.CheckElementRemovable( pathElement ) )
				{
					if ( isBoundary
						&& !FCKDomTools.CheckIsEmptyElement( pathElement,
								function( el )
								{
									return ( el != bookmarkStart ) ;
								} )
						)
					{
						lastBoundaryElement = pathElement ;

						boundaryLimitIndex = boundaryElements.length - 1 ;
					}
					else
					{
						var pathElementName = pathElement.nodeName.toLowerCase() ;

						if ( pathElementName == this.Element )
						{
							for ( var att in styleAttribs )
							{
								if ( FCKDomTools.HasAttribute( pathElement, att ) )
								{
									switch ( att )
									{
										case 'style' :
											this._RemoveStylesFromElement( pathElement ) ;
											break ;

										case 'class' :
											if ( FCKDomTools.GetAttributeValue( pathElement, att ) != this.GetFinalAttributeValue( att ) )
												continue ;

										default :
											FCKDomTools.RemoveAttribute( pathElement, att ) ;
									}
								}
							}
						}

						this._RemoveOverrides( pathElement, styleOverrides[ pathElementName ] ) ;

						if ( this.GetType() == FCK_STYLE_INLINE)
							this._RemoveNoAttribElement( pathElement ) ;
					}
				}
				else if ( isBoundary )
					boundaryElements.push( pathElement ) ;

				isBoundary = isBoundary && ( ( isBoundaryRight && !FCKDomTools.GetNextSibling( pathElement ) ) || ( !isBoundaryRight && !FCKDomTools.GetPreviousSibling( pathElement ) ) ) ;

				if ( lastBoundaryElement && ( !isBoundary || ( i == path.Elements.length - 1 ) ) )
				{
					var currentElement = FCKDomTools.RemoveNode( bookmarkStart ) ;

					for ( var j = 0 ; j <= boundaryLimitIndex ; j++ )
					{
						var newElement = FCKDomTools.CloneElement( boundaryElements[j] ) ;
						newElement.appendChild( currentElement ) ;
						currentElement = newElement ;
					}

					if ( isBoundaryRight )
						FCKDomTools.InsertAfterNode( lastBoundaryElement, currentElement ) ;
					else
						lastBoundaryElement.parentNode.insertBefore( currentElement, lastBoundaryElement ) ;

					isBoundary = false ;
					lastBoundaryElement = null ;
				}
			}

			if ( selectIt )
				range.SelectBookmark( bookmark ) ;

			if ( updateRange )
				range.MoveToBookmark( bookmark ) ;

			return ;
		}

		range.Expand( 'inline_elements' ) ;

		bookmark = range.CreateBookmark( true ) ;

		var startNode	= range.GetBookmarkNode( bookmark, true ) ;
		var endNode		= range.GetBookmarkNode( bookmark, false ) ;

		range.Release( true ) ;

		var path = new FCKElementPath( startNode ) ;
		var pathElements = path.Elements ;
		var pathElement ;

		for ( var i = 1 ; i < pathElements.length ; i++ )
		{
			pathElement = pathElements[i] ;

			if ( pathElement == path.Block || pathElement == path.BlockLimit )
				break ;

			if ( this.CheckElementRemovable( pathElement ) )
				FCKDomTools.BreakParent( startNode, pathElement, range ) ;
		}

		path = new FCKElementPath( endNode ) ;
		pathElements = path.Elements ;

		for ( var i = 1 ; i < pathElements.length ; i++ )
		{
			pathElement = pathElements[i] ;

			if ( pathElement == path.Block || pathElement == path.BlockLimit )
				break ;

			elementName = pathElement.nodeName.toLowerCase() ;

			if ( this.CheckElementRemovable( pathElement ) )
				FCKDomTools.BreakParent( endNode, pathElement, range ) ;
		}

		var currentNode = FCKDomTools.GetNextSourceNode( startNode, true ) ;

		while ( currentNode )
		{
			var nextNode = FCKDomTools.GetNextSourceNode( currentNode ) ;

			if ( currentNode.nodeType == 1 )
			{
				var elementName = currentNode.nodeName.toLowerCase() ;

				var mayRemove = ( elementName == this.Element ) ;
				if ( mayRemove )
				{
					for ( var att in styleAttribs )
					{
						if ( FCKDomTools.HasAttribute( currentNode, att ) )
						{
							switch ( att )
							{
								case 'style' :
									this._RemoveStylesFromElement( currentNode ) ;
									break ;

								case 'class' :
									if ( FCKDomTools.GetAttributeValue( currentNode, att ) != this.GetFinalAttributeValue( att ) )
										continue ;

								default :
									FCKDomTools.RemoveAttribute( currentNode, att ) ;
							}
						}
					}
				}
				else
					mayRemove = !!styleOverrides[ elementName ] ;

				if ( mayRemove )
				{
					this._RemoveOverrides( currentNode, styleOverrides[ elementName ] ) ;

					this._RemoveNoAttribElement( currentNode ) ;
				}
			}

			if ( nextNode == endNode )
				break ;

			currentNode = nextNode ;
		}

		this._FixBookmarkStart( startNode ) ;

		if ( selectIt )
			range.SelectBookmark( bookmark ) ;

		if ( updateRange )
			range.MoveToBookmark( bookmark ) ;
	},

	CheckElementRemovable : function( element, fullMatch )
	{
		if ( !element )
			return false ;

		var elementName = element.nodeName.toLowerCase() ;

		if ( elementName == this.Element )
		{
			if ( !fullMatch && !FCKDomTools.HasAttributes( element ) )
				return true ;

			var attribs = this._GetAttribsForComparison() ;
			var allMatched = ( attribs._length == 0 ) ;
			for ( var att in attribs )
			{
				if ( att == '_length' )
					continue ;

				if ( this._CompareAttributeValues( att, FCKDomTools.GetAttributeValue( element, att ), ( this.GetFinalAttributeValue( att ) || '' ) ) )
				{
					allMatched = true ;
					if ( !fullMatch )
						break ;
				}
				else
				{
					allMatched = false ;
					if ( fullMatch )
						return false ;
				}
			}
			if ( allMatched )
				return true ;
		}

		var override = this._GetOverridesForComparison()[ elementName ] ;
		if ( override )
		{
			if ( !( attribs = override.Attributes ) )
				return true ;

			for ( var i = 0 ; i < attribs.length ; i++ )
			{
				var attName = attribs[i][0] ;
				if ( FCKDomTools.HasAttribute( element, attName ) )
				{
					var attValue = attribs[i][1] ;

					if ( attValue == null ||
							( typeof attValue == 'string' && FCKDomTools.GetAttributeValue( element, attName ) == attValue ) ||
							attValue.test( FCKDomTools.GetAttributeValue( element, attName ) ) )
						return true ;
				}
			}
		}

		return false ;
	},

	CheckActive : function( elementPath )
	{
		switch ( this.GetType() )
		{
			case FCK_STYLE_BLOCK :
				return this.CheckElementRemovable( elementPath.Block || elementPath.BlockLimit, true ) ;

			case FCK_STYLE_INLINE :

				var elements = elementPath.Elements ;

				for ( var i = 0 ; i < elements.length ; i++ )
				{
					var element = elements[i] ;

					if ( element == elementPath.Block || element == elementPath.BlockLimit )
						continue ;

					if ( this.CheckElementRemovable( element, true ) )
						return true ;
				}
		}
		return false ;
	},

	RemoveFromElement : function( element )
	{
		var attribs = this._GetAttribsForComparison() ;
		var overrides = this._GetOverridesForComparison() ;

		var innerElements = element.getElementsByTagName( this.Element ) ;

		for ( var i = innerElements.length - 1 ; i >= 0 ; i-- )
		{
			var innerElement = innerElements[i] ;

			for ( var att in attribs )
			{
				if ( FCKDomTools.HasAttribute( innerElement, att ) )
				{
					switch ( att )
					{
						case 'style' :
							this._RemoveStylesFromElement( innerElement ) ;
							break ;

						case 'class' :
							if ( FCKDomTools.GetAttributeValue( innerElement, att ) != this.GetFinalAttributeValue( att ) )
								continue ;

						default :
							FCKDomTools.RemoveAttribute( innerElement, att ) ;
					}
				}
			}

			this._RemoveOverrides( innerElement, overrides[ this.Element ] ) ;

			this._RemoveNoAttribElement( innerElement ) ;
		}

		for ( var overrideElement in overrides )
		{
			if ( overrideElement != this.Element )
			{
				innerElements = element.getElementsByTagName( overrideElement ) ;

				for ( var i = innerElements.length - 1 ; i >= 0 ; i-- )
				{
					var innerElement = innerElements[i] ;
					this._RemoveOverrides( innerElement, overrides[ overrideElement ] ) ;
					this._RemoveNoAttribElement( innerElement ) ;
				}
			}
		}
	},

	_RemoveStylesFromElement : function( element )
	{
		var elementStyle = element.style.cssText ;
		var pattern = this.GetFinalStyleValue() ;

		if ( elementStyle.length > 0 && pattern.length == 0 )
			return ;

		pattern = '(^|;)\\s*(' +
			pattern.replace( /\s*([^ ]+):.*?(;|$)/g, '$1|' ).replace( /\|$/, '' ) +
			'):[^;]+' ;

		var regex = new RegExp( pattern, 'gi' ) ;

		elementStyle = elementStyle.replace( regex, '' ).Trim() ;

		if ( elementStyle.length == 0 || elementStyle == ';' )
			FCKDomTools.RemoveAttribute( element, 'style' ) ;
		else
			element.style.cssText = elementStyle.replace( regex, '' ) ;
	},

	_RemoveOverrides : function( element, override )
	{
		var attributes = override && override.Attributes ;

		if ( attributes )
		{
			for ( var i = 0 ; i < attributes.length ; i++ )
			{
				var attName = attributes[i][0] ;

				if ( FCKDomTools.HasAttribute( element, attName ) )
				{
					var attValue	= attributes[i][1] ;

					if ( attValue == null ||
							( attValue.test && attValue.test( FCKDomTools.GetAttributeValue( element, attName ) ) ) ||
							( typeof attValue == 'string' && FCKDomTools.GetAttributeValue( element, attName ) == attValue ) )
						FCKDomTools.RemoveAttribute( element, attName ) ;
				}
			}
		}
	},

	_RemoveNoAttribElement : function( element )
	{
		if ( !FCKDomTools.HasAttributes( element ) )
		{
			var firstChild	= element.firstChild ;
			var lastChild	= element.lastChild ;

			FCKDomTools.RemoveNode( element, true ) ;

			this._MergeSiblings( firstChild ) ;

			if ( firstChild != lastChild )
				this._MergeSiblings( lastChild ) ;
		}
	},

	BuildElement : function( targetDoc, element )
	{
		var el = element || targetDoc.createElement( this.Element ) ;

		var attribs	= this._StyleDesc.Attributes ;
		var attValue ;
		if ( attribs )
		{
			for ( var att in attribs )
			{
				attValue = this.GetFinalAttributeValue( att ) ;

				if ( att.toLowerCase() == 'class' )
					el.className = attValue ;
				else
					el.setAttribute( att, attValue ) ;
			}
		}

		if ( this._GetStyleText().length > 0 )
			el.style.cssText = this.GetFinalStyleValue() ;

		return el ;
	},

	_CompareAttributeValues : function( attName, valueA, valueB )
	{
		if ( attName == 'style' && valueA && valueB )
		{
			valueA = valueA.replace( /;$/, '' ).toLowerCase() ;
			valueB = valueB.replace( /;$/, '' ).toLowerCase() ;
		}

		return ( valueA == valueB || ( ( valueA === null || valueA === '' ) && ( valueB === null || valueB === '' ) ) )
	},

	GetFinalAttributeValue : function( attName )
	{
		var attValue = this._StyleDesc.Attributes ;
		var attValue = attValue ? attValue[ attName ] : null ;

		if ( !attValue && attName == 'style' )
			return this.GetFinalStyleValue() ;

		if ( attValue && this._Variables )
			attValue = attValue.Replace( FCKRegexLib.StyleVariableAttName, this._GetVariableReplace, this ) ;

		return attValue ;
	},

	GetFinalStyleValue : function()
	{
		var attValue = this._GetStyleText() ;

		if ( attValue.length > 0 && this._Variables )
		{
			attValue = attValue.Replace( FCKRegexLib.StyleVariableAttName, this._GetVariableReplace, this ) ;
			attValue = FCKTools.NormalizeCssText( attValue ) ;
		}

		return attValue ;
	},

	_GetVariableReplace : function()
	{
		return this._Variables[ arguments[2] ] || arguments[0] ;
	},

	SetVariable : function( name, value )
	{
		var variables = this._Variables ;

		if ( !variables )
			variables = this._Variables = {} ;

		this._Variables[ name ] = value ;
	},

	_FromPre : function( doc, block, newBlock )
	{
		var innerHTML = block.innerHTML ;

		innerHTML = innerHTML.replace( /(\r\n|\r)/g, '\n' ) ;
		innerHTML = innerHTML.replace( /^[ \t]*\n/, '' ) ;
		innerHTML = innerHTML.replace( /\n$/, '' ) ;

		innerHTML = innerHTML.replace( /^[ \t]+|[ \t]+$/g, function( match, offset, s )
				{
					if ( match.length == 1 )
						return '&nbsp;' ;
					else if ( offset == 0 )
						return new Array( match.length ).join( '&nbsp;' ) + ' ' ;
					else
						return ' ' + new Array( match.length ).join( '&nbsp;' ) ;
				} ) ;

		var htmlIterator = new FCKHtmlIterator( innerHTML ) ;
		var results = [] ;
		htmlIterator.Each( function( isTag, value )
			{
				if ( !isTag )
				{
					value = value.replace( /\n/g, '<br>' ) ;
					value = value.replace( /[ \t]{2,}/g,
							function ( match )
							{
								return new Array( match.length ).join( '&nbsp;' ) + ' ' ;
							} ) ;
				}
				results.push( value ) ;
			} ) ;
		newBlock.innerHTML = results.join( '' ) ;
		return newBlock ;
	},

	_ToPre : function( doc, block, newBlock )
	{
		var innerHTML = block.innerHTML.Trim() ;

		innerHTML = innerHTML.replace( /[ \t\r\n]*(<br[^>]*>)[ \t\r\n]*/gi, '<br />' ) ;

		var htmlIterator = new FCKHtmlIterator( innerHTML ) ;
		var results = [] ;
		htmlIterator.Each( function( isTag, value )
			{
				if ( !isTag )
					value = value.replace( /([ \t\n\r]+|&nbsp;)/g, ' ' ) ;
				else if ( isTag && value == '<br />' )
					value = '\n' ;
				results.push( value ) ;
			} ) ;

		if ( FCKBrowserInfo.IsIE )
		{
			var temp = doc.createElement( 'div' ) ;
			temp.appendChild( newBlock ) ;
			newBlock.outerHTML = '<pre>\n' + results.join( '' ) + '</pre>' ;
			newBlock = temp.removeChild( temp.firstChild ) ;
		}
		else
			newBlock.innerHTML = results.join( '' ) ;

		return newBlock ;
	},

	_CheckAndMergePre : function( previousBlock, preBlock )
	{
		if ( previousBlock != FCKDomTools.GetPreviousSourceElement( preBlock, true ) )
			return ;

		var innerHTML = previousBlock.innerHTML.replace( /\n$/, '' ) + '\n\n' +
				preBlock.innerHTML.replace( /^\n/, '' ) ;

		if ( FCKBrowserInfo.IsIE )
			preBlock.outerHTML = '<pre>' + innerHTML + '</pre>' ;
		else
			preBlock.innerHTML = innerHTML ;

		FCKDomTools.RemoveNode( previousBlock ) ;
	},

	_CheckAndSplitPre : function( newBlock )
	{
		var lastNewBlock ;

		var cursor = newBlock.firstChild ;

		cursor = cursor && cursor.nextSibling ;

		while ( cursor )
		{
			var next = cursor.nextSibling ;

			if ( next && next.nextSibling && cursor.nodeName.IEquals( 'br' ) && next.nodeName.IEquals( 'br' ) )
			{
				FCKDomTools.RemoveNode( cursor ) ;

				cursor = next.nextSibling ;

				FCKDomTools.RemoveNode( next ) ;

				lastNewBlock = FCKDomTools.InsertAfterNode( lastNewBlock || newBlock, FCKDomTools.CloneElement( newBlock ) ) ;

				continue ;
			}

			if ( lastNewBlock )
			{
				cursor = cursor.previousSibling ;
				FCKDomTools.MoveNode(cursor.nextSibling, lastNewBlock ) ;
			}

			cursor = cursor.nextSibling ;
		}
	},

	_ApplyBlockStyle : function( range, selectIt, updateRange )
	{
		var bookmark ;

		if ( selectIt )
			bookmark = range.CreateBookmark() ;

		var iterator = new FCKDomRangeIterator( range ) ;
		iterator.EnforceRealBlocks = true ;

		var block ;
		var doc = range.Window.document ;
		var previousPreBlock ;

		while( ( block = iterator.GetNextParagraph() ) )
		{
			var newBlock = this.BuildElement( doc ) ;

			var newBlockIsPre	= newBlock.nodeName.IEquals( 'pre' ) ;
			var blockIsPre		= block.nodeName.IEquals( 'pre' ) ;

			var toPre	= newBlockIsPre && !blockIsPre ;
			var fromPre	= !newBlockIsPre && blockIsPre ;

			if ( toPre )
				newBlock = this._ToPre( doc, block, newBlock ) ;
			else if ( fromPre )
				newBlock = this._FromPre( doc, block, newBlock ) ;
			else
				FCKDomTools.MoveChildren( block, newBlock ) ;

			block.parentNode.insertBefore( newBlock, block ) ;
			FCKDomTools.RemoveNode( block ) ;

			if ( newBlockIsPre )
			{
				if ( previousPreBlock )
					this._CheckAndMergePre( previousPreBlock, newBlock ) ;
				previousPreBlock = newBlock ;
			}
			else if ( fromPre )
				this._CheckAndSplitPre( newBlock ) ;
		}

		if ( selectIt )
			range.SelectBookmark( bookmark ) ;

		if ( updateRange )
			range.MoveToBookmark( bookmark ) ;
	},

	_ApplyInlineStyle : function( range, selectIt, updateRange )
	{
		var doc = range.Window.document ;

		if ( range.CheckIsCollapsed() )
		{
			var collapsedElement = this.BuildElement( doc ) ;
			range.InsertNode( collapsedElement ) ;
			range.MoveToPosition( collapsedElement, 2 ) ;
			range.Select() ;

			return ;
		}

		var elementName = this.Element ;

		var elementDTD = FCK.DTD[ elementName ] || FCK.DTD.span ;

		var styleAttribs = this._GetAttribsForComparison() ;
		var styleNode ;

		range.Expand( 'inline_elements' ) ;


		var bookmark = range.CreateBookmark( true ) ;


		var startNode	= range.GetBookmarkNode( bookmark, true ) ;
		var endNode		= range.GetBookmarkNode( bookmark, false ) ;



		range.Release( true ) ;



		var currentNode = FCKDomTools.GetNextSourceNode( startNode, true ) ;

		while ( currentNode )
		{
			var applyStyle = false ;

			var nodeType = currentNode.nodeType ;
			var nodeName = nodeType == 1 ? currentNode.nodeName.toLowerCase() : null ;


			if ( !nodeName || elementDTD[ nodeName ] )
			{


				if ( ( FCK.DTD[ currentNode.parentNode.nodeName.toLowerCase() ] || FCK.DTD.span )[ elementName ] || !FCK.DTD[ elementName ] )
				{


					if ( !range.CheckHasRange() )
						range.SetStart( currentNode, 3 ) ;



					if ( nodeType != 1 || currentNode.childNodes.length == 0 )
					{
						var includedNode = currentNode ;
						var parentNode = includedNode.parentNode ;





						while ( includedNode == parentNode.lastChild
							&& elementDTD[ parentNode.nodeName.toLowerCase() ] )
						{
							includedNode = parentNode ;
						}

						range.SetEnd( includedNode, 4 ) ;




						if ( includedNode == includedNode.parentNode.lastChild && !elementDTD[ includedNode.parentNode.nodeName.toLowerCase() ] )
							applyStyle = true ;
					}
					else
					{




						range.SetEnd( currentNode, 3 ) ;
					}
				}
				else
					applyStyle = true ;
			}
			else
				applyStyle = true ;


			currentNode = FCKDomTools.GetNextSourceNode( currentNode ) ;



			if ( currentNode == endNode )
			{
				currentNode = null ;
				applyStyle = true ;
			}


			if ( applyStyle && range.CheckHasRange() && !range.CheckIsCollapsed() )
			{

				styleNode = this.BuildElement( doc ) ;


				range.ExtractContents().AppendTo( styleNode ) ;


				if ( styleNode.innerHTML.RTrim().length > 0 )
				{


					range.InsertNode( styleNode ) ;



					this.RemoveFromElement( styleNode ) ;


					this._MergeSiblings( styleNode, this._GetAttribsForComparison() ) ;







					if ( !FCKBrowserInfo.IsIE )
						styleNode.normalize() ;
				}



				range.Release( true ) ;
			}
		}

		this._FixBookmarkStart( startNode ) ;


		if ( selectIt )
			range.SelectBookmark( bookmark ) ;

		if ( updateRange )
			range.MoveToBookmark( bookmark ) ;
	},

	_FixBookmarkStart : function( startNode )
	{



		var startSibling ;
		while ( ( startSibling = startNode.nextSibling ) )
		{
			if ( startSibling.nodeType == 1
				&& FCKListsLib.InlineNonEmptyElements[ startSibling.nodeName.toLowerCase() ] )
			{

				if ( !startSibling.firstChild )
					FCKDomTools.RemoveNode( startSibling ) ;
				else
					FCKDomTools.MoveNode( startNode, startSibling, true ) ;
				continue ;
			}


			if ( startSibling.nodeType == 3 && startSibling.length == 0 )
			{
				FCKDomTools.RemoveNode( startSibling ) ;
				continue ;
			}

			break ;
		}
	},

	_MergeSiblings : function( element, attribs )
	{
		if ( !element || element.nodeType != 1 || !FCKListsLib.InlineNonEmptyElements[ element.nodeName.toLowerCase() ] )
			return ;

		this._MergeNextSibling( element, attribs ) ;
		this._MergePreviousSibling( element, attribs ) ;
	},

	_MergeNextSibling : function( element, attribs )
	{

		var sibling = element.nextSibling ;


		var hasBookmark = ( sibling && sibling.nodeType == 1 && sibling.getAttribute( '_fck_bookmark' ) ) ;
		if ( hasBookmark )
			sibling = sibling.nextSibling ;

		if ( sibling && sibling.nodeType == 1 && sibling.nodeName == element.nodeName )
		{
			if ( !attribs )
				attribs = this._CreateElementAttribsForComparison( element ) ;

			if ( this._CheckAttributesMatch( sibling, attribs ) )
			{

				var innerSibling = element.lastChild ;

				if ( hasBookmark )
					FCKDomTools.MoveNode( element.nextSibling, element ) ;


				FCKDomTools.MoveChildren( sibling, element ) ;
				FCKDomTools.RemoveNode( sibling ) ;


				if ( innerSibling )
					this._MergeNextSibling( innerSibling ) ;
			}
		}
	},

	_MergePreviousSibling : function( element, attribs )
	{

		var sibling = element.previousSibling ;


		var hasBookmark = ( sibling && sibling.nodeType == 1 && sibling.getAttribute( '_fck_bookmark' ) ) ;
		if ( hasBookmark )
			sibling = sibling.previousSibling ;

		if ( sibling && sibling.nodeType == 1 && sibling.nodeName == element.nodeName )
		{
			if ( !attribs )
				attribs = this._CreateElementAttribsForComparison( element ) ;

			if ( this._CheckAttributesMatch( sibling, attribs ) )
			{

				var innerSibling = element.firstChild ;

				if ( hasBookmark )
					FCKDomTools.MoveNode( element.previousSibling, element, true ) ;


				FCKDomTools.MoveChildren( sibling, element, true ) ;
				FCKDomTools.RemoveNode( sibling ) ;


				if ( innerSibling )
					this._MergePreviousSibling( innerSibling ) ;
			}
		}
	},

	_GetStyleText : function()
	{
		var stylesDef = this._StyleDesc.Styles ;


		var stylesText = ( this._StyleDesc.Attributes ? this._StyleDesc.Attributes['style'] || '' : '' ) ;

		if ( stylesText.length > 0 )
			stylesText += ';' ;

		for ( var style in stylesDef )
			stylesText += style + ':' + stylesDef[style] + ';' ;




		if ( stylesText.length > 0 && !( /#\(/.test( stylesText ) ) )
		{
			stylesText = FCKTools.NormalizeCssText( stylesText ) ;
		}

		return (this._GetStyleText = function() { return stylesText ; })() ;
	},

	_GetAttribsForComparison : function()
	{

		var attribs = this._GetAttribsForComparison_$ ;
		if ( attribs )
			return attribs ;

		attribs = new Object() ;


		var styleAttribs = this._StyleDesc.Attributes ;
		if ( styleAttribs )
		{
			for ( var styleAtt in styleAttribs )
			{
				attribs[ styleAtt.toLowerCase() ] = styleAttribs[ styleAtt ].toLowerCase() ;
			}
		}


		if ( this._GetStyleText().length > 0 )
		{
			attribs['style'] = this._GetStyleText().toLowerCase() ;
		}


		FCKTools.AppendLengthProperty( attribs, '_length' ) ;


		return ( this._GetAttribsForComparison_$ = attribs ) ;
	},

	_GetOverridesForComparison : function()
	{

		var overrides = this._GetOverridesForComparison_$ ;
		if ( overrides )
			return overrides ;

		overrides = new Object() ;

		var overridesDesc = this._StyleDesc.Overrides ;

		if ( overridesDesc )
		{


			if ( !FCKTools.IsArray( overridesDesc ) )
				overridesDesc = [ overridesDesc ] ;


			for ( var i = 0 ; i < overridesDesc.length ; i++ )
			{
				var override = overridesDesc[i] ;
				var elementName ;
				var overrideEl ;
				var attrs ;


				if ( typeof override == 'string' )
					elementName = override.toLowerCase() ;

				else
				{
					elementName = override.Element ? override.Element.toLowerCase() : this.Element ;
					attrs = override.Attributes ;
				}




				overrideEl = overrides[ elementName ] || ( overrides[ elementName ] = {} ) ;

				if ( attrs )
				{



					var overrideAttrs = ( overrideEl.Attributes = overrideEl.Attributes || new Array() ) ;
					for ( var attName in attrs )
					{



						overrideAttrs.push( [ attName.toLowerCase(), attrs[ attName ] ] ) ;
					}
				}
			}
		}

		return ( this._GetOverridesForComparison_$ = overrides ) ;
	},

	_CreateElementAttribsForComparison : function( element )
	{
		var attribs = new Object() ;
		var attribsCount = 0 ;

		for ( var i = 0 ; i < element.attributes.length ; i++ )
		{
			var att = element.attributes[i] ;

			if ( att.specified )
			{
				attribs[ att.nodeName.toLowerCase() ] = FCKDomTools.GetAttributeValue( element, att ).toLowerCase() ;
				attribsCount++ ;
			}
		}

		attribs._length = attribsCount ;

		return attribs ;
	},

	_CheckAttributesMatch : function( element, styleAttribs )
	{




		var elementAttrbs = element.attributes ;
		var matchCount = 0 ;

		for ( var i = 0 ; i < elementAttrbs.length ; i++ )
		{
			var att = elementAttrbs[i] ;
			if ( att.specified )
			{
				var attName = att.nodeName.toLowerCase() ;
				var styleAtt = styleAttribs[ attName ] ;


				if ( !styleAtt )
					break ;


				if ( styleAtt != FCKDomTools.GetAttributeValue( element, att ).toLowerCase() )
					break ;

				matchCount++ ;
			}
		}

		return ( matchCount == styleAttribs._length ) ;
	}
} ;