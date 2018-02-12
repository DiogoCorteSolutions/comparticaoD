using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoRelatorio
/// </summary>
public class DoRelatorio
{
    #region Listar
    public static List<Relatorio> Listar(Relatorio pRelatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_Relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pRelatorio.TipoRelatorio.Id > 0)
            objComando.Parameters.Add("@TipoRelatorioId", SqlDbType.Int).Value = pRelatorio.TipoRelatorio.Id;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Relatorio> objList = new List<Relatorio>();
            Relatorio obj = default(Relatorio);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Relatorio();
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

    public static Relatorio Obter(Relatorio relatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_Relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (relatorio.ID > 0)
            objComando.Parameters.Add("@relatorioId", SqlDbType.Int).Value = relatorio.ID;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            Relatorio obj = default(Relatorio);

            IDataReader idrReader = default(IDataReader);
            obj = new Relatorio();

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

    public static int Excluir(Relatorio relatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_Relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@RelatorioId", SqlDbType.Int).Value = relatorio.ID;

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

    public static Relatorio Inserir(Relatorio pObjRelatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_Relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@TipoRelatorioId", SqlDbType.Int).Value = pObjRelatorio.TipoRelatorio.Id;
        objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pObjRelatorio.IdiomaId;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 100).Value = pObjRelatorio.Titulo;
        objComando.Parameters.Add("@Descricao", SqlDbType.VarChar, 400).Value = pObjRelatorio.Descricao;
        objComando.Parameters.Add("@DataRelatorio", SqlDbType.DateTime).Value = pObjRelatorio.DataRelatorio;
        objComando.Parameters.Add("@DataCadastro", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@UsuarioCadastroId", SqlDbType.Int).Value = pObjRelatorio.UsuarioCadastro.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjRelatorio.StatusId;
        objComando.Parameters.Add("@RelatorioId", SqlDbType.Int).Direction = ParameterDirection.Output;

        try
        {
            //Abre Conexao
            objConexao.Open();

            objComando.ExecuteNonQuery();
            pObjRelatorio.ID = int.Parse(objComando.Parameters["@RelatorioId"].Value.ToString());

            return pObjRelatorio;

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

    public static int Alterar(Relatorio pObjRelatorio)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_Relatorio");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        int retorno = 0;

        //Define parametros da procedure               
        objComando.Parameters.Add("@RelatorioId", SqlDbType.Int).Value = pObjRelatorio.ID;
        objComando.Parameters.Add("@TipoRelatorioId", SqlDbType.Int).Value = pObjRelatorio.TipoRelatorio.Id;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 100).Value = pObjRelatorio.Titulo;
        objComando.Parameters.Add("@Descricao", SqlDbType.VarChar, 400).Value = pObjRelatorio.Descricao;
        objComando.Parameters.Add("@DataRelatorio", SqlDbType.DateTime).Value = pObjRelatorio.DataRelatorio;
        objComando.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@UsuarioAtualizacaoId", SqlDbType.Int).Value = pObjRelatorio.UsuarioCadastro.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjRelatorio.StatusId;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
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
}