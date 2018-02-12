using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Noticias_EditarNoticia : System.Web.UI.Page
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            if (UsuarioLogado() == null)
                Response.Redirect("/Manager/Login.aspx", true);

            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            noticia = Convert.ToInt32(Request.QueryString["Noticia"]);
            hdnNoticiaId.Value = noticia.ToString();
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (noticia > 0)
            {
                gobjNoticia = DONoticia.Obter(new Noticia() { ID = noticia });
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gobjNoticia);
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Noticias/ListarNoticias.aspx", true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Salvar();
        }
        catch (Exception ex)
        {

            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    protected void btnDataNoticia_Click(object sender, EventArgs e)
    {
        caledarDataNoticia.Visible = true;
    }

    protected void caledarDataNoticia_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            txtDataNoticia.Text = this.caledarDataNoticia.SelectedDate.ToShortDateString();
            caledarDataNoticia.Visible = false;
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion

    #region Variáveis
    private int codigo, noticia, idioma;
    private Noticia gobjNoticia;

    private List<ArquivoNoticia> listaArquivos = new List<ArquivoNoticia>();
    #endregion

    #region Métodos

    public Usuario UsuarioLogado()
    {
        if (UserContext.Logado)
        {
            return UserContext.UsuarioLogado;
        }
        else
        {
            Response.Redirect("/Manager/Login.aspx?l=1");
        }
        return null;
    }

    private void IniciaTela()
    {
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        CarregarTipoNoticia();

        CarregarSubCategoriaNoticia();

        imgCapa.Visible = false;

        //btnAddFile.Text = Resources.Textos.Botao_Adicionar;
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTipoNoticia.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDataNoticia.Text = Resources.Textos.Campos_Obrigatorios;
        this.rfvFonte.Text = Resources.Textos.Campos_Obrigatorios;
        this.rfvCapa.Text = Resources.Textos.Campos_Obrigatorios;
        this.rfvSubTipoNoticia.Text = Resources.Textos.Campos_Obrigatorios;


        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Login.aspx");
    }



    private void CarregarSubCategoriaNoticia()
    {
        try
        {
            ddlSubTipoNoticia.DataSource = DoTipoNoticia.Listar();
            ddlSubTipoNoticia.DataTextField = "Descricao";
            ddlSubTipoNoticia.DataValueField = "Id";
            ddlSubTipoNoticia.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void CarregarTipoNoticia()
    {
        try
        {
            this.ddlTipoNoticia.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Noticia = true });
            this.ddlTipoNoticia.DataTextField = "Descricao";
            this.ddlTipoNoticia.DataValueField = "ID";
            this.ddlTipoNoticia.DataBind();

            this.ddlTipoNoticia.Items.Insert(0, new ListItem("Selecione o tipo de notícia", "-1"));
            this.ddlTipoNoticia.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Noticia objNoticia = null)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Limpar:
                ddlIdioma.SelectedIndex = 0;
                ddlSubTipoNoticia.SelectedIndex = 0;
                txtTituloNoticia.Text = string.Empty;
                txtDataNoticia.Text = string.Empty;
                txtFonte.Text = string.Empty;
                txtResumo.Value = string.Empty;
                txtIntegra.Value = string.Empty;
                chkDestaque.Text = Resources.Noticias.LabelDestaque;
                chkDestaque.Checked = false;
                //ddlArquivoCapa.SelectedIndex = 0;
                imgCapa.Visible = false;
                hdnCapaId.Value = string.Empty;
                break;
            case Utilitarios.TipoTransacao.Salvar:
                if (gobjNoticia == null)
                {
                    gobjNoticia = new Noticia();
                }

                if (hdnNoticiaId.Value != null)
                    gobjNoticia.ID = int.Parse(hdnNoticiaId.Value.ToString());

                gobjNoticia.Titulo = txtTituloNoticia.Text;
                gobjNoticia.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjNoticia.TipoArquivo = DOTipoArquivo.Obter(new TipoArquivo() { Id = Convert.ToInt32(ddlTipoNoticia.SelectedValue) });
                gobjNoticia.TipoNoticia = DoTipoNoticia.Obter(new TipoNoticia() { ID = Convert.ToInt32(ddlSubTipoNoticia.SelectedValue) });
                gobjNoticia.DataNoticia = DateTime.Parse(txtDataNoticia.Text);
                gobjNoticia.Fonte = txtFonte.Text;
                gobjNoticia.Resumo = txtResumo.Value.ToString();
                gobjNoticia.Integra = txtIntegra.Value;
                gobjNoticia.Destaque = chkDestaque.Checked;
                gobjNoticia.Usuario = UsuarioLogado();
                gobjNoticia.StatusId = (int)Utilitarios.Status.Criado;

                if (uplCapa.HasFile)
                    gobjNoticia.Capa = SalvarArquivo();
                else
                {
                    gobjNoticia.Capa = new Arquivos() { Id = Convert.ToInt32(hdnCapaId.Value) };
                    rfvCapa.ValidationGroup = "";
                }
                //gobjNoticia.Arquivos = listaArquivos;

                //gobjNoticia.Capa = DOArquivos.Obter(new Arquivos() {  Id= })
                //gobjNoticia.Capa = DOArquivos.Obter(new Arquivos() { Id = Convert.ToInt32(ddlArquivoCapa.SelectedValue) });
                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjNoticia.IdiomaId.ToString();
                ddlIdioma.Enabled = false;
                ddlTipoNoticia.SelectedValue = gobjNoticia.TipoArquivo.Id.ToString();
                ddlSubTipoNoticia.SelectedValue = gobjNoticia.TipoNoticia.ID.ToString();
                hdnNoticiaId.Value = gobjNoticia.ID.ToString();
                txtTituloNoticia.Text = gobjNoticia.Titulo;
                txtDataNoticia.Text = gobjNoticia.DataNoticia.ToShortDateString();
                txtFonte.Text = gobjNoticia.Fonte;
                txtResumo.Value = gobjNoticia.Resumo;
                txtIntegra.Value = gobjNoticia.Integra;
                chkDestaque.Checked = gobjNoticia.Destaque;
                if (gobjNoticia.Capa != null)
                {
                    gobjNoticia.Capa = DOArquivos.Obter(gobjNoticia.Capa);
                    //ddlArquivoCapa.SelectedValue = gobjNoticia.Capa.Id.ToString();
                    hdnCapaId.Value = gobjNoticia.Capa.Id.ToString();
                    imgCapa.ImageUrl = gobjNoticia.Capa.Caminho;
                    imgCapa.Visible = true;
                }
                break;
        }
    }

    private Arquivos SalvarArquivo()
    {
        Guid guid;
        string strFileName = string.Empty;
        var strPathArquivo = string.Empty;
        var strPathRetorno = string.Empty;
        try
        {
            if (uplCapa.HasFile)
            {
                guid = Guid.NewGuid();

                FileInfo fi = new FileInfo(uplCapa.PostedFile.FileName);
                string ext = fi.Extension;

                strFileName = guid.ToString() + ext;

                strPathArquivo = Server.MapPath(String.Format("/Uploads/Arquivos/{0}/{1}", ddlIdioma.SelectedValue, strFileName));
                strPathRetorno = String.Format("/Uploads/Arquivos/{0}/{1}", ddlIdioma.SelectedValue, strFileName);

                if (!Directory.Exists(Path.GetDirectoryName(strPathArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strPathArquivo));

                uplCapa.SaveAs(strPathArquivo);

                Arquivos arq = new Arquivos();
                arq.Caminho = strPathRetorno;
                arq.TipoArquivoId = 7;
                arq.Titulo = txtTituloNoticia.Text;
                arq.Descricao = txtTituloNoticia.Text;
                arq.Extensao = System.IO.Path.GetExtension(strPathRetorno).ToLower();
                arq.Tamanho = Utilitarios.TamanhoArquivo.Amigavel(uplCapa.PostedFile.ContentLength);
                arq.Capa = true;
                arq.DataCadastro = System.DateTime.Now;
                arq.UsuarioCadastroId = UsuarioLogado().Id;
                arq.StatusId = (int)Utilitarios.StatusArquivo.Ativo;
                arq.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                arq.DataArquivo = System.DateTime.Now;

                int retorno = DOArquivos.Inserir(arq);
                if (retorno > 0)
                    gobjNoticia.Capa = DOArquivos.Obter(new Arquivos() { Id = retorno });


            }

            return gobjNoticia.Capa;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<ArquivoNoticia> CarregarArquivos(Noticia pModNoticia)
    {
        try
        {
            return DOArquivoNoticia.ListaArquivosNoticia(pModNoticia);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Salvar()
    {

        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gobjNoticia);

            if (hdnNoticiaId.Value == "0")
            {
                gobjNoticia = DONoticia.Inserir(gobjNoticia);

                if (gobjNoticia.ID > 0)
                    hdnNoticiaId.Value = gobjNoticia.ID.ToString();

                Response.Redirect(string.Format("EditarNoticia.aspx?Noticia={0}&sucesso=1", gobjNoticia.ID));

            }
            else
            {
                if (DOModNoticia.Alterar(gobjNoticia) > 0)
                    Response.Redirect(string.Format("EditarNoticia.aspx?Noticia={0}&sucesso=2", gobjNoticia.ID));
            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }
    #endregion



    //protected void ddlArquivoCapa_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlArquivoCapa.SelectedIndex > 0)
    //        {
    //            Arquivos arq = DOArquivos.Obter(new Arquivos() { Id = Convert.ToInt32(ddlArquivoCapa.SelectedValue) });
    //            imgCapa.ImageUrl = arq.Caminho;
    //            imgCapa.Visible = true;
    //        }
    //        else
    //        {
    //            imgCapa.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}



}