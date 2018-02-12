using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModIndicadoresFinanceiros
/// </summary>
public class DOModIndicadoresFinanceiros
{
    #region Listar
    public static List<ModIndicadoresFinanceiros> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoCanalFinanceiro"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("pc_IndicadoresFinanceirosRI");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;


        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ModIndicadoresFinanceiros> objList = new List<ModIndicadoresFinanceiros>();
            ModIndicadoresFinanceiros obj = default(ModIndicadoresFinanceiros);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new ModIndicadoresFinanceiros();
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