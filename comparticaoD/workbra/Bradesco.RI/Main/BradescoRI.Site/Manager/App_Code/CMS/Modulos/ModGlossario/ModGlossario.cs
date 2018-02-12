using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for ModGlossario
/// </summary>
public class ModGlossario
{
    #region Propriedades
    public virtual int IdiomaId { get; set; }
    public virtual int GlossarioId { get; set; }
    public virtual int ConteudoId { get; set; }
    public virtual DateTime Data { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["glossarioId"], DBNull.Value)))
            this.GlossarioId = Convert.ToInt32(pobjIDataReader["glossarioId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["glossarioId"], DBNull.Value)))
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["conteudoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);

    }
    #endregion
}