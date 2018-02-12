var oEditor		= window.parent.InnerDialogLoaded() ;
var FCK			= oEditor.FCK ;
var FCKLang		= oEditor.FCKLang ;
var FCKConfig	= oEditor.FCKConfig ;

window.onload = function()
{

	GetE('eList').style.height = document.all ? '100%' : '295px' ;


	oEditor.FCKLanguageManager.TranslatePage(document) ;

	GetE('xChkReplaceAll').checked = ( FCKConfig.TemplateReplaceAll !== false ) ;

	if ( FCKConfig.TemplateReplaceCheckbox !== false )
		GetE('xReplaceBlock').style.display = '' ;

	window.parent.SetAutoSize( true ) ;

	LoadTemplatesXml() ;
}

function LoadTemplatesXml()
{
	var oTemplate ;

	if ( !FCK._Templates )
	{
		GetE('eLoading').style.display = '' ;


		FCK._Templates = new Array() ;


		var oXml = new oEditor.FCKXml() ;
		oXml.LoadUrl( FCKConfig.TemplatesXmlPath ) ;


		var oAtt = oXml.SelectSingleNode( 'Templates/@imagesBasePath' ) ;
		var sImagesBasePath = oAtt ? oAtt.value : '' ;


		var aTplNodes = oXml.SelectNodes( 'Templates/Template' ) ;

		for ( var i = 0 ; i < aTplNodes.length ; i++ )
		{
			var oNode = aTplNodes[i] ;

			oTemplate = new Object() ;

			var oPart ;


			if ( (oPart = oNode.attributes.getNamedItem('title')) )
				oTemplate.Title = oPart.value ;
			else
				oTemplate.Title = 'Template ' + ( i + 1 ) ;


			if ( (oPart = oXml.SelectSingleNode( 'Description', oNode )) )
				oTemplate.Description = oPart.text ? oPart.text : oPart.textContent ;


			if ( (oPart = oNode.attributes.getNamedItem('image')) )
				oTemplate.Image = sImagesBasePath + oPart.value ;


			if ( (oPart = oXml.SelectSingleNode( 'Html', oNode )) )
				oTemplate.Html = oPart.text ? oPart.text : oPart.textContent ;
			else
			{
				alert( 'No HTML defined for template index ' + i + '. Please review the "' + FCKConfig.TemplatesXmlPath + '" file.' ) ;
				continue ;
			}

			FCK._Templates[ FCK._Templates.length ] = oTemplate ;
		}

		GetE('eLoading').style.display = 'none' ;
	}

	if ( FCK._Templates.length == 0 )
		GetE('eEmpty').style.display = '' ;
	else
	{
		for ( var j = 0 ; j < FCK._Templates.length ; j++ )
		{
			oTemplate = FCK._Templates[j] ;

			var oItemDiv = GetE('eList').appendChild( document.createElement( 'DIV' ) ) ;
			oItemDiv.TplIndex = j ;
			oItemDiv.className = 'TplItem' ;


			var sInner = '<table><tr>' ;

			if ( oTemplate.Image )
				sInner += '<td valign="top"><img src="' + oTemplate.Image + '"><\/td>' ;

			sInner += '<td valign="top"><div class="TplTitle">' + oTemplate.Title + '<\/div>' ;

			if ( oTemplate.Description )
				sInner += '<div>' + oTemplate.Description + '<\/div>' ;

			sInner += '<\/td><\/tr><\/table>' ;

			oItemDiv.innerHTML = sInner ;

			oItemDiv.onmouseover = ItemDiv_OnMouseOver ;
			oItemDiv.onmouseout = ItemDiv_OnMouseOut ;
			oItemDiv.onclick = ItemDiv_OnClick ;
		}
	}
}

function ItemDiv_OnMouseOver()
{
	this.className += ' PopupSelectionBox' ;
}

function ItemDiv_OnMouseOut()
{
	this.className = this.className.replace( /\s*PopupSelectionBox\s*/, '' ) ;
}

function ItemDiv_OnClick()
{
	SelectTemplate( this.TplIndex ) ;
}

function SelectTemplate( index )
{
	oEditor.FCKUndo.SaveUndoStep() ;

	if ( GetE('xChkReplaceAll').checked )
		FCK.SetData( FCK._Templates[index].Html ) ;
	else
		FCK.InsertHtml( FCK._Templates[index].Html ) ;

	window.parent.Cancel( true ) ;
}