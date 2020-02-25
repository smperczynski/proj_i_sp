<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sprzedaj24._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="ResultsPanel" runat="server">
        <br>

        <div>
            <div style="float: left;">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="300px" placeholder="Wyszukaj przedmiot..."></asp:TextBox>
            </div>

            <div style="float: left;">
                <asp:DropDownList ID="ddCategoriesSwitch" DataValueField="MenuId" DataTextField="Path"
                    CssClass="form-control" AutoPostBack="False" Style="float: right; margin-left: 5px" OnLoad="ddCategoriesSwitch_Load" runat="server">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnSearch" runat="server" Style="margin-left: 5px" CssClass="btn btn-default" Text="Szukaj" OnClick="btnSearch_Click" />
            <div style="clear: both">
            </div>
        </div>

        <br>
        <br>

        <div class="row">
            <div class="col-md-3">
                <h4>MOTORYZACJA</h4>
                <hr class="round55">
                <asp:Label ID="lbl1" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>DLA DOMU I OGRODU</h4>
                <hr class="round55">
                <asp:Label ID="lbl2" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>NIERUCHOMOŚCI</h4>
                <hr class="round55">
                <asp:Label ID="lbl3" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>PRACA</h4>
                <hr class="round55">
                <asp:Label ID="lbl4" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <h4>ZWIERZĘTA I ROŚLINY</h4>
                <hr class="round55">
                <asp:Label ID="lbl5" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>DLA DZIECI</h4>
                <hr class="round55">
                <asp:Label ID="lbl6" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>ŚLUB I WESELA</h4>
                <hr class="round55">
                <asp:Label ID="lbl7" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>ODZIEŻ, OBUWIE, URODA</h4>
                <hr class="round55">
                <asp:Label ID="lbl8" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <h4>TELEFONY I AKCESORIA</h4>
                <hr class="round55">
                <asp:Label ID="lbl9" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT KOMPUTEROWY</h4>
                <hr class="round55">
                <asp:Label ID="lbl10" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT SPORTOWY</h4>
                <hr class="round55">
                <asp:Label ID="lbl11" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT RTV</h4>
                <hr class="round55">
                <asp:Label ID="lbl12" runat="server" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
