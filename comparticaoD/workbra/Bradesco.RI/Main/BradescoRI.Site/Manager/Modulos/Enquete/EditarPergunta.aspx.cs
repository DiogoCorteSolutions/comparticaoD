using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Enquete_EditarPerguntas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IdEnquete = Convert.ToInt32(Request.QueryString["EnqueteId"]);

            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["PerguntaId"] != null)
            {
                IdPergunta = Convert.ToInt32(Request.QueryString["PerguntaId"]);
                IdIdioma = Convert.ToInt32(Request.QueryString["IdiomaId"]);

                gobjEnquetePergunta = DOModEnquete.ObterPergunta(IdPergunta, IdEnquete, IdIdioma);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    public int IdPergunta
    {
        get { return (int)(ViewState["IdPergunta"] ?? 0); }
        set { ViewState["IdPergunta"] = value; }
    }
    public int IdEnquete
    {
        get { return (int)(ViewState["IdEnquete"] ?? 0); }
        set { ViewState["IdEnquete"] = value; }
    }
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 0); }
        set { ViewState["IdIdioma"] = value; }
    }
    private EnquetePergunta gobjEnquetePergunta;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("ListarPerguntas.aspx?EnqueteId={0}", Request.QueryString["EnqueteId"].ToString()));
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.txtEnquete.Text = DOModEnquete.ObterEnquete(IdEnquete).Descricao;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();
        
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvPergunta.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvResposta1.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvResposta2.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Grupo
            case Utilitarios.TipoTransacao.Limpar:

                ddlIdioma.SelectedIndex = 0;
                txtPergunta.Text = string.Empty;
                txtResposta1.Text = string.Empty;
                txtResposta2.Text = string.Empty;
                txtResposta3.Text = string.Empty;
                txtResposta4.Text = string.Empty;
                txtResposta5.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjEnquetePergunta == null)
                {
                    gobjEnquetePergunta = new EnquetePergunta();
                }

                gobjEnquetePergunta.IdEnquetePergunta = IdPergunta;
                gobjEnquetePergunta.IdEnquete = IdEnquete;
                gobjEnquetePergunta.IdIdioma = Convert.ToInt16(ddlIdioma.SelectedValue);
                gobjEnquetePergunta.Pergunta = txtPergunta.Text;
                gobjEnquetePergunta.Resposta1 = txtResposta1.Text;
                gobjEnquetePergunta.Resposta2 = txtResposta2.Text;

                if (! string.IsNullOrEmpty(txtResposta3.Text))
                    gobjEnquetePergunta.Resposta3 = txtResposta3.Text;
                if (!string.IsNullOrEmpty(txtResposta4.Text))
                    gobjEnquetePergunta.Resposta4 = txtResposta4.Text;
                if (!string.IsNullOrEmpty(txtResposta5.Text))
                    gobjEnquetePergunta.Resposta5 = txtResposta5.Text;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjEnquetePergunta.IdIdioma.ToString();
                ddlIdioma.Enabled = false;
                txtPergunta.Text = gobjEnquetePergunta.Pergunta;
                txtResposta1.Text = gobjEnquetePergunta.Resposta1;
                txtResposta2.Text = gobjEnquetePergunta.Resposta2;

                if (!String.IsNullOrWhiteSpace(gobjEnquetePergunta.Resposta3))
                    txtResposta3.Text = gobjEnquetePergunta.Resposta3;
                if (!String.IsNullOrWhiteSpace(gobjEnquetePergunta.Resposta4))
                    txtResposta4.Text = gobjEnquetePergunta.Resposta4;
                if (!String.IsNullOrWhiteSpace(gobjEnquetePergunta.Resposta5))
                    txtResposta5.Text = gobjEnquetePergunta.Resposta5;

                break;
        }
    }

    private void Salvar()
    {

        try
        {
            IdPergunta = Convert.ToInt32(Request.QueryString["PerguntaId"]);
            IdEnquete = Convert.ToInt32(Request.QueryString["EnqueteId"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (IdPergunta == 0)
            {
                DOModEnquete.InserirPergunta(gobjEnquetePergunta);

                Response.Redirect(string.Format("ListarPerguntas.aspx?EnqueteId={0}&sucesso=1", IdEnquete));
            }
            else
            {

                DOModEnquete.AtualizarPergunta(gobjEnquetePergunta);

                Response.Redirect(string.Format("ListarPerguntas.aspx?EnqueteId={0}&sucesso=2", IdEnquete));
            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}