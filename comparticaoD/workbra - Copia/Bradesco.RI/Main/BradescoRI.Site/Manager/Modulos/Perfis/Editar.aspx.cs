using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Perfis_Editar : System.Web.UI.Page
{
    #region Variáveis
    private int codigo;
    private Perfil gobjPerfil;
    private List<string> gobjListGrupos;
    private List<Secao> gobjListSecoes;
    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Id"] != null)
            {
                codigo = Convert.ToInt32(Request.QueryString["Id"]);
            }

            gobjPerfil = DOPerfil.Obter(codigo);
            gobjListSecoes = DOSecao.Listar(codigo);
            gobjListGrupos = (from s in gobjListSecoes select s.Grupo).Distinct().ToList();

            //Permissão de edição
            if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
                Response.Redirect("/Manager/Modulos/Default.aspx");

            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
        }
    }
    protected void rptGrupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptPermissao = (Repeater)e.Item.FindControl("rptPermissao");

            String grupo = e.Item.DataItem.ToString();
            rptPermissao.DataSource = (from f in gobjListSecoes where f.Grupo == grupo select f).ToList();
            rptPermissao.DataBind();
        }
    }

    protected void rptPermissao_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Secao item = (Secao)e.Item.DataItem;
            CheckBox chkInserir = (CheckBox)e.Item.FindControl("chkInserir");
            CheckBox chkEditar = (CheckBox)e.Item.FindControl("chkEditar");
            CheckBox chkExcluir = (CheckBox)e.Item.FindControl("chkExcluir");

            if (item.Caminho.ToLower().Contains("/logs"))
            {
                chkInserir.Checked = chkEditar.Checked = chkExcluir.Checked = true;
                chkInserir.Visible = chkEditar.Visible = chkExcluir.Visible = false;
            }
            

        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listar.aspx");
    }

    #endregion

    #region Métodos

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                codigo = 0;

                txtNome.Text = string.Empty;


                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjPerfil == null)
                {
                    gobjPerfil = new Perfil();
                }
                gobjPerfil.Id = codigo;
                gobjPerfil.Nome = txtNome.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                if (!String.IsNullOrWhiteSpace(gobjPerfil.Nome))
                    txtNome.Text = gobjPerfil.Nome.ToString();

                rptGrupos.DataSource = gobjListGrupos;
                rptGrupos.DataBind();

                break;
        }
    }

    private void Salvar()
    {
        try
        {
            codigo = Convert.ToInt32(Request.QueryString["Id"]);
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            //Obter os acessos selecionados
            List<Secao> lstSecoes = new List<Secao>();

            foreach (RepeaterItem item in rptGrupos.Items)
            {
                Repeater rptPermissao = (Repeater)item.FindControl("rptPermissao");

                foreach (RepeaterItem itemAcesso in rptPermissao.Items)
                {
                    HiddenField hdnID = (HiddenField)itemAcesso.FindControl("hdnID");
                    CheckBox chkControleTotal = (CheckBox)itemAcesso.FindControl("chkControleTotal");
                    CheckBox chkAcessar = (CheckBox)itemAcesso.FindControl("chkAcessar");
                    CheckBox chkInserir = (CheckBox)itemAcesso.FindControl("chkInserir");
                    CheckBox chkEditar = (CheckBox)itemAcesso.FindControl("chkEditar");
                    CheckBox chkExcluir = (CheckBox)itemAcesso.FindControl("chkExcluir");

                    Secao objSecao = new Secao();

                    objSecao.Id = Convert.ToInt32(hdnID.Value);
                    objSecao.PossuiControleTotal = chkControleTotal.Checked;
                    objSecao.PodeAcessar = chkAcessar.Checked;
                    objSecao.PodeInserir = chkInserir.Checked;
                    objSecao.PodeAlterar = chkEditar.Checked;
                    objSecao.PodeExcluir = chkExcluir.Checked;

                    lstSecoes.Add(objSecao);
                }
            }

            if (codigo > 0)
            {
                DOSecao.ExcluirAcessosPerfil(codigo);
                DOPerfil.Atualizar(gobjPerfil);
            }
            else {
                codigo = DOPerfil.Inserir(gobjPerfil);
            }

            foreach (Secao objSecao in lstSecoes)
            {
                objSecao.IdPerfil = codigo;
                DOSecao.InserirAcessoPerfil(objSecao);
            }


            Response.Redirect("Listar.aspx?sucesso=1");
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion
}