<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Modulos_CMS_Paginas_Footer" %>


<!-- Footer -->

<footer>
   <div class="row" style="position:relative;"><button class="btn btn-primary scroll-top" data-scroll="up" type="button">
                   <i class="glyphicon glyphicon-arrow-up"></i>Voltar ao topo
            </button></div>
    <asp:Panel ID="pnlSemConteudo" runat="server">
        <div>SEM CONTEÚDO</div>
    </asp:Panel>
    <asp:Panel ID="pnlConteudo" runat="server">
        <div class="container-fluid">
            <div class="row horizon-footer">
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-telefones">
                    <div class="horizon-swiper swiper-footer">
                        <div class="col-md-3 col-md-offset-1 horizon-item horizon-footer-item box-tel bloco-tel">
                            <p class="dias">
                                <asp:Label ID="lblTituloN1" runat="server"></asp:Label></p>
                            <h3>
                                <asp:Label ID="lblTelefoneN1" runat="server"></asp:Label></h3>
                            <p>
                                <asp:Label ID="lblTextoN1" runat="server"></asp:Label></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n1 -->
                        <div class="col-md-3 horizon-item horizon-footer-item box-tel bloco-tel">
                            <p class="dias">
                                <asp:Label ID="lblTituloN2" runat="server"></asp:Label></p>
                            <h3>
                                <asp:Label ID="lblTelefoneN2" runat="server"></asp:Label></h3>
                            <p>
                                <asp:Label ID="lblTextoN2" runat="server"></asp:Label></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n2 -->
                        <div class="col-md-3 horizon-item horizon-footer-item box-tel bloco-tel2">
                            <p class="dias">
                                <asp:Label ID="lblTituloN3" runat="server"></asp:Label></p>
                            <h3>
                                <asp:Label ID="lblTelefoneN3" runat="server"></asp:Label></h3>
                            <p>
                                <asp:Label ID="lblTextoN3" runat="server"></asp:Label></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n3 -->
                        <div class="col-md-2 horizon-item horizon-footer-item box-tel bloco-tel3">
                            <p class="dias">Demais telefones, <a href="#">acesse aqui</a></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n4 -->
                    </div>
                    <!-- /.horizon-swiper -->
                    <div class="col-md-12 bloco-tel3b">
                        <p class="demais-tels">Demais telefones, <a href="#">acesse aqui</a></p>
                    </div>
                </div>
            </div>
            <!-- /. row horizon footer -->


            <div class="row row-footer">
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-endereco">
                    <p>
                        <asp:Label ID="lblTextoCentral" runat="server"></asp:Label></p>
                </div>
                <!-- /.b-endereco -->
            </div>
            <!-- /.row footer-->
            <div class="row row-footer">
                <div class="col-lg-11 col-lg-offset-1 col-md-11 col-md-offset-1 l-importantes">
                    <div class="col-md-5ths col-xs-12"><span class="l-impor"></span>
                        <asp:HyperLink ID="linkN1" runat="server" CssClass="btn-foot "></asp:HyperLink></div>
                    <div class="col-md-5ths col-xs-12"><span class="l-impor"></span>
                        <asp:HyperLink ID="linkN2" runat="server" CssClass="btn-foot "></asp:HyperLink></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <asp:HyperLink ID="linkN3" runat="server" CssClass="btn-foot "></asp:HyperLink></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <asp:HyperLink ID="linkN4" runat="server" CssClass="btn-foot "></asp:HyperLink></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <asp:HyperLink ID="linkN5" runat="server" CssClass="btn-foot "></asp:HyperLink></div>
                </div>
                <!-- /.l-importantes -->
            </div>
            <!-- /.row footer-->
            <div class="row row-footer">
                <div class="col-md-10 col-md-offset-1 b-redes-soci">
                    <p>Redes sociais </p>
                </div>
                <!-- /.b-redes-soci -->
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-endereco-mobile">
                    <p class="site-ende">www.bradesco.com.br</p>
                    <p>
                        <asp:Label ID="lblTextoCentralMobile" runat="server"></asp:Label></p>
                </div>
                <!-- /.b-endereco -->
            </div>
            <!-- /.row footer-->

        </div>
        <!-- /.container-fluid -->
        <div class="container-fluid icons-footer-gray">
            <div class="row">

                <div class="col-lg-10 col-lg-offset-2 col-md-10 col-md-offset-2 ">
                    <div class="col-md-4 footer-marcas1"></div>
                    <div class="col-md-4 footer-marcas2"></div>
                </div>
                <!-- /.icons-footer-gray -->

            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </asp:Panel>


</footer>
