using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

public partial class Modulos_CMS_Modulos_ModEventos_CalendarioEventos : System.Web.UI.UserControl
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if(! Page.IsPostBack)
        {
            IniciarTela();
            ListarProximosEventos();
            ListarPeriodoSilencio();
        }

        ListarEventosMes();
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
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
                lblDia.Text = item.DataInicio.ToString("dd") + "-" + item.DataFim.ToString("dd");
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

    protected void rptCategorias_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            EventoMes item = (EventoMes)e.Item.DataItem;
            Literal ltrBullet = (Literal)e.Item.FindControl("ltrBullet");

            ltrBullet.Text= "<span class=\"box-eve-ico\" style=\"color:"+ item.Cor +"\">• </span>";
            
        }
    }

    protected void Calendario_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
            e.Cell.BackColor = System.Drawing.Color.White;

        if (lstEventosMes != null)
        {
            foreach (EventoMes evento in lstEventosMes)
            {

                if (evento.DataFim != null && evento.DataFim > DateTime.MinValue)
                {
                    if (e.Day.Date >= evento.DataInicio && e.Day.Date <= evento.DataFim)
                    {
                        e.Cell.ToolTip = evento.Titulo;

                        Literal literal1 = new Literal();
                        literal1.Text = "<br/><div class=\"box-eve-icoint\" style=\"color:" + evento.Cor + "\">• </div>";
                        e.Cell.Controls.Add(literal1);
                    }
                }
                else if (evento.DataInicio.Equals(e.Day.Date))
                {
                    e.Cell.ToolTip = evento.Titulo;

                    Literal literal1 = new Literal();
                    literal1.Text = "<br/><div class=\"box-eve-icoint\" style=\"color:" + evento.Cor + "\">• </div>";
                    e.Cell.Controls.Add(literal1);
                }
            }
        }
    }

    protected void Calendario_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        if (e.NewDate != DateTime.MinValue)
        {
            primeiroDia = e.NewDate;
            ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);

            ListarEventosMes();
        }
    }

    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        Calendario.VisibleDate = Convert.ToDateTime("01/01/" + ddlAno.SelectedValue);
    }

    protected void ddlTipoEvento_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarProximosEventos();
    }

    protected void ddlAnoPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarPeriodoSilencio();
    }

    #endregion

    #region Métodos
    private void IniciarTela()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        SiglaCultura = DOIdioma.ObterSigla(IdIdioma);

        this.ddlTipoEvento.DataSource = DOModEvento.ListarTipoEvento(IdIdioma);
        this.ddlTipoEvento.DataTextField = "Descricao";
        this.ddlTipoEvento.DataValueField = "IdTipoEvento";
        this.ddlTipoEvento.DataBind();
        this.ddlTipoEvento.Items.Insert(0, new ListItem(Resources.Calendario.Evento) );

        this.ddlTipoEvento.SelectedIndex = 0;

        this.ddlAno.DataSource = DOModEvento.ListarAnoEvento();
        this.ddlAno.DataBind();

        this.ddlAno.SelectedValue = DateTime.Now.Year.ToString();

        this.ddlAnoPeriodo.DataSource = DOModEvento.ListarAnoPeriodo();
        this.ddlAnoPeriodo.DataBind();

        this.ddlAnoPeriodo.SelectedValue = DateTime.Now.Year.ToString();


        Calendario.VisibleDate = DateTime.Now;

        primeiroDia = Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy"));
        ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);        
    }

    private void ListarProximosEventos()
    {
        List<ProxEventos> lstProxEventos;

        if (ddlTipoEvento.SelectedIndex > 0)
            lstProxEventos = DOModEvento.ListarProxEventos(IdIdioma, Convert.ToInt32(ddlTipoEvento.SelectedValue));
        else
            lstProxEventos = DOModEvento.ListarProxEventos(IdIdioma);

        rptProximosEventos.DataSource = lstProxEventos;
        rptProximosEventos.DataBind();
    }

    private void ListarEventosMes()
    {
        if (primeiroDia > DateTime.MinValue && ultimoDia > DateTime.MinValue)
        {
            lstEventosMes = DOModEvento.ListarEventosMes(IdIdioma, primeiroDia, ultimoDia);

            rptCategorias.DataSource = lstEventosMes;
            rptCategorias.DataBind();
        }

    }

    private void ListarPeriodoSilencio()
    {
        rptPeriodoSilencio.DataSource = DOModEvento.ListarPeriodoSilencio(Convert.ToInt32(ddlAnoPeriodo.SelectedValue)) ;
        rptPeriodoSilencio.DataBind();

    }
    #endregion

}