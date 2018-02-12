using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModRelatorio_Relatorio : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    #endregion


    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        ConteudoPagina conteudo = new ConteudoPagina() { ConteudoId = IdConteudo };

        ModRelatorio modRelatorio = new ModRelatorio() { Conteudo = conteudo };

        List<ModRelatorio> lModRelatorio = DOModRelatorio.Listar(modRelatorio);
        
        System.Text.StringBuilder sbTipoRelartorio = new System.Text.StringBuilder();
        sbTipoRelartorio.Append("<ul>");
        foreach (ModRelatorio mRelatorio in lModRelatorio)
        {
            sbTipoRelartorio.Append("<li>");
            sbTipoRelartorio.Append("<a href='#'>");
            sbTipoRelartorio.Append(DOTipoArquivo.Obter(new TipoArquivo() { Id = mRelatorio.TipoRelatorio.ID }).Descricao); 
            sbTipoRelartorio.Append("</a>");
            sbTipoRelartorio.Append("</li>");
        }
        sbTipoRelartorio.Append("</ul>");

        ModComunicado modComunicado = new ModComunicado() { ConteudoId = conteudo.ConteudoId };
        List<ModComunicado> lModComunicado = DOModComunicado.Listar(modComunicado);

        System.Text.StringBuilder sbTipoComunicado = new System.Text.StringBuilder();
        sbTipoComunicado.Append("<ul>");
        foreach (ModComunicado mComunicado in lModComunicado)
        {
            sbTipoComunicado.Append("<li>");
            sbTipoComunicado.Append("<a href='#'>");
            sbTipoComunicado.Append(DoComunicado.Obter(new Comunicado() { ID = mComunicado.ComunicadoId }).Titulo);
            sbTipoComunicado.Append("</a>");
            sbTipoComunicado.Append("</li>");
        }
        sbTipoComunicado.Append("</ul>");

        litComunicado.Text = sbTipoComunicado.ToString();
        litTipoRelatorio.Text = sbTipoRelartorio.ToString();

        if (lModRelatorio.Count > 0 || lModComunicado.Count > 0)
        {
            divSemConteudo.Visible = false;
            divComCOnteudo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            divComCOnteudo.Visible = false;

        }

    }


}