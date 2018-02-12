using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModGlossario_Glossario : System.Web.UI.UserControl
{
    #region Variáveis
    public int IdConteudo { get; set; }

    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObterConteudo();
    }

    protected void btnFiltro_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            string letra = btn.Text;

            List<Glossario> list = (List<Glossario>)Session["sGlossario"];

            rptGlossario.DataSource = list.Where(x => x.Titulo.StartsWith(letra)).ToList();
            rptGlossario.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Metodos


    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));

        var objDadosModulo = DOModGlossario.Listar(new ModGlossario() { ConteudoId = IdConteudo });

        var lGlossario = new List<Glossario>();

        foreach (ModGlossario item in objDadosModulo)
            lGlossario.Add(DOGlossario.Obter(new Glossario() { Id = item.GlossarioId }));

        Session["sGlossario"] = lGlossario;
        if (lGlossario.Count > 0)
        {
            divSemConteudo.Visible = false;
            divConteudo.Visible = true;

            rptGlossario.DataSource = lGlossario;
            rptGlossario.DataBind();
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
    }
    #endregion



}