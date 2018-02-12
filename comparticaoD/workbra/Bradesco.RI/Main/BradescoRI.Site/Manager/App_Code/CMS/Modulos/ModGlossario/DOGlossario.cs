using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOGlossario
/// </summary>
public class DOGlossario
{
    #region Listar
    public static List<Glossario> Listar(Glossario objGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (objGlossario.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = objGlossario.IdiomaId;

        if (objGlossario.Titulo != null && objGlossario.Titulo.Length > 0)
            objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 50).Value = objGlossario.Titulo;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Glossario> objList = new List<Glossario>();
            Glossario obj = default(Glossario);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Glossario();
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
    public static Glossario Obter(Glossario objGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@glossarioId", SqlDbType.Int).Value = objGlossario.Id;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            Glossario obj = default(Glossario);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            obj = new Glossario();

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
    public static int Inserir(Glossario objGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = objGlossario.IdiomaId;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 50).Value = objGlossario.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 1000).Value = objGlossario.Descricao;
        objComando.Parameters.Add("@usuarioCadastroId", SqlDbType.Int).Value = objGlossario.UsuarioCadastro.Id;
        objComando.Parameters.Add("@dataCadastro", SqlDbType.DateTime).Value = objGlossario.DataCadastro;
        objComando.Parameters.Add("@statusId", SqlDbType.Int).Value = objGlossario.StatusId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            return objComando.ExecuteNonQuery();

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
    public static int Atualizar(Glossario objGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@glossarioId", SqlDbType.Int).Value = objGlossario.Id;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = objGlossario.IdiomaId;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 50).Value = objGlossario.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 1000).Value = objGlossario.Descricao;
        objComando.Parameters.Add("@usuarioAtualizacaoId", SqlDbType.Int).Value = objGlossario.UsuarioAtualizacao.Id;
        objComando.Parameters.Add("@dataAtualizacao", SqlDbType.DateTime).Value = objGlossario.DataAtualizacao;
        objComando.Parameters.Add("@statusId", SqlDbType.Int).Value = objGlossario.StatusId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            return objComando.ExecuteNonQuery();

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

    #region Apagar 
    public static int Apagar(Glossario objGlossario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_GLOSSARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@glossarioId", SqlDbType.Int).Value = objGlossario.Id;

        try
        {
            //Abre Conexao
            objConexao.Open();

            return objComando.ExecuteNonQuery();

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