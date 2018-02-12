using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

public class DOUsuario
{

    #region  Obter
    /// <summary>
    /// Obter usuário pelo login e senha
    /// </summary>
    /// <param name="pstrLogin">Login</param>
    /// <param name="pstrSenha">Senha</param>
    /// <returns></returns>
    public static Usuario ObterUsuarioLogin(string pstrLogin, string pstrSenha)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_USUARIO_LOGIN");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = pstrLogin;
        objComando.Parameters.Add("@senha", SqlDbType.VarChar, 50).Value = pstrSenha;

        try
        {
            objConexao.Open();

            Usuario objUsuario = new Usuario();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario.FromIDataReader(idrReader);
            }

            return objUsuario;

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

    public static Usuario ObterUsuarioEmail(string pstrEmail)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_USUARIO_EMAIL");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@EMAIL", SqlDbType.VarChar, 300).Value = pstrEmail;

        try
        {
            objConexao.Open();

            Usuario objUsuario = new Usuario();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario.FromIDataReader(idrReader);
            }

            return objUsuario;

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
    /// Obter usuário pelo id
    /// </summary>
    /// <param name="Id">Id Usuario</param>
    /// <param name="pstrSenha">Senha</param>
    /// <returns></returns>
    public static Usuario ObterUsuarioId(int Id)
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_USUARIO_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;

        try
        {
            objConexao.Open();

            Usuario objUsuario = new Usuario();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario.FromIDataReader(idrReader);
            }

            return objUsuario;

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
    /// Objeto Usuario
    /// </summary>
    /// <param name="pobjUsuario">Obejto Usuario</param>
    /// <returns></returns>
    public static int Atualizar(Usuario pobjUsuario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_USUARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pobjUsuario.Id;
        objComando.Parameters.Add("@PERFILID", SqlDbType.Int).Value = pobjUsuario.IdPerfil;
        objComando.Parameters.Add("@ATIVO", SqlDbType.Bit).Value = pobjUsuario.Ativo;
        objComando.Parameters.Add("@NOMEUSUARIO", SqlDbType.VarChar, 400).Value = pobjUsuario.Nome;
        objComando.Parameters.Add("@EMAILUSUARIO", SqlDbType.VarChar, 400).Value = pobjUsuario.Email;
        objComando.Parameters.Add("@LOGIN", SqlDbType.VarChar, 50).Value = pobjUsuario.Login;

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

    /// <summary>
    /// Atualiza último acesso do usuário
    /// </summary>
    /// <param name="pstrLogin">Login</param>
    /// <returns></returns>
    public static int AtualizarAcesso(string pstrLogin)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_USUARIO_ACESSO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = pstrLogin;


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

    /// <summary>
    /// Atualiza a senha do usuário
    /// </summary>
    /// <param name="pintId">ID</param>
    /// <param name="pstrSenha">Senha gerada pelo sistema</param>
    /// <returns></returns>
    public static int AtualizarSenha(int pintId, string pstrSenha)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_USUARIO_SENHA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@id", SqlDbType.Int).Value = pintId;
        objComando.Parameters.Add("@senha", SqlDbType.VarChar, 50).Value = pstrSenha;


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

    #region Inserir
    /// <summary>
    /// Objeto Usuario
    /// </summary>
    /// <param name="pobjUsuario">Obejto Usuario</param>
    /// <returns></returns>
    public static int Inserir(Usuario pobjUsuario)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_USUARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@PERFILID", SqlDbType.Int).Value = pobjUsuario.IdPerfil;
        objComando.Parameters.Add("@ATIVO", SqlDbType.Bit).Value = pobjUsuario.Ativo;
        objComando.Parameters.Add("@NOMEUSUARIO", SqlDbType.VarChar, 400).Value = pobjUsuario.Nome;
        objComando.Parameters.Add("@EMAILUSUARIO", SqlDbType.VarChar, 400).Value = pobjUsuario.Email;
        objComando.Parameters.Add("@LOGIN", SqlDbType.VarChar, 50).Value = pobjUsuario.Login;
        objComando.Parameters.Add("@SENHA", SqlDbType.VarChar, 100).Value = pobjUsuario.Senha;

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
    /// <summary>
    /// Obter usuário pelo login e senha
    /// </summary>
    /// <param name="pstrLogin">Login</param>
    /// <param name="pstrSenha">Senha</param>
    /// <returns></returns>
    public static List<Usuario> Listar(string pstrNome = null, string pstrEmail = null, string pstrLogin = null,
                                       int pintPerfilId = 0, bool? ativo = null)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_USUARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        if (pstrNome != string.Empty && pstrNome != null) objComando.Parameters.Add("@NOMEUSUARIO", SqlDbType.VarChar, 200).Value = pstrNome;
        if (pstrEmail != string.Empty && pstrEmail != null) objComando.Parameters.Add("@EMAILUSUARIO", SqlDbType.VarChar, 200).Value = pstrEmail;
        if (pstrLogin != string.Empty && pstrLogin != null) objComando.Parameters.Add("@LOGIN", SqlDbType.VarChar, 50).Value = pstrLogin;
        if (pintPerfilId > 0) objComando.Parameters.Add("@PERFILID", SqlDbType.Int).Value = pintPerfilId;
        if (ativo.HasValue) objComando.Parameters.Add("@ATIVO", SqlDbType.Bit).Value = ativo.Value;
       
        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Usuario> objListUsuario = new List<Usuario>();
            Usuario objUsuario = default(Usuario);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objUsuario = new Usuario();
                objUsuario.FromIDataReader(idrReader);
                objListUsuario.Add(objUsuario);
            }

            return objListUsuario;

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
    public static void Excluir(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_USUARIO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@ID", SqlDbType.Int).Value = pintId;

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