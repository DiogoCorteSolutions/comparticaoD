using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tabela
/// </summary>
/// 
[Serializable()]
public class Tabela
{
    #region Propriedades
    //public int IdModTabela { get; set; }
    //public string EmMilhares { get; set; }
    //public string Marco2017 { get; set; }
    //public string Dezembro2017 { get; set; }
    //public string Marco2016 { get; set; }
    public string NomeColuna { get; set; }
    public string ValorColuna { get; set; }
    public int IdModTabela { get; set; }
    public int IdModTabela1 { get; set; }
    public string NomeAcionario { get; set; }



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

        if ((!object.ReferenceEquals(pobjIDataReader["NomeAcionario"], DBNull.Value)))
            this.NomeAcionario = pobjIDataReader["NomeAcionario"].ToString();


        if ((!object.ReferenceEquals(pobjIDataReader["IdModTabela"], DBNull.Value)))
            this.IdModTabela = Convert.ToInt32(pobjIDataReader["IdModTabela"]);

        //if ((!object.ReferenceEquals(pobjIDataReader["emMilhares"], DBNull.Value)))
        //    this.EmMilhares = pobjIDataReader["emMilhares"].ToString();

        //if ((!object.ReferenceEquals(pobjIDataReader["marco2017"], DBNull.Value)))
        //    this.Marco2017 = pobjIDataReader["marco2017"].ToString();

        //if ((!object.ReferenceEquals(pobjIDataReader["dezembro2017"], DBNull.Value)))
        //    this.Dezembro2017 = pobjIDataReader["dezembro2017"].ToString();

        //if ((!object.ReferenceEquals(pobjIDataReader["marco2016"], DBNull.Value)))
        //    this.Marco2016 = pobjIDataReader["marco2016"].ToString();

    }

    #endregion
}