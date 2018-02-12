using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModNoticia_Noticia : System.Web.UI.Page
{

    #region Variáveis
    public int IdNoticia
    {
        get { return (int)(ViewState["IdNoticia"] ?? 1); }
        set { ViewState["IdNoticia"] = value; }
    }
    public int IdConteudo
    {
        get { return (int)(ViewState["IdConteudo"] ?? 0); }
        set { ViewState["IdConteudo"] = value; }
    }
    public ModNoticia gobjModNoticiaSimuladores
    {
        get { return (ModNoticia)(ViewState["ModNoticia"] ?? null); }
        set { ViewState["ModNoticia"] = value; }
    }

    public ModNoticia gobjModNoticiaSustentabilidade
    {
        get { return (ModNoticia)(ViewState["ModNoticia"] ?? null); }
        set { ViewState["ModNoticia"] = value; }
    }

    public ModNoticia gobjModNoticiaPodCats
    {
        get { return (ModNoticia)(ViewState["ModNoticia"] ?? null); }
        set { ViewState["ModNoticia"] = value; }
    }

    public ModNoticia gobjModNoticiaInfCad
    {
        get { return (ModNoticia)(ViewState["ModNoticia"] ?? null); }
        set { ViewState["ModNoticia"] = value; }
    }
    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                this.IniciaTela();

                if (Request.QueryString["conteudoId"] != null)
                {
                    CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
                }
            }
            catch (Exception ex)
            {
                MostrarMensagem(ex.Message);
            }

        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarNoticia(2);
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }



    protected void ddlArquivos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void btnSalvarListagem_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarNoticia(1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void btnVincularModuloNoticia_Click(object sender, ImageClickEventArgs e)
    //{

    //    try
    //    {
    //        if (ddlTipoNoticia.SelectedIndex > 0 && ddlNoticia.SelectedIndex > 0)
    //        {
    //            List<Noticia> lNoticia = new List<Noticia>();
    //            lNoticia = (List<Noticia>)Session["sNoticia"];

    //            var noticia = DONoticia.Obter(new Noticia() { ID = Convert.ToInt32(ddlNoticia.SelectedValue), TipoNoticia = new TipoNoticia() { ID = Convert.ToInt32(ddlTipoNoticia.SelectedValue) } });
    //            lNoticia.Add(noticia);

    //            grvNoticias.DataSource = lNoticia;
    //            grvNoticias.DataBind();

    //            Session["sNoticia"] = lNoticia;

    //        }
    //        else
    //            MostrarMensagem("Selecione o tipo de noticia e a noticia");

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void btnSalvarDestaque_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarNoticia(3);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTipoModuloNoticia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlNoticiaHome.Visible = false;
            pnlNoticiaListagem.Visible = false;

            switch (ddlTipoModuloNoticia.SelectedIndex)
            {
                case 1:
                    CarregaTipoNoticia(ddlE1);
                    CarregaTipoNoticia(ddlD1);
                    CarregaTipoNoticia(ddlD2);
                    CarregaTipoNoticia(ddlD3);
                    pnlNoticiaHome.Visible = true;
                    pnlNoticiaDestaque.Visible = false;
                    pnlNoticiaListagem.Visible = false;
                    break;

                case 2:
                    pnlNoticiaHome.Visible = false;
                    pnlNoticiaListagem.Visible = true;
                    pnlNoticiaDestaque.Visible = false;
                    break;

                case 3:
                    CarregaTipoNoticia(ddlE1Destaque);
                    CarregaTipoNoticia(ddlD1Destaque);
                    CarregaTipoNoticia(ddlD2Destaque);
                    pnlNoticiaDestaque.Visible = true;
                    pnlNoticiaListagem.Visible = false;
                    pnlNoticiaHome.Visible = false;
                    break;
            }

            CarregarObjetos(Utilitarios.TipoTransacao.Carregar);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTipoNoticia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTipoNoticia.SelectedIndex > 0)
            {
                //CarregaNoticia(Convert.ToInt32(ddlTipoNoticia.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Métodos
    private void IniciaTela()
    {
        pnlNoticiaHome.Visible = false;
        pnlNoticiaListagem.Visible = false;
        pnlNoticiaDestaque.Visible = false;
        Session.Add("sNoticia", new List<Noticia>());
    }


    private void CarregaTipoNoticia(DropDownList pDDL)
    {
        Noticia noticia = null;
        try
        {
            noticia = new Noticia();
            noticia.Destaque = true;
            noticia.TipoArquivo = new TipoArquivo() { Noticia = true };
            pDDL.DataSource = DOTipoArquivo.Listar(noticia.TipoArquivo);
            pDDL.DataTextField = "Descricao";
            pDDL.DataValueField = "Id";
            pDDL.DataBind();
            pDDL.Items.Insert(0, new ListItem(Resources.Textos.Texto_Selecione, "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<ModNoticia> CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        int IdConteudo = Convert.ToInt32(Request.QueryString["conteudoId"]);
        List<ModNoticia> lModNoticias = new List<ModNoticia>();

        ModNoticia objESquerda = new ModNoticia();
        ModNoticia objDireitoSuperior = new ModNoticia();
        ModNoticia objDireitoCentral = new ModNoticia();
        ModNoticia objDireitoInferior = new ModNoticia();


        switch (objTipoTransacao)
        {
            #region Salvar
            case Utilitarios.TipoTransacao.Salvar:
                switch (ddlTipoModuloNoticia.SelectedValue)
                {
                    #region Home
                    case "1":
                        objESquerda.ID = IdConteudo;
                        objESquerda.TipoArquivoId = Convert.ToInt32(ddlE1.SelectedValue);
                        //objESquerda.IdNoticia = Convert.ToInt32(ddlE1.SelectedValue);
                        objESquerda.TipoNoticiaId = (int)Utilitarios.TipoNoticia.Esquerdo;
                        lModNoticias.Add(objESquerda);

                        objDireitoSuperior.ID = IdConteudo;
                        objDireitoSuperior.TipoArquivoId = Convert.ToInt32(ddlD1.SelectedValue);
                        //objDireitoSuperior.IdNoticia = Convert.ToInt32(ddlD1.SelectedValue);
                        objDireitoSuperior.TipoNoticiaId = (int)Utilitarios.TipoNoticia.DireitoSuperior;
                        lModNoticias.Add(objDireitoSuperior);

                        objDireitoCentral.ID = IdConteudo;
                        objDireitoCentral.TipoArquivoId = Convert.ToInt32(ddlD2.SelectedValue);
                        //objDireitoCentral.IdNoticia = Convert.ToInt32(ddlD2.SelectedValue);
                        objDireitoCentral.TipoNoticiaId = (int)Utilitarios.TipoNoticia.DireitoCentral;
                        lModNoticias.Add(objDireitoCentral);

                        objDireitoInferior.ID = IdConteudo;
                        objDireitoInferior.TipoArquivoId = Convert.ToInt32(ddlD3.SelectedValue);
                        //objDireitoInferior.IdNoticia = Convert.ToInt32(ddlD3.SelectedValue);
                        objDireitoInferior.TipoNoticiaId = (int)Utilitarios.TipoNoticia.DireitoInferior;
                        lModNoticias.Add(objDireitoInferior);
                        break;
                    #endregion

                    #region Listagem
                    case "2":
                        lModNoticias = new List<ModNoticia>();

                        //foreach (GridViewRow row in grvNoticias.Rows)
                        //{
                        ModNoticia objModNoticia = new ModNoticia() { TipoArquivoId = Convert.ToInt32(ddlTipoNoticia.SelectedValue) };
                        objModNoticia.ID = IdConteudo;
                        objModNoticia.Home = false;
                        objModNoticia.Data = System.DateTime.Now;

                        lModNoticias.Add(objModNoticia);

                        //}

                        break;
                    #endregion

                    case "3":
                        objESquerda.ID = IdConteudo;
                        objESquerda.TipoArquivoId = Convert.ToInt32(ddlE1Destaque.SelectedValue);
                        //objESquerda.IdNoticia = Convert.ToInt32(ddlE1Destaque.SelectedValue);
                        objESquerda.TipoNoticiaId = (int)Utilitarios.TipoNoticia.Esquerdo;
                        lModNoticias.Add(objESquerda);

                        objDireitoSuperior.ID = IdConteudo;
                        objDireitoSuperior.TipoArquivoId = Convert.ToInt32(ddlD1Destaque.SelectedValue);
                        //objDireitoSuperior.IdNoticia = Convert.ToInt32(ddlD1Destaque.SelectedValue);
                        objDireitoSuperior.TipoNoticiaId = (int)Utilitarios.TipoNoticia.DireitoSuperior;
                        lModNoticias.Add(objDireitoSuperior);

                        objDireitoInferior.ID = IdConteudo;
                        objDireitoInferior.TipoArquivoId = Convert.ToInt32(ddlD2Destaque.SelectedValue);
                        //objDireitoInferior.IdNoticia = Convert.ToInt32(ddlD2Destaque.SelectedValue);
                        objDireitoInferior.TipoNoticiaId = (int)Utilitarios.TipoNoticia.DireitoInferior;
                        lModNoticias.Add(objDireitoInferior);
                        break;
                }

                break;
            #endregion

            #region Carregar
            case Utilitarios.TipoTransacao.Carregar:
                switch (ddlTipoModuloNoticia.SelectedValue)
                {
                    case "1":
                        CarregarObjetosHome();
                        break;

                    case "2":
                        CarregarObjetosListagem();
                        break;

                    case "3":
                        CarregarDadosDestaque();
                        break;
                }
                break;
                #endregion
        }
        return lModNoticias;
    }

    private void CarregarDadosDestaque()
    {
        try
        {
            ddlE1Destaque.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlE1Destaque.SelectedValue.ToString())).ID.ToString();
            ddlD1Destaque.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlD1Destaque.SelectedValue.ToString())).ID.ToString();
            ddlD2Destaque.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlD2Destaque.SelectedValue.ToString())).ID.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetosListagem()
    {
        try
        {
            CarregarTipoNoticia();
            List<ModNoticia> objListModNoticia = new List<ModNoticia>();

            objListModNoticia = DOModNoticia.ListarNoticiasModulos(IdConteudo, null);

            List<Noticia> lstNoticia = new List<Noticia>();

            foreach (ModNoticia modNoticia in objListModNoticia)
                lstNoticia.Add(DONoticia.Obter(new Noticia() { ID = modNoticia.IdNoticia }));

            //grvNoticias.DataSource = lstNoticia;
            //grvNoticias.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarObjetosHome()
    {
        try
        {
            ddlE1.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlE1.SelectedValue.ToString())).ID.ToString();
            ddlD1.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlD1.SelectedValue.ToString())).ID.ToString();
            ddlD2.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlD2.SelectedValue.ToString())).ID.ToString();
            ddlD3.SelectedValue = DOModNoticia.ObterNoticiaModulo(IdConteudo, Convert.ToInt32(ddlD3.SelectedValue.ToString())).ID.ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CarregarTipoNoticia()
    {
        try
        {
            ddlTipoNoticia.DataSource = DOTipoArquivo.Listar(new TipoArquivo() { Noticia = true });
            ddlTipoNoticia.DataValueField = "Id";
            ddlTipoNoticia.DataTextField = "Descricao";
            ddlTipoNoticia.DataBind();

            ddlTipoNoticia.Items.Insert(0, new ListItem("Selecione o tipo de notícia", "-1"));
            ddlTipoNoticia.SelectedIndex = 0;


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

    //private void CarregaNoticia(int pTipoNoticiaId)
    //{
    //    try
    //    {
    //        ddlNoticia.DataSource = DONoticia.Listar(new Noticia() { TipoNoticia = new TipoNoticia() { ID = pTipoNoticiaId } });
    //        ddlNoticia.DataTextField = "Titulo";
    //        ddlNoticia.DataValueField = "Id";
    //        ddlNoticia.DataBind();

    //        ddlNoticia.Items.Insert(0, new ListItem("Selecione a notícia", "-1"));
    //        ddlNoticia.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void SalvarNoticia(int iNoticia)
    {
        List<ModNoticia> lNoticia = null;
        bool retorno = false;
        try
        {
            lNoticia = new List<ModNoticia>();

            lNoticia = CarregarObjetos(Utilitarios.TipoTransacao.Salvar);


            if (lNoticia.Count > 0)
            {
                DOModNoticia.Excluir(lNoticia[0].ID);
                foreach (var item in lNoticia)
                {
                    switch (iNoticia)
                    {
                        case 1:
                            item.Listagem = true;
                            break;

                        case 2:
                            item.Home = true;
                            break;

                        case 3:
                            item.Destaque = true;
                            break;
                    }

                    if (DOModNoticia.Inserir(item) > 0)
                        retorno = true;
                }
            }

            if (retorno)
            {
                MostrarMensagem(Resources.Noticias.Mensagem_Inserir_Modulo_Noticia_Sucesso);
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "refreshParent();", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion
}