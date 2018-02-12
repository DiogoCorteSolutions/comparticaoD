using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_CMS_Modulos_ModAlertaRI_AlertaRI : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IniciaTela();
    }

    #region Variáveis
    public int IdIdioma
    {
        get { return (int)(ViewState["IdIdioma"] ?? 1); }
        set { ViewState["IdIdioma"] = value; }
    }
    #endregion

    #region Métodos

    private void IniciaTela()
    {
        IdIdioma = 1;

        HttpCookie cookie = Request.Cookies["_culture"];
        if (cookie != null)
            IdIdioma = Convert.ToInt32(cookie.Value);

        btnEnviar.Text = Resources.AlertaRI.Botao_Enviar;

        rdlIdioma.Items[0].Text = Resources.AlertaRI.IdiomaPortugues;
        rdlIdioma.Items[1].Text = Resources.AlertaRI.IdiomaIngles;
        rdlIdioma.Items[2].Text = Resources.AlertaRI.IdiomaAmbos;
        rdlIdioma.SelectedIndex = 0;

        rdlProfissionalMercado.Items[0].Text = Resources.AlertaRI.RespostaSim;
        rdlProfissionalMercado.Items[1].Text = Resources.AlertaRI.RespostaNao;
        rdlProfissionalMercado.SelectedIndex = 0;

        rdlMailing.Items[0].Text = Resources.AlertaRI.RespostaSim;
        rdlMailing.Items[1].Text = Resources.AlertaRI.RespostaNao;
        rdlMailing.SelectedIndex = 0;

        this.ddlPais.DataSource = DOPais.Listar(IdIdioma);
        this.ddlPais.DataTextField = "Nome";
        this.ddlPais.DataValueField = "Id";
        this.ddlPais.DataBind();
        this.ddlPais.Items.Insert(0, new ListItem(Resources.AlertaRI.SelecionePais, "0"));

        this.ddlSegmentoEmpresa.DataSource = DOSegmentoEmpresa.Listar(IdIdioma);
        this.ddlSegmentoEmpresa.DataTextField = "Nome";
        this.ddlSegmentoEmpresa.DataValueField = "Id";
        this.ddlSegmentoEmpresa.DataBind();
        this.ddlSegmentoEmpresa.Items.Insert(0, new ListItem(Resources.AlertaRI.SelecioneSegmento, "0"));

        this.rfvEmail.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.rfvNome.Text = Resources.Textos.Texto_Campo_Obrigatorio;
        this.revEmail.Text = Resources.Textos.Texto_Email_Invalido;

        this.txtNome.ToolTip = Resources.AlertaRI.NomeDescricao;
        this.txtEmail.ToolTip = Resources.AlertaRI.EmailDescricao;
        this.txtTelefoneDDD.ToolTip = Resources.AlertaRI.TelefoneDDDDescricao;
        this.txtTelefone.ToolTip = Resources.AlertaRI.TelefoneDescricao;
        this.txtEstado.ToolTip = Resources.AlertaRI.EstadoDescricao;
        this.ddlPais.ToolTip = Resources.AlertaRI.PaisResidencia;
        this.txtEmpresa.ToolTip = Resources.AlertaRI.EmpresaDescricao;
        this.ddlSegmentoEmpresa.ToolTip = Resources.AlertaRI.Segmento;

        this.txtNome.Text = string.Empty;
        this.txtEmail.Text = string.Empty;
        this.txtTelefone.Text = string.Empty;
        this.txtTelefoneDDD.Text = string.Empty;
        this.txtEstado.Text = string.Empty;
        this.txtEmpresa.Text = string.Empty;
    }
    #endregion

    #region Eventos
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        
        try
        {
            ModAlerta objModAlerta = new ModAlerta();
            objModAlerta.Nome = txtNome.Text;
            objModAlerta.Email = txtEmail.Text;
            objModAlerta.Empresa = txtEmpresa.Text;
            objModAlerta.TelefoneDDD = txtTelefoneDDD.Text;
            objModAlerta.Telefone = txtTelefone.Text;
            objModAlerta.IdSegmentoEmpresa = Convert.ToInt32(ddlSegmentoEmpresa.SelectedValue);
            objModAlerta.Estado = txtEstado.Text;
            objModAlerta.IdPais = Convert.ToInt32(ddlPais.SelectedValue);
            objModAlerta.ProfissionalMercado = rdlProfissionalMercado.SelectedValue == "1";
            objModAlerta.IdIdiomaMailing = Convert.ToInt32(rdlIdioma.SelectedValue);
            objModAlerta.ReceberMailing = rdlMailing.SelectedValue == "1";

            DOModAlerta.Inserir(objModAlerta);

            IniciaTela();

            lblMensagemSucesso.Visible = true;
            lblMensagemEmailExiste.Visible = false;
            lblMensagemErro.Visible = false;
        }
        catch (SqlException sqlEx)
        {
            lblMensagemSucesso.Visible = false;
            lblMensagemEmailExiste.Visible = true;
            lblMensagemErro.Visible = false;
        }
        catch (Exception ex)
        {
            lblMensagemSucesso.Visible = false;
            lblMensagemEmailExiste.Visible = false;
            lblMensagemErro.Visible = true;
        }
        


    }
    #endregion
}