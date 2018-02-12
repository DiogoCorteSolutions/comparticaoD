using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
public partial class Modulos_CMS_Modulos_ModMenuCircular_MenuCircular : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    #endregion

    #region Eventos
    protected void rptMenuCircular_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            MenuCircular item = (MenuCircular)e.Item.DataItem;
            Image imgImagem = (Image)e.Item.FindControl("imgImagem");

            imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircular"], item.IdMenuCircular, item.Arquivo);

            if (!string.IsNullOrWhiteSpace(item.Tooltip))
                imgImagem.ToolTip = item.Tooltip;


        }
    }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<MenuCircular> lstMenuCircular = DOModMenuCircular.ListarModMenuCircular(IdConteudo, IdIdioma);

        if (lstMenuCircular.Count == 0)
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
        else
        {
            rptMenuCircular.DataSource = lstMenuCircular;
            rptMenuCircular.DataBind();

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }

    }
    #endregion
}