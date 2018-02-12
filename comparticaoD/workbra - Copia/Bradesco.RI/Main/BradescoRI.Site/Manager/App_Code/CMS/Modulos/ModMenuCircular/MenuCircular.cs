using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for MenuCircular
/// </summary>
public class MenuCircular
{
    #region Propriedades
    public virtual int IdMenuCircular { get; set; }
    public virtual int IdGrupo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual string Target { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Tooltip { get; set; }
    public virtual string Url { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["menuCircularId"], DBNull.Value)))
        {
            this.IdMenuCircular = Convert.ToInt32(pobjIDataReader["menuCircularId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["grupoId"], DBNull.Value)))
        {
            this.IdGrupo = Convert.ToInt32(pobjIDataReader["grupoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["arquivo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["target"], DBNull.Value)))
        {
            this.Target = pobjIDataReader["target"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }        
        if ((!object.ReferenceEquals(pobjIDataReader["tooltip"], DBNull.Value)))
        {
            this.Tooltip = pobjIDataReader["tooltip"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["url"], DBNull.Value)))
        {
            this.Url = pobjIDataReader["url"].ToString();
        }
    }

    #endregion
}