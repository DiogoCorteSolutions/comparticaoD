﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_AlertaRI_Listar : System.Web.UI.Page
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
        if (listPager.DataSource != null && listPager.DataSource is IEnumerable<ModAlerta>)
        {
            IEnumerable<ModAlerta> dados = (IEnumerable<ModAlerta>)listPager.DataSource;
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
                        dados = dados.OrderByDescending(u => u.Id).ToArray();
                    else
                        dados = dados.OrderBy(u => u.Id).ToArray();
                    break;
                case "Nome":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Nome).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Nome).ToArray();
                    break;
                case "Email":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Email).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Email).ToArray();
                    break;
                case "ProfissionalMercado":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.ProfissionalMercado).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.ProfissionalMercado).ToArray();
                    break;
                case "NomeSegmento":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.NomeSegmento).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.NomeSegmento).ToArray();
                    break;
                case "Empresa":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Empresa).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Empresa).ToArray();
                    break;
                case "NomePais":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.NomePais).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.NomePais).ToArray();
                    break;
                case "Data":
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Data).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Data).ToArray();
                    break;
                default:
                    if (AscendingSort)
                        dados = dados.OrderBy(u => u.Id).ToArray();
                    else
                        dados = dados.OrderByDescending(u => u.Id).ToArray();
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

    protected void btnExportarXls_Click(object sender, EventArgs e)
    {
        try
        {
            string strNomeExcel = (DateTime.Now.ToString("yyyyMMdd_hhmmss")) + ".xls";
            Utilitarios.Excel.CriarExcel(HttpContext.Current.Response, DOModAlerta.TrataExcel((List<ModAlerta>)listPager.DataSource), strNomeExcel);
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
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
            this.ddlRegistros.SelectedIndex = 0;
            this.txtNome.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtDataFim.Text = string.Empty;
            this.txtDataInicio.Text = string.Empty;

            this.btnBuscar.Text = Resources.Textos.Botao_Buscar;
            this.btnExcluir.Text = Resources.Textos.Botao_Excluir;
            this.btnNovo.Text = Resources.Textos.Botao_Novo;
            this.btnExportarXls.Text = Resources.Textos.Botao_Exportar;

            //Permissão de edição
            this.grdDados.Columns[9].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

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
            DateTime? datInicio = null;
            DateTime? datFim = null;
            List<ModAlerta> objDados = null;

            if (!String.IsNullOrWhiteSpace(txtDataInicio.Text))
                datInicio = Convert.ToDateTime(txtDataInicio.Text);

            if (!String.IsNullOrWhiteSpace(txtDataFim.Text))
                datFim = Convert.ToDateTime(txtDataFim.Text);

            objDados = DOModAlerta.Listar((int)Utilitarios.Idioma.Portugues, txtNome.Text, txtEmail.Text, datInicio, datFim);

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
                            DOModAlerta.Excluir(Convert.ToInt32(item.Cells[1].Text));
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