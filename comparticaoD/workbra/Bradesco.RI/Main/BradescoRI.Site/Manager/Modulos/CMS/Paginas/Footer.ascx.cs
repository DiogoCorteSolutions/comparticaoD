using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Paginas_Footer : System.Web.UI.UserControl
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Carregar();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Métodos
    private void Carregar()
    {
        Footer objFooter = null;
        pnlConteudo.Visible = false;
        pnlSemConteudo.Visible = false;
        try
        {
            objFooter = new Footer() { StatusId = (int)Utilitarios.StatusPagina.Publicado};
            objFooter = DOFooter.Obter(objFooter);

            if (objFooter.Id > 0)
            {
                PreencherTela(objFooter);
                pnlConteudo.Visible = true;
            }
            else
                pnlSemConteudo.Visible = true;


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PreencherTela(Footer objFooter)
    {
        try
        {
            lblTituloN1.Text = objFooter.TituloN1;
            lblTituloN2.Text = objFooter.TituloN2;
            lblTituloN3.Text = objFooter.TituloN3;

            lblTelefoneN1.Text = objFooter.TelefoneN1;
            lblTelefoneN2.Text = objFooter.TelefoneN2;
            lblTelefoneN3.Text = objFooter.TelefoneN3;

            lblTextoN1.Text = objFooter.TextoN1;
            lblTextoN2.Text = objFooter.TextoN2;
            lblTextoN3.Text = objFooter.TextoN3;

            lblTextoCentral.Text = objFooter.TextoCentral;
            lblTextoCentralMobile.Text = objFooter.TextoCentral;

            linkN1.Text = objFooter.TituloLinkN1;
            linkN1.NavigateUrl = objFooter.UrlLinkN1;

            linkN2.Text = objFooter.TituloLinkN2;
            linkN2.NavigateUrl = objFooter.UrlLinkN2;

            linkN3.Text = objFooter.TituloLinkN3;
            linkN3.NavigateUrl = objFooter.UrlLinkN3;

            linkN4.Text = objFooter.TituloLinkN4;
            linkN4.NavigateUrl = objFooter.UrlLinkN4;

            linkN5.Text = objFooter.TituloLinkN5;
            linkN5.NavigateUrl = objFooter.UrlLinkN5;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}