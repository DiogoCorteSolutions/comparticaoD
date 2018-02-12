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