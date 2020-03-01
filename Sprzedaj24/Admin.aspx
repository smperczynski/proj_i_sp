<%@  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Sprzedaj24.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .advBtn {
            display: inline-block;
            float: left;
            margin: 5px;
        }

        .TableUsers tr td {
            padding: 10px;
        }

        th {
            padding: 10px;
        }
    </style>

    <br>
    <h3>Zarządzanie użytkownikami</h3>
    <br>
    <br>

    <asp:GridView ID="gvUsers" CellSpacing="100" CellPadding="10" CssClass="TableUsers" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Login" HeaderText="Login" HeaderStyle-Width="200px" />
            <asp:BoundField DataField="Email" HeaderText="E-mail" />
            <asp:TemplateField HeaderText="Opcje">
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="hlAdmin" CssClass="btn btn-default" Text='<%# ((string)Eval("TypeId") == "1") ? "Anuluj Admin" : "Ustaw Admin" %>' NavigateUrl='<%# "Admin?go=setAdm&id=" + Eval("UserId")%>'></asp:HyperLink>
                    <asp:HyperLink ID="btnAdvDel" ClientIDMode="Static" Text="Usuń" runat="server" CssClass="btn btn-danger" NavigateUrl='<%# "Admin?go=del&id=" + Eval("UserId")%>' onclick="return confirm('Czy na pewno chcesz usunąć użytkownika?')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
