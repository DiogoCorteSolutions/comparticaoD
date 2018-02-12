using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOModGlossario
/// </summary>
public class DOModGlossario
{
    public static List<ModGlossario> Listar(ModGlossario modGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (modGlossario.ConteudoId > 0)
            objComando.Parameters.Add("@conteudoId", SqlDbType.VarChar, 50).Value = modGlossario.ConteudoId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ModGlossario> objList = new List<ModGlossario>();
            ModGlossario obj = default(ModGlossario);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new ModGlossario();
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

    public static int Excluir(ModGlossario objModGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MODULO_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.VarChar, 50).Value = objModGlossario.ConteudoId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            return objComando.ExecuteNonQuery();

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

    public static int Inserir(ModGlossario objModGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = objModGlossario.IdiomaId;
        objComando.Parameters.Add("@conteudoId", SqlDbType.VarChar, 50).Value = objModGlossario.ConteudoId;
        objComando.Parameters.Add("@glossarioId", SqlDbType.Int).Value = objModGlossario.GlossarioId;
        objComando.Parameters.Add("@data", SqlDbType.DateTime).Value = objModGlossario.Data;

        try
        {
            //Abre Conexao
            objConexao.Open();

            return objComando.ExecuteNonQuery();


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
}