﻿var oListManager = new Object() ;

oListManager.Clear = function()
{
	document.body.innerHTML = '' ;
}

function ProtectPath(path)
{
	path = path.replace( /\\/g, '\\\\') ;
	path = path.replace( /'/g, '\\\'') ;
	return path ;
}

oListManager.GetFolderRowHtml = function( folderName, folderPath )
{

	var sLink = '<a href="#" onclick="OpenFolder(\'' + ProtectPath( folderPath ) + '\');return false;">' ;

	return '<tr>' +
			'<td width="16">' +
				sLink +
				'<img alt="" src="images/Folder.gif" width="16" height="16" border="0"><\/a>' +
			'<\/td><td nowrap colspan="2">&nbsp;' +
				sLink +
				folderName +
				'<\/a>' +
		'<\/td><\/tr>' ;
}

oListManager.GetFileRowHtml = function( fileName, fileUrl, fileSize )
{

	var sLink = '<a href="#" onclick="OpenFile(\'' + ProtectPath( fileUrl ) + '\');return false;">' ;


	var sIcon = oIcons.GetIcon( fileName ) ;

	return '<tr>' +
			'<td width="16">' +
				sLink +
				'<img alt="" src="images/icons/' + sIcon + '.gif" width="16" height="16" border="0"><\/a>' +
			'<\/td><td>&nbsp;' +
				sLink +
				fileName +
				'<\/a>' +
			'<\/td><td align="right" nowrap>&nbsp;' +
				fileSize +
				' KB' +
		'<\/td><\/tr>' ;
}

function OpenFolder( folderPath )
{

	window.parent.frames['frmFolders'].LoadFolders( folderPath ) ;
}

function OpenFile( fileUrl )
{
	window.top.opener.SetUrl( encodeURI( fileUrl ).replace( '#', '%23' ) ) ;
	window.top.close() ;
	window.top.opener.focus() ;
}

function LoadResources( resourceType, folderPath )
{
	oListManager.Clear() ;
	oConnector.ResourceType = resourceType ;
	oConnector.CurrentFolder = folderPath ;
	oConnector.SendCommand( 'GetFoldersAndFiles', null, GetFoldersAndFilesCallBack ) ;
}

function Refresh()
{
	LoadResources( oConnector.ResourceType, oConnector.CurrentFolder ) ;
}

function GetFoldersAndFilesCallBack( fckXml )
{
	if ( oConnector.CheckError( fckXml ) != 0 )
		return ;


	var oFolderNode = fckXml.SelectSingleNode( 'Connector/CurrentFolder' ) ;
	if ( oFolderNode == null )
	{
		alert( 'The server didn\'t reply with a proper XML data. Please check your configuration.' ) ;
		return ;
	}
	var sCurrentFolderPath	= oFolderNode.attributes.getNamedItem('path').value ;
	var sCurrentFolderUrl	= oFolderNode.attributes.getNamedItem('url').value ;



	var oHtml = new StringBuilder( '<table id="tableFiles" cellspacing="1" cellpadding="0" width="100%" border="0">' ) ;


	var oNodes ;
	oNodes = fckXml.SelectNodes( 'Connector/Folders/Folder' ) ;
	for ( var i = 0 ; i < oNodes.length ; i++ )
	{
		var sFolderName = oNodes[i].attributes.getNamedItem('name').value ;
		oHtml.Append( oListManager.GetFolderRowHtml( sFolderName, sCurrentFolderPath + sFolderName + "/" ) ) ;
	}


	oNodes = fckXml.SelectNodes( 'Connector/Files/File' ) ;
	for ( var j = 0 ; j < oNodes.length ; j++ )
	{
		var oNode = oNodes[j] ;
		var sFileName = oNode.attributes.getNamedItem('name').value ;
		var sFileSize = oNode.attributes.getNamedItem('size').value ;


		var oFileUrlAtt = oNodes[j].attributes.getNamedItem('url') ;
		var sFileUrl = oFileUrlAtt != null ? oFileUrlAtt.value : sCurrentFolderUrl + sFileName ;

		oHtml.Append( oListManager.GetFileRowHtml( sFileName, sFileUrl, sFileSize ) ) ;
	}

	oHtml.Append( '<\/table>' ) ;

	document.body.innerHTML = oHtml.ToString() ;



}

window.onload = function()
{
	window.top.IsLoadedResourcesList = true ;
}