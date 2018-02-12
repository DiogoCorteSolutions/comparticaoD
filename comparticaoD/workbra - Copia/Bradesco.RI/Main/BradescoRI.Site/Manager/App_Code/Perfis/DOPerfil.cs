using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

public class DOPerfil
{
    #region  Obter
    /// <summary>
    /// Obter perfil pelo id
    /// </summary>
    /// <returns></returns>
    public static Perfil Obter(int Id)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PERFIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;

        try
        {
            objConexao.Open();

            Perfil objPerfil = new Perfil();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objPerfil.FromIDataReader(idrReader);
            }

            return objPerfil;

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
    /// <summary>
    /// Obter perfis
    /// </summary>
    /// <returns></returns>
    public static List<Perfil> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PERFIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Perfil> objListUsuario = new List<Perfil>();
            Perfil objUsuario = default(Perfil);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario = new Perfil();
                objUsuario.FromIDataReader(idrReader);
                objListUsuario.Add(objUsuario);
            }

            return objListUsuario;

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
    /// <summary>
    /// Atualiza um perfil
    /// </summary>
    /// <param name="pobjPerfil">Perfil</param>
    /// <returns></returns>
    public static int Atualizar(Perfil pobjPerfil)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_PERFIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pobjPerfil.Id;
        objComando.Parameters.Add("@NOMEPERFIL", SqlDbType.VarChar, 50).Value = pobjPerfil.Nome;
        
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
    /// <summary>
    /// Insere um perfil
    /// </summary>
    /// <param name="pobjPerfil">Perfil</param>
    /// <returns></returns>
    public static int Inserir(Perfil pobjPerfil)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_PERFIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@NOMEPERFIL", SqlDbType.VarChar, 50).Value = pobjPerfil.Nome;

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

        SqlCommand objComando = new SqlCommand("SPE_D_PERFIL");
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