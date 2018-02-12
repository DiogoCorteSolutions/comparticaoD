var FCKListHandler =
{
	OutdentListItem : function( listItem )
	{
		var eParent = listItem.parentNode ;


		if ( eParent.tagName.toUpperCase().Equals( 'UL','OL' ) )
		{
			var oDocument = FCKTools.GetElementDocument( listItem ) ;
			var oDogFrag = new FCKDocumentFragment( oDocument ) ;


			var eNextSiblings = oDogFrag.RootNode ;
			var eHasLiSibling = false ;


			var eChildList = FCKDomTools.GetFirstChild( listItem, ['UL','OL'] ) ;
			if ( eChildList )
			{
				eHasLiSibling = true ;

				var eChild ;

				while ( (eChild = eChildList.firstChild) )
					eNextSiblings.appendChild( eChildList.removeChild( eChild ) ) ;

				FCKDomTools.RemoveNode( eChildList ) ;
			}


			var eSibling ;
			var eHasSuccessiveLiSibling = false ;

			while ( (eSibling = listItem.nextSibling) )
			{
				if ( !eHasLiSibling && eSibling.nodeType == 1 && eSibling.nodeName.toUpperCase() == 'LI' )
					eHasSuccessiveLiSibling = eHasLiSibling = true ;

				eNextSiblings.appendChild( eSibling.parentNode.removeChild( eSibling ) ) ;


				if ( !eHasSuccessiveLiSibling && eSibling.nodeType == 1 && eSibling.nodeName.toUpperCase().Equals( 'UL','OL' ) )
					FCKDomTools.RemoveNode( eSibling, true ) ;
			}


			var sParentParentTag = eParent.parentNode.tagName.toUpperCase() ;
			var bWellNested = ( sParentParentTag == 'LI' ) ;
			if ( bWellNested || sParentParentTag.Equals( 'UL','OL' ) )
			{
				if ( eHasLiSibling )
				{
					var eChildList = eParent.cloneNode( false ) ;
					oDogFrag.AppendTo( eChildList ) ;
					listItem.appendChild( eChildList ) ;
				}
				else if ( bWellNested )
					oDogFrag.InsertAfterNode( eParent.parentNode ) ;
				else
					oDogFrag.InsertAfterNode( eParent ) ;


				if ( bWellNested )
					FCKDomTools.InsertAfterNode( eParent.parentNode, eParent.removeChild( listItem ) ) ;
				else
					FCKDomTools.InsertAfterNode( eParent, eParent.removeChild( listItem ) ) ;
			}
			else
			{
				if ( eHasLiSibling )
				{
					var eNextList = eParent.cloneNode( false ) ;
					oDogFrag.AppendTo( eNextList ) ;
					FCKDomTools.InsertAfterNode( eParent, eNextList ) ;
				}

				var eBlock = oDocument.createElement( FCKConfig.EnterMode == 'p' ? 'p' : 'div' ) ;
				FCKDomTools.MoveChildren( eParent.removeChild( listItem ), eBlock ) ;
				FCKDomTools.InsertAfterNode( eParent, eBlock ) ;

				if ( FCKConfig.EnterMode == 'br' )
				{


					if ( FCKBrowserInfo.IsGecko )
						eBlock.parentNode.insertBefore( FCKTools.CreateBogusBR( oDocument ), eBlock ) ;
					else
						FCKDomTools.InsertAfterNode( eBlock, FCKTools.CreateBogusBR( oDocument ) ) ;

					FCKDomTools.RemoveNode( eBlock, true ) ;
				}
			}

			if ( this.CheckEmptyList( eParent ) )
				FCKDomTools.RemoveNode( eParent, true ) ;
		}
	},

	CheckEmptyList : function( listElement )
	{
		return ( FCKDomTools.GetFirstChild( listElement, 'LI' ) == null ) ;
	},


	CheckListHasContents : function( listElement )
	{
		var eChildNode = listElement.firstChild ;

		while ( eChildNode )
		{
			switch ( eChildNode.nodeType )
			{
				case 1 :
					if ( !eChildNode.nodeName.IEquals( 'UL','LI' ) )
						return true ;
					break ;

				case 3 :
					if ( eChildNode.nodeValue.Trim().length > 0 )
						return true ;
			}

			eChildNode = eChildNode.nextSibling ;
		}

		return false ;
	}
} ;