using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Lista
/// </summary>
[Serializable()]
public class Lista
{

    #region Propriedades
    public string NomeColuna { get; set; }
    public int IdColunaTabela { get; set; }



    #endregion
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["NomeColuna"], DBNull.Value)))
            this.NomeColuna = pobjIDataReader["NomeColuna"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["IdColunaTabela"], DBNull.Value)))
            this.IdColunaTabela = Convert.ToInt32(pobjIDataReader["IdColunaTabela"]);


    }


}