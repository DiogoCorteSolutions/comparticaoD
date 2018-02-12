using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for ModJCP
/// </summary>
public class ModJCP
{
    #region Propriedades
    public virtual int IdConteudo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual int IdJCP { get; set; }
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
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["jcpId"], DBNull.Value)))
        {
            this.IdJCP = Convert.ToInt32(pobjIDataReader["jcpId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);
        }
    }

    #endregion
}