using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModFaleConosco
/// </summary>
[Serializable()]
public class ModFaleConosco
{
    #region Propriedades
    public virtual int IdConteudo { get; set; }
    public virtual int IdIdioma { get; set; }
    public virtual string Assunto{ get; set; }
    public virtual string Email { get; set; }
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
        if ((!object.ReferenceEquals(pobjIDataReader["assunto"], DBNull.Value)))
        {
            this.Assunto = pobjIDataReader["assunto"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["email"], DBNull.Value)))
        {
            this.Email = pobjIDataReader["email"].ToString();
        }
    }

    #endregion
}