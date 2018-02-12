using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModGlossario_Glossario : System.Web.UI.Page
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

    public ModGlossario gobjModGlossario
    {
        get { return (ModGlossario)(ViewState["ModGlossario"] ?? null); }
        set { ViewState["ModGlossario"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);
                hdnConteudoId.Value = IdConteudo.ToString();
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Salvar();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Salvar()
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModGlossario.Excluir(gobjModGlossario);
            foreach (GridViewRow item in grvGlossario.Rows)
            {
                gobjModGlossario.GlossarioId = Convert.ToInt32(item.Cells[0].Text);
                DOModGlossario.Inserir(gobjModGlossario);
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModGlossario == null)
                    gobjModGlossario = new ModGlossario();

                gobjModGlossario.ConteudoId = Convert.ToInt32(Request.QueryString["conteudoId"].ToString());
                gobjModGlossario.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjModGlossario.Data = System.DateTime.Now;
                break;

            case Utilitarios.TipoTransacao.Carregar:
                if (gobjModGlossario == null)
                    gobjModGlossario = new ModGlossario();

                var lModGlossario = DOModGlossario.Listar(new ModGlossario() { IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue), ConteudoId = Convert.ToInt32(Request.QueryString["conteudoId"].ToString()) });

                var lstGlossario = new List<Glossario>();

                foreach (ModGlossario item in lModGlossario)
                    lstGlossario.Add(DOGlossario.Obter(new Glossario() { Id = item.GlossarioId }));

                Session["sGlossario"] = lstGlossario;

                grvGlossario.DataSource = lstGlossario;
                grvGlossario.DataBind();

                break;

            case Utilitarios.TipoTransacao.Limpar:
                CarregarTela();
                ddlIdioma.SelectedValue = IdIdioma.ToString();
                break;
        }
    }

    private void CarregarTela()
    {
        try
        {
            ddlIdioma.DataSource = DOIdioma.Listar();
            ddlIdioma.DataTextField = "Nome";
            ddlIdioma.DataValueField = "Id";
            ddlIdioma.DataBind();

            ddlGlossario.DataSource = DOGlossario.Listar(new Glossario());
            ddlGlossario.DataTextField = "Titulo";
            ddlGlossario.DataValueField = "Id";
            ddlGlossario.DataBind();

            Session["sGlossario"] = new List<Glossario>();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {

        try
        {
            var lst = (List<Glossario>)Session["sGlossario"];

            Glossario glossario = DOGlossario.Obter(new Glossario() { Id = Convert.ToInt32(ddlGlossario.SelectedValue) });
            lst.Add(glossario);

            Session["sGlossario"] = lst;

            grvGlossario.DataSource = lst;
            grvGlossario.DataBind();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void grvGlossario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Glossario glossario = new Glossario();
                glossario = (Glossario)e.Row.DataItem;

                Label lbl = new Label();
                lbl = (Label)e.Row.FindControl("lblTituloGlossario");
                lbl.Text = DOGlossario.Obter(new Glossario() { Id = glossario.Id }).Titulo;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grvGlossario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                var lst = (List<Glossario>)Session["sGlossario"];

                var glossario = lst.Where(x => x.Id == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();

                lst.Remove(glossario);

                
                Session["sGlossario"] = lst;

                grvGlossario.DataSource = lst;
                grvGlossario.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}