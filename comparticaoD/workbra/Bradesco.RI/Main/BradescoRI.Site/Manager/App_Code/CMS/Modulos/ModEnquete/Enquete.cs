using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for Enquete
/// </summary>
public class Enquete
{
    #region Propriedades
    public virtual int IdEnquete { get; set; }
    public virtual string Descricao { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["enqueteId"], DBNull.Value)))
        {
            this.IdEnquete = Convert.ToInt32(pobjIDataReader["enqueteId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }
       
    }

    #endregion
}