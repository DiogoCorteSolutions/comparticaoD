using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Eventos_Listar : System.Web.UI.Page
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
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        Excluir();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LerDados();
    }

    protected void listPager_PageChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void grdDados_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        if (listPager.DataSource != null && listPager.DataSource is IEnumerable<Evento>)
        {
            IEnumerable<Evento> dados = (IEnumerable<Evento>)listPager.DataSource;
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
                case "IdEvento":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.IdEvento).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.IdEvento).ToArray();
                    break;
                case "TipoEvento":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.TipoEvento).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.TipoEvento).ToArray();
                    break;
                case "DataInicio":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.DataInicio.ToString("yyyyMMdd")).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.DataInicio.ToString("yyyyMMdd")).ToArray();
                    break;
                case "DataFim":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.DataFim.ToString("yyyyMMdd")).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.DataFim.ToString("yyyyMMdd")).ToArray();
                    break;
                case "Titulo":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Titulo).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Titulo).ToArray();
                    break;
                case "Responsavel":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Responsavel).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Responsavel).ToArray();
                    break;
                case "Cidade":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Cidade).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Cidade).ToArray();
                    break;

                default:
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.DataInicio.ToString("{yyyyMMdd}")).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.DataInicio.ToString("yyyyMMdd")).ToArray();
                    break;
            }
            #endregion

            listPager.DataSource = dados;
            listPager.DataBind();
            BindGrid();
        }

    }

    protected void ddlRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {
        listPager.CurrentPageNumber = 1;
        BindGrid();
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Editar.aspx");
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlTipoEvento.DataSource = DOModEvento.ListarTipoEvento(Convert.ToInt32(ddlIdioma.SelectedValue));
        this.ddlTipoEvento.DataTextField = "Descricao";
        this.ddlTipoEvento.DataValueField = "IdTipoEvento";
        this.ddlTipoEvento.DataBind();
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
            this.ddlIdioma.DataSource = DOIdioma.Listar();
            this.ddlIdioma.DataTextField = "Nome";
            this.ddlIdioma.DataValueField = "Id";
            this.ddlIdioma.DataBind();

            this.ddlTipoEvento.DataSource = DOModEvento.ListarTipoEvento(Convert.ToInt32( ddlIdioma.SelectedValue));
            this.ddlTipoEvento.DataTextField = "Descricao";
            this.ddlTipoEvento.DataValueField = "IdTipoEvento";
            this.ddlTipoEvento.DataBind();

            this.ddlRegistros.SelectedIndex = 0;
            this.txtDataInicio.Text = string.Empty;
            this.txtTitulo.Text = string.Empty;
            this.txtResponsavel.Text = string.Empty;
            this.txtCidade.Text = string.Empty;            

            this.btnBuscar.Text = Resources.Textos.Botao_Buscar;
            this.btnExcluir.Text = Resources.Textos.Botao_Excluir;
            this.btnNovo.Text = Resources.Textos.Botao_Novo;

            //Permissão de edição
            this.grdDados.Columns[7].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

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
            List<Evento> objDados = null;

            objDados = DOModEvento.Listar(Convert.ToInt32(ddlIdioma.SelectedValue), Convert.ToInt32(ddlTipoEvento.SelectedValue), txtTitulo.Text, 
                                          txtResponsavel.Text, txtCidade.Text, txtDataInicio.Text);

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
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSeleciona");
                    if (chk.Checked)
                    {
                        try
                        {
                            DOModEvento.Excluir(Convert.ToInt32(item.Cells[1].Text));
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