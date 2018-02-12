using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Arquivos_ListarArquivos : System.Web.UI.Page
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
        listPager.CurrentPageNumber = 1;
        BindGrid();
    }


    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("/manager/Modulos/Arquivos/EditarArquivo.aspx", true);
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
                                var arquivo = DOArquivos.Obter(new Arquivos() { Id = int.Parse(item.Cells[1].Text) });

                                if (DOArquivos.Excluir(arquivo) > 0)
                                {
                                    if (arquivo.Streaming)
                                        ApagarArquivo(arquivo);

                                    registroExcluido = true;
                                }
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ApagarArquivo(Arquivos arquivo)
    {
        try
        {
            string[] lstFiles = Directory.GetFiles(Path.GetDirectoryName(Server.MapPath(arquivo.Caminho)));

            foreach (string _file in lstFiles)
                if (_file.Contains(arquivo.Caminho))
                    File.Delete(_file);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdDados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[2].Visible = false;
                    break;
                case DataControlRowType.Footer:
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[2].Visible = false;

                    Label lblTipoArquivo = (Label)e.Row.FindControl("lblTipoArquivoId");
                    lblTipoArquivo.Text = DOTipoArquivo.Obter(new TipoArquivo() { Id = Convert.ToInt32(e.Row.Cells[2].Text) }).Descricao;

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

    protected void grdDados_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    protected void ddlTipoArquivo_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
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
    private void IniciarTela()
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

            CarregarTipoArquivo();
            ddlIdioma.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }

    }

    private void CarregarTipoArquivo()
    {
        try
        {
            ddlTipoArquivo.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = null });
            ddlTipoArquivo.DataTextField = "Descricao";
            ddlTipoArquivo.DataValueField = "Id";
            ddlTipoArquivo.DataBind();

            ddlTipoArquivo.Items.Insert(0, "Todos os tipos de arquivos");
            ddlTipoArquivo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void LerDados()
    {
        List<Arquivos> objDados = null;
        Arquivos objArquivo = null;
        try
        {
            btnNovo.Enabled = true;
            btnExcluir.Enabled = true;

            objArquivo = new Arquivos();

            if (ddlTipoArquivo.SelectedIndex > 0)
                objArquivo.TipoArquivoId = Convert.ToInt32(ddlTipoArquivo.SelectedValue);

            if (ddlIdioma.SelectedIndex > 0)
                objArquivo.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);

            objDados = DOArquivos.Listar(objArquivo);

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

    protected void grdDados_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (listPager.DataSource != null && listPager.DataSource is IEnumerable<Arquivos>)
            {
                IEnumerable<Arquivos> dados = (IEnumerable<Arquivos>)listPager.DataSource;
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

                var icount = 0;

                icount = 0;
                foreach (var item in grdDados.Columns)
                {
                    if (icount > 0 && icount < 4)

                        switch (item.GetType().Name)
                        {
                            case "TemplateField":
                                TemplateField t = new TemplateField();
                                t = (TemplateField)item;
                                t.HeaderStyle.CssClass = string.Format("tabelaHeader asc");
                                if (t.SortExpression == e.SortExpression)
                                {
                                    if (AscendingSort)
                                    {
                                        t.HeaderStyle.CssClass = string.Format("tabelaHeader asc");
                                    }
                                    else
                                    {
                                        t.HeaderStyle.CssClass = string.Format("tabelaHeader desc");
                                    }
                                }
                                break;

                            case "BoundField":
                                BoundField f = new BoundField();
                                f = (BoundField)item;
                                f.HeaderStyle.CssClass = string.Format("tabelaHeader asc");
                                if (f.SortExpression == e.SortExpression)
                                {
                                    if (AscendingSort)
                                    {
                                        f.HeaderStyle.CssClass = string.Format("tabelaHeader asc");
                                    }
                                    else
                                    {
                                        f.HeaderStyle.CssClass = string.Format("tabelaHeader desc");
                                    }
                                }
                                break;
                        }
                    icount++;

                }

                #region Campos Sorting
                switch (e.SortExpression)
                {
                    case "TipoArquivoId":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.TipoArquivoId).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.TipoArquivoId).ToArray();
                        break;

                    case "Titulo":
                        if (AscendingSort)
                            dados = dados.OrderBy(u => u.Titulo).ToArray();
                        else
                            dados = dados.OrderByDescending(u => u.Titulo).ToArray();
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
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

}