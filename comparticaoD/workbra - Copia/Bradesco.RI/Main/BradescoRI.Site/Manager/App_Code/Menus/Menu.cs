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
public class Menu
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Url { get; set; }
    public virtual string Target { get; set; }
    public virtual string Hierarquia { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual string ChaveNome { get; set; }
    public virtual string CssClass { get; set; }
    public virtual List<Menu> ItensMenu { get; set; }

    public virtual string HierarquiaPai
    {
        get
        {
            if (Hierarquia.Length <= 3)
                return string.Empty;
            else
                return Hierarquia.Substring(0, Hierarquia.Length - 3);
        }
    }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["menuId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["menuId"]);
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
        if ((!object.ReferenceEquals(pobjIDataReader["hierarquia"], DBNull.Value)))
        {
            this.Hierarquia = pobjIDataReader["hierarquia"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["chaveNome"], DBNull.Value)))
        {
            this.ChaveNome = pobjIDataReader["chaveNome"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["cssClass"], DBNull.Value)))
        {
            this.CssClass = pobjIDataReader["cssClass"].ToString();
        }

    }

    #endregion
}