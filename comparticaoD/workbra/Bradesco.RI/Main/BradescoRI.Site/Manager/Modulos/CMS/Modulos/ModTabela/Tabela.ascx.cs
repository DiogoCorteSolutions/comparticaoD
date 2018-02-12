using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTabela_Tabela : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variaveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    #endregion

    #region Eventos
    protected void rptTabela_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Tabela item = (Tabela)e.Item.DataItem;
        }
    }
    #endregion

    #region Métodos
    //private void ObterConteudo()
    //{
    //    IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
    //    IdIdioma = 1;

    //    HttpCookie cookie = Request.Cookies["_culture"];
    //    if (cookie != null)
    //        IdIdioma = Convert.ToInt32(cookie.Value);

    //    List<Tabela> listTabela = DOModTabela.Listar();

    //    if (listTabela.Count == 0)
    //    {
    //        divSemConteudo.Visible = true;
    //        divConteudo.Visible = false;
    //    }
    //    else
    //    {
    //        rptTabela.DataSource = listTabela;
    //        rptTabela.DataBind();

    //        divSemConteudo.Visible = false;
    //        divConteudo.Visible = true;
    //    }

    //}
    private void ObterConteudo()
    {
        rptTabela.DataSource = DOModTabela.Listar();
        rptTabela.DataBind();
    }
    #endregion

}