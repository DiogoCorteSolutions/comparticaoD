using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for modArquivoItem
/// </summary>
public class ModArquivoItem
{
    #region propriedades
    public virtual int ConteudoId { get; set; }
    public virtual int ArquivoId { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ModuloArquivoId"], DBNull.Value)))
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["ConteudoId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoId"], DBNull.Value)))
            this.ArquivoId = Convert.ToInt32(pobjIDataReader["ArquivoId"].ToString());


    }
    #endregion
}