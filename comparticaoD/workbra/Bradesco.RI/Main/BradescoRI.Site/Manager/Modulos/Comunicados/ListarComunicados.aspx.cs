using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Comunicados_ListarComunicados : System.Web.UI.Page
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
        Response.Redirect("EditarComunicados.aspx", true);
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
                                var comunicado = new Comunicado() { ID = int.Parse(item.Cells[1].Text) };
                                if (DoComunicado.Excluir(comunicado) > 0)
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
                    //((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Exclusao_sucesso);
                    LerDados();
                }
                else
                {
                    //((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Exclusao_Erro);
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

    protected void grdDados_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    Comunicado comunicado = (Comunicado)e.Row.DataItem;

                    Label lbl = (Label)e.Row.FindControl("lblTipoComunicado");
                    lbl.Text = DOTipoArquivo.Obter(new TipoArquivo() { Id = comunicado.TipoComunicado.ID }).Descricao;
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

    protected void listPager_PageChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoComunicado_SelectedIndexChanged(object sender, EventArgs e)
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
        TipoNoticia  objTipoArquivo = null;
        List<Comunicado> objDados = null;
        try
        {
            btnNovo.Enabled = true;
            btnExcluir.Enabled = true;

            objTipoArquivo = new TipoNoticia();

            if (ddlTipoComunicado.SelectedIndex > 0)
                objTipoArquivo.ID = Convert.ToInt32(ddlTipoComunicado.SelectedValue.ToString());

            objDados = DoComunicado.Listar(new Comunicado() { TipoComunicado = objTipoArquivo });

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

                CarregarTipoComunicado();
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

    private void CarregarTipoComunicado()
    {
        try
        {
            ddlTipoComunicado.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Comunicado = true }); //DoTipoComunicado.Listar();
            ddlTipoComunicado.DataTextField = "Descricao";
            ddlTipoComunicado.DataValueField = "Id";
            ddlTipoComunicado.DataBind();

            ddlTipoComunicado.Items.Insert(0, new ListItem() { Value = "-1", Text = Resources.Comunicados.SelecioneTipoComunicado });
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion



    
}