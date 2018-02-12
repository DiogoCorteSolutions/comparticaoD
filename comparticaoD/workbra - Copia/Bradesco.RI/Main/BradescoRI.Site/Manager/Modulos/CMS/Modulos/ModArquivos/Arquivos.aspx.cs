using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModArquivos_Arquivos : System.Web.UI.Page
{
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

    public ModArquivo gobjModArquivo
    {
        get { return (ModArquivo)(ViewState["ModArquivo"] ?? null); }
        set { ViewState["ModArquivo"] = value; }
    }
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);


            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);
                hdnModArquivo.Value = IdConteudo.ToString();
                var cookie = Request.Cookies["_culture"];
                if (cookie != null)
                    IdIdioma = Convert.ToInt32(cookie.Value);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }

        }
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoLayout_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);
            if (Salvar())
            {
                MostrarMensagem(Resources.Noticias.Mensagem_Inserir_Modulo_Noticia_Sucesso);
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
            }
            else
            {
                //Mensagem Erro
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void grvArquivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                e.Row.Cells[0].Visible = false;
                break;
            case DataControlRowType.Footer:
                break;
            case DataControlRowType.DataRow:
                e.Row.Cells[0].Visible = false;
                var arq = (Arquivos)e.Row.DataItem;

                arq = DOArquivos.Obter(arq);

                Label lblTipoArquivo = (Label)e.Row.FindControl("lblTipoArquivo");
                lblTipoArquivo.Text = DOTipoArquivo.Obter(new TipoArquivo() { Id = Convert.ToInt32(arq.TipoArquivoId) }).Descricao;



                break;
            case DataControlRowType.Separator:
                break;
            case DataControlRowType.Pager:
                break;
            case DataControlRowType.EmptyDataRow:
                break;
            default:
                break;
        }
    }


    protected void btnExcluir_Command(object sender, CommandEventArgs e)
    {
        List<Arquivos> lstArquivos = new List<Arquivos>();

        lstArquivos = (List<Arquivos>)Session["sArquivos"];

        if (e.CommandName.ToString() == "ExcluirArquivo")
        {
            int rowID = Convert.ToInt32(e.CommandArgument.ToString());
            lstArquivos.RemoveAt(rowID);

            Session["sArquivos"] = lstArquivos;

            grvArquivos.DataSource = lstArquivos;
            grvArquivos.DataBind();
        }
    }

    protected void ddltipoArquivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltipoArquivo.SelectedIndex > 0)
            {
                ddlArquivo.DataSource = DOArquivos.Listar(new Arquivos() { TipoArquivoId = Convert.ToInt32(ddltipoArquivo.SelectedValue) });
                ddlArquivo.DataTextField = "Titulo";
                ddlArquivo.DataValueField = "Id";
                ddlArquivo.DataBind();
                ddlArquivo.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnIncluirArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            IncluirArquivoGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Métodos privados
    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModArquivo == null)
                    gobjModArquivo = new ModArquivo();

                gobjModArquivo.ConteudoId = Convert.ToInt32(Request.QueryString["conteudoId"].ToString());
                gobjModArquivo.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjModArquivo.Titulo = txtTitulo.Text;
                gobjModArquivo.ShowTitulo = chkMostraTitulo.Checked;
                gobjModArquivo.ShowFiltro = ddlFiltros.SelectedValue.ToString() == "0" ? false : true;
                gobjModArquivo.TipoLayoutId = Convert.ToInt32(ddlTipoLayout.SelectedValue);
                gobjModArquivo.Arquivos = (List<Arquivos>)Session["sArquivos"];
                gobjModArquivo.Data = System.DateTime.Now;
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                if (gobjModArquivo == null)
                    gobjModArquivo = new ModArquivo();

                gobjModArquivo = DOModArquivo.Obter(new ModArquivo() { ConteudoId = Convert.ToInt32(Request.QueryString["conteudoId"].ToString()) });

                ddlIdioma.SelectedValue = gobjModArquivo.IdiomaId.ToString();
                ddlTipoLayout.SelectedValue = gobjModArquivo.TipoLayoutId.ToString();
                chkMostraTitulo.Checked = gobjModArquivo.ShowTitulo;
                ddlFiltros.SelectedValue = gobjModArquivo.ShowFiltro ? "1" : "0";
                txtTitulo.Text = gobjModArquivo.Titulo;
                Session["sArquivos"] = DoModArquivoItem.Listar(gobjModArquivo);
                grvArquivos.DataSource = (List<Arquivos>)Session["sArquivos"];
                grvArquivos.DataBind();
                break;

            case Utilitarios.TipoTransacao.Limpar:
                CarregarTela();
                ddlIdioma.SelectedValue = IdIdioma.ToString();
                ddlTipoLayout.SelectedIndex = 0;
                chkMostraTitulo.Checked = true;
                ddlFiltros.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;

                Session["sArquivos"] = new List<Arquivos>();
                break;
        }
    }

    private void CarregarTela()
    {
        try
        {
            List<Arquivos> lstArquivos = new List<Arquivos>();

            ddlIdioma.DataSource = DOIdioma.Listar();
            ddlIdioma.DataTextField = "Nome";
            ddlIdioma.DataValueField = "Id";
            ddlIdioma.DataBind();

            ddlTipoLayout.Items.Insert(0, new ListItem("Download por lote", "0"));
            ddlTipoLayout.Items.Insert(1, new ListItem("Download único", "1"));
            ddlTipoLayout.Items.Insert(2, new ListItem("Download podcast", "2"));

            ddltipoArquivo.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = null });
            ddltipoArquivo.DataTextField = "Descricao";
            ddltipoArquivo.DataValueField = "Id";
            ddltipoArquivo.DataBind();

            ddltipoArquivo.Items.Insert(0, new ListItem("Selecione o tipo de arquivo", "-1"));
            ddltipoArquivo.SelectedIndex = 0;

            ddlArquivo.Items.Insert(0, new ListItem("Selecione o arquivo", "-1"));
            ddlArquivo.SelectedIndex = 0;

            if (ddltipoArquivo.SelectedIndex > 0)
                ddlArquivo.Enabled = true;
            else
                ddlArquivo.Enabled = false;

            ddlFiltros.Items.Insert(0, new ListItem("Não", "0"));
            ddlFiltros.Items.Insert(1, new ListItem("Sim", "1"));
            ddlFiltros.SelectedIndex = 0;

            //grvArquivos.DataSource = DOModArquivo.Listar(new ModArquivo() { ConteudoId = Convert.ToInt32(Request.QueryString["Conteudo"]) });

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void MostrarMensagem(string pMensagem)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + pMensagem + "');", true);
    }

    private bool Salvar()
    {
        bool retorno = false;
        try
        {
            if (DOModArquivo.Salvar(gobjModArquivo) > 0)
            {
                DoModArquivoItem.Apagar(gobjModArquivo);
                foreach (var item in gobjModArquivo.Arquivos)
                {
                    ModArquivoItem arquivoItem = new ModArquivoItem() { ConteudoId = gobjModArquivo.ConteudoId, ArquivoId = item.Id };
                    DoModArquivoItem.Inserir(arquivoItem);
                }
                retorno = true;
            }
            return retorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void IncluirArquivoGrid()
    {
        List<Arquivos> lstArquivos = new List<Arquivos>();
        try
        {
            Arquivos arquivo = new Arquivos() { Id = Convert.ToInt32(ddlArquivo.SelectedValue) };
            arquivo = DOArquivos.Obter(arquivo);
            lstArquivos = (List<Arquivos>)Session["sArquivos"];

            lstArquivos.Add(arquivo);

            Session["sArquivos"] = lstArquivos;

            grvArquivos.DataSource = lstArquivos;
            grvArquivos.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion



}