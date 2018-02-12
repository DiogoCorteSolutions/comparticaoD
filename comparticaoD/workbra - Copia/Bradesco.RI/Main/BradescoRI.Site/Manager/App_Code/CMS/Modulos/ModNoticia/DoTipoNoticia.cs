using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoTipoNoticia
/// </summary>
public class DoTipoNoticia
{
    #region  Listar
    public static List<TipoNoticia> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
        try
        {
            objConexao.Open();

            List<TipoNoticia> objList = new List<TipoNoticia>();
            TipoNoticia obj = new TipoNoticia();

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
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }

    }
    #endregion

    #region Obter
    public static TipoNoticia Obter(TipoNoticia pTipoNoticiaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pTipoNoticiaId.ID;
        try
        {
            objConexao.Open();
            
            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            TipoNoticia obj = new TipoNoticia();

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
            if (objConexao.State != ConnectionState.Closed)
            {
                objConexao.Close();
            }
        }

    }
    #endregion
}