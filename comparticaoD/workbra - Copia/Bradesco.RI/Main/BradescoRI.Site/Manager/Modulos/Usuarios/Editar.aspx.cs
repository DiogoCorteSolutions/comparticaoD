using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Usuarios_Editar : System.Web.UI.Page
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

                gobjUsuario = DOUsuario.ObterUsuarioId(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gobjUsuario);
            }
            else
                btnGerarSenha.Visible = false;
        }
    }

    #region Variáveis
    private int codigo;
    private Usuario gobjUsuario;
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

    protected void btnGerarSenha_Click(object sender, EventArgs e)
    {
        AtualizarSenha();
    }
    
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.ddlPerfil.DataSource = DOPerfil.Listar();
        this.ddlPerfil.DataTextField = "Nome";
        this.ddlPerfil.DataValueField = "Id";
        this.ddlPerfil.DataBind();
        this.ddlPerfil.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        btnGerarSenha.OnClientClick = string.Concat("javascript:return confirm('", Resources.Textos.Confirm_Gerar_Senha, "')");

        this.rfvPerfil.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvSenha.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvLogin.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvSenha.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Usuario objUsuario = null)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                codigo = 0;

                ddlPerfil.SelectedValue = "0";
                txtNome.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtLogin.Text = string.Empty;
                txtSenha.Text = string.Empty;
                chkAtivo.Checked = false;
                pnlUltimoAcesso.Visible = false;

                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (objUsuario == null)
                {
                    gobjUsuario = new Usuario();
                }

                gobjUsuario.Id = codigo;
                gobjUsuario.IdPerfil = Convert.ToInt32(ddlPerfil.SelectedValue);
                gobjUsuario.Ativo = chkAtivo.Checked;
                gobjUsuario.Nome = txtNome.Text;
                gobjUsuario.Email = txtEmail.Text;
                gobjUsuario.Login = txtLogin.Text;
                
                if (!string.IsNullOrEmpty(txtSenha.Text))
                {
                    gobjUsuario.Senha = Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(txtSenha.Text);
                }
                
                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:

                ddlPerfil.SelectedValue = objUsuario.IdPerfil.ToString();
                chkAtivo.Checked = objUsuario.Ativo;
                txtNome.Text = objUsuario.Nome.ToString();
                txtEmail.Text = objUsuario.Email.ToString();
                txtLogin.Text = objUsuario.Login.ToString();
                txtSenha.Text = objUsuario.Senha.ToString();
                pnlSenha.Visible = false;
                rfvSenha.Enabled = false;
                pnlUltimoAcesso.Visible = true;
                txtDataUltimoAcesso.Text = objUsuario.DataUltimoAcesso.ToString();

                break;
        }
    }

    private void AtualizarSenha()
    {
        try
        {
            string senhaNova = Utilitarios.CriptografiaSeguranca.GerarSenha();

            DOUsuario.AtualizarSenha(Convert.ToInt32(Request.QueryString["Id"]), Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(senhaNova));
            ((Modulos_Modulos)Master).ExibirMensagem(Resources.Usuario.Mensagem_Senha_Alterada + " " + senhaNova);
            txtDataUltimoAcesso.Text = string.Empty;
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }

    }

    private void Salvar()
    {

        if (pnlSenha.Visible)
            if (!string.IsNullOrEmpty(txtSenha.Text))
            {
                if (!Utilitarios.Validacao.Campos.ValidaSenha(txtSenha.Text))
                {
                    ((Modulos_Modulos)Master).ExibirMensagem(Resources.Login.Erro_Validar_Senha);
                    return;
                }
            }

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gobjUsuario);

            if (codigo == 0)
            {
                DOUsuario.Inserir(gobjUsuario);
                Response.Redirect("Listar.aspx?sucesso=1");
            }
            else
            {
                DOUsuario.Atualizar(gobjUsuario);
                Response.Redirect("Listar.aspx?sucesso=2");
            }


        }
        catch (SqlException sqlEx)
        {
            //"Cannot insert duplicate key row in object"
            if (sqlEx.Number == 2601)
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Login.Mensagem_Login_Duplicado);
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