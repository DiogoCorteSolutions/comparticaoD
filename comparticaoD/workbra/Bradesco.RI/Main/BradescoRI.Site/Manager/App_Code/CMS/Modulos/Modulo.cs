using System;
using System.Data;

/// <summary>
/// Classe com as propriedades da tabela TB_MODULO
/// </summary>

[Serializable()]
public class Modulo
{
    #region Propriedades
    public virtual int ModuloId { get; set; }
    public virtual string Nome { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual Boolean  Dinamico { get; set; }

    public virtual string IdModuloArquivo { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["ModuloId"], DBNull.Value)))
        {
            this.ModuloId = Convert.ToInt32(pobjIDataReader["ModuloId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["Nome"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["Nome"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["Arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["Arquivo"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["Dinamico"], DBNull.Value)))
        {
            this.Dinamico = Convert.ToBoolean(pobjIDataReader["Dinamico"]);
        }
        
        this.IdModuloArquivo = string.Concat(this.ModuloId.ToString(), "|", this.Arquivo); 
    }

    #endregion
}