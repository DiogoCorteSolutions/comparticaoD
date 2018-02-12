using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOModEvento
/// </summary>
public class DOModEvento
{
    #region  Obter
   
    public static Evento Obter(int pintId, int pintIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_EVENTO_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@eventoId", SqlDbType.Int).Value = pintId;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdioma;

        try
        {
            objConexao.Open();

            Evento obj = new Evento();

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

    public static ModEventos ObterModulo()
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_EVENTOS_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        try
        {
            objConexao.Open();

            ModEventos obj = new ModEventos();

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

    #region Atualizar

    public static int Atualizar(Evento pobjEvento)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_U_EVENTO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@eventoId", SqlDbType.Int).Value = pobjEvento.IdEvento;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjEvento.IdIdioma;
        objComando.Parameters.Add("@tipoEventoId", SqlDbType.Int).Value = pobjEvento.IdTipoEvento;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjEvento.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 200).Value = pobjEvento.Descricao;
        objComando.Parameters.Add("@texto", SqlDbType.VarChar, 1000).Value = pobjEvento.Texto;
        if (!string.IsNullOrEmpty(pobjEvento.Responsavel)) objComando.Parameters.Add("@responsavel", SqlDbType.VarChar, 50).Value = pobjEvento.Responsavel;
        if (!string.IsNullOrEmpty(pobjEvento.Local)) objComando.Parameters.Add("@local", SqlDbType.VarChar, 100).Value = pobjEvento.Local;
        if (!string.IsNullOrEmpty(pobjEvento.Cidade)) objComando.Parameters.Add("@cidade", SqlDbType.VarChar, 50).Value = pobjEvento.Cidade;
        objComando.Parameters.Add("@dataInicio", SqlDbType.Date).Value = pobjEvento.DataInicio;
        if (pobjEvento.DataFim != null && pobjEvento.DataFim > DateTime.MinValue) objComando.Parameters.Add("@dataFim", SqlDbType.Date).Value = pobjEvento.DataFim;
        if (!string.IsNullOrEmpty(pobjEvento.Arquivo)) objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjEvento.Arquivo;

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

    public static int Inserir(Evento pobjEvento)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_EVENTO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjEvento.IdIdioma;
        objComando.Parameters.Add("@tipoEventoId", SqlDbType.Int).Value = pobjEvento.IdTipoEvento;
        objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pobjEvento.Titulo;
        objComando.Parameters.Add("@descricao", SqlDbType.VarChar, 200).Value = pobjEvento.Descricao;
        objComando.Parameters.Add("@texto", SqlDbType.VarChar, 1000).Value = pobjEvento.Texto;
        if (!string.IsNullOrEmpty(pobjEvento.Responsavel)) objComando.Parameters.Add("@responsavel", SqlDbType.VarChar, 50).Value = pobjEvento.Responsavel;
        if (!string.IsNullOrEmpty(pobjEvento.Local)) objComando.Parameters.Add("@local", SqlDbType.VarChar, 100).Value = pobjEvento.Local;
        if (!string.IsNullOrEmpty(pobjEvento.Cidade)) objComando.Parameters.Add("@cidade", SqlDbType.VarChar, 50).Value = pobjEvento.Cidade;
        objComando.Parameters.Add("@dataInicio", SqlDbType.Date).Value = pobjEvento.DataInicio;
        if (pobjEvento.DataFim != null && pobjEvento.DataFim > DateTime.MinValue) objComando.Parameters.Add("@dataFim", SqlDbType.Date).Value = pobjEvento.DataFim;


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

    public static int InserirArquivo(Evento pobjEvento)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_EVENTO_ARQUIVO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@eventoId", SqlDbType.Int).Value = pobjEvento.IdEvento;
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pobjEvento.IdIdioma;
        objComando.Parameters.Add("@arquivo", SqlDbType.VarChar, 200).Value = pobjEvento.Arquivo;

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

    public static int InserirModulo(ModEventos pobjModEvento)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_EVENTOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@urlListaEvento", SqlDbType.VarChar, 1000).Value = pobjModEvento.UrlListaEvento;
        objComando.Parameters.Add("@urlTodosEventos", SqlDbType.VarChar, 1000).Value = pobjModEvento.UrlTodosEventos;
        

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
    public static List<Evento> Listar(int pintIdIdioma, int pintIdTipoEvento = 0, string pstrTitulo = null, string pstrResponsavel= null, string pstrCidade = null, string pstrData = null)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_EVENTO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;
        if (!string.IsNullOrEmpty(pstrTitulo)) objComando.Parameters.Add("@titulo", SqlDbType.VarChar, 200).Value = pstrTitulo;
        if (!string.IsNullOrEmpty(pstrResponsavel)) objComando.Parameters.Add("@responsavel", SqlDbType.VarChar, 50).Value = pstrResponsavel;
        if (pintIdTipoEvento > 0) objComando.Parameters.Add("@tipoEventoId", SqlDbType.Int).Value = pintIdTipoEvento;
        if (!string.IsNullOrEmpty(pstrCidade)) objComando.Parameters.Add("@cidade", SqlDbType.VarChar, 50).Value = pstrCidade;
        if (!string.IsNullOrEmpty(pstrData)) objComando.Parameters.Add("@dataInicio", SqlDbType.DateTime).Value = Convert.ToDateTime(pstrData);

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Evento> objListEvento = new List<Evento>();
            Evento objEvento = default(Evento);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objEvento = new Evento();
                objEvento.FromIDataReader(idrReader);
                objListEvento.Add(objEvento);
            }

            return objListEvento;

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

    public static List<ProxEventos> ListarProxEventos(int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_PROXIMOS_EVENTOS");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<ProxEventos> objListProxEventos = new List<ProxEventos>();
            ProxEventos objProxEventos = default(ProxEventos);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                objProxEventos = new ProxEventos();
                objProxEventos.FromIDataReader(idrReader);
                objListProxEventos.Add(objProxEventos);
            }

            return objListProxEventos;

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

    public static List<TipoEvento> ListarTipoEvento(int pintIdIdioma)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_TIPO_EVENTO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<TipoEvento> objList = new List<TipoEvento>();
            TipoEvento obj = default(TipoEvento);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new TipoEvento();
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

    public static List<EventoMes> ListarEventosMes(int pintIdIdioma, DateTime pdttDataInicio, DateTime pdttDataFim)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_EVENTO_MES");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        ///Parametros
        objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = pintIdIdioma;
        objComando.Parameters.Add("@dataInicio", SqlDbType.Date).Value = pdttDataInicio;
        objComando.Parameters.Add("@dataFim", SqlDbType.Date).Value = pdttDataFim;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<EventoMes> objList = new List<EventoMes>();
            EventoMes obj = default(EventoMes);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new EventoMes();
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
    public static void Excluir(int pintId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_EVENTO");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@eventoId", SqlDbType.Int).Value = pintId;

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