using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModNoticia_Noticia : System.Web.UI.UserControl
{
    #region Eventos 

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObterConteudo();
    }

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
    #endregion

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
            noticia.TipoArquivo = new TipoArquivo() { Id = item.TipoArquivoId };
            noticia = DONoticia.ObterDestaqueHome(noticia);
            item.IdNoticia = noticia.ID;
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
                    Noticia noticia = DONoticia.ObterDestaqueHome(new Noticia() { TipoArquivo = new TipoArquivo() { Id = mNoticia.TipoArquivoId }, Destaque = true });
                    //Noticia noticia = DOModNoticia.Obter(new Noticia() { ID = mNoticia.IdNoticia });
                    if (noticia.ID > 0)
                    {

                        switch (mNoticia.TipoNoticiaId)
                        {
                            case ((int)(Utilitarios.TipoNoticia.Esquerdo)):
                                divEsquerdoDestaque.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoEsquerdaDestaque.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloEsquerdaDestaque.Text = noticia.Titulo;
                                lblResumoEsquerdaDestaque.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoSuperior)):
                                divDireitaSuperiorDestaque.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoDireitaSuperiorDestaque.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloDireitaSuperiorDestaque.Text = noticia.Titulo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoInferior)):
                                divDireitaInferiorDestaque.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoDireitaInferiorDestaque.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloDireitaInferiorDestaque.Text = noticia.Titulo;

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
                    Noticia noticia = DONoticia.Obter(new Noticia() { ID = mNoticia.IdNoticia });
                    //DONoticia.ObterDestaqueHome(new Noticia() { TipoArquivo = new TipoArquivo() { Id = mNoticia.TipoArquivoId },  Destaque = true });
                    //Noticia noticia = DOModNoticia.Obter(new Noticia() { ID = mNoticia.IdNoticia });
                    if (noticia.ID > 0)
                    {
                        switch (mNoticia.TipoNoticiaId)
                        {
                            case ((int)(Utilitarios.TipoNoticia.Esquerdo)):
                                divEsquerdoHome.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoEsquerdo.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloEsquerdo.Text = noticia.Titulo;
                                lblResumoEsquerdo.Text = noticia.Resumo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoSuperior)):
                                divDireitaSuperiorHome.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoDireitoSuperior.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloDireitoSuperior.Text = noticia.Titulo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoCentral)):
                                divDireitaCentralHome.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoDireitoCentral.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
                                lblTituloDireitoCentral.Text = noticia.Titulo;
                                break;

                            case ((int)(Utilitarios.TipoNoticia.DireitoInferior)):
                                divDireitaInferiorHome.Attributes.Add("style", "background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
                                lblSubTipoDireitoInferior.Text = DoTipoNoticia.Obter(noticia.TipoNoticia).Descricao;
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

    private int BindNoticias()
    {
        int retorno = 0;
        try
        {

            List<Noticia> lst = new List<Noticia>();
            lst = (List<Noticia>)Session["sNoticias"];

            if (lst[0] != null && lst[0].ID > 0)
            {

                CarregaDestaque(lst[0].TipoArquivo.Id);

                lst = FiltrosNoticia(DONoticia.Listar(new Noticia() { TipoArquivo = new TipoArquivo() { Id = lst[0].TipoArquivo.Id } }));

                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = lst.OrderByDescending(x => x.DataNoticia).ToList();

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
            }
            return retorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CarregaDestaque(int pTipoArquivoId)
    {
        try
        {
            Noticia noticia = DONoticia.ObterDestaqueHome(new Noticia() { Destaque = true, TipoArquivo = new TipoArquivo() { Id = pTipoArquivoId } });
            lblSubTipoDestaqueInterna.Text = noticia.TipoNoticia.Descricao;
            lblTituloDestaqueInterna.Text = noticia.Titulo;
            lblResumoDestaqueInterna.Text = noticia.Resumo;
            divBackGround.Attributes.Add("style", "height: 432px; background-image:url('" + DOArquivos.Obter(new Arquivos() { Id = noticia.Capa.Id }).Caminho + "')");
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
            item.TipoArquivo = DOTipoArquivo.Obter(item.TipoArquivo);
            Label lblVer = (Label)e.Item.FindControl("lblVer");
            if (item.TipoArquivo.Descricao == "Entrevistas")
                lblVer.Text = Resources.Noticias.VerEntrevista;
            else
                lblVer.Text = Resources.Noticias.VerNoticia;

            Image img = (Image)e.Item.FindControl("imgNoticia");
            img.ImageUrl = DOArquivos.Obter(new Arquivos() { Id = pObjNoticia.Capa.Id }).Caminho;

            Label lblDataNoticia = (Label)e.Item.FindControl("lblDataNoticia");
            lblDataNoticia.Text = pObjNoticia.DataNoticia.ToShortDateString();

            Label lblDataNoticialMobil = (Label)e.Item.FindControl("lblDataNoticiaMobil");
            lblDataNoticialMobil.Text = pObjNoticia.DataNoticia.ToShortDateString();

            Label lblTitulo = (Label)e.Item.FindControl("lblTitulo");
            lblTitulo.Text = pObjNoticia.Titulo;

            Label lblResumo = (Label)e.Item.FindControl("lblResumo");
            lblResumo.Text = pObjNoticia.Resumo.Replace("<p>", "").Replace("</p>", "").Replace("&nbsp;", "");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion



}