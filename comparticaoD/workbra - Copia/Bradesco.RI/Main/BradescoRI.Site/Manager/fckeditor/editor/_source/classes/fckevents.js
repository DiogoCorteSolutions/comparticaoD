var FCKEvents = function( eventsOwner )
{
	this.Owner = eventsOwner ;
	this._RegisteredEvents = new Object() ;
}

FCKEvents.prototype.AttachEvent = function( eventName, functionPointer )
{
	var aTargets ;

	if ( !( aTargets = this._RegisteredEvents[ eventName ] ) )
		this._RegisteredEvents[ eventName ] = [ functionPointer ] ;
	else
	{
		if ( aTargets.IndexOf( functionPointer ) == -1 )
			aTargets.push( functionPointer ) ;
	}
}

FCKEvents.prototype.FireEvent = function( eventName, params )
{
	var bReturnValue = true ;

	var oCalls = this._RegisteredEvents[ eventName ] ;

	if ( oCalls )
	{
		for ( var i = 0 ; i < oCalls.length ; i++ )
		{
			try
			{
				bReturnValue = ( oCalls[ i ]( this.Owner, params ) && bReturnValue ) ;
			}
			catch(e)
			{
				if ( e.number != -2146823277 )
					throw e ;
			}
		}
	}

	return bReturnValue ;
}