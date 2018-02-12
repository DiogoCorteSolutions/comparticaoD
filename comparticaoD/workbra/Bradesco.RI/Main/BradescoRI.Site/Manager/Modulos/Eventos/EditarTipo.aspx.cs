using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Modulos_Eventos_EditarTipo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["IdTipo"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["IdTipo"]);

                gobjTipoEvento = DOModEvento.ObterTipo(codigo);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, idioma;
    private TipoEvento gobjTipoEvento;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarTipo.aspx");
    }

    protected void ddlCor_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgCor.BackColor = Color.FromName(ddlCor.Text);
    }
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        string[] cores = System.Enum.GetNames(typeof(System.Drawing.KnownColor));

        this.ddlCor.DataSource = cores;
        this.ddlCor.DataBind();

        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDescricao.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvCor.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        this.txtDescricao.Text = string.Empty;
        this.imgCor.BackColor = Color.White;

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
                txtDescricao.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjTipoEvento == null)
                {
                    gobjTipoEvento = new TipoEvento();
                }

                gobjTipoEvento.IdTipoEvento = codigo;
                gobjTipoEvento.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjTipoEvento.Descricao = txtDescricao.Text;
                gobjTipoEvento.Cor = ddlCor.SelectedValue;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjTipoEvento.IdIdioma.ToString();
                ddlIdioma.Enabled = false;

                ddlCor.SelectedValue = gobjTipoEvento.Cor;
                imgCor.BackColor = Color.FromName(gobjTipoEvento.Cor);

                txtDescricao.Text = gobjTipoEvento.Descricao;

                break;
        }
    }

    private void Salvar()
    {

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["IdTipo"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (codigo == 0)
            {

                DOModEvento.InserirTipo(gobjTipoEvento);

                Response.Redirect("ListarTipo.aspx?sucesso=1");

            }
            else
            {

                DOModEvento.AtualizarTipo(gobjTipoEvento);

                Response.Redirect("ListarTipo.aspx?sucesso=2");

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