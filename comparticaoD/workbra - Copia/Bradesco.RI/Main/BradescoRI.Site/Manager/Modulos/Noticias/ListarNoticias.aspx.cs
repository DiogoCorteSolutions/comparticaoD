using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Noticias_ListarNoticias : System.Web.UI.Page
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
        try
        {
            listPager.CurrentPageNumber = 1;
            BindGrid();
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Noticias/EditarNoticia.aspx", true);
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
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
                            var noticia = new Noticia() { ID = int.Parse(item.Cells[1].Text) };
                            if (DONoticia.Excluir(noticia) > 0)
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
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    protected void grdDados_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        try
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
                    case "Nome":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.Titulo).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.Titulo).ToArray();
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
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
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
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion

    #region Métodos Privados
    private void LerDados()
    {
        List<Noticia> objDados = null;
        try
        {
            btnNovo.Enabled = true;
            btnExcluir.Enabled = true;
            objDados = DOModNoticia.Listar(new Noticia());

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
                this.grdDados.Columns[2].Visible = ((Modulos_Modulos)Master).VerificaPermissaoEdicao();

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
        catch (Exception ex)
        {

            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }
    #endregion


    
}