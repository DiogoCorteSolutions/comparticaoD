using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOTraducao
/// </summary>
public class DOTraducao
{
    #region  Obter
    public static Traducao Obter(int Id)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TRADUCAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;

        try
        {
            objConexao.Open();

            Traducao obj = new Traducao();

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

    #region Listar
    public static List<Traducao> Listar(Traducao pobjTraducao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TRADUCAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pobjTraducao.ID > 0)
            objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pobjTraducao.ID;
        if (pobjTraducao.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjTraducao.IdiomaId;
        if (!string.IsNullOrWhiteSpace(pobjTraducao.ChaveNome))
            objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjTraducao.ChaveNome;
        if (!string.IsNullOrWhiteSpace(pobjTraducao.Texto))
            objComando.Parameters.Add("@texto", SqlDbType.VarChar, 100).Value = pobjTraducao.Texto;
        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Traducao> objList = new List<Traducao>();
            Traducao obj = default(Traducao);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Traducao();
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

    #region Atualizar
    public static int Atualizar(Traducao pobjTraducao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_TRADUCAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pobjTraducao.ID;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjTraducao.IdiomaId;
        objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjTraducao.ChaveNome;
        objComando.Parameters.Add("@texto", SqlDbType.VarChar, 100).Value = pobjTraducao.Texto;

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

    #region Inserir
    public static int Inserir(Traducao pobjTraducao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_TRADUCAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjTraducao.IdiomaId;
        objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjTraducao.ChaveNome;
        objComando.Parameters.Add("@texto", SqlDbType.VarChar, 100).Value = pobjTraducao.Texto;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Declara variavel de retorno
            int intRetorno = 0;

            //Executa comando no banco de dados
            intRetorno = Convert.ToInt32(objComando.ExecuteScalar());

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

        SqlCommand objComando = new SqlCommand("SPE_D_TRADUCAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pintId;

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
}