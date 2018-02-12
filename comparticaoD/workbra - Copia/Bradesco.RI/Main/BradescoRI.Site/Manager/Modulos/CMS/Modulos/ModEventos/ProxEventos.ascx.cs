using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModEventos_ProxEventos : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    #endregion

    #region Eventos
    protected void rptProximosEventos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ProxEventos item = (ProxEventos)e.Item.DataItem;

            Label lblData = (Label)e.Item.FindControl("lblData");

                lblData.Text = item.DataInicio.ToString("dd 'de' MMMM", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
        }
    }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<ProxEventos> lstProxEventos = DOModEvento.ListarProxEventos(IdIdioma);

        rptProximosEventos.DataSource = lstProxEventos;
        rptProximosEventos.DataBind();

    }
    #endregion
}