var FCKToolbarSpecialCombo = function()
{
	this.SourceView			= false ;
	this.ContextSensitive	= true ;
	this.FieldWidth			= null ;
	this.PanelWidth			= null ;
	this.PanelMaxHeight		= null ;

}

FCKToolbarSpecialCombo.prototype.DefaultLabel = '' ;

function FCKToolbarSpecialCombo_OnSelect( itemId, item )
{
	FCK.ToolbarSet.CurrentInstance.Commands.GetCommand( this.CommandName ).Execute( itemId, item ) ;
}

FCKToolbarSpecialCombo.prototype.Create = function( targetElement )
{
	this._Combo = new FCKSpecialCombo( this.GetLabel(), this.FieldWidth, this.PanelWidth, this.PanelMaxHeight, FCKBrowserInfo.IsIE ? window : FCKTools.GetElementWindow( targetElement ).parent ) ;

	this._Combo.Tooltip	= this.Tooltip ;
	this._Combo.Style	= this.Style ;

	this.CreateItems( this._Combo ) ;

	this._Combo.Create( targetElement ) ;

	this._Combo.CommandName = this.CommandName ;

	this._Combo.OnSelect = FCKToolbarSpecialCombo_OnSelect ;
}

function FCKToolbarSpecialCombo_RefreshActiveItems( combo, value )
{
	combo.DeselectAll() ;
	combo.SelectItem( value ) ;
	combo.SetLabelById( value ) ;
}

FCKToolbarSpecialCombo.prototype.RefreshState = function()
{

	var eState ;





		var sValue = FCK.ToolbarSet.CurrentInstance.Commands.GetCommand( this.CommandName ).GetState() ;



		if ( sValue != FCK_TRISTATE_DISABLED )
		{
			eState = FCK_TRISTATE_ON ;

			if ( this.RefreshActiveItems )
				this.RefreshActiveItems( this._Combo, sValue ) ;
			else
			{
				if ( this._LastValue !== sValue)
				{
					this._LastValue = sValue ;

					if ( !sValue || sValue.length == 0 )
					{
						this._Combo.DeselectAll() ;
						this._Combo.SetLabel( this.DefaultLabel ) ;
					}
					else
						FCKToolbarSpecialCombo_RefreshActiveItems( this._Combo, sValue ) ;
				}
			}
		}
		else
			eState = FCK_TRISTATE_DISABLED ;



	if ( eState == this.State ) return ;

	if ( eState == FCK_TRISTATE_DISABLED )
	{
		this._Combo.DeselectAll() ;
		this._Combo.SetLabel( '' ) ;
	}


	this.State = eState ;


	this._Combo.SetEnabled( eState != FCK_TRISTATE_DISABLED ) ;
}

FCKToolbarSpecialCombo.prototype.Enable = function()
{
	this.RefreshState() ;
}

FCKToolbarSpecialCombo.prototype.Disable = function()
{
	this.State = FCK_TRISTATE_DISABLED ;
	this._Combo.DeselectAll() ;
	this._Combo.SetLabel( '' ) ;
	this._Combo.SetEnabled( false ) ;
}
