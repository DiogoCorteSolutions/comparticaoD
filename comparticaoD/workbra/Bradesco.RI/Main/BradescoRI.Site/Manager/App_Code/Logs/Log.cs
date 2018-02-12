using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>

[Serializable()]
public class Log
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual string Mensagem { get; set; }
    public virtual DateTime Data_hora { get; set; }
    public virtual int Tipo { get; set; }
    public virtual string LoginUsuario { get; set; }
    public virtual int IdUsuario { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Id"], DBNull.Value)))
        {
            this.ID = Convert.ToInt32(pobjIDataReader["Id"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["MsgLog"], DBNull.Value)))
        {
            this.Mensagem = pobjIDataReader["MsgLog"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["DataLog"], DBNull.Value)))
        {
            this.Data_hora = Convert.ToDateTime(pobjIDataReader["DataLog"].ToString());
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Tipo"], DBNull.Value)))
        {
            this.Tipo = Convert.ToInt32(pobjIDataReader["Tipo"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["login"], DBNull.Value)))
        {
            this.LoginUsuario = pobjIDataReader["login"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioId"], DBNull.Value)))
        {
            this.IdUsuario = Convert.ToInt32(pobjIDataReader["usuarioId"].ToString());
        }
    }

    #endregion
}