using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for ModProxEventos
/// </summary>
public class Evento
{
    #region Propriedades
    public int IdEvento { get; set; }
    public int IdIdioma { get; set; }
    public int IdTipoEvento { get; set; }
    public string TipoEvento { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Texto { get; set; }
    public string Responsavel { get; set; }
    public string Local { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Arquivo { get; set; }
    public string Cidade { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["eventoId"], DBNull.Value)))
        {
            this.IdEvento = Convert.ToInt32(pobjIDataReader["eventoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["tipoEventoId"], DBNull.Value)))
        {
            this.IdTipoEvento = Convert.ToInt32(pobjIDataReader["tipoEventoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["tipoEvento"], DBNull.Value)))
        {
            this.TipoEvento = pobjIDataReader["tipoEvento"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto"], DBNull.Value)))
        {
            this.Texto = pobjIDataReader["texto"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["responsavel"], DBNull.Value)))
        {
            this.Responsavel = pobjIDataReader["responsavel"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["local"], DBNull.Value)))
        {
            this.Local = pobjIDataReader["local"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataInicio"], DBNull.Value)))
        {
            this.DataInicio = Convert.ToDateTime(pobjIDataReader["dataInicio"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataFim"], DBNull.Value)))
        {
            this.DataFim = Convert.ToDateTime(pobjIDataReader["dataFim"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["arquivo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["cidade"], DBNull.Value)))
        {
            this.Cidade = pobjIDataReader["cidade"].ToString();
        }
    }

    #endregion
}