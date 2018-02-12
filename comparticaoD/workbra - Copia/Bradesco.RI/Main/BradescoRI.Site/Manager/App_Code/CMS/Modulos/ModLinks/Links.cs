using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for Links
/// </summary>
public class Links
{
    #region Propriedades
    public virtual int IdLink { get; set; }
    public virtual int IdGrupo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Target { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Url { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["linkId"], DBNull.Value)))
        {
            this.IdLink = Convert.ToInt32(pobjIDataReader["linkId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["grupoId"], DBNull.Value)))
        {
            this.IdGrupo = Convert.ToInt32(pobjIDataReader["grupoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["target"], DBNull.Value)))
        {
            this.Target = pobjIDataReader["target"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["url"], DBNull.Value)))
        {
            this.Url = pobjIDataReader["url"].ToString();
        }
    }

    #endregion
}