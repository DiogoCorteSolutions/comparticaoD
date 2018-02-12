using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;

/// <summary>
/// Summary description for UserContext
/// </summary>
public class UserContext
{

    public static Usuario UsuarioLogado
    {
        get
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                           
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            Usuario objUsuario = null;

            if (authCookie == null)
            {
               // return null;
                return DOUsuario.ObterUsuarioId(Convert.ToInt32(ticket.Name));
            }

            if (System.Web.HttpContext.Current.Cache["UsuarioLogado"] == null)
                System.Web.HttpContext.Current.Cache["UsuarioLogado"] = DOUsuario.ObterUsuarioId(Convert.ToInt32(ticket.Name));
            else
                objUsuario = (Usuario)System.Web.HttpContext.Current.Cache["UsuarioLogado"];

            return objUsuario;
        }
        set
        {
            if (value == null)
                System.Web.HttpContext.Current.Cache.Remove("UsuarioLogado");
            else
            {
                System.Web.HttpContext.Current.Cache["UsuarioLogado"] = value;
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(480));
            }
        }
    }

    public static bool Logado
    {
        get
        {
            return System.Web.HttpContext.Current.Cache["UsuarioLogado"] != null;            
        }
    }

}