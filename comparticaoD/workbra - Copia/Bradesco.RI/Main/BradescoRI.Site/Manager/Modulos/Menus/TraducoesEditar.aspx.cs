using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Menus_TraducoesEditar : System.Web.UI.Page
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

                gobjTraducao = DOTraducao.Obter(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo;
    private Traducao gobjTraducao;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("TraducoesListar.aspx");
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataValueField = "Id";
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataBind();

        this.rfvTexto.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvChave.Text = Resources.Textos.Texto_Campo_Obrigatorio;
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                codigo = 0;

                txtChave.Text = string.Empty;
                ddlIdioma.SelectedIndex = 0;
                txtTexto.Text = string.Empty;
                
                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjTraducao == null)
                {
                    gobjTraducao = new Traducao();
                }

                gobjTraducao.ID = codigo;
                gobjTraducao.ChaveNome = txtChave.Text;
                gobjTraducao.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjTraducao.Texto = txtTexto.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                
                txtChave.Text = gobjTraducao.ChaveNome;
                ddlIdioma.SelectedValue = gobjTraducao.IdiomaId.ToString();
                txtTexto.Text = gobjTraducao.Texto;
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
                DOTraducao.Inserir(gobjTraducao);
                Response.Redirect("TraducoesListar.aspx?sucesso=1");
            }
            else
            {
                DOTraducao.Atualizar(gobjTraducao);
                Response.Redirect("TraducoesListar.aspx?sucesso=2");
            }


        }
        catch (SqlException sqlEx)
        {
            //"Cannot insert duplicate key row in object"
            if (sqlEx.Number == 2601)
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Menu.Mensagem_Chave_Duplicado);
            else
                ((Modulos_Modulos)Master).ExibirAlerta(sqlEx);

        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}