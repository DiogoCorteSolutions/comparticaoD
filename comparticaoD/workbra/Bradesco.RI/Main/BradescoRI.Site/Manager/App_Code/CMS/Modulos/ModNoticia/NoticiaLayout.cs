using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for NoticiaLayout
/// </summary>
public class NoticiaLayout
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Descricao { get; set; }
    public virtual string Tamanho { get; set; }
    public virtual string Altura { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["NoticiaLayoutId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["NoticiaLayoutId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["Nome"], DBNull.Value)))
            this.Descricao = pobjIDataReader["Nome"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Width"], DBNull.Value)))
            this.Tamanho = pobjIDataReader["Width"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Height"], DBNull.Value)))
            this.Altura = pobjIDataReader["Height"].ToString();

    }
    #endregion
}