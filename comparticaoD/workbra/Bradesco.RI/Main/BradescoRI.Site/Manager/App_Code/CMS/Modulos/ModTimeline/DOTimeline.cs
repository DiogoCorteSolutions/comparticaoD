using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOTimeline
/// </summary>
public class DOTimeline
{
    public DOTimeline()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<Timeline> Listar(int Id = 0, int Idioma = 1)
    {

        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);
        try
        {

            using (var objComando = new SqlCommand("SPE_L_Modulo_Timeline"))
            {
                objComando.Connection = objConexao;
                objComando.CommandType = CommandType.StoredProcedure;
                if (Id > 0)
                {
                    objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                    objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = Idioma;

                }

                //Abre Conexao
                objConexao.Open();

                //Declara variavel de retorno           
                var objList = new List<Timeline>();
                var obj = default(Timeline);

                var idrReader = default(IDataReader);

                idrReader = objComando.ExecuteReader();

                while ((idrReader.Read()))
                {
                    obj = new Timeline();
                    obj.FromIDataReader(idrReader);
                    objList.Add(obj);
                }

                return objList;
            }

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
    //public static List<Timeline> Listar()
    //{

    //    var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
    //    var objConexao = new SqlConnection(strConectionString);
    //    try
    //    {

    //        using (var objComando = new SqlCommand("SPE_L_MODULO_TIMELINE_VERSAOMODULOS"))
    //        {
    //            objComando.Connection = objConexao;
    //            objComando.CommandType = CommandType.StoredProcedure;


    //                objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
    //                objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = Idioma;



    //            //Abre Conexao
    //            objConexao.Open();

    //            //Declara variavel de retorno           
    //            var objList = new List<Timeline>();
    //            var obj = default(Timeline);

    //            var idrReader = default(IDataReader);

    //            idrReader = objComando.ExecuteReader();

    //            while ((idrReader.Read()))
    //            {
    //                obj = new Timeline();
    //                obj.FromIDataReader(idrReader);
    //                objList.Add(obj);
    //            }

    //            return objList;
    //        }

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

    public static Timeline Obter(int Id, int Idioma = 1)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        try
        {
            using (var objComando = new SqlCommand("SPE_L_Modulo_Timeline"))
            {
                objComando.Connection = objConexao;
                objComando.CommandType = CommandType.StoredProcedure;

                objComando.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = Idioma;
                objConexao.Open();

                var obj = new Timeline();

                var idrReader = default(IDataReader);

                idrReader = objComando.ExecuteReader();

                while ((idrReader.Read()))
                {
                    obj.FromIDataReader(idrReader);
                }

                return obj;
            }
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

    public static int Inserir(Timeline item)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        using (var objComando = new SqlCommand("SPE_I_TIMELINE"))
        {
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;

            objComando.Parameters.Add("@conteudoId", SqlDbType.Int).Value = item.conteudoId;
            objComando.Parameters.Add("@idiomaId", SqlDbType.Int).Value = item.Idioma;
            objComando.Parameters.Add("@ano", SqlDbType.VarChar).Value = item.Ano;
            objComando.Parameters.Add("@titulo", SqlDbType.VarChar).Value = item.Titulo;
            objComando.Parameters.Add("@imagem", SqlDbType.VarChar).Value = item.Imagem;
            objComando.Parameters.Add("@texto", SqlDbType.VarChar).Value = item.Texto;
            objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = item.statusId;

            try
            {
                //Abre conexão com o banco de dados
                objConexao.Open();

                //Executa comando no banco de dados
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

    }

    public static int Alterar(Timeline item)
    {

        var retorno = 0;

        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        using (var objComando = new SqlCommand("SPE_U_Modulo_Timeline"))
        {
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;

            //Define parametros da procedure               
            objComando.Parameters.Add("@IdiomaId", SqlDbType.Int).Value = item.Idioma;
            objComando.Parameters.Add("@ConteudoId", SqlDbType.Int).Value = item.Id;
            objComando.Parameters.Add("@Ano", SqlDbType.VarChar).Value = item.Ano;
            objComando.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = item.Titulo;
            objComando.Parameters.Add("@Imagem", SqlDbType.VarChar).Value = item.Imagem;
            objComando.Parameters.Add("@Texto", SqlDbType.VarChar).Value = item.Texto;
            //objComando.Parameters.Add("@TimelineId", SqlDbType.Int).Value = item.TimelineId;


            try
            {
                objConexao.Open();
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
    }

    public static int Excluir(Timeline item)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        using (var objComando = new SqlCommand("SPE_D_Modulo_Timeline"))
        {
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;

            //Define parametros da procedure               
            objComando.Parameters.Add("@TimelineId", SqlDbType.Int).Value = item.Id;
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
    }

    
}



