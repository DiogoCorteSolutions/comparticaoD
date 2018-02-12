FCKTools.CancelEvent = function( e )
{
	if ( e )
		e.preventDefault() ;
}

FCKTools.DisableSelection = function( element )
{
	if ( FCKBrowserInfo.IsGecko )
		element.style.MozUserSelect		= 'none' ;
	else if ( FCKBrowserInfo.IsSafari )
		element.style.KhtmlUserSelect	= 'none' ;
	else
		element.style.userSelect		= 'none' ;
}


FCKTools._AppendStyleSheet = function( documentElement, cssFileUrl )
{
	var e = documentElement.createElement( 'LINK' ) ;
	e.rel	= 'stylesheet' ;
	e.type	= 'text/css' ;
	e.href	= cssFileUrl ;
	documentElement.getElementsByTagName("HEAD")[0].appendChild( e ) ;
	return e ;
}


FCKTools.AppendStyleString = function( documentElement, cssStyles )
{
	if ( !cssStyles )
		return null ;

	var e = documentElement.createElement( "STYLE" ) ;
	e.appendChild( documentElement.createTextNode( cssStyles ) ) ;
	documentElement.getElementsByTagName( "HEAD" )[0].appendChild( e ) ;
	return e ;
}


FCKTools.ClearElementAttributes = function( element )
{

	for ( var i = 0 ; i < element.attributes.length ; i++ )
	{

		element.removeAttribute( element.attributes[i].name, 0 ) ;
	}
}


FCKTools.GetAllChildrenIds = function( parentElement )
{

	var aIds = new Array() ;


	var fGetIds = function( parent )
	{
		for ( var i = 0 ; i < parent.childNodes.length ; i++ )
		{
			var sId = parent.childNodes[i].id ;


			if ( sId && sId.length > 0 ) aIds[ aIds.length ] = sId ;


			fGetIds( parent.childNodes[i] ) ;
		}
	}


	fGetIds( parentElement ) ;

	return aIds ;
}



FCKTools.RemoveOuterTags = function( e )
{
	var oFragment = e.ownerDocument.createDocumentFragment() ;

	for ( var i = 0 ; i < e.childNodes.length ; i++ )
		oFragment.appendChild( e.childNodes[i].cloneNode(true) ) ;

	e.parentNode.replaceChild( oFragment, e ) ;
}

FCKTools.CreateXmlObject = function( object )
{
	switch ( object )
	{
		case 'XmlHttp' :
			return new XMLHttpRequest() ;

		case 'DOMDocument' :




			var doc = ( new DOMParser() ).parseFromString( '<tmp></tmp>', 'text/xml' ) ;
			FCKDomTools.RemoveNode( doc.firstChild ) ;
			return doc ;
	}
	return null ;
}

FCKTools.GetScrollPosition = function( relativeWindow )
{
	return { X : relativeWindow.pageXOffset, Y : relativeWindow.pageYOffset } ;
}

FCKTools.AddEventListener = function( sourceObject, eventName, listener )
{
	sourceObject.addEventListener( eventName, listener, false ) ;
}

FCKTools.RemoveEventListener = function( sourceObject, eventName, listener )
{
	sourceObject.removeEventListener( eventName, listener, false ) ;
}


FCKTools.AddEventListenerEx = function( sourceObject, eventName, listener, paramsArray )
{
	sourceObject.addEventListener(
		eventName,
		function( e )
		{
			listener.apply( sourceObject, [ e ].concat( paramsArray || [] ) ) ;
		},
		false
	) ;
}


FCKTools.GetViewPaneSize = function( win )
{
	return { Width : win.innerWidth, Height : win.innerHeight } ;
}

FCKTools.SaveStyles = function( element )
{
	var data = FCKTools.ProtectFormStyles( element ) ;

	var oSavedStyles = new Object() ;

	if ( element.className.length > 0 )
	{
		oSavedStyles.Class = element.className ;
		element.className = '' ;
	}

	var sInlineStyle = element.getAttribute( 'style' ) ;

	if ( sInlineStyle && sInlineStyle.length > 0 )
	{
		oSavedStyles.Inline = sInlineStyle ;
		element.setAttribute( 'style', '', 0 ) ;	// 0 : Case Insensitive
	}

	FCKTools.RestoreFormStyles( element, data ) ;
	return oSavedStyles ;
}

FCKTools.RestoreStyles = function( element, savedStyles )
{
	var data = FCKTools.ProtectFormStyles( element ) ;
	element.className = savedStyles.Class || '' ;

	if ( savedStyles.Inline )
		element.setAttribute( 'style', savedStyles.Inline, 0 ) ;	// 0 : Case Insensitive
	else
		element.removeAttribute( 'style', 0 ) ;
	FCKTools.RestoreFormStyles( element, data ) ;
}

FCKTools.RegisterDollarFunction = function( targetWindow )
{
	targetWindow.$ = function( id )
	{
		return targetWindow.document.getElementById( id ) ;
	} ;
}

FCKTools.AppendElement = function( target, elementName )
{
	return target.appendChild( target.ownerDocument.createElement( elementName ) ) ;
}




FCKTools.GetElementPosition = function( el, relativeWindow )
{

	var c = { X:0, Y:0 } ;

	var oWindow = relativeWindow || window ;

	var oOwnerWindow = FCKTools.GetElementWindow( el ) ;

	var previousElement = null ;

	while ( el )
	{
		var sPosition = oOwnerWindow.getComputedStyle(el, '').position ;



		if ( sPosition && sPosition != 'static' && el.style.zIndex != FCKConfig.FloatingPanelsZIndex )
			break ;

		c.X += el.offsetLeft - el.scrollLeft ;
		c.Y += el.offsetTop - el.scrollTop  ;



		if ( ! FCKBrowserInfo.IsOpera )
		{
			var scrollElement = previousElement ;
			while ( scrollElement && scrollElement != el )
			{
				c.X -= scrollElement.scrollLeft ;
				c.Y -= scrollElement.scrollTop ;
				scrollElement = scrollElement.parentNode ;
			}
		}

		previousElement = el ;
		if ( el.offsetParent )
			el = el.offsetParent ;
		else
		{
			if ( oOwnerWindow != oWindow )
			{
				el = oOwnerWindow.frameElement ;
				previousElement = null ;
				if ( el )
					oOwnerWindow = FCKTools.GetElementWindow( el ) ;
			}
			else
			{
				c.X += el.scrollLeft ;
				c.Y += el.scrollTop  ;
				break ;
			}
		}
	}


	return c ;
}