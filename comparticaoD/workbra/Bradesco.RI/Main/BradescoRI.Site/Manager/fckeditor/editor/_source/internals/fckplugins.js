var FCKPlugins = FCK.Plugins = new Object() ;
FCKPlugins.ItemsCount = 0 ;
FCKPlugins.Items = new Object() ;

FCKPlugins.Load = function()
{
	var oItems = FCKPlugins.Items ;


	for ( var i = 0 ; i < FCKConfig.Plugins.Items.length ; i++ )
	{
		var oItem = FCKConfig.Plugins.Items[i] ;
		var oPlugin = oItems[ oItem[0] ] = new FCKPlugin( oItem[0], oItem[1], oItem[2] ) ;
		FCKPlugins.ItemsCount++ ;
	}


	for ( var s in oItems )
		oItems[s].Load() ;


	FCKPlugins.Load = null ;
}