using System;
using System.Data;

[Serializable()]
/// <summary>
/// Classe para categorias relacionadas as paginas
/// </summary>
public class Categoria
{
    #region Propriedades
    public virtual int IdCategoria { get; set; }
    public virtual string Descricao { get; set; }

    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["categoriaId"], DBNull.Value)))
        {
            this.IdCategoria = Convert.ToInt32(pobjIDataReader["categoriaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["descricao"], DBNull.Value)))
        {
            this.Descricao = pobjIDataReader["descricao"].ToString();
        }
       
    }

    #endregion
}