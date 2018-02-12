var FCKURLParams = new Object() ;

(function()
{
	var aParams = document.location.search.substr(1).split('&') ;
	for ( var i = 0 ; i < aParams.length ; i++ )
	{
		var aParam = aParams[i].split('=') ;
		var sParamName  = decodeURIComponent( aParam[0] ) ;
		var sParamValue = decodeURIComponent( aParam[1] ) ;

		FCKURLParams[ sParamName ] = sParamValue ;
	}
})();