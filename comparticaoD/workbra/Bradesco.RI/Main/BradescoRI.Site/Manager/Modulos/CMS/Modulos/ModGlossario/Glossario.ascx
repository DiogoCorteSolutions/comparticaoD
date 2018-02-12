<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Glossario.ascx.cs" Inherits="Modulos_CMS_Modulos_ModGlossario_Glossario" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Glossário") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <!-- INÍCIO DO MÓDULO GLOSSÁRIO  -->
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 not-sub-tot">
                <div class="glo-tit">Mostrar termos que iniciam pela letra</div>
                <div class="glo-alf">
                    <asp:Button ID="btnA" runat="server" CssClass="glo-letra-active" Text="A" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnB" runat="server" CssClass="glo-letra" Text="B" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnC" runat="server" CssClass="glo-letra" Text="C" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnD" runat="server" CssClass="glo-letra" Text="D" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnE" runat="server" CssClass="glo-letra" Text="E" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnF" runat="server" CssClass="glo-letra" Text="F" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnG" runat="server" CssClass="glo-letra" Text="G" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnH" runat="server" CssClass="glo-letra" Text="H" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnI" runat="server" CssClass="glo-letra" Text="I" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnJ" runat="server" CssClass="glo-letra" Text="J" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnK" runat="server" CssClass="glo-letra" Text="K" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnL" runat="server" CssClass="glo-letra" Text="L" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnM" runat="server" CssClass="glo-letra" Text="M" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnN" runat="server" CssClass="glo-letra" Text="N" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnO" runat="server" CssClass="glo-letra" Text="O" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnP" runat="server" CssClass="glo-letra" Text="P" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnQ" runat="server" CssClass="glo-letra" Text="Q" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnR" runat="server" CssClass="glo-letra" Text="R" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnS" runat="server" CssClass="glo-letra" Text="S" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnT" runat="server" CssClass="glo-letra" Text="T" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnU" runat="server" CssClass="glo-letra" Text="U" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnV" runat="server" CssClass="glo-letra" Text="V" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnW" runat="server" CssClass="glo-letra" Text="W" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnY" runat="server" CssClass="glo-letra" Text="Y" OnClick="btnFiltro_Click" />
                    <asp:Button ID="btnZ" runat="server" CssClass="glo-letra" Text="Z" OnClick="btnFiltro_Click" />
                </div>

                <div class="glo-cont">
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                        <div id="divGlossario">

                            <asp:Repeater ID="rptGlossario" runat="server" >
                                <ItemTemplate>

                                    <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="heading<%# Eval("Id") %>">
                                        <h4 class="panel-title">
                                            <a role="button" class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapse<%# Eval("Id") %>" aria-expanded="false" aria-controls="collapseOne"><%# Eval("Titulo") %></a>
                                        </h4>
                                    </div>
                                    <div id="collapse<%# Eval("Id") %>" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading<%# Eval("Id") %>">
                                        <div class="panel-body">
                                            Texto
                                        </div>
                                    </div>
                                </div>

                                    
                                </ItemTemplate>
                            </asp:Repeater>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- FIM DO MÓDULO GLOSSÁRIO  -->
</div>
