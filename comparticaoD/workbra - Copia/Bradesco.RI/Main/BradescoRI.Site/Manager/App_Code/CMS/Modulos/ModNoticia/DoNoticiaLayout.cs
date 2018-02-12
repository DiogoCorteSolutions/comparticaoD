using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoNoticiaLayout
/// </summary>
public class DoNoticiaLayout
{
    #region  Listar
    public static List<NoticiaLayout> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_NOTICIA_LAYOUT");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            objConexao.Open();
            List<NoticiaLayout> ListaLayout = new List<NoticiaLayout>();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                NoticiaLayout obj = new NoticiaLayout();
                obj.FromIDataReader(idrReader);
                ListaLayout.Add(obj);
            }

            return ListaLayout;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
