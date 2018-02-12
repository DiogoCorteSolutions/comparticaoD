using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class Modulos_CMS_Modulos_ModMenuCircularHome_MenuCircularHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                this.IniciaTela();
                CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
                LerDados();
            }
        }
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    public int codigo
    {
        get { return (int)(ViewState["codigo"] ?? 0); }
        set { ViewState["codigo"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public MenuCircularHome gobjMenuCircularHome
    {
        get { return (MenuCircularHome)(ViewState["MenuCircularHome"] ?? null); }
        set { ViewState["MenuCircularHome"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (grdDados.Items.Count > 4 && codigo == 0)
            lblMensagem.Text = string.Format(Resources.Textos.Mensagem_Maximo_Registros, "5");
        else
        {
            try
            {
                CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

                if (codigo == 0)
                {
                    if (SalvarArquivo())
                    {
                        int idMenuCircularHome = DOModMenuCircularHome.InserirMenuCircularHome(gobjMenuCircularHome);

                        DOModMenuCircularHome.Inserir(new ModMenuCircularHome() { ID = IdConteudo, IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue), IdMenuCircularHome = idMenuCircularHome });
                    }
                }
                else
                {
                    if (SalvarArquivo())
                        DOModMenuCircularHome.AtualizarMenuCircularHome(gobjMenuCircularHome);
                }
                
                LerDados();
                CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
            }
        }
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
        LerDados();
    }

    protected void grdDados_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "excluir")
        {
            try
            {
                lblMensagem.Text = string.Empty;
                DOModCaixa.ExcluirCaixa(Convert.ToInt32(e.CommandArgument));
                LerDados();
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Excluir, ex.Message);
            }
        }
        else if (e.CommandName == "editar")
        {
            try
            {
                lblMensagem.Text = string.Empty;
                codigo = Convert.ToInt32(e.CommandArgument);
                gobjMenuCircularHome = DOModMenuCircularHome.ObterMenuCircularHome(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Excluir, ex.Message);
            }
        }
    }

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
    }

    protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        divUpload.Visible = true;
        divImagem.Visible = false;
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {        
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winroot, "_top"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winparent, "_parent"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winblank, "_blank"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winself, "_self"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlPaginas.DataSource = DOPagina.Listar();
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Limpar:

                codigo = 0;
                divImagem.Visible = false;
                divUpload.Visible = true;                
                lblMensagem.Text = string.Empty;
                ddlPaginas.SelectedIndex = 0;
                ddlTarget.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;
                txtTooltip.Text = string.Empty;
                txtUrl.Text = string.Empty;

                break;
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjMenuCircularHome == null)
                {
                    gobjMenuCircularHome = new MenuCircularHome();
                }

                gobjMenuCircularHome.IdMenuCircularHome = codigo;
                gobjMenuCircularHome.Titulo = txtTitulo.Text;
                gobjMenuCircularHome.Tooltip = txtTooltip.Text;
                gobjMenuCircularHome.Url = txtUrl.Text;

                if (ddlTarget.SelectedValue != "0")
                    gobjMenuCircularHome.Target = ddlTarget.SelectedValue;                
                if (fupArquivo.HasFile)
                    gobjMenuCircularHome.Arquivo = String.Format("{0}_{1}_{2}", IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                break;
            case Utilitarios.TipoTransacao.Carregar:
                
                if (!String.IsNullOrWhiteSpace(gobjMenuCircularHome.Target))
                    ddlTarget.SelectedValue = gobjMenuCircularHome.Target;

                txtTitulo.Text = gobjMenuCircularHome.Titulo;
                txtTooltip.Text = gobjMenuCircularHome.Tooltip;
                txtUrl.Text = gobjMenuCircularHome.Url;

                if (String.IsNullOrWhiteSpace(gobjMenuCircularHome.Arquivo))
                {
                    divUpload.Visible = true;
                    divImagem.Visible = false;
                }
                else
                {
                    imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircularHome"],IdConteudo, gobjMenuCircularHome.Arquivo);

                    divUpload.Visible = false;
                    divImagem.Visible = true;
                }

                break;

        }
    }

    private bool SalvarArquivo()
    {
        try
        {
            if (fupArquivo.HasFile)
            {
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircularHome"], IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

                if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

                fupArquivo.SaveAs(strNomeArquivo);
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LerDados()
    {
        try
        {
            List<MenuCircularHome> objDados = null;

            objDados = DOModMenuCircularHome.Listar(IdConteudo, Convert.ToInt32(ddlIdioma.SelectedValue));

            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hasData = false;

                if (grdDados.Items.Count > 0)
                {
                    hasData = true;
                }

                lblNoRecordsFound.Visible = !hasData;
                grdDados.Visible = hasData;
            }
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }

    #endregion


}