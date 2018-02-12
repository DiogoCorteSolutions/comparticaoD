using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Paginas_Listar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

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
        get { return (bool)(ViewState["AscendingSort"] ?? true); }
        set { ViewState["AscendingSort"] = value; }
    }
    #endregion

    #region Eventos  

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Session["paginaId"] = null;
        Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "window.open('Template.aspx', '_newtab')", true);
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        Excluir();
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
        if (listPager.DataSource != null && listPager.DataSource is IEnumerable<Pagina>)
        {
            IEnumerable<Pagina> dados = (IEnumerable<Pagina>)listPager.DataSource;
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
                        dados = dados.OrderBy(u => u.PaginaId).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.PaginaId).ToArray();
                    break;
                case "Titulo":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Titulo).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Titulo).ToArray();
                    break;
                case "Categoria":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Categoria).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Categoria).ToArray();
                    break;
                case "CriadoPor":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.CriadoPor).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.CriadoPor).ToArray();
                    break;
                case "DataCriacao":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.DataCriacao).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.DataCriacao).ToArray();
                    break;
                case "StatusDescricao":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.StatusDescricao).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.StatusDescricao).ToArray();
                    break;
                case "PublicadoPor":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.PublicadoPor).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.PublicadoPor).ToArray();
                    break;
                case "DataPublicacaoString":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.DataPublicacaoString).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.DataPublicacaoString).ToArray();
                    break;
                default:
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.PaginaId).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.PaginaId).ToArray();
                    break;
            }
            #endregion

            listPager.DataSource = dados;
            listPager.DataBind();
            BindGrid();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LerDados();
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Inicia a tela
    /// </summary>
    private void IniciaTela()
    {
        try
        {
            this.ddlRegistros.SelectedValue = "50";
            this.btnExcluir.Text = Resources.Textos.Botao_Excluir;
            this.btnNovo.Text = Resources.Textos.Botao_Novo;
            this.btnBuscar.Text = Resources.Textos.Botao_Buscar;

            this.ddlCategoria.DataSource = DOPagina.ListarCategoria();
            this.ddlCategoria.DataTextField = "Descricao";
            this.ddlCategoria.DataValueField = "IdCategoria";
            this.ddlCategoria.DataBind();
            this.ddlCategoria.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

            //Permissão de edição
            this.grdDados.Columns[3].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

            //Permissão de exclusão
            this.grdDados.Columns[0].Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();
            this.btnExcluir.Visible = ((Modulos_Modulos)Master).VerificaPermissaoExclusao();

            //Permissão de inclusão
            this.btnNovo.Visible = ((Modulos_Modulos)Master).VerificaPermissaoInclusao();
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Lista dados do banco de dados
    /// </summary>
    private void LerDados()
    {
        try
        {
            List<Pagina> objDados = null;

            objDados = DOPagina.Listar(Convert.ToInt32(ddlCategoria.SelectedValue),Convert.ToInt32(ddlStatus.SelectedValue));

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

    /// <summary>
    /// Verifica todos os registros selecionados na grid e exclui do banco de dados
    /// </summary>
    private void Excluir()
    {
        bool excluidoSucesso = true;

        try
        {
            foreach (DataGridItem item in grdDados.Items)
            {
                if (item.ItemType == ListItemType.Item ||
               item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSeleciona");
                    if (chk.Checked)
                    {
                        try
                        {
                            DOPagina.Excluir(Convert.ToInt32(item.Cells[1].Text));

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.IndexOf("conflicted", StringComparison.InvariantCultureIgnoreCase) > -1)
                            {
                                excluidoSucesso = false;
                            }
                        }
                    }
                }
            }
            if (excluidoSucesso)
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Exclusao_sucesso);
            }
            else
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Erro_FK);
            }

        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
        IniciaTela();
        LerDados();
    }
    #endregion
}