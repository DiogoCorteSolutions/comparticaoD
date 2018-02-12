using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOTipoRelatorio
/// </summary>
public class DOTipoRelatorio
{
    #region Listar
    public static List<TipoNoticia> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<TipoNoticia> objList = new List<TipoNoticia>();
            TipoNoticia obj = default(TipoNoticia);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new TipoNoticia();
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

    public static TipoRelatorio Obter(TipoRelatorio tipoRelatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("spe_l_tipo_relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@TipoRelatorioId", SqlDbType.Int).Value = tipoRelatorio.ID;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            TipoRelatorio obj = default(TipoRelatorio);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            obj = new TipoRelatorio();

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
}