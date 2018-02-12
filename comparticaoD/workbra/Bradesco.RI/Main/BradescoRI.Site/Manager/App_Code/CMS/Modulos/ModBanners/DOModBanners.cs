using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModBanners
/// </summary>
public class DOModBanners
{
    #region  Obter
    public static GrupoBanners ObterGrupo(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GRUPO_BANNERS_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            GrupoBanners obj = new GrupoBanners();

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
    public static Banners ObterBanner(int pintIdBanner, int pintIdGrupo, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_BANNERS_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@bannerId", SqlDbType.Int).Value = pintIdBanner;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            objConexao.Open();

            Banners obj = new Banners();

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
    public static ModBanners Obter(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_BANNERS_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            ModBanners obj = new ModBanners();

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
    public static int InserirGrupo(GrupoBanners pobjGrupoBanners)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_GRUPO_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 100).Value = pobjGrupoBanners.Descricao;

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
    public static int InserirBanner(Banners pobjBanners)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjBanners.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjBanners.IdIdioma;

        if (!String.IsNullOrWhiteSpace(pobjBanners.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjBanners.Target;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Texto1))
            objComando.Parameters.Add("@texto1", SqlDbType.VarChar, 200).Value = pobjBanners.Texto1;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Texto2))
            objComando.Parameters.Add("@texto2", SqlDbType.VarChar, 200).Value = pobjBanners.Texto2;
        if (!String.IsNullOrWhiteSpace(pobjBanners.TextoUrl))
            objComando.Parameters.Add("@textoUrl", SqlDbType.VarChar, 200).Value = pobjBanners.TextoUrl;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjBanners.Url;


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
    public static int InserirArquivo(Banners pobjBanners)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_BANNERS_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@bannerId", SqlDbType.Int).Value = pobjBanners.IdBanner;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjBanners.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjBanners.IdIdioma;
        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjBanners.Arquivo;

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
    public static int Inserir(ModBanners pobjModBanners)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModBanners.ID;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjModBanners.IdGrupo;

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
    public static int AtualizarBanner(Banners pobjBanners)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@bannerId", SqlDbType.Int).Value = pobjBanners.IdBanner;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjBanners.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjBanners.IdIdioma;

        if (!String.IsNullOrWhiteSpace(pobjBanners.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjBanners.Target;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Texto1))
            objComando.Parameters.Add("@texto1", SqlDbType.VarChar, 200).Value = pobjBanners.Texto1;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Texto2))
            objComando.Parameters.Add("@texto2", SqlDbType.VarChar, 200).Value = pobjBanners.Texto2;
        if (!String.IsNullOrWhiteSpace(pobjBanners.TextoUrl))
            objComando.Parameters.Add("@textoUrl", SqlDbType.VarChar, 200).Value = pobjBanners.TextoUrl;
        if (!String.IsNullOrWhiteSpace(pobjBanners.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjBanners.Url;

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
    public static List<GrupoBanners> ListarGrupos()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GRUPO_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<GrupoBanners> objList = new List<GrupoBanners>();
            GrupoBanners obj = default(GrupoBanners);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new GrupoBanners();
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
    public static List<Banners> ListarBanners(int pintIdGrupo, int pintIdIdioma = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;
        if(pintIdIdioma > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Banners> objList = new List<Banners>();
            Banners obj = default(Banners);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Banners();
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
    public static List<Banners> ListarModBanners(int pintId, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Banners> objList = new List<Banners>();
            Banners obj = default(Banners);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Banners();
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
    public static int ExcluirGrupo(int pintIdGrupo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_GRUPO_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;

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
    public static int ExcluirBanner(int pintid, int pintIdGrupo, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_BANNERS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@bannerId", SqlDbType.Int).Value = pintid;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;
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