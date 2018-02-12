using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Idioma
/// </summary>
public class Idioma
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Sigla { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nome"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["sigla"], DBNull.Value)))
        {
            this.Sigla = pobjIDataReader["sigla"].ToString();
        }
    }

    #endregion
}