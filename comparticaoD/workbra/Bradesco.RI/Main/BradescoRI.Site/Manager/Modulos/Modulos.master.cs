using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Modulos : System.Web.UI.MasterPage
{
    #region Variáveis
    public List<Secao> gobjSecoes
    {
        get { return (List<Secao>)(Cache["Secoes"] ?? null); }
        set { Cache["Secoes"] = value; }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        var usuario = UsuarioLogado();

        if (usuario == null)
            Response.Redirect("/Manager/Default.aspx");

        ltlUser.Text = usuario.Nome;

        ObterSecoes(usuario.IdPerfil);
        VerificaAcessoPagina();
    }

    #region Métodos

    #region Mensagens Modal

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
            mtvViews.ActiveViewIndex = 0;

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
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, IdUsuario);
        }
    }

    /// <summary>
    /// Exibe uma mensagem amigável ao usuário
    /// </summary>
    /// <param name="pstrMensagem">Mensagem</param>
    public void ExibirMensagem(string pstrMensagem)
    {
        int IdUsuario = 0;
        try
        {
            //Habilita a view a ser exibida
            mtvViews.ActiveViewIndex = 1;

            if (UserContext.Logado)
            {
                IdUsuario = UserContext.UsuarioLogado.Id;
            }
            //Adiciona o erro na label
            litMensagem.Text = pstrMensagem;

            //Abre a Div
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "javascript:openModal('M');", true);

        }
        catch (Exception ex)
        {
            //Adiciona o erro na label
            lblErro.Text = ex.ToString();
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, IdUsuario);
        }
    }
    #endregion

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
            //Adiciona o erro na label
            lblErro.Text = ex.ToString();
            DOLog.Inserir(string.Concat("Erro Sistema: ", ex), Utilitarios.TipoLog.Sistema, IdUsuario);
        }
    }

    /// <summary>
    /// Verifica se o usuáio possui permissão na página
    /// </summary>
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

    public bool VerificaPermissaoInclusao()
    {
        if(gobjSecoes == null)
        {
            Response.Redirect("/Manager/Modulos/Default.aspx");
        }

        //Caminho do request sem o nome da página
        string caminhoRequest = Request.Path.ToString().Substring(0, Request.Path.ToString().LastIndexOf('/')) + "/";

        bool blnPossuiPermissao = (from s in gobjSecoes where s.Caminho.ToLower().Contains(caminhoRequest.ToLower()) && s.PodeInserir select s).Any();

        return blnPossuiPermissao;
    }
    public bool VerificaPermissaoEdicao()
    {
        if (gobjSecoes == null)
        {
            Response.Redirect("/Manager/Modulos/Default.aspx");
        }

        //Caminho do request sem o nome da página
        string caminhoRequest = Request.Path.ToString().Substring(0, Request.Path.ToString().LastIndexOf('/')) + "/";

        bool blnPossuiPermissao = (from s in gobjSecoes where s.Caminho.ToLower().Contains(caminhoRequest.ToLower()) && s.PodeAlterar select s).Any();

        return blnPossuiPermissao;
    }
    public bool VerificaPermissaoExclusao()
    {
        if (gobjSecoes == null)
        {
            Response.Redirect("/Manager/Modulos/Default.aspx");
        }

        //Caminho do request sem o nome da página
        string caminhoRequest = Request.Path.ToString().Substring(0, Request.Path.ToString().LastIndexOf('/')) + "/";

        bool blnPossuiPermissao = (from s in gobjSecoes where s.Caminho.ToLower().Contains(caminhoRequest.ToLower()) && s.PodeExcluir select s).Any();

        return blnPossuiPermissao;
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
    /// Verifica se o usuário logado possui permissão para uma funcionalidade
    /// </summary>
    /// <param name="pintIdSecao"></param>
    /// <param name="pintIdFuncionalidade"></param>
    /// <returns></returns>
    public bool UsuarioPossuiPermissaoAcessoFuncionalidade(int pintIdSecao, int pintIdFuncionalidade)
    {
        Usuario usuarioLogado = UsuarioLogado();

        //return DOAcessoFuncionalidade.VerificaAcessoFuncionalidade(
        //                                     usuarioLogado.Id,
        //                                     pintIdSecao,
        //                                     pintIdFuncionalidade);
        return true;
    }

    #endregion
}
