var FCKDocumentProcessor = new Object() ;
FCKDocumentProcessor._Items = new Array() ;

FCKDocumentProcessor.AppendNew = function()
{
	var oNewItem = new Object() ;
	this._Items.push( oNewItem ) ;
	return oNewItem ;
}

FCKDocumentProcessor.Process = function( document )
{
	var bIsDirty = FCK.IsDirty() ;
	var oProcessor, i = 0 ;
	while( ( oProcessor = this._Items[i++] ) )
		oProcessor.ProcessDocument( document ) ;
	if ( !bIsDirty )
		FCK.ResetIsDirty() ;
}

var FCKDocumentProcessor_CreateFakeImage = function( fakeClass, realElement )
{
	var oImg = FCKTools.GetElementDocument( realElement ).createElement( 'IMG' ) ;
	oImg.className = fakeClass ;
	oImg.src = FCKConfig.BasePath + 'images/spacer.gif' ;
	oImg.setAttribute( '_fckfakelement', 'true', 0 ) ;
	oImg.setAttribute( '_fckrealelement', FCKTempBin.AddElement( realElement ), 0 ) ;
	return oImg ;
}


if ( FCKBrowserInfo.IsIE || FCKBrowserInfo.IsOpera )
{
	var FCKAnchorsProcessor = FCKDocumentProcessor.AppendNew() ;
	FCKAnchorsProcessor.ProcessDocument = function( document )
	{
		var aMenuCircular = document.getElementsByTagName( 'A' ) ;

		var oLink ;
		var i = aMenuCircular.length - 1 ;
		while ( i >= 0 && ( oLink = aMenuCircular[i--] ) )
		{

			if ( oLink.name.length > 0 )
			{

				if ( oLink.innerHTML !== '' )
				{
					if ( FCKBrowserInfo.IsIE )
						oLink.className += ' FCK__AnchorC' ;
				}
				else
				{
					var oImg = FCKDocumentProcessor_CreateFakeImage( 'FCK__Anchor', oLink.cloneNode(true) ) ;
					oImg.setAttribute( '_fckanchor', 'true', 0 ) ;

					oLink.parentNode.insertBefore( oImg, oLink ) ;
					oLink.parentNode.removeChild( oLink ) ;
				}
			}
		}
	}
}


var FCKPageBreaksProcessor = FCKDocumentProcessor.AppendNew() ;
FCKPageBreaksProcessor.ProcessDocument = function( document )
{
	var aDIVs = document.getElementsByTagName( 'DIV' ) ;

	var eDIV ;
	var i = aDIVs.length - 1 ;
	while ( i >= 0 && ( eDIV = aDIVs[i--] ) )
	{
		if ( eDIV.style.pageBreakAfter == 'always' && eDIV.childNodes.length == 1 && eDIV.childNodes[0].style && eDIV.childNodes[0].style.display == 'none' )
		{
			var oFakeImage = FCKDocumentProcessor_CreateFakeImage( 'FCK__PageBreak', eDIV.cloneNode(true) ) ;

			eDIV.parentNode.insertBefore( oFakeImage, eDIV ) ;
			eDIV.parentNode.removeChild( eDIV ) ;
		}
	}
}


