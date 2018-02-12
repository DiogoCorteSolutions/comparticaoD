function SetCurrentFolder( resourceType, folderPath )
{
	oConnector.ResourceType = resourceType ;
	oConnector.CurrentFolder = folderPath ;
}

function CreateFolder()
{
	var sFolderName ;

	while ( true )
	{
		sFolderName = prompt( 'Type the name of the new folder:', '' ) ;

		if ( sFolderName == null )
			return ;
		else if ( sFolderName.length == 0 )
			alert( 'Please type the folder name' ) ;
		else
			break ;
	}

	oConnector.SendCommand( 'CreateFolder', 'NewFolderName=' + encodeURIComponent( sFolderName) , CreateFolderCallBack ) ;
}

function CreateFolderCallBack( fckXml )
{
	if ( oConnector.CheckError( fckXml ) == 0 )
		window.parent.frames['frmResourcesList'].Refresh() ;
}

window.onload = function()
{
	window.top.IsLoadedCreateFolder = true ;
}