<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=System.Configuration.ConfigurationManager.AppSettings("title")%></title>
    <link rel="stylesheet" href="/erro/erro.css" type="text/css" />
</head>
<body>
    <table border="0" width="100%">
        <tr>
            <td style="padding:25px;"><h1><%=System.Configuration.ConfigurationManager.AppSettings("title")%></h1></td>
        </tr>
        <tr>
            <td style="padding:25px;"><h2><%=System.Configuration.ConfigurationManager.AppSettings("msgErro")%></h2></td>
        </tr>
        <tr>
            <td style="padding:25px;">
                <div class="box">
			        <p>
			            <a href='<%=System.Configuration.ConfigurationManager.AppSettings("Link")%>' title='<%=System.Configuration.ConfigurationManager.AppSettings("dsLink")%>'>
<%=System.Configuration.ConfigurationManager.AppSettings("dsLink")%></a>

			             <%=System.Configuration.ConfigurationManager.AppSettings("msgLink")%>
			       </p>
			    </div>
            </td>
        </tr>
	</table>
</body>        
    
</html>
