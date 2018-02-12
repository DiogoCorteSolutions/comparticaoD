using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Modulos_Aprovacao_Preview : System.Web.UI.Page
{

    private List<string> ModulosDinamicos = new List<string>();

    protected override void Render(HtmlTextWriter writer)
    {

        Boolean publicar = false;

        if (!string.IsNullOrEmpty(Request.QueryString["Aprovar"]) && Convert.ToBoolean(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["Aprovar"])))
        {
            publicar = true;
        }

        if (publicar)
        {
            CarregarPlaceHolder(publicar);
        }

        StringBuilder sb = new StringBuilder();
        HtmlTextWriter tw = new HtmlTextWriter(new System.IO.StringWriter(sb));
        base.Render(tw);
        string sContent = sb.ToString();
        writer.Write(sContent);

        if (publicar)
        {
            SalvarPagina(sContent);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.EnableViewState = false;

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Aprovar"]) && Convert.ToBoolean(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["Aprovar"])))
            {
                ExibirAlerta(Resources.Aprovacao.Publicacao_Pagina_Sucesso, false);
            }
            else
            {
                CarregarPagina();
            }
        }
    }

    #region "Eventos"

    protected void btnPublicar_Click(object sender, EventArgs e)
    {
        Publicar();
        ExibirAlerta(Resources.Aprovacao.Publicacao_Pagina_Sucesso, false);
    }

    protected void btnReprovar_Click(object sender, EventArgs e)
    {
        ExibirAlertaMotivo();
    }

    protected void btnReprovarMotivo_Click(object sender, EventArgs e)
    {
        ReprovarTela();
        ExibirAlerta(Resources.Textos.Mensagem_Salva_Sucesso, false);
    }

    #endregion

    #region "Metodos"
    private void CarregarPagina()
    {
        // Adiciona textos nos botões
        btnPublicar.Text = Resources.Textos.Botao_Publicar;
        btnReprovar.Text = Resources.Textos.Botao_Reprovar;
        btnReprovarMotivo.Text = Resources.Textos.Botao_Salvar;

        //Carrega Conteudo da página       
        CarregarPlaceHolder(false);
    }

    private void Publicar()
    {
        int paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["PaginaId"]));
        int aprovacaoId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["AprovacaoId"]));

        string urlPagina = Request.RawUrl.Split('?')[0];

        urlPagina = String.Concat(urlPagina, "?AprovacaoId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(aprovacaoId.ToString()), "&Aprovar=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt("True"), "&PaginaId=", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(paginaId.ToString()));

        //Chama a propria tela porem com o parametro de publicação
        Response.Redirect(urlPagina);
    }

    private void ReprovarTela()
    {
        try
        {
            PaginaAprovacao objPagAprovacao = null;

            if (Request.QueryString["AprovacaoId"] != null)
            {
                int aprovacaoId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["AprovacaoId"]));

                objPagAprovacao = DOPaginaAprovacao.Obter(aprovacaoId);
            }

            if (objPagAprovacao != null)
            {
                string observacao = txtMotivo.Text;

                //Aprova Pagina
                DOPaginaAprovacao.Reprovar(objPagAprovacao.AprovacaoId, UserContext.UsuarioLogado.Id, observacao);
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
        }
    }

    private void ExibirAlerta(string mensagem, Boolean erro)
    {

        int IdUsuario = 0;
        try
        {
            //Fecha a Div Motivo
            divMotivo.Visible = false;

            btnFecharTela.Visible = true;

            if (erro)
            {
                //Habilita a view a ser exibida           
                if (UserContext.Logado)
                {
                    IdUsuario = UserContext.UsuarioLogado.Id;
                }

                //Insere erro na tabela de log
                DOLog.Inserir(string.Concat("Erro Sistema: ", mensagem), Utilitarios.TipoLog.Sistema, IdUsuario);
            }

            //Abre a Div
            lblMensagem.Text = mensagem;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "javascript:openModalMensagem();", true);

        }
        catch (Exception ex)
        {
            //Adiciona o erro na label
            lblMensagem.Text = ex.ToString();
        }
    }

    private void ExibirAlertaMotivo()
    {
        try
        {
            //Abre a Div
            divMotivo.Visible = true;

            btnFecharTela.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "javascript:openModalMensagem();window.opener.location.reload(false);", true);

        }
        catch (Exception ex)
        {
            //Adiciona o erro na label
            lblMensagem.Text = ex.ToString();
        }
    }


    private void CarregarPlaceHolder(Boolean publicar)
    {
        try
        {
            if (Request.QueryString["AprovacaoId"] != null)
            {
                int aprovacaoId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["AprovacaoId"]));

                List<PaginaAprovacaoConteudo> lstConteudoPagina = DOPaginaAprovacao.ListarConteudoPaginaAprovacao(aprovacaoId);

                placeHolderTemplate.Controls.Clear();
                placeHolderHeader.Controls.Clear();

                if (lstConteudoPagina.Count > 0)
                {
                    //Insere template a ser usado
                    string arquivoTemplate = lstConteudoPagina[0].ArquivoTemplate;

                    //Insere Template na pagina
                    if (!string.IsNullOrEmpty(arquivoTemplate))
                    {
                        InsereTemplate(arquivoTemplate);
                    }

                    LiteralControl litConteudoPublicar;

                    foreach (PaginaAprovacaoConteudo objConteudoPagina in lstConteudoPagina)
                    {
                        Control objContainer = LoadControl("~/Modulos/CMS/Paginas/Container.ascx");

                        //Limpa botões de editar / excluir / subir / descer
                        objContainer.Controls.Clear();

                        objContainer.ID = string.Concat("CTT_", objConteudoPagina.ConteudoAprovacaoId);
                        objContainer.Controls.AddAt(0, LoadControl(String.Concat("~/Modulos/CMS/Modulos", objConteudoPagina.Arquivo)));

                        string nomeModulo = objConteudoPagina.Arquivo.Split('.')[0].Split('/')[2];

                        if (objConteudoPagina.Arquivo.ToLower().Contains("header"))
                        {
                            foreach (Control objControl in objContainer.Controls)
                            {
                                if (objControl.ID == "divSubir" || objControl.ID == "divDescer")
                                {
                                    objControl.Visible = false;
                                }
                            }

                            if (publicar && objConteudoPagina.ModuloDinamico)
                            {
                                if (!ModulosDinamicos.Contains(nomeModulo))
                                {
                                    //<%@ Register Src='~/Modulos/CMS/Modulos/ModFaleConosco/FaleConosco.ascx' TagPrefix='uc1' TagName='FaleConosco' %> 
                                    ModulosDinamicos.Add(String.Concat("<%@ Register Src=\"~/Modulos/CMS/Modulos", objConteudoPagina.Arquivo, "\" TagPrefix=\"uc", nomeModulo, objConteudoPagina.ModuloId, "\" TagName=\"", nomeModulo, "\" %>"));
                                }

                                litConteudoPublicar = new LiteralControl
                                {
                                    //<uc1:FaleConosco runat='server' ID='FaleConosco' IdConteudo='878' />
                                    //Text = string.Concat("<ContDinamico:CarregaConteudoDinamico runat='server' ID ='", objConteudoPagina.ConteudoAprovacaoId, "' IdModulo ='", objConteudoPagina.ModuloId, "' PathModulo='", objConteudoPagina.Arquivo, "' IdConteudo='", objConteudoPagina.ConteudoAprovacaoId, "' />")
                                    ID = objConteudoPagina.ConteudoAprovacaoId.ToString(),
                                    Text = string.Concat("<uc", nomeModulo, objConteudoPagina.ModuloId, ":", nomeModulo, " runat =\"server\" ID=\"", nomeModulo, objConteudoPagina.ConteudoAprovacaoId, "\" IdConteudo=\"", objConteudoPagina.ConteudoAprovacaoId, "\" />")
                                };

                                placeHolderHeader.Controls.Add(litConteudoPublicar);
                            }
                            else
                            {
                                placeHolderHeader.Controls.Add(objContainer);
                            }
                        }
                        else
                        {
                            //Percorre o template para encontrar qual placeHolder deve ser colocado o módulo
                            foreach (Control objControl in placeHolderTemplate.Controls[0].Controls)
                            {
                                if (objControl.ID == string.Concat("placeHolder_", objConteudoPagina.PosicaoTemplate))
                                {
                                    if (publicar && objConteudoPagina.ModuloDinamico)
                                    {
                                        
                                        if (!ModulosDinamicos.Contains(nomeModulo))
                                        {
                                            //<%@ Register Src='~/Modulos/CMS/Modulos/ModFaleConosco/FaleConosco.ascx' TagPrefix='uc1' TagName='FaleConosco' %> 
                                            ModulosDinamicos.Add(String.Concat("<%@ Register Src=\"~/Modulos/CMS/Modulos", objConteudoPagina.Arquivo, "\" TagPrefix=\"uc", nomeModulo, objConteudoPagina.ModuloId, "\" TagName=\"",nomeModulo,"\" %>"));
                                        }

                                        litConteudoPublicar = new LiteralControl
                                        {
                                            //<uc1:FaleConosco runat='server' ID='FaleConosco' IdConteudo='878' />
                                            //Text = string.Concat("<ContDinamico:CarregaConteudoDinamico runat='server' ID ='", objConteudoPagina.ConteudoAprovacaoId, "' IdModulo ='", objConteudoPagina.ModuloId, "' PathModulo='", objConteudoPagina.Arquivo, "' IdConteudo='", objConteudoPagina.ConteudoAprovacaoId, "' />")
                                            ID = objConteudoPagina.ConteudoAprovacaoId.ToString(),
                                            Text = string.Concat("<uc", nomeModulo, objConteudoPagina.ModuloId, ":", nomeModulo, " runat =\"server\" ID=\"", nomeModulo, objConteudoPagina.ConteudoAprovacaoId, "\" IdConteudo=\"", objConteudoPagina.ConteudoAprovacaoId, "\" />")
                                        };

                                        objControl.Controls.Add(litConteudoPublicar);
                                    }
                                    else
                                    {
                                        objControl.Controls.Add(objContainer);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
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
            ExibirAlerta(ex.ToString(), true);
        }
    }

    private void SalvarPagina(string renderPagina)
    {
        try
        {
            PaginaAprovacao objPagAprovacao = null;

            if (Request.QueryString["AprovacaoId"] != null)
            {
                int aprovacaoId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["AprovacaoId"]));

                objPagAprovacao = DOPaginaAprovacao.Obter(aprovacaoId);
            }


            if (objPagAprovacao != null)
            {
                string pathPagina;
                string pathBackup;

                if (objPagAprovacao.HomePage)
                {
                    pathPagina = Path.Combine(ConfigurationManager.AppSettings["BradescoRI.Path.Pagina.Default"], "Default.aspx");
                    pathBackup = Path.Combine(ConfigurationManager.AppSettings["BradescoRI.Path.Pagina.Aprovadas.Backup"], String.Concat(DateTime.Now.Day, "_", DateTime.Now.Month, "_", "Default.aspx"));
                }
                else
                {
                    pathPagina = Path.Combine(ConfigurationManager.AppSettings["BradescoRI.Path.Pagina.Aprovadas"], String.Concat(objPagAprovacao.PaginaId.ToString(), "_", objPagAprovacao.AliasPagina, ".aspx"));
                    pathBackup = Path.Combine(ConfigurationManager.AppSettings["BradescoRI.Path.Pagina.Aprovadas.Backup"], String.Concat(DateTime.Now.Day, "_", DateTime.Now.Month, "_", objPagAprovacao.PaginaId.ToString(), "_", objPagAprovacao.AliasPagina, ".aspx"));
                }

                if (!string.IsNullOrEmpty(pathPagina))
                {
                    if (File.Exists(pathPagina))
                    {
                        if (File.Exists(pathBackup))
                        {
                            File.Delete(pathBackup);
                        }

                        File.Copy(pathPagina, pathBackup);
                        File.Delete(pathPagina);
                    }
                }

                renderPagina = TrataConteudo(renderPagina, objPagAprovacao.MetatagsKeywords, objPagAprovacao.MetatagsDescription);

                using (StreamWriter writer = new StreamWriter(pathPagina, true, Encoding.UTF8))
                {
                    writer.WriteLine(renderPagina);
                }

                string observacao = string.Concat(DateTime.Now.ToString(), " - Página publicada");

                //Aprova Pagina
                DOPaginaAprovacao.Aprovar(objPagAprovacao.AprovacaoId, objPagAprovacao.PaginaId, UserContext.UsuarioLogado.Id, observacao, objPagAprovacao.HomePage);
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
        }
    }

    private string TrataConteudo(string conteudo, string MetatagsKeywords, string MetatagsDescription)
    {
        try
        {
            ////adiciona Metatags
            conteudo = conteudo.Replace("%%METATAGSKEYWORDS%%", MetatagsKeywords);
            conteudo = conteudo.Replace("%%METATAGSDESCRIPTION%%", MetatagsDescription);

            //Remove controle de publicação
            int auxOffSet = 0;
            //-------------------------------------------------------------------------
            //REMOVES <!--COMANDOS_TELA --> <!--/COMANDOS_TELA -->
            //-------------------------------------------------------------------------              
            auxOffSet = conteudo.IndexOf("<!--COMANDOS_TELA") + 1;
            conteudo = conteudo.Substring(0, auxOffSet - 1) + conteudo.Substring(conteudo.IndexOf("<!--/COMANDOS_TELA -->", auxOffSet) + 22);

            //REMOVE ACTION
            auxOffSet = conteudo.IndexOf("action=") + 1;
            conteudo = conteudo.Substring(0, auxOffSet - 1) + "runat=\"server\" " + conteudo.Substring(conteudo.IndexOf("id=", auxOffSet));

            //REMOVE VIEWSTATE
            while (conteudo.IndexOf("<div class=\"aspNetHidden\">") > 0)
            {
                auxOffSet = conteudo.IndexOf("<div class=\"aspNetHidden\">") + 1;
                conteudo = conteudo.Substring(0, auxOffSet - 1) + conteudo.Substring(conteudo.IndexOf("</div>", auxOffSet) + 6);
            }
            
            foreach (string tagRegistrar in ModulosDinamicos)
            {
                conteudo = string.Concat(tagRegistrar, conteudo);
            }            
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
        }

        return conteudo;
    }

    #endregion
}