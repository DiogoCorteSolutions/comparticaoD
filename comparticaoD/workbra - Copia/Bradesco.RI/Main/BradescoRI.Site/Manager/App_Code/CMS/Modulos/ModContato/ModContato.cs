using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModTexto
/// </summary>
[Serializable()]
public class ModContato
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string AssuntoEmail { get; set; }
    public virtual string Assuntos { get; set; }
    public virtual string EmailTo { get; set; }
    public virtual string EmailToCc { get; set; }
    public virtual string EmailToCco { get; set; }
    public virtual DateTime Data { get; set; }
    public virtual string ConteudoTemplate { get; set; }
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
        if ((!object.ReferenceEquals(pobjIDataReader["assuntoEmail"], DBNull.Value)))
        {
            this.AssuntoEmail = pobjIDataReader["assuntoEmail"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["assuntos"], DBNull.Value)))
        {
            this.Assuntos = pobjIDataReader["assuntos"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["emailTo"], DBNull.Value)))
        {
            this.EmailTo = pobjIDataReader["emailTo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["emailCc"], DBNull.Value)))
        {
            this.EmailToCc = pobjIDataReader["emailCc"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["emailCco"], DBNull.Value)))
        {
            this.EmailToCco = pobjIDataReader["emailCco"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["conteudo"], DBNull.Value)))
        {
            this.ConteudoTemplate = pobjIDataReader["conteudo"].ToString();
        }
    }

    #endregion
}