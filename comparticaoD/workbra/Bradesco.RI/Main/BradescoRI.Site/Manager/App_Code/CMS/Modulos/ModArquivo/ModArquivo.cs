using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for modArquivo
/// </summary>
public class ModArquivo
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual int ConteudoId { get; set; }
    public virtual int IdiomaId { get; set; }
    public virtual int TipoLayoutId { get; set; }
    public virtual string Titulo { get; set; }
    public virtual bool ShowTitulo { get; set; }
    public virtual bool ShowFiltro { get; set; }
    public virtual DateTime Data { get; set; }

    public virtual List<Arquivos> Arquivos { get; set; }

    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["conteudoId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
            this.IdiomaId = Convert.ToInt32(pobjIDataReader["idiomaId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["tipoLayoutId"], DBNull.Value)))
            this.TipoLayoutId = Convert.ToInt32(pobjIDataReader["tipoLayoutId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
            this.Titulo = pobjIDataReader["titulo"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["showTitulo"], DBNull.Value)))
            this.ShowTitulo = Convert.ToBoolean(pobjIDataReader["showTitulo"]);

        if ((!object.ReferenceEquals(pobjIDataReader["showFiltro"], DBNull.Value)))
            this.ShowFiltro = Convert.ToBoolean(pobjIDataReader["showFiltro"]);

        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
            this.Data = Convert.ToDateTime(pobjIDataReader["data"].ToString());


    }
    #endregion

}