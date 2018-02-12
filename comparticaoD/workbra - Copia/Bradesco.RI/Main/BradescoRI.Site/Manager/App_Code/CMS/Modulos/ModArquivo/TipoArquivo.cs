using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for TipoArquivo
/// </summary>
public class TipoArquivo
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual string Descricao { get; set; }
    public virtual Boolean? Relatorio { get; set; }
    public virtual Boolean? Comunicado { get; set; }
    public virtual Boolean? Noticia { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["tipoArquivoId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["TipoArquivoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["descricao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["relatorio"], DBNull.Value)))
            this.Relatorio = Convert.ToBoolean(pobjIDataReader["relatorio"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["comunicado"], DBNull.Value)))
            this.Comunicado = Convert.ToBoolean(pobjIDataReader["comunicado"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["noticia"], DBNull.Value)))
            this.Noticia = Convert.ToBoolean(pobjIDataReader["noticia"].ToString());

    }
    #endregion

}