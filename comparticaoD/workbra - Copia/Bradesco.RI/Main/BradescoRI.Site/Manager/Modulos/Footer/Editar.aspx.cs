using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

public partial class Modulos_Footer_Editar : System.Web.UI.Page
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (UsuarioLogado() == null)
                    Response.Redirect("/Manager/Default.aspx");

                IniciarTela();
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Default.aspx", true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarFooter();
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }
    #endregion

    #region Metodos
    public Usuario UsuarioLogado()
    {
        if (UserContext.Logado)
        {
            return UserContext.UsuarioLogado;
        }
        else
        {
            Response.Redirect("~/Login.aspx?l=1");
        }
        return null;
    }

    private void SalvarFooter()
    {
        try
        {
            CarregarObjetos(TipoTransacao.Salvar);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        Footer objFooter = null;
        try
        {
            switch (objTipoTransacao)
            {

                case Utilitarios.TipoTransacao.Salvar:

                    objFooter = new Footer();

                    objFooter.TituloN1 = txtTituloN1.Text;
                    objFooter.TelefoneN1 = txtTelefoneN1.Text;
                    objFooter.TextoN1 = txtTextoN1.Text;

                    objFooter.TituloN2 = txtTituloN2.Text;
                    objFooter.TelefoneN2 = txtTelefoneN2.Text;
                    objFooter.TextoN2 = txtTextoN2.Text;

                    objFooter.TituloN3 = txtTituloN3.Text;
                    objFooter.TelefoneN3 = txtTelefoneN3.Text;
                    objFooter.TextoN3 = txtTextoN3.Text;

                    objFooter.TextoCentral = txtTextoCentral.Text;

                    objFooter.TituloLinkN1 = txtTituloLinkN1.Text;
                    objFooter.UrlLinkN1 = txtUrlLinkN1.Text;

                    objFooter.TituloLinkN2 = txtTituloLinkN2.Text;
                    objFooter.UrlLinkN2 = txtUrlLinkN2.Text;

                    objFooter.TituloLinkN3 = txtTituloLinkN3.Text;
                    objFooter.UrlLinkN3 = txtUrlLinkN3.Text;

                    objFooter.TituloLinkN4 = txtTituloLinkN4.Text;
                    objFooter.UrlLinkN4 = txtUrlLinkN4.Text;

                    objFooter.TituloLinkN5 = txtTituloLinkN5.Text;
                    objFooter.UrlLinkN5 = txtUrlLinkN5.Text;

                    objFooter.DataCadastro = System.DateTime.Now;
                    objFooter.UsuarioCadastroId = (UsuarioLogado()).Id;
                    objFooter.StatusId = (int)Utilitarios.StatusPagina.PendenteAprovacao;

                    if (DOFooter.Inserir(objFooter) > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('Rodapé inserido com sucesso. Seus dados foram enviados para aprovação.');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('Erro ao processar o rodapé');", true);
                        
                    }
                    break;

                case TipoTransacao.Carregar:
                    objFooter = CarregarFooter();

                    txtTituloN1.Text = objFooter.TituloN1;
                    txtTelefoneN1.Text = objFooter.TelefoneN1;
                    txtTextoN1.Text = objFooter.TextoN1;

                    txtTituloN2.Text = objFooter.TituloN2;
                    txtTelefoneN2.Text = objFooter.TelefoneN2;
                    txtTextoN2.Text = objFooter.TextoN2;

                    txtTituloN3.Text = objFooter.TituloN3;
                    txtTelefoneN3.Text = objFooter.TelefoneN3;
                    txtTextoN3.Text = objFooter.TextoN3;

                    txtTextoCentral.Text = objFooter.TextoCentral;

                    txtTituloLinkN1.Text = objFooter.TituloLinkN1;
                    txtUrlLinkN1.Text = objFooter.UrlLinkN1;

                    txtTituloLinkN2.Text = objFooter.TituloLinkN2;
                    txtUrlLinkN2.Text = objFooter.UrlLinkN2;

                    txtTituloLinkN3.Text = objFooter.TituloLinkN3;
                    txtUrlLinkN3.Text = objFooter.UrlLinkN3;

                    txtTituloLinkN4.Text = objFooter.TituloLinkN4;
                    txtUrlLinkN4.Text = objFooter.UrlLinkN4;

                    txtTituloLinkN5.Text = objFooter.TituloLinkN5;
                    txtUrlLinkN5.Text = objFooter.UrlLinkN5;

                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private Footer CarregarFooter()
    {
        Footer objFooter = null;
        try
        {
            //objFooter = new Footer() { StatusId = (int)Utilitarios.StatusPagina.Aprovado };

            objFooter = DOFooter.Obter(objFooter);

            return objFooter;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void IniciarTela()
    {
        try
        {
            this.rfvTextoN1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTextoN2.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTextoN3.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTelefoneN1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTelefoneN2.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTelefoneN3.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTextoN1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTextoN2.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTextoN3.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTituloLinkN1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTituloLinkN2.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTituloLinkN3.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTituloLinkN4.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvTituloLinkN5.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvUrlLinkN1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvUrlLinkN2.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvUrlLinkN3.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvUrlLinkN4.Text = Resources.Textos.Texto_Campo_Obrigatorio;
            this.rfvUrlLinkN5.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}