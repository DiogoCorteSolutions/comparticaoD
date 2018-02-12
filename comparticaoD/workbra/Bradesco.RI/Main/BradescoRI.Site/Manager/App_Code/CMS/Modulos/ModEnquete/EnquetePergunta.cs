using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

[Serializable()]
/// <summary>
/// Summary description for EnquetePergunta
/// </summary>
public class EnquetePergunta
{
    #region Propriedades
    public virtual int IdEnquetePergunta { get; set; }
    public virtual int IdEnquete { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Pergunta { get; set; }
    public virtual string Resposta1 { get; set; }
    public virtual string Resposta2 { get; set; }
    public virtual string Resposta3 { get; set; }
    public virtual string Resposta4 { get; set; }
    public virtual string Resposta5 { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["enquetePerguntaId"], DBNull.Value)))
        {
            this.IdEnquetePergunta = Convert.ToInt32(pobjIDataReader["enquetePerguntaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["enqueteId"], DBNull.Value)))
        {
            this.IdEnquete = Convert.ToInt32(pobjIDataReader["enqueteId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["pergunta"], DBNull.Value)))
        {
            this.Pergunta = pobjIDataReader["pergunta"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta1"], DBNull.Value)))
        {
            this.Resposta1 = pobjIDataReader["resposta1"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta2"], DBNull.Value)))
        {
            this.Resposta2 = pobjIDataReader["resposta2"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta3"], DBNull.Value)))
        {
            this.Resposta3 = pobjIDataReader["resposta3"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta4"], DBNull.Value)))
        {
            this.Resposta4 = pobjIDataReader["resposta4"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta5"], DBNull.Value)))
        {
            this.Resposta5 = pobjIDataReader["resposta5"].ToString();
        }
    }

    #endregion
}