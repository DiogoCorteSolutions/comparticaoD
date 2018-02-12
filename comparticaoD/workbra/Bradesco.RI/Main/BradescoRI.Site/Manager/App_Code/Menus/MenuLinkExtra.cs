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
public class MenuLinkExtra
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Url { get; set; }
    public virtual string Target { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual string ChaveNome { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["menuLinkId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["menuLinkId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nomeMenu"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nomeMenu"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["url"], DBNull.Value)))
        {
            this.Url = pobjIDataReader["url"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["target"], DBNull.Value)))
        {
            this.Target = pobjIDataReader["target"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["chaveNome"], DBNull.Value)))
        {
            this.ChaveNome = pobjIDataReader["chaveNome"].ToString();
        }
    }

    #endregion
}