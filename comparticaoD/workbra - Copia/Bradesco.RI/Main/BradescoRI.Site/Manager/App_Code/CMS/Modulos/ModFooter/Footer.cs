using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

[Serializable()]
/// <summary>
/// Summary description for Footer
/// </summary>
public class Footer
{
    #region Propriedades
    public virtual int Id { get; set; }
    public virtual string TextoCentral { get; set; }
    public virtual string TituloN1 { get; set; }
    public virtual string TelefoneN1 { get; set; }
    public virtual string TextoN1 { get; set; }
    public virtual string TituloN2 { get; set; }
    public virtual string TelefoneN2 { get; set; }
    public virtual string TextoN2 { get; set; }
    public virtual string TituloN3 { get; set; }
    public virtual string TelefoneN3 { get; set; }
    public virtual string TextoN3 { get; set; }
    public virtual string TituloLinkN1 { get; set; }
    public virtual string UrlLinkN1 { get; set; }
    public virtual string TituloLinkN2 { get; set; }
    public virtual string UrlLinkN2 { get; set; }
    public virtual string TituloLinkN3 { get; set; }
    public virtual string UrlLinkN3 { get; set; }
    public virtual string TituloLinkN4 { get; set; }
    public virtual string UrlLinkN4 { get; set; }
    public virtual string TituloLinkN5 { get; set; }
    public virtual string UrlLinkN5 { get; set; }
    public virtual DateTime DataCadastro { get; set; }
    public virtual DateTime DataAtualizacao { get; set; }
    public virtual int UsuarioCadastroId { get; set; }
    public virtual int UsuarioAtualizacaoId { get; set; }
    public virtual int StatusId { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
            return;

        if ((!object.ReferenceEquals(pobjIDataReader["FooterId"], DBNull.Value)))
            this.Id = Convert.ToInt32(pobjIDataReader["FooterId"]);

        if ((!object.ReferenceEquals(pobjIDataReader["TextoCentral"], DBNull.Value)))
            this.TextoCentral = pobjIDataReader["TextoCentral"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloN1"], DBNull.Value)))
            this.TituloN1 = pobjIDataReader["TituloN1"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TelefoneN1"], DBNull.Value)))
            this.TelefoneN1 = pobjIDataReader["TelefoneN1"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TextoN1"], DBNull.Value)))
            this.TextoN1 = pobjIDataReader["TextoN1"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloN2"], DBNull.Value)))
            this.TituloN2 = pobjIDataReader["TituloN2"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TelefoneN2"], DBNull.Value)))
            this.TelefoneN2 = pobjIDataReader["TelefoneN2"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TextoN2"], DBNull.Value)))
            this.TextoN2 = pobjIDataReader["TextoN2"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloN3"], DBNull.Value)))
            this.TituloN3 = pobjIDataReader["TituloN3"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TelefoneN3"], DBNull.Value)))
            this.TelefoneN3 = pobjIDataReader["TelefoneN3"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TextoN3"], DBNull.Value)))
            this.TextoN3 = pobjIDataReader["TextoN3"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloLinkN1"], DBNull.Value)))
            this.TituloLinkN1 = pobjIDataReader["TituloLinkN1"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["UrlLinkN1"], DBNull.Value)))
            this.UrlLinkN1 = pobjIDataReader["UrlLinkN1"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloLinkN2"], DBNull.Value)))
            this.TituloLinkN2 = pobjIDataReader["TituloLinkN2"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["UrlLinkN2"], DBNull.Value)))
            this.UrlLinkN2 = pobjIDataReader["UrlLinkN2"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloLinkN3"], DBNull.Value)))
            this.TituloLinkN3 = pobjIDataReader["TituloLinkN3"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["UrlLinkN3"], DBNull.Value)))
            this.UrlLinkN3 = pobjIDataReader["UrlLinkN3"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloLinkN4"], DBNull.Value)))
            this.TituloLinkN4 = pobjIDataReader["TituloLinkN4"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["UrlLinkN4"], DBNull.Value)))
            this.UrlLinkN4 = pobjIDataReader["UrlLinkN4"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["TituloLinkN5"], DBNull.Value)))
            this.TituloLinkN5 = pobjIDataReader["TituloLinkN5"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["UrlLinkN5"], DBNull.Value)))
            this.UrlLinkN5 = pobjIDataReader["UrlLinkN5"].ToString();

        if ((!object.ReferenceEquals(pobjIDataReader["DataCadastro"], DBNull.Value)))
            this.DataCadastro = Convert.ToDateTime(pobjIDataReader["DataCadastro"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["DataAtualizacao"], DBNull.Value)))
            this.DataAtualizacao = Convert.ToDateTime(pobjIDataReader["DataAtualizacao"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioCadastroId"], DBNull.Value)))
            this.UsuarioCadastroId = Convert.ToInt32(pobjIDataReader["UsuarioCadastroId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["UsuarioAtualizacaoId"], DBNull.Value)))
            this.UsuarioAtualizacaoId = Convert.ToInt32(pobjIDataReader["UsuarioAtualizacaoId"].ToString());

        if ((!object.ReferenceEquals(pobjIDataReader["StatusId"], DBNull.Value)))
            this.StatusId = Convert.ToInt32(pobjIDataReader["StatusId"].ToString());
    }
    #endregion
}