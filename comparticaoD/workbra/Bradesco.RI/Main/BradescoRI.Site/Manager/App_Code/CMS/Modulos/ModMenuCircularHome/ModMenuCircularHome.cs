using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for ModMenuCircular
/// </summary>
public class ModMenuCircularHome
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual int IdMenuCircularHome { get; set; }
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
            this.ID = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["menuCircularHomeId"], DBNull.Value)))
        {
            this.IdMenuCircularHome = Convert.ToInt32(pobjIDataReader["menuCircularHomeId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);
        }
    }

    #endregion
}