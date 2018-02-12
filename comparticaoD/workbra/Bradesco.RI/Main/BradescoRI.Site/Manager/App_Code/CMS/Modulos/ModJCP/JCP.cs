using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for JCP
/// </summary>
public class JCP
{
    #region Propriedades
    public virtual int IdJCP { get; set; }
    public virtual string Ano { get; set; }
    public virtual string Periodo { get; set; }
    public virtual string TipoProvento { get; set; }
    public virtual DateTime PosicaoAcionaria { get; set; }
    public virtual DateTime DataPagamento { get; set; }
    public virtual string ValorAcao { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["jcpId"], DBNull.Value)))
        {
            this.IdJCP = Convert.ToInt32(pobjIDataReader["jcpId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["ano"], DBNull.Value)))
        {
            this.Ano = pobjIDataReader["ano"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["periodo"], DBNull.Value)))
        {
            this.Periodo = pobjIDataReader["periodo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["posicaoAcionaria"], DBNull.Value)))
        {
            this.PosicaoAcionaria = Convert.ToDateTime( pobjIDataReader["posicaoAcionaria"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["tipoProvento"], DBNull.Value)))
        {
            this.TipoProvento = pobjIDataReader["tipoProvento"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataPagamento"], DBNull.Value)))
        {
            this.DataPagamento = Convert.ToDateTime(pobjIDataReader["dataPagamento"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["valorAcao"], DBNull.Value)))
        {
            this.ValorAcao = pobjIDataReader["valorAcao"].ToString();
        }
    }

    #endregion
}