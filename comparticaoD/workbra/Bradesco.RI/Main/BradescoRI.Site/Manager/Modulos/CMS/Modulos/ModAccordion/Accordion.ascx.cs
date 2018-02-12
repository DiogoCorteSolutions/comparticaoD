using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModAccordion_Accordion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObterConteudo();
    }

    #region Variáveis
    public int IdConteudo { get; set; }
   
    #endregion

    #region Métodos
    private void ObterConteudo()
    {        
        IdConteudo = Convert.ToInt32(this.Parent.ID.Replace("CTT_", string.Empty));

        var objDados = DOAccordion.Listar(IdConteudo);

        if (objDados != null)
        {
            divSemConteudo.Visible = false;
            divConteudo.Visible = true;

            rptAccordian.DataSource = objDados;
            rptAccordian.DataBind();
        }
        else
        {
            divSemConteudo.Visible = true;
            divConteudo.Visible = false;
        }
    }

    protected void rptAccordian_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Accordions objDados = (Accordions)e.Item.DataItem;

            List<ConteudoPagina> lstConteudoPagina = DOConteudoPagina.ListarAccordion(objDados.AccordionId);

            string urlPagina = Request.RawUrl.Split('?')[0];

            PlaceHolder placeHolderControl = (PlaceHolder)e.Item.FindControl("PlaceHolder");

            if (lstConteudoPagina.Count > 0)
            {
                foreach (ConteudoPagina objConteudoPagina in lstConteudoPagina)
                {
                    Control objContainer = LoadControl("~/Modulos/CMS/Paginas/Container.ascx");

                    if (urlPagina.Contains("Preview.aspx"))
                    {
                        //Limpa os controles para exibição
                        objContainer.Controls.Clear();
                    }
                    else
                    {
                        foreach (Control objControl in objContainer.Controls)
                        {
                            if (objControl.ID == "divSubir" || objControl.ID == "divDescer")
                            {
                                objControl.Visible = false;
                            }
                        }
                    }

                    objContainer.ID = string.Concat("CTT_", objConteudoPagina.ConteudoId);
                    objContainer.Controls.AddAt(0, LoadControl(String.Concat("~/Modulos/CMS/Modulos", objConteudoPagina.Arquivo)));

                    placeHolderControl.Controls.Add(objContainer);
                }
            }
        }
    }
    #endregion
}