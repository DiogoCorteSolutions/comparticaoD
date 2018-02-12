using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manager.Controls
{
    /// <summary>
    /// Summary description for CarregaConteudoDinamico
    /// </summary>
    public class CarregaConteudoDinamico : WebControl, INamingContainer
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
      
        public string IdConteudo
        {
            get
            {
                String id = (String)ViewState["IdConteudo"];

                return ((id == null) ? String.Empty : id);
            }
            set
            {
                ViewState["IdConteudo"] = value;
            }
        }
        protected override void Render(HtmlTextWriter output)
        {
            EnsureChildControls();
            RenderChildren(output);
        }

        protected override void CreateChildControls()
        {
            this.ID = IdConteudo;
            this.Controls.Add(TemplateControl.LoadControl(String.Concat("~/Modulos/CMS/Modulos", PathModulo)));
        }

        protected override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
        {
            string auxOutputPar = null;
            dynamic tempWriter = new StringWriter();
            base.RenderChildren(new HtmlTextWriter(tempWriter));
            auxOutputPar = tempWriter.ToString();

            //se o resultado for um div vazio, retorna string.Empty. Essa verificação feita assim ignora case e qualquer atributo no div, 
            //além de considerar vazio um div que contenha apenas espaçamento (espaços, tabs, quebras de linha, mas sem conteúdo)
            if (System.Text.RegularExpressions.Regex.IsMatch(auxOutputPar, "(?is)^\\W*<div[^>]*>\\W*</div>\\W*$"))
            {
                auxOutputPar = string.Empty;
            }

            writer.Write(auxOutputPar);
        }


    }

}
