using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for TipoRelatorio
/// </summary>
public class TipoRelatorio
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }
    public virtual bool Imagem { get; set; }
    public virtual bool Lista { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["TipoRelatorioId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["TipoRelatorioId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["Descricao"], DBNull.Value)))
            this.Titulo = pobjIDataReader["Descricao"].ToString();
        
    }
    #endregion

}