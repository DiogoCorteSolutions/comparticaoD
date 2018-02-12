using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

public partial class Modulos_CMS_Paginas_Preview : System.Web.UI.Page
{
    #region Variáveis    
    private int paginaId;
    #endregion

    #region Eventos
   
    protected void Page_Load(object sender, EventArgs e)
    {
        CarregaPagina();
    }

    protected void btnEnviarAprovacao_Click(object sender, EventArgs e)
    {        
        try
        {
            EnviarPaginaAprovacao(chkDefinirHome.Checked);
            ExibirAlerta(Resources.Aprovacao.Aprovacao_Pagina_Sucesso, false);
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
        }
    }
    #endregion

    #region Métodos
    private void CarregaPagina()
    {
        try
        {
            ObterDadosQueryString();

            //Carrega texto dos botões
            btnEnviarAprovacao.Text = Resources.Textos.Botao_EnviarAprovacao;
            chkDefinirHome.Text = Resources.Textos.CMS_Definir_HomePage;

            //Carrega os modulos da tela
            if (paginaId > 0)
            {
                CarregarPlaceHolder(paginaId);
            }
            else
            {
                ExibirAlerta("Erro ao carregar a página, favor repetir a operação.", false);
            }
        }
        catch (Exception ex)
        {
            ExibirAlerta(ex.ToString(), true);
        }
    }

    private void CarregarPlaceHolder(int PaginaId)
    {
        try
        {
            List<ConteudoPagina> lstConteudoPagina = DOConteudoPagina.Listar(PaginaId);

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
                
                foreach (ConteudoPagina objConteudoPagina in lstConteudoPagina)
                {
                    Control objContainer = LoadControl("Container.ascx");

                    //Limpa botões de editar / excluir / subir / descer
                    objContainer.Controls.Clear();

                    objContainer.ID = string.Concat("CTT_", objConteudoPagina.ConteudoId);
                    objContainer.Controls.AddAt(0, LoadControl(String.Concat("~/Modulos/CMS/Modulos", objConteudoPagina.Arquivo)));

                    if (objConteudoPagina.Arquivo.ToLower().Contains("header"))
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
                            if (objControl.ID == string.Concat("placeHolder_", objConteudoPagina.PosicaoTemplate))
                            {

                                objControl.Controls.Add(objContainer);
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

    private void EnviarPaginaAprovacao(Boolean homePage)
    {
        try
        {           
            //Envia pagina para aprovação e atualiza o status da mesma para pendente aprovação
            DOPaginaAprovacao.EnviarParaAprovacao(paginaId, UserContext.UsuarioLogado.Id, string.Empty, homePage);
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

    private void ObterDadosQueryString()
    {
        if (!String.IsNullOrEmpty(Request["PaginaId"]))
        {
            paginaId = Convert.ToInt32(Utilitarios.EnCryptDecrypt.CryptorEngine.Decrypt(Request.QueryString["PaginaId"]));
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
    #endregion   
}