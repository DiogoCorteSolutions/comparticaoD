using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoTipoArquivo
/// </summary>
public class DOTipoArquivo
{
    public static List<TipoArquivo> Listar(TipoArquivo pObjTipoArquivo)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pObjTipoArquivo.Relatorio != null)
            objComando.Parameters.Add("@relatorio", SqlDbType.Bit).Value = pObjTipoArquivo.Relatorio;

        if (pObjTipoArquivo.Comunicado != null)
            objComando.Parameters.Add("@comunicado", SqlDbType.Bit).Value = pObjTipoArquivo.Comunicado;

        if (pObjTipoArquivo.Noticia != null)
            objComando.Parameters.Add("@noticia", SqlDbType.Bit).Value = pObjTipoArquivo.Noticia;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<TipoArquivo> objList = new List<TipoArquivo>();
            TipoArquivo obj = default(TipoArquivo);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new TipoArquivo();
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



    public static TipoArquivo Obter(TipoArquivo objTipoArquivo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = objTipoArquivo.Id;
        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            TipoArquivo obj = default(TipoArquivo);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            obj = new TipoArquivo();
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