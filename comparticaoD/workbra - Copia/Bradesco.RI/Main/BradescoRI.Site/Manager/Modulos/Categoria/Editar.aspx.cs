using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Paginas_EditarArea : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Id"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["Id"]);
                gobjCategoria = DOPagina.ObterCategoria(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, idioma;
    private Categoria gobjCategoria;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listar.aspx");
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
       
        this.rfvDescricao.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Grupo
            case Utilitarios.TipoTransacao.Limpar:

                txtDescricao.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjCategoria == null)
                {
                    gobjCategoria = new Categoria();
                }

                gobjCategoria.IdCategoria = codigo;
                gobjCategoria.Descricao = txtDescricao.Text;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:
                
                txtDescricao.Text = gobjCategoria.Descricao;
                
                break;
        }
    }

    private void Salvar()
    {

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (codigo == 0)
            {
                DOPagina.InserirCategoria(gobjCategoria);

                Response.Redirect("Listar.aspx?sucesso=1");
            }
            else
            {
                DOPagina.AtualizarCategoria(gobjCategoria);

                Response.Redirect("Listar.aspx?sucesso=2");
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