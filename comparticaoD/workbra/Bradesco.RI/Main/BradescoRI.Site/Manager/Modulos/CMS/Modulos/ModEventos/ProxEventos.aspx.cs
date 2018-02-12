using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModEventos_ProxEventos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                IdIdioma = Convert.ToInt32(cookie.Value);

            gobjModEventos = DOModEvento.ObterModulo();
            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);

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
    public ModEventos gobjModEventos
    {
        get { return (ModEventos)(ViewState["ModEventos"] ?? null); }
        set { ViewState["ModEventos"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModEvento.InserirModulo(gobjModEventos);

            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);

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
        this.txtUrlListaEvento.Text = string.Empty;
        this.txtUrlTodosEventos.Text = string.Empty;

        this.ddlPaginas.DataSource = DOPagina.Listar(0,2);
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlPaginas2.DataSource = DOPagina.Listar(0,2);
        this.ddlPaginas2.DataValueField = "Caminho";
        this.ddlPaginas2.DataTextField = "Titulo";
        this.ddlPaginas2.DataBind();
        this.ddlPaginas2.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModEventos == null)
                {
                    gobjModEventos = new ModEventos();
                }
                
                gobjModEventos.UrlListaEvento = txtUrlListaEvento.Text;
                gobjModEventos.UrlTodosEventos = txtUrlTodosEventos.Text;
                

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                txtUrlListaEvento.Text = gobjModEventos.UrlListaEvento;
                txtUrlTodosEventos.Text = gobjModEventos.UrlTodosEventos;
                
                break;
        }
    }


    #endregion
}