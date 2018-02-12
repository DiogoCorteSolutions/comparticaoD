using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Relatorios_EditarRelatorios : System.Web.UI.Page
{
    #region Variáveis
    private List<Arquivos> Arquivos = new List<Arquivos>();
    private Usuario objUsuario;
    private int codigo, relatorio, idioma;
    private Relatorio gobjrelatorio;
    #endregion


    #region Eventos 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objUsuario = UsuarioLogado();

            if (objUsuario == null)
                Response.Redirect("~/Default.aspx", true);

            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            relatorio = Convert.ToInt32(Request.QueryString["relatorio"]);
            hdnRelatoriosId.Value = relatorio.ToString();
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (relatorio > 0)
            {
                gobjrelatorio = DoRelatorio.Obter(new Relatorio() { ID = relatorio });
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gobjrelatorio);
            }
        }
    }

    private Usuario UsuarioLogado()
    {
        try
        {
            return UserContext.UsuarioLogado;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdArquivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ExcluirArquivo")
            {
                int rowID = Convert.ToInt32(e.CommandArgument.ToString());
                Arquivos = (List<Arquivos>)Session["Arquivos"];

                if (Arquivos[rowID].Id > 0)
                {
                    Arquivos[rowID].Deletar = true;
                    Arquivos[rowID].Inserir = false;
                }
                else
                {
                    Arquivos.RemoveAt(rowID);
                }

                Session["Arquivos"] = Arquivos;

                grdArquivos.DataSource = Arquivos.Where(x => x.Deletar == false).ToList();
                grdArquivos.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTipoArquivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoArquivo.SelectedIndex > 0)
            {
                CarregarArquivo(Convert.ToInt32(ddlTipoArquivo.SelectedValue));
                ddlArquivo.Enabled = true;
            }
            else
                ddlArquivo.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
    protected void grdArquivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[0].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[0].Visible = false;
                    break;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddFile_Click(object sender, EventArgs e)
    {
        try
        {
            AdicionarArquivo();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AdicionarArquivo()
    {
        try
        {
            Arquivos = (List<Arquivos>)Session["Arquivos"];

            Arquivos arq = new Arquivos();
            arq = DOArquivos.Obter(new Arquivos() { Id = Convert.ToInt32(ddlArquivo.SelectedValue) });
            arq.Inserir = true;
            Arquivos.Add(arq);
            Session["Arquivos"] = Arquivos;
            grdArquivos.DataSource = Arquivos;
            grdArquivos.DataBind();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Relatorios/ListarRelatorios.aspx", true);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Salvar();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #endregion

    #region Metodos

    private void CarregarArquivo(int pTipoArquivoId)
    {
        List<Arquivos> lstArquivos = new List<Arquivos>();
        try
        {
            lstArquivos =  DOArquivos.Listar(new Arquivos() { TipoArquivoId = pTipoArquivoId });

            ddlArquivo.DataSource = lstArquivos;
            ddlArquivo.DataValueField = "Id";
            ddlArquivo.DataTextField = "Titulo";
            ddlArquivo.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void Salvar()
    {
        bool novo = false;
        bool alterar = false;
        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gobjrelatorio);

            if (hdnRelatoriosId.Value == "0")
            {
                gobjrelatorio = DoRelatorio.Inserir(gobjrelatorio);

                if (gobjrelatorio.ID > 0)
                    hdnRelatoriosId.Value = gobjrelatorio.ID.ToString();

                novo = true;
            }
            else
            {
                if (DoRelatorio.Alterar(gobjrelatorio) > 0)
                    alterar = true;
            }

            Arquivos = (List<Arquivos>)Session["Arquivos"];

            foreach (Arquivos arq in Arquivos)
            {
                arq.TipoArquivoId = gobjrelatorio.TipoRelatorio.Id;

                if (arq.Inserir)
                {
                    DOModRelatorio.RelacionarRelatorioArquivo(gobjrelatorio.ID, arq.Id, System.DateTime.Now);
                }

                if (arq.Deletar)
                {
                    DOArquivo.RemoverRelatorioArquivo(gobjrelatorio.ID, arq.Id);
                }
            }

            if (novo)
                Response.Redirect(string.Format("EditarRelatorios.aspx?Relatorio={0}&sucesso=1", gobjrelatorio.ID));

            if (alterar)
                Response.Redirect(string.Format("EditarRelatorios.aspx?Relatorio={0}&sucesso=2", gobjrelatorio.ID));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void IniciaTela()
    {
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();
        CarregarTipoRelatorio();
        CarregarTipoArquivo();
        Session["Arquivos"] = Arquivos;
        btnAddFile.Text = Resources.Textos.Botao_Adicionar;
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvTipoNoticia.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvDataNoticia.Text = Resources.Textos.Campos_Obrigatorios;
        //this.rfvFonte.Text = Resources.Textos.Campos_Obrigatorios;

        //this.rfvNomeArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvTipoArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Login.aspx");
    }

    private void CarregarTipoArquivo()
    {
        try
        {
            ddlTipoArquivo.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = true });
            ddlTipoArquivo.DataTextField = "Descricao";
            ddlTipoArquivo.DataValueField = "Id";

            ddlTipoArquivo.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarTipoRelatorio()
    {
        try
        {
            ddlTipoRelatorio.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = true });
            ddlTipoRelatorio.DataTextField = "Descricao";
            ddlTipoRelatorio.DataValueField = "Id";
            ddlTipoRelatorio.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Relatorio objNoticia = null)
    {
        switch (objTipoTransacao)
        {
            //Nova noticia
            case Utilitarios.TipoTransacao.Limpar:
                ddlIdioma.SelectedIndex = 0;
                ddlTipoRelatorio.SelectedIndex = 0;
                txtTituloRelatorio.Text = string.Empty;
                txtDescricaoRelatorio.Text = string.Empty;
                txtDataRelatorio.Text = string.Empty;
                Session["Arquivos"] = new List<Arquivos>();
                break;
            //Carregar Dados da notícia
            case Utilitarios.TipoTransacao.Salvar:
                if (gobjrelatorio == null)
                {
                    gobjrelatorio = new Relatorio();
                }

                if (hdnRelatoriosId.Value != null)
                    gobjrelatorio.ID = int.Parse(hdnRelatoriosId.Value.ToString());

                gobjrelatorio.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjrelatorio.Titulo = txtTituloRelatorio.Text;
                gobjrelatorio.Descricao = txtDescricaoRelatorio.Text;
                gobjrelatorio.TipoRelatorio = DOTipoArquivo.Obter(new TipoArquivo() { Id = Convert.ToInt32(ddlTipoRelatorio.SelectedValue) });
                gobjrelatorio.UsuarioCadastro = UsuarioLogado();
                gobjrelatorio.UsuarioAtualizacao = UsuarioLogado();
                gobjrelatorio.DataRelatorio = Convert.ToDateTime(txtDataRelatorio.Text);
                gobjrelatorio.DataCadastro = System.DateTime.Now;
                gobjrelatorio.DataAtualizacao = System.DateTime.Now;
                gobjrelatorio.StatusId = (int)Utilitarios.Status.Criado;
                gobjrelatorio.Arquivos = (List<Arquivos>)Session["Arquivos"];
                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjrelatorio.IdiomaId.ToString();
                ddlIdioma.Enabled = false;
                ddlTipoRelatorio.SelectedValue = gobjrelatorio.TipoRelatorio.Id.ToString();
                txtTituloRelatorio.Text = gobjrelatorio.Titulo;
                txtDescricaoRelatorio.Text = gobjrelatorio.Descricao;
                txtDataRelatorio.Text = gobjrelatorio.DataCadastro.ToShortDateString();

                Session["Arquivos"] = CarregarArquivoRelatorio(gobjrelatorio.ID);

                grdArquivos.DataSource = (List<Arquivos>)Session["Arquivos"];
                grdArquivos.DataBind();
                pnlFormArquivos.Visible = true;
                pnlGridArquivos.Visible = true;

                break;
        }
    }

    private List<Arquivos> CarregarArquivoRelatorio(int pRelatorioId)
    {
        try
        {
            return DOArquivos.ListarRelatorioArquivo(pRelatorioId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    
}