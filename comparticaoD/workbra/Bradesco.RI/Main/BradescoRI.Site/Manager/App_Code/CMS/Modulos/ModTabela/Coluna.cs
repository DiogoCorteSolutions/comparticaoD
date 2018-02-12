using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Coluna
/// </summary>
[Serializable()]
public class Coluna
{
    #region Propriedades

    public string NomeColuna { get; set; }
    public string ValorColuna { get; set; }
    public int IdModTabela { get; set; }


    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["NomeColuna"], DBNull.Value)))
            this.NomeColuna = pobjIDataReader["NomeColuna"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["ValorColuna"], DBNull.Value)))
            this.ValorColuna = pobjIDataReader["ValorColuna"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["IdModTabela"], DBNull.Value)))
            this.IdModTabela = Convert.ToInt32(pobjIDataReader["IdModTabela"]);

    }

    #endregion
}