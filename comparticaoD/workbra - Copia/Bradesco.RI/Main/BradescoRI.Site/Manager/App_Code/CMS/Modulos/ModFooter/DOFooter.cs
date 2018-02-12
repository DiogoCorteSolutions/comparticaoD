using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOFooter
/// </summary>
public class DOFooter
{
    public static Footer Obter(Footer objFooter)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_FOOTER");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = objFooter.StatusId;

        try
        {
            objConexao.Open();

            Footer obj = new Footer();

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

    public static int Inserir(Footer objFooter)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_FOOTER");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@TextoCentral", SqlDbType.VarChar, 120).Value = objFooter.TextoCentral;
        objComando.Parameters.Add("@TituloN1", SqlDbType.VarChar, 30).Value = objFooter.TituloN1;
        objComando.Parameters.Add("@TelefoneN1", SqlDbType.VarChar, 15).Value = objFooter.TelefoneN1;
        objComando.Parameters.Add("@TextoN1", SqlDbType.VarChar, 40).Value = objFooter.TextoN1;
        objComando.Parameters.Add("@TituloN2", SqlDbType.VarChar, 30).Value = objFooter.TituloN2;
        objComando.Parameters.Add("@TelefoneN2", SqlDbType.VarChar, 15).Value = objFooter.TelefoneN2;
        objComando.Parameters.Add("@TextoN2", SqlDbType.VarChar, 40).Value = objFooter.TextoN2;
        objComando.Parameters.Add("@TituloN3", SqlDbType.VarChar, 30).Value = objFooter.TituloN3;
        objComando.Parameters.Add("@TelefoneN3", SqlDbType.VarChar, 15).Value = objFooter.TelefoneN3;
        objComando.Parameters.Add("@TextoN3", SqlDbType.VarChar, 40).Value = objFooter.TextoN3;
        objComando.Parameters.Add("@TituloLinkN1", SqlDbType.VarChar, 50).Value = objFooter.TituloLinkN1;
        objComando.Parameters.Add("@UrlLinkN1", SqlDbType.VarChar, 100).Value = objFooter.UrlLinkN1;
        objComando.Parameters.Add("@TituloLinkN2", SqlDbType.VarChar, 50).Value = objFooter.TituloLinkN2;
        objComando.Parameters.Add("@UrlLinkN2", SqlDbType.VarChar, 100).Value = objFooter.UrlLinkN2;
        objComando.Parameters.Add("@TituloLinkN3", SqlDbType.VarChar, 50).Value = objFooter.TituloLinkN3;
        objComando.Parameters.Add("@UrlLinkN3", SqlDbType.VarChar, 100).Value = objFooter.UrlLinkN3;
        objComando.Parameters.Add("@TituloLinkN4", SqlDbType.VarChar, 50).Value = objFooter.TituloLinkN4;
        objComando.Parameters.Add("@UrlLinkN4", SqlDbType.VarChar, 100).Value = objFooter.UrlLinkN4;
        objComando.Parameters.Add("@TituloLinkN5", SqlDbType.VarChar, 50).Value = objFooter.TituloLinkN5;
        objComando.Parameters.Add("@UrlLinkN5", SqlDbType.VarChar, 100).Value = objFooter.UrlLinkN5;
        objComando.Parameters.Add("@DataCadastro", SqlDbType.DateTime).Value = objFooter.DataCadastro;
        objComando.Parameters.Add("@UsuarioCadastroId", SqlDbType.Int).Value = objFooter.UsuarioCadastroId;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = objFooter.StatusId;

        int retorno = 0;
        try
        {
            objConexao.Open();

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
}