<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modulos/Modulos.master" CodeFile="Salvar.aspx.cs" Inherits="Modulos_PeriodoSilencio_Salvar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%= Resources.PeriodoSilencio.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblDataDivulgacao" runat="server" AssociatedControlID="txtDataDivulgacao" title="Data Divulgacao"><%=Resources.PeriodoSilencio.DataDivulgacao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDataDivulgacao" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de divulgação" AutoPostBack="true" OnTextChanged="txtDataDivulgacao_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataDivulgacao" runat="server" ControlToValidate="txtDataDivulgacao" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="ctvDataDivulgacao" runat="server" ErrorMessage="Data inválida" ControlToValidate="txtDataDivulgacao" OnServerValidate="ValidateDate"></asp:CustomValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblPeriodoSilencio" runat="server" title="Periodo de Silêncio"><%=Resources.PeriodoSilencio.Titulo %></asp:Label>
            </span>
            <asp:Label ID="lblDe" runat="server" title="De"><%=Resources.PeriodoSilencio.De%></asp:Label>
            <asp:TextBox runat="server" ID="txtDataInicio" CssClass="frmTxt" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblAte" runat="server" title="Até"><%=Resources.PeriodoSilencio.Ate%></asp:Label>
            <asp:TextBox runat="server" ID="txtDataFim" CssClass="frmTxt" Enabled="false"></asp:TextBox>
        </label>
    </div>
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblCamposObrigatorios" runat="server" title="Campos obrigatórios"><%=Resources.Textos.Campos_Obrigatorios %></asp:Label>
            </span>
        </label>

    </div>

    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>
