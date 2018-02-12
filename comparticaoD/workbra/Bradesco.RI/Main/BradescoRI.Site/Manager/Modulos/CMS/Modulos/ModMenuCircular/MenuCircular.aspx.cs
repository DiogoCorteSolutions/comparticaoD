using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModMenuCircular_MenuCircular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                gobjModMenuCircular = DOModMenuCircular.Obter(IdConteudo);

                if (gobjModMenuCircular != null)
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
    public ModMenuCircular gobjModMenuCircular
    {
        get { return (ModMenuCircular)(ViewState["ModMenuCircular"] ?? null); }
        set { ViewState["ModMenuCircular"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModMenuCircular.Inserir(gobjModMenuCircular);

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
        ddlGrupoMenuCircular.DataSource = DOModMenuCircular.ListarGrupos();
        ddlGrupoMenuCircular.DataTextField = "Descricao";
        ddlGrupoMenuCircular.DataValueField = "IdGrupo";
        ddlGrupoMenuCircular.DataBind();
        ddlGrupoMenuCircular.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModMenuCircular == null)
                {
                    gobjModMenuCircular = new ModMenuCircular();
                }

                gobjModMenuCircular.ID = IdConteudo;
                gobjModMenuCircular.IdGrupo = Convert.ToInt32(ddlGrupoMenuCircular.SelectedValue);

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlGrupoMenuCircular.SelectedValue = gobjModMenuCircular.IdGrupo.ToString();

                break;
        }
    }
    #endregion
}