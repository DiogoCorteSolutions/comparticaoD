using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModNoticia_Noticia : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    public int CurrentPage
    {
        get
        {
            object obj = this.ViewState["_CurrentPage"]; if (obj == null) { return 0; }
            else
            {
                return (int)obj;
            }
        }

        set
        {
            //set in viewstate the current page number
            this.ViewState["_CurrentPage"] = value;
        }
    }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<ModNoticia> lModNoticia = DOModNoticia.ListarNoticiasModulos(IdConteudo, null);

        List<Noticia> lNoticia = new List<Noticia>();

        foreach (ModNoticia item in lModNoticia)
        {
            Noticia noticia = new Noticia();
            noticia.ID = item.IdNoticia;
            noticia = DONoticia.Obter(noticia);
            lNoticia.Add(noticia);
        }

        List<int> lstAno = new List<int>();

        foreach (var item in lNoticia)
        {
            if (!lstAno.Contains(item.DataNoticia.Year))
                lstAno.Add(item.DataNoticia.Year);
        }

        ddlAno.DataSource = lstAno;
        ddlAno.DataBind();


        Session.Add("sNoticias", lNoticia);

        pnlNoticiaHome.Visible = false;
        pnlNoticiaInterna.Visible = false;
        pnlNoticiaDestaque.Visible = false;

        if (lModNoticia.Count > 0)
        {
            if (lModNoticia[0].Home)
            {
                pnlNoticiaHome.Visible = true;
                CarregarDadosHome(lModNoticia);
            }
            else if (lModNoticia[0].Listagem)
            {
                pnlNoticiaInterna.Visible = true;
                BindNoticias();
            }
            else if (lModNoticia[0].Destaque)
            {
                pnlNoticiaDestaque.Visible = true;
                CarregarDadosDestaque(lModNoticia);
            }

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;

        }
        else
        {
            divSemConteudo.Visible = true;
            pnlNoticiaHome.Visible = false;
            pnlNoticiaInterna.Visible = false;
            pnlNoticiaDestaque.Visible = false;
            divConteudo.Visible = false;
        }
    }

    private void CarregarDadosDestaque(List<ModNoticia> objListModNoticia)
    {
        try
        {
            if (objListModNoticia.Count > 0)
            {
                foreach (ModNoticia mNoticia in objListModNoticia)
                {
                    Noticia noticia = DOModNoticia.Obter(new Noticia() { ID = mNoticia.IdNoticia });
                    if (noticia.ID > 0)
                    {
                        switch (mNoticia.TipoNoticiaId)
                        {
                            case ((int)(Utilitarios.TipoNoticia.Esquerdo)):
                                lblTituloEsquerdaDestaque.Text = noticia.Titulo;
                                lblResumoEsquerdaDestaque.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoSuperior)):
                                lblSubTipoDireitaSuperiorDestaque.Text = noticia.Titulo;
                                lblTituloDireitaSuperiorDestaque.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoInferior)):
                                lblSubTipoDireitaInferiorDestaque.Text = noticia.Titulo;
                                lblTituloDireitaInferiorDestaque.Text = noticia.Resumo;
                                break;
                        }
                    }
                }
                divSemConteudo.Visible = false;
                divConteudo.Visible = true;

            }
            else
            {
                divSemConteudo.Visible = true;
                divConteudo.Visible = false;
                pnlNoticiaHome.Visible = false;
                pnlNoticiaInterna.Visible = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregarDadosHome(List<ModNoticia> objListModNoticia)
    {
        try
        {
            if (objListModNoticia.Count > 0)
            {
                foreach (ModNoticia mNoticia in objListModNoticia)
                {
                    Noticia noticia = DOModNoticia.Obter(new Noticia() { ID = mNoticia.IdNoticia });
                    if (noticia.ID > 0)
                    {
                        switch (noticia.TipoNoticia.ID)
                        {
                            case ((int)(Utilitarios.TipoNoticia.Esquerdo)):
                                lblTituloEsquerdo.Text = noticia.Titulo;
                                lblResumoEsquerdo.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoSuperior)):
                                lblTituloDireitoSuperior.Text = noticia.Titulo;
                                lblResumoDireitoSuperior.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoCentral)):
                                lblTituloDireitoCentral.Text = noticia.Titulo;
                                lblResumoDireitoCentral.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoInferior)):
                                lblTituloDireitoInferior.Text = noticia.Titulo;
                                lblResumoDireitoInferior.Text = noticia.Resumo;
                                break;
                        }
                    }
                }
                divSemConteudo.Visible = false;
                divConteudo.Visible = true;

            }
            else
            {
                divSemConteudo.Visible = true;
                divConteudo.Visible = false;
                pnlNoticiaHome.Visible = false;
                pnlNoticiaInterna.Visible = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregaArquivosNoticia(Noticia noticia)
    {
        List<ArquivoNoticia> arquivos = DOArquivoNoticia.ListaArquivosNoticia(noticia);

        foreach (var arquivo in arquivos)
        {
            var tipoArquivo = string.Empty;

            if (arquivo.Capa)
                tipoArquivo = "Capa";

            if (arquivo.Detalhe)
                tipoArquivo = "Detalhe";

            if (arquivo.Lista)
                tipoArquivo = "Lista";

            System.Text.StringBuilder sbArquivo = new System.Text.StringBuilder();
            sbArquivo.Append("<table>");
            sbArquivo.Append("   <tr>");
            sbArquivo.Append("       <td>");
            sbArquivo.Append(tipoArquivo);
            sbArquivo.Append("       </td>");
            sbArquivo.Append("       <td>");
            sbArquivo.Append("          <div>");
            sbArquivo.Append("              <img src='" + arquivo.PathArquivo + "'>");
            sbArquivo.Append("          </div><br>");
            sbArquivo.Append("       </td>");
            sbArquivo.Append("   </tr>");
            sbArquivo.Append("<table>");

            litArquivos.Text += sbArquivo.ToString();
        }
    }



    #endregion


    //protected void rptNoticia_ItemCreated(object sender, RepeaterItemEventArgs e)
    //{
    //    RepeaterItem ri = (RepeaterItem)e.Item;
    //    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Noticia noticia = (Noticia)ri.DataItem;
    //        Label lblDataCadastro = ri.FindControl("lblDataCadastro") as Label;
    //        lblDataCadastro.Text = noticia.DataNoticia.ToShortDateString();

    //        Label lblTitulo = ri.FindControl("lblTitulo") as Label;
    //        lblTitulo.Text = noticia.Titulo;

    //        Label lblResumo = ri.FindControl("lblResumo") as Label;
    //        lblResumo.Text = noticia.Resumo;
    //    }
    //}

    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        BindNoticias();
    }

    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        BindNoticias();
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        BindNoticias();
    }

    protected void lbtnLast_Click(object sender, EventArgs e)
    {

        CurrentPage = BindNoticias() - 1;
        BindNoticias();

    }

    private int BindNoticias()
    {
        int retorno = 0;
        try
        {

            List<Noticia> lst = new List<Noticia>();
            lst = (List<Noticia>)Session["sNoticias"];


            lst = FiltrosNoticia(lst);

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = lst;

            pds.AllowPaging = true;
            pds.PageSize = 3;

            int count = pds.PageCount;
            pds.CurrentPageIndex = CurrentPage;

            if (pds.Count > 0)
            {
                lbtnPrev.Visible = true;
                lbtnNext.Visible = true;
                lbtnFirst.Visible = true;
                lbtnLast.Visible = true;

                lblStatus.Text = "| Página " + Convert.ToString(CurrentPage + 1) + " de " + Convert.ToString(pds.PageCount);

            }

            else
            {
                lbtnPrev.Visible = false;
                lbtnNext.Visible = false;
                lbtnFirst.Visible = false;
                lbtnLast.Visible = false;
            }

            lbtnPrev.Enabled = !pds.IsFirstPage;
            lbtnNext.Enabled = !pds.IsLastPage;
            lbtnFirst.Enabled = !pds.IsFirstPage;
            lbtnLast.Enabled = !pds.IsLastPage;

            rptNoticia.DataSource = pds;
            rptNoticia.DataBind();

            retorno = count;

            return retorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<Noticia> FiltrosNoticia(List<Noticia> lst)
    {

        try
        {
            lst = lst.Where(x => x.DataNoticia.Year == Convert.ToInt32(ddlAno.SelectedValue.ToString())).ToList();

            if (txtPalavraChave.Text.Trim().Count() > 0)
                lst = lst.Where(x => x.Titulo.ToLower().Contains(txtPalavraChave.Text.ToLower())).ToList();

            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PreencheDadosTela(Noticia pObjNoticia, RepeaterItemEventArgs e)
    {
        try
        {
            var item = e.Item.DataItem as Noticia;


            Label lblDataNoticia = (Label)e.Item.FindControl("lblDataNoticia");
            lblDataNoticia.Text = pObjNoticia.DataNoticia.ToShortDateString();

            Label lblDataNoticialMobil = (Label)e.Item.FindControl("lblDataNoticiaMobil");
            lblDataNoticialMobil.Text = pObjNoticia.DataNoticia.ToShortDateString();

            Label lblTitulo = (Label)e.Item.FindControl("lblTitulo");
            lblTitulo.Text = pObjNoticia.Titulo;

            Label lblResumo = (Label)e.Item.FindControl("lblResumo");
            lblResumo.Text = pObjNoticia.Resumo;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rptNoticia_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Noticia noticia = new Noticia();

        try
        {
            noticia = (Noticia)e.Item.DataItem;
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    break;
                case ListItemType.Footer:
                    break;
                case ListItemType.Item:
                    PreencheDadosTela(noticia, e);
                    break;
                case ListItemType.AlternatingItem:
                    PreencheDadosTela(noticia, e);
                    break;
                case ListItemType.SelectedItem:
                    break;
                case ListItemType.EditItem:
                    break;
                case ListItemType.Separator:
                    break;
                case ListItemType.Pager:
                    break;
                default:
                    break;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNoticias();
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindNoticias();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}