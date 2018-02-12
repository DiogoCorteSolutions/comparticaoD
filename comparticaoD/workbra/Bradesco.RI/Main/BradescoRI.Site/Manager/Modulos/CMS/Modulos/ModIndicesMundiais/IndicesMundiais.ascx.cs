using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModIndicesMundiais_IndicesMundiais : System.Web.UI.UserControl
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
        List<ModIndicesMundiais> lstIndices = DOModIndicesMundiais.Listar();

        rptAcao.DataSource = lstIndices;
        rptAcao.DataBind();

        Label lblDataAtualizacao = (Label)rptAcao.Controls[0].Controls[0].FindControl("lblDataAtualizacao");

        CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

        lblDataAtualizacao.Text = (from i in lstIndices select i).OrderByDescending(i => i.Data).FirstOrDefault().Data.ToString("d", ci);
    }
    #endregion
}