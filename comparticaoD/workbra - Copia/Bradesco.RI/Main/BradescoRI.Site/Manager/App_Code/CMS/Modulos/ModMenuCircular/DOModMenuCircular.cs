using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModMenuCircular
/// </summary>
public class DOModMenuCircular
{
    #region  Obter
    public static GrupoMenuCircular ObterGrupo(int pintIdGrupo)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GRUPO_MENU_CIRCULAR_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;

        try
        {
            objConexao.Open();

            GrupoMenuCircular obj = new GrupoMenuCircular();

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

    public static MenuCircular ObterMenuCircular(int pintIdMenuCircular, int pintIdGrupo, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_CIRCULAR_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@menuCircularId", SqlDbType.Int).Value = pintIdMenuCircular;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pintIdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            objConexao.Open();

            MenuCircular obj = new MenuCircular();

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

    public static ModMenuCircular Obter(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_MENU_CIRCULAR_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            ModMenuCircular obj = new ModMenuCircular();

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
    public static int InserirGrupo(GrupoMenuCircular pobjGrupoMenuCircular)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_GRUPO_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 100).Value = pobjGrupoMenuCircular.Descricao;

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

    public static int InserirMenuCircular(MenuCircular pobjMenuCircular)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjMenuCircular.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenuCircular.IdIdioma;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Titulo;

        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenuCircular.Target;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Tooltip))
            objComando.Parameters.Add("@tooltip", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Tooltip;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenuCircular.Url;


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

    public static int InserirArquivo(MenuCircular pobjMenuCircular)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MENU_CIRCULAR_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@menuCircularId", SqlDbType.Int).Value = pobjMenuCircular.IdMenuCircular;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjMenuCircular.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenuCircular.IdIdioma;
        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Arquivo;

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

    public static int Inserir(ModMenuCircular pobjModMenuCircular)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModMenuCircular.ID;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjModMenuCircular.IdGrupo;

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
    public static int AtualizarMenuCircular(MenuCircular pobjMenuCircular)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@menuCircularId", SqlDbType.Int).Value = pobjMenuCircular.IdMenuCircular;
        objComando.Parameters.Add("@grupoId", SqlDbType.Int).Value = pobjMenuCircular.IdGrupo;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenuCircular.IdIdioma;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Titulo;

        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Arquivo;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenuCircular.Target;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Tooltip))
            objComando.Parameters.Add("@tooltip", SqlDbType.VarChar, 200).Value = pobjMenuCircular.Tooltip;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircular.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenuCircular.Url;


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
    public static List<GrupoMenuCircular> ListarGrupos()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_GRUPO_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;
       

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<GrupoMenuCircular> objList = new List<GrupoMenuCircular>();
            GrupoMenuCircular obj = default(GrupoMenuCircular);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new GrupoMenuCircular();
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

    public static List<MenuCircular> ListarMenuCircular(int pintIdGrupo, int pintIdIdioma = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_CIRCULAR");
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
            List<MenuCircular> objList = new List<MenuCircular>();
            MenuCircular obj = default(MenuCircular);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new MenuCircular();
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

    public static List<MenuCircular> ListarModMenuCircular(int pintId, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_MENU_CIRCULAR");
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
            List<MenuCircular> objList = new List<MenuCircular>();
            MenuCircular obj = default(MenuCircular);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new MenuCircular();
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

        SqlCommand objComando = new SqlCommand("SPE_D_GRUPO_MENU_CIRCULAR");
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

    public static int ExcluirMenuCircular(int pintidMenuCircular, int pintIdGrupo, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MENU_CIRCULAR");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@menuCircularId", SqlDbType.Int).Value = pintidMenuCircular;
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