var FCKEmbedAndObjectProcessor = (function()
{
	var customProcessors = [] ;

	var processElement = function( el )
	{
		var clone = el.cloneNode( true ) ;
		var replaceElement ;
		var fakeImg = replaceElement = FCKDocumentProcessor_CreateFakeImage( 'FCK__UnknownObject', clone ) ;
		FCKEmbedAndObjectProcessor.RefreshView( fakeImg, el ) ;

		for ( var i = 0 ; i < customProcessors.length ; i++ )
			replaceElement = customProcessors[i]( el, replaceElement ) || replaceElement ;

		if ( replaceElement != fakeImg )
			FCKTempBin.RemoveElement( fakeImg.getAttribute( '_fckrealelement' ) ) ;

		el.parentNode.replaceChild( replaceElement, el ) ;
	}

	var processElementsByName = function( elementName, doc )
	{
		var aObjects = doc.getElementsByTagName( elementName );
		for ( var i = aObjects.length - 1 ; i >= 0 ; i-- )
			processElement( aObjects[i] ) ;
	}

	var processObjectAndEmbed = function( doc )
	{
		processElementsByName( 'object', doc );
		processElementsByName( 'embed', doc );
	}

	return FCKTools.Merge( FCKDocumentProcessor.AppendNew(),
		       {
				ProcessDocument : function( doc )
				{


					if ( FCKBrowserInfo.IsGecko )
						FCKTools.RunFunction( processObjectAndEmbed, this, [ doc ] ) ;
					else
						processObjectAndEmbed( doc ) ;
				},

				RefreshView : function( placeHolder, original )
				{
					if ( original.getAttribute( 'width' ) > 0 )
						placeHolder.style.width = FCKTools.ConvertHtmlSizeToStyle( original.getAttribute( 'width' ) ) ;

					if ( original.getAttribute( 'height' ) > 0 )
						placeHolder.style.height = FCKTools.ConvertHtmlSizeToStyle( original.getAttribute( 'height' ) ) ;
				},

				AddCustomHandler : function( func )
				{
					customProcessors.push( func ) ;
				}
			} ) ;
} )() ;

FCK.GetRealElement = function( fakeElement )
{
	var e = FCKTempBin.Elements[ fakeElement.getAttribute('_fckrealelement') ] ;

	if ( fakeElement.getAttribute('_fckflash') )
	{
		if ( fakeElement.style.width.length > 0 )
				e.width = FCKTools.ConvertStyleSizeToHtml( fakeElement.style.width ) ;

		if ( fakeElement.style.height.length > 0 )
				e.height = FCKTools.ConvertStyleSizeToHtml( fakeElement.style.height ) ;
	}

	return e ;
}




if ( FCKBrowserInfo.IsIE )
{
	FCKDocumentProcessor.AppendNew().ProcessDocument = function( document )
	{
		var aHRs = document.getElementsByTagName( 'HR' ) ;

		var eHR ;
		var i = aHRs.length - 1 ;
		while ( i >= 0 && ( eHR = aHRs[i--] ) )
		{

			var newHR = document.createElement( 'hr' ) ;
			newHR.mergeAttributes( eHR, true ) ;


			FCKDomTools.InsertAfterNode( eHR, newHR ) ;

			eHR.parentNode.removeChild( eHR ) ;
		}
	}
}


FCKDocumentProcessor.AppendNew().ProcessDocument = function( document )
{
	var aInputs = document.getElementsByTagName( 'INPUT' ) ;

	var oInput ;
	var i = aInputs.length - 1 ;
	while ( i >= 0 && ( oInput = aInputs[i--] ) )
	{
		if ( oInput.type == 'hidden' )
		{
			var oImg = FCKDocumentProcessor_CreateFakeImage( 'FCK__InputHidden', oInput.cloneNode(true) ) ;
			oImg.setAttribute( '_fckinputhidden', 'true', 0 ) ;

			oInput.parentNode.insertBefore( oImg, oInput ) ;
			oInput.parentNode.removeChild( oInput ) ;
		}
	}
}


FCKEmbedAndObjectProcessor.AddCustomHandler( function( el, fakeImg )
	{
		if ( ! ( el.nodeName.IEquals( 'embed' ) && ( el.type == 'application/x-shockwave-flash' || /\.swf($|#|\?)/i.test( el.src ) ) ) )
			return ;
		fakeImg.className = 'FCK__Flash' ;
		fakeImg.setAttribute( '_fckflash', 'true', 0 );
	} ) ;


if ( FCKBrowserInfo.IsSafari )
{
	FCKDocumentProcessor.AppendNew().ProcessDocument = function( doc )
	{
		var spans = doc.getElementsByClassName ?
			doc.getElementsByClassName( 'Apple-style-span' ) :
			Array.prototype.filter.call(
					doc.getElementsByTagName( 'span' ),
					function( item ){ return item.className == 'Apple-style-span' ; }
					) ;
		for ( var i = spans.length - 1 ; i >= 0 ; i-- )
			FCKDomTools.RemoveNode( spans[i], true ) ;
	}
}