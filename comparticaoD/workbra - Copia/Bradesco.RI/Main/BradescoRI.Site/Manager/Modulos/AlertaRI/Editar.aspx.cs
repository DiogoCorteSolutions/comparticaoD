using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_AlertaRI_Editar : System.Web.UI.Page
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

                gobjModAlerta = DOModAlerta.Obter(codigo,(int)Utilitarios.Idioma.Portugues);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo;
    private ModAlerta gobjModAlerta;
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
        this.ddlIdioma.Items.Insert(0, new ListItem(Resources.Textos.Texto_Ambos, "3"));

        this.ddlPais.DataSource = DOPais.Listar((int)Utilitarios.Idioma.Portugues);
        this.ddlPais.DataTextField = "Nome";
        this.ddlPais.DataValueField = "Id";
        this.ddlPais.DataBind();
        this.ddlPais.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlSegmentoEmpresa.DataSource = DOSegmentoEmpresa.Listar((int)Utilitarios.Idioma.Portugues);
        this.ddlSegmentoEmpresa.DataTextField = "Nome";
        this.ddlSegmentoEmpresa.DataValueField = "Id";
        this.ddlSegmentoEmpresa.DataBind();
        this.ddlSegmentoEmpresa.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

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

                ddlIdioma.SelectedValue = "3";
                ddlPais.SelectedValue = "0";
                ddlSegmentoEmpresa.SelectedValue = "0";
                txtEmail.Text = string.Empty;
                txtEmpresa.Text = string.Empty;
                txtEstado.Text = string.Empty;
                txtNome.Text = string.Empty;
                txtTelefone.Text = string.Empty;
                txtTelefoneDDD.Text = string.Empty;
                chkProfissionalMercado.Checked = false;
                chkReceberMailing.Checked = false;

                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjModAlerta == null)
                {
                    gobjModAlerta = new ModAlerta();
                }

                gobjModAlerta.Id = codigo;
                gobjModAlerta.Nome = txtNome.Text;
                gobjModAlerta.Email = txtEmail.Text;
                gobjModAlerta.Empresa = txtEmpresa.Text;
                gobjModAlerta.TelefoneDDD = txtTelefoneDDD.Text;
                gobjModAlerta.Telefone = txtTelefone.Text;
                gobjModAlerta.IdSegmentoEmpresa = Convert.ToInt32(ddlSegmentoEmpresa.SelectedValue);
                gobjModAlerta.Estado = txtEstado.Text;
                gobjModAlerta.IdPais = Convert.ToInt32(ddlPais.SelectedValue);
                gobjModAlerta.ProfissionalMercado = chkProfissionalMercado.Checked;
                gobjModAlerta.IdIdiomaMailing = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjModAlerta.ReceberMailing = chkReceberMailing.Checked;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                txtNome.Text = gobjModAlerta.Nome;
                txtEmail.Text = gobjModAlerta.Email;
                txtEmpresa.Text = gobjModAlerta.Empresa;
                txtTelefone.Text = gobjModAlerta.Telefone;
                txtTelefoneDDD.Text = gobjModAlerta.TelefoneDDD;
                txtEstado.Text = gobjModAlerta.Estado;
                if (gobjModAlerta.IdIdiomaMailing > 0)
                    ddlIdioma.SelectedValue = gobjModAlerta.IdIdiomaMailing.ToString();
                if (gobjModAlerta.IdPais > 0)
                    ddlPais.SelectedValue = gobjModAlerta.IdPais.ToString();
                if (gobjModAlerta.IdSegmentoEmpresa > 0)
                    ddlSegmentoEmpresa.SelectedValue = gobjModAlerta.IdSegmentoEmpresa.ToString();
                chkProfissionalMercado.Checked = gobjModAlerta.ProfissionalMercado;
                chkReceberMailing.Checked = gobjModAlerta.ReceberMailing;

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
                DOModAlerta.Inserir(gobjModAlerta);
                Response.Redirect("Listar.aspx?sucesso=1");
            }
            else
            {
                DOModAlerta.Atualizar(gobjModAlerta);
                Response.Redirect("Listar.aspx?sucesso=2");
            }            
        }
        catch(SqlException sqlEx)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Texto_Email_Existe);
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}