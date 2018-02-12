using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_PerfilAdr_Editar : System.Web.UI.Page
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

                gobjModPerfilAdr = DOModPerfilAdr.Obter(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo;
    private ModPerfilAdr gobjModPerfilAdr;
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
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "Id";
        this.ddlIdioma.DataBind();
        this.ddlIdioma.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.rfvIdioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvOrdem.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTexto.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvValor.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                codigo = 0;

                ddlIdioma.SelectedValue = "0";
                txtOrdem.Text = string.Empty;
                txtTexto.Text = string.Empty;
                txtValor.Text = string.Empty;

                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModPerfilAdr == null)
                {
                    gobjModPerfilAdr = new ModPerfilAdr();
                }

                int intOrdem = 0;
                int.TryParse(txtOrdem.Text, out intOrdem);

                gobjModPerfilAdr.ID = codigo;
                gobjModPerfilAdr.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjModPerfilAdr.Ordem = intOrdem;
                gobjModPerfilAdr.Titulo = txtTexto.Text;
                gobjModPerfilAdr.Valor = txtValor.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjModPerfilAdr.IdIdioma.ToString();
                txtOrdem.Text = gobjModPerfilAdr.Ordem.ToString();
                txtTexto.Text = gobjModPerfilAdr.Titulo;
                txtValor.Text = gobjModPerfilAdr.Valor;


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
                DOModPerfilAdr.Inserir(gobjModPerfilAdr);
                Response.Redirect("Listar.aspx?sucesso=1");
            }
            else
            {
                DOModPerfilAdr.Atualizar(gobjModPerfilAdr);
                Response.Redirect("Listar.aspx?sucesso=2");
            }


        }
        catch (SqlException sqlEx)
        {
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