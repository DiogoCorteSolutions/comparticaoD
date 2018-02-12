using System;
using System.IO;
using System.Web.UI;

public partial class Modulos_CMS_Modulos_ModTimeline_Timeline : System.Web.UI.Page
{

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdTimeline"] ?? 0); }
        set { ViewState["IdTimeline"] = value; }
    }
    public Timeline gobjModTimeline
    {
        get { return (Timeline)(ViewState["Timeline"] ?? null); }
        set { ViewState["Timeline"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                var cookie = Request.Cookies["_culture"];
                if (cookie != null)
                    IdIdioma = Convert.ToInt32(cookie.Value);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
            this.IniciaTela();
        }
    }

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

                if (gobjModTimeline == null)
                {
                    gobjModTimeline = new Timeline();
                }

                gobjModTimeline.Id = IdConteudo;
                gobjModTimeline.Idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjModTimeline.Titulo = txtTitulo.Text;
                gobjModTimeline.Texto = txtTexto.Value;
                gobjModTimeline.Ano = Convert.ToInt32(txtAno.Text);
                gobjModTimeline.Imagem = Path.GetFileName(fupTimeline.FileName);

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlIdioma.SelectedValue = IdIdioma.ToString();
                txtTitulo.Text = string.Empty;
                txtTexto.Value = string.Empty;
                txtAno.Text = string.Empty;
                fupTimeline.Attributes.Clear();
                break;
        }
    }

    protected void UploadFile(object sender, EventArgs e)
    {
        try
        {
            if (fupTimeline.HasFile)
            {
                var strNomeArquivo = Server.MapPath(String.Format("/Manager/Uploads/Imagens/{0}/{0}_{1}_{2}", IdConteudo, ddlIdioma.SelectedValue, fupTimeline.PostedFile.FileName));

                if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

                fupTimeline.SaveAs(strNomeArquivo);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    protected void btnSalvarModuloTimeline_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOTimeline.Inserir(gobjModTimeline);

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
        gobjModTimeline = DOTimeline.Obter(IdConteudo, IdIdioma);
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
    }

}