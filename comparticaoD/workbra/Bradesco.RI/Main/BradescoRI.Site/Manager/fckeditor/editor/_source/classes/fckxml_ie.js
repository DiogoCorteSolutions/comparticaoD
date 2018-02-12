FCKXml.prototype =
{
	LoadUrl : function( urlToCall )
	{
		this.Error = false ;

		var oXmlHttp = FCKTools.CreateXmlObject( 'XmlHttp' ) ;

		if ( !oXmlHttp )
		{
			this.Error = true ;
			return ;
		}

		oXmlHttp.open( "GET", urlToCall, false ) ;

		oXmlHttp.send( null ) ;

		if ( oXmlHttp.status == 200 || oXmlHttp.status == 304 || ( oXmlHttp.status == 0 && oXmlHttp.readyState == 4 ) )
		{
			this.DOMDocument = oXmlHttp.responseXML ;



			if ( !this.DOMDocument || this.DOMDocument.firstChild == null )
			{
				this.DOMDocument = FCKTools.CreateXmlObject( 'DOMDocument' ) ;
				this.DOMDocument.async = false ;
				this.DOMDocument.resolveExternals = false ;
				this.DOMDocument.loadXML( oXmlHttp.responseText ) ;
			}
		}
		else
		{
			this.DOMDocument = null ;
		}

		if ( this.DOMDocument == null || this.DOMDocument.firstChild == null )
		{
			this.Error = true ;
			if (window.confirm( 'Error loading "' + urlToCall + '"\r\nDo you want to see more info?' ) )
				alert( 'URL requested: "' + urlToCall + '"\r\n' +
							'Server response:\r\nStatus: ' + oXmlHttp.status + '\r\n' +
							'Response text:\r\n' + oXmlHttp.responseText ) ;
		}
	},

	SelectNodes : function( xpath, contextNode )
	{
		if ( this.Error )
			return new Array() ;

		if ( contextNode )
			return contextNode.selectNodes( xpath ) ;
		else
			return this.DOMDocument.selectNodes( xpath ) ;
	},

	SelectSingleNode : function( xpath, contextNode )
	{
		if ( this.Error )
			return null ;

		if ( contextNode )
			return contextNode.selectSingleNode( xpath ) ;
		else
			return this.DOMDocument.selectSingleNode( xpath ) ;
	}
} ;