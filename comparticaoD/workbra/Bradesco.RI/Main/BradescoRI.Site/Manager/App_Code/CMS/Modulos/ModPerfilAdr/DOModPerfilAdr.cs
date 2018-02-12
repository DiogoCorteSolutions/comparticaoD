using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModPerfilAdr
/// </summary>
public class DOModPerfilAdr
{
    #region  Obter
    public static ModPerfilAdr Obter(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_PERFIL_ADR_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@perfilAdrId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            ModPerfilAdr obj = new ModPerfilAdr();

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
    public static int Inserir(ModPerfilAdr pobjModPerfilAdr)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_PERFIL_ADR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModPerfilAdr.IdIdioma;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjModPerfilAdr.Titulo;
        objComando.Parameters.Add("@valor", SqlDbType.VarChar, 200).Value = pobjModPerfilAdr.Valor;
        objComando.Parameters.Add("@ordem", SqlDbType.Int).Value = pobjModPerfilAdr.Ordem;

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
    public static int Atualizar(ModPerfilAdr pobjModPerfilAdr)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MODULO_PERFIL_ADR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@perfilAdrId", SqlDbType.Int).Value = pobjModPerfilAdr.ID;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModPerfilAdr.IdIdioma;

        if (!String.IsNullOrWhiteSpace(pobjModPerfilAdr.Titulo))
            objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjModPerfilAdr.Titulo;
        if (!String.IsNullOrWhiteSpace(pobjModPerfilAdr.Valor))
            objComando.Parameters.Add("@valor", SqlDbType.VarChar, 200).Value = pobjModPerfilAdr.Valor;
        objComando.Parameters.Add("@ordem", SqlDbType.Int).Value = pobjModPerfilAdr.Ordem;

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

    #region Listar
    public static List<ModPerfilAdr> Listar(int pintIdiomaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_PERFIL_ADR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if(pintIdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdiomaId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ModPerfilAdr> objList = new List<ModPerfilAdr>();
            ModPerfilAdr obj = default(ModPerfilAdr);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new ModPerfilAdr();
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

    #region Excluir
    public static int Excluir(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MODULO_PERFIL_ADR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@perfilAdrId", SqlDbType.Int).Value = pintId;

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
}