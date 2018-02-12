using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModBanners_Banners : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();

            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                gobjModBanners = DOModBanners.Obter(IdConteudo);

                if (gobjModBanners != null)
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
    public ModBanners gobjModBanners
    {
        get { return (ModBanners)(ViewState["ModBanners"] ?? null); }
        set { ViewState["ModBanners"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModBanners.Inserir(gobjModBanners);

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
        ddlGrupoBanner.DataSource = DOModBanners.ListarGrupos();
        ddlGrupoBanner.DataTextField = "Descricao";
        ddlGrupoBanner.DataValueField = "IdGrupo";
        ddlGrupoBanner.DataBind();
        ddlGrupoBanner.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModBanners == null)
                {
                    gobjModBanners = new ModBanners();
                }

                gobjModBanners.ID = IdConteudo;
                gobjModBanners.IdGrupo = Convert.ToInt32(ddlGrupoBanner.SelectedValue);

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlGrupoBanner.SelectedValue = gobjModBanners.IdGrupo.ToString();

                break;
        }
    }
    #endregion
}