using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOPaginaAprovacao
/// </summary>
public class DOPaginaAprovacao
{
    #region EnviarParaAprovacao
    public static void EnviarParaAprovacao(int paginaId, int usuarioId, string observacao, Boolean homePage)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_PAGINA_APROVACAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = paginaId;
        objComando.Parameters.Add("@USUARIOID", SqlDbType.Int).Value = usuarioId;
        objComando.Parameters.Add("@OBSERVACAO", SqlDbType.VarChar).Value = observacao;
        objComando.Parameters.Add("@HOMEPAGE", SqlDbType.Bit).Value = homePage;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            objComando.ExecuteNonQuery();
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

    #region Listar

    public static List<PaginaAprovacao> Listar(Boolean aprovados, Boolean reprovados, int aprovacaoId = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINA_APROVACAO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        if (aprovacaoId > 0)
        {
            objComando.Parameters.Add("@APROVACAOID", SqlDbType.Int).Value = aprovacaoId;
        }

        objComando.Parameters.Add("@APROVADOS", SqlDbType.Bit).Value = aprovados;
        objComando.Parameters.Add("@REPROVADOS", SqlDbType.Bit).Value = reprovados;

        try
        {
            objConexao.Open();

            List<PaginaAprovacao> objList = new List<PaginaAprovacao>();
            PaginaAprovacao obj = default(PaginaAprovacao);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new PaginaAprovacao();
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

    public static List<PaginaAprovacaoConteudo> ListarConteudoPaginaAprovacao(int aprovacaoId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINA_APROVACAO_CONTEUDO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure             
        objComando.Parameters.Add("@APROVACAOID", SqlDbType.Int).Value = aprovacaoId;

        try
        {
            objConexao.Open();

            List<PaginaAprovacaoConteudo> objList = new List<PaginaAprovacaoConteudo>();
            PaginaAprovacaoConteudo obj = default(PaginaAprovacaoConteudo);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new PaginaAprovacaoConteudo();
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

    #region Obter

    public static PaginaAprovacao Obter(int aprovacaoId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINA_APROVACAO_OBTER");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@APROVACAOID", SqlDbType.Int).Value = aprovacaoId;

        try
        {
            objConexao.Open();

            PaginaAprovacao objPaginaAprovacao = new PaginaAprovacao();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objPaginaAprovacao.FromIDataReader(idrReader);
            }
            
            return objPaginaAprovacao;
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

    #region Aprovar
    public static bool Aprovar(int aprovacaoId, int paginaId, int usuarioId, string observacao, Boolean homePage)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_PAGINA_APROVACAO_APROVADO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Parametros
        objComando.Parameters.Add("@APROVACAOID", SqlDbType.Int).Value = aprovacaoId;
        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = paginaId;
        objComando.Parameters.Add("@USUARIOIDVALIDADOR", SqlDbType.Int).Value = usuarioId;
        objComando.Parameters.Add("@HOMEPAGE", SqlDbType.Bit).Value = homePage;
        objComando.Parameters.Add("@OBSERVACAO", SqlDbType.VarChar).Value = observacao;
        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            return Convert.ToBoolean(objComando.ExecuteNonQuery());
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

    #region Reprovar
    public static bool Reprovar(int aprovacaoId, int usuarioId, string observacao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_PAGINA_APROVACAO_REPROVADO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Parametros
        objComando.Parameters.Add("@APROVACAOID", SqlDbType.Int).Value = aprovacaoId;
        objComando.Parameters.Add("@USUARIOIDVALIDADOR", SqlDbType.Int).Value = usuarioId;
        objComando.Parameters.Add("@OBSERVACAO", SqlDbType.VarChar).Value = observacao;
        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            return Convert.ToBoolean(objComando.ExecuteNonQuery());
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