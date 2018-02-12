function SetResourceType( type )
{
	window.parent.frames["frmFolders"].SetResourceType( type ) ;
}

var aTypes = [
	['File','File'],
	['Image','Image'],
	['Flash','Flash'],
	['Media','Media']
] ;

window.onload = function()
{
	var oCombo = document.getElementById('cmbType') ;
	oCombo.innerHTML = '' ;
	for ( var i = 0 ; i < aTypes.length ; i++ )
	{
		if ( oConnector.ShowAllTypes || aTypes[i][0] == oConnector.ResourceType )
			AddSelectOption( oCombo, aTypes[i][1], aTypes[i][0] ) ;
	}
}