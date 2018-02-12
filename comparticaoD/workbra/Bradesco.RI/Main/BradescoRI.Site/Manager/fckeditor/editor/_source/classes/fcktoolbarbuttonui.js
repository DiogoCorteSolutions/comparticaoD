var FCKToolbarButtonUI = function( name, label, tooltip, iconPathOrStripInfoArray, style, state )
{
	this.Name		= name ;
	this.Label		= label || name ;
	this.Tooltip	= tooltip || this.Label ;
	this.Style		= style || FCK_TOOLBARITEM_ONLYICON ;
	this.State		= state || FCK_TRISTATE_OFF ;

	this.Icon = new FCKIcon( iconPathOrStripInfoArray ) ;

	if ( FCK.IECleanup )
		FCK.IECleanup.AddItem( this, FCKToolbarButtonUI_Cleanup ) ;
}


FCKToolbarButtonUI.prototype._CreatePaddingElement = function( document )
{
	var oImg = document.createElement( 'IMG' ) ;
	oImg.className = 'TB_Button_Padding' ;
	oImg.src = FCK_SPACER_PATH ;
	return oImg ;
}

FCKToolbarButtonUI.prototype.Create = function( parentElement )
{
	var oDoc = FCKTools.GetElementDocument( parentElement ) ;


	var oMainElement = this.MainElement = oDoc.createElement( 'DIV' ) ;
	oMainElement.title = this.Tooltip ;


	if ( FCKBrowserInfo.IsGecko )
		 oMainElement.onmousedown	= FCKTools.CancelEvent ;

	FCKTools.AddEventListenerEx( oMainElement, 'mouseover', FCKToolbarButtonUI_OnMouseOver, this ) ;
	FCKTools.AddEventListenerEx( oMainElement, 'mouseout', FCKToolbarButtonUI_OnMouseOut, this ) ;
	FCKTools.AddEventListenerEx( oMainElement, 'click', FCKToolbarButtonUI_OnClick, this ) ;

	this.ChangeState( this.State, true ) ;

	if ( this.Style == FCK_TOOLBARITEM_ONLYICON && !this.ShowArrow )
	{


		oMainElement.appendChild( this.Icon.CreateIconElement( oDoc ) ) ;
	}
	else
	{



		var oTable = oMainElement.appendChild( oDoc.createElement( 'TABLE' ) ) ;
		oTable.cellPadding = 0 ;
		oTable.cellSpacing = 0 ;

		var oRow = oTable.insertRow(-1) ;


		var oCell = oRow.insertCell(-1) ;

		if ( this.Style == FCK_TOOLBARITEM_ONLYICON || this.Style == FCK_TOOLBARITEM_ICONTEXT )
			oCell.appendChild( this.Icon.CreateIconElement( oDoc ) ) ;
		else
			oCell.appendChild( this._CreatePaddingElement( oDoc ) ) ;

		if ( this.Style == FCK_TOOLBARITEM_ONLYTEXT || this.Style == FCK_TOOLBARITEM_ICONTEXT )
		{

			oCell = oRow.insertCell(-1) ;
			oCell.className = 'TB_Button_Text' ;
			oCell.noWrap = true ;
			oCell.appendChild( oDoc.createTextNode( this.Label ) ) ;
		}

		if ( this.ShowArrow )
		{
			if ( this.Style != FCK_TOOLBARITEM_ONLYICON )
			{

				oRow.insertCell(-1).appendChild( this._CreatePaddingElement( oDoc ) ) ;
			}

			oCell = oRow.insertCell(-1) ;
			var eImg = oCell.appendChild( oDoc.createElement( 'IMG' ) ) ;
			eImg.src	= FCKConfig.SkinPath + 'images/toolbar.buttonarrow.gif' ;
			eImg.width	= 5 ;
			eImg.height	= 3 ;
		}


		oCell = oRow.insertCell(-1) ;
		oCell.appendChild( this._CreatePaddingElement( oDoc ) ) ;
	}

	parentElement.appendChild( oMainElement ) ;
}

FCKToolbarButtonUI.prototype.ChangeState = function( newState, force )
{
	if ( !force && this.State == newState )
		return ;

	var e = this.MainElement ;


	if ( !e )
		return ;

	switch ( parseInt( newState, 10 ) )
	{
		case FCK_TRISTATE_OFF :
			e.className		= 'TB_Button_Off' ;
			break ;

		case FCK_TRISTATE_ON :
			e.className		= 'TB_Button_On' ;
			break ;

		case FCK_TRISTATE_DISABLED :
			e.className		= 'TB_Button_Disabled' ;
			break ;
	}

	this.State = newState ;
}

function FCKToolbarButtonUI_OnMouseOver( ev, button )
{
	if ( button.State == FCK_TRISTATE_OFF )
		this.className = 'TB_Button_Off_Over' ;
	else if ( button.State == FCK_TRISTATE_ON )
		this.className = 'TB_Button_On_Over' ;
}

function FCKToolbarButtonUI_OnMouseOut( ev, button )
{
	if ( button.State == FCK_TRISTATE_OFF )
		this.className = 'TB_Button_Off' ;
	else if ( button.State == FCK_TRISTATE_ON )
		this.className = 'TB_Button_On' ;
}

function FCKToolbarButtonUI_OnClick( ev, button )
{
	if ( button.OnClick && button.State != FCK_TRISTATE_DISABLED )
		button.OnClick( button ) ;
}

function FCKToolbarButtonUI_Cleanup()
{


	this.MainElement = null ;
}