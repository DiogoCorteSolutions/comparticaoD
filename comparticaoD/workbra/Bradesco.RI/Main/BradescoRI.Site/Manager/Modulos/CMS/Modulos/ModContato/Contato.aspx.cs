using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModContato_Contato : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                HttpCookie cookie = Request.Cookies["_culture"];
                if (cookie != null)
                    IdIdioma = Convert.ToInt32(cookie.Value);

                gobjModContato = DOModContato.Obter(IdConteudo, IdIdioma);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
            this.IniciaTela();
        }
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public ModContato gobjModContato
    {
        get { return (ModContato)(ViewState["ModContato"] ?? null); }
        set { ViewState["ModContato"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModContato.Inserir(gobjModContato);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);

        gobjModContato = DOModContato.Obter(IdConteudo, IdIdioma);
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        ddlIdioma.DataSource = DOIdioma.Listar();
        ddlIdioma.DataTextField = "Nome";
        ddlIdioma.DataValueField = "ID";
        ddlIdioma.DataBind();
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModContato == null)
                {
                    gobjModContato = new ModContato();
                }

                gobjModContato.ID = IdConteudo;
                gobjModContato.IdIdioma = IdIdioma;
                gobjModContato.Assuntos = txtAssuntos.Text;
                gobjModContato.AssuntoEmail = txtAssuntoEmail.Text;
                gobjModContato.EmailTo = txtEmailTo.Text;
                gobjModContato.EmailToCc = txtEmailToCc.Text;
                gobjModContato.EmailToCco = txtEmailToCco.Text;
                gobjModContato.ConteudoTemplate = fckEditor.Value;
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlIdioma.SelectedValue = IdIdioma.ToString();

                if (!string.IsNullOrWhiteSpace(gobjModContato.Assuntos))
                {
                    txtAssuntoEmail.Text = gobjModContato.AssuntoEmail;
                    txtAssuntos.Text = gobjModContato.Assuntos;
                    txtEmailTo.Text = gobjModContato.EmailTo;
                    txtEmailToCc.Text = gobjModContato.EmailToCc;
                    txtEmailToCco.Text = gobjModContato.EmailToCco;
                    fckEditor.Value = gobjModContato.ConteudoTemplate;
                }
                else
                {
                    txtAssuntoEmail.Text = string.Empty;
                    txtAssuntos.Text = string.Empty;
                    txtEmailTo.Text = string.Empty;
                    txtEmailToCc.Text = string.Empty;
                    txtEmailToCco.Text = string.Empty;
                    fckEditor.Value = @"<p><strong>Formul&aacute;rio de Contato</strong></p>
                                        <p> Nome: <strong>%% Nome %%</strong><br />
                                        E-mail: <strong>%% Email %%</strong><br />
                                        Telefone: <strong>%% Telefone %%</strong><br />
                                        Empresa: <strong>%% Empresa %%</strong><br />
                                        Assunto: <strong>%% Assunto %%</strong><br />
                                        Mensagem: <strong>%% Mensagem %%</strong><br />
                                        &nbsp;</p> ";    
                }
                break;
        }
    }
    #endregion
}