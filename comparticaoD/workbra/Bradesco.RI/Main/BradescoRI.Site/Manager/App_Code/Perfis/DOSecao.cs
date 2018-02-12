using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

public class DOSecao
{
    #region Listar
    /// <summary>
    /// Obter Seções de um perfil
    /// </summary>
    /// <returns></returns>
    public static List<Secao> Listar(int pintidPerfil)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_SECAO_PERFIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
        objComando.Parameters.Add("@perfilId", SqlDbType.VarChar, 20).Value = pintidPerfil;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Secao> objListSecao = new List<Secao>();
            Secao objSecao = default(Secao);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objSecao = new Secao();
                objSecao.FromIDataReader(idrReader);
                objListSecao.Add(objSecao);
            }

            return objListSecao;

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

    /// <summary>
    /// Obter Seções de um perfil
    /// </summary>
    /// <returns></returns>
    public static List<Secao> ListarSecoesMenu(int pintidPerfil)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_SECAO_PERFIL_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
        objComando.Parameters.Add("@perfilId", SqlDbType.VarChar, 20).Value = pintidPerfil;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Secao> objListSecao = new List<Secao>();
            Secao objSecao = default(Secao);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objSecao = new Secao();
                objSecao.FromIDataReader(idrReader);
                objListSecao.Add(objSecao);
            }

            return objListSecao;

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

    #region Excluir Acessos Perfil
    public static void ExcluirAcessosPerfil(int pintIdPerfil)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_ACESSO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@PERFILID", SqlDbType.Int).Value = pintIdPerfil;

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

    #region Inserir Acesso Perfil
    public static int InserirAcessoPerfil(Secao pobjSecao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ACESSO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@perfilId", SqlDbType.Int).Value = pobjSecao.IdPerfil;
        objComando.Parameters.Add("@secaoId", SqlDbType.Int).Value = pobjSecao.Id;
        objComando.Parameters.Add("@controleTotal", SqlDbType.Bit).Value = pobjSecao.PossuiControleTotal;
        objComando.Parameters.Add("@acessar", SqlDbType.Bit).Value = pobjSecao.PodeAcessar;
        objComando.Parameters.Add("@inserir", SqlDbType.Bit).Value = pobjSecao.PodeInserir;
        objComando.Parameters.Add("@alterar", SqlDbType.Bit).Value = pobjSecao.PodeAlterar;
        objComando.Parameters.Add("@excluir", SqlDbType.Bit).Value = pobjSecao.PodeExcluir;

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
}