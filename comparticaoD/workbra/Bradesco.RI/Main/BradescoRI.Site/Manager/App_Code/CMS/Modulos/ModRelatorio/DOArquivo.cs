using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOArquivo
/// </summary>
public class DOArquivo
{
    public static Arquivo Inserir(Arquivo pObjArquivo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_COMUNICADO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Value = pObjArquivo.Comunicado.ID;
        objComando.Parameters.Add("@arquivoId", SqlDbType.Int).Value = pObjArquivo.Id;

        try
        {
            //Abre Conexao
            objConexao.Open();

            objComando.ExecuteNonQuery();
            pObjArquivo.Id = int.Parse(objComando.Parameters["@ArquivoId"].Value.ToString());

            return pObjArquivo;

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

    #region Listar Arquivos Comunicados
    public static List<Arquivo> Listar(Comunicado pObjComunicado)
    {
        try
        {
            string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
            SqlConnection objConexao = new SqlConnection(strConectionString);

            SqlCommand objComando = new SqlCommand("SPE_L_ARQUIVOS_COMUNICADO");
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;

            if (pObjComunicado.ID > 0)
                objComando.Parameters.Add("@ComunicadoId", SqlDbType.Int).Value = pObjComunicado.ID;

            try
            {
                //Abre Conexao
                objConexao.Open();

                //Declara variavel de retorno           
                List<Arquivo> objList = new List<Arquivo>();
                Arquivo obj = default(Arquivo);

                IDataReader idrReader = default(IDataReader);

                idrReader = objComando.ExecuteReader();

                while ((idrReader.Read()))
                {
                    obj = new Arquivo();
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
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Listar Arquivos Relatórios
    public static List<Arquivo> Listar(Relatorio pObjRelatorio)
    {
        try
        {
            string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
            SqlConnection objConexao = new SqlConnection(strConectionString);

            SqlCommand objComando = new SqlCommand("SPE_L_ARQUIVOS");
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;

            if (pObjRelatorio.ID > 0)
                objComando.Parameters.Add("@ArquivoId", SqlDbType.Int).Value = pObjRelatorio.ID;

            if (pObjRelatorio.IdiomaId > 0)
                objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pObjRelatorio.IdiomaId;

            if (pObjRelatorio.TipoRelatorio != null)
                objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = pObjRelatorio.TipoRelatorio.Id;

            try
            {
                //Abre Conexao
                objConexao.Open();

                //Declara variavel de retorno           
                List<Arquivo> objList = new List<Arquivo>();
                Arquivo obj = default(Arquivo);

                IDataReader idrReader = default(IDataReader);

                idrReader = objComando.ExecuteReader();

                while ((idrReader.Read()))
                {
                    obj = new Arquivo();
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
        catch (Exception ex)
        {
            throw ex;
        }
        #endregion
    }


    public static int RemoverRelatorioArquivo(int pRelatorioId, int pArquivoId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = null;

        objComando = new SqlCommand("SPE_D_RELATORIO_ARQUIVO");
        objComando.Parameters.Add("@relatorioId", SqlDbType.Int).Value = pRelatorioId;
        objComando.Parameters.Add("arquivoId", SqlDbType.Int).Value = pArquivoId;


        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

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
    
    
}