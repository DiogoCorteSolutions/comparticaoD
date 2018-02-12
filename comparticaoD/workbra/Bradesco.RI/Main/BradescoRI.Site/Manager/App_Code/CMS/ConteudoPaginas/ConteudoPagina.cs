using System;
using System.Data;

/// <summary>
/// Classe com as propriedades da tabela TB_CONTEUDO
/// </summary>

[Serializable()]
public class ConteudoPagina
{
    #region Propriedades
    public virtual int ConteudoId { get; set; }
    public virtual int PaginaId { get; set; }
    public virtual int ModuloId { get; set; }
    public virtual int Ordem { get; set; }
    public virtual string Arquivo { get; set; }
    public virtual int PosicaoTemplate { get; set; }
    public virtual string ArquivoTemplate { get; set; }
    public virtual Boolean ModuloDinamico { get; set; }
    public virtual int AccordionId { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["ConteudoId"], DBNull.Value)))
        {
            this.ConteudoId = Convert.ToInt32(pobjIDataReader["ConteudoId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["PaginaId"], DBNull.Value)))
        {
            this.PaginaId = Convert.ToInt32(pobjIDataReader["PaginaId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["ModuloId"], DBNull.Value)))
        {
            this.ModuloId = Convert.ToInt32(pobjIDataReader["ModuloId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Ordem"], DBNull.Value)))
        {
            this.Ordem = Convert.ToInt32(pobjIDataReader["Ordem"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Arquivo"], DBNull.Value)))
        {
            this.Arquivo = pobjIDataReader["Arquivo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["PosicaoTemplate"], DBNull.Value)))
        {
            this.PosicaoTemplate = Convert.ToInt32(pobjIDataReader["PosicaoTemplate"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoTemplate"], DBNull.Value)))
        {
            this.ArquivoTemplate = pobjIDataReader["ArquivoTemplate"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Dinamico"], DBNull.Value)))
        {
            this.ModuloDinamico = Convert.ToBoolean(pobjIDataReader["Dinamico"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["AccordionId"], DBNull.Value)))
        {
            this.AccordionId = Convert.ToInt32(pobjIDataReader["AccordionId"]);
        }
    }   
    #endregion
}