using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for PeriodoSilencio
/// </summary>
public class PeriodoSilencio
{
    #region Propriedades
    public int IdPeriodo { get; set; }
    public DateTime DataDivulgacao { get; set; }
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

        if ((!object.ReferenceEquals(pobjIDataReader["periodoId"], DBNull.Value)))
        {
            this.IdPeriodo = Convert.ToInt32(pobjIDataReader["periodoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataDivulgacao"], DBNull.Value)))
        {
            this.DataDivulgacao = Convert.ToDateTime(pobjIDataReader["dataDivulgacao"]);
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