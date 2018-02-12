using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTexto_Texto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        ModTexto objModtexto = DOModTexto.Obter(IdConteudo, IdIdioma);

        if (!string.IsNullOrWhiteSpace(objModtexto.Conteudo))
        {
            litConteudoHtml.Text = objModtexto.Conteudo;
            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
            litConteudoHtml.Text = string.Empty;
        }

    }
    #endregion
}