using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTimeline_Timeline : System.Web.UI.UserControl
{

    #region Variáveis
    public int IdConteudo { get; set; }
    public int IdIdioma { get; set; }
    public int IdTimeline { get; set; }
    public int Ano { get; set; }
    public string Titulo { get; set; }
    public string Imagem { get; set; }
    public string Texto { get; set; }
    public static string Primeiro = "class=\"selected\"";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    //private void ObterConteudo()
    //{
    //    IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
    //    IdIdioma = 1;

    //    var cookie = Request.Cookies["_culture"];
    //    if (cookie != null)
    //        IdIdioma = Convert.ToInt32(cookie.Value);


    //    var objDados = DOTimeline.Listar(IdConteudo, IdIdioma);

    //    if (objDados != null)
    //    {
    //       // divSemConteudo.Visible = false;
    //        divConteudo.Visible = true;
    //        rptTimeline.DataSource = objDados;
    //        rptTimeline.DataBind();
    //        //rptEventos.DataSource = objDados;
    //        //rptEventos.DataBind();
    //    }
    //    else
    //    {
    //        //divSemConteudo.Visible = true;
    //        divConteudo.Visible = false;

    //    }

    //}

    public string GetFirst()
    {
        var retorno = Primeiro;
        Primeiro = string.Empty;
        return retorno;
    }

    private void ObterConteudo()
    {
        rptAcao.DataSource = DOTimeline.Listar();
        rptAcao.DataBind();
    }

}