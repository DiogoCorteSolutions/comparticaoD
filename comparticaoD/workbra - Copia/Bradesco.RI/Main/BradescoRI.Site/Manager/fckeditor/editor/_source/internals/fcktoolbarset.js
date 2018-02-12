function FCKToolbarSet_Create( overhideLocation )
{
	var oToolbarSet ;

	var sLocation = overhideLocation || FCKConfig.ToolbarLocation ;
	switch ( sLocation )
	{
		case 'In' :
			document.getElementById( 'xToolbarRow' ).style.display = '' ;
			oToolbarSet = new FCKToolbarSet( document ) ;
			break ;
		case 'None' :
			oToolbarSet = new FCKToolbarSet( document ) ;
			break ;




		default :
			FCK.Events.AttachEvent( 'OnBlur', FCK_OnBlur ) ;
			FCK.Events.AttachEvent( 'OnFocus', FCK_OnFocus ) ;

			var eToolbarTarget ;


			var oOutMatch = sLocation.match( /^Out:(.+)\((\w+)\)$/ ) ;
			if ( oOutMatch )
			{
				if ( FCKBrowserInfo.IsAIR )
					FCKAdobeAIR.ToolbarSet_GetOutElement( window, oOutMatch ) ;
				else
					eToolbarTarget = eval( 'parent.' + oOutMatch[1] ).document.getElementById( oOutMatch[2] ) ;
			}
			else
			{

				oOutMatch = sLocation.match( /^Out:(\w+)$/ ) ;
				if ( oOutMatch )
					eToolbarTarget = parent.document.getElementById( oOutMatch[1] ) ;
			}

			if ( !eToolbarTarget )
			{
				alert( 'Invalid value for "ToolbarLocation"' ) ;
				return arguments.callee( 'In' );
			}


			oToolbarSet = eToolbarTarget.__FCKToolbarSet ;
			if ( oToolbarSet )
				break ;


			var eToolbarIFrame = FCKTools.GetElementDocument( eToolbarTarget ).createElement( 'iframe' ) ;
			eToolbarIFrame.src = 'javascript:void(0)' ;
			eToolbarIFrame.frameBorder = 0 ;
			eToolbarIFrame.width = '100%' ;
			eToolbarIFrame.height = '10' ;
			eToolbarTarget.appendChild( eToolbarIFrame ) ;
			eToolbarIFrame.unselectable = 'on' ;


			var eTargetDocument = eToolbarIFrame.contentWindow.document ;


			var sBase = '' ;
			if ( FCKBrowserInfo.IsSafari )
				sBase = '<base href="' + window.document.location + '">' ;


			eTargetDocument.open() ;
			eTargetDocument.write( '<html><head>' + sBase + '<script type="text/javascript"> var adjust = function() { window.frameElement.height = document.body.scrollHeight ; }; '
					+ 'window.onresize = window.onload = '
					+ 'function(){'
					+ 'var timer = null;'
					+ 'var lastHeight = -1;'
					+ 'var lastChange = 0;'
					+ 'var poller = function(){'
					+ 'var currentHeight = document.body.scrollHeight || 0;'
					+ 'var currentTime = (new Date()).getTime();'
					+ 'if (currentHeight != lastHeight){'
					+ 'lastChange = currentTime;'
					+ 'adjust();'
					+ 'lastHeight = document.body.scrollHeight;'
					+ '}'
					+ 'if (lastChange < currentTime - 1000) clearInterval(timer);'
					+ '};'
					+ 'timer = setInterval(poller, 100);'
					+ '}'
					+ '</script></head><body style="overflow: hidden">' + document.getElementById( 'xToolbarSpace' ).innerHTML + '</body></html>' ) ;
			eTargetDocument.close() ;

			if( FCKBrowserInfo.IsAIR )
				FCKAdobeAIR.ToolbarSet_InitOutFrame( eTargetDocument ) ;

			FCKTools.AddEventListener( eTargetDocument, 'contextmenu', FCKTools.CancelEvent ) ;



			FCKTools.AppendStyleSheet( eTargetDocument, FCKConfig.SkinEditorCSS ) ;

			oToolbarSet = eToolbarTarget.__FCKToolbarSet = new FCKToolbarSet( eTargetDocument ) ;
			oToolbarSet._IFrame = eToolbarIFrame ;

			if ( FCK.IECleanup )
				FCK.IECleanup.AddItem( eToolbarTarget, FCKToolbarSet_Target_Cleanup ) ;
	}

	oToolbarSet.CurrentInstance = FCK ;
	if ( !oToolbarSet.ToolbarItems )
		oToolbarSet.ToolbarItems = FCKToolbarItems ;

	FCK.AttachToOnSelectionChange( oToolbarSet.RefreshItemsState ) ;

	return oToolbarSet ;
}

