using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Paginas_Template : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        //SelecionarIdioma();      
        CarregaModulosPagina();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var usuario = UsuarioLogado();

            if (usuario == null)
                Response.Redirect("~/Default.aspx", true);

            ObterSecoes(usuario.IdPerfil);
            VerificaAcessoPagina();

            if (!Page.IsPostBack)
            {
                this.IniciaTela();

                if (paginaId == 0)
                {
                    if (Request.QueryString["paginaId"] != null)
                    {
                        paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"]));
                    }
                }

                if (paginaId != 0)
                {
                    gobjPagina = DOPagina.Obter(paginaId);

                    CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
                    hidPaginaId.Value = paginaId.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    #region Variáveis
    private int paginaId;
    private Pagina gobjPagina;

    public List<Secao> gobjSecoes
    {
        get { return (List<Secao>)(Cache["Secoes"] ?? null); }
        set { Cache["Secoes"] = value; }
    }
    #endregion

    #region Eventos
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        Salvar();
        
        HabilitaEdicaoConteudo();

        if (paginaId > 0)
        {
            Response.Redirect(String.Format("Template.aspx?paginaId={0}", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(paginaId.ToString())));
        }
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        InsereModuloPagina();
    }

    protected void btnEditarAbas_Click(object sender, EventArgs e)
    {
        try
        {
            string page = String.Concat("/Manager/Modulos/CMS/Paginas/EditarAbas.aspx?paginaId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(paginaId.ToString()));

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(900/2);var Mtop = (screen.height/2)-(500/2);window.open( '" + page + "', null, 'height=500,width=900,modal=yes;status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SelecionarIdioma();
        this.CarregaModulosPagina();
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        VisualizaModuloPagina();
    }


    #endregion

    #region Métodos
    /// <summary>
    /// Inicia a tela
    /// </summary>
    private void IniciaTela()
    {
        try
        {
            lblAlias.Text = Resources.Pagina.Alias;
            lblTitulo.Text = Resources.Pagina.TituloPagina;
            lblMetaTags.Text = Resources.Pagina.MetaTags;
            lblCategoria.Text = Resources.Pagina.Categoria;
            lblDescricao.Text = Resources.Pagina.Descricao;
            lblKeyWords.Text = Resources.Pagina.KeyWords;
            lblDescription.Text = Resources.Pagina.Description;
            lblIdioma.Text = Resources.Pagina.Idioma;
            lblModulos.Text = Resources.Pagina.Modulos;
            lblCorMenu.Text = Resources.Pagina.CorMenu;
            lblTemplate.Text = Resources.Pagina.Template;
            lblAba.Text = Resources.Pagina.Aba;

            btnSalvar.Text = Resources.Textos.Botao_Salvar;
            btnAdicionar.Text = Resources.Textos.Botao_Adicionar;
            btnPreview.Text = Resources.Textos.Botao_Visualizar;
            btnEditarAbas.Text = Resources.Pagina.EditarAbas;

            divModulos.Visible = false;

            ddlCategoria.DataSource = DOPagina.ListarCategoria();
            ddlCategoria.DataTextField = "Descricao";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();

            ddlIdioma.DataSource = DOIdioma.Listar();
            ddlIdioma.DataTextField = "Nome";
            ddlIdioma.DataValueField = "ID";
            ddlIdioma.DataBind();

            ddlTemplate.DataSource = DOPagina.ListarTemplate();
            ddlTemplate.DataTextField = "Nome";
            ddlTemplate.DataValueField = "IdArquivo";
            ddlTemplate.DataBind();

            ddlCorMenu.Items.Insert(0, new ListItem(Resources.Pagina.CorMenu_Branco, "link-branco"));
            ddlCorMenu.Items.Insert(0, new ListItem(Resources.Pagina.CorMenu_Preto, "link-preto"));

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                ddlIdioma.SelectedValue = cookie.Value;
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Salva os registros no banco de dados
    /// </summary>
    private void Salvar()
    {
        try
        {
            if (Request.QueryString["paginaId"] != null)
                paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"]));

            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (paginaId > 0)
                DOPagina.Atualizar(gobjPagina);
            else
            {
                if (VerificaPaginaExistente(gobjPagina))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Resources.Textos.Mensagem_Existe_Alias + "')", true);
                    txtAlias.Text = string.Empty;
                    txtAlias.Focus();
                    return;
                }

                paginaId = DOPagina.Inserir(gobjPagina);
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    private bool VerificaPaginaExistente(Pagina gobjPagina)
    {
        try
        {
            List<Pagina> lPaginas = DOPagina.Listar();

            foreach (var pagina in lPaginas)
            {
                if (pagina.Alias.Trim() == gobjPagina.Alias.Trim())
                    return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                paginaId = 0;
                ddlCategoria.SelectedIndex = 0;
                ddlTemplate.SelectedIndex = 0;
                txtAlias.Text = string.Empty;
                txtMetatagsKeyWords.Text = string.Empty;
                txtMetatagsDescription.Text = string.Empty;
                txtTitulo.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                ddlCorMenu.SelectedIndex = 0;

                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjPagina == null)
                {
                    gobjPagina = new Pagina();
                }

                var usuario = UsuarioLogado();

                gobjPagina.PaginaId = paginaId;
                gobjPagina.CategoriaId = Convert.ToInt32(ddlCategoria.SelectedValue);
                gobjPagina.TemplateId = Convert.ToInt32(ddlTemplate.SelectedValue.Split('|')[0]);
                gobjPagina.Alias = txtAlias.Text.Replace(" ", string.Empty);
                gobjPagina.Titulo = txtTitulo.Text;
                gobjPagina.MetatagsKeywords = txtMetatagsKeyWords.Text;
                gobjPagina.MetatagsDescription = txtMetatagsDescription.Text;
                gobjPagina.Status = (int)Utilitarios.StatusPagina.Construção;
                gobjPagina.Usuario = new Usuario() { Id = usuario.Id };
                gobjPagina.Descricao = txtDescricao.Text;
                gobjPagina.CorMenu = ddlCorMenu.SelectedValue.ToString();

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                ddlCategoria.SelectedValue = gobjPagina.CategoriaId.ToString();
                ddlTemplate.SelectedValue = gobjPagina.IdTemplateArquivo;
                txtAlias.Text = gobjPagina.Alias;
                txtTitulo.Text = gobjPagina.Titulo;
                txtMetatagsKeyWords.Text = gobjPagina.MetatagsKeywords;
                txtMetatagsDescription.Text = gobjPagina.MetatagsDescription;
                txtDescricao.Text = gobjPagina.Descricao;
                ddlCorMenu.SelectedValue = gobjPagina.CorMenu;

                CarregarDropDownAbas(gobjPagina.ConfiguracaoAbas);
                HabilitaEdicaoConteudo();

                break;
        }
    }

    private void CarregarDropDownAbas(string configuracaoAbas)
    {

        if(ddlAba.Items.Count == 0)
        {
            ddlAba.Items.Insert(0, new ListItem(Resources.Pagina.AbaDefault, "0"));
        }
        
        if (!string.IsNullOrEmpty(configuracaoAbas))
        {
            int value = 1;

            foreach (var aba in configuracaoAbas.Split('|'))
            {
                if(!string.IsNullOrEmpty(aba))
                {
                    ddlAba.Items.Insert(value, new ListItem(aba, value.ToString()));

                    value = value + 1;
                }                
            }
        }
    }

    /// <summary>
    /// Popula a combo de Módulos com os cadastrados no banco de dados
    /// </summary>
    private void PopulaModulos()
    {
        try
        {
            List<Modulo> lstModulos = DOModulo.Listar();
            ddlModulos.DataTextField = "Nome";
            ddlModulos.DataValueField = "IdModuloArquivo";
            ddlModulos.DataSource = lstModulos;
            ddlModulos.DataBind();
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Adiciona o modulo selecionado no combo na pagina
    /// </summary>
    private void InsereModuloPagina()
    {
        try
        {
            SelecionarIdioma();

            //Carrega o conteudo que será adicionado na tela
            ConteudoPagina objConteudoPagina = new ConteudoPagina()
            {
                PaginaId = Convert.ToInt32(Request.Params.Get("hidPaginaId")),
                ModuloId = Convert.ToInt32(ddlModulos.SelectedValue.Split('|')[0]),
                PosicaoTemplate = Convert.ToInt32(ddlAba.SelectedValue)
            };

            int idConteudo = DOConteudoPagina.Inserir(objConteudoPagina);

            InsereConteudo(idConteudo, ddlModulos.SelectedValue.Split('|')[1], ddlTemplate.SelectedValue.Split('|')[1], objConteudoPagina.PosicaoTemplate);
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Visualiza os módulos da página
    /// </summary>
    private void VisualizaModuloPagina()
    {
        try
        {
            ConteudoPagina objConteudoPagina = new ConteudoPagina()
            {
                PaginaId = Convert.ToInt32(Request.Params.Get("hidpaginaId")),
            };

            string page = String.Concat(String.Concat("/Manager/Modulos/CMS/Paginas/Preview.aspx"), "?paginaId=" + Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(paginaId.ToString()));
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "', '', ',type=fullWindow,fullscreen,scrollbars=yes, menubar=no');", true);

        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Carrega os controles já adicionados na pagina
    /// </summary>
    private void CarregaModulosPagina()
    {
        try
        {
            if (!String.IsNullOrEmpty(Request.QueryString["paginaId"]))
            {
                paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["paginaId"]));
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.Params.Get("hidPaginaId")))
                {
                    paginaId = Convert.ToInt32(Request.Params.Get("hidPaginaId"));
                }
            }

            if (paginaId > 0)
            {
                List<ConteudoPagina> lstConteudoPagina = DOConteudoPagina.Listar(paginaId);

                placeHolderTemplate.Controls.Clear();
                placeHolderHeader.Controls.Clear();

                if (lstConteudoPagina.Count > 0)
                {
                    //Insere template a ser usado
                    string arquivoTemplate = lstConteudoPagina[0].ArquivoTemplate;

                    foreach (ConteudoPagina objConteudoPagina in lstConteudoPagina)
                    {
                        InsereConteudo(objConteudoPagina.ConteudoId, objConteudoPagina.Arquivo, arquivoTemplate, objConteudoPagina.PosicaoTemplate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    private void InsereConteudo(int pintIdConteudo, string pstrArquivo, string arquivoTemplate, int posicaoTemplate)
    {
        //Insere Template na pagina
        if (!string.IsNullOrEmpty(arquivoTemplate))
        {
            InsereTemplate(arquivoTemplate);
        }

        Control objContainer = LoadControl("Container.ascx");
        objContainer.ID = string.Concat("CTT_", pintIdConteudo);
        objContainer.Controls.AddAt(0, LoadControl(String.Concat("~/Modulos/CMS/Modulos", pstrArquivo)));

        if (pstrArquivo.ToLower().Contains("header"))
        {
            foreach (Control objControl in objContainer.Controls)
            {
                if (objControl.ID == "divSubir" || objControl.ID == "divDescer")
                {
                    objControl.Visible = false;
                }
            }

            placeHolderHeader.Controls.Add(objContainer);
        }
        else
        {
            //Percorre o template para encontrar qual placeHolder deve ser colocado o módulo
            foreach (Control objControl in placeHolderTemplate.Controls[0].Controls)
            {
                if (objControl.ID == string.Concat("placeHolder_", posicaoTemplate))
                {
                    objControl.Controls.Add(objContainer);
                }
            }
        }
    }

    private void HabilitaEdicaoConteudo()
    {
        divModulos.Visible = true;
        txtAlias.Enabled = false;
        ddlTemplate.Enabled = false;

        if (Convert.ToInt32(ddlTemplate.SelectedValue.Split('|')[0]) > 1)
        {
            lblAba.Visible = true;
            ddlAba.Visible = true;
            btnEditarAbas.Visible = true;

            CarregarDropDownAbas(string.Empty);
        }

        PopulaModulos();
    }

    /// <summary>
    /// Seleciona o Idioma da aplicação
    /// </summary>
    private void SelecionarIdioma()
    {
        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
        {
            string strIdioma = "1";

            if (!String.IsNullOrWhiteSpace(ddlIdioma.SelectedValue))
                strIdioma = ddlIdioma.SelectedValue;

            cookie.Value = strIdioma;
        }
        else
        {
            cookie = new HttpCookie("_culture");
            cookie.HttpOnly = false;
            cookie.Value = ddlIdioma.SelectedValue;
            cookie.Expires = DateTime.Now.AddYears(1);
        }
        switch (cookie.Value)
        {
            case "1":
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
                break;
            case "2":
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                break;
            default:
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
                break;
        }

        Response.Cookies.Add(cookie);
    }



    private void VerificaAcessoPagina()
    {
        try
        {
            if (Request.Path.ToLower().Contains("/modulos/default.aspx"))
            {
                return;
            }
            else
            {
                //Caminho do request sem o nome da página
                string caminhoRequest = Request.Path.ToString().Substring(0, Request.Path.ToString().LastIndexOf('/')) + "/";

                bool blnPossuiPermissao = (from s in gobjSecoes where s.Caminho.ToLower().Contains(caminhoRequest.ToLower()) select s).Any();

                if (!blnPossuiPermissao)
                {
                    Response.Redirect("/Manager/Modulos/Default.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Obtém Usuário logado, caso não esteja mais logado, é redirecionado para a página de login.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Obtém seções de menu para o usuário logado.
    /// </summary>
    /// <param name="pintIdPerfil">Id do Perfil</param>
    private void ObterSecoes(int pintIdPerfil)
    {
        int IdUsuario = 0;
        try
        {
            if (UserContext.Logado)
            {
                IdUsuario = UserContext.UsuarioLogado.Id;
            }

            gobjSecoes = DOSecao.ListarSecoesMenu(pintIdPerfil);
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    /// <summary>
    /// Exibe mensagem de erro para o usuário
    /// </summary>
    /// <param name="pex">Erro</param>
    public void ExibirAlerta(Exception pex)
    {
        int IdUsuario = 0;
        try
        {
            //Habilita a view a ser exibida           
            if (UserContext.Logado)
            {
                IdUsuario = UserContext.UsuarioLogado.Id;
            }

            //Insere erro na tabela de log
            DOLog.Inserir(string.Concat("Erro Sistema: ", pex.ToString()), Utilitarios.TipoLog.Sistema, IdUsuario);

            //Adiciona o erro na label
            lblErro.Text = pex.ToString();

            //Abre a Div
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "javascript:openModal('E');", true);

        }
        catch (Exception ex)
        {
            //Adiciona o erro na label
            lblErro.Text = ex.ToString();
        }
    }

    private void InsereTemplate(string arquivoTemplate)
    {
        try
        {
            //Somente insere Template caso não tenha sido inserido
            if (placeHolderTemplate.Controls.Count == 0)
            {
                Control objTemplate = LoadControl(string.Concat("~/Modulos/CMS/Paginas/Templates/", arquivoTemplate));
                placeHolderTemplate.Controls.AddAt(0, objTemplate);
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex);
        }
    }

    #endregion
}