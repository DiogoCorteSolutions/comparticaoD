<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master"  AutoEventWireup="true" CodeFile="EditarLinks.aspx.cs" Inherits="Modulos_Links_EditarLinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>

        $(document).ready(function () {

            $(".ddlPaginas").change(function () {
                if (this.value != "0") {
                    $(".txtUrl").val(this.value);
                } else {
                    $(".txtUrl").val('');
                }
            });
        });

    </script>

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Links.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">        
        <label>
            <span>
                <asp:Label ID="lblGrupo" runat="server" AssociatedControlID="txtGrupo" title="Grupo"><%=Resources.Links.Grupo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtGrupo" CssClass="frmTxt" MaxLength="200" Enabled="false"></asp:TextBox>
        </label>
                <label>
            <span>
                <asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtTitulo" title="Título"><%=Resources.Links.Descricao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTitulo" CssClass="frmTxt" MaxLength="200" title="Informe o título"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Links.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>           
        
        <label>
            <span>
                <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" title="Url"><%=Resources.Links.Url %></asp:Label>
            </span>
            <asp:TextBox ID="txtUrl" runat="server" class="txtUrl"></asp:TextBox>
        </label>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTarget" runat="server" AssociatedControlID="ddlTarget" title="Target"><%=Resources.Links.Target %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTarget" title="Selecione o target"></asp:DropDownList>
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


