var FCKMenuItem = function( parentMenuBlock, name, label, iconPathOrStripInfoArray, isDisabled, customData )
{
	this.Name		= name ;
	this.Label		= label || name ;
	this.IsDisabled	= isDisabled ;

	this.Icon = new FCKIcon( iconPathOrStripInfoArray ) ;

	this.SubMenu			= new FCKMenuBlockPanel() ;
	this.SubMenu.Parent		= parentMenuBlock ;
	this.SubMenu.OnClick	= FCKTools.CreateEventListener( FCKMenuItem_SubMenu_OnClick, this ) ;
	this.CustomData = customData ;

	if ( FCK.IECleanup )
		FCK.IECleanup.AddItem( this, FCKMenuItem_Cleanup ) ;
}


FCKMenuItem.prototype.AddItem = function( name, label, iconPathOrStripInfoArrayOrIndex, isDisabled, customData )
{
	this.HasSubMenu = true ;
	return this.SubMenu.AddItem( name, label, iconPathOrStripInfoArrayOrIndex, isDisabled, customData ) ;
}

FCKMenuItem.prototype.AddSeparator = function()
{
	this.SubMenu.AddSeparator() ;
}

FCKMenuItem.prototype.Create = function( parentTable )
{
	var bHasSubMenu = this.HasSubMenu ;

	var oDoc = FCKTools.GetElementDocument( parentTable ) ;

	var r = this.MainElement = parentTable.insertRow(-1) ;
	r.className = this.IsDisabled ? 'MN_Item_Disabled' : 'MN_Item' ;

	if ( !this.IsDisabled )
	{
		FCKTools.AddEventListenerEx( r, 'mouseover', FCKMenuItem_OnMouseOver, [ this ] ) ;
		FCKTools.AddEventListenerEx( r, 'click', FCKMenuItem_OnClick, [ this ] ) ;

		if ( !bHasSubMenu )
			FCKTools.AddEventListenerEx( r, 'mouseout', FCKMenuItem_OnMouseOut, [ this ] ) ;
	}

	var eCell = r.insertCell(-1) ;
	eCell.className = 'MN_Icon' ;
	eCell.appendChild( this.Icon.CreateIconElement( oDoc ) ) ;

	eCell = r.insertCell(-1) ;
	eCell.className = 'MN_Label' ;
	eCell.noWrap = true ;
	eCell.appendChild( oDoc.createTextNode( this.Label ) ) ;

	eCell = r.insertCell(-1) ;
	if ( bHasSubMenu )
	{
		eCell.className = 'MN_Arrow' ;

		var eArrowImg = eCell.appendChild( oDoc.createElement( 'IMG' ) ) ;
		eArrowImg.src = FCK_IMAGES_PATH + 'arrow_' + FCKLang.Dir + '.gif' ;
		eArrowImg.width	 = 4 ;
		eArrowImg.height = 7 ;

		this.SubMenu.Create() ;
		this.SubMenu.Panel.OnHide = FCKTools.CreateEventListener( FCKMenuItem_SubMenu_OnHide, this ) ;
	}
}

FCKMenuItem.prototype.Activate = function()
{
	this.MainElement.className = 'MN_Item_Over' ;

	if ( this.HasSubMenu )
	{
		this.SubMenu.Show( this.MainElement.offsetWidth + 2, -2, this.MainElement ) ;
	}

	FCKTools.RunFunction( this.OnActivate, this ) ;
}

FCKMenuItem.prototype.Deactivate = function()
{
	this.MainElement.className = 'MN_Item' ;

	if ( this.HasSubMenu )
		this.SubMenu.Hide() ;
}

function FCKMenuItem_SubMenu_OnClick( clickedItem, listeningItem )
{
	FCKTools.RunFunction( listeningItem.OnClick, listeningItem, [ clickedItem ] ) ;
}

function FCKMenuItem_SubMenu_OnHide( menuItem )
{
	menuItem.Deactivate() ;
}

function FCKMenuItem_OnClick( ev, menuItem )
{
	if ( menuItem.HasSubMenu )
		menuItem.Activate() ;
	else
	{
		menuItem.Deactivate() ;
		FCKTools.RunFunction( menuItem.OnClick, menuItem, [ menuItem ] ) ;
	}
}

function FCKMenuItem_OnMouseOver( ev, menuItem )
{
	menuItem.Activate() ;
}

function FCKMenuItem_OnMouseOut( ev, menuItem )
{
	menuItem.Deactivate() ;
}

function FCKMenuItem_Cleanup()
{
	this.MainElement = null ;
}