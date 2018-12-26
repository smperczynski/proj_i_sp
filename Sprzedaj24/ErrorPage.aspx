<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Sprzedaj24.ErrorPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css">
    <br>
    <div class="alert alert-danger" role="alert">
        <h3 class="err5"><span class="glyphicon glyphicon-alert"></span>
            Ooops.. Coś poszło nie tak...<br>
            <br>
            Sprawdź poprawność adresu URL lub przejdź na <a href="/Default" class="alert-link">stronę główną</a>.</h3>
    </div>
</asp:Content>
