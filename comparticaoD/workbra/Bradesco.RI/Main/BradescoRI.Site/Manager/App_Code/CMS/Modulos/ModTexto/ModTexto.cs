using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModTexto
/// </summary>
[Serializable()]
public class ModTexto
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Conteudo { get; set; }
    public virtual DateTime Data { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto"], DBNull.Value)))
        {
            this.Conteudo = pobjIDataReader["texto"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);
        }
    }

    #endregion
}