String.prototype.Contains = function( textToCheck )
{
	return ( this.indexOf( textToCheck ) > -1 ) ;
}

String.prototype.Equals = function()
{
	var aArgs = arguments ;

	if ( aArgs.length == 1 && aArgs[0].pop )
		aArgs = aArgs[0] ;

	for ( var i = 0 ; i < aArgs.length ; i++ )
	{
		if ( this == aArgs[i] )
			return true ;
	}
	return false ;
}

String.prototype.IEquals = function()
{
	var thisUpper = this.toUpperCase() ;

	var aArgs = arguments ;

	if ( aArgs.length == 1 && aArgs[0].pop )
		aArgs = aArgs[0] ;

	for ( var i = 0 ; i < aArgs.length ; i++ )
	{
		if ( thisUpper == aArgs[i].toUpperCase() )
			return true ;
	}
	return false ;
}

String.prototype.ReplaceAll = function( searchArray, replaceArray )
{
	var replaced = this ;

	for ( var i = 0 ; i < searchArray.length ; i++ )
	{
		replaced = replaced.replace( searchArray[i], replaceArray[i] ) ;
	}

	return replaced ;
}

String.prototype.StartsWith = function( value )
{
	return ( this.substr( 0, value.length ) == value ) ;
}

String.prototype.EndsWith = function( value, ignoreCase )
{
	var L1 = this.length ;
	var L2 = value.length ;

	if ( L2 > L1 )
		return false ;

	if ( ignoreCase )
	{
		var oRegex = new RegExp( value + '$' , 'i' ) ;
		return oRegex.test( this ) ;
	}
	else
		return ( L2 == 0 || this.substr( L1 - L2, L2 ) == value ) ;
}

String.prototype.Remove = function( start, length )
{
	var s = '' ;

	if ( start > 0 )
		s = this.substring( 0, start ) ;

	if ( start + length < this.length )
		s += this.substring( start + length , this.length ) ;

	return s ;
}

String.prototype.Trim = function()
{
	return this.replace( /(^[ \t\n\r]*)|([ \t\n\r]*$)/g, '' ) ;
}

String.prototype.LTrim = function()
{
	return this.replace( /^[ \t\n\r]*/g, '' ) ;
}

String.prototype.RTrim = function()
{
	return this.replace( /[ \t\n\r]*$/g, '' ) ;
}

String.prototype.ReplaceNewLineChars = function( replacement )
{
	return this.replace( /\n/g, replacement ) ;
}

String.prototype.Replace = function( regExp, replacement, thisObj )
{
	if ( typeof replacement == 'function' )
	{
		return this.replace( regExp,
			function()
			{
				return replacement.apply( thisObj || this, arguments ) ;
			} ) ;
	}
	else
		return this.replace( regExp, replacement ) ;
}

Array.prototype.IndexOf = function( value )
{
	for ( var i = 0 ; i < this.length ; i++ )
	{
		if ( this[i] == value )
			return i ;
	}
	return -1 ;
}