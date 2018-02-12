using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DOModMenuCircularHome
/// </summary>
public class DOModMenuCircularHome
{
    #region  Obter
    
    public static MenuCircularHome ObterMenuCircularHome(int pintIdMenuCircularHome)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_CIRCULAR_HOME_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@menuCircularHomeId", SqlDbType.Int).Value = pintIdMenuCircularHome;
        
        try
        {
            objConexao.Open();

            MenuCircularHome obj = new MenuCircularHome();

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

    public static ModMenuCircularHome Obter(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_MENU_CIRCULAR_HOME_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            ModMenuCircularHome obj = new ModMenuCircularHome();

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
    
    public static int InserirMenuCircularHome(MenuCircularHome pobjMenuCircularHome)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MENU_CIRCULAR_HOME");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Titulo;
        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Arquivo;

        if (!String.IsNullOrWhiteSpace(pobjMenuCircularHome.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenuCircularHome.Target;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircularHome.Tooltip))
            objComando.Parameters.Add("@tooltip", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Tooltip;
        objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenuCircularHome.Url;


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
    
    public static int Inserir(ModMenuCircularHome pobjModMenuCircularHome)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_MENU_CIRCULAR_HOME");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModMenuCircularHome.ID;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjModMenuCircularHome.IdIdioma;
        objComando.Parameters.Add("@menuCircularHomeId", SqlDbType.Int).Value = pobjModMenuCircularHome.IdMenuCircularHome;

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
    public static int AtualizarMenuCircularHome(MenuCircularHome pobjMenuCircularHome)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MENU_CIRCULAR_HOME");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@menuCircularHomeId", SqlDbType.Int).Value = pobjMenuCircularHome.IdMenuCircularHome;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Titulo;

        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Arquivo;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircularHome.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenuCircularHome.Target;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircularHome.Tooltip))
            objComando.Parameters.Add("@tooltip", SqlDbType.VarChar, 200).Value = pobjMenuCircularHome.Tooltip;
        if (!String.IsNullOrWhiteSpace(pobjMenuCircularHome.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenuCircularHome.Url;


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
    
    public static List<MenuCircularHome> Listar(int pintId, int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_CIRCULAR_HOME");
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
            List<MenuCircularHome> objList = new List<MenuCircularHome>();
            MenuCircularHome obj = default(MenuCircularHome);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new MenuCircularHome();
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

    public static int ExcluirMenuCircularHome(int pintidMenuCircularHome)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MENU_CIRCULAR_HOME");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@menuCircularHomeId", SqlDbType.Int).Value = pintidMenuCircularHome;
        
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