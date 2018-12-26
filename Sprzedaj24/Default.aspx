<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sprzedaj24._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="ResultsPanel" runat="server">
        <br>

        <div class="row">
            <div class="container">
                <div class="form-group">
                    <input type="text" class="form-control input-lg col-md-12" placeholder="Search">
                </div>
                <button type="submit">
                    <span class="glyphicon glyphicon-search"></span>
                </button>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <h4>MOTORYZACJA</h4>
                <hr class="round55">
                <asp:Label ID="label1" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>DLA DOMU I OGRODU</h4>
                <hr class="round55">
                <asp:Label ID="label2" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>NIERUCHOMOŚCI</h4>
                <hr class="round55">
                <asp:Label ID="label3" runat="server" />
            </div>

            <div class="col-md-3">
                <h4>PRACA</h4>
                <hr class="round55">
                <asp:Label ID="label4" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <h4>ZWIERZĘTA I ROŚLINY</h4>
                <hr class="round55">
                <asp:Label ID="label5" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>DLA DZIECI</h4>
                <hr class="round55">
                <asp:Label ID="label6" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>ŚLUB I WESELA</h4>
                <hr class="round55">
                <asp:Label ID="label7" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>ODZIEŻ, OBUWIE, URODA</h4>
                <hr class="round55">
                <asp:Label ID="label8" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <h4>TELEFONY I AKCESORIA</h4>
                <hr class="round55">
                <asp:Label ID="label9" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT KOMPUTEROWY</h4>
                <hr class="round55">
                <asp:Label ID="label10" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT SPORTOWY</h4>
                <hr class="round55">
                <asp:Label ID="label11" runat="server" />
            </div>
            <div class="col-md-3">
                <h4>SPRZĘT RTV</h4>
                <hr class="round55">
                <asp:Label ID="label12" runat="server" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
