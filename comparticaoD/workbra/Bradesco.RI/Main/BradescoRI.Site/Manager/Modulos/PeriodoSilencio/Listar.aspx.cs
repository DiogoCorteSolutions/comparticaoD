using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_PeriodoSilencio_Listar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
    
            this.IniciaTela();
            this.LerDados();
        }
    }
    
    #region Eventos
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

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Salvar.aspx");
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

            this.btnExcluir.Text = Resources.Textos.Botao_Excluir;
            this.btnNovo.Text = Resources.Textos.Botao_Novo;

            
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
            List<PeriodoSilencio> objDados = null;

            objDados = DOModEvento.ListarPeriodoSilencio();

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
                            DOModEvento.ExcluirPeriodoSilencio(Convert.ToInt32(item.Cells[1].Text));
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