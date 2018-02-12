using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for Banners
/// </summary>
public class Banners
{
    #region Propriedades
    public virtual int IdBanner { get; set; }
    public virtual int IdGrupo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual string Target { get; set; }
    public virtual string Texto1 { get; set; }
    public virtual string Texto2 { get; set; }
    public virtual string TextoUrl { get; set; }
    public virtual string Url { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["bannerId"], DBNull.Value)))
        {
            this.IdBanner = Convert.ToInt32(pobjIDataReader["bannerId"]);
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
        if ((!object.ReferenceEquals(pobjIDataReader["texto1"], DBNull.Value)))
        {
            this.Texto1 = pobjIDataReader["texto1"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto2"], DBNull.Value)))
        {
            this.Texto2 = pobjIDataReader["texto2"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["textoUrl"], DBNull.Value)))
        {
            this.TextoUrl = pobjIDataReader["textoUrl"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["url"], DBNull.Value)))
        {
            this.Url = pobjIDataReader["url"].ToString();
        }
    }

    #endregion
}