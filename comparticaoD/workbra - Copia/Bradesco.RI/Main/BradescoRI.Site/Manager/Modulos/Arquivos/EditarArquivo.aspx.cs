using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Arquivos_EditarArquivo : System.Web.UI.Page
{
    #region Variáveis
    private Usuario objUsuario;
    private int arquivo;
    private Arquivos gObjArquivos;
    #endregion

    #region Eventos

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Arquivos/ListarArquivos.aspx", true);
    }


    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        int pRetorno = 0;
        try
        {
            pRetorno = Salvar();
            if (pRetorno >= 1)
                Response.Redirect(string.Format("ListarArquivos.aspx?sucesso={0}", pRetorno));
            else
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_erro_Upload_File);

        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    protected void rdoStreaming_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VerificaStreaming();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objUsuario = UsuarioLogado();

            if (objUsuario == null)
                Response.Redirect("~/Manager/Default.aspx", true);

            if ((Request.QueryString["Sucesso"] == "1"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Salva_Sucesso);
            }
            else if ((Request.QueryString["Sucesso"] == "2"))
            {
                ((Modulos_Modulos)Master).ExibirMensagem(Resources.Textos.Mensagem_Atualizado_Sucesso);
            }

            arquivo = Convert.ToInt32(Request.QueryString["arquivo"]);
            hdnArquivosId.Value = arquivo.ToString();
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (arquivo > 0)
            {
                gObjArquivos = DOArquivos.Obter(new Arquivos() { Id = arquivo });
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gObjArquivos);
            }
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Arquivos objArquivos = null)
    {
        try
        {
            switch (objTipoTransacao)
            {

                case Utilitarios.TipoTransacao.Limpar:
                    ddlIdioma.SelectedIndex = 0;
                    ddlTipoArquivo.SelectedIndex = 0;
                    txtTituloArquivo.Text = string.Empty;
                    txtDescricaoArquivo.Text = string.Empty;
                    break;

                case Utilitarios.TipoTransacao.Salvar:
                    if (gObjArquivos == null)
                        gObjArquivos = new Arquivos();

                    if (hdnArquivosId.Value != null)
                        gObjArquivos.Id = int.Parse(hdnArquivosId.Value.ToString());

                    gObjArquivos = DOArquivos.Obter(gObjArquivos);
                    gObjArquivos.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                    gObjArquivos.TipoArquivoId = Convert.ToInt32(ddlTipoArquivo.SelectedValue);
                    gObjArquivos.Titulo = txtTituloArquivo.Text;
                    gObjArquivos.Descricao = txtTituloArquivo.Text;

                    string strPath = string.Empty;

                    if (rdoStreaming.SelectedValue == "0")
                    {
                        if (fulArquivo.HasFile)
                        {
                            strPath = SalvarArquivo(ref gObjArquivos);
                            gObjArquivos.Caminho = strPath;
                            gObjArquivos.Extensao = System.IO.Path.GetExtension(strPath).ToLower();
                            gObjArquivos.Tamanho = Utilitarios.TamanhoArquivo.Amigavel(fulArquivo.PostedFile.ContentLength);
                        }
                        else
                        {
                            gObjArquivos.Caminho = litArquivoUploaded.Text;
                            gObjArquivos.Extensao = System.IO.Path.GetExtension(gObjArquivos.Caminho).ToLower();
                        }
                    }

                    else
                        gObjArquivos.Caminho = txtUrlStreaming.Text;

                    if (txtDataArquivo.Text.Length > 0)
                        gObjArquivos.DataArquivo = Convert.ToDateTime(txtDataArquivo.Text);

                    gObjArquivos.DataCadastro = System.DateTime.Now;
                    gObjArquivos.DataAtualizacao = System.DateTime.Now;
                    gObjArquivos.UsuarioCadastroId = UsuarioLogado().Id;
                    gObjArquivos.UsuarioAtualizacaoId = UsuarioLogado().Id;
                    gObjArquivos.Streaming = rdoStreaming.SelectedIndex == 0 ? false : true;
                    gObjArquivos.StatusId = (int)Utilitarios.StatusArquivo.Ativo;

                    break;

                case Utilitarios.TipoTransacao.Carregar:
                    gObjArquivos = DOArquivos.Obter(gObjArquivos);

                    ddlIdioma.SelectedValue = gObjArquivos.IdiomaId.ToString();
                    ddlIdioma.Enabled = false;
                    ddlTipoArquivo.SelectedValue = gObjArquivos.TipoArquivoId.ToString();
                    txtTituloArquivo.Text = gObjArquivos.Titulo;
                    txtDescricaoArquivo.Text = objArquivos.Descricao;
                    txtDataArquivo.Text = objArquivos.DataArquivo.ToShortDateString();
                    rdoStreaming.SelectedValue = objArquivos.Streaming == true ? "1" : "0";

                    if (gObjArquivos.Streaming)
                    {
                        pnlArquivoFisico.Visible = false;
                        pnlStreaming.Visible = true;
                        txtUrlStreaming.Text = objArquivos.Caminho;
                    }
                    else
                    {
                        pnlArquivoFisico.Visible = true;
                        pnlStreaming.Visible = false;
                        litArquivoUploaded.Text = objArquivos.Caminho;
                    }

                    break;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }

    private void VerificaStreaming()
    {
        try
        {
            switch (rdoStreaming.SelectedValue)
            {
                case "0":
                    pnlArquivoFisico.Visible = true;
                    pnlStreaming.Visible = false;
                    rfvArquivo.ValidationGroup = "vgrArquivos";
                    rfvUrlStreaming.ValidationGroup = string.Empty;
                    break;

                case "1":
                    pnlStreaming.Visible = true;
                    pnlArquivoFisico.Visible = false;
                    rfvArquivo.ValidationGroup = string.Empty;
                    rfvUrlStreaming.ValidationGroup = "vgrArquivos";
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string SalvarArquivo(ref Arquivos pObjArquivo)
    {
        Guid guid;
        string strFileName = string.Empty;
        var strPathArquivo = string.Empty;
        var strPathRetorno = string.Empty;
        try
        {
            if (fulArquivo.HasFile)
            {
                guid = Guid.NewGuid();

                FileInfo fi = new FileInfo(fulArquivo.PostedFile.FileName);
                string ext = fi.Extension;

                strFileName = guid.ToString() + ext;

                strPathArquivo = Server.MapPath(String.Format("/Uploads/Arquivos/{0}/{1}", ddlIdioma.SelectedValue, strFileName));
                strPathRetorno = String.Format("/Uploads/Arquivos/{0}/{1}", ddlIdioma.SelectedValue, strFileName);

                if (!Directory.Exists(Path.GetDirectoryName(strPathArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strPathArquivo));

                fulArquivo.SaveAs(strPathArquivo);

            }

            return strPathRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    #endregion

    #region Métodos Privados 
    public Usuario UsuarioLogado()
    {
        if (UserContext.Logado)
        {
            return UserContext.UsuarioLogado;
        }
        else
        {
            Response.Redirect("~/Login.aspx?l=1");
        }
        return null;
    }

    private bool VerificaArquivo()
    {
        bool retorno = false;
        try
        {
            if (rdoStreaming.SelectedValue == "0" && fulArquivo.HasFile)
                retorno = true;

            return retorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int Salvar()
    {
        int pRetorno = 0;
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gObjArquivos);

            if (gObjArquivos.Id > 0)
            {
                if (DOArquivos.Atualizar(gObjArquivos) > 0)
                    pRetorno = 2;

            }
            else
            {
                if (DOArquivos.Inserir(gObjArquivos) > 0)
                    pRetorno = 1;
            }
            return pRetorno;
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
            CarregarTipoArquivo();
            VerificaStreaming();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTipoArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDescricao.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvUrlStreaming.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvRdoStreaming.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Login.aspx");
    }

    private void CarregarTipoArquivo()
    {
        try
        {
            this.ddlTipoArquivo.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = null });
            this.ddlTipoArquivo.DataTextField = "Descricao";
            this.ddlTipoArquivo.DataValueField = "Id";
            this.ddlTipoArquivo.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarIdioma()
    {
        try
        {
            this.ddlIdioma.DataSource = DOIdioma.Listar();
            this.ddlIdioma.DataTextField = "Nome";
            this.ddlIdioma.DataValueField = "ID";
            this.ddlIdioma.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    #endregion
}