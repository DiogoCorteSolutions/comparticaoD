var FCKPlugin = function( name, availableLangs, basePath )
{
	this.Name = name ;
	this.BasePath = basePath ? basePath : FCKConfig.PluginsPath ;
	this.Path = this.BasePath + name + '/' ;

	if ( !availableLangs || availableLangs.length == 0 )
		this.AvailableLangs = new Array() ;
	else
		this.AvailableLangs = availableLangs.split(',') ;
}

FCKPlugin.prototype.Load = function()
{
	if ( this.AvailableLangs.length > 0 )
	{
		var sLang ;

		if ( this.AvailableLangs.IndexOf( FCKLanguageManager.ActiveLanguage.Code ) >= 0 )
			sLang = FCKLanguageManager.ActiveLanguage.Code ;
		else
			sLang = this.AvailableLangs[0] ;

		LoadScript( this.Path + 'lang/' + sLang + '.js' ) ;
	}

	LoadScript( this.Path + 'fckplugin.js' ) ;
}