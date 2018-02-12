if ( document.location.protocol == 'file:' )
{
	try
	{
		window.parent.document.domain ;
	}
	catch ( e )
	{
		window.addEventListener( 'load', function()
			{
				document.body.innerHTML = '\
					<div style="border: 1px red solid; font-family: arial; font-size: 12px; color: red; padding:10px;">\
						<p>\
							<b>Your browser security settings don\'t allow FCKeditor to be opened from\
							the local filesystem.<\/b>\
						<\/p>\
						<p>\
							Please open the <b>about:config<\/b> page and disable the\
							&quot;security.fileuri.strict_origin_policy&quot; option; then load this page again.\
						<\/p>\
						<p>\
							Check our <a href="http://docs.fckeditor.net/FCKeditor_2.x/Developers_Guide/FAQ#ff3perms">FAQ<\/a>\
							for more information.\
						<\/p>\
					<\/div>' ;
			}, false ) ;
	}
}


var FCK_ORIGINAL_DOMAIN ;


(function()
{
	var d = FCK_ORIGINAL_DOMAIN = document.domain ;

	while ( true )
	{

		try
		{
			var test = window.parent.document.domain ;
			break ;
		}
		catch( e ) {}


		d = d.replace( /.*?(?:\.|$)/, '' ) ;

		if ( d.length == 0 )
			break ;

		try
		{
			document.domain = d ;
		}
		catch (e)
		{
			break ;
		}
	}
})() ;


var FCK_RUNTIME_DOMAIN = document.domain ;

var FCK_IS_CUSTOM_DOMAIN = ( FCK_ORIGINAL_DOMAIN != FCK_RUNTIME_DOMAIN ) ;






function LoadScript( url )
{
	document.write( '<scr' + 'ipt type="text/javascript" src="' + url + '"><\/scr' + 'ipt>' ) ;
}


var sSuffix = ( /*@cc_on!@*/false ) ? 'ie' : 'gecko' ;

LoadScript( 'js/fckeditorcode_' + sSuffix + '.js' ) ;


LoadScript( '../fckconfig.js' ) ;