using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for EventoMes
/// </summary>
public class EventoMes
{
    #region Propriedades
    public int IdTipoEvento { get; set; }
    public string Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["tipoEventoId"], DBNull.Value)))
        {
            this.IdTipoEvento = Convert.ToInt32(pobjIDataReader["tipoEventoId"]);
        }        
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataInicio"], DBNull.Value)))
        {
            this.DataInicio = Convert.ToDateTime(pobjIDataReader["dataInicio"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataFim"], DBNull.Value)))
        {
            this.DataFim = Convert.ToDateTime(pobjIDataReader["dataFim"]);
        }
        
    }

    #endregion
}