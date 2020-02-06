﻿<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sprzedaj24.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br>

    <div class="row">
        <div class="container">
            <div class="form-group">
                Login:
    <br>
                <asp:TextBox ID="tbxLogin" ValidationGroup="vgLogin" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="valLogin"
                    ControlToValidate="tbxLogin" ForeColor="Red" Display="Dynamic"
                    ValidationGroup="vgLogin" Font-Size="X-Small"
                    ErrorMessage="Wprowadź login"
                    runat="server">
                </asp:RequiredFieldValidator>
                <br>
                Hasło:
    <br>
                <asp:TextBox ID="tbxPassword" TextMode="Password" ValidationGroup="vgLogin" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="valPassword"
                    ControlToValidate="tbxPassword" ForeColor="Red"
                    ValidationGroup="vgLogin" Font-Size="X-Small"
                    ErrorMessage="Wprowadź hasło"
                    runat="server">
                </asp:RequiredFieldValidator>

                <div id="divRegister" runat="server" visible="false">
                    E-mail:
                    <asp:TextBox ID="tbxEmail" TextMode="Email" ValidationGroup="vgEmail" class="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="valEmail"
                        ControlToValidate="tbxEmail" ForeColor="Red"
                        ValidationGroup="vgLogin" Font-Size="X-Small"
                        ErrorMessage="Wprowadź adres e-mail"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </div>
                <br />
                <asp:Label ID="lblError" ForeColor="Red" Visible="false" runat="server"/>
                <br />
                <asp:Button ID="btnLogin" ValidationGroup="vgLogin" OnClick="btnLogin_Click" runat="server" class="btn btn-primary" Text="Zaloguj" />
                <br>
                <br>
                <a id="hlReg" runat="server" href="/Login?go=registration">Nie posiadasz jeszcze konta? Zarejestruj się >></a>
            </div>
        </div>
    </div>
    <asp:Label ID="lbWarning" runat="server" Visible="false" />

    <br>
    <br>
</asp:Content>
