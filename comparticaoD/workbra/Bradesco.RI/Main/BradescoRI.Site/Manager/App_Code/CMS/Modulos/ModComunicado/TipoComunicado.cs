using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for TipoComunicado
/// </summary>
public class TipoComunicado
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

        if ((!object.ReferenceEquals(pobjIDataReader["TipoComunicadoId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["TipoComunicadoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["Descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["Descricao"].ToString();

    }
    #endregion
}