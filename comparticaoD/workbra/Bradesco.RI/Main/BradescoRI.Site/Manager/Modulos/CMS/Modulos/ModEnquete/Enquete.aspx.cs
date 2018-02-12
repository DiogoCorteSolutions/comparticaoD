using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModEnquete_Enquete : System.Web.UI.Page
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

                gobjModEnquete = DOModEnquete.Obter(IdConteudo);
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
    public ModEnquete gobjModEnquete
    {
        get { return (ModEnquete)(ViewState["gobjModEnquete"] ?? null); }
        set { ViewState["gobjModEnquete"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModEnquete.Inserir(gobjModEnquete);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
        }
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        this.lblMensagem.Text = string.Empty;
        this.txtTitulo.Text = string.Empty;
        this.txtUrl.Text = string.Empty;

        this.ddlPaginas.DataSource = DOPagina.Listar(0,2);
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlEnquete.DataSource = DOModEnquete.ListarEnquete();
        this.ddlEnquete.DataValueField = "IdEnquete";
        this.ddlEnquete.DataTextField = "Descricao";
        this.ddlEnquete.DataBind();
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModEnquete == null)
                {
                    gobjModEnquete = new ModEnquete();
                }

                gobjModEnquete.IdEnquete = Convert.ToInt32(ddlEnquete.SelectedValue);
                gobjModEnquete.IdConteudo = IdConteudo;
                gobjModEnquete.Titulo = txtTitulo.Text;
                gobjModEnquete.UrlFaleConosco = txtUrl.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                txtTitulo.Text = gobjModEnquete.Titulo;
                txtUrl.Text = gobjModEnquete.UrlFaleConosco;
                ddlEnquete.SelectedValue = gobjModEnquete.IdEnquete.ToString();

                break;
        }
    }

    #endregion
}