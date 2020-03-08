<%@  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Sprzedaj24.Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .advBtn {
            display: inline-block;
            float: left;
            margin: 5px;
        }
    </style>
    <br><br>
    <asp:Label ID="lblSearch" runat="server" Visible="false"></asp:Label>

    <div class="row">
        <div class="container" style="padding: 35px">
            <h5>
                <asp:Label ID="lblBreadCrumbs" runat="server" /></h5>

            <asp:LinkButton ID="btnNoweOgl" Visible="false"
                runat="server" OnClick="btnNoweOgl_Click"
                CssClass="btn btn-success" Style="float: right">
            <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> Dodaj ogłoszenie
            </asp:LinkButton>

            <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label>

            <asp:DropDownList ID="ddCategoriesSwitch" Visible="false" DataValueField="MenuId" DataTextField="Name"
                CssClass="form-control" AutoPostBack="True" Style="float: right" OnLoad="ddCategoriesSwitch_Load"
                runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <%--        <div class="row">
        <div class="container">--%>

    <%--    <div id="divCategoriesSwitch">
        <asp:DropDownList ID="ddCategoriesSwitch" Visible="false" DataValueField="MenuId" DataTextField="Name"
            CssClass="form-control" AutoPostBack="True" OnLoad="ddCategoriesSwitch_Load"
            runat="server">
        </asp:DropDownList>
    </div>--%>

    <div class="row">
        <div class="col-lg-15 col-md-offset-2">
            <table>
                <tr>
                    <td>
                        <br>
                        <asp:Repeater ID="rptAdv" runat="server">
                            <ItemTemplate>
                                <table style="border: 1px solid #c1c0c0; background-color: #ffffff;">
                                    <tr style="width: 500px; padding: 10px 10px 10px 10px;">

                                        <td style="width: 350px">
                                            <h5><b>&nbsp;<%# Eval("Title")%></b></h5>

                                            <asp:Image ID="imgEmployee"
                                                ImageUrl='<%# Eval("PhotoPath")%>'
                                                runat="server" Width="300px" />
                                        </td>

                                        <td style="width: 600px; padding: 10px 10px 10px 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <b>Opis:</b><br>
                                                        <br>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" Style="margin: 10px"
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
                                                        <asp:Label ID="lblGender" Style="margin: 10px"
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
                                                        <asp:Label ID="lblCity" Style="margin: 10px"
                                                            runat="server"
                                                            Text='<%#Eval("City") %>'>
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <div style="float: right; vertical-align: middle">
                                                        <asp:HyperLink ID="hlAdvShow" Text="Pokaż" runat="server" Style="width: 60px" CssClass="advBtn btn btn btn-default btn-sm" NavigateUrl='<%# "Adv?go=show&id=" + Eval("CategoryId") + "&a=" + Eval("AdvertisementId")%>' />
                                                        <br />
                                                        <asp:HyperLink ID="hlAdvEdit" ClientIDMode="Static" Visible='<%# ((int)Eval("Edit") == 1) ? true : false %>' Text="Edytuj" runat="server" Style="width: 60px" CssClass="advBtn btn btn btn-success btn-sm" NavigateUrl='<%# "Adv?go=edit&id=" + Eval("CategoryId") + "&a=" + Eval("AdvertisementId")%>' />
                                                        <br />                         
                                                        <asp:HyperLink ID="hlAdvDel" ClientIDMode="Static" Visible='<%# ((int)Eval("Edit") == 1) ? true : false %>' Text="Usuń" runat="server" Style="width: 60px" CssClass="advBtn btn btn btn-danger btn-sm" NavigateUrl='<%# "Adv?go=del&id=" + Eval("CategoryId") + "&a=" + Eval("AdvertisementId")%>' onclick="return confirm('Czy na pewno chcesz usunąć ogłoszenie?')" />
                                                    </div>
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
