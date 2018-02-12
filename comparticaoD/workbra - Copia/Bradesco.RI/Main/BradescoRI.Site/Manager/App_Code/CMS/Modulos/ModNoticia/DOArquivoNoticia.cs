using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DOArquivoNoticia
/// </summary>
public class DOArquivoNoticia
{
    #region Obter Arquivos 
    public static List<ArquivoNoticia> ListaArquivosNoticia(Noticia pModNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_L_ARQUIVO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pModNoticia.ID;
        try
        {
            objConexao.Open();
            List<ArquivoNoticia> listArquivoNoticia = new List<ArquivoNoticia>();

            IDataReader idrReader = default(IDataReader);

            idrReader = objComando.ExecuteReader();

            while ((idrReader.Read()))
            {
                ArquivoNoticia obj = new ArquivoNoticia();
                obj.FromIDataReader(idrReader);
                listArquivoNoticia.Add(obj);
            }

            return listArquivoNoticia;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int Excluir(ArquivoNoticia pArquivoNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_D_ARQUIVO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@ArquivoNoticiaId", SqlDbType.Int).Value = pArquivoNoticia.ID;

        int retorno = 0;
        try
        {
            objConexao.Open();
            //Executa comando no banco de dados
            retorno = objComando.ExecuteNonQuery();

            return retorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static ArquivoNoticia Inserir(ArquivoNoticia pArquivoNoticia)
    {
        string strConectionString = ConfigurationManager.ConnectionStrings["BradescoRI"].ConnectionString;
        SqlConnection objConexao = new SqlConnection(strConectionString);

        SqlCommand objComando = new SqlCommand("SPE_I_ARQUIVO_NOTICIA");
        objComando.Connection = objConexao;
        objComando.CommandType = CommandType.StoredProcedure;

        objComando.Parameters.Add("@NoticiaId", SqlDbType.Int).Value = pArquivoNoticia.Noticia.ID;
        objComando.Parameters.Add("@NomeArquivo", SqlDbType.VarChar, 100).Value = pArquivoNoticia.Nome;
        objComando.Parameters.Add("@PathArquivo", SqlDbType.VarChar, 255).Value = pArquivoNoticia.PathArquivo;
        objComando.Parameters.Add("@ArquivoCapa", SqlDbType.Bit).Value = pArquivoNoticia.Capa;
        objComando.Parameters.Add("@ArquivoLista", SqlDbType.Bit).Value = pArquivoNoticia.Lista;
        objComando.Parameters.Add("@ArquivoDetalhe", SqlDbType.Bit).Value = pArquivoNoticia.Detalhe;
        objComando.Parameters.Add("@DataInclusao", SqlDbType.DateTime).Value = System.DateTime.Now;
        objComando.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = pArquivoNoticia.UsuarioInclusao.Id;
        objComando.Parameters.Add("@StatusId", SqlDbType.Int).Value = pArquivoNoticia.StatusId;
        objComando.Parameters.Add("@ArquivoNoticiaId", SqlDbType.Int).Direction = ParameterDirection.Output;
        try
        {
            objConexao.Open();
            //Executa comando no banco de dados
            objComando.ExecuteNonQuery();
            pArquivoNoticia.ID = int.Parse(objComando.Parameters["@ArquivoNoticiaId"].Value.ToString());
            return pArquivoNoticia;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}