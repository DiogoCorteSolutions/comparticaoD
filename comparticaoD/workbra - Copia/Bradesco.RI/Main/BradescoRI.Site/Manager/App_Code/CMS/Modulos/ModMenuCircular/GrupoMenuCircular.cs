using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for GrupoMenuCircular
/// </summary>
public class GrupoMenuCircular
{
    #region Propriedades
    public virtual int IdGrupo { get; set; }
    public virtual string Descricao { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["grupoId"], DBNull.Value)))
        {
            this.IdGrupo = Convert.ToInt32(pobjIDataReader["grupoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }        
    }

    #endregion
}