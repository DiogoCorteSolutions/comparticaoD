var oWindow ;
var oDiv ;

if ( !window.FCKMessages )
	window.FCKMessages = new Array() ;

window.onload = function()
{
	oWindow = document.getElementById('xOutput').contentWindow ;
	oWindow.document.open() ;
	oWindow.document.write( '<div id="divMsg"><\/div>' ) ;
	oWindow.document.close() ;
	oDiv	= oWindow.document.getElementById('divMsg') ;
}

function Output( message, color, noParse )
{
	if ( !noParse && message != null && isNaN( message ) )
		message = message.replace(/</g, "&lt;") ;

	if ( color )
		message = '<font color="' + color + '">' + message + '<\/font>' ;

	window.FCKMessages[ window.FCKMessages.length ] = message ;
	StartTimer() ;
}

function OutputObject( anyObject, color )
{
	var message ;

	if ( anyObject != null )
	{
		message = 'Properties of: ' + anyObject + '</b><blockquote>' ;

		for (var prop in anyObject)
		{
			try
			{
				var sVal = anyObject[ prop ] != null ? anyObject[ prop ] + '' : '[null]' ;
				message += '<b>' + prop + '</b> : ' + sVal.replace(/</g, '&lt;') + '<br>' ;
			}
			catch (e)
			{
				try
				{
					message += '<b>' + prop + '</b> : [' + typeof( anyObject[ prop ] ) + ']<br>' ;
				}
				catch (e)
				{
					message += '<b>' + prop + '</b> : [-error-]<br>' ;
				}
			}
		}

		message += '</blockquote><b>' ;
	} else
		message = 'OutputObject : Object is "null".' ;

	Output( message, color, true ) ;
}

function StartTimer()
{
	window.setTimeout( 'CheckMessages()', 100 ) ;
}

function CheckMessages()
{
	if ( window.FCKMessages.length > 0 )
	{

		var sMessage = window.FCKMessages[0] ;


		var oTempArray = new Array() ;
		for ( i = 1 ; i < window.FCKMessages.length ; i++ )
			oTempArray[ i - 1 ] = window.FCKMessages[ i ] ;
		window.FCKMessages = oTempArray ;

		var d = new Date() ;
		var sTime =
			( d.getHours() + 100 + '' ).substr( 1,2 ) + ':' +
			( d.getMinutes() + 100 + '' ).substr( 1,2 ) + ':' +
			( d.getSeconds() + 100 + '' ).substr( 1,2 ) + ':' +
			( d.getMilliseconds() + 1000 + '' ).substr( 1,3 ) ;

		var oMsgDiv = oWindow.document.createElement( 'div' ) ;
		oMsgDiv.innerHTML = sTime + ': <b>' + sMessage + '<\/b>' ;
		oDiv.appendChild( oMsgDiv ) ;
		oMsgDiv.scrollIntoView() ;
	}
}

function Clear()
{
	oDiv.innerHTML = '' ;
}