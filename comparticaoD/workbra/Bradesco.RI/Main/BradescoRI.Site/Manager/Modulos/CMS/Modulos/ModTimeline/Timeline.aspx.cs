using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTimeline_Timeline : System.Web.UI.Page
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

    public Timeline gobjTimeline
    {
        get { return (Timeline)(ViewState["Timeline"] ?? null); }
        set { ViewState["Timeline"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (SalvarArquivo())
                if (DOTimeline.Inserir(gobjTimeline) > 0)
                {
                    IniciaTela();
                    LerDados();
                }
                else
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "alert('" + Resources.Textos.Mensagem_Salva_Erro + "' ", true);
            else
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "alert('Erro ao fazer o uploado do arquivo' ", true);

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
                DOTimeline.Excluir(new Timeline() { Id = Convert.ToInt32(e.CommandArgument) });
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
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        this.lblMensagem.Text = string.Empty;
        this.txtTitulo.Text = string.Empty;
        this.txtAno.Text = string.Empty;
        this.txtTexto.Value = string.Empty;
        this.txtAno.Text = string.Empty;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        gobjTimeline = null;
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjTimeline == null)
                {
                    gobjTimeline = new Timeline();
                }
                gobjTimeline.conteudoId = IdConteudo;
                gobjTimeline.Idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjTimeline.Titulo = txtTitulo.Text;
                gobjTimeline.Ano = Convert.ToInt32(txtAno.Text);
                gobjTimeline.Texto = txtTexto.Value;
                gobjTimeline.Imagem = String.Format("{0}_{1}_{2}", IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                break;

        }
    }


    private bool SalvarArquivo()
    {
        try
        {
            if (fupArquivo.HasFile)
            {
                string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}", "/Manager/Uploads/Imagens/", IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

                if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

                fupArquivo.SaveAs(strNomeArquivo);
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LerDados()
    {
        try
        {
            List<Timeline> objDados = null;

            objDados = DOTimeline.Listar(IdConteudo, Convert.ToInt32(ddlIdioma.SelectedValue));

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