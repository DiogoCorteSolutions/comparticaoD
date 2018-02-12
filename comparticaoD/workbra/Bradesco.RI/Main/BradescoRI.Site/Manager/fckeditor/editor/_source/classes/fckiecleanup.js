var	FCKIECleanup = function( attachWindow )
{
	if ( attachWindow._FCKCleanupObj )
		this.Items = attachWindow._FCKCleanupObj.Items ;
	else
	{
		this.Items = new Array() ;

		attachWindow._FCKCleanupObj = this ;
		FCKTools.AddEventListenerEx( attachWindow, 'unload', FCKIECleanup_Cleanup ) ;
	}
}

FCKIECleanup.prototype.AddItem = function( dirtyItem, cleanupFunction )
{
	this.Items.push( [ dirtyItem, cleanupFunction ] ) ;
}

function FCKIECleanup_Cleanup()
{
	if ( !this._FCKCleanupObj || ( FCKConfig.MsWebBrowserControlCompat && !window.FCKUnloadFlag ) )
		return ;

	var aItems = this._FCKCleanupObj.Items ;

	while ( aItems.length > 0 )
	{
		var oItem = aItems.pop() ;
		if ( oItem )
			oItem[1].call( oItem[0] ) ;
	}

	this._FCKCleanupObj = null ;

	if ( CollectGarbage )
		CollectGarbage() ;
}