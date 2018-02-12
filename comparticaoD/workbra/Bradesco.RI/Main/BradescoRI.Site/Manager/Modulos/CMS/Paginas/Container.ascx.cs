using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Modulos_Paginas_Container : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
            ExibeControles();
    }

    #region Eventos
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        Excluir();
    }

    protected void btnSubir_Click(object sender, EventArgs e)
    {
        SubirModulo();
    }

    protected void btnDescer_Click(object sender, EventArgs e)
    {
        DescerModulo();
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        EditarModulo();
    }
    #endregion

    #region Métodos

    private void ExibeControles()
    {
        try
        {
            //Verifica se existe página de configuração do módulo e exibe botão para edição.
            ConteudoPagina objConteudoPagina = DOConteudoPagina.Obter(Convert.ToInt32(this.ID.Split('_')[1]));
            btnEditar.Visible = File.Exists(Server.MapPath(String.Concat("/Manager/Modulos/CMS/Modulos", objConteudoPagina.Arquivo.Replace(".ascx", ".aspx"))));
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }
    private void Excluir()
    {
        try
        {
            DOConteudoPagina.Excluir(Convert.ToInt32(this.ID.Split('_')[1]));

            ExcluirConteudoImagens();

            Response.Redirect(string.Concat("Template.aspx?paginaId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(Request.Params.Get("hidPaginaId"))));
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    private void SubirModulo()
    {
        try
        {
            DOConteudoPagina.OrganizarConteudo(Convert.ToInt32(this.ID.Split('_')[1]), true);
            Response.Redirect(string.Concat("Template.aspx?paginaId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(Request.Params.Get("hidPaginaId"))));
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    private void DescerModulo()
    {
        try
        {
            DOConteudoPagina.OrganizarConteudo(Convert.ToInt32(this.ID.Split('_')[1]), false);
            Response.Redirect(string.Concat("Template.aspx?paginaId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(Request.Params.Get("hidPaginaId"))));
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }


    private void EditarModulo()
    {
        try
        {
            ConteudoPagina objConteudoPagina = DOConteudoPagina.Obter(Convert.ToInt32(this.ID.Split('_')[1]));

            string page = String.Concat(String.Concat("/Manager/Modulos/CMS/Modulos", objConteudoPagina.Arquivo.Replace(".ascx", ".aspx")), "?conteudoId=" + objConteudoPagina.ConteudoId.ToString() + "&paginaId=" + Request.Params.Get("hidPaginaId"));

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(900/2);var Mtop = (screen.height/2)-(500/2);window.open( '" + page + "', null, 'height=500,width=900,modal=yes;status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// 
    /// Verifica se o módulo possui algum conteúdo de imagens e caso positivo exclui
    /// </summary>
    private void ExcluirConteudoImagens()
    {
        string strDiretorio = Server.MapPath(System.IO.Path.Combine("~/Uploads/Imagens", this.ID.Split('_')[1]));

        if (Directory.Exists(strDiretorio))
            Directory.Delete(strDiretorio, true);
    }

    /// <summary>
    /// Chama o Método da Template passando o erro
    /// </summary>
    /// <param name="pex"></param>
    public void ExibirAlerta(Exception pex)
    {
        this.Page.GetType().InvokeMember("ExibirAlerta", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { pex });
    }


    #endregion
}