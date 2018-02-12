using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Menus_LinksExtras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            this.IniciaTela();

            codigo = 1;
            gobjMenuLinkExtra = DOMenuLinkExtra.Listar(codigo).FirstOrDefault();
            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
        }
    }

    #region Variáveis
    private int codigo;
    private MenuLinkExtra gobjMenuLinkExtra;
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
    protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
    {
        gobjMenuLinkExtra = DOMenuLinkExtra.Listar(Convert.ToInt32(ddlNome.SelectedValue)).FirstOrDefault();
        CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.ddlNome.DataSource = DOMenuLinkExtra.Listar();
        this.ddlNome.DataTextField = "Nome";
        this.ddlNome.DataValueField = "ID";
        this.ddlNome.DataBind();

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winroot, "_top"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winparent, "_parent"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winblank, "_blank"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winself, "_self"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlPaginas.DataSource = DOPagina.Listar(0, 2);
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.rfvTarget.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTexto.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvUrl.Text = Resources.Textos.Texto_Campo_Obrigatorio;

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
                ddlNome.SelectedValue = "1";
                ddlIdioma.SelectedIndex = 0;
                ddlPaginas.SelectedIndex = 0;
                ddlTarget.SelectedIndex = 0;
                txtChave.Text = string.Empty;
                txtUrl.Text = string.Empty;
                txtTexto.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjMenuLinkExtra == null)
                {
                    gobjMenuLinkExtra = new MenuLinkExtra();
                }

                gobjMenuLinkExtra.ID = Convert.ToInt32(ddlNome.SelectedValue);
                gobjMenuLinkExtra.Nome = txtTexto.Text;
                gobjMenuLinkExtra.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjMenuLinkExtra.Target = ddlTarget.SelectedValue;
                gobjMenuLinkExtra.Url = txtUrl.Text;
                if (!String.IsNullOrWhiteSpace(txtChave.Text))
                    gobjMenuLinkExtra.ChaveNome = txtChave.Text;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlNome.SelectedValue = gobjMenuLinkExtra.ID.ToString();
                txtTexto.Text = gobjMenuLinkExtra.Nome;
                ddlIdioma.SelectedValue = gobjMenuLinkExtra.IdiomaId.ToString();
                ddlTarget.SelectedValue = gobjMenuLinkExtra.Target;
                txtUrl.Text = gobjMenuLinkExtra.Url;
                txtChave.Text = gobjMenuLinkExtra.ChaveNome;

                break;
        }
    }

    private void Salvar()
    {

        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOMenuLinkExtra.Atualizar(gobjMenuLinkExtra);
            Response.Redirect("LinksExtras.aspx?sucesso=2");
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}