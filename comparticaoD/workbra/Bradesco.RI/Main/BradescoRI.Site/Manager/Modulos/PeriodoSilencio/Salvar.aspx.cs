using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_PeriodoSilencio_Salvar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.IniciaTela();
            CarregarObjetos(Utilitarios.TipoTransacao.Limpar);
        }
    }

    #region Variáveis
    private int codigo;
    private PeriodoSilencio gobjPeriodoSilencio;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listar.aspx");
    }

    public void ValidateDate(object source, ServerValidateEventArgs args)
    {
        DateTime dt;

        if (DateTime.TryParse(args.Value, out dt) == false)
            args.IsValid = false;

        //Valida se a data é maior que a atual
        if (dt <= DateTime.Now)
            args.IsValid = false;

    }

    protected void txtDataDivulgacao_TextChanged(object sender, EventArgs e)
    {
        txtDataInicio.Text = (Convert.ToDateTime(txtDataDivulgacao.Text).AddDays(-15)).ToString("dd/MM/yyyy");
        txtDataFim.Text = (Convert.ToDateTime(txtDataDivulgacao.Text).AddDays(-1)).ToString("dd/MM/yyyy");
    }
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.rfvDataDivulgacao.Text = Resources.Textos.Texto_Campo_Obrigatorio;

        //Permissão de edição
        if (!((Modulos_Modulos)Master).VerificaPermissaoEdicao())
            Response.Redirect("/Manager/Modulos/Default.aspx");
    }

    private void CarregarObjetos(Utilitarios.TipoTransacao objTipoTransacao)
    {
        switch (objTipoTransacao)
        {
            //Novo Grupo
            case Utilitarios.TipoTransacao.Limpar:
                codigo = 0;

                txtDataDivulgacao.Text = string.Empty;
                txtDataFim.Text = string.Empty;
                txtDataInicio.Text = string.Empty;

                break;
            //Carregar Dados do Grupo
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjPeriodoSilencio == null)
                {
                    gobjPeriodoSilencio = new PeriodoSilencio();
                }

                gobjPeriodoSilencio.DataDivulgacao= Convert.ToDateTime(txtDataDivulgacao.Text);
                gobjPeriodoSilencio.DataInicio = Convert.ToDateTime(txtDataInicio.Text);
                gobjPeriodoSilencio.DataFim = Convert.ToDateTime(txtDataFim.Text);

                break;

        }
    }

    private void Salvar()
    {

        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModEvento.InserirPeriodoSilencio(gobjPeriodoSilencio);
            Response.Redirect("Listar.aspx?sucesso=1");

        }
        catch (Exception ex)
        {
            //Chama o método para gravar erro
            ((Modulos_Modulos)Master).ExibirAlerta(ex);
        }
    }

    #endregion

   
}