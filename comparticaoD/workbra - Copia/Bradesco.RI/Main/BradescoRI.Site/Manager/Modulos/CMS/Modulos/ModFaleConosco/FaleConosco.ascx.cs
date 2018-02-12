using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDN_Captcha;
using System.IO;
using System.Text;

public partial class Modulos_CMS_Modulos_ModFaleConosco_FaleConosco : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IniciaTela();            
        }

        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    public string Email { get; set; }
    #endregion

    #region Métodos

    private void IniciaTela()
    { 
        btnEnviar.Text = Resources.FaleConosco.Botao_Enviar;
        
        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvMensagem.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

        this.txtNome.ToolTip = Resources.FaleConosco.NomeDescricao;
        this.txtEmail.ToolTip = Resources.FaleConosco.EmailDescricao;
        this.txtTelefoneDDD.ToolTip = Resources.FaleConosco.TelefoneDDDDescricao;
        this.txtTelefone.ToolTip = Resources.FaleConosco.TelefoneDescricao;
        this.txtEmpresa.ToolTip = Resources.FaleConosco.EmpresaDescricao;
        this.txtMensagem.ToolTip = Resources.FaleConosco.MensagemDescricao;

        LimparTela();
    }

    private void LimparTela()
    {
        this.txtNome.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        this.txtTelefone.Text = string.Empty;
        this.txtTelefoneDDD.Text = string.Empty;
        this.txtEmpresa.Text = string.Empty;
        this.txtMensagem.Text = string.Empty;
        this.ddlAssunto.SelectedIndex = 0;
    }

    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        ModFaleConosco objModFaleConosco = DOModFaleConosco.Obter(IdConteudo, IdIdioma);

        if (objModFaleConosco.Assunto != null)
        {
            string[] lstAssunto= objModFaleConosco.Assunto.Split(';');
            Email = objModFaleConosco.Email;

            ddlAssunto.DataSource = lstAssunto;
            ddlAssunto.DataBind();

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }

    }
    #endregion

    #region Eventos
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (CaptchaFaleConosco.IsValid == true)
        {
            try
            {
                EnviarEmail();

                LimparTela();

                lblMensagemSucesso.Visible = true;
                lblMensagemErro.Visible = false;
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema);
                lblMensagemSucesso.Visible = false;
                lblMensagemErro.Visible = true;
            }
        }
        else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrtErro", "alert('" + (String)CaptchaFaleConosco.ErrorMessage + " \\n Digite a sequencia nova disponiblizada.');", true);

    }

    private bool EnviarEmail()
    {
        try
        {
            Utilitarios.Email.MailMessage msg = new Utilitarios.Email.MailMessage();

            string strCorpo = string.Empty;
            FileStream fileStream;
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

            fileStream = new FileStream(System.Configuration.ConfigurationManager.AppSettings["BradescoRI.Template.FaleConosco"], FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream, encoding);
            strCorpo = streamReader.ReadToEnd();
            streamReader.Close();

            if (!string.IsNullOrEmpty(strCorpo))
            {
                strCorpo = strCorpo.Replace("#NOME#", txtNome.Text);
                strCorpo = strCorpo.Replace("#EMAIL#", txtEmail.Text);
                strCorpo = strCorpo.Replace("#TELEFONEDDD#", txtTelefoneDDD.Text);
                strCorpo = strCorpo.Replace("#TELEFONE#", txtTelefone.Text);
                strCorpo = strCorpo.Replace("#EMPRESA#", txtEmpresa.Text);
                strCorpo = strCorpo.Replace("#MENSAGEM#", txtMensagem.Text);
                strCorpo = strCorpo.Replace("#ASSUNTO#", ddlAssunto.SelectedItem.Text);

                string[] lstEmail = Email.Split(';');

                for (int i = 0; i < lstEmail.Length; i++)
                {
                    // Adiciona Destinatário
                    msg.To.Add(new System.Net.Mail.MailAddress(lstEmail[i]));
                }

                msg.Body = strCorpo;
                msg.IsBodyHtml = true;
                msg.Subject = Resources.FaleConosco.Email_Titulo_Fale_Conosco;

                //ENVIA O E-MAIL
                new Utilitarios.Email.SendMail(msg, false);

                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}