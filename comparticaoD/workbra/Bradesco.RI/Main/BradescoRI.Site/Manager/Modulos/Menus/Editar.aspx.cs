using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Menus_Editar : System.Web.UI.Page
{
    #region Variáveis
    private string Hierarquia
    {
        get
        {
            return ViewState["hierarquiaMenu"].ToString();
        }
        set
        {
            ViewState["hierarquiaMenu"] = value;
        }
    }
    private Menu gobjMenu;
    private List<Menu> gobjListMenus;
    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);

            if (Request.QueryString["Hierarquia"] != null)
            {
                Hierarquia = Request.QueryString["Hierarquia"];
            }

            CarregarHierarquia();
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
    private void IniciaTela()
    {
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winroot, "_top"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winparent, "_parent"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winblank, "_blank"));
        this.ddlTarget.Items.Insert(0, new ListItem(Resources.Menu.winself, "_self"));

        this.ddlIdioma.DataSource = DOIdioma.Listar();
        this.ddlIdioma.DataValueField = "Id";
        this.ddlIdioma.DataTextField = "Nome";
        this.ddlIdioma.DataBind();


        this.ddlPaginas.DataSource = DOPagina.Listar(0, 2);
        this.ddlPaginas.DataValueField = "Caminho";
        this.ddlPaginas.DataTextField = "Titulo";
        this.ddlPaginas.DataBind();
        this.ddlPaginas.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));

        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvUrl.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Usuario
            case Utilitarios.TipoTransacao.Limpar:
                Hierarquia = string.Empty;

                txtNome.Text = string.Empty;
                txtUrl.Text = string.Empty;
                ddlTarget.SelectedIndex = 0;
                ddlIdioma.SelectedIndex = 0;
                txtChave.Text = string.Empty;
                txtEstilo.Text = string.Empty;

                break;
            //Carregar Dados do Usuario
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjMenu == null)
                {
                    gobjMenu = new Menu();
                }
                gobjMenu.Hierarquia = hdnHierarquia.Value;
                gobjMenu.Nome = txtNome.Text;
                gobjMenu.Url = txtUrl.Text;
                gobjMenu.Target = ddlTarget.SelectedValue;
                gobjMenu.IdiomaId = Convert.ToInt32(ddlIdioma.SelectedValue);
                gobjMenu.ChaveNome = txtChave.Text;
                gobjMenu.CssClass = txtEstilo.Text;

                break;
            //Descarregar Dados do Usuario
            case Utilitarios.TipoTransacao.Carregar:
                if (gobjMenu != null)
                {
                    hdnHierarquia.Value = gobjMenu.Hierarquia;

                    if (!String.IsNullOrWhiteSpace(gobjMenu.Nome))
                        txtNome.Text = gobjMenu.Nome.ToString();
                    if (!String.IsNullOrWhiteSpace(gobjMenu.Url))
                        txtUrl.Text = gobjMenu.Url.ToString();
                    if (!String.IsNullOrWhiteSpace(gobjMenu.Target))
                        ddlTarget.SelectedValue = gobjMenu.Target.ToString();
                    if (gobjMenu.IdiomaId > 0)
                        ddlIdioma.SelectedValue = gobjMenu.IdiomaId.ToString();
                    if (!String.IsNullOrWhiteSpace(gobjMenu.ChaveNome))
                        txtChave.Text = gobjMenu.ChaveNome.ToString();
                    if (!String.IsNullOrWhiteSpace(gobjMenu.CssClass))
                        txtEstilo.Text = gobjMenu.CssClass.ToString();

                }

                break;
        }
    }

    private void Salvar()
    {
        try
        {
            CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            if (hdnAcao.Value == "Editar")
                DOMenu.Atualizar(gobjMenu);
            else
                Hierarquia = DOMenu.InserirItemFilho(gobjMenu).Substring(0, 3);

            Response.Redirect("Editar.aspx?Hierarquia=" + Hierarquia);
        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }

    }

    public void CarregarHierarquia()
    {
        if (!string.IsNullOrWhiteSpace(Hierarquia))
        {
            gobjListMenus = DOMenu.Listar(Hierarquia);
            trvMenus.Nodes.Clear();

            List<Menu> objRetorno = new List<Menu>();

            if (gobjListMenus.Any())
            {
                string hierarquiaMax = (from h in gobjListMenus orderby h.Hierarquia.Length descending select h.Hierarquia).First();

                IEnumerable<string> items = Enumerable.Range(0, hierarquiaMax.Length / 3).Select(i => hierarquiaMax.Substring(i * 3, 3));
                int limiteMax = Enumerable.Range(0, hierarquiaMax.Length / 3).Select(i => hierarquiaMax.Substring(i * 3, 3)).Count();

                for (int i = 1; i <= limiteMax; i++)
                {
                    List<Menu> itensMenu = (from m in gobjListMenus where m.Hierarquia.Length == (i * 3) select m).ToList();

                    foreach (Menu item in itensMenu)
                    {
                        string hierarquiaItem = item.Hierarquia;

                        if (item.Hierarquia.Length > 3)
                            hierarquiaItem = item.Hierarquia.Substring(0, item.Hierarquia.Length - 3);
                        AdicionarItemsTreeview(hierarquiaItem, item);
                    }
                }
                trvMenus.ExpandAll();
            }
        }
    }

    [WebMethod]
    [ScriptMethod]
    public static Menu SelecionarItem(string pstrHierarquia)
    {
        return DOMenu.Obter(pstrHierarquia);
    }

    [WebMethod]
    [ScriptMethod]
    public static void DeletarItemMenu(string pstrHierarquia)
    {
        DOMenu.Excluir(pstrHierarquia);
    }

    #region TreeView
    private void AdicionarItemsTreeview(string pstrHierarquia, Menu itemMenu)
    {

        TreeNode node = EncontrarItemTreeview(pstrHierarquia);

        if (node == null)
        {
            TreeNode novo = new TreeNode();
            novo.Text = String.Format("<label class='btnSelectMenu' onclick=\"selecionarNode(\'{0}\',\'{1}\')\">{1}</label>", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Text += "<input type='button' style='display:none;' value='+' />";
            novo.Text += String.Format("&nbsp;<input type='button' class='btnAddMenu' value='+' onclick=\"addNodeAbaixo(\'{0}\',\'{1}\')\" />", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Text += String.Format("&nbsp;<input type='button' class='btnDeleteMenu' value='x' onclick=\"excluirNode(\'{0}\',\'{1}\')\" />", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Value = itemMenu.Hierarquia;
            novo.SelectAction = TreeNodeSelectAction.None;
            trvMenus.Nodes.Add(novo);
        }
        else
        {
            TreeNode novo = new TreeNode();
            novo.Text = String.Format("<label class='btnSelectMenu' onclick=\"selecionarNode(\'{0}\',\'{1}\')\">{1}</label>", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Text += "<input type='button' style='display:none;' value='+' />";
            novo.Text += String.Format("&nbsp;<input type='button' class='btnAddMenu' value='+' onclick=\"addNodeAbaixo(\'{0}\',\'{1}\')\" />", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Text += String.Format("&nbsp;<input type='button' class='btnDeleteMenu' value='x' onclick=\"excluirNode(\'{0}\',\'{1}\')\" />", itemMenu.Hierarquia, itemMenu.Nome);
            novo.Value = itemMenu.Hierarquia;
            novo.SelectAction = TreeNodeSelectAction.None;
            node.ChildNodes.Add(novo);
        }
    }
    private TreeNode EncontrarItemTreeview(string searchstring)
    {
        try
        {
            for (int vLoop = 0; vLoop < trvMenus.Nodes.Count; vLoop++)
            {
                TreeNode trNode = trvMenus.Nodes[vLoop];
                if (trNode.Value == searchstring)
                    return trNode;
                else
                {
                    TreeNode trAnswerNode = EncontrarItemFilhoTreeView(trNode, searchstring);
                    if (trAnswerNode != null)
                        return trAnswerNode;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private TreeNode EncontrarItemFilhoTreeView(TreeNode trNode, string searchstring)
    {
        try
        {
            for (int vLoop = 0; vLoop < trNode.ChildNodes.Count; vLoop++)
            {
                TreeNode trChildNode = trNode.ChildNodes[vLoop];
                if (trChildNode.Value == searchstring)
                    return trChildNode;
                else
                {
                    TreeNode trAnswerNode = EncontrarItemFilhoTreeView(trChildNode, searchstring);
                    if (trAnswerNode != null)
                        return trAnswerNode;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #endregion
}