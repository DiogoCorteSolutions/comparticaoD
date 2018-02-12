var FCKDocumentFragment = function( parentDocument, baseDocFrag )
{
	this.RootNode = baseDocFrag || parentDocument.createDocumentFragment() ;
}

FCKDocumentFragment.prototype =
{
	AppendTo : function( targetNode )
	{
		targetNode.appendChild( this.RootNode ) ;
	},

	AppendHtml : function( html )
	{
		var eTmpDiv = this.RootNode.ownerDocument.createElement( 'div' ) ;
		eTmpDiv.innerHTML = html ;
		FCKDomTools.MoveChildren( eTmpDiv, this.RootNode ) ;
	},

	InsertAfterNode : function( existingNode )
	{
		FCKDomTools.InsertAfterNode( existingNode, this.RootNode ) ;
	}
}