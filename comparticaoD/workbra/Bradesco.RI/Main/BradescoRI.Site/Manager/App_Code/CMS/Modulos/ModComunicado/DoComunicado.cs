using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoComunicado
/// </summary>
public class DoComunicado
{
    #region Listar
    public static List<Comunicado> Listar(Comunicado pComunicado)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_Comunicado");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pComunicado.TipoComunicado.ID > 0)
            objComando.Parameters.Add("@TipoComunicadoId", SqlDbType.Int).Value = pComunicado.TipoComunicado.ID;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Comunicado> objList = new List<Comunicado>();
            Comunicado obj = default(Comunicado);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Comunicado();
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

    public static Comunicado Obter(Comunicado comunicado)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_Comunicado");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Value = comunicado.ID;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            Comunicado obj = default(Comunicado);

            IDataReader idrReader = default(IDataReader);
            obj = new Comunicado();

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

    public static int Excluir(Comunicado comunicado)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_Comunicado");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Value = comunicado.ID;

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

    public static Comunicado Inserir(Comunicado pObjComunicado)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_Comunicado");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@TipoComunicadoId", SqlDbType.Int).Value = pObjComunicado.TipoComunicado.ID;
        objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pObjComunicado.IdiomaId;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 100).Value = pObjComunicado.Titulo;
        objComando.Parameters.Add("@Descricao", SqlDbType.VarChar, 400).Value = pObjComunicado.Descricao;
        objComando.Parameters.Add("@DataComunicado", SqlDbType.DateTime).Value = pObjComunicado.DataComunicado;
        objComando.Parameters.Add("@DataCadastro", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@UsuarioCadastroId", SqlDbType.Int).Value = pObjComunicado.UsuarioCadastro.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjComunicado.StatusId;
        objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Direction = ParameterDirection.Output;

        try
        {
            //Abre Conexao
            objConexao.Open();

            objComando.ExecuteNonQuery();
            pObjComunicado.ID = int.Parse(objComando.Parameters["@ComunicadoId"].Value.ToString());

            return pObjComunicado;

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

    public static int Alterar(Comunicado pObjComunicado)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_Comunicado");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        int retorno = 0;

        //Define parametros da procedure               
        objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Value = pObjComunicado.ID;
        objComando.Parameters.Add("@TipoComunicadoId", SqlDbType.Int).Value = pObjComunicado.TipoComunicado.ID;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 100).Value = pObjComunicado.Titulo;
        objComando.Parameters.Add("@Descricao", SqlDbType.VarChar, 400).Value = pObjComunicado.Descricao;
        objComando.Parameters.Add("@DataComunicado", SqlDbType.DateTime).Value = pObjComunicado.DataComunicado;
        objComando.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@UsuarioAtualizacaoId", SqlDbType.Int).Value = pObjComunicado.UsuarioCadastro.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pObjComunicado.StatusId;

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