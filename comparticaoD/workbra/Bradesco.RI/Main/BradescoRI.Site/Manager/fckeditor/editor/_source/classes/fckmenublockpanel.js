var FCKMenuBlockPanel = function()
{
	FCKMenuBlock.call( this ) ;
}

FCKMenuBlockPanel.prototype = new FCKMenuBlock() ;

FCKMenuBlockPanel.prototype.Create = function()
{
	var oPanel = this.Panel = ( this.Parent && this.Parent.Panel ? this.Parent.Panel.CreateChildPanel() : new FCKPanel() ) ;
	oPanel.AppendStyleSheet( FCKConfig.SkinEditorCSS ) ;

	FCKMenuBlock.prototype.Create.call( this, oPanel.MainNode ) ;
}

FCKMenuBlockPanel.prototype.Show = function( x, y, relElement )
{
	if ( !this.Panel.CheckIsOpened() )
		this.Panel.Show( x, y, relElement ) ;
}

FCKMenuBlockPanel.prototype.Hide = function()
{
	if ( this.Panel.CheckIsOpened() )
		this.Panel.Hide() ;
}