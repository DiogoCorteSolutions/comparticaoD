using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

public partial class Modulos_CMS_Modulos_ModBanners_Banners : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public int IdConteudo { get; set; }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        List<Banners> lstBanners = DOModBanners.ListarModBanners(IdConteudo, IdIdioma);

        if (lstBanners.Count == 0)
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
        else
        {
            LoadBanner(lstBanners);

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }

    }

    private void LoadBanner(List<Banners> lstBanners)
    {
        int count = 0;
        StringBuilder indicators = new StringBuilder();
        StringBuilder slides = new StringBuilder();

        if (lstBanners.Count == 1)
        {
            litdiv.Text = "<div id=\"x_006_img_thumbnail\" class=\"carousel slide ps_slide_y x_006_img_thumbnail_indicators x_006_img_thumbnail_control_button thumb_scroll_y swipe_y ps_easeOutInCubic\" data-ride=\"carousel\" data-pause=\"hover\"  data-interval=\"false\" data-duration=\"2000\">";

            Banners item = lstBanners[0];
            
            slides.AppendFormat("<div class=\"item {0}\" style=\"transition-duration: 2000ms;\">", (count == 0 ? "active" : string.Empty));
            slides.AppendFormat("   <img src=\"{0}\" alt=\"x_006_img_thumbnail_{1}\">", String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModBanner"], item.IdBanner, item.Arquivo), count.ToString().PadLeft(2, '0'));
            slides.AppendFormat("   <div class=\"x_006_img_thumbnail_text x_006_img_thumbnail_text_center\">");

            if (!string.IsNullOrEmpty(item.Texto1))
                slides.AppendFormat("       <h1 data-animation=\"animated fadeInUp\" class=\"page-heading\">{0}</h1><hr class=\"small\">", item.Texto1);
            if (!string.IsNullOrEmpty(item.Texto2))
                slides.AppendFormat("       <p data-animation=\"animated fadeInUp\" class=\"subheading\">{0}</p>", item.Texto2);
            if (!string.IsNullOrEmpty(item.TextoUrl))
                slides.AppendFormat("       <a href=\"{0}\" data-animation=\"animated fadeInUp\" class=\"link-play\"><span class=\"lplay-icon_white\"></span>{1}</a>", item.Url, item.TextoUrl);

            slides.AppendFormat("   </div>");
            slides.AppendFormat("</div>");
        } 
        else
        {
            litdiv.Text = "<div id=\"x_006_img_thumbnail\" class=\"carousel slide ps_slide_y x_006_img_thumbnail_indicators x_006_img_thumbnail_control_button thumb_scroll_y swipe_y ps_easeOutInCubic\" data-ride=\"carousel\" data-pause=\"hover\"  data-interval=\"8000\" data-duration=\"2000\">";
            litButtons.Text = "<a class=\"left carousel-control\" href=\"#x_006_img_thumbnail\" role=\"button\" data-slide=\"prev\"><span class=\"fa fa-angle-up\"></span></a><a class=\"right carousel-control\" href=\"#x_006_img_thumbnail\" role=\"button\" data-slide=\"next\"><span class=\"fa fa-angle-down\"></span></a>";

            foreach (Banners item in lstBanners)
            {
                string imgSrc = String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModBanner"], item.IdBanner, item.Arquivo);

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(imgSrc)))
                {
                    if (count == 0)
                    {
                        indicators.AppendFormat("<li data-target=\"#x_006_img_thumbnail\" data-slide-to=\"0\" class=\"active\" style=\"\">");
                        indicators.AppendFormat("   <img src=\"{0}\" alt=\"bootstrap carousel with thumbnail navigation\"></li>", imgSrc);
                    }
                    else
                    {
                        indicators.AppendFormat("<li data-target=\"#x_006_img_thumbnail\" data-slide-to=\"{0}\" class=\"\" style=\"\">", count, (count == 0 ? "active" : string.Empty));
                        indicators.AppendFormat("   <img src=\"{0}\" alt=\"x_006_img_thumbnail_{1}_sm\"></li>", imgSrc, count.ToString().PadLeft(2, '0'));
                    }

                    slides.AppendFormat("<div class=\"item {0}\" style=\"transition-duration: 2000ms;\">", (count == 0 ? "active" : string.Empty));
                    slides.AppendFormat("   <img src=\"{0}\" alt=\"x_006_img_thumbnail_{1}\">", imgSrc, count.ToString().PadLeft(2, '0'));
                    slides.AppendFormat("   <div class=\"x_006_img_thumbnail_text x_006_img_thumbnail_text_center\">");

                    if (!string.IsNullOrEmpty(item.Texto1))
                        slides.AppendFormat("       <h1 data-animation=\"animated fadeInUp\" class=\"page-heading\">{0}</h1><hr class=\"small\">", item.Texto1);
                    if (!string.IsNullOrEmpty(item.Texto2))
                        slides.AppendFormat("       <p data-animation=\"animated fadeInUp\" class=\"subheading\">{0}</p>", item.Texto2);
                    if (!string.IsNullOrEmpty(item.TextoUrl))
                        slides.AppendFormat("       <a href=\"{0}\" data-animation=\"animated fadeInUp\" class=\"link-play\"><span class=\"lplay-icon_white\"></span>{1}</a>", item.Url, item.TextoUrl);

                    slides.AppendFormat("   </div>");
                    slides.AppendFormat("</div>");

                    count++;
                }
            }

            litIndicators.Text = indicators.ToString();
        }

        litSlides.Text = slides.ToString();
        litFechaDiv.Text = "</div>";

    }
    #endregion
}