using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for ProxEventos
/// </summary>
public class ProxEventos
{
    #region Propriedades
    public int IdEvento { get; set; }
   public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Cidade { get; set; }
    public string UrlListaEvento { get; set; }
    public string UrlTodosEventos { get; set; }
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
        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["urlListaEvento"], DBNull.Value)))
        {
            this.UrlListaEvento = pobjIDataReader["urlListaEvento"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["urlTodosEventos"], DBNull.Value)))
        {
            this.UrlTodosEventos = pobjIDataReader["urlTodosEventos"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataInicio"], DBNull.Value)))
        {
            this.DataInicio = Convert.ToDateTime(pobjIDataReader["dataInicio"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataFim"], DBNull.Value)))
        {
            this.DataFim = Convert.ToDateTime(pobjIDataReader["dataFim"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["cidade"], DBNull.Value)))
        {
            this.Cidade = pobjIDataReader["cidade"].ToString();
        }

    }

    #endregion
}