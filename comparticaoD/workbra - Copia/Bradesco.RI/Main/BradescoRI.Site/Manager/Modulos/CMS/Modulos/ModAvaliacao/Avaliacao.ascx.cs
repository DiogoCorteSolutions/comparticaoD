using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModAvaliacao_Avaliacao : System.Web.UI.UserControl
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

        rdbRespostaFacilidadeNavegacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaPessimo));
        rdbRespostaFacilidadeNavegacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRuim));
        rdbRespostaFacilidadeNavegacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRegular));
        rdbRespostaFacilidadeNavegacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaBom));
        rdbRespostaFacilidadeNavegacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaOtimo));

        rdbRespostaQualidadeInformacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaPessimo));
        rdbRespostaQualidadeInformacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRuim));
        rdbRespostaQualidadeInformacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRegular));
        rdbRespostaQualidadeInformacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaBom));
        rdbRespostaQualidadeInformacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaOtimo));

        rdbRespostaQuantidadeConteudo.Items.Add(new ListItem(Resources.Avaliacao.RespostaPessimo));
        rdbRespostaQuantidadeConteudo.Items.Add(new ListItem(Resources.Avaliacao.RespostaRuim));
        rdbRespostaQuantidadeConteudo.Items.Add(new ListItem(Resources.Avaliacao.RespostaRegular));
        rdbRespostaQuantidadeConteudo.Items.Add(new ListItem(Resources.Avaliacao.RespostaBom));
        rdbRespostaQuantidadeConteudo.Items.Add(new ListItem(Resources.Avaliacao.RespostaOtimo));

        rdbRespostaFrequenciaAtualizacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaPessimo));
        rdbRespostaFrequenciaAtualizacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRuim));
        rdbRespostaFrequenciaAtualizacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaRegular));
        rdbRespostaFrequenciaAtualizacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaBom));
        rdbRespostaFrequenciaAtualizacao.Items.Add(new ListItem(Resources.Avaliacao.RespostaOtimo));

        rdbRespostaLayout.Items.Add(new ListItem(Resources.Avaliacao.RespostaPessimo));
        rdbRespostaLayout.Items.Add(new ListItem(Resources.Avaliacao.RespostaRuim));
        rdbRespostaLayout.Items.Add(new ListItem(Resources.Avaliacao.RespostaRegular));
        rdbRespostaLayout.Items.Add(new ListItem(Resources.Avaliacao.RespostaBom));
        rdbRespostaLayout.Items.Add(new ListItem(Resources.Avaliacao.RespostaOtimo));

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