using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for ModRelatorio
/// </summary>
public class ModRelatorio
{
    #region Propriedades
    public virtual ConteudoPagina Conteudo { get; set; }
    public virtual TipoRelatorio TipoRelatorio { get; set; }
    public virtual Relatorio Relatorio { get; set; }
    public virtual DateTime Data { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ConteudoId"], DBNull.Value)))
            this.Conteudo = new ConteudoPagina() { ConteudoId = Convert.ToInt32(pobjIDataReader["ConteudoId"]) };

        if ((!object.ReferenceEquals(pobjIDataReader["TipoRelatorioId"], DBNull.Value)))
            this.TipoRelatorio = new TipoRelatorio() { ID = Convert.ToInt32(pobjIDataReader["TipoRelatorioId"]) };

        if ((!object.ReferenceEquals(pobjIDataReader["Data"], DBNull.Value)))
            this.Data = Convert.ToDateTime(pobjIDataReader["Data"].ToString());

    }

    #endregion
}