var s = navigator.userAgent.toLowerCase() ;

var FCKBrowserInfo =
{
	IsIE		: /*@cc_on!@*/false,
	IsIE7		: /*@cc_on!@*/false && ( parseInt( s.match( /msie (\d+)/ )[1], 10 ) >= 7 ),
	IsIE6		: /*@cc_on!@*/false && ( parseInt( s.match( /msie (\d+)/ )[1], 10 ) >= 6 ),
	IsSafari	: s.Contains(' applewebkit/'),
	IsOpera		: !!window.opera,
	IsAIR		: s.Contains(' adobeair/'),
	IsMac		: s.Contains('macintosh')
} ;


(function( browserInfo )
{
	browserInfo.IsGecko = ( navigator.product == 'Gecko' ) && !browserInfo.IsSafari && !browserInfo.IsOpera ;
	browserInfo.IsGeckoLike = ( browserInfo.IsGecko || browserInfo.IsSafari || browserInfo.IsOpera ) ;

	if ( browserInfo.IsGecko )
	{
		var geckoMatch = s.match( /rv:(\d+\.\d+)/ ) ;
		var geckoVersion = geckoMatch && parseFloat( geckoMatch[1] ) ;

		if ( geckoVersion )
		{
			browserInfo.IsGecko10 = ( geckoVersion < 1.8 ) ;
			browserInfo.IsGecko19 = ( geckoVersion > 1.8 ) ;
		}
	}
})(FCKBrowserInfo) ;