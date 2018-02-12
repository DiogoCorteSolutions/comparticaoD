 var FCKCoreStyleCommand = function( coreStyleName )
 {
 	this.Name = 'CoreStyle' ;
 	this.StyleName = '_FCK_' + coreStyleName ;
 	this.IsActive = false ;

 	FCKStyles.AttachStyleStateChange( this.StyleName, this._OnStyleStateChange, this ) ;
 }

 FCKCoreStyleCommand.prototype =
 {
	Execute : function()
	{
		FCKUndo.SaveUndoStep() ;

		if ( this.IsActive )
			FCKStyles.RemoveStyle( this.StyleName ) ;
		else
			FCKStyles.ApplyStyle( this.StyleName ) ;

		FCK.Focus() ;
		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
	},

	GetState : function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return FCK_TRISTATE_DISABLED ;
		return this.IsActive ? FCK_TRISTATE_ON : FCK_TRISTATE_OFF ;
	},

	_OnStyleStateChange : function( styleName, isActive )
	{
		this.IsActive = isActive ;
	}
 };