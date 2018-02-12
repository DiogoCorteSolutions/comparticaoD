using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for ModComunicado
/// </summary>
public class ModComunicado
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual int ComunicadoId { get; set; }
    public virtual int ConteudoId { get; set; }
    public virtual DateTime Data { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ConteudoId"], DBNull.Value)))
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["ConteudoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["comunicadoId"], DBNull.Value)))
            this.ComunicadoId = Convert.ToInt32(pobjIDataReader["comunicadoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["Data"], DBNull.Value)))
            this.Data = Convert.ToDateTime(pobjIDataReader["Data"].ToString());

    }

    #endregion
}