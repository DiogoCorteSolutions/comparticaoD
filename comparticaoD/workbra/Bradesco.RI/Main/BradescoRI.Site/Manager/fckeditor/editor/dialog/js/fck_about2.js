document.write( '<b>User Agent<\/b><br />' + window.navigator.userAgent + '<br /><br />' ) ;
document.write( '<b>Browser<\/b><br />' + window.navigator.appName + ' ' + window.navigator.appVersion + '<br /><br />' ) ;
document.write( '<b>Platform<\/b><br />' + window.navigator.platform + '<br /><br />' ) ;

var sUserLang = '?' ;

if ( window.navigator.language )
	sUserLang = window.navigator.language.toLowerCase() ;
else if ( window.navigator.userLanguage )
	sUserLang = window.navigator.userLanguage.toLowerCase() ;

document.write( '<b>User Language<\/b><br />' + sUserLang ) ;