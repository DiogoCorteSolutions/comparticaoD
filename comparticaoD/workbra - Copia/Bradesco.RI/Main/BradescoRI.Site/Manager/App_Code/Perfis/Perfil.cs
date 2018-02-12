
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

[Serializable()]
public class Perfil
{
    #region Propriedades

    public virtual int Id { get; set; }
    public virtual string Nome { get; set; }
    
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["perfilId"], DBNull.Value)))
        {
            this.Id = Convert.ToInt32(pobjIDataReader["perfilId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["nomePerfil"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nomePerfil"].ToString();
        }
    }

    #endregion
}