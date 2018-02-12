using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModContato_Contato : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IniciaTela();
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public ModContato gobjModContato
    {
        get { return (ModContato)(ViewState["ModContato"] ?? null); }
        set { ViewState["ModContato"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        EnviarEmail();
    }
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        btnEnviar.Text = Resources.ModContato.Botao_Enviar;
    }

    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        gobjModContato = DOModContato.Obter(IdConteudo, IdIdioma);

        if (!String.IsNullOrWhiteSpace(gobjModContato.Assuntos) && !(String.IsNullOrWhiteSpace(gobjModContato.EmailTo)))
        {
            string[] strAssuntos = gobjModContato.Assuntos.Split(';');

            foreach (string strAssunto in strAssuntos)
            {
                if (!String.IsNullOrWhiteSpace(strAssunto))
                    ddlAssuntos.Items.Add(new ListItem(strAssunto, strAssunto));
            }
            ddlAssuntos.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
    }

    private void EnviarEmail()
    {
        if (gobjModContato != null)
        {
            Utilitarios.Email.MailMessage objMailMessage = new Utilitarios.Email.MailMessage();

            string strCorpo = gobjModContato.ConteudoTemplate;

            strCorpo = strCorpo.Replace("%%Nome%%", txtNome.Text);
            strCorpo = strCorpo.Replace("%%Email%%", txtEmail.Text);
            if (!String.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                string strTelefone = string.Empty;
                if (!String.IsNullOrWhiteSpace(txtTelefoneDdd.Text))
                    strTelefone = String.Format("({0}) {1}", txtTelefoneDdd.Text, txtTelefone.Text);
                else
                    strTelefone = txtTelefone.Text;

                strCorpo = strCorpo.Replace("%%Telefone%%", strTelefone);
            }

            strCorpo = strCorpo.Replace("%%Empresa%%", txtEmpresa.Text);
            strCorpo = strCorpo.Replace("%%Assunto%%", ddlAssuntos.SelectedValue);
            strCorpo = strCorpo.Replace("%%Mensagem%%", txtMensagem.Text);

            string[] lstEmailTo = gobjModContato.EmailTo.Split(';');
            string[] lstEmailTocc = null;
            string[] lstEmailTocco = null;

            if (!string.IsNullOrWhiteSpace(gobjModContato.EmailToCc))
                lstEmailTocc = gobjModContato.EmailToCc.Split(';'); ;

            if (!string.IsNullOrWhiteSpace(gobjModContato.EmailToCco))
                lstEmailTocco = gobjModContato.EmailToCco.Split(';'); ;

            //Destinatário(s)
            foreach (string strEmailTo in lstEmailTo)
            {
                if (!String.IsNullOrWhiteSpace(strEmailTo))
                    objMailMessage.To.Add(new System.Net.Mail.MailAddress(strEmailTo));
            }

            //Destinatário(s) em cópia
            if (lstEmailTocc != null)
                foreach (string strEmailTocc in lstEmailTocc)
                {
                    if (!String.IsNullOrWhiteSpace(strEmailTocc))
                        objMailMessage.CC.Add(new System.Net.Mail.MailAddress(strEmailTocc));
                }

            //Destinatário(s) em cópia oculta
            if (lstEmailTocco != null)
                foreach (string strEmailTocco in lstEmailTocco)
                {
                    if (!String.IsNullOrWhiteSpace(strEmailTocco))
                        objMailMessage.Bcc.Add(new System.Net.Mail.MailAddress(strEmailTocco));
                }

            objMailMessage.Body = strCorpo;
            objMailMessage.IsBodyHtml = true;
            objMailMessage.Subject = gobjModContato.AssuntoEmail;
            
            //ENVIA O E-MAIL
            new Utilitarios.Email.SendMail(objMailMessage, false);

            lblMensagem.Visible = true;
        }
    }

    #endregion
}