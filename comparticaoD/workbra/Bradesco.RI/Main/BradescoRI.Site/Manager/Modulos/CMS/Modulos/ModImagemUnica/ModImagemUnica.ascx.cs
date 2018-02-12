using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Modulos_CMS_Modulos_ModImagemUnica_ModImagemUnica : System.Web.UI.UserControl
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

        ModImagemUnica objModImagemUnica = DOModImagemUnica.Obter(IdConteudo, IdIdioma);

        if (objModImagemUnica.Arquivo != null)
        {
            // Montagem da Imagem
            imgImagemModulo.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModImagemUnica"], IdConteudo, objModImagemUnica.Arquivo);
            imgImagemModulo.ToolTip = objModImagemUnica.Tooltip;

            //Montagem dos textos
            if (!String.IsNullOrWhiteSpace(objModImagemUnica.Texto1))
            {
                lblTexto1.Text = objModImagemUnica.Texto1;
                divTraco.Visible = true;
            }
            if (!String.IsNullOrWhiteSpace(objModImagemUnica.Texto2))
                lblTexto2.Text = objModImagemUnica.Texto2;
            if (!String.IsNullOrWhiteSpace(objModImagemUnica.Texto3))
                lblTexto3.Text = objModImagemUnica.Texto3;
            if (!String.IsNullOrWhiteSpace(objModImagemUnica.TextoUrl))
            {
                lblTextoUrl.Text = objModImagemUnica.TextoUrl;
                linkImagem.Visible = true;
            }
            // Montagem do link
            if (!String.IsNullOrWhiteSpace(objModImagemUnica.Target))
                linkImagem.Target = objModImagemUnica.Target;

            if (!String.IsNullOrWhiteSpace(objModImagemUnica.Url))
                linkImagem.HRef = objModImagemUnica.Url;
            
            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
            imgImagemModulo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
            imgImagemModulo.Visible = false;
        }

    }
    #endregion
}