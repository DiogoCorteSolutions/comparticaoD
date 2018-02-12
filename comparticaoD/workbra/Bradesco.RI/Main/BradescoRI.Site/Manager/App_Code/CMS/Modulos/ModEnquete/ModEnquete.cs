using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for ModEnquete
/// </summary>
public class ModEnquete
{
    #region Propriedades
    public virtual int IdConteudo { get; set; }
    public virtual int IdEnquete { get; set; }
    public virtual string Titulo{ get; set; }
    public virtual string UrlFaleConosco { get; set; }
    public virtual DateTime Data { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
        {
            this.IdConteudo = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["enqueteId"], DBNull.Value)))
        {
            this.IdEnquete = Convert.ToInt32(pobjIDataReader["enqueteId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["urlFaleConosco"], DBNull.Value)))
        {
            this.UrlFaleConosco = pobjIDataReader["urlFaleConosco"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);
        }
    }

    #endregion
}