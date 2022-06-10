<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConvertPDF.aspx.cs" Inherits="FileConverter._ConvertPDF" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

     $(document).ready(function () {
         AjaxFileUpload_change_text();

     });

     function AjaxFileUpload_change_text() {
        //document.getElementsByClassName('ajax__fileupload_selectFileButton')[0].innerHTML = "Abrir";
        //document.getElementsByClassName('ajax__fileupload_dropzone')[0].innerHTML = "Arrastre archivo aquí";
        document.getElementsByClassName('ajax__fileupload_uploadbutton')[0].innerHTML = "Convert";
        //document.getElementById("ctl00_MainContent_AjaxFileUpload1_FileStatusContainer").innerHTML = "Seleccione archivo(s) a subir.";

         //Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "Select File";
         //Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "Drop files here";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Pending = "pending";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "Remove";
         Sys.Extended.UI.Resources.AjaxFileUpload_Upload = "Convert";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Uploaded = "Uploaded";
         //Sys.Extended.UI.Resources.AjaxFileUpload_UploadedPercentage = "uploaded {0} %";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Uploading = "Uploading";
         //Sys.Extended.UI.Resources.AjaxFileUpload_FileInQueue = "{0} file(s) in queue.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_AllFilesUploaded = "All Files Uploaded.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_FileList = "List of Uploaded files:";
         //Sys.Extended.UI.Resources.AjaxFileUpload_SelectFileToUpload = "Please select file(s) to upload.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Cancelling = "Cancelling...";
         //Sys.Extended.UI.Resources.AjaxFileUpload_UploadError = "An Error occured during file upload.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_CancellingUpload = "Cancelling upload...";
         //Sys.Extended.UI.Resources.AjaxFileUpload_UploadingInputFile = "Uploading file: {0}.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Cancel = "Cancel";
         //Sys.Extended.UI.Resources.AjaxFileUpload_Canceled = "cancelled";
         //Sys.Extended.UI.Resources.AjaxFileUpload_UploadCanceled = "File upload cancelled";
         //Sys.Extended.UI.Resources.AjaxFileUpload_DefaultError = "File upload error";
         //Sys.Extended.UI.Resources.AjaxFileUpload_UploadingHtml5File = "Uploading file: {0} of size {1} bytes.";
         //Sys.Extended.UI.Resources.AjaxFileUpload_error = "error";
     }

     function uploadCompleteAll(sender, args) {
                document.getElementById("<%=btnDownload.ClientID %>").click();
       }
</script>
    <div class="jumbotron">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
             Convert PDF to <asp:DropDownList ID="ddlFileType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFileType_SelectedIndexChanged">
            <asp:ListItem>Excel</asp:ListItem>
            <asp:ListItem Value="WordPDFLayout">Word (PDF Layout)</asp:ListItem>
            <asp:ListItem Value="WordTextLayout">Word (Text Layout)</asp:ListItem>
        </asp:DropDownList><br /><br />
                                                          </ContentTemplate></asp:UpdatePanel>
       
    <asp:AjaxFileUpload ID="ajaxUploads" runat="server"
         OnUploadComplete="ajaxUploads_UploadComplete" AllowedFileTypes="pdf" OnClientUploadCompleteAll="uploadCompleteAll" ContextKeys=""  />
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" style="display:none" />
        <div>
    </div>

</div>
</asp:Content>
