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
							Check our <a href="http:
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

LoadScript( '_source/fckconstants.js' ) ;
LoadScript( '_source/fckjscoreextensions.js' ) ;

if ( sSuffix == 'ie' )
	LoadScript( '_source/classes/fckiecleanup.js' ) ;

LoadScript( '_source/internals/fckbrowserinfo.js' ) ;
LoadScript( '_source/internals/fckurlparams.js' ) ;
LoadScript( '_source/classes/fckevents.js' ) ;
LoadScript( '_source/classes/fckdataprocessor.js' ) ;
LoadScript( '_source/internals/fck.js' ) ;
LoadScript( '_source/internals/fck_' + sSuffix + '.js' ) ;
LoadScript( '_source/internals/fckconfig.js' ) ;

LoadScript( '_source/internals/fckdebug_empty.js' ) ;
LoadScript( '_source/internals/fckdomtools.js' ) ;
LoadScript( '_source/internals/fcktools.js' ) ;
LoadScript( '_source/internals/fcktools_' + sSuffix + '.js' ) ;
LoadScript( '_source/fckeditorapi.js' ) ;
LoadScript( '_source/classes/fckimagepreloader.js' ) ;
LoadScript( '_source/internals/fckregexlib.js' ) ;
LoadScript( '_source/internals/fcklistslib.js' ) ;
LoadScript( '_source/internals/fcklanguagemanager.js' ) ;
LoadScript( '_source/internals/fckxhtmlentities.js' ) ;
LoadScript( '_source/internals/fckxhtml.js' ) ;
LoadScript( '_source/internals/fckxhtml_' + sSuffix + '.js' ) ;
LoadScript( '_source/internals/fckcodeformatter.js' ) ;
LoadScript( '_source/internals/fckundo.js' ) ;
LoadScript( '_source/classes/fckeditingarea.js' ) ;
LoadScript( '_source/classes/fckkeystrokehandler.js' ) ;

LoadScript( 'dtd/fck_xhtml10transitional.js' ) ;
LoadScript( '_source/classes/fckstyle.js' ) ;
LoadScript( '_source/internals/fckstyles.js' ) ;

LoadScript( '_source/internals/fcklisthandler.js' ) ;
LoadScript( '_source/classes/fckelementpath.js' ) ;
LoadScript( '_source/classes/fckdomrange.js' ) ;
LoadScript( '_source/classes/fckdocumentfragment_' + sSuffix + '.js' ) ;
LoadScript( '_source/classes/fckw3crange.js' ) ;
LoadScript( '_source/classes/fckdomrange_' + sSuffix + '.js' ) ;
LoadScript( '_source/classes/fckdomrangeiterator.js' ) ;
LoadScript( '_source/classes/fckenterkey.js' ) ;

LoadScript( '_source/internals/fckdocumentprocessor.js' ) ;
LoadScript( '_source/internals/fckselection.js' ) ;
LoadScript( '_source/internals/fckselection_' + sSuffix + '.js' ) ;

LoadScript( '_source/internals/fcktablehandler.js' ) ;
LoadScript( '_source/internals/fcktablehandler_' + sSuffix + '.js' ) ;
LoadScript( '_source/classes/fckxml.js' ) ;
LoadScript( '_source/classes/fckxml_' + sSuffix + '.js' ) ;

LoadScript( '_source/commandclasses/fcknamedcommand.js' ) ;
LoadScript( '_source/commandclasses/fckstylecommand.js' ) ;
LoadScript( '_source/commandclasses/fck_othercommands.js' ) ;
LoadScript( '_source/commandclasses/fckshowblocks.js' ) ;
LoadScript( '_source/commandclasses/fcktextcolorcommand.js' ) ;
LoadScript( '_source/commandclasses/fckpasteplaintextcommand.js' ) ;
LoadScript( '_source/commandclasses/fckpastewordcommand.js' ) ;
LoadScript( '_source/commandclasses/fcktablecommand.js' ) ;
LoadScript( '_source/commandclasses/fckfitwindow.js' ) ;
LoadScript( '_source/commandclasses/fcklistcommands.js' ) ;
LoadScript( '_source/commandclasses/fckjustifycommands.js' ) ;
LoadScript( '_source/commandclasses/fckindentcommands.js' ) ;
LoadScript( '_source/commandclasses/fckblockquotecommand.js' ) ;
LoadScript( '_source/commandclasses/fckcorestylecommand.js' ) ;
LoadScript( '_source/commandclasses/fckremoveformatcommand.js' ) ;
LoadScript( '_source/internals/fckcommands.js' ) ;

