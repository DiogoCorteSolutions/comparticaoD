using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Comunicado
/// </summary>
public class Comunicado
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual TipoNoticia TipoComunicado { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }
    public virtual DateTime DataComunicado { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime? DataAtualizacao { get; set; }
    public virtual Usuario UsuarioCadastro { get; set; }
    public virtual Usuario UsuarioAtualizacao { get; set; }
    public virtual int StatusId { get; set; }
    public virtual List<Arquivos> Arquivos { get; set; }

    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ComunicadoId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["ComunicadoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["IdiomaId"], DBNull.Value)))
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["IdiomaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["TipoComunicadoId"], DBNull.Value)))
            this.TipoComunicado = new TipoNoticia() { ID = Convert.ToInt32(pobjIDataReader["TipoComunicadoId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["Titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["Titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["Descricao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["DataComunicado"], DBNull.Value)))
            this.DataComunicado = Convert.ToDateTime(pobjIDataReader["DataComunicado"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["DataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = Convert.ToDateTime(pobjIDataReader["DataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["StatusId"], DBNull.Value)))
            this.StatusId = Convert.ToInt32(pobjIDataReader["StatusId"].ToString());
    }


    #endregion
}