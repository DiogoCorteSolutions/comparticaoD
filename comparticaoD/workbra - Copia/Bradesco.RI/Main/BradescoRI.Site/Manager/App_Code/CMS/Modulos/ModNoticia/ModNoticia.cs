using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for ModNoticia
/// </summary>
public class ModNoticia
{

    #region propriedades
    public virtual int ID { get; set; }
    public virtual int IdNoticia { get; set; }
    public virtual int TipoNoticiaId { get; set; }
    public virtual DateTime Data { get; set; }
    public virtual bool Home { get; set; }
    public virtual bool Listagem { get; set; }
    public virtual bool Destaque { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        
        if ((!object.ReferenceEquals(pobjIDataReader["noticiaId"], DBNull.Value)))
            this.IdNoticia = Convert.ToInt32(pobjIDataReader["noticiaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["tipoNoticiaId"], DBNull.Value)))
            this.TipoNoticiaId = Convert.ToInt32(pobjIDataReader["tipoNoticiaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
            this.Data = Convert.ToDateTime(pobjIDataReader["data"]);

        if ((!object.ReferenceEquals(pobjIDataReader["home"], DBNull.Value)))
            this.Home = Convert.ToBoolean(pobjIDataReader["home"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["listagem"], DBNull.Value)))
            this.Listagem = Convert.ToBoolean(pobjIDataReader["listagem"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["destaque"], DBNull.Value)))
            this.Destaque = Convert.ToBoolean(pobjIDataReader["destaque"].ToString());
    }
    #endregion
}
