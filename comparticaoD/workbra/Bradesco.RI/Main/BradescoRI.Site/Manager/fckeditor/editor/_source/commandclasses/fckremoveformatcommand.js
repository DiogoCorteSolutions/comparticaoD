﻿ var FCKRemoveFormatCommand = function()
 {
 	this.Name = 'RemoveFormat' ;
 }

 FCKRemoveFormatCommand.prototype =
 {
	Execute : function()
	{
		FCKStyles.RemoveAll() ;

		FCK.Focus() ;
		FCK.Events.FireEvent( 'OnSelectionChange' ) ;
	},

	GetState : function()
	{
		return FCK.EditorWindow ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
	}
 };