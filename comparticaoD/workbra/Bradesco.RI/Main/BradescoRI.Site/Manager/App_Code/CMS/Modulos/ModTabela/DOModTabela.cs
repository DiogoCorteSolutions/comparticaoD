using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOModTabela
/// </summary>
public class DOModTabela
{
    #region  Obter
    public static Tabela Obter()
    {

        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_S_MODTABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            objConexao.Open();

            Tabela obj = new Tabela();

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

    //public static int Inserir(Tabela pobjTabela)
    //{
    //    string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
    //    SqlConnection objConexao = new SqlConnection(strConectionString);

    //    SqlCommand objComando = new SqlCommand("SPE_I_MODTABELA");
    //    objComando.Connection = objConexao;
    //    objComando.CommandType = CommandType.StoredProcedure;

    //    //Define parametros da procedure               
    //    objComando.Parameters.Add("@emMilhares", SqlDbType.Int).Value = pobjTabela.EmMilhares;
    //    objComando.Parameters.Add("@marco2017", SqlDbType.Int).Value = pobjTabela.Marco2017;
    //    objComando.Parameters.Add("@dezembro2017", SqlDbType.VarChar, 200).Value = pobjTabela.Dezembro2017;
    //    objComando.Parameters.Add("@marco2016", SqlDbType.VarChar, -1).Value = pobjTabela.Marco2016;


    //    try
    //    {
    //        //Abre conexão com o banco de dados
    //        objConexao.Open();

    //        //Declara variavel de retorno
    //        int intRetorno = 0;

    //        //Executa comando no banco de dados
    //        intRetorno = objComando.ExecuteNonQuery();

    //        return intRetorno;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;

    //    }
    //    finally
    //    {
    //        //Fecha a conexao se aberta
    //        if (objConexao.State != ConnectionState.Closed)
    //        {
    //            objConexao.Close();
    //        }
    //    }
    //}
    #endregion

    #region InserirTabela

    public static int InserirTabela(Tabela jobTabela)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_TB_TABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@NomeColuna", SqlDbType.VarChar, 100).Value = jobTabela.NomeAcionario;

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

    #endregion

    #region InserirColuna

    public static int InserirColuna(Tabela joTabela)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_TB_COLUNAS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@NomeColuna", SqlDbType.VarChar, 100).Value = joTabela.NomeColuna;
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = joTabela.IdModTabela;

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

    #region ListaColuna

    public static List<Tabela> ListarPorId(int SetModTabelas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = SetModTabelas;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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

    #region ListaValorCol
    public static List<Tabela> ListarPorIdFind(int SetModTabelas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TABELA01");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = SetModTabelas;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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

    #region listaColunaFind
    public static List<Tabela> ListaColunaFind(int SetModTabelas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TABELAVALORCOLUNA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = SetModTabelas;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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


    #region AddValorColuna
    public static int InserirValorColuna(VColuna pobjModTabela)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODVALORCOLUNATABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@IdColunaTabela", SqlDbType.Int).Value = pobjModTabela.IdColunaTabela;
        objComando.Parameters.Add("@ValorColuna", SqlDbType.VarChar, 300).Value = pobjModTabela.ValorColuna;
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = pobjModTabela.IdModTabela;



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

    public static List<Tabela> ListarPorColId(int SetModTabelas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_COLTABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = SetModTabelas;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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


    public static List<Lista> ListarColunasPorId(int SetModTabelas)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_COLUNATABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = SetModTabelas;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Lista> objList = new List<Lista>();
            Lista obj = default(Lista);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Lista();
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

    
    #region ListaColuna

    public static List<Tabela> ListarPorIdMoTabela(int idModTabela)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TB_COLUNASPID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure
        objComando.Parameters.Add("@IdModTabela", SqlDbType.Int).Value = idModTabela;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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
    public static List<Tabela> Listar()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TBMODTABELA_TBS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;


        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
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
    #region Listar
    //public static List<Tabela> Listar(int pintIdConteudo, int pintIdIdioma)
    //{
    //    string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
    //    SqlConnection objConexao = new SqlConnection(strConectionString);

    //    SqlCommand objComando = new SqlCommand("SPE_L_MODTABELA");
    //    objComando.Connection = objConexao;
    //    objComando.CommandType = CommandType.StoredProcedure;

    //    //Define parametros da procedure
    //    objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintIdConteudo;
    //    objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

    //    try
    //    {
    //        //Abre Conexao
    //        objConexao.Open();

    //        //Declara variavel de retorno           
    //        List<Tabela> objList = new List<Tabela>();
    //        Tabela obj = default(Tabela);

    //        IDataReader idrReader = default(IDataReader);

    //        idrReader = objComando.ExecuteReader();

    //        while ((idrReader.Read()))
    //        {
    //            obj = new Tabela();
    //            obj.FromIDataReader(idrReader);
    //            objList.Add(obj);
    //        }

    //        return objList;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;

    //    }
    //    finally
    //    {
    //        //Fecha a conexao se aberta
    //        if (objConexao.State != ConnectionState.Closed)
    //        {
    //            objConexao.Close();
    //        }
    //    }
    //}
    #endregion
    #endregion

    #region setid

    public static int setIdTable(int IdModTabela)
    {
        int idTabela = IdModTabela;

        return idTabela;
    }
    #endregion


    #region listUId
    public static List<Tabela> ListarUTB()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_UTABELA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;


        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Tabela> objList = new List<Tabela>();
            Tabela obj = default(Tabela);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Tabela();
                obj.IdModTabela = obj.IdModTabela;
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
}