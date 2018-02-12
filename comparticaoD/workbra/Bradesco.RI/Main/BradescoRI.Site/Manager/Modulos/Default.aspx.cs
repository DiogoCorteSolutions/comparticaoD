using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserContext.Logado)
        {
            Response.Redirect("~/Default.aspx");
        }

        var usuario = UserContext.UsuarioLogado;
        lblUsuarioLogado.Text = usuario.Nome;
        
    }
}