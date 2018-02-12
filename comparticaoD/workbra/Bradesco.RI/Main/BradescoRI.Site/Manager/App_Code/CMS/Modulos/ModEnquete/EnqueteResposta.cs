using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for EnqueteResposta
/// </summary>
public class EnqueteResposta
{
    #region Propriedades
    public virtual string Pergunta { get; set; }
    public virtual int Total { get; set; }
    public virtual string Resposta1 { get; set; }
    public virtual int TotalResposta1 { get; set; }
    public virtual string Resposta2 { get; set; }
    public virtual int TotalResposta2 { get; set; }
    public virtual string Resposta3 { get; set; }
    public virtual int TotalResposta3 { get; set; }
    public virtual string Resposta4 { get; set; }
    public virtual int TotalResposta4 { get; set; }
    public virtual string Resposta5 { get; set; }
    public virtual int TotalResposta5 { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        
        if ((!object.ReferenceEquals(pobjIDataReader["pergunta"], DBNull.Value)))
        {
            this.Pergunta = pobjIDataReader["pergunta"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["total"], DBNull.Value)))
        {
            this.Total = Convert.ToInt32(pobjIDataReader["total"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta1"], DBNull.Value)))
        {
            this.Resposta1 = pobjIDataReader["resposta1"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["totalResposta1"], DBNull.Value)))
        {
            this.TotalResposta1 = Convert.ToInt32(pobjIDataReader["totalResposta1"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta2"], DBNull.Value)))
        {
            this.Resposta2 = pobjIDataReader["resposta2"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["totalResposta2"], DBNull.Value)))
        {
            this.TotalResposta2 = Convert.ToInt32(pobjIDataReader["totalResposta2"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta3"], DBNull.Value)))
        {
            this.Resposta3 = pobjIDataReader["resposta3"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["totalResposta3"], DBNull.Value)))
        {
            this.TotalResposta3 = Convert.ToInt32(pobjIDataReader["totalResposta3"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta4"], DBNull.Value)))
        {
            this.Resposta4 = pobjIDataReader["resposta4"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["totalResposta4"], DBNull.Value)))
        {
            this.TotalResposta4 = Convert.ToInt32(pobjIDataReader["totalResposta4"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["resposta5"], DBNull.Value)))
        {
            this.Resposta5 = pobjIDataReader["resposta5"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["totalResposta5"], DBNull.Value)))
        {
            this.TotalResposta5 = Convert.ToInt32(pobjIDataReader["totalResposta5"].ToString());
        }
    }

    #endregion
}