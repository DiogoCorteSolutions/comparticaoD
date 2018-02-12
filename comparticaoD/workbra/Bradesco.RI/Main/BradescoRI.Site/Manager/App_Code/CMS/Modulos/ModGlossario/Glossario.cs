using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Glossario
/// </summary>
public class Glossario
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }
    public virtual Usuario UsuarioCadastro { get; set; }
    public virtual Usuario UsuarioAtualizacao { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime DataAtualizacao { get; set; }
    public virtual int StatusId { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["glossarioId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["glossarioId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["descricao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioCadastroId"], DBNull.Value)))
            this.UsuarioCadastro = new Usuario() { Id = Convert.ToInt32(pobjIDataReader["usuarioCadastroId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioAtualizacaoId"], DBNull.Value)))
            this.UsuarioAtualizacao = new Usuario() { Id = Convert.ToInt32(pobjIDataReader["usuarioAtualizacaoId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["dataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["dataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["dataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = Convert.ToDateTime(pobjIDataReader["dataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["statusId"], DBNull.Value)))
            this.StatusId = Convert.ToInt32(pobjIDataReader["statusId"].ToString());

    }
    #endregion
}