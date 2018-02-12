using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ArquivoNoticia
/// </summary>
[Serializable()]
public class ArquivoNoticia
{
    #region Propriedades
    public virtual int ID { get; set; }
    public virtual Noticia Noticia { get; set; }
    public string Nome { get; set; }
    public virtual NoticiaLayout NoticiaLayoutId {get;set;}
    public virtual string PathArquivo { get; set; }
    public virtual bool Capa { get; set; }
    public virtual bool Lista { get; set; }
    public virtual bool Detalhe { get; set; }
    public virtual DateTime DataInclusao { get; set; }
    public virtual Usuario UsuarioInclusao { get; set; }
    public virtual int StatusId { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoNoticiaId"], DBNull.Value)))
            this.ID = Convert.ToInt32(pobjIDataReader["ArquivoNoticiaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["NomeArquivo"], DBNull.Value)))
            this.Nome = pobjIDataReader["NomeArquivo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["NoticiaId"], DBNull.Value)))
            this.Noticia = new Noticia() { ID = int.Parse(pobjIDataReader["NoticiaId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["NoticiaLayoutId"], DBNull.Value)))
            this.NoticiaLayoutId = new NoticiaLayout() { ID = Convert.ToInt32(pobjIDataReader["NoticiaLayoutId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["PathArquivo"], DBNull.Value)))
            this.PathArquivo = pobjIDataReader["PathArquivo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoCapa"], DBNull.Value)))
            this.Capa = bool.Parse(pobjIDataReader["ArquivoCapa"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoLista"], DBNull.Value)))
            this.Lista = bool.Parse(pobjIDataReader["ArquivoLista"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoDetalhe"], DBNull.Value)))
            this.Detalhe = bool.Parse(pobjIDataReader["ArquivoDetalhe"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataInclusao"], DBNull.Value)))
            this.DataInclusao = DateTime.Parse(pobjIDataReader["DataInclusao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioId"], DBNull.Value)))
            this.UsuarioInclusao = new Usuario() { Id = int.Parse(pobjIDataReader["UsuarioId"].ToString()) };

        if ((!object.ReferenceEquals(pobjIDataReader["StatusId"], DBNull.Value)))
            this.StatusId = int.Parse(pobjIDataReader["StatusId"].ToString());

    }
    #endregion
}