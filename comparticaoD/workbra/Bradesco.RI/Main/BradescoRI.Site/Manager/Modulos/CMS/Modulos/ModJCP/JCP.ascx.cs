using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Modulos_CMS_Modulos_ModJCP_JCP : System.Web.UI.UserControl
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

        int count = 0;
        StringBuilder presentation = new StringBuilder();
        StringBuilder table = new StringBuilder();

        List<string> lstAnos = DOModJCP.ListarAno();

        if (lstAnos.Count == 0)
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
        else
        {
            List<JCP> lstJCP;
            string aba = string.Empty;

            foreach (string ano in lstAnos)
            {
                aba = (ano.Equals("0") ? Resources.JCP.HistoricoEventos : ano);

                presentation.AppendFormat("<li role=\"presentation\" class=\"{0}\"><a href=\"#{1}\" aria-controls=\"home\" role=\"tab\" data-toggle=\"tab\">{2}</a></li>", (count == 0 ? "active" : string.Empty), aba.Replace(" ", ""), aba);

                lstJCP = DOModJCP.Listar(IdConteudo, IdIdioma, Convert.ToInt32(ano));

                table.AppendFormat("<div role=\"tabpanel\" class=\"tab-pane fade in {0}\" id=\"{1}\">", (count == 0 ? "active" : string.Empty), aba.Replace(" ", ""));
                table.AppendLine("<table class=\"jcp-table\" align=\"left\">");
                table.AppendLine("<tr>");
                table.AppendFormat("<td class=\"jcp-table-col1l1\">{0}</td>", Resources.JCP.Periodo);
                table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", Resources.JCP.TipoProvento);
                table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", Resources.JCP.PosicaoAcionaria);

                if (!ano.Equals("0"))
                {
                    table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", Resources.JCP.DataPagamento);
                    table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", Resources.JCP.ValorAcao);
                }

                table.AppendLine("</tr>");

                foreach (JCP item in lstJCP)
                {
                    table.AppendLine("<tr>");
                    table.AppendFormat("<td class=\"jcp-table-col1\">{0}</td>", item.Periodo);
                    table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", item.TipoProvento);
                    table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", item.PosicaoAcionaria.ToString("dd/MM/yyyy"));

                    if (!ano.Equals("0"))
                    {
                        table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", item.DataPagamento.ToString("dd/MM/yyyy"));
                        table.AppendFormat("<td class=\"jcp-table-coln\">{0}</td>", item.ValorAcao);
                    }

                    table.AppendFormat("</tr>");
                }

                table.AppendFormat("</table>");
                table.AppendFormat("</div>");
                count++;
            }

            divSemConteudo.Visible = false;
            divConteudo.Visible = true;
        }

        litPresentation.Text = presentation.ToString();
        litTable.Text = table.ToString();
    }
    
    #endregion
}