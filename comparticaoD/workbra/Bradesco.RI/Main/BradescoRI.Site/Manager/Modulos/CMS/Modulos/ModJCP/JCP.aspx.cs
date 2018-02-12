using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModJCP_JCP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

                this.IniciaTela();
                LerDados();
            }
        }
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
    public JCP gobjJCP
    {
        get { return (JCP)(ViewState["gobjJCP"] ?? null); }
        set { ViewState["gobjJCP"] = value; }
    }
    public string SiglaCultura
    {
        get { return (string)(ViewState["SiglaCultura"] ?? "pt-BR"); }
        set { ViewState["SiglaCultura"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
            try
            {
                CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

                int idJCP = DOModJCP.InserirJCP(gobjJCP);

                DOModJCP.Inserir(new ModJCP() { IdConteudo = IdConteudo, IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue), IdJCP = idJCP });

                IniciaTela();
                LerDados();
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
            }
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensagem.Text = string.Empty;
        LerDados();
    }

    protected void grdDados_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "excluir")
        {
            try
            {
                lblMensagem.Text = string.Empty;
                DOModJCP. ExcluirJCP(Convert.ToInt32(e.CommandArgument));
                LerDados();
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Excluir, ex.Message);
            }
        }
    }

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
    }

    protected void ddlAba_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAba.Text.Equals("Ano"))
        {
            ddlMes.Visible = true;
            txtPeriodo.Visible = false;
            divAno.Visible = true;
            divDataPagamento.Visible = true;
            divValorAcao.Visible = true;
        }
        else
        {
            ddlMes.Visible = false;
            txtPeriodo.Visible = true;
            divAno.Visible = false;
            divDataPagamento.Visible = false;
            divValorAcao.Visible = false;
        }
    }

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        LerDados();
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        SiglaCultura = DOIdioma.ObterSigla(IdIdioma);

        this.lblMensagem.Text = string.Empty;
        this.txtAno.Text = string.Empty;
        this.txtTipoProvento.Text = string.Empty;
        this.txtPosicaoAcionaria.Text = string.Empty;
        this.txtDataPagamento.Text = string.Empty;
        this.txtValorAcao.Text = string.Empty;

        this.ddlAba.SelectedIndex = 0;
        
        string[] months = System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura).DateTimeFormat.MonthNames;

        for (int i = 0; i < months.Length -1; i++)
        {
            ddlMes.Items.Add(new ListItem(char.ToUpper(months[i][0]) + months[i].Substring(1), i.ToString()));
        }

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();
        this.ddlIdioma.SelectedIndex = 0;

        this.ddlAno.DataSource = DOModJCP.ListarAno();
        this.ddlAno.DataBind();
        this.ddlAno.Items.Add(new ListItem( "HISTÓRICO DE EVENTO","0") );
        this.ddlAno.SelectedIndex = 0;

        this.ddlMes.Visible = true;
        this.txtPeriodo.Visible = false;
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjJCP == null)
                {
                    gobjJCP = new JCP();
                }

                if (ddlAba.Text.Equals("Ano"))
                {
                    gobjJCP.Ano = txtAno.Text;
                    gobjJCP.Periodo = ddlMes.SelectedItem.ToString();
                    gobjJCP.TipoProvento = txtTipoProvento.Text;
                    gobjJCP.PosicaoAcionaria = Convert.ToDateTime(txtPosicaoAcionaria.Text);
                    gobjJCP.DataPagamento = Convert.ToDateTime(txtDataPagamento.Text);
                    gobjJCP.ValorAcao = txtValorAcao.Text;
                }
                else {
                    gobjJCP.Ano = "0";
                    gobjJCP.Periodo = txtPeriodo.Text;
                    gobjJCP.TipoProvento = txtTipoProvento.Text;
                    gobjJCP.PosicaoAcionaria = Convert.ToDateTime(txtPosicaoAcionaria.Text);                    
                }
                break;

        }
    }

    private void LerDados()
    {
        try
        {
            List<JCP> objDados = null;

            objDados = DOModJCP.Listar(IdConteudo, Convert.ToInt32(ddlIdioma.SelectedValue), Convert.ToInt32( ddlAno.SelectedValue));

            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hasData = false;

                if (grdDados.Items.Count > 0)
                {
                    hasData = true;
                }

                lblNoRecordsFound.Visible = !hasData;
                grdDados.Visible = hasData;
            }
        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }

    #endregion


}