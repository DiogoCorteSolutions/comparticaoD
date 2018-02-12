<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Accordion.ascx.cs" Inherits="Modulos_CMS_Modulos_ModAccordion_Accordion" %>
<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTituloSemConteudo" runat="server"><%= string.Format(Resources.Accordion.Modulo_Sem_Conteudo, "Accordion") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-9 col-lg-offset-1 col-md-12 col-md-offset-0">
                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    <div id="dvAccordian">
                        <asp:Repeater ID="rptAccordian" runat="server" OnItemDataBound="rptAccordian_ItemDataBound">
                            <ItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="heading<%# Eval("AccordionId") %>">
                                        <h4 class="panel-title">
                                            <a role="button" class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapse<%# Eval("AccordionId") %>" aria-expanded="false" aria-controls="collapseOne"><%# Eval("Titulo") %></a>
                                        </h4>
                                    </div>
                                    <div id="collapse<%# Eval("AccordionId") %>" class="<%# Eval("EstiloPainel") %>" role="tabpanel" aria-labelledby="heading<%# Eval("AccordionId") %>">
                                        <div class="panel-body">
                                            <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
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




