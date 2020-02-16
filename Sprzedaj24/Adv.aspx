<%@  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Adv.aspx.cs" Inherits="Sprzedaj24.Adv" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ImagePreview(input) {
            console.log(input.files.length < 5);


            for (var i = 0; i < 4; i++) {
                $(lblError).html('');

                $('#imgUpload' + i.toString()).removeAttr("src")
                    .removeAttr("width")
                    .removeAttr("height")
                    .attr("hidden", true);
            }

            for (var i = 0; i < 4; i++) {
                if (typeof input.files[i] != 'undefined') {
                    if (input.files[i].type != 'image/jpeg') {
                        FileUploadControl.value = '';
                        $(lblError).html('Nieprawidłowy format pliku');
                        return;
                    }
                }

                if ((input.files.length) > 4) {
                    FileUploadControl.value = '';
                    $(lblError).html('Wybierz maksymalnie 4 pliki');
                    return;
                }
            }

            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload0).prop('src', e.target.result)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[0]);
            }

            if (input.files && input.files[1]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload1).prop('src', e.target.result)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[1]);
            }

            if (input.files && input.files[2]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload2).prop('src', e.target.result)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[2]);
            }

            if (input.files && input.files[3]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload3).prop('src', e.target.result)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[3]);
            }
        }

    </script>
    <h5>
        <asp:Label ID="lblBreadCrumbs" runat="server" />
    </h5>

    <div class="row" id="divEdit" runat="server" visible="false">
        <div class="container">

            <br>
            <h4>Nowe ogłoszenie</h4>
            <br>
            <table>
                <tr>
                    <td style="padding: 20px">Tytuł:</td>
                    <td>
                        <asp:TextBox ID="tbxTitle" CssClass="form-control" Width="500px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">Opis:</td>
                    <td>
                        <asp:TextBox ID="tbxDescription" runat="server" CssClass="form-control" Height="100px" Width="500px" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">Miejscowość:</td>
                    <td>
                        <asp:TextBox ID="tbxCity" CssClass="form-control" Width="500px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px">Nr telefonu:</td>
                    <td>
                        <asp:TextBox ID="tbxPhoneNumber" CssClass="form-control" MaxLength="10" Width="500px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px; vertical-align: top">Zdjęcia (maks. 4 pliki):</td>
                    <td>
                        <asp:FileUpload ID="FileUploadControl" Style="margin-top: 15px" accept=".jpg, .jpeg" ClientIDMode="Static" CssClass="form-control" runat="server" AllowMultiple="true" onchange="ImagePreview(this);" />
                        <asp:Label ID="lblUploadInfo" Style="font-size: xx-small" Text="Uwaga! Aktualne pliki zostaną nadpisane" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <br>
            <br>
            <asp:Image ID="imgUpload0" Height="200px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload1" Height="200px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload2" Height="200px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload3" Height="200px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <br>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" ClientIDMode="Static" />
            <br>
            <br>
            <asp:LinkButton ID="btnSaveAdv" runat="server" Visible="false" OnClick="btnSaveAdv_Click" CssClass="btn btn-success">Wyślij ogłoszenie<span aria-hidden="true"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnEditAdv" runat="server" Visible="false" OnClick="btnEditAdv_Click" CssClass="btn btn-success">Zapisz zmiany<span aria-hidden="true"></span>
            </asp:LinkButton>
        </div>
    </div>


    <div class="row" id="divShow" runat="server" visible="false">
        <div class="container">
            <br>
            <asp:Label ID="lblTitle" Font-Size="X-Large" runat="server" />
            <br>
            <br>
            <center>
            <asp:Image ID="imgPrvUpload0" Height="500px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            </center>
            <br>
            <br>
            Opis 
            <br>
            <asp:Label ID="lblDescription" Font-Size="Large" runat="server" />
            <br>
            <br>
            Miejscowość 
            <br>
            <asp:Label ID="lblCity" Font-Size="Large" runat="server" />
            <br>
            <br>
            Telefon 
            <br>
            <asp:Label ID="lblPhoneNumber" Font-Size="Large" runat="server" />
            <br>
            <center>
            <asp:Image ID="imgPrvUpload1" Height="500px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgPrvUpload2" Height="500px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgPrvUpload3" Height="500px" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            </center>
            <br>
            <br>
            <asp:Label ID="Label3" runat="server" ForeColor="Red" ClientIDMode="Static" />
            <br>
            <br>
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="btnSaveAdv_Click" CssClass="btn btn-success">Wyślij ogłoszenie<span aria-hidden="true"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" runat="server" Visible="false" OnClick="btnEditAdv_Click" CssClass="btn btn-success">Zapisz zmiany<span aria-hidden="true"></span>
            </asp:LinkButton>
        </div>
    </div>


</asp:Content>
