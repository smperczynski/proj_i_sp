<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Sprzedaj24.Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="container">
            <h5>
                <asp:Label ID="lblSciezka" runat="server" /></h5>
        </div>
    </div>
        <div class="row">
        <div class="container">
    <div class="row">
        <div class="container">
            <table>
                <tr>

                    <td>
                        <br>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <table style="border: 1px solid #808080; background-color: #ffffff">
                                    <tr style="width: 500px">
                                        <td style="width: 200px">
                                            <asp:Image ID="imgEmployee"
                                                ImageUrl='<%# Eval("Sciezka_Foto1")%>'
                                                runat="server" Width="200px" />
                                        </td>
                                        <td style="width: 500px; padding: 10px 10px 10px 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <b>Tytuł:</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblId"
                                                            runat="server"
                                                            Text='<%#Eval("Tytul") %>'>
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Opis:</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName"
                                                            runat="server"
                                                            Text='<%#Eval("Opis") %>'>
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Telefon:</b>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGender"
                                                            runat="server"
                                                            Text='<%#Eval("Telefon") %>'>
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>City:</b>
                                                    </td>
                                                    <%--                                <td>
                                    <asp:Label ID="lblCity" 
                                    runat="server" 
                                    Text='<%#Eval("City") %>'>
                                    </asp:Label>
                                </td>--%>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <asp:Image ID="Image1"
                                    ImageUrl="~/Images/1x1PixelImage.png"
                                    runat="server" />
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