function FCK_OnBlur( editorInstance )
{
	var eToolbarSet = editorInstance.ToolbarSet ;

	if ( eToolbarSet.CurrentInstance == editorInstance )
		eToolbarSet.Disable() ;
}

function FCK_OnFocus( editorInstance )
{
	var oToolbarset = editorInstance.ToolbarSet ;
	var oInstance = editorInstance || FCK ;


	oToolbarset.CurrentInstance.FocusManager.RemoveWindow( oToolbarset._IFrame.contentWindow ) ;


	oToolbarset.CurrentInstance = oInstance ;


	oInstance.FocusManager.AddWindow( oToolbarset._IFrame.contentWindow, true ) ;

	oToolbarset.Enable() ;
}

function FCKToolbarSet_Cleanup()
{
	this._TargetElement = null ;
	this._IFrame = null ;
}

function FCKToolbarSet_Target_Cleanup()
{
	this.__FCKToolbarSet = null ;
}

var FCKToolbarSet = function( targetDocument )
{
	this._Document = targetDocument ;


	this._TargetElement	= targetDocument.getElementById( 'xToolbar' ) ;


	var eExpandHandle	= targetDocument.getElementById( 'xExpandHandle' ) ;
	var eCollapseHandle	= targetDocument.getElementById( 'xCollapseHandle' ) ;

	eExpandHandle.title		= FCKLang.ToolbarExpand ;
	FCKTools.AddEventListener( eExpandHandle, 'click', FCKToolbarSet_Expand_OnClick ) ;

	eCollapseHandle.title	= FCKLang.ToolbarCollapse ;
	FCKTools.AddEventListener( eCollapseHandle, 'click', FCKToolbarSet_Collapse_OnClick ) ;


	if ( !FCKConfig.ToolbarCanCollapse || FCKConfig.ToolbarStartExpanded )
		this.Expand() ;
	else
		this.Collapse() ;


	eCollapseHandle.style.display = FCKConfig.ToolbarCanCollapse ? '' : 'none' ;

	if ( FCKConfig.ToolbarCanCollapse )
		eCollapseHandle.style.display = '' ;
	else
		targetDocument.getElementById( 'xTBLeftBorder' ).style.display = '' ;


	this.Toolbars = new Array() ;
	this.IsLoaded = false ;

	if ( FCK.IECleanup )
		FCK.IECleanup.AddItem( this, FCKToolbarSet_Cleanup ) ;
}

function FCKToolbarSet_Expand_OnClick()
{
	FCK.ToolbarSet.Expand() ;
}

function FCKToolbarSet_Collapse_OnClick()
{
	FCK.ToolbarSet.Collapse() ;
}

FCKToolbarSet.prototype.Expand = function()
{
	this._ChangeVisibility( false ) ;
}

FCKToolbarSet.prototype.Collapse = function()
{
	this._ChangeVisibility( true ) ;
}

FCKToolbarSet.prototype._ChangeVisibility = function( collapse )
{
	this._Document.getElementById( 'xCollapsed' ).style.display = collapse ? '' : 'none' ;
	this._Document.getElementById( 'xExpanded' ).style.display = collapse ? 'none' : '' ;

	if ( FCKBrowserInfo.IsGecko )
	{


		FCKTools.RunFunction( window.onresize ) ;
	}
}

