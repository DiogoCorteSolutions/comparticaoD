using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
public class Pais
{
    #region Propriedades
    public int Id { get; set; }
    public int IdIdioma { get; set; }
    public string Sigla { get; set; }
    public string Sigla2 { get; set; }
    public string Nome{ get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["paisId"], DBNull.Value)))
        {
            this.Id = Convert.ToInt32(pobjIDataReader["paisId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["sigla"], DBNull.Value)))
        {
            this.Sigla = pobjIDataReader["sigla"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["sigla2"], DBNull.Value)))
        {
            this.Sigla2 = pobjIDataReader["sigla2"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nome"].ToString();
        }
    }

    #endregion
}