using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModImagemUnica
/// </summary>
[Serializable()]
public class ModImagemUnica
{
    #region Propriedades
    public virtual int IdConteudo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual string Texto1 { get; set; }
    public virtual string Texto2 { get; set; }
    public virtual string Texto3 { get; set; }
    public virtual string TextoUrl { get; set; }
    public virtual string Url { get; set; }
    public virtual string Target { get; set; }
    public virtual int Tamanho { get; set; }
    public virtual string Tooltip { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["conteudoId"], DBNull.Value)))
        {
            this.IdConteudo = Convert.ToInt32(pobjIDataReader["conteudoId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaId"], DBNull.Value)))
        {
            this.IdIdioma = Convert.ToInt32(pobjIDataReader["idiomaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["arquivo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["target"], DBNull.Value)))
        {
            this.Target = pobjIDataReader["target"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["tamanho"], DBNull.Value)))
        {
            this.Tamanho = Convert.ToInt32(pobjIDataReader["tamanho"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto1"], DBNull.Value)))
        {
            this.Texto1 = pobjIDataReader["texto1"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto2"], DBNull.Value)))
        {
            this.Texto2 = pobjIDataReader["texto2"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["texto3"], DBNull.Value)))
        {
            this.Texto3 = pobjIDataReader["texto3"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["textoUrl"], DBNull.Value)))
        {
            this.TextoUrl = pobjIDataReader["textoUrl"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["tooltip"], DBNull.Value)))
        {
            this.Tooltip = pobjIDataReader["tooltip"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["url"], DBNull.Value)))
        {
            this.Url = pobjIDataReader["url"].ToString();
        }

    }

    #endregion

    #region Enum

    public enum Tamanhos
    {
        TelaInteira = 1,
        Tam1 = 2,
        Tam2 = 3,
        Tam3 = 4
    }

    #endregion
}