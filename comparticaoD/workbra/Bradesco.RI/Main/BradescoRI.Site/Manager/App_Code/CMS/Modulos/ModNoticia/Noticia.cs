using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModNoticia
/// </summary>
[Serializable()]
public class Noticia
{

    #region Propriedades
    public virtual int ID { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual TipoArquivo TipoArquivo { get; set; }
    public virtual TipoNoticia TipoNoticia { get; set; }
    public virtual int? NoticiaLayoutId { get; set; }
    public virtual DateTime DataNoticia { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Resumo { get; set; }
    public virtual string Integra { get; set; }
    public virtual string Fonte { get; set; }
    public DateTime DataInclusao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public bool Destaque { get; set; }
    public Usuario Usuario { get; set; }
    public int? StatusId { get; set; }
    public Arquivos Capa { get; set; }
    public Arquivos Detalhe { get; set; }
    public Arquivos Listagem { get; set; }
    public List<ArquivoNoticia> Arquivos { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["NoticiaId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["NoticiaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["IdiomaId"], DBNull.Value)))
            this.IdiomaId = int.Parse(pobjIDataReader["IdiomaId"].ToString());

        //if ((!object.ReferenceEquals(pobjIDataReader["TipoArquivoId"], DBNull.Value)))
        //    this.TipoArquivo = new TipoArquivo { Id = Convert.ToInt32(pobjIDataReader["TipoArquivoId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["TipoNoticiaId"], DBNull.Value)))
            this.TipoArquivo = new TipoArquivo() { Id = Convert.ToInt32(pobjIDataReader["TipoNoticiaId"].ToString()) };
    
        if ((!object.ReferenceEquals(pobjIDataReader["DataNoticia"], DBNull.Value)))
            this.DataNoticia = DateTime.Parse(pobjIDataReader["DataNoticia"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["Titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["Titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Resumo"], DBNull.Value)))
            this.Resumo = pobjIDataReader["Resumo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Integra"], DBNull.Value)))
            this.Integra = pobjIDataReader["Integra"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["Fonte"], DBNull.Value)))
            this.Fonte = pobjIDataReader["Fonte"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["DataInclusao"], DBNull.Value)))
            this.DataInclusao = DateTime.Parse(pobjIDataReader["DataInclusao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = DateTime.Parse(pobjIDataReader["DataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["Destaque"], DBNull.Value)))
            this.Destaque = bool.Parse(pobjIDataReader["Destaque"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioId"], DBNull.Value)))
            this.Usuario = new Usuario() { Id = int.Parse(pobjIDataReader["UsuarioId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["StatusId"], DBNull.Value)))
            this.StatusId = int.Parse(pobjIDataReader["StatusId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoCapaId"], DBNull.Value)))
            this.Capa = new Arquivos() { Id = Convert.ToInt32(pobjIDataReader["ArquivoCapaId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["SubTipoNoticiaId"], DBNull.Value)))
            this.TipoNoticia = new TipoNoticia() { ID = Convert.ToInt32(pobjIDataReader["SubTipoNoticiaId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoDetalheId"], DBNull.Value)))
            this.Detalhe = new Arquivos() { Id = Convert.ToInt32(pobjIDataReader["ArquivoDetalheId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoListagemId"], DBNull.Value)))
            this.Listagem = new Arquivos() { Id = Convert.ToInt32(pobjIDataReader["ArquivoListagemId"].ToString()) };

    }

    #endregion

}