using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for DOLog
/// </summary>
public class DOMenu
{
    #region Listar
    /// <summary>
    /// Obter Todos os menus principais
    /// </summary>
    /// <returns></returns>
    public static List<Menu> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_PAI");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Menu> objList = new List<Menu>();
            Menu obj = default(Menu);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Menu();
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

    #region Listar
    /// <summary>
    /// Listar Menus de uma hierarquia
    /// </summary>
    /// <returns></returns>
    public static List<Menu> Listar(string pstrHierarquia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_HIERARQUIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@hierarquia", SqlDbType.NVarChar, 45).Value = pstrHierarquia;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Menu> objList = new List<Menu>();
            Menu obj = default(Menu);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Menu();
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

    #region Listar Cultura
    /// <summary>
    /// Obter perfis pela cultura
    /// </summary>
    /// <returns></returns>
    public static List<Menu> ListarCultura(int pintIdiomaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pintIdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdiomaId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Menu> objList = new List<Menu>();
            Menu obj = default(Menu);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Menu();
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
    /// <summary>
    /// Obter Menu pela hierarquia
    /// </summary>
    /// <returns></returns>
    public static Menu Obter(string pstrHierarquia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_ITEM");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@hierarquia", SqlDbType.NVarChar, 45).Value = pstrHierarquia;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            Menu obj = default(Menu);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Menu();
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
    /// <summary>
    /// Atualiza um menu
    /// </summary>
    /// <param name="pobjMenu">Menu</param>
    /// <returns></returns>
    public static int Atualizar(Menu pobjMenu)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@hierarquia", SqlDbType.NVarChar, 45).Value = pobjMenu.Hierarquia;

        if (!String.IsNullOrWhiteSpace(pobjMenu.Nome))
            objComando.Parameters.Add("@nomeMenu", SqlDbType.VarChar, 100).Value = pobjMenu.Nome;
        if (!String.IsNullOrWhiteSpace(pobjMenu.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenu.Url;
        if (!String.IsNullOrWhiteSpace(pobjMenu.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenu.Target;
        if (pobjMenu.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenu.IdiomaId;
        if (!String.IsNullOrWhiteSpace(pobjMenu.ChaveNome))
            objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjMenu.ChaveNome;
        if (!String.IsNullOrWhiteSpace(pobjMenu.CssClass))
            objComando.Parameters.Add("@cssClass", SqlDbType.VarChar, 30).Value = pobjMenu.CssClass;

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

    #region Inserir Item Filho
    /// <summary>
    /// Atualiza um menu
    /// </summary>
    /// <param name="pobjMenu">Menu</param>
    /// <returns></returns>
    public static string InserirItemFilho(Menu pobjMenu)
    {
        string hierarquia = string.Empty;

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        hierarquia = ObterProximaHierarquia(pobjMenu.Hierarquia);

        //Define parametros da procedure
        objComando.Parameters.Add("@hierarquia", SqlDbType.NVarChar, 45).Value = hierarquia;

        if (!String.IsNullOrWhiteSpace(pobjMenu.Nome))
            objComando.Parameters.Add("@nomeMenu", SqlDbType.VarChar, 100).Value = pobjMenu.Nome;
        if (!String.IsNullOrWhiteSpace(pobjMenu.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenu.Url;
        if (!String.IsNullOrWhiteSpace(pobjMenu.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenu.Target;
        if (pobjMenu.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenu.IdiomaId;
        if (!String.IsNullOrWhiteSpace(pobjMenu.ChaveNome))
            objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjMenu.ChaveNome;
        if (!String.IsNullOrWhiteSpace(pobjMenu.CssClass))
            objComando.Parameters.Add("@cssClass", SqlDbType.VarChar, 30).Value = pobjMenu.CssClass;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Declara variavel de retorno
            int intRetorno = 0;

            //Executa comando no banco de dados
            intRetorno = objComando.ExecuteNonQuery();

            return hierarquia;

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

    /// <summary>
    /// Recebe o item atual e retornar a hierarquia do próx item
    /// </summary>
    /// <param name="pobjMenu">Menu</param>
    /// <returns></returns>
    private static string ObterProximaHierarquia(string pstrHierarquia)
    {
        string strNovaHierarquia = string.Empty;

        //Obtém último item filho
        List<Menu> itens = (from m in Listar(pstrHierarquia) where m.Hierarquia.Length == pstrHierarquia.Length + 3 select m).ToList();

        if (itens.Any())
        {
            Menu ultimoItem = (from m in Listar(pstrHierarquia) where m.Hierarquia.Length == pstrHierarquia.Length + 3 select m).Last();

            //Divide a hierarquia para adicionar novo item ao final
            List<string> items = Enumerable.Range(0, ultimoItem.Hierarquia.Length / 3).Select(i => ultimoItem.Hierarquia.Substring(i * 3, 3)).ToList();

            //Adicionar 1 após última hierarquia
            int intUltimo = Convert.ToInt32(items.Last()) + 1;

            for (int i = 0; i < items.Count(); i++)
            {
                if (i == items.Count() - 1)
                {
                    strNovaHierarquia += intUltimo.ToString().PadLeft(3, '0');
                }
                else
                    strNovaHierarquia += items[i];
            }
        }
        else
        {
            strNovaHierarquia = pstrHierarquia + "001";
        }

        return strNovaHierarquia;
    }

    #endregion

    #region Excluir
    public static void Excluir(String pstrHierarquia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@hierarquia", SqlDbType.NVarChar, 45).Value = pstrHierarquia;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            //Executa comando no banco de dados
            objComando.ExecuteNonQuery();

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