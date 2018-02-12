using System;
using BDN_Cripto;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Web.UI;
namespace Utilitarios
{
    #region Enums
    /// <summary>
    /// Enum para os Tipos de Log
    /// </summary>
    public enum TipoLog
    {
        Usuario = 1
       , Sistema = 2
    }

    /// <summary>
    /// Tipo da Transacao
    /// </summary>
    public enum TipoTransacao
    {
        Carregar = 0
        , Salvar = 1
        , Limpar = 2
    }

    public enum StatusArquivo
    {
        Ativo = 0,
        Inativo = 1
    }

    public enum Status
    {
        Criado = 0,
        Aprovado = 2
    }

    public enum StatusPagina
    {
        Construção = 0,
        PendenteAprovacao = 1,
        Publicado = 2
    }

    public enum TipoArquivo
    {
        Noticias = 0
    }

    public enum TipoNoticia
    {
        Esquerdo = 1,
        DireitoSuperior = 2,
        DireitoCentral = 3,
        DireitoInferior = 4
    }
    public enum Idioma
    {
        Portugues = 1,
        Ingles = 2
    }

    public enum TipoLayoutArquivo
    {
        BoxComCheck = 0,
        BoxSemCheck = 1,
        BoxPodCast = 2,
        BoxVideos = 3
    }

    #endregion

    #region Criptografia
    public static class CriptografiaSeguranca
    {
        public static string GerarSenha()
        {
            string strValC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strValN = "0123456789";
            string result;

            int i = 0;
            Random random = new Random();

            result = string.Empty;


            for (i = 0; i < 6; i++)
            {
                if ((i == 2) || (i == 4))
                {
                    result += strValN.Substring(random.Next(strValN.Length), 1);
                }
                else
                {
                    result += strValC.Substring(random.Next(strValC.Length), 1);
                }
            }
            return result;
        }

        public static string Rc4(string strMessage)
        {
            string strKey = ConfigurationManager.AppSettings["BradescoRI.ChaveLogin"];
            int i = 0;
            int j = 0;
            int k = 0;
            int intLength = strKey.Length;
            int[] key = new int[256];
            int[] sbox = new int[256];
            int tempSwap;
            int temp;
            string cipher = "";
            int cipherby;

            for (int lngArrayA = 0; lngArrayA <= 255; lngArrayA++)
            {
                key[lngArrayA] = Convert.ToInt32(Convert.ToChar(strKey.Substring((lngArrayA % intLength), 1)));
                sbox[lngArrayA] = lngArrayA;
            }

            int lngArrayB = 0;
            for (int lngArrayA = 0; lngArrayA <= 255; lngArrayA++)
            {
                lngArrayB = ((lngArrayB + sbox[lngArrayA] + key[lngArrayA]) % 256);
                tempSwap = sbox[lngArrayA];
                sbox[lngArrayA] = sbox[lngArrayB];
                sbox[lngArrayB] = tempSwap;
            }

            for (int a = 0; a < strMessage.Length; a++)
            {
                i = (i + 1) % 256;
                j = (j + sbox[i]) % 256;
                temp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = temp;

                k = sbox[(sbox[i] + sbox[j]) % 256];

                cipherby = Convert.ToInt32(Convert.ToChar(strMessage.Substring(a, 1))) ^ k;
                cipher = cipher.ToString() + ((char)cipherby).ToString();
            }

            return cipher;
        }
    }
    #endregion

    #region Email

    namespace Email
    {
        #region "SendMail"
        /// <summary>
        /// Classe responsável pelo mecanisco de envio de e-mail.
        /// </summary>
        public class SendMail
        {
            /// <summary>
            /// Envia e-mail de forma que a aplicação fique aguardando o término do envio.
            /// </summary>
            /// <param name="mailMessage"></param>
            public SendMail(MailMessage mailMessage)
            {
                new SendMail(mailMessage, true);
            }

            /// <summary>
            /// Envia o e-mail abrindo uma thread, ou seja, a aplicação não aguarda o envio do e-mail, o envio se torna independente da aplicação.
            /// </summary>
            /// <param name="mailMessage"></param>
            /// <param name="thread"></param>
            public SendMail(MailMessage mailMessage, bool thread)
            {
                if (thread)
                {
                    Thread tr = new Thread(new ParameterizedThreadStart(StartSendMail));
                    tr.Start(mailMessage);
                }
                else
                {
                    StartSendMail(mailMessage);
                }
            }

