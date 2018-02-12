var FCKConfig = oEditor.FCKConfig ;

var sBasePath	= FCKConfig.SmileyPath ;
var aImages		= FCKConfig.SmileyImages ;
var iCols		= FCKConfig.SmileyColumns ;
var iColWidth	= parseInt( 100 / iCols, 10 ) ;

var i = 0 ;
while (i < aImages.length)
{
	document.write( '<tr>' ) ;
	for(var j = 0 ; j < iCols ; j++)
	{
		if (aImages[i])
		{
			var sUrl = sBasePath + aImages[i] ;
			document.write( '<td width="' + iColWidth + '%" align="center" class="DarkBackground Hand" onclick="InsertSmiley(\'' + sUrl.replace(/'/g, "\\'" ) + '\')" onmouseover="over(this)" onmouseout="out(this)">' ) ;
			document.write( '<img src="' + sUrl + '" border="0" />' ) ;
		}
		else
			document.write( '<td width="' + iColWidth + '%" class="DarkBackground">&nbsp;' ) ;
		document.write( '<\/td>' ) ;
		i++ ;
	}
	document.write('<\/tr>') ;
}