using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class Modulos_Banner_EditarBanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grupo = Convert.ToInt32(Request.QueryString["Grupo"]);

            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Id"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["Id"]);
                idioma = Convert.ToInt32(Request.QueryString["Idioma"]);

                gobjBanners = DOModBanners.ObterBanner(codigo, grupo, idioma);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, grupo, idioma;
    private Banners gobjBanners;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("ListarBanners.aspx?Grupo={0}", Request.QueryString["Grupo"].ToString()));
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
        GrupoBanners objGrupo = DOModBanners.ObterGrupo(grupo);
        this.txtGrupo.Text = objGrupo.Descricao;

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

        this.rfvArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Grupo
            case Utilitarios.TipoTransacao.Limpar:

                ddlIdioma.SelectedIndex = 0;
                ddlPaginas.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;
                txtSubtitulo.Text = string.Empty;
                txtTextoUrl.Text = string.Empty;
                txtUrl.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjBanners == null)
                {
                    gobjBanners = new Banners();
                }

                gobjBanners.IdBanner = codigo;
                gobjBanners.IdGrupo = grupo;
                gobjBanners.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);

                if (ddlTarget.SelectedValue != "0")
                    gobjBanners.Target = ddlTarget.SelectedValue;

                gobjBanners.Texto1 = txtTitulo.Text;
                gobjBanners.Texto2 = txtSubtitulo.Text;             
                gobjBanners.TextoUrl = txtTextoUrl.Text;
                gobjBanners.Url = txtUrl.Text;

                if (codigo > 0 && divUpload.Visible)
                    gobjBanners.Arquivo = String.Format("{0}_{1}_{2}_{3}", codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjBanners.IdIdioma.ToString();
                ddlIdioma.Enabled = false;

                if (!String.IsNullOrWhiteSpace(gobjBanners.Target))
                    ddlTarget.SelectedValue = gobjBanners.Target;

                txtTitulo.Text = gobjBanners.Texto1;
                txtSubtitulo.Text = gobjBanners.Texto2;
                txtTextoUrl.Text = gobjBanners.TextoUrl;
                txtUrl.Text = gobjBanners.Url;

                if (String.IsNullOrWhiteSpace(gobjBanners.Arquivo))
                {
                    divUpload.Visible = true;
                    divImagem.Visible = false;
                }
                else
                {
                    imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModBanner"], gobjBanners.IdBanner, gobjBanners.Arquivo);

                    divUpload.Visible = false;
                    divImagem.Visible = true;
                }

                break;
        }
    }

    private void Salvar()
    {

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);
            grupo = Convert.ToInt32(Request.QueryString["Grupo"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (codigo == 0)
            {

                codigo = DOModBanners.InserirBanner(gobjBanners);

                if (SalvarImagem())
                {
                    if (fupArquivo.HasFile)
                    {
                        gobjBanners.IdBanner = codigo;
                        gobjBanners.Arquivo = String.Format("{0}_{1}_{2}_{3}", codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                        DOModBanners.InserirArquivo(gobjBanners);
                    }
                }

                Response.Redirect(string.Format("ListarBanners.aspx?Grupo={0}&sucesso=1", grupo));

            }
            else
            {
                if (SalvarImagem())
                {
                    DOModBanners.AtualizarBanner(gobjBanners);

                    Response.Redirect(string.Format("ListarBanners.aspx?Grupo={0}&sucesso=2", grupo));
                }
            }


        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    private bool SalvarImagem()
    {
        try
        {
            if (fupArquivo.HasFile)
            {
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}_{4}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModBanner"], codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

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