            static public string defaultSenderEmail = ConfigurationManager.AppSettings["BradescoRI.RemetentePadrao"];

            private void StartSendMail(object param)
            {
                MailMessage DadosEmail = (MailMessage)param;

                if (DadosEmail.Dicionary.Count > 0)
                    ReadTemplate(DadosEmail);

                //Para evitar problemas com caracteres especiais configuramos o Charset            
                DadosEmail.BodyEncoding = Encoding.GetEncoding("utf-8");

                // Adiciona Remetente
                DadosEmail.From = new MailAddress(defaultSenderEmail);

                if (!String.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["BradescoRI.DestinatarioPadrao"]))
                {
                    DadosEmail.To.Clear();
                    DadosEmail.To.Add(System.Configuration.ConfigurationManager.AppSettings["BradescoRI.DestinatarioPadrao"]);
                }


                //cria o eto SMTP, pega as configurações do App.config ou Web.Config
                //SmtpClient SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["BradescoRI.HostPadrao"]);

                SmtpClient SmtpClient = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient.EnableSsl = true;
                SmtpClient.Credentials = new System.Net.NetworkCredential("XXX", "XXXX");

                try
                {
                    SmtpClient.Send(DadosEmail);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                DadosEmail.Dispose();
            }


            private void ReadTemplate(MailMessage mailMessage)
            {
                StreamReader sr = new StreamReader(mailMessage.PathTemplate, Encoding.Default);
                string content = sr.ReadToEnd();
                sr.Close();

                foreach (DictionaryEntry item in mailMessage.Dicionary)
                {
                    content = content.Replace(item.Key.ToString(), item.Value.ToString());
                }

                mailMessage.Body = content;
            }
        }
        #endregion

        #region "MailMessage"
        /// <summary>
        /// Classe responsável herdada do (System.Net.Mail.MailMessage), com a algumas expansões
        /// </summary>
        public class MailMessage : System.Net.Mail.MailMessage
        {
            private IDictionary _dictionary = new Dictionary<string, string>();
            private string _pathTemplate = string.Empty;

            public MailMessage()
            {
                //Contructor
            }

            /// <summary>
            /// Propriedade responsável pela implementação da funcionalidade de substituição de palavras chaves no Template por parametros da aplicação.
            /// </summary>
            /// <example> This sample shows how to call the GetZero method.
            /// <code>
            ///   MailMessage msg = new MailMessage();
            ///   msg.PathTemplate = "C:\Template\template.htm";
            ///   msg.Dicionary.Add("#nome#", variavel);
            /// </code>
            /// </example>
            public IDictionary Dicionary
            {
                get { return this._dictionary; }
            }

            /// <summary>
            /// Propriedade responsável em indicar o caminho físico do Template de e-mail para a aplicação.
            /// </summary>
            /// <example> This sample shows how to call the GetZero method.
            /// <code>
            ///   MailMessage msg = new MailMessage();
            ///   msg.PathTemplate = "C:\Template\template.htm";
            /// </code>
            /// </example>
            public string PathTemplate
            {
                get { return this._pathTemplate; }
                set { this._pathTemplate = value; }
            }
        }
        #endregion
    }

    #endregion

    #region Validação

    namespace Validacao
    {
        public class Campos
        {
            #region CPF
            /// <summary>
            /// Valida se CPF é válido
            /// </summary>
            /// <param name="cpf"></param>
            /// <returns></returns>
            public static bool ValidaCPF(string cpf)
            {
                string valor = cpf.Replace(".", "");
                valor = valor.Replace("-", "");

                if (valor.Length != 11)
                    return false;

                bool igual = true;

                for (int i = 1; i < 11 && igual; i++)
                    if (valor[i] != valor[0])
                        igual = false;

                if (igual || valor == "12345678909")
                    return false;

                int[] numeros = new int[11];

                for (int i = 0; i < 11; i++)
                    numeros[i] = int.Parse(
                      valor[i].ToString());

                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += (10 - i) * numeros[i];

                int resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                        return false;
                }

