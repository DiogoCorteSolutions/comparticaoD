using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Aprovacao_Listar : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        //Altera textos do CheckBox
        chkAprovados.Text = Resources.Aprovacao.Aprovados;
        chkReprovados.Text = Resources.Aprovacao.Reprovados;
        btnBuscar.Text = Resources.Textos.Botao_Buscar;  

        if (!IsPostBack)
        {
            LerDados(false, false);
        }
    }

    #region Eventos      
    protected void grdDados_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        try
        {
            if (listPager.DataSource != null && listPager.DataSource is IEnumerable<PaginaAprovacao>)
            {
                IEnumerable<PaginaAprovacao> dados = (IEnumerable<PaginaAprovacao>)listPager.DataSource;
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
                    case "paginaId":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.PaginaId).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.PaginaId).ToArray();
                        break;
                    case "TituloPagina":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.TituloPagina).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.TituloPagina).ToArray();
                        break;

                    case "DataCadastro":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.DataCadastro).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.DataCadastro).ToArray();
                        break;

                    case "NomeUsuario":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.NomeUsuario).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.NomeUsuario).ToArray();
                        break;

                    case "Status":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.Status).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.Status).ToArray();
                        break;
                        
                    default:
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.AprovacaoId).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.AprovacaoId).ToArray();
                        break;
                }
                #endregion

                listPager.DataSource = dados;
                listPager.DataBind();
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex); throw ex;
        }
    }

    protected void listPager_PageChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    protected void ddlRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            listPager.CurrentPageNumber = 1;
            BindGrid();
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LerDados(chkAprovados.Checked, chkReprovados.Checked);
    }    
    #endregion

    #region Métodos    
    private void LerDados(Boolean aprovados, Boolean reprovados)
    {
        List<PaginaAprovacao> objDados = null;
        try
        {
            objDados = DOPaginaAprovacao.Listar(aprovados, reprovados); 

            if (objDados != null)
            {
                listPager.DataSource = objDados;
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