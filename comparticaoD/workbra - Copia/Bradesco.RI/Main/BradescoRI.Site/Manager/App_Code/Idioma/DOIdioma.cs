using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOIdioma
/// </summary>
public class DOIdioma
{
    #region  Obter

    public static string ObterSigla(int pintIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_IDIOMA_SIGLA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
        
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdioma;

        try
        {
            objConexao.Open();

            IDataReader idrReader = objComando.ExecuteReader();

            if (idrReader.Read())
            {
                return Convert.ToString(idrReader["sigla"]);
            }
            else
                return string.Empty;

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
    /// Obter Idiomas
    /// </summary>
    /// <returns></returns>
    public static List<Idioma> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_IDIOMA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
        
        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Idioma> objList = new List<Idioma>();
            Idioma obj = default(Idioma);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Idioma();
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
}