using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTexto_Texto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                HttpCookie cookie = Request.Cookies["_culture"];
                if (cookie != null)
                    IdIdioma = Convert.ToInt32(cookie.Value);

                gobjModTexto = DOModTexto.Obter(IdConteudo, IdIdioma);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
            this.IniciaTela();
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
    public ModTexto gobjModTexto
    {
        get { return (ModTexto)(ViewState["ModTexto"] ?? null); }
        set { ViewState["ModTexto"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvarModuloTexto_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModTexto.Inserir(gobjModTexto);

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

        gobjModTexto = DOModTexto.Obter(IdConteudo, IdIdioma);
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
    }
    #endregion

    #region Métodos
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

                if (gobjModTexto == null)
                {
                    gobjModTexto = new ModTexto();
                }

                gobjModTexto.ID = IdConteudo;
                gobjModTexto.IdIdioma = IdIdioma;
                gobjModTexto.Conteudo = fckEditor.Value;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlIdioma.SelectedValue = IdIdioma.ToString();

                if (gobjModTexto.Conteudo != null)
                    fckEditor.Value = gobjModTexto.Conteudo;
                else
                    fckEditor.Value = string.Empty;
                break;
        }
    }
    #endregion
}