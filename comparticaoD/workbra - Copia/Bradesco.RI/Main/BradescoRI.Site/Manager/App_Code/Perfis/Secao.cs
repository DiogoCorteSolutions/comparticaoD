
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

[Serializable()]
public class Secao
{
    #region Propriedades

    public virtual int Id { get; set; }
    public virtual int IdPerfil { get; set; }
    public virtual string Grupo { get; set; }
    public virtual string Nome { get; set; }
    public virtual string ToolTip { get; set; }
    public virtual string Caminho { get; set; }
    public virtual int Ordem { get; set; }
    public virtual bool PossuiControleTotal { get; set; }
    public virtual bool PodeAcessar { get; set; }
    public virtual bool PodeInserir { get; set; }
    public virtual bool PodeAlterar { get; set; }
    public virtual bool PodeExcluir { get; set; }
    #endregion

    #region FromIDataReader

    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["secaoId"], DBNull.Value)))
        {
            this.Id = Convert.ToInt32(pobjIDataReader["secaoId"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["grupo"], DBNull.Value)))
        {
            this.Grupo = pobjIDataReader["grupo"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["nomeSecao"], DBNull.Value)))
        {
            this.Nome = pobjIDataReader["nomeSecao"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["tooltip"], DBNull.Value)))
        {
            this.ToolTip = pobjIDataReader["tooltip"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["path"], DBNull.Value)))
        {
            this.Caminho = pobjIDataReader["path"].ToString();
        }

        if ((!object.ReferenceEquals(pobjIDataReader["ordemMenu"], DBNull.Value)))
        {
            this.Ordem = Convert.ToInt32(pobjIDataReader["ordemMenu"]);
        }
        
        if ((!object.ReferenceEquals(pobjIDataReader["controleTotal"], DBNull.Value)))
        {
            this.PossuiControleTotal = Convert.ToBoolean(pobjIDataReader["controleTotal"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["acessar"], DBNull.Value)))
        {
            this.PodeAcessar = Convert.ToBoolean(pobjIDataReader["acessar"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["inserir"], DBNull.Value)))
        {
            this.PodeInserir = Convert.ToBoolean(pobjIDataReader["inserir"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["alterar"], DBNull.Value)))
        {
            this.PodeAlterar = Convert.ToBoolean(pobjIDataReader["alterar"]);
        }

        if ((!object.ReferenceEquals(pobjIDataReader["excluir"], DBNull.Value)))
        {
            this.PodeExcluir = Convert.ToBoolean(pobjIDataReader["excluir"]);
        }
    }

    #endregion
}