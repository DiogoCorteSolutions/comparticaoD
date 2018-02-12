if ( FCKBrowserInfo.IsAIR )
	LoadScript( 'js/fckadobeair.js' ) ;

if ( FCKBrowserInfo.IsIE )
{

	try
	{
		document.execCommand( 'BackgroundImageCache', false, true ) ;
	}
	catch (e)
	{


	}


	FCK.IECleanup = new FCKIECleanup( window ) ;
	FCK.IECleanup.AddItem( FCKTempBin, FCKTempBin.Reset ) ;
	FCK.IECleanup.AddItem( FCK, FCK_Cleanup ) ;
}




FCK.Events.AttachEvent( 'OnSelectionChange', function() { FCKStyles.CheckSelectionChanges() ; } ) ;



FCKConfig.ProcessHiddenField() ;


if ( FCKConfig.CustomConfigurationsPath.length > 0 )
	LoadScript( FCKConfig.CustomConfigurationsPath ) ;