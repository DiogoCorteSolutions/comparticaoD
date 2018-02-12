using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Logs_Listar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            this.LerDados();
        }
    }

    #region Variáveis
    public string SortField
    {
        get { return (string)(ViewState["SortField"] ?? ""); }
        set { ViewState["SortField"] = value; }
    }

    public bool AscendingSort
    {
        get { return (bool)(ViewState["AscendingSort"] ?? false); }
        set { ViewState["AscendingSort"] = value; }
    }
    #endregion

    #region Eventos

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LerDados();
    }

    protected void listPager_PageChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void ddlRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {
        listPager.CurrentPageNumber = 1;
        BindGrid();
    }

    protected void grdDados_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (listPager.DataSource != null && listPager.DataSource is IEnumerable<Log>)
        {
            IEnumerable<Log> dados = (IEnumerable<Log>)listPager.DataSource;
            SortField = e.SortExpression;

            if ((e.SortExpression == SortField))
            {
                if (AscendingSort)
                {
                    AscendingSort = false;
                }
                else
                {
                    AscendingSort = true;
                }

            }
            else
            {
                AscendingSort = true;
            }

            foreach (DataGridColumn c in grdDados.Columns)
            {
                c.HeaderStyle.CssClass = string.Format("tabelaHeader");
            }

            foreach (DataGridColumn c in grdDados.Columns)
            {
                if ((!string.IsNullOrWhiteSpace(c.SortExpression)
                            && c.SortExpression.Equals(e.SortExpression, StringComparison.OrdinalIgnoreCase)))
                {
                    if (AscendingSort)
                    {
                        c.HeaderStyle.CssClass = string.Format("tabelaHeader asc");
                    }
                    else
                    {
                        c.HeaderStyle.CssClass = string.Format("tabelaHeader desc");
                    }

                    break;
                }

            }

            #region Campos Sorting
            switch (e.SortExpression)
            {
                case "Id":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.ID).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.ID).ToArray();
                    break;
                case "Data_hora":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Data_hora).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Data_hora).ToArray();
                    break;
                case "Mensagem":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Mensagem).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Mensagem).ToArray();
                    break;
                case "LoginUsuario":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.LoginUsuario).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.LoginUsuario).ToArray();
                    break;
                default:
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.ID).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.ID).ToArray();
                    break;
            }
            #endregion

            listPager.DataSource = dados;
            listPager.DataBind();
            BindGrid();
        }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Inicia a tela
    /// </summary>
    private void IniciaTela()
    {
        this.btnBuscar.Text = Resources.Textos.Botao_Buscar;
    }

    /// <summary>
    /// Lista dados do banco de dados
    /// </summary>
    private void LerDados()
    {
        try
        {
            List<Log> objLogs = null;

            DateTime? datInicio = null;
            DateTime? datFim = null;

            if (!String.IsNullOrWhiteSpace(txtDataInicio.Text))
                datInicio = Convert.ToDateTime(txtDataInicio.Text);

            if (!String.IsNullOrWhiteSpace(txtDataFim.Text))
                datFim = Convert.ToDateTime(txtDataFim.Text);

            objLogs = DOLog.Listar(txtDescricao.Text, datInicio, datFim, 2, null);

            if (objLogs != null)
            {
                listPager.DataSource = objLogs;
                listPager.DataBind();

                BindGrid();
            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }

    }

    /// <summary>
    /// Dados para o Grid
    /// </summary>
    private void BindGrid()
    {
        listPager.PageSize = Convert.ToInt32(ddlRegistros.SelectedValue);

        grdDados.DataSource = listPager.PageDataItems;
        grdDados.DataBind();

        bool hasData = false;

        listPager.Visible = (listPager.PageCount > 1);
        if (listPager.DataSource != null)
        {
            if (grdDados.Items.Count > 0)
            {
                hasData = true;
            }
        }

        lblNoRecordsFound.Visible = !hasData;
        grdDados.Visible = hasData;
    }
    #endregion
}