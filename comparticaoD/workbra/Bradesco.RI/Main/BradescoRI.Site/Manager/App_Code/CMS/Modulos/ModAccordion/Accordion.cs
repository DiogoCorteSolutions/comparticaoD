using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Accordion
/// </summary>
[Serializable()]
public class Accordions
{

    #region Propriedades    
    public virtual int AccordionId { get; set; }
    public virtual int ConteudoId { get; set; }
    public virtual string Titulo { get; set; }
    public virtual DateTime Data { get; set; }
    public virtual int statusId { get; set; }
    public virtual Boolean PainelAberto { get; set; }

    public virtual string EstiloPainel { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["accordionId"], DBNull.Value)))
        {
            this.AccordionId = Convert.ToInt32(pobjIDataReader["accordionId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
        {
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        }
                                        
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = DateTime.Parse(pobjIDataReader["data"].ToString());
        }
        
        if ((!object.ReferenceEquals(pobjIDataReader["statusId"], DBNull.Value)))
        {
            this.statusId = Convert.ToInt32(pobjIDataReader["statusId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["PainelAberto"], DBNull.Value)))
        {
            this.PainelAberto = Convert.ToBoolean(pobjIDataReader["PainelAberto"]);
        }

        if(PainelAberto)
        {
            EstiloPainel = "panel-collapse collapse in";
        }
        else
        {
            EstiloPainel = "panel-collapse collapse";
        }                
    }
    #endregion

}
