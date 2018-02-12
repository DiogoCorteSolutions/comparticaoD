using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Classe com as propriedades da tabela TB_PAGINA_APROVACAO
/// </summary>

[Serializable()]
public class PaginaAprovacao
{
    #region Propriedades
    public virtual int AprovacaoId { get; set; }
    public virtual int PaginaId { get; set; }
    public virtual string TituloPagina { get; set; }
    public virtual string AliasPagina { get; set; }
    public virtual Boolean  HomePage { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual string NomeUsuario { get; set; }
    public virtual string Status { get; set; }
    public virtual string MetatagsKeywords { get; set; }
    public virtual string MetatagsDescription { get; set; }
    #endregion


    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["AprovacaoId"], DBNull.Value)))
        {
            this.AprovacaoId = Convert.ToInt32(pobjIDataReader["AprovacaoId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["PaginaId"], DBNull.Value)))
        {
            this.PaginaId = Convert.ToInt32(pobjIDataReader["PaginaId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Titulo"], DBNull.Value)))
        {
            this.TituloPagina = pobjIDataReader["Titulo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Alias"], DBNull.Value)))
        {
            this.AliasPagina = pobjIDataReader["Alias"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["HomePage"], DBNull.Value)))
        {
            this.HomePage = Convert.ToBoolean(pobjIDataReader["HomePage"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["DataCadastro"], DBNull.Value)))
        {
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["DataCadastro"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["NomeUsuario"], DBNull.Value)))
        {
            this.NomeUsuario = pobjIDataReader["NomeUsuario"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Status"], DBNull.Value)))
        {
            this.Status = pobjIDataReader["Status"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["MetatagsKeywords"], DBNull.Value)))
        {
            this.MetatagsKeywords = pobjIDataReader["MetatagsKeywords"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["MetatagsDescription"], DBNull.Value)))
        {
            this.MetatagsDescription = pobjIDataReader["MetatagsDescription"].ToString();
        }
    }

    #endregion
}