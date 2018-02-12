using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TabelaON
/// </summary>
[Serializable()]
public class TabelaON
{

    #region Propriedades
    public int IdModTabela { get; set; }
    public string NomeAcionario { get; set; }
    public string NomeTabela { get; set; }
    public string NomeColuna { get; set; }
    

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;


        if ((!object.ReferenceEquals(pobjIDataReader["NomeColuna"], DBNull.Value)))
            this.NomeColuna = pobjIDataReader["NomeColuna"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["NomeTabela"], DBNull.Value)))
            this.NomeAcionario = pobjIDataReader["NomeTabela"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["NomeAcionario"], DBNull.Value)))
            this.NomeTabela = pobjIDataReader["NomeAcionario"].ToString();


        if ((!object.ReferenceEquals(pobjIDataReader["IdModTabela"], DBNull.Value)))
            this.IdModTabela = Convert.ToInt32(pobjIDataReader["IdModTabela"]);
    }

    #endregion
}
