using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Modulos_CMS_Modulos_ModEventos_CalendarioEventos : System.Web.UI.UserControl
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if(! Page.IsPostBack)
        { 
            Calendario.VisibleDate = DateTime.Now;
            
            primeiroDia = Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy"));
            ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
        }

        ObterConteudo();
    }

    #region Variáveis
    public int IdIdioma { get; set; }
    public List<EventoMes> lstEventosMes;
    public DateTime ultimoDia
    {
        get { return (DateTime)(ViewState["ultimoDia"] ?? DateTime.MinValue); }
        set { ViewState["ultimoDia"] = value; }
    }
    public DateTime primeiroDia
    {
        get { return (DateTime)(ViewState["primeiroDia"] ?? DateTime.MinValue); }
        set { ViewState["primeiroDia"] = value; }
    }
    public string SiglaCultura
    {
        get { return (string)(ViewState["SiglaCultura"] ?? "pt-BR"); }
        set { ViewState["SiglaCultura"] = value; }
    }
    #endregion

    #region Eventos
    protected void rptProximosEventos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ProxEventos item = (ProxEventos)e.Item.DataItem;

            Label lblDia = (Label)e.Item.FindControl("lblDia");
            Label lblMes = (Label)e.Item.FindControl("lblMes");
            Label lblData = (Label)e.Item.FindControl("lblData");


            if (item.DataFim != null && item.DataFim > DateTime.MinValue)
            {
                lblDia.Text = item.DataInicio.ToString("dd") + " - " + item.DataFim.ToString("dd");
                lblMes.Text = (item.DataInicio.ToString("MM").Equals(item.DataFim.ToString("MM")) ? item.DataInicio.ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) : item.DataInicio.ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) + " - " + item.DataFim.ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)));

                if (SiglaCultura.Equals("pt-BR"))
                {
                    lblData.Text = (item.DataInicio.ToString("MM").Equals(item.DataFim.ToString("MM")) ? item.DataInicio.ToString("dd") + " a " + item.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) : item.DataInicio.ToString("dd 'de' MMMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) + " a " + item.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura))); 
                }
                else
                {
                    //lblData.Text = (item.DataInicio.ToString("MM").Equals(item.DataFim.ToString("MM")) ? item.DataInicio.ToString("dd") + " a " + item.DataFim.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) : item.DataInicio.ToString("dd 'de' MMMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)) + " a " + item.DataFim.ToString("dd 'de' MMMM de yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura)));
                }

            }
            else
            {
                lblDia.Text = item.DataInicio.ToString("dd");

                lblMes.Text = item.DataInicio.ToString("MMM", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura));

                if (SiglaCultura.Equals("pt-BR"))
                {
                    lblData.Text = item.DataInicio.ToString("dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura));
                }
                else
                {
                    //lblData.Text = item.DataInicio.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.GetCultureInfo(SiglaCultura));
                }

            }            
        }
    }

    protected void rptEventosMes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            EventoMes item = (EventoMes)e.Item.DataItem;

            Label lblEventoMes = (Label)e.Item.FindControl("lblEventoMes");


            if (item.DataFim != null && item.DataFim > DateTime.MinValue)
                lblEventoMes.Text = (item.DataInicio.Month.Equals(primeiroDia.Month) ? (item.DataFim.Month != item.DataInicio.Month ? string.Format("{0}-{1} {2}", item.DataInicio.Day, ultimoDia.Day, item.Titulo) : string.Format("{0}-{1} {2}", item.DataInicio.Day, item.DataFim.Day, item.Titulo)) : string.Format("{0}-{1} {2}", primeiroDia.Day, item.DataFim.Day, item.Titulo));
            else if (item.DataInicio >= primeiroDia && item.DataFim <= ultimoDia)
            {
                lblEventoMes.Text = string.Format("{0} {1}", item.DataInicio.Day, item.Titulo);
            }
        }
    }

    protected void Calendario_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
            e.Cell.BackColor = System.Drawing.Color.Red;

        foreach (EventoMes evento in lstEventosMes)
        {
            if (evento.DataFim != null && evento.DataFim > DateTime.MinValue)
            {
                if (e.Day.Date >= evento.DataInicio && e.Day.Date <= evento.DataFim)
                {
                    Literal literal1 = new Literal();
                    literal1.Text = "<br/>";
                    e.Cell.Controls.Add(literal1);
                    System.Web.UI.WebControls.Image image;
                    image = new System.Web.UI.WebControls.Image();
                    image.ImageUrl = "/Manager/Imagens/home-on.png";
                    image.ToolTip = evento.Titulo;
                    e.Cell.Controls.Add(image);
                }
            }
            else if (evento.DataInicio.Equals(e.Day.Date))
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                System.Web.UI.WebControls.Image image;
                image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "/Manager/Imagens/home-on.png";
                image.ToolTip = evento.Titulo;
                e.Cell.Controls.Add(image);
                //Label label1 = new Label();
                //label1.Text = "*";
                //label1.Font.Name = "Trebuchet MS";
                //label1.Font.Size = new FontUnit(FontSize.Medium);
                //label1.ForeColor = System.Drawing.Color.DarkOrange;
                //label1.Font.Bold = true;
                //e.Cell.Controls.Add(label1);
            }
        }
    }
    #endregion

    #region Métodos
    private void ObterConteudo()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        SiglaCultura = DOIdioma.ObterSigla(IdIdioma);

        List<ProxEventos> lstProxEventos = DOModEvento.ListarProxEventos(IdIdioma);

        rptProximosEventos.DataSource = lstProxEventos;
        rptProximosEventos.DataBind();

        lstEventosMes = DOModEvento.ListarEventosMes(IdIdioma,primeiroDia, ultimoDia);

        rptEventosMes.DataSource = lstEventosMes;
        rptEventosMes.DataBind();

    }
    #endregion


    protected void Calendario_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        if (e.NewDate != DateTime.MinValue)
        {
            primeiroDia = e.NewDate;
            ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);

            lstEventosMes = DOModEvento.ListarEventosMes(IdIdioma, primeiroDia, ultimoDia);

            rptEventosMes.DataSource = lstEventosMes;
            rptEventosMes.DataBind();
        }
    }

}