using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModJCP
/// </summary>
public class DOModJCP
{
    #region  Obter
    public static JCP ObterJCP(int pintIdJCP)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_JCP_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@jcpId", SqlDbType.Int).Value = pintIdJCP;

        try
        {
            objConexao.Open();

            JCP obj = new JCP();

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

    public static ModJCP Obter(int pintIdConteudo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_JCP_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;

        try
        {
            objConexao.Open();

            ModJCP obj = new ModJCP();

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
    public static int InserirJCP(JCP pobjJCP)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_JCP");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@ano", SqlDbType.Int).Value = pobjJCP.Ano;
        objComando.Parameters.Add("@periodo", SqlDbType.VarChar, 50).Value = pobjJCP.Periodo;
        objComando.Parameters.Add("@tipoProvento", SqlDbType.VarChar, 50).Value = pobjJCP.TipoProvento;
        objComando.Parameters.Add("@posicaoAcionaria", SqlDbType.Date).Value = pobjJCP.PosicaoAcionaria;
        objComando.Parameters.Add("@dataPagamento", SqlDbType.Date).Value = pobjJCP.DataPagamento;
        objComando.Parameters.Add("@valorAcao", SqlDbType.VarChar, 100).Value = pobjJCP.ValorAcao;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            IDataReader idrReader = objComando.ExecuteReader();

            if (idrReader.Read())
            {
                return Convert.ToInt32(idrReader["Identity"]);
            }
            else
                return -1;

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

    public static int Inserir(ModJCP pobjModJCP)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_JCP");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModJCP.IdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModJCP.IdIdioma;
        objComando.Parameters.Add("@JCPId", SqlDbType.Int).Value = pobjModJCP.IdJCP;

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

    //#region Atualizar
    //public static int AtualizarJCP(JCP pobjJCP)
    //{
    //    string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
    //    SqlConnection objConexao = new SqlConnection(strConectionString);

    //    SqlCommand objComando = new SqlCommand("SPE_U_JCP");
    //    objComando.Connection = objConexao;
    //    objComando.CommandType = CommandType.StoredProcedure;

    //    //Define parametros da procedure               
    //    objComando.Parameters.Add("@jcpId", SqlDbType.Int).Value = pobjJCP.IdJCP;
    //    objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjJCP.Titulo;
    //    objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 200).Value = pobjJCP.Descricao;
    //    objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjJCP.Arquivo;

    //    try
    //    {
    //        //Abre conexão com o banco de dados
    //        objConexao.Open();

    //        //Declara variavel de retorno
    //        int intRetorno = 0;

    //        //Executa comando no banco de dados
    //        intRetorno = objComando.ExecuteNonQuery();

    //        return intRetorno;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;

    //    }
    //    finally
    //    {
    //        //Fecha a conexao se aberta
    //        if (objConexao.State != ConnectionState.Closed)
    //        {
    //            objConexao.Close();
    //        }
    //    }
    //}
    //#endregion

    #region Listar
    public static List<JCP> Listar(int pintIdConteudo, int pintIdIdioma, int pintAno)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_JCP");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;
        objComando.Parameters.Add("@ano", SqlDbType.Int).Value = pintAno;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<JCP> objList = new List<JCP>();
            JCP obj = default(JCP);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new JCP();
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

    public static List<string> ListarAno()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ANO_JCP");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<string> objList = new List<string>();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objList.Add(idrReader["ano"].ToString());
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

    #region Excluir
    public static int ExcluirJCP(int pintIdJCP)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_JCP");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@jcpId", SqlDbType.Int).Value = pintIdJCP;
        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Declara variavel de retorno
            int intRetorno = 0;

            //Executa comando no banco de dados
            intRetorno = Convert.ToInt32(objComando.ExecuteScalar());

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