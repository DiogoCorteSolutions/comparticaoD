using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class Modulos_MenuCircular_EditarMenuCircular : System.Web.UI.Page
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

                gobjMenuCircular = DOModMenuCircular.ObterMenuCircular(codigo, grupo, idioma);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, grupo, idioma;
    private MenuCircular gobjMenuCircular;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("ListarMenuCircular.aspx?Grupo={0}", Request.QueryString["Grupo"].ToString()));
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
        this.txtGrupo.Text = DOModMenuCircular.ObterGrupo(grupo).Descricao;

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
        this.rfvTitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Limpar:

                ddlIdioma.SelectedIndex = 0;
                ddlPaginas.SelectedIndex = 0;
                ddlTarget.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;
                txtTooltip.Text = string.Empty;
                txtUrl.Text = string.Empty;

                break;
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjMenuCircular == null)
                {
                    gobjMenuCircular = new MenuCircular();
                }

                gobjMenuCircular.IdMenuCircular = codigo;
                gobjMenuCircular.IdGrupo = grupo;
                gobjMenuCircular.Titulo = txtTitulo.Text;
                gobjMenuCircular.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);

                if (ddlTarget.SelectedValue != "0")
                    gobjMenuCircular.Target = ddlTarget.SelectedValue;

                gobjMenuCircular.Tooltip = txtTooltip.Text;
                gobjMenuCircular.Url = txtUrl.Text;

                if (codigo > 0 && divUpload.Visible)
                    gobjMenuCircular.Arquivo = String.Format("{0}_{1}_{2}_{3}", codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                break;
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjMenuCircular.IdIdioma.ToString();
                ddlIdioma.Enabled = false;

                if (!String.IsNullOrWhiteSpace(gobjMenuCircular.Target))
                    ddlTarget.SelectedValue = gobjMenuCircular.Target;

                txtTitulo.Text = gobjMenuCircular.Titulo;
                txtTooltip.Text = gobjMenuCircular.Tooltip;
                txtUrl.Text = gobjMenuCircular.Url;

                if (String.IsNullOrWhiteSpace(gobjMenuCircular.Arquivo))
                {
                    divUpload.Visible = true;
                    divImagem.Visible = false;
                }
                else
                {
                    imgImagem.ImageUrl = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircular"], gobjMenuCircular.IdMenuCircular, gobjMenuCircular.Arquivo);

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

                codigo = DOModMenuCircular.InserirMenuCircular(gobjMenuCircular);

                if (SalvarImagem())
                {
                    if (fupArquivo.HasFile)
                    {
                        gobjMenuCircular.IdMenuCircular = codigo;
                        gobjMenuCircular.Arquivo = String.Format("{0}_{1}_{2}_{3}", codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                        DOModMenuCircular.InserirArquivo(gobjMenuCircular);
                    }

                    Response.Redirect(string.Format("ListarMenuCircular.aspx?Grupo={0}&sucesso=1", grupo));
                }                

            }
            else
            {
                if (SalvarImagem())
                {
                    DOModMenuCircular.AtualizarMenuCircular(gobjMenuCircular);

                    Response.Redirect(string.Format("ListarMenuCircular.aspx?Grupo={0}&sucesso=2", grupo));
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
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}_{4}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircular"], codigo, grupo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

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