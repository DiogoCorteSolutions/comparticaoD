var FCKToolbarPanelButton = function( commandName, label, tooltip, style, icon )
{
	this.CommandName = commandName ;

	var oIcon ;

	if ( icon == null )
		oIcon = FCKConfig.SkinPath + 'toolbar/' + commandName.toLowerCase() + '.gif' ;
	else if ( typeof( icon ) == 'number' )
		oIcon = [ FCKConfig.SkinPath + 'fck_strip.gif', 16, icon ] ;

	var oUIButton = this._UIButton = new FCKToolbarButtonUI( commandName, label, tooltip, oIcon, style ) ;
	oUIButton._FCKToolbarPanelButton = this ;
	oUIButton.ShowArrow = true ;
	oUIButton.OnClick = FCKToolbarPanelButton_OnButtonClick ;
}

FCKToolbarPanelButton.prototype.TypeName = 'FCKToolbarPanelButton' ;

FCKToolbarPanelButton.prototype.Create = function( parentElement )
{
	parentElement.className += 'Menu' ;

	this._UIButton.Create( parentElement ) ;

	var oPanel = FCK.ToolbarSet.CurrentInstance.Commands.GetCommand( this.CommandName )._Panel ;
	this.RegisterPanel( oPanel ) ;
}

FCKToolbarPanelButton.prototype.RegisterPanel = function( oPanel )
{
	if ( oPanel._FCKToolbarPanelButton )
		return ;

	oPanel._FCKToolbarPanelButton = this ;

	var eLineDiv = oPanel.Document.body.appendChild( oPanel.Document.createElement( 'div' ) ) ;
	eLineDiv.style.position = 'absolute' ;
	eLineDiv.style.top = '0px' ;

	var eLine = oPanel._FCKToolbarPanelButtonLineDiv = eLineDiv.appendChild( oPanel.Document.createElement( 'IMG' ) ) ;
	eLine.className = 'TB_ConnectionLine' ;
	eLine.style.position = 'absolute' ;

	eLine.src = FCK_SPACER_PATH ;

	oPanel.OnHide = FCKToolbarPanelButton_OnPanelHide ;
}

function FCKToolbarPanelButton_OnButtonClick( toolbarButton )
{
	var oButton = this._FCKToolbarPanelButton ;
	var e = oButton._UIButton.MainElement ;

	oButton._UIButton.ChangeState( FCK_TRISTATE_ON ) ;



	var oCommand = FCK.ToolbarSet.CurrentInstance.Commands.GetCommand( oButton.CommandName ) ;
	var oPanel = oCommand._Panel ;
	oPanel._FCKToolbarPanelButtonLineDiv.style.width = ( e.offsetWidth - 2 ) + 'px' ;
	oCommand.Execute( 0, e.offsetHeight - 1, e ) ;
}

function FCKToolbarPanelButton_OnPanelHide()
{
	var oMenuButton = this._FCKToolbarPanelButton ;
	oMenuButton._UIButton.ChangeState( FCK_TRISTATE_OFF ) ;
}

FCKToolbarPanelButton.prototype.RefreshState	= FCKToolbarButton.prototype.RefreshState ;
FCKToolbarPanelButton.prototype.Enable			= FCKToolbarButton.prototype.Enable ;
FCKToolbarPanelButton.prototype.Disable			= FCKToolbarButton.prototype.Disable ;