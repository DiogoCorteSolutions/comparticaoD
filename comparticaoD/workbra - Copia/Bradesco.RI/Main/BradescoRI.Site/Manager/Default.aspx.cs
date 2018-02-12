using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtLogin.Attributes["placeholder"] = "usuário";
        txtSenha.Attributes["placeholder"] = "senha";

        if (!Page.IsPostBack && Request.QueryString["l"] == "1")
        {
            if (UserContext.UsuarioLogado != null)
            {
                DOLog.Inserir("Usuário - LogOff", Utilitarios.TipoLog.Usuario, UserContext.UsuarioLogado.Id);
            }

            UserContext.UsuarioLogado = null;
        }

        this.Master.FindControl("body").ID = "login";

        this.phlMensagem.Visible = false;
        this.lblMensagem.Text = string.Empty;
    }

    #region Eventos
    protected void btSubmit_Click(object sender, EventArgs e)
    {
        EfetuarLogin();
    }

    protected void btSubmitEsqueciSenha_Click(object sender, EventArgs e)
    {
        this.pnlLogin.Visible = false;
        this.pnlEsqueciSenha.Visible = true;
    }

    protected void btnEnviarEsqueci_Click(object sender, EventArgs e)
    {
        EsqueciMinhaSenha();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        this.pnlLogin.Visible = true;
        this.pnlEsqueciSenha.Visible = false;
    }

    protected void btnSalvarNovaSenha_Click(object sender, EventArgs e)
    {
        SalvarNovaSenha();
    }
    #endregion

    #region Metodos
    private void EfetuarLogin()
    {
        try
        {
            string strLogin = txtLogin.Text;
            string strSenha = txtSenha.Text;

            Usuario usuarioLogado = DOUsuario.ObterUsuarioLogin(strLogin.TrimStart('0'), Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(strSenha));

            UserContext.UsuarioLogado = usuarioLogado;

            if (usuarioLogado.Id > 0)
            {
                if (!usuarioLogado.Ativo)
                {
                    lblMensagem.Text = Resources.Login.Mensagem_Usuario_Bloqueado;
                    phlMensagem.Visible = true;
                }
                else
                {
                    bool primeiroAcesso = usuarioLogado.DataUltimoAcesso == null;

                    if (primeiroAcesso)
                    {
                        pnlLogin.Visible = false;
                        pnlPrimeiroAcesso.Visible = true;
                    }
                    else
                    {
                        DOUsuario.AtualizarAcesso(strLogin);
                        DOLog.Inserir("Usuário - Login", Utilitarios.TipoLog.Usuario, usuarioLogado.Id);

                        FormsAuthentication.SetAuthCookie(usuarioLogado.Id.ToString(), false);
                        FormsAuthentication.RedirectFromLoginPage(usuarioLogado.Id.ToString(), false);                       
                    }
                }
            }
            else
            {
                lblMensagem.Text = Resources.Login.Mensagem_Usuario_e_senha_invalidas;
                phlMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema);
            lblMensagem.Text = ex.Message.ToString();
            phlMensagem.Visible = true;
        }
    }

    private void EsqueciMinhaSenha()
    {
        try
        {
            string strEmail = txtEmailEsqueci.Text.TrimStart('0');


            if (!string.IsNullOrEmpty(strEmail))
            {
                Usuario usuarioLogado = DOUsuario.ObterUsuarioEmail(strEmail);

                if (usuarioLogado.Id > 0)
                {
                    EnviarEmail(usuarioLogado);

                    lblMensagem.Text = Resources.Login.Mensagem_Email_Enviado_Sucesso;
                    phlMensagem.Visible = true;
                }
                else
                {
                    lblMensagem.Text = Resources.Login.Mensagem_Usuario_Nao_Encontrado;
                    phlMensagem.Visible = true;
                }
            }
            else
            {
                lblMensagem.Text = Resources.Login.Mensagem_Esqueci_Senha_Login_Email;
                phlMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema);
            lblMensagem.Text = ex.Message.ToString();
            phlMensagem.Visible = true;
        }
    }

    private void EnviarEmail(Usuario objUsuario)
    {
        try
        {
            Utilitarios.Email.MailMessage msg = new Utilitarios.Email.MailMessage();

            string strCorpo = string.Empty;
            FileStream fileStream;
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

            fileStream = new FileStream(System.Configuration.ConfigurationManager.AppSettings["BradescoRI.Template.EsqueciMinhaSenha"], FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream, encoding);
            strCorpo = streamReader.ReadToEnd();
            streamReader.Close();

            if (!string.IsNullOrEmpty(strCorpo))
            {
                string novaSenha = Utilitarios.CriptografiaSeguranca.GerarSenha();

                strCorpo = strCorpo.Replace("#NOME#", objUsuario.Nome);
                strCorpo = strCorpo.Replace("#LOGIN#", objUsuario.Login);
                strCorpo = strCorpo.Replace("#SENHA#", novaSenha);

                DOUsuario.AtualizarSenha(objUsuario.Id, Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(novaSenha));

                // Adiciona Destinatário
                msg.To.Add(new System.Net.Mail.MailAddress(objUsuario.Email));

                msg.Body = strCorpo;
                msg.IsBodyHtml = true;
                msg.Subject = Resources.Login.Email_Titulo_Esqueci_Senha;

                //ENVIA O E-MAIL
                new Utilitarios.Email.SendMail(msg, false);
            }
            else
            {
                lblMensagem.Text = Resources.Login.Mensagem_Erro_Template_Email;
                phlMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SalvarNovaSenha()
    {
        try
        {
            if (txtSenhaNova.Text.Equals(txtSenhaNovaConfirma.Text))
            {
                if (!Utilitarios.Validacao.Campos.ValidaSenha(txtSenhaNova.Text, UserContext.UsuarioLogado.Senha))
                {
                    lblMensagem.Text = Resources.Login.Erro_Validar_Senha;
                    phlMensagem.Visible = true;
                }
                else
                    AtualizarAcesso();
            }
            else
            {
                lblMensagem.Text = Resources.Login.Mensagem_Erro_Confirmar_Senha;
                phlMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = Resources.Login.Mensagem_Erro_Nova_Senha;
            phlMensagem.Visible = true;
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema);
        }
    }

    private void AtualizarAcesso()
    {
        try
        {
            Usuario usuarioLogado = UserContext.UsuarioLogado;

            DOUsuario.AtualizarSenha(usuarioLogado.Id, Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(txtSenhaNova.Text));
            DOUsuario.AtualizarAcesso(usuarioLogado.Login);
            DOLog.Inserir("Usuário - Login", Utilitarios.TipoLog.Usuario, usuarioLogado.Id);

            FormsAuthentication.SetAuthCookie(usuarioLogado.Nome, false);
            FormsAuthentication.RedirectFromLoginPage(usuarioLogado.Nome, false);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

}