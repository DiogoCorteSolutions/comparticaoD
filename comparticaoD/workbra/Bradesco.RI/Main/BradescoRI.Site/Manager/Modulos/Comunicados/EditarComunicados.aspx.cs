using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Comunicados_EditarComunicados : System.Web.UI.Page
{
    #region Variáveis
    private List<Arquivos> Arquivos = new List<Arquivos>();
    private Usuario objUsuario;
    private int codigo, comunicado, idioma;
    private Comunicado gobjComunicado;
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

            comunicado = Convert.ToInt32(Request.QueryString["comunicado"]);
            hdnComunicadoId.Value = comunicado.ToString();
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (comunicado > 0)
            {
                gobjComunicado = DoComunicado.Obter(new Comunicado() { ID = comunicado });
                CarregarObjetos(Utilitarios.TipoTransacao.Carregar, gobjComunicado);
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

            //if (fulArquivo.HasFile)
            //{

            //    string strNomeArquivo = Server.MapPath(String.Format("/Uploads/Imagens/Comunicados/TEMP/{0}_{1}_{2}_{3}", ddlTipoComunicado.SelectedValue, ddlIdioma.SelectedValue, txtNomeArquivo.Text, fulArquivo.PostedFile.FileName));

            //    if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
            //        Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

            //    fulArquivo.SaveAs(strNomeArquivo);
            //    arq.Nome = txtNomeArquivo.Text;
            //    arq.Extensao = Path.GetExtension(strNomeArquivo);
            //    arq.Path = String.Format("/Uploads/Imagens/Comunicados/TEMP/{0}_{1}_{2}_{3}", ddlTipoComunicado.SelectedValue, ddlIdioma.SelectedValue, txtNomeArquivo.Text, fulArquivo.PostedFile.FileName);
            //    arq.UsuarioCadastro = UsuarioLogado();
            //    arq.DataCadastro = System.DateTime.Now;
            //    arq.StatusId = (int)Utilitarios.Status.Criado;
            //    arq.Inserir = true;
            //    arq.Deletar = false;
            //    Arquivos.Add(arq);
            //    Session["Arquivos"] = Arquivos;

            //    grdArquivos.DataSource = (List<Arquivo>)Session["Arquivos"];
            //    grdArquivos.DataBind();
            //}



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Manager/Modulos/Comunicados/ListarComunicados.aspx", true);
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

    protected void ddlTipoComunicado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoComunicado.SelectedIndex > 0)
            {
                CarregarArquivoComunicado();
                ddlArquivo.Enabled = true;
            }
            else
            {
                ddlArquivo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }



    #endregion

    #region Metodos

    private void CarregarArquivoComunicado()
    {
        try
        {

            ddlArquivo.DataSource = DOArquivos.Listar(new Arquivos() { TipoArquivoId = Convert.ToInt32(ddlTipoComunicado.SelectedValue) });
            ddlArquivo.DataTextField = "Titulo";
            ddlArquivo.DataValueField = "Id";
            ddlArquivo.DataBind();

            ddlArquivo.Items.Insert(0, new ListItem("Selecione o arquivo", "-1"));
            ddlArquivo.SelectedIndex = 0;

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
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar, gobjComunicado);

            if (hdnComunicadoId.Value == "0")
            {
                gobjComunicado = DoComunicado.Inserir(gobjComunicado);

                if (gobjComunicado.ID > 0)
                    hdnComunicadoId.Value = gobjComunicado.ID.ToString();

                novo = true;
            }
            else
            {
                if (DoComunicado.Alterar(gobjComunicado) > 0)
                    alterar = true;
            }

            Arquivos = (List<Arquivos>)Session["Arquivos"];

            foreach (Arquivos arq in Arquivos)
            {
                arq.TipoArquivoId = gobjComunicado.TipoComunicado.ID;

                if (arq.Inserir)
                {
                    DOModComunicado.RelacionarComunicadoArquivo(gobjComunicado.ID, arq.Id, System.DateTime.Now);
                }

                if (arq.Deletar)
                {
                    DOModComunicado.RemoverComunicadoArquivo(gobjComunicado.ID, arq.Id);
                }
            }

            if (novo)
                Response.Redirect(string.Format("EditarComunicados.aspx?Comunicado={0}&sucesso=1", gobjComunicado.ID));

            if (alterar)
                Response.Redirect(string.Format("EditarComunicados.aspx?Comunicado={0}&sucesso=2", gobjComunicado.ID));
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
        CarregarTipoComunicado();
        Session["Arquivos"] = Arquivos;
        btnAddFile.Text = Resources.Textos.Botao_Adicionar;
        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTipoComunicado.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvtitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDataComunicado.Text = Resources.Textos.Campos_Obrigatorios;

        //this.rfvNomeArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        //this.rfvArquivo.Text = Resources.Textos.Texto_Campo_Obrigatorio;


        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarTipoComunicado()
    {
        try
        {
            ddlTipoComunicado.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Comunicado = true }); //DoTipoComunicado.Listar();
            ddlTipoComunicado.DataTextField = "Descricao";
            ddlTipoComunicado.DataValueField = "Id";
            ddlTipoComunicado.DataBind();

            ddlTipoComunicado.Items.Insert(0, new ListItem("Selecione o tipo de comunicado", "-1"));
            ddlTipoComunicado.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao, Comunicado objComunicado = null)
    {
        switch (objTipoTransacao)
        {
            //Nova noticia
            case Utilitarios.TipoTransacao.Limpar:
                ddlIdioma.SelectedIndex = 0;
                ddlTipoComunicado.SelectedIndex = 0;
                txtTituloComunicado.Text = string.Empty;
                txtDescricaoComunicado.Text = string.Empty;
                txtDataComunicado.Text = string.Empty;
                Session["Arquivos"] = new List<Arquivos>();
                ddlArquivo.Items.Insert(0, new ListItem("Selecione o arquivo", "-1"));
                ddlArquivo.SelectedIndex = 0;
                break;
            //Carregar Dados da notícia
            case Utilitarios.TipoTransacao.Salvar:
                if (gobjComunicado == null)
                {
                    gobjComunicado = new Comunicado();
                }

                if (hdnComunicadoId.Value != null)
                    gobjComunicado.ID = int.Parse(hdnComunicadoId.Value.ToString());

                gobjComunicado.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjComunicado.Titulo = txtTituloComunicado.Text;
                gobjComunicado.Descricao = txtDescricaoComunicado.Text;
                gobjComunicado.TipoComunicado = DoTipoNoticia.Obter(new TipoNoticia() { ID = Convert.ToInt32(ddlTipoComunicado.SelectedValue) });
                gobjComunicado.UsuarioCadastro = UsuarioLogado();
                gobjComunicado.UsuarioAtualizacao = UsuarioLogado();
                gobjComunicado.DataComunicado = Convert.ToDateTime(txtDataComunicado.Text);
                gobjComunicado.DataCadastro = System.DateTime.Now;
                gobjComunicado.DataAtualizacao = System.DateTime.Now;
                gobjComunicado.StatusId = (int)Utilitarios.Status.Criado;
                gobjComunicado.Arquivos = (List<Arquivos>)Session["Arquivos"];
                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjComunicado.IdiomaId.ToString();
                ddlIdioma.Enabled = false;
                ddlTipoComunicado.SelectedValue = gobjComunicado.TipoComunicado.ID.ToString();
                txtTituloComunicado.Text = gobjComunicado.Titulo;
                txtDescricaoComunicado.Text = gobjComunicado.Descricao;
                txtDataComunicado.Text = gobjComunicado.DataCadastro.ToShortDateString();

                Session["Arquivos"] = CarregarArquivoComunicado(gobjComunicado);

                grdArquivos.DataSource = (List<Arquivos>)Session["Arquivos"];
                grdArquivos.DataBind();
                pnlFormArquivos.Visible = true;
                pnlGridArquivos.Visible = true;

                break;
        }
    }

    private List<Arquivos> CarregarArquivoComunicado(Comunicado gobjComunicado)
    {
        try
        {
            return DOArquivos.Listar(new Arquivos() { TipoArquivoId = gobjComunicado.TipoComunicado.ID });
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


}