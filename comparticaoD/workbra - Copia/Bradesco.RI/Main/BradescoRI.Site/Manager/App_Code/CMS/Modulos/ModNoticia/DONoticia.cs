using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DONoticia
/// </summary>
public class DONoticia
{
    #region Listar
    public static List<Noticia> Listar(Noticia pObjNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pObjNoticia.ID > 0)
            objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pObjNoticia.ID;

        if (pObjNoticia.TipoNoticia != null && pObjNoticia.TipoNoticia.ID > 0)
            objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pObjNoticia.TipoNoticia.ID;

        if (pObjNoticia.IdiomaId > 0)
            objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pObjNoticia.IdiomaId;

        try
        {
            objConexao.Open();
            List<Noticia> listNoticia = new List<Noticia>();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                Noticia obj = new Noticia();
                obj.FromIDataReader(idrReader);
                listNoticia.Add(obj);
            }

            return listNoticia;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Obter
    public static Noticia Obter(Noticia pObjNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pObjNoticia.ID > 0)
            objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pObjNoticia.ID;

        try
        {
            objConexao.Open();

            Noticia obj = new Noticia();

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
    }
    #endregion

    #region Inserir
    public static Noticia Inserir(Noticia pobjNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pobjNoticia.IdiomaId;
        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pobjNoticia.TipoNoticia.ID;
        objComando.Parameters.Add("@DataNoticia", SqlDbType.DateTime).Value = pobjNoticia.DataNoticia;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 512).Value = pobjNoticia.Titulo;
        objComando.Parameters.Add("@Resumo", SqlDbType.VarChar, 4096).Value = pobjNoticia.Resumo;
        objComando.Parameters.Add("@Integra", SqlDbType.VarChar).Value = pobjNoticia.Integra;
        objComando.Parameters.Add("@Fonte", SqlDbType.VarChar, 2048).Value = pobjNoticia.Fonte;
        objComando.Parameters.Add("@DataInclusao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@Destaque", SqlDbType.Bit).Value = pobjNoticia.Destaque;
        objComando.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = pobjNoticia.Usuario.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pobjNoticia.StatusId;
        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Direction = ParameterDirection.Output;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            objComando.ExecuteNonQuery();
            pobjNoticia.ID = int.Parse(objComando.Parameters["@NoticiaId"].Value.ToString());

            return pobjNoticia;

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
    public static int Excluir(Noticia pNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pNoticia.ID;
        try
        {
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

    #region Alterar
    public static int Alterar(Noticia pNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        int retorno = 0;

        //Define parametros da procedure               
        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pNoticia.ID;
        objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pNoticia.IdiomaId;
        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pNoticia.TipoNoticia.ID;
        objComando.Parameters.Add("@DataNoticia", SqlDbType.DateTime).Value = pNoticia.DataNoticia;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 512).Value = pNoticia.Titulo;
        objComando.Parameters.Add("@Resumo", SqlDbType.VarChar, 4096).Value = pNoticia.Resumo;
        objComando.Parameters.Add("@Integra", SqlDbType.VarChar).Value = pNoticia.Integra;
        objComando.Parameters.Add("@Fonte", SqlDbType.VarChar, 2048).Value = pNoticia.Fonte;
        objComando.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@Destaque", SqlDbType.Bit).Value = pNoticia.Destaque;
        objComando.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = pNoticia.Usuario.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pNoticia.StatusId;

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