FCKToolbarSet.prototype.Load = function( toolbarSetName )
{
	this.Name = toolbarSetName ;

	this.Items = new Array() ;


	this.ItemsWysiwygOnly = new Array() ;


	this.ItemsContextSensitive = new Array() ;


	this._TargetElement.innerHTML = '' ;

	var ToolbarSet = FCKConfig.ToolbarSets[toolbarSetName] ;

	if ( !ToolbarSet )
	{
		alert( FCKLang.UnknownToolbarSet.replace( /%1/g, toolbarSetName ) ) ;
		return ;
	}

	this.Toolbars = new Array() ;

	for ( var x = 0 ; x < ToolbarSet.length ; x++ )
	{
		var oToolbarItems = ToolbarSet[x] ;



		if ( !oToolbarItems )
			continue ;

		var oToolbar ;

		if ( typeof( oToolbarItems ) == 'string' )
		{
			if ( oToolbarItems == '/' )
				oToolbar = new FCKToolbarBreak() ;
		}
		else
		{
			oToolbar = new FCKToolbar() ;

			for ( var j = 0 ; j < oToolbarItems.length ; j++ )
			{
				var sItem = oToolbarItems[j] ;

				if ( sItem == '-')
					oToolbar.AddSeparator() ;
				else
				{
					var oItem = FCKToolbarItems.GetItem( sItem ) ;
					if ( oItem )
					{
						oToolbar.AddItem( oItem ) ;

						this.Items.push( oItem ) ;

						if ( !oItem.SourceView )
							this.ItemsWysiwygOnly.push( oItem ) ;

						if ( oItem.ContextSensitive )
							this.ItemsContextSensitive.push( oItem ) ;
					}
				}
			}


		}

		oToolbar.Create( this._TargetElement ) ;

		this.Toolbars[ this.Toolbars.length ] = oToolbar ;
	}

	FCKTools.DisableSelection( this._Document.getElementById( 'xCollapseHandle' ).parentNode ) ;

	if ( FCK.Status != FCK_STATUS_COMPLETE )
		FCK.Events.AttachEvent( 'OnStatusChange', this.RefreshModeState ) ;
	else
		this.RefreshModeState() ;

	this.IsLoaded = true ;
	this.IsEnabled = true ;

	FCKTools.RunFunction( this.OnLoad ) ;
}

FCKToolbarSet.prototype.Enable = function()
{
	if ( this.IsEnabled )
		return ;

	this.IsEnabled = true ;

	var aItems = this.Items ;
	for ( var i = 0 ; i < aItems.length ; i++ )
		aItems[i].RefreshState() ;
}

FCKToolbarSet.prototype.Disable = function()
{
	if ( !this.IsEnabled )
		return ;

	this.IsEnabled = false ;

	var aItems = this.Items ;
	for ( var i = 0 ; i < aItems.length ; i++ )
		aItems[i].Disable() ;
}

FCKToolbarSet.prototype.RefreshModeState = function( editorInstance )
{
	if ( FCK.Status != FCK_STATUS_COMPLETE )
		return ;

	var oToolbarSet = editorInstance ? editorInstance.ToolbarSet : this ;
	var aItems = oToolbarSet.ItemsWysiwygOnly ;

	if ( FCK.EditMode == FCK_EDITMODE_WYSIWYG )
	{

		for ( var i = 0 ; i < aItems.length ; i++ )
			aItems[i].Enable() ;


		oToolbarSet.RefreshItemsState( editorInstance ) ;
	}
	else
	{

		oToolbarSet.RefreshItemsState( editorInstance ) ;


		for ( var j = 0 ; j < aItems.length ; j++ )
			aItems[j].Disable() ;
	}
}

FCKToolbarSet.prototype.RefreshItemsState = function( editorInstance )
{

	var aItems = ( editorInstance ? editorInstance.ToolbarSet : this ).ItemsContextSensitive ;

	for ( var i = 0 ; i < aItems.length ; i++ )
		aItems[i].RefreshState() ;
}
