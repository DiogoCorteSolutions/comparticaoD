using System;
using System.Data;

[Serializable()]
/// <summary>
/// Classe para templates relacionadas as paginas
/// </summary>
public class Template
{
    #region Propriedades
    public virtual int TemplateId { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual int QuantidadeAba { get; set; }

    public virtual string IdArquivo { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["templateId"], DBNull.Value)))
        {
            this.TemplateId = Convert.ToInt32(pobjIDataReader["templateId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Nome"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["Nome"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["Arquivo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["QuantidadeAba"], DBNull.Value)))
        {
            this.QuantidadeAba = Convert.ToInt32(pobjIDataReader["QuantidadeAba"]);
        }

        IdArquivo = string.Concat(TemplateId.ToString(), "|", Arquivo); 
    }
    #endregion
}