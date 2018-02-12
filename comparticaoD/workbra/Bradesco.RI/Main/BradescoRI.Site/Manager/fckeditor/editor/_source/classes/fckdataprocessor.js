var FCKDataProcessor = function()
{}

FCKDataProcessor.prototype =
{
	ConvertToHtml : function( data )
	{
		if ( FCKConfig.FullPage )
		{
			FCK.DocTypeDeclaration = data.match( FCKRegexLib.DocTypeTag ) ;

			if ( !FCKRegexLib.HasBodyTag.test( data ) )
				data = '<body>' + data + '</body>' ;

			if ( !FCKRegexLib.HtmlOpener.test( data ) )
				data = '<html dir="' + FCKConfig.ContentLangDirection + '">' + data + '</html>' ;

			if ( !FCKRegexLib.HeadOpener.test( data ) )
				data = data.replace( FCKRegexLib.HtmlOpener, '$&<head><title></title></head>' ) ;

			return data ;
		}
		else
		{
			var html =
				FCKConfig.DocType +
				'<html dir="' + FCKConfig.ContentLangDirection + '"' ;

			if ( FCKBrowserInfo.IsIE && FCKConfig.DocType.length > 0 && !FCKRegexLib.Html4DocType.test( FCKConfig.DocType ) )
				html += ' style="overflow-y: scroll"' ;

			html += '><head><title></title></head>' +
				'<body' + FCKConfig.GetBodyAttributes() + '>' +
				data +
				'</body></html>' ;

			return html ;
		}
	},

	ConvertToDataFormat : function( rootNode, excludeRoot, ignoreIfEmptyParagraph, format )
	{
		var data = FCKXHtml.GetXHTML( rootNode, !excludeRoot, format ) ;

		if ( ignoreIfEmptyParagraph && FCKRegexLib.EmptyOutParagraph.test( data ) )
			return '' ;

		return data ;
	},

	FixHtml : function( html )
	{
		return html ;
	}
} ;