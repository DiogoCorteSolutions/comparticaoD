using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Links_EditarLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grupo = Convert.ToInt32(Request.QueryString["Grupo"]);

            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Id"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["Id"]);
                idioma = Convert.ToInt32(Request.QueryString["Idioma"]);

                gobjLinks = DOModLinks.ObterLink(codigo, grupo, idioma);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, grupo, idioma;
    private Links gobjLinks;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("ListarLinks.aspx?Grupo={0}", Request.QueryString["Grupo"].ToString()));
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.txtGrupo.Text = DOModMenuCircular.ObterGrupo(grupo).Descricao;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winroot, "_top"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winparent, "_parent"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winblank, "_blank"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winself, "_self"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.ddlPaginas.DataSource = DOPagina.Listar();
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
        
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;

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
                ddlPaginas.SelectedIndex = 0;
                ddlTarget.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;
                txtUrl.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjLinks == null)
                {
                    gobjLinks = new Links();
                }

                gobjLinks.IdLink = codigo;
                gobjLinks.IdGrupo = grupo;
                gobjLinks.Titulo = txtTitulo.Text;
                gobjLinks.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);

                if (ddlTarget.SelectedValue != "0")
                    gobjLinks.Target = ddlTarget.SelectedValue;

                gobjLinks.Url = txtUrl.Text;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjLinks.IdIdioma.ToString();
                ddlIdioma.Enabled = false;

                if (!String.IsNullOrWhiteSpace(gobjLinks.Target))
                    ddlTarget.SelectedValue = gobjLinks.Target;

                txtTitulo.Text = gobjLinks.Titulo;
                txtUrl.Text = gobjLinks.Url;
                
                break;
        }
    }

    private void Salvar()
    {

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);
            grupo = Convert.ToInt32(Request.QueryString["Grupo"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (codigo == 0)
            {
                codigo = DOModLinks.InserirLink(gobjLinks);

                Response.Redirect(string.Format("ListarLinks.aspx?Grupo={0}&sucesso=1", grupo));
            }
            else
            {

                DOModLinks.AtualizarLink(gobjLinks);

                Response.Redirect(string.Format("ListarLinks.aspx?Grupo={0}&sucesso=2", grupo));
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