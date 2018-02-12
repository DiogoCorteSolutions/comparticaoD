FCKXml.prototype =
{
	LoadUrl : function( urlToCall )
	{
		this.Error = false ;

		var oXml ;
		var oXmlHttp = FCKTools.CreateXmlObject( 'XmlHttp' ) ;
		oXmlHttp.open( 'GET', urlToCall, false ) ;
		oXmlHttp.send( null ) ;

		if ( oXmlHttp.status == 200 || oXmlHttp.status == 304 || ( oXmlHttp.status == 0 && oXmlHttp.readyState == 4 ) )
		{
			oXml = oXmlHttp.responseXML ;


			if ( !oXml )
				oXml = (new DOMParser()).parseFromString( oXmlHttp.responseText, 'text/xml' ) ;
		}
		else
			oXml = null ;

		if ( oXml )
		{

			try
			{
				var test = oXml.firstChild ;
			}
			catch (e)
			{



				oXml = (new DOMParser()).parseFromString( oXmlHttp.responseText, 'text/xml' ) ;
			}
		}

		if ( !oXml || !oXml.firstChild )
		{
			this.Error = true ;
			if ( window.confirm( 'Error loading "' + urlToCall + '" (HTTP Status: ' + oXmlHttp.status + ').\r\nDo you want to see the server response dump?' ) )
				alert( oXmlHttp.responseText ) ;
		}

		this.DOMDocument = oXml ;
	},

	SelectNodes : function( xpath, contextNode )
	{
		if ( this.Error )
			return new Array() ;

		var aNodeArray = new Array();

		var xPathResult = this.DOMDocument.evaluate( xpath, contextNode ? contextNode : this.DOMDocument,
				this.DOMDocument.createNSResolver(this.DOMDocument.documentElement), XPathResult.ORDERED_NODE_ITERATOR_TYPE, null) ;
		if ( xPathResult )
		{
			var oNode = xPathResult.iterateNext() ;
			while( oNode )
			{
				aNodeArray[aNodeArray.length] = oNode ;
				oNode = xPathResult.iterateNext();
			}
		}
		return aNodeArray ;
	},

	SelectSingleNode : function( xpath, contextNode )
	{
		if ( this.Error )
			return null ;

		var xPathResult = this.DOMDocument.evaluate( xpath, contextNode ? contextNode : this.DOMDocument,
				this.DOMDocument.createNSResolver(this.DOMDocument.documentElement), 9, null);

		if ( xPathResult && xPathResult.singleNodeValue )
			return xPathResult.singleNodeValue ;
		else
			return null ;
	}
} ;