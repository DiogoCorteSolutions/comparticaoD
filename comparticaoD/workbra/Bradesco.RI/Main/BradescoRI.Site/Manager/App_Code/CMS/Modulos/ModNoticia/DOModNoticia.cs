﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOMModNoticia
/// </summary>
public class DOModNoticia
{
    #region Obter
    public static Noticia Obter(Noticia pNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pNoticia.ID;

        if (pNoticia.StatusId != null)
            objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pNoticia.StatusId;

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
    public static Noticia Inserir(Noticia pobjNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = pobjNoticia.IdiomaId;
        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pobjNoticia.TipoArquivo.Id;
        objComando.Parameters.Add("@DataNoticia", SqlDbType.DateTime).Value = pobjNoticia.DataNoticia;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 512).Value = pobjNoticia.Titulo;
        objComando.Parameters.Add("@Resumo", SqlDbType.VarChar, 4096).Value = pobjNoticia.Resumo;
        objComando.Parameters.Add("@Integra", SqlDbType.VarChar).Value = pobjNoticia.Integra;
        objComando.Parameters.Add("@Fonte", SqlDbType.VarChar, 2048).Value = pobjNoticia.Fonte;
        objComando.Parameters.Add("@DataInclusao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@Destaque", SqlDbType.Bit).Value = pobjNoticia.Destaque;
        objComando.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = pobjNoticia.Usuario.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pobjNoticia.StatusId;
        objComando.Parameters.Add("@ArquivoCapaId", SqlDbType.Int).Value = pobjNoticia.Capa.Id;
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

    public static int Excluir(int pModNoticiaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_MODULO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pModNoticiaId;
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

    public static List<Noticia> Listar(Noticia pNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        if (pNoticia != null && pNoticia.TipoNoticia != null)
            objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pNoticia.TipoNoticia.ID;

        try
        {
            //Abre Conexao
            objConexao.Open();

            //Declara variavel de retorno           
            List<Noticia> objList = new List<Noticia>();
            Noticia obj = default(Noticia);

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                obj = new Noticia();
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
        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pNoticia.TipoArquivo.Id;
        objComando.Parameters.Add("@DataNoticia", SqlDbType.DateTime).Value = pNoticia.DataNoticia;
        objComando.Parameters.Add("@Titulo", SqlDbType.VarChar, 512).Value = pNoticia.Titulo;
        objComando.Parameters.Add("@Resumo", SqlDbType.VarChar, 4096).Value = pNoticia.Resumo;
        objComando.Parameters.Add("@Integra", SqlDbType.VarChar).Value = pNoticia.Integra;
        objComando.Parameters.Add("@Fonte", SqlDbType.VarChar, 2048).Value = pNoticia.Fonte;
        objComando.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@Destaque", SqlDbType.Bit).Value = pNoticia.Destaque;
        objComando.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = pNoticia.Usuario.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pNoticia.StatusId;
        objComando.Parameters.Add("@ArquivoCapaId", SqlDbType.Int).Value = pNoticia.Capa.Id;
        //objComando.Parameters.Add("@arquivoDetalheId", SqlDbType.Int).Value = pNoticia.Detalhe.Id;
        //objComando.Parameters.Add("@arquivoListagemId", SqlDbType.Int).Value = pNoticia.Listagem.Id;
        objComando.Parameters.Add("@SubTipoNoticiaId", SqlDbType.Int).Value = pNoticia.TipoNoticia.ID;

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

    public static ModNoticia ObterNoticiaModulo(int idConteudo, int pTipoNoticiaId)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_NOTICIA_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = idConteudo;
        objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pTipoNoticiaId;

        try
        {
            objConexao.Open();

            ModNoticia obj = new ModNoticia();

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

    #region Obter Modulo Noticia pelo tipo de notícia
    public static List<ModNoticia> ListarNoticiasModulos(int pintId, int? pTipoNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_MODULO_NOTICIA_ID");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pintId;

        if (pTipoNoticia != null)
            objComando.Parameters.Add("@TipoNoticiaId", SqlDbType.Int).Value = pTipoNoticia;



        try
        {
            objConexao.Open();

            List<ModNoticia> lModNoticia = new List<ModNoticia>();
            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                ModNoticia obj = new ModNoticia();
                obj.FromIDataReader(idrReader);
                lModNoticia.Add(obj);
            }

            return lModNoticia;

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

    #region Inserir Modulo Noticia
    public static int Inserir(ModNoticia pobjModNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_MODULO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        //Define parametros da procedure               
        objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = pobjModNoticia.ID;
        objComando.Parameters.Add("@noticiaId", SqlDbType.Int).Value = pobjModNoticia.IdNoticia;
        objComando.Parameters.Add("@tipoNoticiaId", SqlDbType.Int).Value = pobjModNoticia.TipoNoticiaId;
        objComando.Parameters.Add("@tipoArquivoId", SqlDbType.Int).Value = pobjModNoticia.TipoArquivoId;
        objComando.Parameters.Add("@home", SqlDbType.Bit).Value = pobjModNoticia.Home;
        objComando.Parameters.Add("@listagem", SqlDbType.Bit).Value = pobjModNoticia.Listagem;
        objComando.Parameters.Add("@destaque", SqlDbType.Bit).Value = pobjModNoticia.Destaque;

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