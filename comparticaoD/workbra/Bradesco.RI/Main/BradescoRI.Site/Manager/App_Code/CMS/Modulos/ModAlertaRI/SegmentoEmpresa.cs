using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
public class SegmentoEmpresa
{
    #region Propriedades
    public int Id { get; set; }
    public int IdIdioma { get; set; }
    public string Nome { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["segmentoEmpresaId"], DBNull.Value)))
        {
            this.Id = Convert.ToInt32(pobjIDataReader["segmentoEmpresaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nome"].ToString();
        }
    }

    #endregion
}