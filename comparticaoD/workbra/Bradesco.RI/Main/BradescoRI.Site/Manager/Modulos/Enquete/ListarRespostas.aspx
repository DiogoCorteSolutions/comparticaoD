<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="ListarRespostas.aspx.cs" Inherits="Modulos_Enquete_ListarRespostas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Enquetes.TituloResposta %></asp:Label>
        </h1>
    </div>
    <br />

    <div class="filtros">
        
        <div class="fields-edit">

            <label class="txtPeriodo">
                <span>
                    <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Links.Idioma %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlIdioma"></asp:DropDownList>
            </label>
        </div>
                <label class="btn-acoes-right">
            <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" title="Buscar" alt="Buscar" />
        </label>
    </div>

   

    <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
        <div class="txt-n-reg" title="Nenhum registro encontrado"><%=Resources.Textos.Nenhum_Registro %></div>
    </asp:Panel>

    <asp:Panel ID="pnlRegistrosEncontrados" runat="server">
        <div class="txt-n-reg">
            <asp:Literal runat="server" ID="ltlRegistrosEncontrados"></asp:Literal><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span>
        </div>
    </asp:Panel>

    <asp:DataGrid summary="Lista de Respostas" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True"
        PageSize="50" >
        <Columns>
            
            <asp:BoundColumn DataField="Pergunta"  HeaderText="Pergunta"/>            
            <asp:BoundColumn DataField="Resposta1"  HeaderText="Resposta 1" />            
            <asp:BoundColumn DataField="TotalResposta1"  HeaderText="Total" />            
            <asp:BoundColumn DataField="Resposta2"  HeaderText="Resposta 2" />            
            <asp:BoundColumn DataField="TotalResposta2"  HeaderText="Total" />            
            <asp:BoundColumn DataField="Resposta3"  HeaderText="Resposta 3" />            
            <asp:BoundColumn DataField="TotalResposta3"  HeaderText="Total" />            
            <asp:BoundColumn DataField="Resposta4"  HeaderText="Resposta 4" />            
            <asp:BoundColumn DataField="TotalResposta4"  HeaderText="Total" />            
            <asp:BoundColumn DataField="Resposta5"  HeaderText="Resposta 5" />            
            <asp:BoundColumn DataField="TotalResposta5"  HeaderText="Total" />   
            <asp:BoundColumn DataField="Total"  HeaderText="Total Respostas" />          
        </Columns>
        <HeaderStyle CssClass="topo-tb"></HeaderStyle>
        <ItemStyle CssClass="impar"></ItemStyle>
        <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
        <PagerStyle Visible="false" />
    </asp:DataGrid>
    <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
    
</asp:Content>