using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModFaleConosco_FaleConosco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                HttpCookie cookie = Request.Cookies["_culture"];
                if (cookie != null)
                    IdIdioma = Convert.ToInt32(cookie.Value);

                gobjModFaleConosco = DOModFaleConosco.Obter(IdConteudo, IdIdioma);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
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
    public ModFaleConosco gobjModFaleConosco
    {
        get { return (ModFaleConosco)(ViewState["gobjModFaleConosco"] ?? null); }
        set { ViewState["gobjModFaleConosco"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);
            DOModFaleConosco.Inserir(gobjModFaleConosco);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
        }
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);

        gobjModFaleConosco = DOModFaleConosco.Obter(IdConteudo, IdIdioma);
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
    }

    #endregion

    #region Métodos
    private void IniciaTela()
    {
        this.lblMensagem.Text = string.Empty;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModFaleConosco == null)
                {
                    gobjModFaleConosco = new ModFaleConosco();
                }

                gobjModFaleConosco.IdIdioma = IdIdioma;
                gobjModFaleConosco.IdConteudo = IdConteudo;
                gobjModFaleConosco.Assunto = txtAssunto.Text;
                gobjModFaleConosco.Email = txtEmail.Text;
                
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlIdioma.SelectedValue = IdIdioma.ToString();
                txtAssunto.Text = gobjModFaleConosco.Assunto;
                txtEmail.Text = gobjModFaleConosco.Email;
                
                break;
        }
    }


    #endregion
}