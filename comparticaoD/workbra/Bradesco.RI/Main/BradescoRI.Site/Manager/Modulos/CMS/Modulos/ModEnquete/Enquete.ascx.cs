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
    public int PaginaAtual
    {
        get { return (int)(ViewState["PaginaAtual"] ?? 0); }
        set { ViewState["PaginaAtual"] = value; }
    }
    public int TotalPagina
    {
        get { return (int)(ViewState["TotalPagina"] ?? 0); }
        set { ViewState["TotalPagina"] = value; }
    }
    public Dictionary<int, string> Respostas
    {
        get { return (Dictionary<int, string>)(ViewState["Respostas"] ?? null); }
        set { ViewState["Respostas"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnEnviar_Click(object sender, EventArgs e)
    {

        try
        {
            int idResposta = DOModEnquete.InserirResposta(txtNome.Text, txtEmail.Text, txtSugestao.Text);

            foreach (KeyValuePair<int, string> item in Respostas)
               DOModEnquete.InserirPerguntaResposta(idResposta, item.Key, item.Value);

            
            IniciaTela();
            ObterConteudo();
                       
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
    protected void rptEnquete_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            EnquetePergunta item = (EnquetePergunta)e.Item.DataItem;
            RadioButtonList rdbResposta = (RadioButtonList)e.Item.FindControl("rdbResposta");
            HiddenField hdnPerguntaId = (HiddenField)e.Item.FindControl("hdnPerguntaId");

            hdnPerguntaId.Value = item.IdEnquetePergunta.ToString();

            rdbResposta.Items.Add(new ListItem(item.Resposta1));
            rdbResposta.Items.Add(new ListItem(item.Resposta2));
            if (!string.IsNullOrEmpty(item.Resposta3)) rdbResposta.Items.Add(new ListItem(item.Resposta3));
            if (!string.IsNullOrEmpty(item.Resposta4)) rdbResposta.Items.Add(new ListItem(item.Resposta4));
            if (!string.IsNullOrEmpty(item.Resposta5)) rdbResposta.Items.Add(new ListItem(item.Resposta5));
            
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            Button btnProximo = (Button)e.Item.FindControl("btnProximo");

            if (PaginaAtual == TotalPagina)
            {
                divEtapaFinal.Visible = true;
                btnProximo.Visible = false;
            }
        }

        
    }

    protected void btnProximo_Click(object sender, EventArgs e)
    {
        PaginaAtual++;
        ObterConteudo();
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.PaginaAtual = 0;

        btnEnviar.Text = Resources.Enquetes.Botao_Enviar;
       
        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

        this.hplFaleConosco.Text = Resources.Enquetes.FaleConosco;

        this.txtNome.ToolTip = Resources.Enquetes.NomeDescricao;
        this.txtEmail.ToolTip = Resources.Enquetes.EmailDescricao;
        this.txtSugestao.ToolTip = Resources.Enquetes.SugestoesDescricao;

        this.divEtapaFinal.Visible = false;

        this.txtNome.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        this.txtSugestao.Text = string.Empty;

        ModEnquete objModEnquete = DOModEnquete.Obter(IdConteudo);

        if (objModEnquete != null)
        {
            lblTituloEnquete.Text = objModEnquete.Titulo;
            hplFaleConosco.NavigateUrl = objModEnquete.UrlFaleConosco;
        }

    }

    private void ObterConteudo()
    {
        //salva as repostas
        foreach (RepeaterItem item in rptEnquete.Items)
        {
            RadioButtonList rdbResposta = (RadioButtonList)item.FindControl("rdbResposta");
            HiddenField hdnPerguntaId = (HiddenField)item.FindControl("hdnPerguntaId");

            if (Respostas == null)
                Respostas = new Dictionary<int, string>();

            if (!string.IsNullOrEmpty(hdnPerguntaId.Value) && !Respostas.ContainsKey(Convert.ToInt32(hdnPerguntaId.Value)) && !string.IsNullOrEmpty(rdbResposta.SelectedValue.ToString()))
                Respostas.Add(Convert.ToInt32(hdnPerguntaId.Value), rdbResposta.SelectedValue.ToString());
        }

        if(IdConteudo ==0)
            IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));

        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<EnquetePergunta> lstPerguntas = DOModEnquete.Listar(IdConteudo, IdIdioma);

        if (lstPerguntas.Count == 0)
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
        else
        {
            TotalPagina = lstPerguntas.Count;

            PagedDataSource Pgs = new PagedDataSource();
            Pgs.AllowPaging = true;
            Pgs.DataSource = lstPerguntas;
            Pgs.PageSize = 1;
            Pgs.CurrentPageIndex = PaginaAtual;

            rptEnquete.DataSource = Pgs;
            rptEnquete.DataBind();

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }

    }
    #endregion

}