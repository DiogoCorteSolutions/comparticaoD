using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for TipoEvento
/// </summary>
public class TipoEvento
{
    #region Propriedades
    public virtual int IdTipoEvento { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Descricao { get; set; }
    public virtual string Cor { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["tipoEventoId"], DBNull.Value)))
        {
            this.IdTipoEvento = Convert.ToInt32(pobjIDataReader["tipoEventoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["cor"], DBNull.Value)))
        {
            this.Cor = pobjIDataReader["cor"].ToString();
        }
    }

    #endregion
}