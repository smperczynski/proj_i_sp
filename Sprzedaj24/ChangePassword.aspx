<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Sprzedaj24.EditProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br>

    <div class="row">
        <div class="container">
            <div class="form-group">
                Aktualne hasło:
    <br>
                <asp:TextBox ID="tbxLast" TextMode="Password" ValidationGroup="Password" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="valLast"
                    ControlToValidate="tbxLast" ForeColor="Red" Display="Dynamic"
                    ValidationGroup="vgPassword" Font-Size="X-Small"
                    ErrorMessage="Wprowadź aktualne hasło"
                    runat="server">
                </asp:RequiredFieldValidator>
                <br>
                Nowe hasło:
    <br>
                <asp:TextBox ID="tbxNew1" TextMode="Password" ValidationGroup="vgPassword" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="valNew1"
                    ControlToValidate="tbxNew1" ForeColor="Red"
                    ValidationGroup="vgPassword" Font-Size="X-Small"
                    ErrorMessage="Wprowadź nowe hasło"
                    runat="server">
                </asp:RequiredFieldValidator>
    <br>
                Powtórz nowe hasło:
                    <asp:TextBox ID="tbxNew2" TextMode="Password" ValidationGroup="vgPassword" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="valNew2"
                    ControlToValidate="tbxNew2" ForeColor="Red"
                    ValidationGroup="vgPassword" Font-Size="X-Small"
                    ErrorMessage="Powtórz nowe hasło"
                    runat="server">
                </asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblError" ForeColor="Red" Visible="false" runat="server" />
                <br />
                <asp:Button ID="btnPassword" ValidationGroup="vgPassword" OnClick="btnPassword_Click" runat="server" class="btn btn-primary" Text="Zmień hasło" />
            </div>
        </div>
    </div>
    <asp:Label ID="lblWarning" runat="server" Visible="false" />

    <br>
    <br>
</asp:Content>
