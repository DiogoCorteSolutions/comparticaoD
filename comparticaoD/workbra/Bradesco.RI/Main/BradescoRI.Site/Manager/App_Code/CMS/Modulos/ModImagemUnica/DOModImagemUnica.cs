using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOModTexto
/// </summary>
public class DOModImagemUnica
{
    #region  Obter
    public static ModImagemUnica Obter(int pintConteudoId, int pintIdioma)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_IMAGEM_UNICA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintConteudoId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdioma;

        try
        {
            objConexao.Open();

            ModImagemUnica obj = new ModImagemUnica();

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
    public static int Inserir(ModImagemUnica pobjModImagemUnica)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_IMAGEM_UNICA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModImagemUnica.IdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModImagemUnica.IdIdioma;
        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.Arquivo;

        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjModImagemUnica.Target;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Tooltip))
            objComando.Parameters.Add("@tooltip", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.Tooltip;
        if (pobjModImagemUnica.Tamanho > 0)
            objComando.Parameters.Add("@tamanho", SqlDbType.Int).Value = pobjModImagemUnica.Tamanho;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Texto1))
            objComando.Parameters.Add("@texto1", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.Texto1;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Texto2))
            objComando.Parameters.Add("@texto2", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.Texto2;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Texto3))
            objComando.Parameters.Add("@texto3", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.Texto3;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.TextoUrl))
            objComando.Parameters.Add("@textoUrl", SqlDbType.VarChar, 200).Value = pobjModImagemUnica.TextoUrl;
        if (!String.IsNullOrWhiteSpace(pobjModImagemUnica.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjModImagemUnica.Url;

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