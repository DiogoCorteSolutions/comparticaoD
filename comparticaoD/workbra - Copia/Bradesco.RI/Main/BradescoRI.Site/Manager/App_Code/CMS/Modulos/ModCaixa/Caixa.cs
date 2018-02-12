using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Caixa
/// </summary>
public class Caixa
{
    #region Propriedades
    public virtual int IdCaixa { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }    
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["caixaId"], DBNull.Value)))
        {
            this.IdCaixa = Convert.ToInt32(pobjIDataReader["caixaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["arquivo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }        
    }

    #endregion
}