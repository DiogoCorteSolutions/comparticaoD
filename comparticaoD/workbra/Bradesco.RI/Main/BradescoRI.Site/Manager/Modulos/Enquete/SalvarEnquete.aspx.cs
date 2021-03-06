﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulos_Enquete_SalvarEnquete : System.Web.UI.Page
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
    private Enquete gobjEnquete;
    #endregion

    #region Eventos
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listars.aspx");
    }

    #endregion

    #region Métodos

    private void IniciaTela()
    {
        this.rfvEnquete.Text = Resources.Textos.Texto_Campo_Obrigatorio;

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

                txtEnquete.Text = string.Empty;

                break;
            //Carregar Dados do Grupo
            case Utilitarios.TipoTransacao.Salvar:

                if (gobjEnquete == null)
                {
                    gobjEnquete = new Enquete();
                }

                gobjEnquete.Descricao = txtEnquete.Text;

                break;

        }
    }

    private void Salvar()
    {

        try
        {
            this.CarregarObjetos(Utilitarios.TipoTransacao.Salvar);

            DOModEnquete.InserirEnquete(gobjEnquete);
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