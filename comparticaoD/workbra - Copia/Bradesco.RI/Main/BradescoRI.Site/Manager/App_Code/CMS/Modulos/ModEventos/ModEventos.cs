using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for ModEventos
/// </summary>
public class ModEventos
{
    #region Propriedades
    public string UrlListaEvento { get; set; }
    public string UrlTodosEventos { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["urlListaEvento"], DBNull.Value)))
        {
            this.UrlListaEvento = pobjIDataReader["urlListaEvento"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["urlTodosEventos"], DBNull.Value)))
        {
            this.UrlTodosEventos = pobjIDataReader["urlTodosEventos"].ToString();
        }
    }

    #endregion
}