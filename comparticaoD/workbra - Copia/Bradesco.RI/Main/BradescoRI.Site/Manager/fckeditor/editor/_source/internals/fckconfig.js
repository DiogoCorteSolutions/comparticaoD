var FCKConfig = FCK.Config = new Object() ;

FCKConfig.BasePath = document.location.protocol + '//' + document.location.host +
	document.location.pathname.substring( 0, document.location.pathname.lastIndexOf( '/' ) + 1) ;

FCKConfig.FullBasePath = FCKConfig.BasePath ;

FCKConfig.EditorPath = FCKConfig.BasePath.replace( /editor\/$/, '' ) ;



try
{
	FCKConfig.ScreenWidth	= screen.width ;
	FCKConfig.ScreenHeight	= screen.height ;
}
catch (e)
{
	FCKConfig.ScreenWidth	= 800 ;
	FCKConfig.ScreenHeight	= 600 ;
}



FCKConfig.ProcessHiddenField = function()
{
	this.PageConfig = new Object() ;


	var oConfigField = window.parent.document.getElementById( FCK.Name + '___Config' ) ;


	if ( ! oConfigField ) return ;

	var aCouples = oConfigField.value.split('&') ;

	for ( var i = 0 ; i < aCouples.length ; i++ )
	{
		if ( aCouples[i].length == 0 )
			continue ;

		var aConfig = aCouples[i].split( '=' ) ;
		var sKey = decodeURIComponent( aConfig[0] ) ;
		var sVal = decodeURIComponent( aConfig[1] ) ;

		if ( sKey == 'CustomConfigurationsPath' )
			FCKConfig[ sKey ] = sVal ;

		else if ( sVal.toLowerCase() == "true" )
			this.PageConfig[ sKey ] = true ;

		else if ( sVal.toLowerCase() == "false" )
			this.PageConfig[ sKey ] = false ;

		else if ( sVal.length > 0 && !isNaN( sVal ) )
			this.PageConfig[ sKey ] = parseInt( sVal, 10 ) ;

		else
			this.PageConfig[ sKey ] = sVal ;
	}
}

function FCKConfig_LoadPageConfig()
{
	var oPageConfig = FCKConfig.PageConfig ;
	for ( var sKey in oPageConfig )
		FCKConfig[ sKey ] = oPageConfig[ sKey ] ;
}

function FCKConfig_PreProcess()
{
	var oConfig = FCKConfig ;


	if ( oConfig.AllowQueryStringDebug )
	{
		try
		{
			if ( (/fckdebug=true/i).test( window.top.location.search ) )
				oConfig.Debug = true ;
		}
		catch (e) { }
	}


	if ( !oConfig.PluginsPath.EndsWith('/') )
		oConfig.PluginsPath += '/' ;


	var sComboPreviewCSS = oConfig.ToolbarComboPreviewCSS ;
	if ( !sComboPreviewCSS || sComboPreviewCSS.length == 0 )
		oConfig.ToolbarComboPreviewCSS = oConfig.EditorAreaCSS ;


	oConfig.RemoveAttributesArray = (oConfig.RemoveAttributes || '').split( ',' );

	if ( !FCKConfig.SkinEditorCSS || FCKConfig.SkinEditorCSS.length == 0 )
		FCKConfig.SkinEditorCSS = FCKConfig.SkinPath + 'fck_editor.css' ;

	if ( !FCKConfig.SkinDialogCSS || FCKConfig.SkinDialogCSS.length == 0 )
		FCKConfig.SkinDialogCSS = FCKConfig.SkinPath + 'fck_dialog.css' ;
}


FCKConfig.ToolbarSets = new Object() ;


FCKConfig.Plugins = new Object() ;
FCKConfig.Plugins.Items = new Array() ;

FCKConfig.Plugins.Add = function( name, langs, path )
{
	FCKConfig.Plugins.Items.push( [name, langs, path] ) ;
}




FCKConfig.ProtectedSource = new Object() ;


FCKConfig.ProtectedSource._CodeTag = (new Date()).valueOf() ;


FCKConfig.ProtectedSource.RegexEntries = [


	/<!--[\s\S]*?-->/g ,


	/<script[\s\S]*?<\/script>/gi,


	/<noscript[\s\S]*?<\/noscript>/gi
] ;

FCKConfig.ProtectedSource.Add = function( regexPattern )
{
	this.RegexEntries.push( regexPattern ) ;
}

FCKConfig.ProtectedSource.Protect = function( html )
{
	var codeTag = this._CodeTag ;
	function _Replace( protectedSource )
	{
		var index = FCKTempBin.AddElement( protectedSource ) ;
		return '<!--{' + codeTag + index + '}-->' ;
	}

	for ( var i = 0 ; i < this.RegexEntries.length ; i++ )
	{
		html = html.replace( this.RegexEntries[i], _Replace ) ;
	}

	return html ;
}

FCKConfig.ProtectedSource.Revert = function( html, clearBin )
{
	function _Replace( m, opener, index )
	{
		var protectedValue = clearBin ? FCKTempBin.RemoveElement( index ) : FCKTempBin.Elements[ index ] ;

		return FCKConfig.ProtectedSource.Revert( protectedValue, clearBin ) ;
	}

	var regex = new RegExp( "(<|&lt;)!--\\{" + this._CodeTag + "(\\d+)\\}--(>|&gt;)", "g" ) ;
	return html.replace( regex, _Replace ) ;
}


FCKConfig.GetBodyAttributes = function()
{
	var bodyAttributes = '' ;

	if ( this.BodyId && this.BodyId.length > 0 )
		bodyAttributes += ' id="' + this.BodyId + '"' ;
	if ( this.BodyClass && this.BodyClass.length > 0 )
		bodyAttributes += ' class="' + this.BodyClass + '"' ;

	return bodyAttributes ;
}


FCKConfig.ApplyBodyAttributes = function( oBody )
{

	if ( this.BodyId && this.BodyId.length > 0 )
		oBody.id = FCKConfig.BodyId ;
	if ( this.BodyClass && this.BodyClass.length > 0 )
		oBody.className += ' ' + FCKConfig.BodyClass ;
}
