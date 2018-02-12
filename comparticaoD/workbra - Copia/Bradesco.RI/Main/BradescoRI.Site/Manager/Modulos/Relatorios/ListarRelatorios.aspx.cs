using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Relatorios_ListarRelatorios : System.Web.UI.Page
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditarRelatorios.aspx", true);
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            Excluir();
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    private void Excluir()
    {
        try
        {
            var registroExcluido = false;
            try
            {
                foreach (GridViewRow item in grdDados.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chkSeleciona");
                        if (chk.Checked)
                        {
                            try
                            {
                                var relatorio = new Relatorio() { ID = int.Parse(item.Cells[1].Text) };
                                if (DoRelatorio.Excluir(relatorio) > 0)
                                    registroExcluido = true;
                                else
                                {
                                    registroExcluido = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("conflicted", StringComparison.InvariantCultureIgnoreCase) > -1)
                                {
                                    ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Erro_FK); break;
                                }
                            }
                        }
                    }
                }
                if (registroExcluido)
                {
                    ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Exclusao_sucesso);
                    LerDados();
                }
                else
                {
                    ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Exclusao_Erro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdDados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    
    protected void listPager_PageChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoRelatio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LerDados();
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    

    #endregion

    #region Métodos Privados
    private void LerDados()
    {
        TipoArquivo objTipoRelatorio  = null;
        List<Relatorio> objDados = null;
        try
        {
            btnNovo.Enabled = true;
            btnExcluir.Enabled = true;

            objTipoRelatorio = new TipoArquivo();

            if (ddlTipoRelatio.SelectedIndex > 0)
                objTipoRelatorio.Id = Convert.ToInt32(ddlTipoRelatio.SelectedValue.ToString());

            objDados = DoRelatorio.Listar(new Relatorio() { TipoRelatorio = objTipoRelatorio });

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

    private void IniciarTela()
    {
        try
        {
            try
            {
                this.btnNovo.Text = Resources.Textos.Botao_Novo;
                this.btnExcluir.Text = Resources.Textos.Botao_Excluir;

                //Permissão de edição
                this.grdDados.Columns[4].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

                //Permissão de exclusão
                this.grdDados.Columns[0].Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();
                this.btnExcluir.Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();

                //Permissão de inclusão
                this.btnNovo.Visible = ((Modulos_Modulos)Master).VerificaPermissaoInclusao();

                CarregarTipoRelatorio();
            }
            catch (Exception ex)
            {
                //Chama o método para gravar erro
                ((Modulos_Modulos)Master).ExibirAlerta(ex);
            }
        }
        catch (Exception ex)
        {

            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    private void CarregarTipoRelatorio()
    {
        try
        {
            ddlTipoRelatio.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = true });  //DOTipoRelatorio.Listar();
            ddlTipoRelatio.DataTextField = "Descricao";
            ddlTipoRelatio.DataValueField = "Id";
            ddlTipoRelatio.DataBind();

            ddlTipoRelatio.Items.Insert(0, new ListItem() { Value = "-1", Text = Resources.Relatorios.SelecioneTipoRelatorio });
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion



    protected void grdDados_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.Footer:
                    break;
                case DataControlRowType.DataRow:

                    Relatorio relatorio = (Relatorio)e.Row.DataItem;

                    Label lblTipoRelatorio = (Label)e.Row.FindControl("lblTipoRelatorio");
                    lblTipoRelatorio.Text = DOTipoArquivo.Obter(new TipoArquivo() { Id = relatorio.TipoRelatorio.Id }).Descricao;
                    
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
        catch (Exception ex)
        {
            throw ex;
        }
    }
}