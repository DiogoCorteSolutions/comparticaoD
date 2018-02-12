using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModEnquete_Enquete : System.Web.UI.UserControl
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
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        btnEnviar.Text = Resources.FaleConosco.Botao_Enviar;

        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

        this.hplFaleConosco.Text = Resources.Enquete.FaleConosco;

        this.txtNome.ToolTip = Resources.Enquete.NomeDescricao;
        this.txtEmail.ToolTip = Resources.Enquete.EmailDescricao;
        this.txtSugestao.ToolTip = Resources.Enquete.SugestoesDescricao;

        rdbResposta.Items.Add(new ListItem(Resources.Enquete.RespostaDividas));
        rdbResposta.Items.Add(new ListItem(Resources.Enquete.RespostaComprar));
        rdbResposta.Items.Add(new ListItem(Resources.Enquete.RespostaPoupar));
        rdbResposta.Items.Add(new ListItem(Resources.Enquete.RespostaNaoPossuo));

        LimparTela();

    }

    private void LimparTela()
    {
        this.txtNome.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        this.txtSugestao.Text = string.Empty;
    }

    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        //ModFaleConosco objModFaleConosco = DOModFaleConosco.Obter(IdConteudo, IdIdioma);

        //if (objModFaleConosco.Assunto != null)
        //{
        //    string[] lstAssunto = objModFaleConosco.Assunto.Split(';');
        //    Email = objModFaleConosco.Email;

        //    ddlAssunto.DataSource = lstAssunto;
        //    ddlAssunto.DataBind();

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        //}
        //else
        //{
        //    divSemConteudo.Visible = true;
        //    divConteudo.Visible = false;
        //}

    }
    #endregion

    #region Eventos
    protected void btnEnviar_Click(object sender, EventArgs e)
    {

        try
        {
            //EnviarEmail();

            IniciaTela();

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
    
    #endregion
}