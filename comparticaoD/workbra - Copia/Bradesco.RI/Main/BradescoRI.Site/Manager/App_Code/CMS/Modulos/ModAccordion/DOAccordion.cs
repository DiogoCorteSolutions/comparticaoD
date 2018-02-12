using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DOAccordion
/// </summary>
public class DOAccordion
{    
    public static List<Accordions> Listar(int conteudoId)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        try
        {
            using (var objComando = new SqlCommand("SPE_L_MODULO_ACCORDION"))
            {
                objComando.Connection = objConexao;
                objComando.CommandType = CommandType.StoredProcedure;

                objComando.Parameters.Add("@CONTEUDOID", SqlDbType.Int).Value = conteudoId;

                //Abre Conexao
                objConexao.Open();

                //Declara variavel de retorno           
                var objList = new List<Accordions>();
                var obj = default(Accordions);

                var idrReader = default(IDataReader);

                idrReader = objComando.ExecuteReader();

                while ((idrReader.Read()))
                {
                    obj = new Accordions();
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

    public static Accordions Obter(int conteudoId)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        try
        {
            using (var objComando = new SqlCommand("SPE_L_MODULO_ACCORDION"))
            {
                objComando.Connection = objConexao;
                objComando.CommandType = CommandType.StoredProcedure;

                objComando.Parameters.Add("@CONTEUDOID", SqlDbType.Int).Value = conteudoId;

                objConexao.Open();

                var obj = new Accordions();

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

    public static int Inserir(int ConteudoId, string Titulo, int PaginaId, int ModuloId, Boolean painelAberto)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        using (var objComando = new SqlCommand("SPE_I_MODULO_ACCORDION"))
        {
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;            
            objComando.Parameters.Add("@CONTEUDOID", SqlDbType.Int).Value = ConteudoId;
            objComando.Parameters.Add("@TITULO", SqlDbType.VarChar).Value = Titulo;
            objComando.Parameters.Add("@PAGINAID", SqlDbType.Int).Value = PaginaId;
            objComando.Parameters.Add("@MODULOID", SqlDbType.Int).Value = ModuloId;
            objComando.Parameters.Add("@PAINELABERTO", SqlDbType.Bit).Value = painelAberto;

            try
            {
                //Abre conexão com o banco de dados
                objConexao.Open();

                //Executa comando no banco de dados
                return  objComando.ExecuteNonQuery();                                
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

    public static int Atualizar(int ConteudoId, string Titulo, Boolean painelAberto)
    {
        var strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        var objConexao = new SqlConnection(strConectionString);

        using (var objComando = new SqlCommand("SPE_U_MODULO_ACCORDION"))
        {
            objComando.Connection = objConexao;
            objComando.CommandType = CommandType.StoredProcedure;
            objComando.Parameters.Add("@CONTEUDOID", SqlDbType.Int).Value = ConteudoId;
            objComando.Parameters.Add("@TITULO", SqlDbType.VarChar).Value = Titulo;
            objComando.Parameters.Add("@PAINELABERTO", SqlDbType.Bit).Value = painelAberto;

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
}