using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;


/// <summary>
/// Summary description for DOModFaleConosco
/// </summary>
public class DOModFaleConosco
{
    #region  Obter
    public static ModFaleConosco Obter(int pintIdConteudo, int pintIdIdioma)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_FALE_CONOSCO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            objConexao.Open();

            ModFaleConosco obj = new ModFaleConosco();

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
    public static int Inserir(ModFaleConosco pobjModFaleConosco)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_FALE_CONOSCO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModFaleConosco.IdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModFaleConosco.IdIdioma;
        objComando.Parameters.Add("@assunto", SqlDbType.VarChar, 200).Value = pobjModFaleConosco.Assunto;
        objComando.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = pobjModFaleConosco.Email;

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