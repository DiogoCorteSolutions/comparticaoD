<%@ WebHandler Language="C#" Class="MenuHandler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

public class MenuHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
         HttpCookie cookie = HttpContext.Current.Request.Cookies["_culture"];
        int intIdiomaId = 1;

        if (cookie != null)
            intIdiomaId = Convert.ToInt32(cookie.Value);

        List<Menu> objListMenus = DOMenu.ListarCultura(intIdiomaId);

        List<Menu> menutree = GetMenuTree(objListMenus, "");

        JavaScriptSerializer js = new JavaScriptSerializer();

        context.Response.Write(js.Serialize(menutree));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private List<Menu> GetMenuTree(List<Menu> plistMenu, string parentId)
    {

        return plistMenu.Where(x => x.HierarquiaPai == parentId).Select(x => new Menu()
        {
            ID = x.ID,
            ChaveNome = x.ChaveNome,
            CssClass = (x.CssClass == null ? "" : x.CssClass),
            Hierarquia = x.Hierarquia,
            IdiomaId = x.IdiomaId,
            Nome = x.Nome,
            Target = x.Target,
            Url = x.Url,
            ItensMenu = GetMenuTree(plistMenu, x.Hierarquia)
        }).ToList();
    }
}