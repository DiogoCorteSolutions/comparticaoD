using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class Modulos_CMS_Modulos_ModHeader_Header : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                gobjModHeader = DOModHeader.Obter(IdConteudo);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public ModHeader gobjModHeader
    {
        get { return (ModHeader)(ViewState["gobjModHeader"] ?? null); }
        set { ViewState["gobjModHeader"] = value; }
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
                DOModHeader.Inserir(gobjModHeader);
            }

            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
        }
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
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModHeader == null)
                {
                    gobjModHeader = new ModHeader();
                }

                if (fupArquivo.HasFile)
                    gobjModHeader.Arquivo = String.Format("{0}_{1}", IdConteudo, fupArquivo.PostedFile.FileName);

                gobjModHeader.IdConteudo = IdConteudo;
               
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                if (String.IsNullOrWhiteSpace(gobjModHeader.Arquivo))
                {
                    divUpload.Visible = true;
                    divImagem.Visible = false;
                }
                else
                {
                    imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModHeader"], gobjModHeader.IdConteudo, gobjModHeader.Arquivo);

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
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModHeader"],IdConteudo, fupArquivo.PostedFile.FileName));

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