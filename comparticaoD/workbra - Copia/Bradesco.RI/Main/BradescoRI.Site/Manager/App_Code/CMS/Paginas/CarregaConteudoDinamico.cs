using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manager.Controls
{
    /// <summary>
    /// Summary description for CarregaConteudoDinamico
    /// </summary>
    public class CarregaConteudoDinamico : WebControl
    {
        public string PathModulo
        {
            get
            {
                String path = (String)ViewState["PathModulo"];
                
                return ((path == null) ? String.Empty : path);
            }
            set
            {
                ViewState["PathModulo"] = value;
            }
        }

        public string IdModulo
        {
            get
            {
                String id = (String)ViewState["IdModulo"];

                return ((id == null) ? String.Empty : id);
            }
            set
            {
                ViewState["IdModulo"] = value;
            }
        }
        protected override void Render(HtmlTextWriter output)
        {

            Control objContainer = TemplateControl.LoadControl(String.Concat("~/Modulos/CMS/Modulos", PathModulo));
            objContainer.ID = string.Concat("CTT_", IdModulo);

            this.Controls.Add(objContainer);
            
            string auxOutputPar = null;
            dynamic tempWriter = new StringWriter();
            base.RenderChildren(new HtmlTextWriter(tempWriter));
            auxOutputPar = tempWriter.ToString();

            //se o resultado for um div vazio, retorna string.Empty. Essa verificação feita assim ignora case e qualquer atributo no div, 
            //além de considerar vazio um div que contenha apenas espaçamento (espaços, tabs, quebras de linha, mas sem conteúdo)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(auxOutputPar, "(?is)^\\W*<div[^>]*>\\W*</div>\\W*$"))
                {
                    auxOutputPar = string.Empty;
                }
            }

            output.Write(auxOutputPar);
        }
    }
}
