var FCKDocumentFragment = function( parentDocument )
{
	this._Document = parentDocument ;
	this.RootNode = parentDocument.createElement( 'div' ) ;
}

FCKDocumentFragment.prototype =
{

	AppendTo : function( targetNode )
	{
		FCKDomTools.MoveChildren( this.RootNode, targetNode ) ;
	},

	AppendHtml : function( html )
	{
		var eTmpDiv = this._Document.createElement( 'div' ) ;
		eTmpDiv.innerHTML = html ;
		FCKDomTools.MoveChildren( eTmpDiv, this.RootNode ) ;
	},

	InsertAfterNode : function( existingNode )
	{
		var eRoot = this.RootNode ;
		var eLast ;

		while( ( eLast = eRoot.lastChild ) )
			FCKDomTools.InsertAfterNode( existingNode, eRoot.removeChild( eLast ) ) ;
	}
} ;