using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Arquivo
/// </summary>
public class Arquivo
{
    #region PROPRIEDADES
    public virtual int Id { get; set; }
    public virtual Relatorio Relatorio { get; set; }
    public virtual Comunicado Comunicado { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Path { get; set; }
    public virtual string Extensao { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime? DataAtualizacao { get; set; }
    public virtual Usuario UsuarioCadastro { get; set; }
    public virtual Usuario UsuarioAtualizacao { get; set; }
    public virtual int StatusId { get; set; }
    public virtual bool Deletar { get; set; }
    public virtual bool Inserir { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["ArquivoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["RelatorioId"], DBNull.Value)))
            this.Relatorio = new Relatorio() { ID = Convert.ToInt32(pobjIDataReader["RelatorioId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["ComunicadoId"], DBNull.Value)))
            this.Comunicado = new Comunicado() { ID = Convert.ToInt32(pobjIDataReader["ComunicadoId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["Nome"], DBNull.Value)))
            this.Nome = pobjIDataReader["Nome"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["PathFile"], DBNull.Value)))
            this.Path = pobjIDataReader["PathFile"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Extensao"], DBNull.Value)))
            this.Path = pobjIDataReader["Extensao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["DataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["DataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = Convert.ToDateTime(pobjIDataReader["DataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioCadastroId"], DBNull.Value)))
            this.UsuarioCadastro = new Usuario() { Id = Convert.ToInt32(pobjIDataReader["UsuarioCadastroId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioAtualizacaoId"], DBNull.Value)))
            this.UsuarioAtualizacao = new Usuario() { Id = Convert.ToInt32(pobjIDataReader["UsuarioAtualizacaoId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["StatusId"], DBNull.Value)))
            this.StatusId = Convert.ToInt32(pobjIDataReader["StatusId"].ToString());
    }
    #endregion
}