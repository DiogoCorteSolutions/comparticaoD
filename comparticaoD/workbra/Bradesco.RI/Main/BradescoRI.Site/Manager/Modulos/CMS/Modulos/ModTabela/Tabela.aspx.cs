using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModTabela_Tabela : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["conteudoId"] != null)
            {
                IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);

            }


        }
    }

    #region variaveis
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

    public int IdModTabela
    {
        get { return (int)(ViewState["IdModTabela"] ?? 0); }
        set { ViewState["IdModTabela"] = value; }

    }
    public int IdModTabelas
    {
        get { return (int)(ViewState["IdModTabelas"] ?? 0); }
        set { ViewState["IdModTabelas"] = value; }

    }
    public int IdModTabela1
    {
        get { return (int)(ViewState["tr"] ?? 0); }
        set { ViewState["tr"] = value; }

    }

    public Tabela gobtabela
    {
        get { return (Tabela)(ViewState["Tabela"] ?? null); }
        set { ViewState["Tabela"] = value; }
    }

    public Coluna gobcoluna
    {
        get { return (Coluna)(ViewState["Coluna"] ?? null); }
        set { ViewState["Coluna"] = value; }
    }
    #endregion

    #region eventos

    protected void btnSalvarColuna_Click(object sender, EventArgs e)
    {
        if (grdDados.Items.Count < 30)
        {
            try
            {
                CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

                int IdModTabelas = DOModTabela.InserirTabela(gobtabela);

                IniciarTela();
                LerDadostb(IdModTabelas);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
            }
        }
        else
            lblMensagem.Text = string.Format(Resources.Textos.Mensagem_Maximo_Registros, "3"); ;
    }

    protected void btnSalvarNovaColuna_Click(object sender, EventArgs e)
    {
        if (grdDados.Items.Count < 30)
        {
            try
            {
                CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

                var IdTabela = DOModTabela.InserirTabela(gobtabela);

                DOModTabela.InserirColuna(new Tabela() { IdModTabela = Convert.ToInt32(gobtabela.IdModTabela), NomeColuna = gobtabela.NomeAcionario });

                IniciarTela();
                LerDados(gobtabela.IdModTabela);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
            }
        }
        else
            lblMensagem.Text = string.Format(Resources.Textos.Mensagem_Maximo_Registros, "3"); ;
    }

    protected void btnSalvarValorColuna_Click(object sender, EventArgs e)
    {
        if (grdDados.Items.Count < 30)
        {
            try
            {
                CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

                //IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue)

                DOModTabela.InserirValorColuna(new VColuna() { IdColunaTabela = Convert.ToInt32(ddlColuna.SelectedValue), ValorColuna = gobtabela.ValorColuna, IdModTabela = Convert.ToInt32(gobtabela.IdModTabela) });

                IniciarTela();
                LerDadosCol(gobtabela.IdModTabela);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Salvar, ex.Message);
            }
        }
        else
            lblMensagem.Text = string.Format(Resources.Textos.Mensagem_Maximo_Registros, "3"); ;
    }


    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensagem.Text = string.Empty;
        LerDados(IdModTabela);
    }

    protected void ddlColuna_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensagem.Text = string.Empty;
        LerColunas(gobtabela.IdModTabela);
    }


    protected void btnFechar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
    }

    protected void grdDados_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "editar")
        {
            try
            {
                lblMensagem.Text = string.Empty;
                DOModTabela.InserirColuna(new Tabela() { IdModTabela = Convert.ToInt32(gobtabela.IdModTabela), NomeColuna = gobtabela.NomeAcionario });
                LerDados(gobtabela.IdModTabela);
            }
            catch (Exception ex)
            {
                DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
                lblMensagem.Text = String.Format(Resources.Modulos.Mensagem_Erro_Excluir, ex.Message);
            }
        }
    }


    #endregion

    #region Metodos

    private void IniciarTela()
    {
        //this.lblMensagem.Text = string.Empty;
        //this.txtColuna.Text = string.Empty;
        //this.txtTabela.Text = string.Empty;

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();
        this.ddlColuna.DataSource = DOModTabela.ListarColunasPorId(gobtabela.IdModTabela);
        this.ddlColuna.DataTextField = "NomeColuna";
        this.ddlColuna.DataValueField = "IdColunaTabela";
        this.ddlColuna.DataBind();

    }
    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Salvar:
                if (gobtabela == null)
                {
                    gobtabela = new Tabela();

                }

                gobtabela.NomeAcionario = txtColuna.Text;
                gobtabela.IdModTabela = Convert.ToInt32(txtIdModTabela.Text);
                gobtabela.ValorColuna = txtValorColuna.Text;

                break;

        }
    }
    private void LerDadostb(int IdModTabela)
    {
        try
        {
            List<Tabela> objDados = null;

            objDados = DOModTabela.ListarPorId(IdModTabela);


            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hastData = false;

                if (grdDados.Items.Count > 0)
                {
                    hastData = true;

                }

                lblNoRecordsFound.Visible = !hastData;
                grdDados.Visible = hastData;

            }

        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = string.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }
    private void LerDados(int IdModTabela)
    {
        try
        {
            List<Tabela> objDados = null;

            objDados = DOModTabela.ListarPorIdFind(gobtabela.IdModTabela);


            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hastData = false;

                if (grdDados.Items.Count > 0)
                {
                    hastData = true;

                }

                lblNoRecordsFound.Visible = !hastData;
                grdDados.Visible = hastData;

            }

        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = string.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }
    private void LerDadosCol(int IdModTabela)
    {
        try
        {
            List<Tabela> objDados = null;

            objDados = DOModTabela.ListaColunaFind(gobtabela.IdModTabela);


            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hastData = false;

                if (grdDados.Items.Count > 0)
                {
                    hastData = true;

                }

                lblNoRecordsFound.Visible = !hastData;
                grdDados.Visible = hastData;

            }

        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = string.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }

    private void LerColunas(int IdModTabela)
    {
        try
        {
            List<Lista> objDados = null;

            objDados = DOModTabela.ListarColunasPorId(IdModTabela);


            if (objDados != null)
            {
                grdDados.DataSource = objDados;
                grdDados.DataBind();

                bool hastData = false;

                if (grdDados.Items.Count > 0)
                {
                    hastData = true;

                }

                lblNoRecordsFound.Visible = !hastData;
                grdDados.Visible = hastData;

            }

        }
        catch (Exception ex)
        {
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, (UserContext.Logado ? UserContext.UsuarioLogado.Id : 0));
            lblMensagem.Text = string.Format(Resources.Modulos.Mensagem_Erro_Bind, ex.Message);
        }
    }

    //public void SalvarArquivo(string[] collection, )
    //{
    //    try
    //    {
    //        if (fupArquivo.HasFile)
    //        {
    //            string strNomeArquivo = Server.MapPath(String.Format("{0}/{1}/{1}_{2}_{3}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModCaixas"], IdConteudo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

    //            if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
    //                Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

    //            fupArquivo.SaveAs(strNomeArquivo);
    //        }

    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    #endregion

    

}