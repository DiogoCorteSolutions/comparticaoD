using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ModAlerta
/// </summary>
[Serializable()]
public class ModAlerta
{
    #region Propriedades
    public int Id { get; set; }
    public int IdSegmentoEmpresa { get; set; }
    public string NomeSegmento { get; set; }
    public int IdPais { get; set; }
    public string NomePais { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime Data { get; set; }
    public string Empresa { get; set; }
    public string TelefoneDDD { get; set; }
    public string Telefone { get; set; }
    public string Estado { get; set; }
    public Boolean ProfissionalMercado { get; set; }
    public int IdIdiomaMailing { get; set; }
    public Boolean ReceberMailing { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["alertaId"], DBNull.Value)))
        {
            this.Id = Convert.ToInt32(pobjIDataReader["alertaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.Nome = Convert.ToString(pobjIDataReader["nome"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["email"], DBNull.Value)))
        {
            this.Email = pobjIDataReader["email"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["dataCadastro"], DBNull.Value)))
        {
            this.Data = Convert.ToDateTime(pobjIDataReader["dataCadastro"].ToString());
        }
        if ((!object.ReferenceEquals(pobjIDataReader["empresa"], DBNull.Value)))
        {
            this.Empresa = pobjIDataReader["empresa"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["telefoneDDD"], DBNull.Value)))
        {
            this.TelefoneDDD = pobjIDataReader["telefoneDDD"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["telefone"], DBNull.Value)))
        {
            this.Telefone = pobjIDataReader["telefone"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["segmentoEmpresaId"], DBNull.Value)))
        {
            this.IdSegmentoEmpresa = Convert.ToInt32(pobjIDataReader["segmentoEmpresaId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["NomeSegmento"], DBNull.Value)))
        {
            this.NomeSegmento = Convert.ToString(pobjIDataReader["NomeSegmento"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["estado"], DBNull.Value)))
        {
            this.Estado = pobjIDataReader["estado"].ToString();
        }
        if ((!object.ReferenceEquals(pobjIDataReader["paisId"], DBNull.Value)))
        {
            this.IdPais = Convert.ToInt32(pobjIDataReader["paisId"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["NomePais"], DBNull.Value)))
        {
            this.NomePais = Convert.ToString(pobjIDataReader["NomePais"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["profissionalMercado"], DBNull.Value)))
        {
            this.ProfissionalMercado = Convert.ToBoolean(pobjIDataReader["profissionalMercado"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["idiomaMailing"], DBNull.Value)))
        {
            this.IdIdiomaMailing = Convert.ToInt32(pobjIDataReader["idiomaMailing"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["receberMailing"], DBNull.Value)))
        {
            this.ReceberMailing = Convert.ToBoolean(pobjIDataReader["receberMailing"]);
        }
    }

    #endregion
}