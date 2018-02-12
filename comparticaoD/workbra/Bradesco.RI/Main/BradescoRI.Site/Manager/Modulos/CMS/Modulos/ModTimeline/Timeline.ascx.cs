using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTimeline_Timeline : System.Web.UI.UserControl
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

        List<Timeline> lstCaixa = DOTimeline.Listar(IdConteudo, IdIdioma);

        if (lstCaixa.Count == 0)
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
        else
        {
            ddlAno.DataSource = lstCaixa.OrderBy(x => x.Ano).ToList();
            ddlAno.DataValueField = "Ano";
            ddlAno.DataTextField = "Ano";
            ddlAno.DataBind();

            MontaTimeLine(lstCaixa.OrderBy(x => x.Ano).ToList());

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }

    }

    private void MontaTimeLine(List<Timeline> lstCaixa)
    {
        System.Text.StringBuilder sbTimeLine = new System.Text.StringBuilder();

        try
        {
            System.Text.StringBuilder sbLitEvents = new System.Text.StringBuilder(); ;
            foreach (Timeline item in lstCaixa)
            {
                sbLitEvents.Append("<li>");
                sbLitEvents.Append("     <a href = '#0' data-date='01/01/" + item.Ano.ToString() +"'  class='selected'>" + item.Ano.ToString() + "<br>");
                sbLitEvents.Append("     <br>");
                sbLitEvents.Append("     <span>");
                sbLitEvents.Append(          item.Titulo);
                sbLitEvents.Append("     </span>");
                sbLitEvents.Append("     </a></li> ");
            }

            litEvents.Text = sbLitEvents.ToString();

            System.Text.StringBuilder sbContent = new System.Text.StringBuilder();
            foreach (Timeline item in lstCaixa)
            {
                //< li class="selected" data-date="01/01/1940">
                //            <div id = "events-destaque" class="events-destaque">1940</div>
                //            <div class="row">
                //                <div class="col-md-6 events-box">
                //                    <img class="events-img" src="img/imagem.gif" width="100%" />
                //                </div>
                //                <div class="col-md-6">
                //                    <h2>Década de 40</h2>
                //                    <p>
                //                        Com apenas oito anos de vida, em 1951, o Bradesco torna-se o maior Banco privado do Brasil.Nessa década, o Banco chega ao norte rural do Paraná e decide também erguer sua nova sede em Osasco.A construção da matriz inicia-se em 1953 e leva seis anos para ser concluída.Em 1956, é criada a Fundação Bradesco, com o objetivo de levar educação gratuita a crianças, jovens e adultos carentes.
                //                    </p>
                //                </div>
                //            </div>
                //        </li>

                sbContent.Append("         <li class='selected' data-date='01/01/" + item.Ano.ToString() + "' >");
                sbContent.Append("             <div id='events-destaque' class='events-destaque'>" + item.Ano.ToString() + "</div>");
                sbContent.Append("             <div class='row'>");
                sbContent.Append("                 <div class='col-md-6 events-box'>");
                sbContent.Append("                     <img class='events -img' src='" + item.Imagem + "'  width='100%' />");
                sbContent.Append("                 </div>");
                sbContent.Append("                 <div class='col -md-6'>");
                sbContent.Append("                     <h2>" + item.Titulo + "</h2>");
                sbContent.Append("                     <p>");
                sbContent.Append(                          item.Texto);
                sbContent.Append("                     </p>");
                sbContent.Append("                 </div>");
                sbContent.Append("             </div>");
                sbContent.Append("         </li>");
            }

            litContent.Text = sbContent.ToString();

            //litTimeLine.Text = sbTimeLine.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion



}