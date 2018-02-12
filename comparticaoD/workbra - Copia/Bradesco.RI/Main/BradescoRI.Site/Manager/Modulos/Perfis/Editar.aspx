<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editar.aspx.cs" MasterPageFile="~/Modulos/Modulos.master" Inherits="Modulos_Perfis_Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <script src="<%=ResolveUrl("~/JS/Perfil.js")%>"></script>
    
    <div id="title">
        <h1>
            <span>
                <%=Resources.Perfil.Titulo %>
            </span>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Nome do Perfil"><%=Resources.Perfil.Nome %>:</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="200" title="Informe o nome do Perfil"></asp:TextBox>
        </label>

    </div>
    <div class="checkExibir" title="Exibir / Esconder Todos">
        <input type="hidden" id="hdnEsconder" value="true" />
        <span class="exibirEsconderTudo" style="cursor: pointer;">+</span> <%=Resources.Perfil.ExibirEsconder %>
    </div>
    <asp:Repeater ID="rptGrupos" runat="server" OnItemDataBound="rptGrupos_ItemDataBound">
        <HeaderTemplate>
            <div class="fields no-columns">
                <span>
                    <ul class="perfil-edicao">
        </HeaderTemplate>
        <ItemTemplate>
            <li title='<%# Container.DataItem %>'>
                <div>
                    <span class="more" style="cursor: pointer;">+</span>

                    <%# Container.DataItem %>
                </div>
            </li>

            <asp:Repeater runat="server" ID="rptPermissao" OnItemDataBound="rptPermissao_ItemDataBound">
                <HeaderTemplate>
                    <div class="fields no-columns item-filho" style="display: none;">
                        <span>
                            <ul class="perfil-edicao">
                                <li>
                                    <div style="background-color: #f5f5f0">
                                        <label style="width: 300px; display: inline-block;" title="Módulo">Módulo</label>
                                        <label style="width: 100px; display: inline-block; text-align: center;" title="Controle Total">Controle Total</label>
                                        <label style="width: 100px; display: inline-block; text-align: center;" title="Acessar">Acessar</label>
                                        <label style="width: 100px; display: inline-block; text-align: center;" title="Inserir">Inserir</label>
                                        <label style="width: 100px; display: inline-block; text-align: center;" title="Editar">Editar</label>
                                        <label style="width: 100px; display: inline-block; text-align: center;" title="Excluir">Excluir</label>
                                    </div>
                                </li>
                </HeaderTemplate>
                <ItemTemplate>
                    <li title='<%# (DataBinder.Eval(Container.DataItem, "Grupo") + " - " + DataBinder.Eval(Container.DataItem, "Nome")) %>'>
                        <div>

                            <label style="width: 300px; display: inline-block;">
                                <asp:HiddenField runat="server" ID="hdnID" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                                <%# ((DataBinder.Eval(Container.DataItem, "Grupo").ToString() == DataBinder.Eval(Container.DataItem, "Nome").ToString()) ? 
                            (DataBinder.Eval(Container.DataItem, "Grupo")) : (DataBinder.Eval(Container.DataItem, "Nome"))) %></label>
                            <label style="width: 100px; display: inline-block; text-align: center;" title="Controle Total">
                                <asp:CheckBox runat="server" ID="chkControleTotal" CssClass="secaoPai" Checked='<%# DataBinder.Eval(Container.DataItem, "PossuiControleTotal")%>' /></label>
                            <label style="width: 100px; display: inline-block; text-align: center;" title="Acessar">
                                <asp:CheckBox runat="server" ID="chkAcessar" CssClass="secaoFilho" Checked='<%# DataBinder.Eval(Container.DataItem, "PodeAcessar")%>' /></label>
                            <label style="width: 100px; display: inline-block; text-align: center;" title="Inserir">
                                <asp:CheckBox runat="server" ID="chkInserir" CssClass="secaoFilho" Checked='<%# DataBinder.Eval(Container.DataItem, "PodeInserir")%>' /></label>
                            <label style="width: 100px; display: inline-block; text-align: center;" title="Editar">
                                <asp:CheckBox runat="server" ID="chkEditar" CssClass="secaoFilho" Checked='<%# DataBinder.Eval(Container.DataItem, "PodeAlterar")%>' /></label>
                            <label style="width: 100px; display: inline-block; text-align: center;" title="Excluir">
                                <asp:CheckBox runat="server" ID="chkExcluir" CssClass="secaoFilho" Checked='<%# DataBinder.Eval(Container.DataItem, "PodeExcluir")%>' /></label>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                    </span>
                </div>
                </FooterTemplate>
            </asp:Repeater>

        </ItemTemplate>
        <FooterTemplate>
            </ul>
                    </span>
                </div>
        </FooterTemplate>
    </asp:Repeater>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" />
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Cancelar" OnClick="btnCancelar_Click" title="Cancelar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>

