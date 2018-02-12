var FCKKeystrokeHandler = function( cancelCtrlDefaults )
{
	this.Keystrokes = new Object() ;
	this.CancelCtrlDefaults = ( cancelCtrlDefaults !== false ) ;
}

FCKKeystrokeHandler.prototype.AttachToElement = function( target )
{
	FCKTools.AddEventListenerEx( target, 'keydown', _FCKKeystrokeHandler_OnKeyDown, this ) ;
	if ( FCKBrowserInfo.IsGecko10 || FCKBrowserInfo.IsOpera || ( FCKBrowserInfo.IsGecko && FCKBrowserInfo.IsMac ) )
		FCKTools.AddEventListenerEx( target, 'keypress', _FCKKeystrokeHandler_OnKeyPress, this ) ;
}

FCKKeystrokeHandler.prototype.SetKeystrokes = function()
{
	for ( var i = 0 ; i < arguments.length ; i++ )
	{
		var keyDef = arguments[i] ;

		if ( !keyDef )
			continue ;

		if ( typeof( keyDef[0] ) == 'object' )
			this.SetKeystrokes.apply( this, keyDef ) ;
		else
		{
			if ( keyDef.length == 1 )
				delete this.Keystrokes[ keyDef[0] ] ;
			else
				this.Keystrokes[ keyDef[0] ] = keyDef[1] === true ? true : keyDef ;
		}
	}
}

function _FCKKeystrokeHandler_OnKeyDown( ev, keystrokeHandler )
{
	var keystroke = ev.keyCode || ev.which ;

	var keyModifiers = 0 ;

	if ( ev.ctrlKey || ev.metaKey )
		keyModifiers += CTRL ;

	if ( ev.shiftKey )
		keyModifiers += SHIFT ;

	if ( ev.altKey )
		keyModifiers += ALT ;

	var keyCombination = keystroke + keyModifiers ;

	var cancelIt = keystrokeHandler._CancelIt = false ;

	var keystrokeValue = keystrokeHandler.Keystrokes[ keyCombination ] ;

	if ( keystrokeValue )
	{
		if ( keystrokeValue === true || !( keystrokeHandler.OnKeystroke && keystrokeHandler.OnKeystroke.apply( keystrokeHandler, keystrokeValue ) ) )
			return true ;

		cancelIt = true ;
	}

	if ( cancelIt || ( keystrokeHandler.CancelCtrlDefaults && keyModifiers == CTRL && ( keystroke < 33 || keystroke > 40 ) ) )
	{
		keystrokeHandler._CancelIt = true ;

		if ( ev.preventDefault )
			return ev.preventDefault() ;

		ev.returnValue = false ;
		ev.cancelBubble = true ;
		return false ;
	}

	return true ;
}

function _FCKKeystrokeHandler_OnKeyPress( ev, keystrokeHandler )
{
	if ( keystrokeHandler._CancelIt )
	{
		if ( ev.preventDefault )
			return ev.preventDefault() ;

		return false ;
	}

	return true ;
}