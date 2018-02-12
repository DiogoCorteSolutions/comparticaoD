using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOModArquivo
/// </summary>
public class DOModArquivo
{
    public static List<ModArquivo> Listar(ModArquivo pobjModArquivo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModArquivo.ConteudoId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ModArquivo> objList = new List<ModArquivo>();
            ModArquivo obj = default(ModArquivo);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new ModArquivo();
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

    public static int Salvar(ModArquivo pObjModArquivo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pObjModArquivo.ConteudoId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pObjModArquivo.IdiomaId;
        objComando.Parameters.Add("@tipoLayoutId", SqlDbType.Int).Value = pObjModArquivo.TipoLayoutId;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 100).Value = pObjModArquivo.Titulo;
        objComando.Parameters.Add("@showTitulo", SqlDbType.Bit).Value = pObjModArquivo.ShowTitulo;
        objComando.Parameters.Add("@showFiltro", SqlDbType.Bit).Value = pObjModArquivo.ShowFiltro;
        objComando.Parameters.Add("@data", SqlDbType.DateTime).Value = pObjModArquivo.Data;

        int retorno = 0;
        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            retorno = objComando.ExecuteNonQuery();

            return retorno;

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

    public static ModArquivo Obter(ModArquivo pObjModArquivo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pObjModArquivo.ConteudoId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            ModArquivo obj = default(ModArquivo);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            obj = new ModArquivo();
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
}