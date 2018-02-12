using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModEnquete
/// </summary>
public class DOModEnquete
{
    #region  Obter
    public static Enquete ObterEnquete(int pintIdEnquete)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ENQUETE_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;

        try
        {
            objConexao.Open();

            Enquete obj = new Enquete();

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

    public static EnquetePergunta ObterPergunta(int pintIdPergunta, int pintIdEnquete, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ENQUETE_PERGUNTA_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@enquetePerguntaId", SqlDbType.Int).Value = pintIdPergunta;
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            objConexao.Open();

            EnquetePergunta obj = new EnquetePergunta();

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

    public static ModEnquete Obter(int pintIdConteudo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ENQUETE_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;

        try
        {
            objConexao.Open();

            ModEnquete obj = new ModEnquete();

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
    public static int InserirEnquete(Enquete pobjEnquete)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ENQUETE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 100).Value = pobjEnquete.Descricao;

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

    public static int InserirPergunta(EnquetePergunta pobjEnquetePergunta)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ENQUETE_PERGUNTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pobjEnquetePergunta.IdEnquete;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjEnquetePergunta.IdIdioma;
        objComando.Parameters.Add("@pergunta", SqlDbType.VarChar, 400).Value = pobjEnquetePergunta.Pergunta;

        objComando.Parameters.Add("@resposta1", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta1;
         objComando.Parameters.Add("@resposta2", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta2;

        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta3))
            objComando.Parameters.Add("@resposta3", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta3;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta4))
            objComando.Parameters.Add("@resposta4", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta4;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta5))
            objComando.Parameters.Add("@resposta5", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta5;

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

    public static int Inserir(ModEnquete pobjModEnquete)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_ENQUETE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModEnquete.IdConteudo;
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pobjModEnquete.IdEnquete;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjModEnquete.Titulo;
        objComando.Parameters.Add("@urlFaleConosco", SqlDbType.VarChar, 1000).Value = pobjModEnquete.UrlFaleConosco;

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

    public static int InserirResposta(string pstrNome, string pstrEmail, string pstrSugestao)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ENQUETE_RESPOSTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@nome", SqlDbType.VarChar, 200).Value = pstrNome;
        objComando.Parameters.Add("@email", SqlDbType.VarChar, 200).Value = pstrEmail;
        objComando.Parameters.Add("@sugestao", SqlDbType.VarChar, 200).Value = pstrSugestao;

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

    public static int InserirPerguntaResposta(int pintIdEnqueteResposta,int pintIdEnquetePergunta, string resposta)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ENQUETE_PERGUNTA_RESPOSTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@enqueteRespostaId", SqlDbType.Int).Value = pintIdEnqueteResposta;
        objComando.Parameters.Add("@enquetePerguntaId", SqlDbType.Int).Value = pintIdEnquetePergunta;
        objComando.Parameters.Add("@resposta", SqlDbType.VarChar, 100).Value = resposta;
        

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

    #region Atualizar
    public static int AtualizarPergunta(EnquetePergunta pobjEnquetePergunta)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_ENQUETE_PERGUNTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure 
        objComando.Parameters.Add("@enquetePerguntaId", SqlDbType.Int).Value = pobjEnquetePergunta.IdEnquetePergunta;
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pobjEnquetePergunta.IdEnquete;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjEnquetePergunta.IdIdioma;
        objComando.Parameters.Add("@pergunta", SqlDbType.VarChar, 400).Value = pobjEnquetePergunta.Pergunta;

        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta1))
            objComando.Parameters.Add("@resposta1", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta1;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta2))
            objComando.Parameters.Add("@resposta2", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta2;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta3))
            objComando.Parameters.Add("@resposta3", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta3;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta4))
            objComando.Parameters.Add("@resposta4", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta4;
        if (!String.IsNullOrWhiteSpace(pobjEnquetePergunta.Resposta5))
            objComando.Parameters.Add("@resposta5", SqlDbType.VarChar, 100).Value = pobjEnquetePergunta.Resposta5;


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

    #region Listar
    public static List<Enquete> ListarEnquete()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ENQUETE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;


        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Enquete> objList = new List<Enquete>();
            Enquete obj = default(Enquete);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Enquete();
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

    public static List<EnquetePergunta> ListarPergunta(int pintIdEnquete, int pintIdIdioma = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ENQUETE_PERGUNTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;
        if (pintIdIdioma > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<EnquetePergunta> objList = new List<EnquetePergunta>();
            EnquetePergunta obj = default(EnquetePergunta);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new EnquetePergunta();
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

    public static List<EnquetePergunta> Listar(int pintIdConteudo, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_ENQUETE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<EnquetePergunta> objList = new List<EnquetePergunta>();
            EnquetePergunta obj = default(EnquetePergunta);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new EnquetePergunta();
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

    public static List<EnqueteResposta> ListarResposta(int pintIdEnquete, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ENQUETE_PERGUNTA_RESPOSTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<EnqueteResposta> objList = new List<EnqueteResposta>();
            EnqueteResposta obj = default(EnqueteResposta);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new EnqueteResposta();
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

    #region Excluir
    public static int ExcluirEnquete(int pintIdEnquete)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_ENQUETE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;

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

    public static int ExcluirPergunta(int pintidEnquetePergunta, int pintIdEnquete, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_ENQUETE_PERGUNTA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@enquetePerguntaId", SqlDbType.Int).Value = pintidEnquetePergunta;
        objComando.Parameters.Add("@enqueteId", SqlDbType.Int).Value = pintIdEnquete;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

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