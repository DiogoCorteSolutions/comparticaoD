using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOPagina
/// </summary>
public class DOPagina
{
    #region Listar           
    /// <summary>
    /// Obter Logs
    /// </summary>
    /// <returns></returns>
    public static List<Pagina> Listar(int pintIdCategoria = 0,int pintStatus = -1)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINAS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if(pintIdCategoria > 0)
        objComando.Parameters.Add("@categoriaId", SqlDbType.Int).Value = pintIdCategoria;

        if (pintStatus > -1)
            objComando.Parameters.Add("@status", SqlDbType.Int).Value = pintStatus;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Pagina> objList = new List<Pagina>();
            Pagina obj = default(Pagina);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Pagina();
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

    public static List<Categoria> ListarCategoria()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_CATEGORIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Categoria> objList = new List<Categoria>();
            Categoria obj = default(Categoria);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Categoria();
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

    public static List<Template> ListarTemplate()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TEMPLATE");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Template> objList = new List<Template>();
            Template obj = default(Template);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Template();
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

    #region Inserir
    /// <summary>
    /// Insere pagina no banco de dados
    /// </summary>
    /// <param name="pobjPagina"></param>
    /// <returns></returns>
    public static int Inserir(Pagina pobjPagina)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_PAGINA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@CATEGORIAID", SqlDbType.Int).Value = pobjPagina.CategoriaId;
        objComando.Parameters.Add("@TEMPLATEID", SqlDbType.Int).Value = pobjPagina.TemplateId;
        objComando.Parameters.Add("@DESCRICAO", SqlDbType.VarChar, 400).Value = pobjPagina.Descricao;
        objComando.Parameters.Add("@ALIAS", SqlDbType.VarChar, 100).Value = pobjPagina.Alias;
        objComando.Parameters.Add("@TITULO", SqlDbType.VarChar).Value = pobjPagina.Titulo;
        objComando.Parameters.Add("@STATUS", SqlDbType.Int).Value = pobjPagina.Status;
        objComando.Parameters.Add("@METATAGSDESCRIPTION", SqlDbType.VarChar).Value = pobjPagina.MetatagsDescription;
        objComando.Parameters.Add("@METATAGSKEYWORDS", SqlDbType.VarChar).Value = pobjPagina.MetatagsKeywords;
        objComando.Parameters.Add("@CORMENU", SqlDbType.VarChar).Value = pobjPagina.CorMenu;
        objComando.Parameters.Add(new SqlParameter("@USUARIOID", pobjPagina.Usuario.Id));
      
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

    public static int InserirCategoria(Categoria pobjCategoria)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_CATEGORIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 200).Value = pobjCategoria.Descricao;
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

    #region Excluir
    /// <summary>
    /// Exclui pagina no banco de dados
    /// </summary>
    /// <param name="pobjPagina"></param>
    /// <returns></returns>
    public static void Excluir(int paginaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_PAGINA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = paginaId;
      
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

    public static void ExcluirCategoria(int pintCategoriaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_CATEGORIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@categoriaId", SqlDbType.Int).Value = pintCategoriaId;

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
      
    #region Atualizar
    public static void Atualizar(Pagina pobjPagina)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_PAGINA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = pobjPagina.PaginaId;
        objComando.Parameters.Add("@TITULO", SqlDbType.VarChar, 100).Value = pobjPagina.Titulo;
        objComando.Parameters.Add("@DESCRICAO", SqlDbType.VarChar, 400).Value = pobjPagina.Descricao;                
        objComando.Parameters.Add("@METATAGSKEYWORDS", SqlDbType.VarChar).Value = pobjPagina.MetatagsKeywords;
        objComando.Parameters.Add("@METATAGSDESCRIPTION", SqlDbType.VarChar).Value = pobjPagina.MetatagsDescription;
        objComando.Parameters.Add("@CORMENU", SqlDbType.VarChar).Value = pobjPagina.CorMenu;
        objComando.Parameters.Add("@USUARIOID", SqlDbType.Int).Value = pobjPagina.Usuario.Id;
        objComando.Parameters.Add("@CATEGORIAID", SqlDbType.Int).Value = pobjPagina.CategoriaId;
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

    public static void AtualizarCategoria(Categoria pobjCategoria)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_CATEGORIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@CATEGORIAID", SqlDbType.Int).Value = pobjCategoria.IdCategoria;
        objComando.Parameters.Add("@DESCRICAO", SqlDbType.VarChar, 200).Value = pobjCategoria.Descricao;
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

    public static void AtualizarAbas(int paginaId, string configuracaoAbas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_PAGINA_ABAS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure      
        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = paginaId;
        objComando.Parameters.Add("@CONFIGURACAOABAS", SqlDbType.VarChar).Value = configuracaoAbas;

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

    #region  Obter
    /// <summary>
    /// Obter perfil pelo id
    /// </summary>
    /// <returns></returns>
    public static Pagina Obter(int pintIdPagina)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINAS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = pintIdPagina;

        try
        {
            objConexao.Open();

            Pagina objPagina = new Pagina();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objPagina.FromIDataReader(idrReader);
            }

            return objPagina;

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

    public static Categoria ObterCategoria(int pintId)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_CATEGORIA_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@categoriaId", SqlDbType.Int).Value = pintId;

        try
        {
            objConexao.Open();

            Categoria obj = new Categoria();

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

    public static Breadcrumb ObterBreadcrumb(int pintIdPagina)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_BREADCRUMB");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@paginaId", SqlDbType.Int).Value = pintIdPagina;

        try
        {
            //Abre Conexao
            objConexao.Open();

            Breadcrumb objRetorno = new Breadcrumb();

            SqlDataReader reader = objComando.ExecuteReader();
            while (reader.Read())
            {
                objRetorno.Titulo = reader[0].ToString();
                objRetorno.Descricao = reader[1].ToString();
                objRetorno.Breadcrumbs = reader[2].ToString();
            }

            return objRetorno;

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

    public static string ObterCorMenu(int pintIdPagina)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PAGINA_COR_MENU");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@paginaId", SqlDbType.Int).Value = pintIdPagina;

        try
        {
            //Abre conexão com o banco de dados
            objConexao.Open();

            IDataReader idrReader = objComando.ExecuteReader();

            if (idrReader.Read())
            {
                return idrReader["corMenu"].ToString();
            }
            else
                return string.Empty;
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