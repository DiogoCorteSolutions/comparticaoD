using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModEventos_ListaEventos : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        if (Request.QueryString["EventoId"] != null)
        {
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                IdIdioma = Convert.ToInt32(cookie.Value);

            Evento objEvento = DOModEvento.Obter(Convert.ToInt32(Request.QueryString["EventoId"]), IdIdioma);

            if (objEvento != null)
            {
                lblTitulo.Text = objEvento.Titulo;

                string siglaCultura = DOIdioma.ObterSigla(IdIdioma);

                if (objEvento.DataFim != null && objEvento.DataFim > DateTime.MinValue)
                {                    
                    if (siglaCultura.Equals("pt-BR"))
                        lblData.Text = (objEvento.DataInicio.ToString("MM").Equals(objEvento.DataFim.ToString("MM")) ? objEvento.DataInicio.ToString("dd") + " a " + objEvento.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)) : objEvento.DataInicio.ToString("dd 'de' MMMM", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)) + " a " + objEvento.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)));
                    else
                        lblData.Text = (objEvento.DataInicio.ToString("MM").Equals(objEvento.DataFim.ToString("MM")) ? objEvento.DataInicio.ToString("dd") + " a " + objEvento.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)) : objEvento.DataInicio.ToString("dd 'de' MMMM", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)) + " a " + objEvento.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura)));

                }
                else
                {
                    if (siglaCultura.Equals("pt-BR"))
                        lblData.Text = objEvento.DataInicio.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura));
                    else
                        lblData.Text = objEvento.DataInicio.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(siglaCultura));
                }
               lblResponsavel.Text = objEvento.Responsavel;
                lblLocal.Text = objEvento.Local;
                lblTexto.Text = objEvento.Texto;
            }
        }
    }
    #endregion
}