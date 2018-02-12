/*
 * FCKeditor - The text editor for Internet - http://www.fckeditor.net
 * Copyright (C) 2003-2007 Frederico Caldeira Knabben
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 * Código da página connector.aspx
 * 
 */

using System ;
using System.Globalization ;
using System.Xml ;
using System.Web ;

namespace Controles.FCKeditor.FileBrowser
{
	public class Connector : FileWorkerBase
	{
		protected override void OnLoad(EventArgs e)
		{
			Config.LoadConfig();

			if ( !Config.Enabled )
			{
				XmlResponseHandler.SendError( Response, 1, "This connector is disabled. Please check the \"editor/filemanager/connectors/aspx/config.ascx\" file." );
				return;
			}

			// Busca informações iniciais.
			string sCommand = Request.QueryString["Command"] ;
			string sResourceType = Request.QueryString[ "Type" ];
			string sCurrentFolder = Request.QueryString[ "CurrentFolder" ];

			if ( sCommand == null || sResourceType == null || sCurrentFolder == null )
			{
				XmlResponseHandler.SendError( Response, 1, "Invalid request." );
				return;
			}

			// Verifica se é um tipo permitido.
			if ( !Config.CheckIsTypeAllowed( sResourceType ) )
			{
				XmlResponseHandler.SendError( Response, 1, "Invalid resource type specified." ) ;
				return ;
			}

			// Verificar a sintaxe do caminho.
			if ( ! sCurrentFolder.EndsWith( "/" ) )
				sCurrentFolder += "/" ;
			if ( ! sCurrentFolder.StartsWith( "/" ) )
				sCurrentFolder = "/" + sCurrentFolder ;

			// Verifica caminhos inválidos (..).
			if ( sCurrentFolder.IndexOf( ".." ) >= 0 || sCurrentFolder.IndexOf( "\\" ) >= 0 )
			{
				XmlResponseHandler.SendError( Response, 102, "" );
				return;
			}

            // Upload de arquivo não tem que retornar o XML, por isso deve ser interceptados antes de qualquer coisa.
			if ( sCommand == "FileUpload" )
			{
				this.FileUpload( sResourceType, sCurrentFolder, false ) ;
				return ;
			}

			XmlResponseHandler oResponseHandler = new XmlResponseHandler( this, Response );
			XmlNode oConnectorNode = oResponseHandler.CreateBaseXml( sCommand, sResourceType, sCurrentFolder );

			// Execute comando.
			switch( sCommand )
			{
				case "GetFolders" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "GetFoldersAndFiles" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					this.GetFiles( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "CreateFolder" :
					this.CreateFolder( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
			}

			oResponseHandler.SendResponse();
		}

		#region Command Handlers

		private void GetFolders( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			// Mapeia o caminho virtual no servidor local.
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder, false ) ;

			// Cria nodo da pasta.
			XmlNode oFoldersNode = XmlUtil.AppendElement( connectorNode, "Folders" ) ;

			System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo( sServerDir ) ;
			System.IO.DirectoryInfo[] aSubDirs = oDir.GetDirectories() ;

			for ( int i = 0 ; i < aSubDirs.Length ; i++ )
			{
                // Cria nodo da pasta.
				XmlNode oFolderNode = XmlUtil.AppendElement( oFoldersNode, "Folder" ) ;
				XmlUtil.SetAttribute( oFolderNode, "name", aSubDirs[i].Name ) ;
			}
		}

		private void GetFiles( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			// Map the virtual path to the local server path.
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder, false ) ;

			// Create the "Files" node.
			XmlNode oFilesNode = XmlUtil.AppendElement( connectorNode, "Files" ) ;

			System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo( sServerDir ) ;
			System.IO.FileInfo[] aFiles = oDir.GetFiles() ;

			for ( int i = 0 ; i < aFiles.Length ; i++ )
			{
				Decimal iFileSize = Math.Round( (Decimal)aFiles[i].Length / 1024 ) ;
				if ( iFileSize < 1 && aFiles[i].Length != 0 ) iFileSize = 1 ;

				// Cria nodo de arquivo.
				XmlNode oFileNode = XmlUtil.AppendElement( oFilesNode, "File" ) ;
				XmlUtil.SetAttribute( oFileNode, "name", aFiles[i].Name ) ;
				XmlUtil.SetAttribute( oFileNode, "size", iFileSize.ToString( CultureInfo.InvariantCulture ) ) ;
			}
		}

		private void CreateFolder( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			string sErrorNumber = "0" ;

			string sNewFolderName = Request.QueryString["NewFolderName"] ;
			sNewFolderName = this.SanitizeFolderName( sNewFolderName );

			if ( sNewFolderName == null || sNewFolderName.Length == 0 )
				sErrorNumber = "102" ;
			else
			{
				// Mapeia o caminho virtual no servidor local da pasta atual.
				string sServerDir = this.ServerMapFolder( resourceType, currentFolder, false ) ;

				try
				{
					Util.CreateDirectory( System.IO.Path.Combine( sServerDir, sNewFolderName )) ;
				}
				catch ( ArgumentException )
				{
					sErrorNumber = "102" ;
				}
				catch ( System.IO.PathTooLongException )
				{
					sErrorNumber = "102" ;
				}
				catch ( System.IO.IOException )
				{
					sErrorNumber = "101" ;
				}
				catch ( System.Security.SecurityException )
				{
					sErrorNumber = "103" ;
				}
				catch ( Exception )
				{
					sErrorNumber = "110" ;
				}
			}

			// Cria nodo de erro.
			XmlNode oErrorNode = XmlUtil.AppendElement( connectorNode, "Error" ) ;
			XmlUtil.SetAttribute( oErrorNode, "number", sErrorNumber ) ;
		}


		#endregion

		#region Directory Mapping

		internal string GetUrlFromPath( string resourceType, string folderPath )
		{
			if ( resourceType == null || resourceType.Length == 0 )
				return this.Config.UserFilesPath.TrimEnd( '/' ) + folderPath;
			else
				return this.Config.TypeConfig[ resourceType ].GetFilesPath().TrimEnd( '/' ) + folderPath;
		}

		#endregion
	}
}
