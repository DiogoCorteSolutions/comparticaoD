using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for DOMenuLinkExtra
/// </summary>
public class DOMenuLinkExtra
{
    #region Listar
    /// <summary>
    /// Obter Todos os menus principais
    /// </summary>
    /// <returns></returns>
    public static List<MenuLinkExtra> Listar(int pintId = 0,int pintIdiomaId = 0)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MENU_LINK_EXTRA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if(pintId > 0)
            objComando.Parameters.Add("@menuLinkId", SqlDbType.Int).Value = pintId;
        if (pintIdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdiomaId;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<MenuLinkExtra> objList = new List<MenuLinkExtra>();
            MenuLinkExtra obj = default(MenuLinkExtra);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new MenuLinkExtra();
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

    #region Atualizar
    /// <summary>
    /// Atualiza um link extra do menu
    /// </summary>
    /// <param name="pobjMenuLinkExtra">MenuLinkExtra</param>
    /// <returns></returns>
    public static int Atualizar(MenuLinkExtra pobjMenuLinkExtra)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_MENU_LINK_EXTRA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@menuLinkId", SqlDbType.Int).Value = pobjMenuLinkExtra.ID;

        if (!String.IsNullOrWhiteSpace(pobjMenuLinkExtra.Nome))
            objComando.Parameters.Add("@nomeMenu", SqlDbType.VarChar, 100).Value = pobjMenuLinkExtra.Nome;
        if (!String.IsNullOrWhiteSpace(pobjMenuLinkExtra.Url))
            objComando.Parameters.Add("@url", SqlDbType.VarChar, 1000).Value = pobjMenuLinkExtra.Url;
        if (!String.IsNullOrWhiteSpace(pobjMenuLinkExtra.Target))
            objComando.Parameters.Add("@target", SqlDbType.VarChar, 20).Value = pobjMenuLinkExtra.Target;
        if (pobjMenuLinkExtra.IdiomaId > 0)
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjMenuLinkExtra.IdiomaId;
        if (!String.IsNullOrWhiteSpace(pobjMenuLinkExtra.ChaveNome))
            objComando.Parameters.Add("@chaveNome", SqlDbType.VarChar, 50).Value = pobjMenuLinkExtra.ChaveNome;
        
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
}