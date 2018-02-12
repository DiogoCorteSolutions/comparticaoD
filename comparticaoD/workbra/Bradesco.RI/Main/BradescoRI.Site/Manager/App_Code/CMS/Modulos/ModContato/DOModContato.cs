using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOModContato
/// </summary>
public class DOModContato
{
    #region  Obter
    public static ModContato Obter(int pintId, int pintIdioma)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_CONTATO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pintId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdioma;

        try
        {
            objConexao.Open();

            ModContato obj = new ModContato();

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
    public static int Inserir(ModContato pobjModContato)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_CONTATO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModContato.ID;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModContato.IdIdioma;
        objComando.Parameters.Add("@assuntoEmail", SqlDbType.VarChar, 200).Value = pobjModContato.AssuntoEmail;
        objComando.Parameters.Add("@assuntos", SqlDbType.VarChar, -1).Value = pobjModContato.Assuntos;
        objComando.Parameters.Add("@emailTo", SqlDbType.VarChar, 200).Value = pobjModContato.EmailTo;
        if (!String.IsNullOrWhiteSpace(pobjModContato.EmailToCc))
            objComando.Parameters.Add("@emailCc", SqlDbType.VarChar, 200).Value = pobjModContato.EmailToCc;
        if (!String.IsNullOrWhiteSpace(pobjModContato.EmailToCco))
            objComando.Parameters.Add("@emailCco", SqlDbType.VarChar, 200).Value = pobjModContato.EmailToCco;
        if (!String.IsNullOrWhiteSpace(pobjModContato.ConteudoTemplate))
            objComando.Parameters.Add("@conteudo", SqlDbType.VarChar, -1).Value = pobjModContato.ConteudoTemplate;

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
}