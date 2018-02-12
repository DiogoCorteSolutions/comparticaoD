using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Modulos_CMS_Modulos_Header_Header : System.Web.UI.UserControl
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

        ModHeader objModHeader = DOModHeader.Obter(IdConteudo);

        if (objModHeader.Arquivo != null)
        {
            imgImagemModulo.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModHeader"], IdConteudo, objModHeader.Arquivo);
            lblTitulo.CssClass = "tit-header2";
            lblSubtitulo.CssClass = "c";
        }

        if (Request.QueryString["paginaId"] != null)
        {
            Breadcrumb objBreadcrumb = DOPagina.ObterBreadcrumb(Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"])));

            if (objBreadcrumb == null ||string.IsNullOrEmpty(objBreadcrumb.Titulo))
            {
                divSemConteudo.Visible = true;
                divConteudo.Visible = false;
            }
            else
            {
                lblTitulo.Text = objBreadcrumb.Titulo;
                lblSubtitulo.Text = objBreadcrumb.Descricao;

                litBreadcrumb.Text = string.Format("<li class=\"breadcrumb-item\"><a class=\"{0}\" href=\"Default.aspx\">Home</a></li>", (objModHeader.Arquivo != null ? "tit - header2": string.Empty));

                string[] itens;

                foreach (string breads in objBreadcrumb.Breadcrumbs.Split('|'))
                {
                    if (!string.IsNullOrEmpty(breads))
                    {
                        itens = breads.Split('_');
                        litBreadcrumb.Text += string.Format("<li class=\"breadcrumb-item\"><a class=\"{0}\" href=\"{1}\">{2}</a></li>", (objModHeader.Arquivo != null ? "tit - header2" : string.Empty),itens[1], itens[0]);
                    }
                }

                divSemConteudo.Visible = false;
                divConteudo.Visible = true;
            }
        }
    }
    
    #endregion
}