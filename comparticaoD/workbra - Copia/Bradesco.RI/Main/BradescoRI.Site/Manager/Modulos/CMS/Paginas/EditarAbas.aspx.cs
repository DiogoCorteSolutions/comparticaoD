using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Paginas_EditarAbas : System.Web.UI.Page
{

    #region Variáveis
    private int paginaId;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      
            if (Request.QueryString["paginaId"] != null)
            {
                paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"]));
                CarregarPagina();
            }         
              
    }

    private void CarregarPagina()
    {
        try
        {
            string nomeAba;

            btnSalvar.Text = Resources.Textos.Botao_Salvar;

            Pagina objPagina = DOPagina.Obter(paginaId);

            for (int i = 1; i <= objPagina.QuantidadeAbas; i++)
            {
                nomeAba = string.Empty;

                if (!String.IsNullOrEmpty(objPagina.ConfiguracaoAbas) && !String.IsNullOrEmpty(objPagina.ConfiguracaoAbas.Split('|')[i - 1]))
                {
                    nomeAba = objPagina.ConfiguracaoAbas.Split('|')[i - 1];
                }

                Label lblTexto = new Label
                {
                    ID = "lblAba" + i.ToString(),
                    Text = "Aba" + i.ToString()
                };

                TextBox txtAba = new TextBox
                {
                    ID = "txtAba" + i.ToString(),
                    Text = nomeAba
                };


                RequiredFieldValidator rfvTextoAba = new RequiredFieldValidator
                {
                    ID = "rfvTextoAba" + i.ToString(),
                    Text = "*",
                    ControlToValidate = "txtAba" + i.ToString(),
                    ForeColor = System.Drawing.Color.Red
                };

                plhControles.Controls.Add(lblTexto);
                plhControles.Controls.Add(txtAba);
                plhControles.Controls.Add(rfvTextoAba);

                plhControles.Controls.Add(new LiteralControl("<br />"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            string configuracaoAbas = string.Empty;

            //Percorre a pagina para encontrar controles do tipo textBox
            foreach (Control objControl in plhControles.Controls)
            {
                if (objControl.ID != null && objControl.ID.Contains("txtAba"))
                {
                    configuracaoAbas = String.Concat(configuracaoAbas, ((TextBox)objControl).Text, "|");
                }
            }

            DOPagina.AtualizarAbas(paginaId, configuracaoAbas);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
        }
        catch (Exception)
        {

            throw;
        }                
    }
    #endregion

}