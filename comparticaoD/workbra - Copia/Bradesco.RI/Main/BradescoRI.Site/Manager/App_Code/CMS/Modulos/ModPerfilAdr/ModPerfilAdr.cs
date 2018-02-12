using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModPerfilAdr
/// </summary>
[Serializable()]
public class ModPerfilAdr
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Valor { get; set; }
    public virtual int Ordem { get; set; }
    public virtual string NomeIdioma { get; set; }
    public virtual string SiglaIdioma { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["perfilAdrId"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["perfilAdrId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["valor"], DBNull.Value)))
        {
            this.Valor = pobjIDataReader["valor"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["ordem"], DBNull.Value)))
        {
            this.Ordem = Convert.ToInt32(pobjIDataReader["ordem"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.NomeIdioma = pobjIDataReader["nome"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["sigla"], DBNull.Value)))
        {
            this.SiglaIdioma = pobjIDataReader["sigla"].ToString();
        }
    }

    #endregion
}