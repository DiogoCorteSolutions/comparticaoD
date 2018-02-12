using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Security.Permissions;
using System.Collections;

/// <summary>
/// Summary description for Log
/// </summary>

[Serializable()]
public class Traducao
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual string IdiomaNome { get; set; }
    public virtual string ChaveNome { get; set; }
    public virtual string Texto { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["traducaoId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["traducaoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto"], DBNull.Value)))
        {
            this.Texto = pobjIDataReader["texto"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.IdiomaNome = pobjIDataReader["nome"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["chaveNome"], DBNull.Value)))
        {
            this.ChaveNome = pobjIDataReader["chaveNome"].ToString();
        }
    }

    #endregion
}