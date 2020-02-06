<%@  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewAdv.aspx.cs" Inherits="Sprzedaj24.NewAdv" %>

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
                        .width(200)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[0]);
            }

            if (input.files && input.files[1]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload1).prop('src', e.target.result)
                        .width(200)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[1]);
            }

            if (input.files && input.files[2]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload2).prop('src', e.target.result)
                        .width(200)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[2]);
            }

            if (input.files && input.files[3]) {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $(imgUpload3).prop('src', e.target.result)
                        .width(200)
                        .height(200)
                        .attr("hidden", false);
                };

                reader.readAsDataURL(input.files[3]);
            }
        }

    </script>

    <div class="row">
        <div class="container">
            <h5>
                <asp:Label ID="lblBreadCrumbs" runat="server" /></h5>
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
                        <asp:TextBox ID="tbxPhoneNumber" CssClass="form-control" MaxLength="10" TextMode="Number" Width="500px" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px; vertical-align: top">Zdjęcia (maks. 4 pliki):</td>
                    <td>
                        <asp:FileUpload ID="FileUploadControl" accept=".jpg, .jpeg" ClientIDMode="Static" CssClass="form-control" runat="server" AllowMultiple="true" onchange="ImagePreview(this);" />
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgUpload0" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload1" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload2" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <asp:Image ID="imgUpload3" runat="server" Style="border-radius: 5px;" ClientIDMode="Static"></asp:Image>
            <br>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" ClientIDMode="Static" />
            <br>
            <br>

            <asp:LinkButton ID="btnSaveAdv" runat="server" OnClick="btnSaveAdv_Click" CssClass="btn btn-success">Wyślij ogłoszenie <span aria-hidden="true"></span>
            </asp:LinkButton>


        </div>
    </div>
</asp:Content>
