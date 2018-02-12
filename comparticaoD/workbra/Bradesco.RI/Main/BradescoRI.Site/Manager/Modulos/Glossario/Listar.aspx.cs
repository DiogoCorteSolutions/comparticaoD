using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Glossario_Listar : System.Web.UI.Page
{
    #region Variáveis
    public string SortField
    {
        get { return (string)(ViewState["SortField"] ?? ""); }
        set { ViewState["SortField"] = value; }
    }

    public bool AscendingSort
    {
        get { return (bool)(ViewState["AscendingSort"] ?? true); }
        set { ViewState["AscendingSort"] = value; }
    }
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            this.IniciarTela();
            this.LerDados();
        }
    }

    protected void ddlRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Glossario/EditarGlossario.aspx", true);
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {

    }

    protected void grdDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdDados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdDados_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void listPager_PageChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region Métodos 
    private void IniciarTela()
    {
        try
        {
            this.btnNovo.Text = Resources.Textos.Botao_Novo;
            this.btnExcluir.Text = Resources.Textos.Botao_Excluir;

            //Permissão de edição
            this.grdDados.Columns[3].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

            //Permissão de exclusão
            this.grdDados.Columns[0].Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();
            this.btnExcluir.Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();

            //Permissão de inclusão
            this.btnNovo.Visible = ((Modulos_Modulos)Master).VerificaPermissaoInclusao();

            ddlIdioma.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LerDados()
    {
        List<Glossario> objDados = null;
        Glossario objGlossario = null;
        try
        {
            btnNovo.Enabled = true;
            btnExcluir.Enabled = true;

            objGlossario = new Glossario();

            if (ddlIdioma.SelectedIndex > 0)
                objGlossario.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);

            objDados = DOGlossario.Listar(objGlossario);

            if (objDados != null)
            {
                listPager.DataSource = objDados;
                listPager.DataBind();

                BindGrid();
            }

            if (objDados.Count <= 0)
                btnExcluir.Enabled = false;

        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    private void BindGrid()
    {
        listPager.PageSize = Convert.ToInt32(ddlRegistros.SelectedValue);

        grdDados.DataSource = listPager.PageDataItems;
        grdDados.DataBind();

        bool hasData = false;

        listPager.Visible = (listPager.PageCount > 1);
        if (listPager.DataSource != null)
        {
            if (grdDados.Rows.Count > 0)
            {
                hasData = true;
            }
        }

        lblNoRecordsFound.Visible = !hasData;
        grdDados.Visible = hasData;
    }
    #endregion
}