LoadScript( '_source/classes/fckpanel.js' ) ;
LoadScript( '_source/classes/fckicon.js' ) ;
LoadScript( '_source/classes/fcktoolbarbuttonui.js' ) ;
LoadScript( '_source/classes/fcktoolbarbutton.js' ) ;
LoadScript( '_source/classes/fckspecialcombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarspecialcombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarstylecombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarfontformatcombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarfontscombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarfontsizecombo.js' ) ;
LoadScript( '_source/classes/fcktoolbarpanelbutton.js' ) ;
LoadScript( '_source/internals/fcktoolbaritems.js' ) ;
LoadScript( '_source/classes/fcktoolbar.js' ) ;
LoadScript( '_source/classes/fcktoolbarbreak_' + sSuffix + '.js' ) ;
LoadScript( '_source/internals/fcktoolbarset.js' ) ;
LoadScript( '_source/internals/fckdialog.js' ) ;
LoadScript( '_source/classes/fckmenuitem.js' ) ;
LoadScript( '_source/classes/fckmenublock.js' ) ;
LoadScript( '_source/classes/fckmenublockpanel.js' ) ;
LoadScript( '_source/classes/fckcontextmenu.js' ) ;
LoadScript( '_source/internals/fck_contextmenu.js' ) ;
LoadScript( '_source/classes/fckhtmliterator.js' ) ;
LoadScript( '_source/classes/fckplugin.js' ) ;
LoadScript( '_source/internals/fckplugins.js' ) ;




LoadScript( '../fckconfig.js' ) ;


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


FCKConfig_LoadPageConfig() ;

FCKConfig_PreProcess() ;


if ( FCKConfig.Debug )
	LoadScript( '_source/internals/fckdebug.js' ) ;

var FCK_InternalCSS			= FCKConfig.BasePath + 'css/fck_internal.css' ;
var FCK_ShowTableBordersCSS	= FCKConfig.BasePath + 'css/fck_showtableborders_gecko.css' ;

if ( FCKConfig.Debug )
	FCKDebug._GetWindow() ;


document.write( FCKTools.GetStyleHtml( FCKConfig.SkinEditorCSS ) ) ;


FCKLanguageManager.Initialize() ;
LoadScript( 'lang/' + FCKLanguageManager.ActiveLanguage.Code + '.js' ) ;


FCK_ContextMenu_Init() ;

FCKPlugins.Load() ;


window.document.dir = FCKLang.Dir ;

window.onload = function()
{
	InitializeAPI() ;

	if ( FCKBrowserInfo.IsIE )
		FCK_PreloadImages() ;
	else
		LoadToolbarSetup() ;
}

function LoadToolbarSetup()
{
	FCKeditorAPI._FunctionQueue.Add( LoadToolbar ) ;
}

function LoadToolbar()
{
	var oToolbarSet = FCK.ToolbarSet = FCKToolbarSet_Create() ;

	if ( oToolbarSet.IsLoaded )
		StartEditor() ;
	else
	{
		oToolbarSet.OnLoad = StartEditor ;
		oToolbarSet.Load( FCKURLParams['Toolbar'] || 'Default' ) ;
	}
}

function StartEditor()
{

	FCK.ToolbarSet.OnLoad = null ;

	FCKeditorAPI._FunctionQueue.Remove( LoadToolbar ) ;

	FCK.Events.AttachEvent( 'OnStatusChange', WaitForActive ) ;


	FCK.StartEditor() ;
}

function WaitForActive( editorInstance, newStatus )
{
	if ( newStatus == FCK_STATUS_ACTIVE )
	{
		if ( FCKBrowserInfo.IsGecko )
			FCKTools.RunFunction( window.onresize ) ;

		if ( !FCKConfig.PreventSubmitHandler )
			_AttachFormSubmitToAPI() ;

		FCK.SetStatus( FCK_STATUS_COMPLETE ) ;



		if ( typeof( window.parent.FCKeditor_OnComplete ) == 'function' )
			window.parent.FCKeditor_OnComplete( FCK ) ;
	}
}



if ( FCKBrowserInfo.IsGecko && !FCKBrowserInfo.IsOpera )
{
	window.onresize = function( e )
	{



		if ( e && e.originalTarget !== document && e.originalTarget !== window && (!e.originalTarget.ownerDocument || e.originalTarget.ownerDocument != document ))
			return ;

		var oCell = document.getElementById( 'xEditingArea' ) ;

		var eInnerElement = oCell.firstChild ;
		if ( eInnerElement )
		{
			eInnerElement.style.height = '0px' ;
			eInnerElement.style.height = ( oCell.scrollHeight - 2 ) + 'px' ;
		}
	}
}