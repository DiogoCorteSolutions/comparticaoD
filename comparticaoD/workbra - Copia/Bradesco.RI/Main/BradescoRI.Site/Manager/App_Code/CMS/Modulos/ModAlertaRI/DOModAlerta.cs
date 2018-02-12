using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOModAlerta
/// </summary>
public class DOModAlerta
{
    #region Listar
    public static List<ModAlerta> Listar(int pintIdIdioma = 0,
                                               string pstrNome = "",
                                               string pstrEmail = "",
                                               DateTime? pdatDataInicio = null,
                                               DateTime? pdatDataFim = null)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ALERTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        if (pintIdIdioma > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;
        if (!String.IsNullOrWhiteSpace(pstrNome))
            objComando.Parameters.Add("@nome", SqlDbType.NVarChar, 128).Value = pstrNome;
        if (!String.IsNullOrWhiteSpace(pstrEmail))
            objComando.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = pstrEmail;
        if (pdatDataInicio.HasValue)
            objComando.Parameters.Add("@dataCadastroInicio", SqlDbType.Date).Value = pdatDataInicio.Value;
        if (pdatDataFim.HasValue)
            objComando.Parameters.Add("@dataCadastroFim", SqlDbType.Date).Value = pdatDataFim.Value;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ModAlerta> objList = new List<ModAlerta>();
            ModAlerta obj = default(ModAlerta);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new ModAlerta();
                obj.FromIDataReader(idrReader);
                objList.Add(obj);
            }

            return objList;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            //Fecha a conexao se aberta
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }
    }
    #endregion

    #region  Obter
    public static ModAlerta Obter(int pintId, int pintIdioma)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ALERTA_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@alertaId", SqlDbType.Int).Value = pintId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdioma;

        try
        {
            objConexao.Open();

            ModAlerta obj = new ModAlerta();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj.FromIDataReader(idrReader);
            }

            return obj;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            //Fecha a conexao se aberta
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }

    }
    #endregion

    #region Inserir
    public static int Inserir(ModAlerta pobjModAlerta)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_ALERTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@nome", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Nome;
        objComando.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Email;
        objComando.Parameters.Add("@profissionalMercado", SqlDbType.Bit).Value = pobjModAlerta.ProfissionalMercado;
        objComando.Parameters.Add("@idiomaMailing", SqlDbType.Int).Value = pobjModAlerta.IdIdiomaMailing;
        objComando.Parameters.Add("@receberMailing", SqlDbType.Bit).Value = pobjModAlerta.ReceberMailing;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Empresa))
            objComando.Parameters.Add("@empresa", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Empresa;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.TelefoneDDD))
            objComando.Parameters.Add("@telefoneDDD", SqlDbType.Char, 10).Value = pobjModAlerta.TelefoneDDD;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Telefone))
            objComando.Parameters.Add("@telefone", SqlDbType.Char, 15).Value = pobjModAlerta.Telefone;
        if (pobjModAlerta.IdSegmentoEmpresa > 0)
            objComando.Parameters.Add("@segmentoEmpresaId", SqlDbType.Int).Value = pobjModAlerta.IdSegmentoEmpresa;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Estado))
            objComando.Parameters.Add("@estado", SqlDbType.NVarChar,128).Value = pobjModAlerta.Estado;
        if (pobjModAlerta.IdPais > 0)
            objComando.Parameters.Add("@paisId", SqlDbType.Int).Value = pobjModAlerta.IdPais;
        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Declara variavel de retorno
            int intRetorno = 0;

            //Executa comando no banco de dados
            intRetorno = objComando.ExecuteNonQuery();

            return intRetorno;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            //Fecha a conexao se aberta
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }
    }
    #endregion

    #region Atualizar
    public static int Atualizar(ModAlerta pobjModAlerta)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MODULO_ALERTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure        
        objComando.Parameters.Add("@alertaId", SqlDbType.Int).Value = pobjModAlerta.Id;
        objComando.Parameters.Add("@nome", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Nome;
        objComando.Parameters.Add("@email", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Email;
        objComando.Parameters.Add("@profissionalMercado", SqlDbType.Bit).Value = pobjModAlerta.ProfissionalMercado;
        objComando.Parameters.Add("@idiomaMailing", SqlDbType.Int).Value = pobjModAlerta.IdIdiomaMailing;
        objComando.Parameters.Add("@receberMailing", SqlDbType.Bit).Value = pobjModAlerta.ReceberMailing;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Empresa))
            objComando.Parameters.Add("@empresa", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Empresa;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.TelefoneDDD))
            objComando.Parameters.Add("@telefoneDDD", SqlDbType.Char, 10).Value = pobjModAlerta.TelefoneDDD;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Telefone))
            objComando.Parameters.Add("@telefone", SqlDbType.Char, 15).Value = pobjModAlerta.Telefone;
        if (pobjModAlerta.IdSegmentoEmpresa > 0)
            objComando.Parameters.Add("@segmentoEmpresaId", SqlDbType.Int).Value = pobjModAlerta.IdSegmentoEmpresa;
        if (!String.IsNullOrWhiteSpace(pobjModAlerta.Estado))
            objComando.Parameters.Add("@estado", SqlDbType.NVarChar, 128).Value = pobjModAlerta.Estado;
        if (pobjModAlerta.IdPais > 0)
            objComando.Parameters.Add("@paisId", SqlDbType.Int).Value = pobjModAlerta.IdPais;
        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Declara variavel de retorno
            int intRetorno = 0;

            //Executa comando no banco de dados
            intRetorno = objComando.ExecuteNonQuery();

            return intRetorno;

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            //Fecha a conexao se aberta
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }
    }
    #endregion

    #region Excluir
    public static void Excluir(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MODULO_ALERTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@alertaId", SqlDbType.Int).Value = pintId;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            objComando.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            //Fecha a conexao se aberta
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }
    }
    #endregion

    #region TrataExcel
    /// <summary>
    /// Formata Datatable para criação de relatório
    /// </summary>
    /// <param name="dt">Datatable</param>
    /// <returns></returns>
    public static DataTable TrataExcel(List<ModAlerta> plstAlertas)
    {
        DataTable dtN = new DataTable();
        dtN.Columns.Add("ID");
        dtN.Columns.Add("Nome");
        dtN.Columns.Add("E-mail");
        dtN.Columns.Add("Profissional de Mercado?");
        dtN.Columns.Add("Segmento da Empresa");
        dtN.Columns.Add("Empresa");
        dtN.Columns.Add("País");
        dtN.Columns.Add("Data Inicial");
        
        foreach (ModAlerta objAlerta in plstAlertas)
        {
            DataRow drN = dtN.NewRow();
            drN["ID"] = objAlerta.Id.ToString();
            drN["Nome"] = objAlerta.Nome;
            drN["E-mail"] = objAlerta.Email;
            drN["Profissional de Mercado?"] = (objAlerta.ProfissionalMercado ? "Sim" : "Não");
            drN["Segmento da Empresa"] = objAlerta.NomeSegmento;
            drN["Empresa"] = objAlerta.Empresa;
            drN["País"] = objAlerta.NomePais;
            drN["Data Inicial"] = objAlerta.Data.ToShortDateString();
            dtN.Rows.Add(drN);
        }
        return dtN;
    }
    #endregion
}