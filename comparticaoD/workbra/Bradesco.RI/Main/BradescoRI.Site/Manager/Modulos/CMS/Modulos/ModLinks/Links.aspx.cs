using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModLinks_Links : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                gobjModLinks = DOModLinks.Obter(IdConteudo);

                if (gobjModLinks != null)
                    CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    public int IdGrupo
    {
        get { return (int)(ViewState["IdGrupo"] ?? 1); }
        set { ViewState["IdGrupo"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public ModLinks gobjModLinks
    {
        get { return (ModLinks)(ViewState["ModLinks"] ?? null); }
        set { ViewState["ModLinks"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModLinks.Inserir(gobjModLinks);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        ddlGrupoLink.DataSource = DOModLinks.ListarGrupos();
        ddlGrupoLink.DataTextField = "Descricao";
        ddlGrupoLink.DataValueField = "IdGrupo";
        ddlGrupoLink.DataBind();
        ddlGrupoLink.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModLinks == null)
                {
                    gobjModLinks = new ModLinks();
                }

                gobjModLinks.ID = IdConteudo;
                gobjModLinks.IdGrupo = Convert.ToInt32(ddlGrupoLink.SelectedValue);

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlGrupoLink.SelectedValue = gobjModLinks.IdGrupo.ToString();

                break;
        }
    }
    #endregion
}