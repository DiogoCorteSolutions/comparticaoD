using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Links_SalvarGrupo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
        }
    }

    #region Variáveis
    private int codigo;
    private GrupoLinks gobjGrupoLinks;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListarGrupos.aspx");
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.rfvGrupo.Text = Resources.Textos.Texto_Campo_Obrigatorio;

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
                codigo = 0;

                txtGrupo.Text = string.Empty;

                break;
            //Carregar Dados do Grupo
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjGrupoLinks == null)
                {
                    gobjGrupoLinks = new GrupoLinks();
                }

                gobjGrupoLinks.Descricao = txtGrupo.Text;

                break;

        }
    }

    private void Salvar()
    {

        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModLinks.InserirGrupo(gobjGrupoLinks);
            Response.Redirect("ListarGrupos.aspx?sucesso=1");

        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}