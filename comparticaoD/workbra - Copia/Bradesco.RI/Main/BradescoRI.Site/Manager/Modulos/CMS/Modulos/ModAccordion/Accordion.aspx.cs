using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;

public partial class Modulos_CMS_Modulos_ModAccordion_Accordion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                ConteudoId = Convert.ToInt32(Request.QueryString["conteudoId"]);
                PaginaId = Convert.ToInt32(Request.QueryString["paginaId"]);

                gobjModAccordion = DOAccordion.Obter(ConteudoId);
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }

            this.IniciaTela();
        }
    }

    #region Variáveis   
    public int ConteudoId
    {
        get { return (int)(ViewState["ConteudoId"] ?? 0); }
        set { ViewState["ConteudoId"] = value; }
    }

    public int PaginaId
    {
        get { return (int)(ViewState["PaginaId"] ?? 0); }
        set { ViewState["PaginaId"] = value; }
    }
    public Accordions gobjModAccordion
    {
        get { return (Accordions)(ViewState["Accordion"] ?? null); }
        set { ViewState["Accordion"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvarModuloAccordion_Click(object sender, EventArgs e)
    {
        try
        {
            DOAccordion.Atualizar(ConteudoId, txtTitulo.Text, chkAberto.Checked);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAdicionarModulo_Click(object sender, EventArgs e)
    {
        try
        {
            DOAccordion.Inserir(ConteudoId, txtTitulo.Text, PaginaId, Convert.ToInt32(ddlModulos.SelectedValue), chkAberto.Checked);

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
        List<Modulo> lstModulos = DOModulo.Listar();

        var resultado = from modulos in lstModulos
                        where ConfigurationManager.AppSettings["ModulosAccordion"].Contains(";" + modulos.ModuloId + ";")
                        select modulos;

        ddlModulos.DataTextField = "Nome";
        ddlModulos.DataValueField = "ModuloId";
        ddlModulos.DataSource = resultado;
        ddlModulos.DataBind();
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                txtTitulo.Text = gobjModAccordion.Titulo;
                chkAberto.Checked = gobjModAccordion.PainelAberto; 
                break;
        }
    }
    #endregion

}