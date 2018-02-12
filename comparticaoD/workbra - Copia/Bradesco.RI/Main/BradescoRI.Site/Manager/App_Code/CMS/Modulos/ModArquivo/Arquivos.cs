using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Arquivos
/// </summary>
public class Arquivos
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual int TipoArquivoId { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }
    public virtual string Caminho { get; set; }
    public virtual string Extensao { get; set; }
    public virtual string Tamanho { get; set; }
    public virtual DateTime DataArquivo { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime DataAtualizacao { get; set; }
    public virtual int UsuarioCadastroId { get; set; }
    public virtual int UsuarioAtualizacaoId { get; set; }
    public virtual bool Streaming { get; set; }
    public virtual int StatusId { get; set; }
    public virtual bool Inserir { get; set; }
    public virtual bool Deletar { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["arquivoId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["arquivoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["tipoArquivoId"], DBNull.Value)))
            this.TipoArquivoId = Convert.ToInt32(pobjIDataReader["tipoArquivoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
            this.Descricao = pobjIDataReader["descricao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["caminho"], DBNull.Value)))
            this.Caminho = pobjIDataReader["caminho"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["extensao"], DBNull.Value)))
            this.Extensao = pobjIDataReader["extensao"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["tamanho"], DBNull.Value)))
            this.Tamanho = pobjIDataReader["tamanho"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["dataArquivo"], DBNull.Value)))
            this.DataArquivo = Convert.ToDateTime(pobjIDataReader["dataArquivo"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["dataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["dataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["dataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = Convert.ToDateTime(pobjIDataReader["dataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["dataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["dataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioCadastroId"], DBNull.Value)))
            this.UsuarioCadastroId = Convert.ToInt32(pobjIDataReader["usuarioCadastroId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioAtualizacaoId"], DBNull.Value)))
            this.UsuarioAtualizacaoId = Convert.ToInt32(pobjIDataReader["usuarioAtualizacaoId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["streaming"], DBNull.Value)))
            this.Streaming = Convert.ToBoolean(pobjIDataReader["streaming"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["statusId"], DBNull.Value)))
            this.StatusId = Convert.ToInt32(pobjIDataReader["statusId"].ToString());
    }
    #endregion
}