                else if (numeros[9] != 11 - resultado)
                    return false;

                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += (11 - i) * numeros[i];

                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                        return false;
                }
                else
                    if (numeros[10] != 11 - resultado)
                    return false;

                return true;

            }
            #endregion

            #region Senha
            /// <summary>
            /// Valida a Senha
            /// </summary>
            /// <param name="senha"></param>
            /// <returns></returns>
            public static bool ValidaSenha(string senha)
            {
                //ter entre 6 e 8 caracteres
                if (senha.Length < 6 || senha.Length > 8)
                    return false;

                //ter pelo menos 1 número
                Regex numeros = new Regex("[0-9]");
                if (numeros.Matches(senha).Count < 1)
                    return false;

                //ter pelo menos 1 caracter especial
                Regex especial = new Regex("[^a-zA-Z0-9]");
                if (especial.Matches(senha).Count < 1)
                    return false;

                //ter pelo menos 2 letras
                Regex letras = new Regex("[a-zA-Z]");
                if (letras.Matches(senha).Count < 2)
                    return false;

                return true;

            }

            public static bool ValidaSenha(string senhaNova, string senhaAntiga)
            {
                //ter entre 6 e 8 caracteres
                if (senhaNova.Length < 6 || senhaNova.Length > 8)
                    return false;

                //ter pelo menos 1 número
                Regex numeros = new Regex("[0-9]");
                if (numeros.Matches(senhaNova).Count < 1)
                    return false;

                //ter pelo menos 1 caracter especial
                Regex especial = new Regex("[^a-zA-Z0-9]");
                if (especial.Matches(senhaNova).Count < 1)
                    return false;

                //ter pelo menos 2 letras
                Regex letras = new Regex("[a-zA-Z]");
                if (letras.Matches(senhaNova).Count < 2)
                    return false;


                //verifica se possui pelo menos 3 caracteres diferentes da senha atual
                int contador = 0;

                foreach (char c in senhaNova)
                {
                    if (!senhaAntiga.Contains(c.ToString()))
                        contador++;

                    if (contador >= 3)
                        return true;
                }

                if (contador < 3)
                    return false;

                return true;

            }
            #endregion

