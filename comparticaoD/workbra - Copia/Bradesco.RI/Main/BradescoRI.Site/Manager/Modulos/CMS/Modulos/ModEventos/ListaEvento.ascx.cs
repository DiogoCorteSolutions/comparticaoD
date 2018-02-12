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

        if (Request.QueryString["IdEvento"] != null)
        {
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                IdIdioma = Convert.ToInt32(cookie.Value);
           
            Evento objEvento = DOModEvento.Obter(Convert.ToInt32(Request.QueryString["IdEvento"]), IdIdioma);

            if (objEvento != null)
            {
                lblTitulo.Text = objEvento.Titulo;
                //lblData.Text = objEvento.Data.ToString("dd/MM/yyyy");
                //lblHora.Text = objEvento.Hora;
                lblResponsavel.Text = objEvento.Responsavel;
                lblLocal.Text = objEvento.Local;
                lblTexto.Text = objEvento.Texto;

                divSemConteudo.Visible = false;
                divConteudo.Visible = true;                
            }
            else
            {
                divSemConteudo.Visible = true;
                divConteudo.Visible = false;                
            }
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }

    }
    #endregion
}