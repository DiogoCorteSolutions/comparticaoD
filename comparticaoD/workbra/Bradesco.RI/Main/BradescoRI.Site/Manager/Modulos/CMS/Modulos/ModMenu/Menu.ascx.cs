using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Modulos_CMS_Modulos_ModMenu_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    private void ObterConteudo()
    {
        if (Request.QueryString["paginaId"] != null)
        {
            int paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"]));
            hdnCorMenu.Value = DOPagina.ObterCorMenu(paginaId);
        }
    }
}