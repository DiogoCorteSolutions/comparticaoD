<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Container.ascx.cs" Inherits="Modulos_Paginas_Container" %>

<div id="Container" class="container-fluid edit-modulos">
    <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0">
        <div><asp:ImageButton ID="btnEditar" class="btnsEditar" runat="server" OnClick="btnEditar_Click" title="Editar Módulo na Página" AlternateText="Editar Módulo na Página" ImageUrl="" /></div>
        <div id="divSubir" runat="server"><asp:ImageButton ID="btnSubir" class="btnsEditar" runat="server" OnClick="btnSubir_Click" title="Subir Módulo na Página" AlternateText="Subir Módulo na Página" ImageUrl="" /></div>
        <div id="divDescer" runat="server"><asp:ImageButton ID="btnDescer" class="btnsEditar" runat="server" OnClick="btnDescer_Click" title="Descer Módulo na Página" AlternateText="Descer Módulo na Página" ImageUrl="" /></div>
        <div><asp:ImageButton ID="btnExcluir" class="btnsEditar" runat="server" OnClick="btnExcluir_Click" title="Excluir Módulo Página" AlternateText="Excluir Módulo Página" ImageUrl="" /></div>
        <div class="especificacao-modulo">Módulo: XXXXXX</div>
    </div>
</div>
