<%@ WebHandler Language="C#" Class="MenuLinkExtraHandler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

public class MenuLinkExtraHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["_culture"];
        int intIdiomaId = 1;

        if (cookie != null)
            intIdiomaId = Convert.ToInt32(cookie.Value);

        List<MenuLinkExtra> objListLinks = DOMenuLinkExtra.Listar(0,intIdiomaId);
            
        JavaScriptSerializer js = new JavaScriptSerializer();

        context.Response.Write(js.Serialize(objListLinks));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}