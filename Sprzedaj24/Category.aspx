<%@  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Sprzedaj24.Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="container">
            <h5>
                <asp:Label ID="lblBreadCrumbs" runat="server" /></h5>

            <asp:LinkButton ID="btnNoweOgl"
                runat="server" OnClick="btnNoweOgl_Click"
                CssClass="btn btn-success" Style="float: right">
            <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> Dodaj ogłoszenie
            </asp:LinkButton>

        </div>
    </div>
    <%--        <div class="row">
        <div class="container">--%>
    <div class="row">
        <div class="col-lg-15 col-md-offset-2">
            <table>
                <tr>
                    <td>
                        <br>
                        <asp:Repeater ID="rptAdv" runat="server">
                            <ItemTemplate>
                                <table style="border: 1px solid #c1c0c0; background-color: #ffffff">
                                    <tr style="width: 500px; padding: 10px 10px 10px 10px;">

                                        <td style="width: 350px">
                                            <h5><b>&nbsp;<%# Eval("Title")%></b></h5>

                                            <asp:Image ID="imgEmployee"
                                                ImageUrl='<%# Eval("PhotoPath")%>'
                                                runat="server" Width="300px" />
                                        </td>

                                        <%--                                        <td style="width: 350px">--%>
                                        <%--                                            <h6><%# Eval("Tytul")%></h6>--%>
                                        <%--                                            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                                <!-- Indicators -->
                                                <ol class="carousel-indicators">
                                                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                                    <li data-target="#myCarousel" data-slide-to="1"></li>
                                                    <li data-target="#myCarousel" data-slide-to="2"></li>
                                                </ol>

                                                <!-- Wrapper for slides -->
                                                <div class="carousel-inner">
                                                    <div class="item active">
                                                        <img src="<%# Eval("Sciezka_Foto1")%>" alt="Los Angeles" style="width: 100%;">
                                                    </div>

                                                    <div class="item">
                                                        <img src="<%# Eval("Sciezka_Foto2")%>" alt="Chicago" style="width: 100%;">
                                                    </div>

                                                    <div class="item">
                                                        <img src="<%# Eval("Sciezka_Foto3")%>" alt="New york" style="width: 100%;">
                                                    </div>
                                                </div>

                                                <!-- Left and right controls -->
                                                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                                    <span class="sr-only">Previous</span>
                                                </a>
                                                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                                    <span class="sr-only">Next</span>
                                                </a>
                                            </div>--%>

                                        <%--                                        </td>--%>


                                        <td style="width: 600px; padding: 10px 10px 10px 10px;">
                                            <table>
                                                <%--<b><%#Eval("Tytul") %></b><br>--%>

                                                <%--                                                <tr>--%>
                                                <%--                                            <td>
                                                <b>
                                                    <asp:Label ID="lblId"
                                                        runat="server"
                                                        Text='<%#Eval("Tytul") %>'>
                                                    </asp:Label></b><br>
                                                <br>
                                            </td>--%>
                                                <%--                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <b>Opis:</b><br>
                                                        <br>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName"
                                                            runat="server"
                                                            Text='<%#Eval("Description") %>'>
                                                        </asp:Label><br>
                                                        <br>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Nr telefonu:</b>
                                                        <br>
                                                        <br>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGender"
                                                            runat="server"
                                                            Text='<%#Eval("PhoneNumber") %>'>
                                                        </asp:Label>
                                                        <br>
                                                        <br>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Lokalizacja:</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCity"
                                                            runat="server"
                                                            Text='<%#Eval("City") %>'>
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br>
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
