var FCKSpellCheckCommand = function()
{
	this.Name = 'SpellCheck' ;
	this.IsEnabled = true ;
}

FCKSpellCheckCommand.prototype.Execute = function()
{
	switch ( FCKConfig.SpellChecker )
	{
		case 'ieSpell' :
			this._RunIeSpell() ;
			break ;

		case 'SpellerPages' :
			FCKDialog.OpenDialog( 'FCKDialog_SpellCheck', 'Spell Check', 'dialog/fck_spellerpages.html', 440, 480 ) ;
			break ;

		case 'WSC' :
			FCKDialog.OpenDialog( 'FCKDialog_SpellCheck', 'Spell Check', 'wsc/w.html', 530, 480 ) ;
	}
}

FCKSpellCheckCommand.prototype._RunIeSpell = function()
{
	try
	{
		var oIeSpell = new ActiveXObject( "ieSpell.ieSpellExtension" ) ;
		oIeSpell.CheckAllLinkedDocuments( FCK.EditorDocument ) ;
	}
	catch( e )
	{
		if( e.number == -2146827859 )
		{
			if ( confirm( FCKLang.IeSpellDownload ) )
				window.open( FCKConfig.IeSpellDownloadUrl , 'IeSpellDownload' ) ;
		}
		else
			alert( 'Error Loading ieSpell: ' + e.message + ' (' + e.number + ')' ) ;
	}
}

FCKSpellCheckCommand.prototype.GetState = function()
{
	if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
		return FCK_TRISTATE_DISABLED ;
	return this.IsEnabled ? FCK_TRISTATE_OFF : FCK_TRISTATE_DISABLED ;
}