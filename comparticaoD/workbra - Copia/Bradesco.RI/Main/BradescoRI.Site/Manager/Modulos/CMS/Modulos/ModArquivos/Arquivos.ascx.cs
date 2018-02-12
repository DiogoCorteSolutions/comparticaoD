using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.IO;

public partial class Modulos_CMS_Modulos_ModArquivos_Arquivos : System.Web.UI.UserControl
{
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

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObterConteudo();
    }

    protected void imgDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=downloadName.pdf");
            Response.WriteFile(Server.MapPath(@"~/path of pdf/actualfile.pdf"));
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rptArquivoDownloadUnico.Visible)
                BindArquivos(rptArquivoDownloadUnico);
            else if (rptDownloadMultiplo.Visible)
                BindArquivos(rptDownloadMultiplo);
            else if (rptDownloadPodCast.Visible)
                BindArquivos(rptDownloadPodCast);

        }
        catch (Exception ex)
        {
            throw;
        }
    }



    protected void rptDownloadMultiplo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Arquivos arquivo = new Arquivos();

        try
        {
            arquivo = (Arquivos)e.Item.DataItem;
            if (arquivo != null)
            {
                switch (e.Item.ItemType)
                {
                    case ListItemType.Header:
                        break;
                    case ListItemType.Footer:
                        break;
                    case ListItemType.Item:
                        PreencheDadosTela(arquivo, e, Utilitarios.TipoLayoutArquivo.BoxComCheck);
                        break;
                    case ListItemType.AlternatingItem:
                        PreencheDadosTela(arquivo, e, Utilitarios.TipoLayoutArquivo.BoxComCheck);
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

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rptArquivoDownloadUnico_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Arquivos arquivo = new Arquivos();

        try
        {
            arquivo = (Arquivos)e.Item.DataItem;
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    break;
                case ListItemType.Footer:
                    break;
                case ListItemType.Item:
                    PreencheDadosTela(arquivo, e, Utilitarios.TipoLayoutArquivo.BoxSemCheck);
                    break;
                case ListItemType.AlternatingItem:
                    PreencheDadosTela(arquivo, e, Utilitarios.TipoLayoutArquivo.BoxSemCheck);
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

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (rptArquivoDownloadUnico.Visible)
                BindArquivos(rptArquivoDownloadUnico);
            else if (rptDownloadMultiplo.Visible)
                BindArquivos(rptDownloadMultiplo);
            else if (rptDownloadPodCast.Visible)
                BindArquivos(rptDownloadPodCast);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlTempo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rptArquivoDownloadUnico.Visible)
                BindArquivos(rptArquivoDownloadUnico);
            else if (rptDownloadMultiplo.Visible)
                BindArquivos(rptDownloadMultiplo);
            else if (rptDownloadPodCast.Visible)
                BindArquivos(rptDownloadPodCast);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void rptArquivoDownloadUnico_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "DownloadFile")
            {
                Arquivos arq = DOArquivos.Obter(new Arquivos() { Id = Convert.ToInt32(e.CommandArgument.ToString()) });

                if (arq.Caminho != string.Empty)
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + arq.Caminho);
                    Response.WriteFile(Server.MapPath(arq.Caminho));
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void chkDownload_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var item = ((CheckBox)sender).Parent as RepeaterItem;



        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rptDownloadMultiplo_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem ri = (RepeaterItem)e.Item;
            if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk = ri.FindControl("chkDownload") as CheckBox;
                chk.CheckedChanged += new EventHandler(chkDownload_CheckedChanged);
                //chk.Attributes.Add("onclick", "downsel('boxdowncheck1', '" + chk.ID + "' )");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rptDownloadMultiplo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        List<Arquivos> lstArquivoZip = new List<Arquivos>();
        try
        {
            foreach (RepeaterItem item in rptDownloadMultiplo.Items)
            {
                var chk = (CheckBox)item.FindControl("chkDownload") as CheckBox;
                var id = chk.InputAttributes["value"].ToString();

                if (chk.Checked)
                {
                    Arquivos arquivo = DOArquivos.Obter(new Arquivos() { Id = Convert.ToInt32(id.ToString()) });
                    lstArquivoZip.Add(arquivo);
                }
            }

            if (lstArquivoZip.Count > 0)
            {
                string arquivoZip = GerarArquivoZip(lstArquivoZip);


                FileInfo file = new FileInfo(Server.MapPath("/Uploads/Arquivos/") + arquivoZip);



                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + arquivoZip);
                Response.AppendHeader("Content-Cength", file.Length.ToString());
                Response.ContentType = PegarExtensaoArquivo(file.Extension.ToLower());
                Response.WriteFile(Server.MapPath("/Uploads/Arquivos/") + arquivoZip);
                Response.Flush();
                Response.End();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Métodos privados

    private int BindArquivos(Repeater objRepeater)
    {
        int retorno = 0;
        try
        {
            List<Arquivos> lstSearch = new List<Arquivos>();
            lstSearch = (List<Arquivos>)Session["sAqruivos"];

            lstSearch = lstSearch.ToList();
            lstSearch = VerificaArquivos(lstSearch);
            ddlAno.CssClass = "down-select";

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = lstSearch;

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

            objRepeater.DataSource = pds;
            objRepeater.DataBind();

            retorno = count;

            return retorno;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private List<Arquivos> VerificaArquivos(List<Arquivos> lista)
    {
        try
        {
            switch (ddlTempo.SelectedIndex)
            {
                case 0:
                    lista = lista.ToList();
                    break;

                case 1:
                    lista = lista.OrderBy(x => x.DataArquivo).ToList();
                    break;

                case 2:
                    lista = lista.OrderByDescending(x => x.DataArquivo).ToList();
                    break;
            }

            if (ddlAno.SelectedIndex > 0)
                lista = lista.Where(x => x.DataArquivo.Year == Convert.ToInt32(ddlAno.SelectedValue.ToString())).ToList();

            if (txtPalavraChave.Text.Trim().Count() > 0)
                lista = lista.Where(x => x.Titulo.ToLower().Contains(txtPalavraChave.Text.ToLower())).ToList();

            return lista;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PreencheDadosTela(Arquivos pObjArquivo, RepeaterItemEventArgs e, Utilitarios.TipoLayoutArquivo TipoLayout)
    {
        try
        {
            var item = e.Item.DataItem as Arquivos;

            switch (TipoLayout)
            {
                case Utilitarios.TipoLayoutArquivo.BoxSemCheck:
                    Label lblTitulo = (Label)e.Item.FindControl("lblTituloArquivo");
                    lblTitulo.Text = pObjArquivo.Titulo;

                    Label lblDescricao = (Label)e.Item.FindControl("lblDescricaoArquivo");
                    lblDescricao.Text = pObjArquivo.Descricao;

                    Label lblExtensao = (Label)e.Item.FindControl("lblExtensaoArquivo");
                    if (pObjArquivo.Extensao != null)
                        lblExtensao.Text = pObjArquivo.Extensao.Replace(".", "").ToUpper();

                    Label lblTamanho = (Label)e.Item.FindControl("lblTamanhoArquivo");
                    if (pObjArquivo.Tamanho != null)
                        lblTamanho.Text = pObjArquivo.Tamanho;

                    break;
                case Utilitarios.TipoLayoutArquivo.BoxComCheck:
                    Label lblTituloMultiplo = (Label)e.Item.FindControl("lblTituloMultiplo");
                    lblTituloMultiplo.Text = pObjArquivo.Titulo;

                    var chk = e.Item.FindControl("chkDownload") as CheckBox;
                    if (chk != null)
                    {
                        chk.Text = " ";
                        chk.InputAttributes.Add("value", item.Id.ToString());
                    }

                    Label lblDescricaoMultiplo = (Label)e.Item.FindControl("lblDescricaoMultiplo");
                    lblDescricaoMultiplo.Text = pObjArquivo.Descricao;

                    Label lblExtensaoMultiplo = (Label)e.Item.FindControl("lblExtensaoMultiplo");
                    if (pObjArquivo.Extensao != null)
                        lblExtensaoMultiplo.Text = pObjArquivo.Extensao.Replace(".", "").ToUpper();

                    Label lblTamanhoMultiplo = (Label)e.Item.FindControl("lblTamanhoMultiplo");
                    if (pObjArquivo.Tamanho != null)
                        lblTamanhoMultiplo.Text = pObjArquivo.Tamanho;
                    break;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private Arquivos CarregarArquivo(ModArquivo pObjModArquivo)
    {
        try
        {
            return DOArquivos.Obter(new Arquivos() { Id = pObjModArquivo.Id });
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);


        ModArquivo objArquivo = new ModArquivo() { ConteudoId = Convert.ToInt32(IdConteudo) };

        List<ModArquivo> lModArquivo = DOModArquivo.Listar(objArquivo);


        if (lModArquivo.Count > 0)
        {
            if (lModArquivo[0].ShowTitulo)
            {
                pnlAccordion.Visible = true;
                lblModuloTitulo.Text = lModArquivo[0].Titulo;
            }
            else
            {
                pnlAccordion.Visible = false;
                lblModuloTitulo.Text = String.Empty;
            }

            var lstItens = DoModArquivoItem.Listar(lModArquivo.FirstOrDefault());

            Session.Add("sAqruivos", lstItens);

            if (lModArquivo[0].ShowFiltro)
            {
                pnlFiltro.Visible = true;
                List<int> lstAno = new List<int>();

                foreach (var item in lstItens)
                {
                    if (!lstAno.Contains(item.DataArquivo.Year))
                        lstAno.Add(item.DataArquivo.Year);
                }

                ddlAno.DataSource = lstAno;
                ddlAno.DataBind();

                ddlAno.Items.Insert(0, new ListItem("Todos", "-1"));
                ddlAno.SelectedIndex = 0;

                txtPalavraChave.Attributes.Add("placeholder", "Busca por palavra chave");

            }
            else
            {
                pnlFiltro.Visible = false;
            }


            if (lModArquivo[0].TipoLayoutId == (int)Utilitarios.TipoLayoutArquivo.BoxSemCheck)
            {
                rptArquivoDownloadUnico.DataSource = lstItens;
                rptArquivoDownloadUnico.DataBind();
                rptArquivoDownloadUnico.Visible = true;
                rptDownloadMultiplo.Visible = false;
                BindArquivos(rptArquivoDownloadUnico);
            }
            else if (lModArquivo[0].TipoLayoutId == (int)Utilitarios.TipoLayoutArquivo.BoxComCheck)
            {
                rptDownloadMultiplo.DataSource = lstItens;
                rptDownloadMultiplo.DataBind();
                rptDownloadMultiplo.Visible = true;
                rptArquivoDownloadUnico.Visible = false;
                BindArquivos(rptDownloadMultiplo);
            }
            else if (lModArquivo[0].TipoLayoutId == (int)Utilitarios.TipoLayoutArquivo.BoxPodCast)
            {
                rptDownloadPodCast.DataSource = lstItens;
                rptDownloadPodCast.DataBind();
                rptDownloadPodCast.Visible = true;
                rptArquivoDownloadUnico.Visible = false;
            }

            divSemConteudo.Visible = false;
            //pnlFiltro.Visible = true;
            divConteudo.Visible = true;
        }
        else
        {
            divSemConteudo.Visible = true;
            pnlFiltro.Visible = false;
            divConteudo.Visible = false;
        }

    }

    private string PegarExtensaoArquivo(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    private string GerarArquivoZip(List<Arquivos> lstArquivoZip)
    {
        try
        {
            ZipFile zip = new ZipFile();

            foreach (var item in lstArquivoZip)
            {
                if (item.Caminho != string.Empty)
                    zip.AddFile(Server.MapPath(item.Caminho), "/");
            }

            var strCaminho = Guid.NewGuid() + ".zip";
            zip.Save(Server.MapPath("/Uploads/Arquivos/") + strCaminho);
            zip.Dispose();

            return strCaminho;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private Repeater VerificaRepeater()
    {
        try
        {
            Repeater repeater = new Repeater();
            if (rptArquivoDownloadUnico.Visible)
                repeater = rptArquivoDownloadUnico;
            else if (rptDownloadMultiplo.Visible)
                repeater = rptDownloadMultiplo;
            else if (rptDownloadPodCast.Visible)
                repeater = rptDownloadPodCast;

            return repeater;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        BindArquivos(VerificaRepeater());
    }

    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        BindArquivos(VerificaRepeater());
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        BindArquivos(VerificaRepeater());
    }

    protected void lbtnLast_Click(object sender, EventArgs e)
    {

        CurrentPage = BindArquivos(VerificaRepeater()) - 1;
        BindArquivos(VerificaRepeater());

    }
}