using System;

public partial class Modulos_CMS_Paginas_Templates_template2Abas : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Pagina objPagina = DOPagina.Obter(Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"])));

        if(!string.IsNullOrEmpty(objPagina.ConfiguracaoAbas))
        {
            controle1.InnerText = objPagina.ConfiguracaoAbas.Split('|')[0];
            controle2.InnerText = objPagina.ConfiguracaoAbas.Split('|')[1];
        }        
    }
}