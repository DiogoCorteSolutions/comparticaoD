FCKTools.CancelEvent = function( e )
{
	return false ;
}


FCKTools._AppendStyleSheet = function( documentElement, cssFileUrl )
{
	return documentElement.createStyleSheet( cssFileUrl ).owningElement ;
}


FCKTools.AppendStyleString = function( documentElement, cssStyles )
{
	if ( !cssStyles )
		return null ;

	var s = documentElement.createStyleSheet( "" ) ;
	s.cssText = cssStyles ;
	return s ;
}


FCKTools.ClearElementAttributes = function( element )
{
	element.clearAttributes() ;
}

FCKTools.GetAllChildrenIds = function( parentElement )
{
	var aIds = new Array() ;
	for ( var i = 0 ; i < parentElement.all.length ; i++ )
	{
		var sId = parentElement.all[i].id ;
		if ( sId && sId.length > 0 )
			aIds[ aIds.length ] = sId ;
	}
	return aIds ;
}

FCKTools.RemoveOuterTags = function( e )
{
	e.insertAdjacentHTML( 'beforeBegin', e.innerHTML ) ;
	e.parentNode.removeChild( e ) ;
}

FCKTools.CreateXmlObject = function( object )
{
	var aObjs ;

	switch ( object )
	{
		case 'XmlHttp' :

			if ( document.location.protocol != 'file:' )
				try { return new XMLHttpRequest() ; } catch (e) {}

			aObjs = [ 'MSXML2.XmlHttp', 'Microsoft.XmlHttp' ] ;
			break ;

		case 'DOMDocument' :
			aObjs = [ 'MSXML2.DOMDocument', 'Microsoft.XmlDom' ] ;
			break ;
	}

	for ( var i = 0 ; i < 2 ; i++ )
	{
		try { return new ActiveXObject( aObjs[i] ) ; }
		catch (e)
		{}
	}

	if ( FCKLang.NoActiveX )
	{
		alert( FCKLang.NoActiveX ) ;
		FCKLang.NoActiveX = null ;
	}
	return null ;
}

FCKTools.DisableSelection = function( element )
{
	element.unselectable = 'on' ;

	var e, i = 0 ;

	while ( (e = element.all[ i++ ]) )
	{
		switch ( e.tagName )
		{
			case 'IFRAME' :
			case 'TEXTAREA' :
			case 'INPUT' :
			case 'SELECT' :
				break ;
			default :
				e.unselectable = 'on' ;
		}
	}
}

FCKTools.GetScrollPosition = function( relativeWindow )
{
	var oDoc = relativeWindow.document ;


	var oPos = { X : oDoc.documentElement.scrollLeft, Y : oDoc.documentElement.scrollTop } ;

	if ( oPos.X > 0 || oPos.Y > 0 )
		return oPos ;


	return { X : oDoc.body.scrollLeft, Y : oDoc.body.scrollTop } ;
}

FCKTools.AddEventListener = function( sourceObject, eventName, listener )
{
	sourceObject.attachEvent( 'on' + eventName, listener ) ;
}

FCKTools.RemoveEventListener = function( sourceObject, eventName, listener )
{
	sourceObject.detachEvent( 'on' + eventName, listener ) ;
}


FCKTools.AddEventListenerEx = function( sourceObject, eventName, listener, paramsArray )
{

	var o = new Object() ;
	o.Source = sourceObject ;
	o.Params = paramsArray || [] ;
	o.Listener = function( ev )
	{
		return listener.apply( o.Source, [ ev ].concat( o.Params ) ) ;
	}

	if ( FCK.IECleanup )
		FCK.IECleanup.AddItem( null, function() { o.Source = null ; o.Params = null ; } ) ;

	sourceObject.attachEvent( 'on' + eventName, o.Listener ) ;

	sourceObject = null ;
	paramsArray = null ;
}


FCKTools.GetViewPaneSize = function( win )
{
	var oSizeSource ;

	var oDoc = win.document.documentElement ;
	if ( oDoc && oDoc.clientWidth )
		oSizeSource = oDoc ;
	else
		oSizeSource = win.document.body ;

	if ( oSizeSource )
		return { Width : oSizeSource.clientWidth, Height : oSizeSource.clientHeight } ;
	else
		return { Width : 0, Height : 0 } ;
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

	var sInlineStyle = element.style.cssText ;

	if ( sInlineStyle.length > 0 )
	{
		oSavedStyles.Inline = sInlineStyle ;
		element.style.cssText = '' ;
	}

	FCKTools.RestoreFormStyles( element, data ) ;
	return oSavedStyles ;
}

FCKTools.RestoreStyles = function( element, savedStyles )
{
	var data = FCKTools.ProtectFormStyles( element ) ;
	element.className		= savedStyles.Class || '' ;
	element.style.cssText	= savedStyles.Inline || '' ;
	FCKTools.RestoreFormStyles( element, data ) ;
}

FCKTools.RegisterDollarFunction = function( targetWindow )
{
	targetWindow.$ = targetWindow.document.getElementById ;
}

FCKTools.AppendElement = function( target, elementName )
{
	return target.appendChild( this.GetElementDocument( target ).createElement( elementName ) ) ;
}


FCKTools.ToLowerCase = function( strValue )
{
	return strValue.toLowerCase() ;
}