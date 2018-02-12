using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Enquete_ListarRespostas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {          
            if (Request.QueryString["EnqueteId"] != null)
            {
                IniciaTela();
                LerDados();
            }
        }
    }

    #region Eventos
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LerDados();
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        try
        {
            
            this.ddlIdioma.DataSource = DOIdioma.Listar();
            this.ddlIdioma.DataTextField = "Nome";
            this.ddlIdioma.DataValueField = "Id";
            this.ddlIdioma.DataBind();

            this.ddlIdioma.SelectedIndex = 0;

            this.btnBuscar.Text = Resources.Textos.Botao_Buscar;           

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
            List<EnqueteResposta> objDados = null;

            objDados = DOModEnquete.ListarResposta(Convert.ToInt32(Request.QueryString["EnqueteId"]), Convert.ToInt32(ddlIdioma.SelectedValue));

            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hasData = false;

                if (grdDados.Items.Count > 0)
                    hasData = true;
                   
                lblNoRecordsFound.Visible = !hasData;
                grdDados.Visible = hasData;
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