            #region CNPJ
            /// <summary>
            /// Método para validação de CNPJ
            /// </summary>
            /// <param name="cnpj"></param>
            /// <returns></returns>
            public static bool ValidaCNPJ(string cnpj)
            {
                if (String.IsNullOrEmpty(cnpj))
                {
                    return false;
                }

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;

                cnpj = cnpj.Trim().Replace(".", String.Empty).Replace("-", String.Empty).Replace("/", String.Empty);

                if (cnpj.Length != 14 || cnpj == "00000000000000")
                {
                    return false;
                }

                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;

                for (int i = 0; i < 12; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;

                for (int i = 0; i < 13; i++)
                {
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                }

                resto = (soma % 11);

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                return cnpj.EndsWith(digito);
            }
            #endregion

            #region ContaCorrente

            public static bool ValidaDigitoConta(string dv_conta, int dv_informado)
            {
                // Valida Numero da Conta
                int lsoma = 0;
                int ipeso = 2;
                int tam = dv_conta.Length;
                string[] conta = new string[tam + 1];
                int i = 0;

                for (i = 0; i <= tam - 1; i++)
                {
                    conta[i] = dv_conta.Substring(i, 1);
                }

                int digito = 0;

                while (tam > 0)
                {
                    tam = tam - 1;
                    digito = Convert.ToInt32(conta[tam]);
                    if (digito >= 0 & digito <= 9)
                    {
                        lsoma = lsoma + digito * ipeso;
                        ipeso = ipeso + 1;
                        if (ipeso > 7)
                        {
                            ipeso = 2;
                        }
                    }
                }

                lsoma = lsoma % 11;
                lsoma = 11 - lsoma;

                if (lsoma == 11 | lsoma == 10)
                {
                    lsoma = 0;
                }

                return lsoma == dv_informado;
            }

            #endregion
        }
    }
    #endregion

    #region Formatação

    namespace Formatacao
    {
        public class campos
        {
            #region CPF
            /// <summary>
            /// Formata CPF para o formato 999.999.999-99
            /// </summary>
            /// <param name="cpf">CPF</param>
            /// <returns></returns>
            public static string FormataCpf(string cpf)
            {
                try
                {
                    return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
                }
                catch
                {
                    return cpf;
                }
            }
            #endregion

            #region Data
            public static DateTime TrataData(string dt)
            {
                DateTime data = new DateTime();
                DateTime.TryParse(dt, out data);
                return data;
            }
            #endregion
        }
    }

    #endregion

    #region Excel
    public static class Excel
    {
        public static void CriarExcel(HttpResponse Response, System.Data.DataTable data, string pstrNomeArquivo)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=" + pstrNomeArquivo);

            // Remover caracteres do header - Content-Type
            Response.Charset = "iso-8859-1";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            Response.HeaderEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            System.Web.UI.WebControls.GridView Dados = new System.Web.UI.WebControls.GridView();
            Dados.DataSource = data;
            Dados.DataBind();

            Dados.RenderControl(hw);
            Response.Write(tw.ToString());

            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
    #endregion

    #region Csv
    public static class Csv
    {
        public static void CriarCsv(HttpResponse Response, System.Data.DataTable dataTable, string pstrNomeArquivo)
        {
            String path = Path.GetTempPath() + HttpContext.Current.Session.SessionID;
            TextWriter textWriter = new StreamWriter(path, false, Encoding.Unicode);
            String cabecalhos = String.Empty;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (i > 0)
                {
                    cabecalhos += ";";
                }
                string coluna = dataTable.Columns[i].ColumnName;

                cabecalhos += coluna;
            }

            textWriter.WriteLine(cabecalhos);

            String linha;
            foreach (DataRow row in dataTable.Rows)
            {
                linha = String.Empty;

                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        linha += ";";
                    }

                    string linhaOk = row[i].ToString();
                    linha += String.Format("{0}", linhaOk.Replace("\"", ""));
                }

                textWriter.WriteLine(linha);
            }

            textWriter.Close();
            HttpExtensions.ForceDownloadRealPath(HttpContext.Current.Response, path, pstrNomeArquivo);
        }

        public static void CriarCsvOcorrencia(HttpResponse Response, System.Data.DataTable dataTable, string pstrNomeArquivo)
        {
            String path = Path.GetTempPath() + HttpContext.Current.Session.SessionID;
            TextWriter textWriter = new StreamWriter(path, false, Encoding.Unicode);
            String cabecalhos = String.Empty;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (i > 0)
                {
                    cabecalhos += ";";
                }
                string coluna = dataTable.Columns[i].ColumnName;

                cabecalhos += coluna;
            }

            textWriter.WriteLine(cabecalhos);

            String linha;
            foreach (DataRow row in dataTable.Rows)
            {
                linha = String.Empty;
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        linha += ";";
                    }
                    linha += String.Format("\"{0}\"", row[i].ToString().Replace("\"", "'").Replace("\r\n", ""));
                }
                textWriter.WriteLine(linha);
            }

            textWriter.Close();
            HttpExtensions.ForceDownloadRealPath(HttpContext.Current.Response, path, pstrNomeArquivo);
        }
    }
    #endregion

    #region Tamanho arquivo
    public static class TamanhoArquivo
    {
        public static string Amigavel(long bytes)
        {
            if (bytes < 0) throw new ArgumentException("bytes");

            double humano;
            string sufixo;

            // Exabyte (1024^6)
            if (bytes >= 1152921504606846976L)
            {
                humano = bytes >> 50;
                sufixo = "EB";
            }
            // Petabyte (1024^5)
            else if (bytes >= 1125899906842624L)
            {
                humano = bytes >> 40;
                sufixo = "PB";
            }
            // Terabyte (1024^4)
            else if (bytes >= 1099511627776L)
            {
                humano = bytes >> 30;
                sufixo = "TB";
            }
            // Gigabyte (1024^3)
            else if (bytes >= 1073741824)
            {
                humano = bytes >> 20;
                sufixo = "GB";
            }
            // Megabyte (1024^2)
            else if (bytes >= 1048576)
            {
                humano = bytes >> 10;
                sufixo = "MB";
            }
            // Kilobyte (1024^1)
            else if (bytes >= 1024)
            {
                humano = bytes;
                sufixo = "KB";
            }
            // Byte
            else return bytes.ToString("0 B");

            humano /= 1024;
            return humano.ToString("0.## ") + sufixo;
        }
    }

    #endregion
}
