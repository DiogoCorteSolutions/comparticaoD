using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Glossario_EditarGlossario : System.Web.UI.Page
{
    #region Variáveis
    private Usuario objUsuario;
    private int glossario;
    private Glossario gObjGlossario;
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objUsuario = ((Modulos_Modulos)Master).UsuarioLogado();

            if (objUsuario == null)
                Response.Redirect("~/Manager/Login.aspx", true);

            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            glossario = Convert.ToInt32(Request.QueryString["glossario"]);
            hdnGlossarioId.Value = glossario.ToString();
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (glossario > 0)
            {
                gObjGlossario = DOGlossario.Obter(new Glossario() { Id = glossario });
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gObjGlossario);
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Glossario/Listar.aspx", true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Salvar();
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }


    #endregion

    #region Métodos

    private void Salvar()
    {

        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gObjGlossario);

            if (gObjGlossario.Id > 0)
            {
                DOGlossario.Atualizar(gObjGlossario);
                Response.Redirect("Listar.aspx?sucesso=2", true);
            }
            else { 
                DOGlossario.Inserir(gObjGlossario);
                Response.Redirect("Listar.aspx?sucesso=1", true);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void IniciaTela()
    {
        try
        {
            CarregarIdioma();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDescricao.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Login.aspx");
    }

    private void CarregarIdioma()
    {
        try
        {
            ddlIdioma.DataSource = DOIdioma.Listar();
            ddlIdioma.DataTextField = "Nome";
            ddlIdioma.DataValueField = "Id";
            ddlIdioma.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Glossario objGlossario = null)
    {
        try
        {
            switch (objTipoTransacao)
            {

                case Utilitarios.TipoTransacao.Limpar:
                    ddlIdioma.SelectedIndex = 0;
                    txtTitulo.Text = string.Empty;
                    txtDescricaoArquivo.Text = string.Empty;
                    break;

                case Utilitarios.TipoTransacao.Salvar:
                    if (gObjGlossario == null)
                        gObjGlossario = new Glossario();

                    if (hdnGlossarioId.Value != null)
                        gObjGlossario.Id = int.Parse(hdnGlossarioId.Value.ToString());

                    gObjGlossario = DOGlossario.Obter(gObjGlossario);
                    gObjGlossario.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                    gObjGlossario.Titulo = txtTitulo.Text;
                    gObjGlossario.Descricao = txtDescricaoArquivo.Text;
                    gObjGlossario.DataCadastro = System.DateTime.Now;
                    gObjGlossario.DataAtualizacao = System.DateTime.Now;
                    gObjGlossario.UsuarioCadastro = new Usuario() { Id = ((Modulos_Modulos)Master).UsuarioLogado().Id };
                    gObjGlossario.UsuarioAtualizacao = new Usuario() { Id = ((Modulos_Modulos)Master).UsuarioLogado().Id };
                    gObjGlossario.StatusId = (int)Utilitarios.Status.Criado;
                    break;

                case Utilitarios.TipoTransacao.Carregar:
                    gObjGlossario = DOGlossario.Obter(gObjGlossario);

                    ddlIdioma.SelectedValue = gObjGlossario.IdiomaId.ToString();
                    ddlIdioma.Enabled = false;
                    txtTitulo.Text = gObjGlossario.Titulo;
                    txtDescricaoArquivo.Text = gObjGlossario.Descricao;

                    break;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    #endregion
}