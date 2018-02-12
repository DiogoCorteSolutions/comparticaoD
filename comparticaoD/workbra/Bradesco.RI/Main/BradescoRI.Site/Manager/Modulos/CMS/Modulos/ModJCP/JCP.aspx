<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCP.aspx.cs" Inherits="Modulos_CMS_Modulos_ModJCP_JCP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="<%=ResolveUrl("~/CSS/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/style-manager.css")%>" rel="stylesheet" />
     <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <title></title>
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }

       </script>
</head>
<body class="modulos-cms">
    <div id="wrapper">
         <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">JCP</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Configuração do módulo
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <form id="form1" runat="server" role="form">
                                        <div class="form-group">
                                            <label>Idioma:</label> 
                                            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label> Aba:</label> 
                                            <asp:DropDownList ID="ddlAba" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAba_SelectedIndexChanged" class="form-control">
                                                <asp:ListItem Text="Ano"></asp:ListItem>
                                                <asp:ListItem Text="Histórico de Eventos"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>      
                                        <div id="divAno" runat="server" class="form-group">
                                            <label>Ano:</label>
                                            <asp:TextBox ID="txtAno" runat="server" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Período:</label>
                                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtPeriodo" MaxLength="10" title="Informe o periodo" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Tipo de Provento:</label>
                                            <asp:TextBox ID="txtTipoProvento" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Posição Acionária:</label>
                                            <asp:TextBox runat="server" ID="txtPosicaoAcionaria" MaxLength="10" title="Informe a posição acionária" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="divDataPagamento" runat="server" class="form-group">
                                            <label>Data de Pagamento:</label>
                                            <asp:TextBox runat="server" ID="txtDataPagamento"  CssClass="form-control" MaxLength="10" title="Informe a data de pagamento"></asp:TextBox>            
                                        </div>
                                        <div id="divValorAcao" runat="server" class="form-group">
                                            <label>Valor por Ação (nominal/líquido):</label>
                                            <asp:TextBox ID="txtValorAcao" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        
                                            <asp:Button ID="btnSalvar" runat="server" Text="Adicionar" OnClick="btnSalvar_Click" ValidationGroup="salvar" CssClass="btn btn-default btn-bra"/>
                                        

                                        <div class="form-group">
                                            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                                        </div>


                                        <label>Aba:</label>
                                        <asp:DropDownList ID="ddlAno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:DataGrid summary="Lista de JCP" ID="grdDados" runat="server" BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="false" CssClass="table table-striped table-bra-cms"
                                            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand" >
                                            <Columns>
                
                                                <asp:BoundColumn Visible="false" DataField="IdJCP" HeaderText="IdJCP" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="Periodo" HeaderText="Periodo" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="TipoProvento" HeaderText="TipoProvento" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="PosicaoAcionaria" HeaderText="TipoProvento" DataFormatString="{0:d}"  HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="DataPagamento" HeaderText="DataPagamento" DataFormatString="{0:d}"  HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="ValorAcao" HeaderText="ValorAcao" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:TemplateColumn HeaderText="Excluir" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate> 
                                                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("IdJCP")%>'
                                                             ImageUrl="/Manager/Imagens/remove-icon.png" style="width: 1.5em;" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>

                                            </Columns>
                                            <HeaderStyle CssClass="topo-tb"></HeaderStyle>
                                            <ItemStyle CssClass="impar"></ItemStyle>
                                            <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
                                            <PagerStyle Visible="false" />
                                        </asp:DataGrid>
                                        <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
                                        <div>
                                            <asp:Button ID="btnFechar" runat="server" Text="Salvar" OnClick="btnFechar_Click" CssClass="btn btn-default btn-bra"/>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

