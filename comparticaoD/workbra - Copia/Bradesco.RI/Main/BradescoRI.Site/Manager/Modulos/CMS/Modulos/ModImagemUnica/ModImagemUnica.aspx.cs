using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Modulos_CMS_Modulos_ModImagemUnica_ModImagemUnica : System.Web.UI.Page
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

                gobjModImagemUnica = DOModImagemUnica.Obter(IdConteudo, IdIdioma);
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
    public ModImagemUnica gobjModImagemUnica
    {
        get { return (ModImagemUnica)(ViewState["gobjModImagemUnica"] ?? null); }
        set { ViewState["gobjModImagemUnica"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (SalvarArquivo())
            {
                DOModImagemUnica.Inserir(gobjModImagemUnica);
            }

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

        gobjModImagemUnica = DOModImagemUnica.Obter(IdConteudo, IdIdioma);
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
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
        this.lblMensagem.Text = string.Empty;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        this.ddlTamanho.Items.Insert(0, new ListItem(Enum.GetName(typeof(ModImagemUnica.Tamanhos), 4), ModImagemUnica.Tamanhos.Tam3.ToString()));
        this.ddlTamanho.Items.Insert(0, new ListItem(Enum.GetName(typeof(ModImagemUnica.Tamanhos), 3), ModImagemUnica.Tamanhos.Tam2.ToString()));
        this.ddlTamanho.Items.Insert(0, new ListItem(Enum.GetName(typeof(ModImagemUnica.Tamanhos), 2), ModImagemUnica.Tamanhos.Tam1.ToString()));
        this.ddlTamanho.Items.Insert(0, new ListItem(Enum.GetName(typeof(ModImagemUnica.Tamanhos), 1), ModImagemUnica.Tamanhos.TelaInteira.ToString()));
        this.ddlTamanho.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

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
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModImagemUnica == null)
                {
                    gobjModImagemUnica = new ModImagemUnica();
                }                

                if (ddlIdioma.SelectedValue != "0")
                    gobjModImagemUnica.IdIdioma = IdIdioma;
                if (ddlTarget.SelectedValue != "0")
                    gobjModImagemUnica.Target = ddlTarget.SelectedValue;
                if (fupArquivo.HasFile)
                    gobjModImagemUnica.Arquivo = String.Format("{0}_{1}_{2}", IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);
                if (ddlTamanho.SelectedValue != "0")
                    gobjModImagemUnica.Tamanho = Convert.ToInt32(ddlTamanho.SelectedValue);

                gobjModImagemUnica.IdConteudo = IdConteudo;               
                gobjModImagemUnica.Tooltip = txtTooltip.Text;
                gobjModImagemUnica.Texto1 = txtTexto1.Text;
                gobjModImagemUnica.Texto2 = txtTexto2.Text;
                gobjModImagemUnica.Texto3 = txtTexto3.Text;
                gobjModImagemUnica.TextoUrl = txtTextoUrl.Text;
                gobjModImagemUnica.Url = txtUrl.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlIdioma.SelectedValue = IdIdioma.ToString();

                if (!String.IsNullOrWhiteSpace(gobjModImagemUnica.Target))
                    ddlTarget.SelectedValue = gobjModImagemUnica.Target;

                if (gobjModImagemUnica.Tamanho > 0)
                    ddlTamanho.SelectedValue = gobjModImagemUnica.Tamanho.ToString();
                
                txtTooltip.Text = gobjModImagemUnica.Tooltip;
                txtTexto1.Text = gobjModImagemUnica.Texto1;
                txtTexto2.Text = gobjModImagemUnica.Texto2;
                txtTexto3.Text = gobjModImagemUnica.Texto3;
                txtTextoUrl.Text = gobjModImagemUnica.TextoUrl;
                txtUrl.Text = gobjModImagemUnica.Url;

                if (String.IsNullOrWhiteSpace(gobjModImagemUnica.Arquivo))
                {
                    divUpload.Visible = true;
                    divImagem.Visible = false;
                }
                else
                {
                    imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModImagemUnica"], gobjModImagemUnica.IdConteudo, gobjModImagemUnica.Arquivo);

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
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModImagemUnica"], IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));
                
                if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

                string[] lstFiles = Directory.GetFiles(Path.GetDirectoryName(strNomeArquivo));

                foreach (string _file in lstFiles)
                    File.Delete(_file);

                fupArquivo.SaveAs(strNomeArquivo);
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}