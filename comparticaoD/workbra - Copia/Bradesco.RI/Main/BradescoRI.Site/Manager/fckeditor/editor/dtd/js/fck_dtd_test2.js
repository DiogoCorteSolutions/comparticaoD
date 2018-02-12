for ( var p in FCK.DTD )
{
	document.write( '<tr><td><b>' + p + '</b></td><td>' ) ;

	var isFirst = true ;

	for ( var c in FCK.DTD[p] )
	{
		if ( !isFirst )
			document.write( ', ' ) ;
		isFirst = false ;

		document.write( c ) ;
	}


	document.write( '</td></tr>' ) ;
}