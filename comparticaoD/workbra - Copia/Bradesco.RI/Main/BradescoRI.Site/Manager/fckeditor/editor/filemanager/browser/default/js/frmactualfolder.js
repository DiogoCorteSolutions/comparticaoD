(function()
{
	var d = document.domain ;

	while ( true )
	{

		try
		{
			var test = window.top.opener.document.domain ;
			break ;
		}
		catch( e )
		{}


		d = d.replace( /.*?(?:\.|$)/, '' ) ;

		if ( d.length == 0 )
			break ;

		try
		{
			document.domain = d ;
		}
		catch (e)
		{
			break ;
		}
	}
})() ;

function SetCurrentFolder( resourceType, folderPath )
{
	document.getElementById('tdName').innerHTML = folderPath ;
}

window.onload = function()
{
	window.top.IsLoadedActualFolder = true ;
}