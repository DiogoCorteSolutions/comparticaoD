(function()
{
	var d = document.domain ;

	while ( true )
	{

		try
		{
			var test = window.opener.document.domain ;
			break ;
		}
		catch( e )
		{}


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

function GetUrlParam( paramName )
{
	var oRegex = new RegExp( '[\?&]' + paramName + '=([^&]+)', 'i' ) ;
	var oMatch = oRegex.exec( window.top.location.search ) ;

	if ( oMatch && oMatch.length > 1 )
		return decodeURIComponent( oMatch[1] ) ;
	else
		return '' ;
}

var oConnector = new Object() ;
oConnector.CurrentFolder	= '/' ;

var sConnUrl = GetUrlParam( 'Connector' ) ;


if ( sConnUrl.substr(0,1) != '/' && sConnUrl.indexOf( '://' ) < 0 )
	sConnUrl = window.location.href.replace( /browser.html.*$/, '' ) + sConnUrl ;

oConnector.ConnectorUrl = sConnUrl + ( sConnUrl.indexOf('?') != -1 ? '&' : '?' ) ;

var sServerPath = GetUrlParam( 'ServerPath' ) ;
if ( sServerPath.length > 0 )
	oConnector.ConnectorUrl += 'ServerPath=' + encodeURIComponent( sServerPath ) + '&' ;

oConnector.ResourceType		= GetUrlParam( 'Type' ) ;
oConnector.ShowAllTypes		= ( oConnector.ResourceType.length == 0 ) ;

if ( oConnector.ShowAllTypes )
	oConnector.ResourceType = 'File' ;

oConnector.SendCommand = function( command, params, callBackFunction )
{
	var sUrl = this.ConnectorUrl + 'Command=' + command ;
	sUrl += '&Type=' + this.ResourceType ;
	sUrl += '&CurrentFolder=' + encodeURIComponent( this.CurrentFolder ) ;

	if ( params ) sUrl += '&' + params ;


	sUrl += '&uuid=' + new Date().getTime() ;

	var oXML = new FCKXml() ;

	if ( callBackFunction )
		oXML.LoadUrl( sUrl, callBackFunction ) ;
	else
		return oXML.LoadUrl( sUrl ) ;

	return null ;
}

oConnector.CheckError = function( responseXml )
{
	var iErrorNumber = 0 ;
	var oErrorNode = responseXml.SelectSingleNode( 'Connector/Error' ) ;

	if ( oErrorNode )
	{
		iErrorNumber = parseInt( oErrorNode.attributes.getNamedItem('number').value, 10 ) ;

		switch ( iErrorNumber )
		{
			case 0 :
				break ;
			case 1 :
				alert( oErrorNode.attributes.getNamedItem('text').value ) ;
				break ;
			case 101 :
				alert( 'Folder already exists' ) ;
				break ;
			case 102 :
				alert( 'Invalid folder name' ) ;
				break ;
			case 103 :
				alert( 'You have no permissions to create the folder' ) ;
				break ;
			case 110 :
				alert( 'Unknown error creating folder' ) ;
				break ;
			default :
				alert( 'Error on your request. Error number: ' + iErrorNumber ) ;
				break ;
		}
	}
	return iErrorNumber ;
}

var oIcons = new Object() ;

oIcons.AvailableIconsArray = [
	'ai','avi','bmp','cs','dll','doc','exe','fla','gif','htm','html','jpg','js',
	'mdb','mp3','pdf','png','ppt','rdp','swf','swt','txt','vsd','xls','xml','zip' ] ;

oIcons.AvailableIcons = new Object() ;

for ( var i = 0 ; i < oIcons.AvailableIconsArray.length ; i++ )
	oIcons.AvailableIcons[ oIcons.AvailableIconsArray[i] ] = true ;

oIcons.GetIcon = function( fileName )
{
	var sExtension = fileName.substr( fileName.lastIndexOf('.') + 1 ).toLowerCase() ;

	if ( this.AvailableIcons[ sExtension ] == true )
		return sExtension ;
	else
		return 'default.icon' ;
}

function OnUploadCompleted( errorNumber, fileUrl, fileName, customMsg )
{
	if (errorNumber == "1")
		window.frames['frmUpload'].OnUploadCompleted( errorNumber, customMsg ) ;
	else
		window.frames['frmUpload'].OnUploadCompleted( errorNumber, fileName ) ;
}