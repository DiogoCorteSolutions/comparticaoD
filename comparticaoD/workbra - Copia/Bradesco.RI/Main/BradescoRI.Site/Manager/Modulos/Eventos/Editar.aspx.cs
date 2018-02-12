using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Modulos_Eventos_Editar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Id"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["Id"]);
                idioma = Convert.ToInt32(Request.QueryString["Idioma"]);

                gobjEvento = DOModEvento.Obter(codigo, idioma);

                CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
            }
        }
    }

    #region Variáveis
    private int codigo, idioma;
    private Evento gobjEvento;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtDataInicio.Text) >= DateTime.Now)
            Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listar.aspx");
    }

    public void ValidateDate(object source, ServerValidateEventArgs args)
    {
        DateTime dt;

        if (DateTime.TryParse(args.Value, out dt) == false)
            args.IsValid = false;

        //Valida se a data é maior que a atual
        if (dt <= DateTime.Now)
            args.IsValid = false;

    }
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataValueField = "ID";
        this.ddlIdioma.DataBind();

        this.ddlTipoEvento.DataSource = DOModEvento.ListarTipoEvento(Convert.ToInt32(ddlIdioma.SelectedValue));
        this.ddlTipoEvento.DataTextField = "Descricao";
        this.ddlTipoEvento.DataValueField = "IdTipoEvento";
        this.ddlTipoEvento.DataBind();

        this.rfvidioma.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTipoEvento.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTitulo.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDescricao.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvTexto.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvDataInicio.Text = Resources.Textos.Texto_Campo_Obrigatorio;

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
                ddlTipoEvento.SelectedIndex = 0;
                txtTitulo.Text = string.Empty;
                txtTexto.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtResponsavel.Text = string.Empty;
                txtLocal.Text = string.Empty;
                txtCidade.Text = string.Empty;
                txtDataInicio.Text = string.Empty;
                txtDataFim.Text = string.Empty;
                fupArquivo.Enabled = true;
                lnbArquivo.Text = string.Empty;

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjEvento == null)
                {
                    gobjEvento = new Evento();
                }

                gobjEvento.IdEvento = codigo;
                gobjEvento.IdIdioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjEvento.IdTipoEvento = Convert.ToInt32(ddlTipoEvento.SelectedValue);
                gobjEvento.Titulo = txtTitulo.Text;
                gobjEvento.Descricao = txtDescricao.Text;
                gobjEvento.Texto = txtTexto.Text;
                gobjEvento.Responsavel = txtResponsavel.Text;
                gobjEvento.Local = txtLocal.Text;
                gobjEvento.Cidade = txtCidade.Text;
                gobjEvento.DataInicio = Convert.ToDateTime(txtDataInicio.Text);
                gobjEvento.DataFim = Convert.ToDateTime(txtDataFim.Text);

                if (codigo > 0 && fupArquivo.HasFile)
                    gobjEvento.Arquivo = String.Format("{0}_{1}_{2}", codigo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                break;
            //Carregar Dados do Link
            case Utilitarios.TipoTransacao.Carregar:

                ddlIdioma.SelectedValue = gobjEvento.IdIdioma.ToString();
                ddlIdioma.Enabled = false;

                if (gobjEvento.DataInicio <= DateTime.Now)
                {
                    ddlTipoEvento.Enabled = false;
                    txtTitulo.Enabled=false;
                    txtTexto.Enabled = false;
                    txtDescricao.Enabled = false;
                    txtResponsavel.Enabled = false;
                    txtLocal.Enabled = false;
                    txtCidade.Enabled = false;
                    txtDataInicio.Enabled = false;
                    txtDataFim.Enabled = false;
                    fupArquivo.Enabled = false;
                }

                txtTitulo.Text = gobjEvento.Titulo;
                txtDescricao.Text = gobjEvento.Descricao;
                txtTexto.Text = gobjEvento.Texto;
                txtResponsavel.Text = gobjEvento.Responsavel;
                txtLocal.Text = gobjEvento.Local;
                txtCidade.Text = gobjEvento.Local;
                txtDataInicio.Text = gobjEvento.DataInicio.ToString();
                txtDataFim.Text = gobjEvento.DataFim.ToString();
                ddlTipoEvento.SelectedValue = gobjEvento.IdTipoEvento.ToString();

                if(! string.IsNullOrEmpty(gobjEvento.Arquivo))
                    lnbArquivo.Text = String.Format("/Manager/Uploads/Eventos/{0}/{1}", gobjEvento.IdEvento, gobjEvento.Arquivo);

                break;
        }
    }

    private void Salvar()
    {

        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (codigo == 0)
            {                

                codigo = DOModEvento.Inserir(gobjEvento);

                if (SalvarArquivo())
                {
                    if (fupArquivo.HasFile)
                    {
                        gobjEvento.IdEvento = codigo;
                        gobjEvento.Arquivo = String.Format("{0}_{1}_{2}", codigo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName);

                        DOModEvento.InserirArquivo(gobjEvento);
                    }

                    Response.Redirect("Listar.aspx?sucesso=1");
                }               

            }
            else
            {
                if (txtDataInicio.Enabled)
                {
                    if (SalvarArquivo())
                    {
                        DOModEvento.Atualizar(gobjEvento);

                        Response.Redirect("Listar.aspx?sucesso=2");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    private bool SalvarArquivo()
    {
        try
        {
            if (fupArquivo.HasFile)
            {
                string strNomeArquivo = Server.MapPath(String.Format("/Manager/Uploads/Eventos/{0}/{0}_{1}_{2}", codigo, ddlIdioma.SelectedValue, fupArquivo.PostedFile.FileName));

                if (!Directory.Exists(Path.GetDirectoryName(strNomeArquivo)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strNomeArquivo));

                string[] lstFiles = Directory.GetFiles(Path.GetDirectoryName(strNomeArquivo));

                foreach (string _file in lstFiles)
                    File.Delete(_file);

                fupArquivo.SaveAs(strNomeArquivo);
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}