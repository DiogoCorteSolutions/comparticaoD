using System;
using System.Web;
using System.Web.UI;
using System.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        dynamic enabledSsl = ConfigurationManager.AppSettings["SSL"];

        if (enabledSsl != null)
        {
            if (enabledSsl.Equals("on") && Request.IsSecureConnection == false & Request.Url.ToString().IndexOf("https://") < 0)
            {
                dynamic url = HttpContext.Current.Response.ApplyAppPathModifier(HttpContext.Current.Request.Url.AbsolutePath);
                dynamic baseUrl = "https://";
                string sslHost = null;

                sslHost = HttpContext.Current.Request.Url.Host;
                baseUrl = string.Concat(baseUrl, sslHost);
               Response.Redirect((new Uri(new Uri(baseUrl), url)).ToString(), true);
            }
        }

        string addr;
        addr = Request.ServerVariables["REMOTE_ADDR"];

        //Response.Write(addr);

        if ((addr.IndexOf("10.") < 0) &&
             (addr.IndexOf("200.246.233.") < 0) &&
             (addr.IndexOf("200.206.167.211") < 0) &&
             (addr.IndexOf("200.99.132.190") < 0) &&
             (addr.IndexOf("200.162.55.59") < 0) &&
             (addr.IndexOf("200.206.167.211") < 0) &&
             (addr.IndexOf("200.246.210.") < 0)
             )
        {
            //sResponse.Redirect("http://www.bradesco.com.br");
            //Response.End();
        }
    }
}
