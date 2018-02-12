using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOLog
/// </summary>
public class DOLog
{
    #region Listar
    /// <summary>
    /// Obter Logs
    /// </summary>
    /// <returns></returns>
    public static List<Log> Listar(string pstrMensagem = null, DateTime? pdatInicio = null, DateTime? pdatFim = null, int? pintTipo = null, string pstrUsuario = null, int? pintUsuarioId = null)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_LOG");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (!String.IsNullOrWhiteSpace(pstrMensagem))
            objComando.Parameters.Add("@MsgLog", SqlDbType.VarChar, 200).Value = pstrMensagem;

        if (pdatInicio.HasValue)
            objComando.Parameters.Add("@DataLogInicio", SqlDbType.DateTime).Value = pdatInicio;

        if (pdatFim.HasValue)
            objComando.Parameters.Add("@DataLogFim", SqlDbType.DateTime).Value = pdatFim;

        if (pintTipo.HasValue)
            objComando.Parameters.Add("@Tipo", SqlDbType.Int).Value = pintTipo;


        if (pintUsuarioId.HasValue)
            objComando.Parameters.Add("@usuarioId", SqlDbType.Int).Value = pintUsuarioId;

        if (!String.IsNullOrWhiteSpace(pstrUsuario))
            objComando.Parameters.Add("@usuarioNome", SqlDbType.VarChar, 200).Value = pstrUsuario;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Log> objListUsuario = new List<Log>();
            Log objUsuario = default(Log);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario = new Log();
                objUsuario.FromIDataReader(idrReader);
                objListUsuario.Add(objUsuario);
            }

            return objListUsuario;

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

    #region InserirLog
    /// <summary>
    /// Insere Log no banco de dados
    /// </summary>
    /// <param name="pobjLog"></param>
    /// <returns></returns>
    public static int Inserir(string mensagem, Utilitarios.TipoLog tipoLog, int pintUsuarioId = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_LOG");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@MENSAGEM", SqlDbType.VarChar).Value = mensagem;
        objComando.Parameters.Add("@TIPO", SqlDbType.Int).Value = Convert.ToInt32(tipoLog);

        if (pintUsuarioId > 0)
            objComando.Parameters.Add("@USUARIOID", SqlDbType.Int).Value = pintUsuarioId;

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