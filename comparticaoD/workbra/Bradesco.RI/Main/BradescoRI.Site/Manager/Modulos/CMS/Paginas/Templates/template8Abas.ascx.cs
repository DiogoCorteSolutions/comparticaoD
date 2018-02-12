using System;

public partial class Modulos_CMS_Paginas_Templates_template8Abas : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Pagina objPagina = DOPagina.Obter(Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"])));

        if (!string.IsNullOrEmpty(objPagina.ConfiguracaoAbas))
        {
            controle1.InnerText = objPagina.ConfiguracaoAbas.Split('|')[0];
            controle2.InnerText = objPagina.ConfiguracaoAbas.Split('|')[1];
            controle3.InnerText = objPagina.ConfiguracaoAbas.Split('|')[2];
            controle4.InnerText = objPagina.ConfiguracaoAbas.Split('|')[3];
            controle5.InnerText = objPagina.ConfiguracaoAbas.Split('|')[4];
            controle6.InnerText = objPagina.ConfiguracaoAbas.Split('|')[5];
            controle7.InnerText = objPagina.ConfiguracaoAbas.Split('|')[6];
            controle8.InnerText = objPagina.ConfiguracaoAbas.Split('|')[7];
        }
    }
}