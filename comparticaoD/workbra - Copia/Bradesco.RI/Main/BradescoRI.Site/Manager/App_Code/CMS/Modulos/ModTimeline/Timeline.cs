using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Timeline
/// </summary>
[Serializable()]
public class Timeline
{

    #region Propriedades
    public virtual int Id { get; set; }
    public virtual int TimelineId { get; set; }
    public virtual int Idioma { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Texto { get; set; }
    public virtual string Imagem { get; set; }
    public virtual int Ano { get; set; }
    public virtual int statusId { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["conteudoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["timelineId"], DBNull.Value)))
            this.TimelineId = Convert.ToInt32(pobjIDataReader["timelineId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
            this.Idioma = int.Parse(pobjIDataReader["idiomaId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["texto"], DBNull.Value)))
            this.Texto = pobjIDataReader["texto"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["ano"], DBNull.Value)))
            this.Ano = Convert.ToInt32(pobjIDataReader["ano"]);

        if ((!object.ReferenceEquals(pobjIDataReader["statusId"], DBNull.Value)))
            this.statusId = Convert.ToInt32(pobjIDataReader["statusId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["imagem"], DBNull.Value)))
            this.Imagem = pobjIDataReader["imagem"].ToString();

    }

    #endregion

}