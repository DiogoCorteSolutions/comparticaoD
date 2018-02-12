using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VColuna
/// </summary>
[Serializable()]
public class VColuna
{
    #region Propriedades

    public string ValorColuna { get; set; }
    public int IdColunaTabela { get; set; }
    public int IdModTabela { get; set; }


    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ValorColuna"], DBNull.Value)))
            this.ValorColuna = pobjIDataReader["ValorColuna"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["IdModTabela"], DBNull.Value)))
            this.IdModTabela = Convert.ToInt32(pobjIDataReader["IdModTabela"]);

        if ((!object.ReferenceEquals(pobjIDataReader["IdColunaTabela"], DBNull.Value)))
            this.IdColunaTabela = Convert.ToInt32(pobjIDataReader["IdColunaTabela"]);


    }

    #endregion
}