using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModIndicesMundiais
/// </summary>
[Serializable()]
public class ModIndicesMundiais
{
    CultureInfo objCultureInfo = new CultureInfo("en-US");

    #region Propriedades
    public string Papel { get; set; }
    public string Descricao { get; set; }
    public double UltimaCotacao { get; set; }
    public string Percentual { get; set; }
    public DateTime Data { get; set; }
    public string Hora { get; set; }
    #endregion

    #region FromIDataReader
    public void FromIDataReader(IDataReader pobjIDataReader)
    {
        if (pobjIDataReader == null)
        {
            return;
        }

        if ((!object.ReferenceEquals(pobjIDataReader["codigo"], DBNull.Value)))
        {
            this.Papel = Convert.ToString(pobjIDataReader["codigo"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["nome"], DBNull.Value)))
        {
            this.Descricao = Convert.ToString(pobjIDataReader["nome"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["ultimo"], DBNull.Value)))
        {
            double dblultimaCotacao = 0.0;

            try
            {

                dblultimaCotacao = Convert.ToDouble(pobjIDataReader["ultimo"], objCultureInfo);
            }
            catch { }

            this.UltimaCotacao = dblultimaCotacao;
        }
        if ((!object.ReferenceEquals(pobjIDataReader["variacao"], DBNull.Value)))
        {
            this.Percentual = Convert.ToString(pobjIDataReader["variacao"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["hora"], DBNull.Value)))
        {
            this.Hora = Convert.ToString(pobjIDataReader["hora"]);
        }
        if ((!object.ReferenceEquals(pobjIDataReader["data"], DBNull.Value)))
        {
            DateTime datDataCotacao = new DateTime();

            try
            {
                datDataCotacao = Convert.ToDateTime(pobjIDataReader["data"], objCultureInfo);

                if (!String.IsNullOrWhiteSpace(Hora))
                {
                    DateTime dt = Convert.ToDateTime(Hora);
                    datDataCotacao = datDataCotacao.AddHours(-datDataCotacao.Hour);
                    datDataCotacao = datDataCotacao.AddMinutes(-datDataCotacao.Minute);
                    datDataCotacao = datDataCotacao.AddHours(dt.Hour);
                    datDataCotacao = datDataCotacao.AddMinutes(dt.Minute);
                }
            }
            catch { }
            this.Data = datDataCotacao;
        }
    }

    #endregion
}