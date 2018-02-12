using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOArquivos
/// </summary>
public class DOArquivos
{
    #region Listar
    public static List<Arquivos> Listar(Arquivos objArquivos)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ARQUIVOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (objArquivos.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = objArquivos.IdiomaId;

        if (objArquivos.TipoArquivoId > 0)
            objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = objArquivos.TipoArquivoId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Arquivos> objList = new List<Arquivos>();
            Arquivos obj = default(Arquivos);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Arquivos();
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
    public static Arquivos Obter(Arquivos arquivos)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ARQUIVOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@arquivoId", SqlDbType.Int).Value = arquivos.Id;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            Arquivos obj = default(Arquivos);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            obj = new Arquivos();

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

    #region Atualizar
    public static int Atualizar(Arquivos pObjArquivos)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_ARQUIVOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@arquivoId", SqlDbType.Int).Value = pObjArquivos.Id;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pObjArquivos.IdiomaId;
        objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = pObjArquivos.TipoArquivoId;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 100).Value = pObjArquivos.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 400).Value = pObjArquivos.Descricao;
        objComando.Parameters.Add("@caminho", SqlDbType.VarChar, 255).Value = pObjArquivos.Caminho;

        if (pObjArquivos.Extensao != null)
            objComando.Parameters.Add("@extensao", SqlDbType.VarChar, 3).Value = pObjArquivos.Extensao;

        if (pObjArquivos.Tamanho != null)
            objComando.Parameters.Add("@tamanho", SqlDbType.VarChar, 50).Value = pObjArquivos.Tamanho;

        objComando.Parameters.Add("@dataArquivo", SqlDbType.DateTime).Value = pObjArquivos.DataArquivo;
        objComando.Parameters.Add("@dataAtualizacao", SqlDbType.DateTime).Value = pObjArquivos.DataAtualizacao;
        objComando.Parameters.Add("@streaming", SqlDbType.Bit).Value = pObjArquivos.Streaming;
        objComando.Parameters.Add("@UsuarioAtualizacaoId", SqlDbType.Int).Value = pObjArquivos.UsuarioAtualizacaoId;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjArquivos.StatusId;
        int retorno = 0;
        try
        {
            //Abre Conexao
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
    #endregion

    #region Inserir
    public static int Inserir(Arquivos pObjArquivos)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ARQUIVOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pObjArquivos.IdiomaId;
        objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = pObjArquivos.TipoArquivoId;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 100).Value = pObjArquivos.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 400).Value = pObjArquivos.Descricao;
        objComando.Parameters.Add("@caminho", SqlDbType.VarChar, 255).Value = pObjArquivos.Caminho;
        objComando.Parameters.Add("@extensao", SqlDbType.VarChar, 5).Value = pObjArquivos.Extensao;
        objComando.Parameters.Add("@tamanho", SqlDbType.VarChar, 50).Value = pObjArquivos.Tamanho;
        objComando.Parameters.Add("@dataArquivo", SqlDbType.DateTime).Value = pObjArquivos.DataArquivo;
        objComando.Parameters.Add("@dataCadastro", SqlDbType.DateTime).Value = pObjArquivos.DataCadastro;
        objComando.Parameters.Add("@UsuarioCadastroId", SqlDbType.Int).Value = pObjArquivos.UsuarioCadastroId;
        objComando.Parameters.Add("@streaming", SqlDbType.Bit).Value = pObjArquivos.Streaming;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjArquivos.StatusId;
        int retorno = 0;
        try
        {
            //Abre Conexao
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
    #endregion

    #region Excluir
    public static int Excluir(Arquivos pObjArquivos)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_ARQUIVOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@arquivoId", SqlDbType.Int).Value = pObjArquivos.Id;
        int retorno = 0;
        try
        {
            //Abre Conexao
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

    public static List<Arquivos> ListarRelatorioArquivo(int pRelatorioId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_RELATORIO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@relatorioId", SqlDbType.Int).Value = pRelatorioId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Arquivos> objList = new List<Arquivos>();
            Arquivos obj = default(Arquivos);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Arquivos();
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
}