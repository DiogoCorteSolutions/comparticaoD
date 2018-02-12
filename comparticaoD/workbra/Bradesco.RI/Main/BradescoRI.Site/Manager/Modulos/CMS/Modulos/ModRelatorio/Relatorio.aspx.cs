using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModRelatorio_Relatorio : System.Web.UI.Page
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.IniciarTela();
            IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
        }
    }

    protected void btnModuloHome_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            foreach (ListItem item in chkTipoRelatorio.Items)
            {
                if (item.Selected)
                    count++;

                if (count > 4)
                {
                    MostrarMensagem(Resources.Textos.Mensagem_Maximo_Tipo_Relatorio);
                    chkTipoRelatorio.Focus();
                    return;
                }
            }

            List<ModComunicado> lComnicado = new List<ModComunicado>();

            lComnicado = (List<ModComunicado>)Session["sModComunicado"];
            
            if (lComnicado.Count> 4)
            {
                MostrarMensagem(Resources.Textos.Mensagem_Maximo_Comunicado);
                grvComunicado.Focus();
                return;
            }

            SalvarModuloHome();
        }
        catch (Exception ex)
        {
            MostrarMensagem(ex.Message);
        }
    }

    protected void grvComunicado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblTituloComunicado");
                lbl.Text = DoComunicado.Obter(new Comunicado() { ID = Convert.ToInt32(e.Row.Cells[0].Text.ToString()) }).Titulo;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlTipoComunicado_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoNoticia tipoArquivo = null;
        try
        {
            if (ddlTipoComunicado.SelectedIndex > 0)
            {
                tipoArquivo = new TipoNoticia() { ID = Convert.ToInt32(ddlTipoComunicado.SelectedValue) };
                CarregarComunicado(tipoArquivo);
                divComunicado.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAdicionarComunicado_Click(object sender, EventArgs e)
    {
        List<ModComunicado> lComunicado = null;
        ModComunicado modComunicado = null;
        try
        {
            lComunicado = (List<ModComunicado>)Session["sModComunicado"];

            modComunicado = new ModComunicado();
            modComunicado.ComunicadoId = Convert.ToInt32(ddlComunicado.SelectedValue);
            modComunicado.ConteudoId = IdConteudo;
            modComunicado.Data = System.DateTime.Now;

            lComunicado.Add(modComunicado);

            Session["sModComunicado"] = lComunicado;
            grvComunicado.DataSource = lComunicado;
            grvComunicado.DataBind();

            if (lComunicado.Count > 0)
                pnlGrid.Visible = true;
            else
                pnlGrid.Visible = false;


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void ddlModuloRelatorio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlModuloRelatorio.SelectedValue)
            {
                case "1":
                    pnlModuloHome.Visible = true;
                    pnlModuloInterna.Visible = false;
                    var lModRelatorios = DOModRelatorio.Listar(new ModRelatorio() { Conteudo = new ConteudoPagina() { ConteudoId = IdConteudo } });
                    PrencheDadosTipoRelatorio(lModRelatorios);
                    PreencheGridComunicados(new ModComunicado() { ConteudoId = IdConteudo });
                    break;

                case "2":
                    pnlModuloHome.Visible = false;
                    pnlModuloInterna.Visible = true;
                    break;
                default:
                    pnlModuloHome.Visible = false;
                    pnlModuloInterna.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            MostrarMensagem(ex.Message);
        }

    }

    private void PreencheGridComunicados(ModComunicado modComunicado)
    {
        try
        {
            List<ModComunicado> lModComunicado = new List<ModComunicado>();

            lModComunicado = DOModComunicado.Listar(modComunicado);
            Session["sModComunicado"] = lModComunicado;

            if (lModComunicado.Count > 0)
                pnlGrid.Visible = true;
            else
                pnlGrid.Visible = false;

            grvComunicado.DataSource = lModComunicado;
            grvComunicado.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Variaveis

    public int IdTipoRelatorio
    {
        get { return (int)(ViewState["IdTipoRelatorio"] ?? 1); }
        set { ViewState["IdTipoRelatorio"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    #endregion

    #region Métodos
    private void IniciarTela()
    {
        try
        {
            this.pnlModuloHome.Visible = false;
            this.pnlModuloInterna.Visible = false;
        }
        catch (Exception ex)
        {
            MostrarMensagem(ex.Message);
        }
    }

    private void CarregarTipoRelatorio()
    {
        try
        {
            chkTipoRelatorio.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Relatorio = true });   //DOTipoRelatorio.Listar();
            chkTipoRelatorio.DataTextField = "Descricao";
            chkTipoRelatorio.DataValueField = "Id";
            chkTipoRelatorio.DataBind();
            chkTipoRelatorio.ClearSelection();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<ModRelatorio> CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {

        List<ModRelatorio> lRelatorios = new List<ModRelatorio>();

        switch (objTipoTransacao)
        {
            case Utilitarios.TipoTransacao.Limpar:
                CarregarTipoRelatorio();
                CarregarTipoComunicado();
                Session.Add("sModComunicado", null);
                break;
            default:
                break;
        }

        return lRelatorios;
    }

    private void CarregarTipoComunicado()
    {
        try
        {
            ddlTipoComunicado.ClearSelection();
            ddlTipoComunicado.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Comunicado = true }); // DoTipoComunicado.Listar();
            ddlTipoComunicado.DataTextField = "Descricao";
            ddlTipoComunicado.DataValueField = "Id";
            ddlTipoComunicado.DataBind();

            ddlTipoComunicado.Items.Insert(0, "Selecione o tipo de Comunicado");
            ddlTipoComunicado.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarComunicado(TipoNoticia objTipoComunicado)
    {
        Comunicado comunicado = null;
        try
        {
            ddlComunicado.ClearSelection();
            comunicado = new Comunicado() { TipoComunicado = objTipoComunicado };
            ddlComunicado.DataSource = DoComunicado.Listar(comunicado);
            ddlComunicado.DataTextField = "Titulo";
            ddlComunicado.DataValueField = "Id";
            ddlComunicado.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PrencheDadosTipoRelatorio(List<ModRelatorio> lRelatorios)
    {
        try
        {
            foreach (ListItem item in chkTipoRelatorio.Items)
            {
                foreach (var rel in lRelatorios)
                    if (rel.TipoRelatorio.ID.ToString() == item.Value.ToString())
                        item.Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MostrarMensagem(string pMensagem)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + pMensagem + "');", true);
    }

    private void SalvarModuloHome()
    {
        string strMensagem = string.Empty;
        bool bFechar = false;
        try
        {

            if (SalvarRelatorioHome() && SalvarComunicadoHome())
            {
                strMensagem = Resources.Textos.Mensagem_Salva_Sucesso;
                bFechar = true;
            }
            else
            {
                strMensagem = Resources.Textos.Mensagem_Salva_Erro;
            }
            MostrarMensagem(strMensagem);

            if (bFechar)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
                ScriptManager.RegisterStartupScript(this, typeof(string), "CLOSE_WINDOW", "this.close();", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SalvarComunicadoHome()
    {
        List<ModComunicado> lComunicado = null;
        try
        {
            lComunicado = new List<ModComunicado>();
            lComunicado = (List<ModComunicado>)Session["sModComunicado"];

            DOModComunicado.ExcluirComunicados(lComunicado[0]);

            if (lComunicado.Count > 0)
            {
                foreach (ModComunicado mComunicado in lComunicado)
                    DOModComunicado.Inserir(mComunicado);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool SalvarRelatorioHome()
    {
        List<ModRelatorio> lRelatorio = null;
        ConteudoPagina objConteudo = null;

        try
        {
            lRelatorio = new List<ModRelatorio>();
            objConteudo = new ConteudoPagina() { ConteudoId = IdConteudo };

            DOModRelatorio.Apagar(new ModRelatorio() { Conteudo = objConteudo });

            foreach (ListItem item in chkTipoRelatorio.Items)
            {
                if (item.Selected)
                {
                    ModRelatorio mRel = new ModRelatorio();
                    mRel.Conteudo = objConteudo;
                    mRel.TipoRelatorio = new TipoRelatorio() { ID = Convert.ToInt32(item.Value) };
                    mRel.Data = System.DateTime.Now;
                    DOModRelatorio.Inserir(mRel);

                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    protected void grvComunicado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<ModComunicado> lModComunicado = null;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow linha = grvComunicado.Rows[index];
            ModComunicado comunicado = new ModComunicado() { Id = Convert.ToInt32(linha.Cells[0].Text) };

            lModComunicado = new List<ModComunicado>();
            lModComunicado = (List<ModComunicado>)Session["sModComunicado"];

            switch (e.CommandName.ToString())
            {
                case "RemoverComunicado":

                    lModComunicado.RemoveAt(index);

                    grvComunicado.DataSource = lModComunicado;
                    grvComunicado.DataBind();

                    break;
            }

            Session["sModComunicado"] = lModComunicado;
        }
        catch (Exception ex)
        {

            throw;
        }
    }


}