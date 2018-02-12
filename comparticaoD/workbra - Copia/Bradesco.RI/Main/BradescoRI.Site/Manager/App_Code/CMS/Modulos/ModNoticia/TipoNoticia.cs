using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for TipoNoticia
/// </summary>
public class TipoNoticia
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Descricao { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["TipoNoticiaId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["TipoNoticiaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["Descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["Descricao"].ToString();

        #endregion
    }
}