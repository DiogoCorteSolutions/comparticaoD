using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Timeline_Editar : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{

    //    if (!Page.IsPostBack)
    //    {

    //        if (UsuarioLogado() == null)
    //            Response.Redirect("~/Default.aspx", true);

    //        if ((Request.QueryString["Sucesso"] == "1"))
    //        {
    //            ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
    //        }
    //        else if ((Request.QueryString["Sucesso"] == "2"))
    //        {
    //            ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
    //        }

    //        noticia = Convert.ToInt32(Request.QueryString["Noticia"]);
    //        hdnNoticiaId.Value = noticia.ToString();
    //        this.IniciaTela();
    //        CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

    //        //if (noticia > 0)
    //        //{
    //        //    gobjNoticia = DOTimeline.Obter(new Timeline() { Id = noticia });
    //        //    CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gobjNoticia);
    //        //}
    //    }
    //}

    //protected void btnCancelar_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("/Manager/Modulos/Timeline/Listar.aspx", true);
    //}

    //protected void btnSalvar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Salvar();
    //    }
    //    catch (Exception ex)
    //    {

    //        ((Modulos_Modulos)Master).ExibirAlerta(ex);
    //    }
    //}

    //protected void btnDataNoticia_Click(object sender, EventArgs e)
    //{
    //    caledarDataNoticia.Visible = true;
    //}

    //protected void caledarDataNoticia_SelectionChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtDataNoticia.Text = this.caledarDataNoticia.SelectedDate.ToShortDateString();
    //        caledarDataNoticia.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        ((Modulos_Modulos)Master).ExibirAlerta(ex);
    //    }
    //}


    //#region Variáveis
    //private int codigo, noticia, idioma;
    //private Timeline gobjNoticia;

    //private List<Timeline> listaArquivos = new List<Timeline>();
    //#endregion


    //#region Métodos

    //public Usuario UsuarioLogado()
    //{
    //    if (UserContext.Logado)
    //    {
    //        return UserContext.UsuarioLogado;
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Login.aspx?l=1");
    //    }
    //    return null;
    //}

    //private void IniciaTela()
    //{
    //    this.ddlIdioma.DataSource = DOIdioma.Listar();
    //    this.ddlIdioma.DataTextField = "Nome";
    //    this.ddlIdioma.DataValueField = "ID";
    //    this.ddlIdioma.DataBind();

    //    CarregarNoticiaLayout();

    //    CarregarTipoNoticia();

    //    //btnAddFile.Text = Resources.Textos.Botao_Adicionar;
    //    this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    //    this.rfvTipoNoticia.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    //    this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    //    this.rfvDataNoticia.Text = Resources.Textos.Campos_Obrigatorios;
    //    this.rfvFonte.Text = Resources.Textos.Campos_Obrigatorios;

    //    //this.rfvNomeArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    //    //this.rfvArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    //    //this.rfvTipoArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;

    //    //Permissão de edição
    //    if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
    //        Response.Redirect("/Manager/Login.aspx");
    //}

    //private void CarregarTipoNoticia()
    //{
    //    try
    //    {
    //        this.ddlTipoNoticia.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Noticia = true });
    //        this.ddlTipoNoticia.DataTextField = "Descricao";
    //        this.ddlTipoNoticia.DataValueField = "ID";
    //        this.ddlTipoNoticia.DataBind();

    //        this.ddlTipoNoticia.Items.Insert(0, new ListItem("Selecione o tipo de notícia", "-1"));
    //        this.ddlTipoNoticia.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void CarregarNoticiaLayout()
    //{
    //    try
    //    {
    //        //this.ddlNoticiaLayout.DataSource = DoNoticiaLayout.Listar();
    //        //this.ddlNoticiaLayout.DataValueField = "ID";
    //        //this.ddlNoticiaLayout.DataTextField = "Descricao";
    //        //this.ddlNoticiaLayout.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Timeline objNoticia = null)
    //{
    //    switch (objTipoTransacao)
    //    {
    //        //Nova noticia
    //        case Utilitarios.TipoTransacao.Limpar:
    //            ddlIdioma.SelectedIndex = 0;
    //            txtTituloNoticia.Text = string.Empty;
    //            txtDataNoticia.Text = string.Empty;
    //            txtFonte.Text = string.Empty;
    //            txtResumo.Value = string.Empty;
    //            txtIntegra.Value = string.Empty;
    //            chkDestaque.Text = Resources.Noticias.LabelDestaque;
    //            chkDestaque.Checked = false;
    //            //pnlFormArquivos.Visible = false;
    //            //pnlGridArquivos.Visible = false;
    //            break;
    //        //Carregar Dados da notícia
    //        case Utilitarios.TipoTransacao.Salvar:
    //            if (gobjNoticia == null)
    //            {
    //                gobjNoticia = new Timeline();
    //            }

    //            if (hdnNoticiaId.Value != null)
    //                gobjNoticia.Id = int.Parse(hdnNoticiaId.Value.ToString());

    //            gobjNoticia.Titulo = txtTituloNoticia.Text;


    //            gobjNoticia.Ano = txt.Text;
    //            gobjNoticia.Titulo = txtTituloNoticia.Text;
    //            gobjNoticia.Titulo = txtTituloNoticia.Text;
    //            gobjNoticia.Titulo = txtTituloNoticia.Text;
    //            gobjNoticia.Titulo = txtTituloNoticia.Text;


    //            //gobjNoticia.TimelineId = Convert.ToInt32(ddlIdioma.SelectedValue);
    //            //gobjNoticia.TipoArquivo = DOTipoArquivo.Obter(new TipoArquivo() { Id = Convert.ToInt32(ddlTipoNoticia.SelectedValue) });
    //            //gobjNoticia.TipoNoticia = DoTipoNoticia.Obter(new TipoNoticia() { ID = Convert.ToInt32(ddlTipoNoticia.SelectedValue) });
    //            //gobjNoticia.DataNoticia = DateTime.Parse(txtDataNoticia.Text);
    //            //gobjNoticia.Fonte = txtFonte.Text;
    //            //gobjNoticia.Resumo = txtResumo.Value.ToString();
    //            //gobjNoticia.Integra = txtIntegra.Value;
    //            //gobjNoticia.Destaque = chkDestaque.Checked;
    //            //gobjNoticia.u = UsuarioLogado();
    //            //gobjNoticia.StatusId = (int)Utilitarios.Status.Criado;
    //            //gobjNoticia.Arquivos = listaArquivos;

    //            break;
    //        //Carregar Dados do Link
    //        case Utilitarios.TipoTransacao.Carregar:

    //            ddlIdioma.SelectedValue = gobjNoticia.TimelineId.ToString();
    //            ddlIdioma.Enabled = false;
    //            ddlTipoNoticia.SelectedValue = gobjNoticia.Ano.ToString();
    //            txtTituloNoticia.Text = gobjNoticia.Titulo;
    //            //txtDataNoticia.Text = gobjNoticia.DataNoticia.ToShortDateString();
    //            //txtFonte.Text = gobjNoticia.Fonte;
    //            //txtResumo.Value = gobjNoticia.Resumo;
    //            //txtIntegra.Value = gobjNoticia.Integra;
    //            //chkDestaque.Checked = gobjNoticia.Destaque;

    //            //gobjNoticia.Arquivos = CarregarArquivos(gobjNoticia);
    //            //grdArquivos.DataSource = gobjNoticia.Arquivos;
    //            //grdArquivos.DataBind();
    //            //pnlFormArquivos.Visible = true;
    //            //pnlGridArquivos.Visible = true;

    //            break;
    //    }
    //}

    //private List<Timeline> CarregarArquivos(Timeline pModNoticia)
    //{
    //    try
    //    {
    //        return DOTimeline.Listar();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void Salvar()
    //{

    //    try
    //    {
    //        this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gobjNoticia);

    //        if (hdnNoticiaId.Value == "0")
    //        {
    //            gobjNoticia = DOModNoticia.Inserir(gobjNoticia);

    //            if (gobjNoticia.ID > 0)
    //                hdnNoticiaId.Value = gobjNoticia.ID.ToString();

    //            Response.Redirect(string.Format("EditarNoticia.aspx?Noticia={0}&sucesso=1", gobjNoticia.ID));

    //        }
    //        else
    //        {
    //            if (DOModNoticia.Alterar(gobjNoticia) > 0)
    //                Response.Redirect(string.Format("EditarNoticia.aspx?Noticia={0}&sucesso=2", gobjNoticia.ID));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //Chama o método para gravar erro
    //        ((Modulos_Modulos)Master).ExibirAlerta(ex);
    //    }
    //}
    //#endregion
}