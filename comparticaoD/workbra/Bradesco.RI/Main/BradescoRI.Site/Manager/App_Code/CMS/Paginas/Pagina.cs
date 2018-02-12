using System;
using System.Data;

/// <summary>
/// Classe com as propriedades da tabela TB_PAGINA
/// </summary>

[Serializable()]
public class Pagina
{
    #region Propriedades
    public virtual int PaginaId { get; set; }
    public virtual int TemplateId { get; set; }
    public virtual int CategoriaId { get; set; }
    public virtual string Alias { get; set; }
    public virtual string Titulo { get; set; }
    public virtual string Descricao { get; set; }
    public virtual int Status { get; set; }
    public virtual string MetatagsKeywords { get; set; }
    public virtual string MetatagsDescription { get; set; }
    public virtual string CorMenu { get; set; }
    public virtual DateTime DataCriacao { get; set; }
    public virtual DateTime DataPublicacao { get; set; }
    public virtual string DataPublicacaoString { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual string Caminho
    {
        get
        {
            //TODO: ALterar montagem de caminho quando finalizar publicação de página
            return String.Concat("/Manager/Paginas/", string.Format("{0}_{1}.aspx", PaginaId, Alias));
        }
    }

    public virtual string Categoria { get; set; }

    public virtual string CriadoPor { get; set; }

    public virtual string StatusDescricao { get; set; }

    public virtual string PublicadoPor { get; set; }

    public virtual string ArquivoTemplate { get; set; }

    public virtual string IdTemplateArquivo { get; set; }

    public virtual string ConfiguracaoAbas { get; set; }

    public virtual int QuantidadeAbas { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["paginaId"], DBNull.Value)))
        {
            this.PaginaId = Convert.ToInt32(pobjIDataReader["paginaId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["categoriaId"], DBNull.Value)))
        {
            this.CategoriaId = Convert.ToInt32(pobjIDataReader["categoriaId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["templateId"], DBNull.Value)))
        {
            this.TemplateId = Convert.ToInt32(pobjIDataReader["templateId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["alias"], DBNull.Value)))
        {
            this.Alias = pobjIDataReader["alias"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["titulo"], DBNull.Value)))
        {
            this.Titulo = pobjIDataReader["titulo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["status"], DBNull.Value)))
        {
            this.Status = Convert.ToInt32(pobjIDataReader["status"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["MetatagsKeywords"], DBNull.Value)))
        {
            this.MetatagsKeywords = pobjIDataReader["MetatagsKeywords"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["MetatagsDescription"], DBNull.Value)))
        {
            this.MetatagsDescription = pobjIDataReader["MetatagsDescription"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["corMenu"], DBNull.Value)))
        {
            this.CorMenu = pobjIDataReader["corMenu"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["dataCriacao"], DBNull.Value)))
        {
            this.DataCriacao = Convert.ToDateTime(pobjIDataReader["dataCriacao"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["dataPublicacao"], DBNull.Value)))
        {
            this.DataPublicacao = Convert.ToDateTime(pobjIDataReader["dataPublicacao"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["usuarioId"], DBNull.Value)))
        {
            this.Usuario = new Usuario() { Id = Convert.ToInt32(pobjIDataReader["usuarioId"]) };
        }

        if ((!object.ReferenceEquals(pobjIDataReader["Categoria"], DBNull.Value)))
        {
            this.Categoria = pobjIDataReader["Categoria"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["CriadoPor"], DBNull.Value)))
        {
            this.CriadoPor = pobjIDataReader["CriadoPor"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["StatusDescricao"], DBNull.Value)))
        {
            this.StatusDescricao = pobjIDataReader["StatusDescricao"].ToString();
        }
      
        if ((!object.ReferenceEquals(pobjIDataReader["PublicadoPor"], DBNull.Value)))
        {
            this.PublicadoPor = pobjIDataReader["PublicadoPor"].ToString();
        }

        if (DataPublicacao > DateTime.MinValue)
        {
            DataPublicacaoString = DataPublicacao.ToString(); 
        }

        if ((!object.ReferenceEquals(pobjIDataReader["ArquivoTemplate"], DBNull.Value)))
        {
            this.ArquivoTemplate = pobjIDataReader["ArquivoTemplate"].ToString();
        }
        
        IdTemplateArquivo = string.Concat(TemplateId.ToString(), "|", ArquivoTemplate);

        if ((!object.ReferenceEquals(pobjIDataReader["ConfiguracaoAbas"], DBNull.Value)))
        {
            this.ConfiguracaoAbas = pobjIDataReader["ConfiguracaoAbas"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["QuantidadeAba"], DBNull.Value)))
        {
            this.QuantidadeAbas = Convert.ToInt32(pobjIDataReader["QuantidadeAba"]);
        }
    }
    #endregion
}