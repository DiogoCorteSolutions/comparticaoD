using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModPerfilAdr_PerfilAdr : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    #endregion

    #region Métodos
    
    private void ObterConteudo()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<ModPerfilAdr> lstobjPerfilAdr = DOModPerfilAdr.Listar(IdIdioma);
        
        if(lstobjPerfilAdr.Any())
        {
            rptAcao.DataSource = lstobjPerfilAdr;
            rptAcao.DataBind();
        }
    }
    #endregion
}