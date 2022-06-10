using aspose = Aspose.Pdf;
using Ionic.Zip;
//using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

namespace FileConverter
{
    public partial class _ConvertPDF : Page
    {
        //public Dictionary<string, byte[]> Files
        //{
        //    get
        //    {
        //        if (Session[hidSessionID.Value] == null)
        //            Session[hidSessionID.Value] = new Dictionary<string, byte[]>();

        //        return (Dictionary<string, byte[]>)Session[hidSessionID.Value];
        //    }
        //    set
        //    {
        //        Session[hidSessionID.Value] = value;
        //    }
        //}
        public Dictionary<string, byte[]> Files
        {
            get
            {
                if (Session["Files"] == null)
                    Session["Files"] = new Dictionary<string, byte[]>();

                return (Dictionary<string, byte[]>)Session["Files"];
            }
            set
            {
                if (value == null) Session.Remove("Files");
                Session["Files"] = value;
            }
        }
        //public string FileType
        //{
        //    get
        //    {
        //        if (Response.Cookies["FileType"] == null)
        //            Session["FileType"] = "Excel";

        //        return Session["FileType"].ToString();
        //    }
        //    set
        //    {
        //        if (value == null) Session.Remove("FileType");
        //        Session["FileType"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                Files = null;
                Response.Cookies["FileType"].Value = ddlFileType.SelectedValue;
            }
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {

        }
        void AddToExcel(string fileName, byte[] bytes)
        {
            //var file = fuAttachment.PostedFile;
            //if (file != null)
            //{
            //    var content = new byte[file.ContentLength];
            //    file.InputStream.Read(content, 0, content.Length);
            //    Session["FileContent"] = content;
            //    Session["FileContentType"] = file.ContentType;
            //}
            lock (Files)
            {
                //var files = Files;
                //PdfDocument pdfDocument = new PdfDocument(bytes);
                //string outFileName = Path.GetFileNameWithoutExtension(fileName) + ".xlsx";
                //pdfDocument.ConvertOptions.SetPdfToXlsxOptions(PdfToXlsxLayout.Text);
                //using (MemoryStream pdfStream = new MemoryStream())
                //{
                //    pdfDocument.SaveToStream(pdfStream, FileFormat.XLSX);
                //    if (files.ContainsKey(outFileName))
                //    {
                //        files[outFileName] = pdfStream.ToArray();
                //    }
                //    else
                //    {
                //        files.Add(outFileName, pdfStream.ToArray());
                //    }
                //    // Dispose memory
                //    pdfStream.Dispose();
                //}
                //// Dispose memory
                //pdfDocument.Dispose();
                //Files = files;

                aspose.ExcelSaveOptions saveOptions = new aspose.ExcelSaveOptions();
                //saveOptions.UniformWorksheets = true;
                //saveOptions.ConversionEngine = aspose.ExcelSaveOptions.ConversionEngines.LegacyEngine;
                //saveOptions.ScaleFactor = 2.5;
                saveOptions.Format = aspose.ExcelSaveOptions.ExcelFormat.XLSX;

                var files = Files;
                string outFileName = Path.GetFileNameWithoutExtension(fileName) + ".xlsx";
                MemoryStream stream = new MemoryStream(bytes);
                aspose.Document pdfDocument = new aspose.Document(stream);

                List<string> list = new List<string>();
                foreach (var page in pdfDocument.Pages)
                {
                    foreach (var img in page.Resources.Images)
                    {
                        var str = CallBackGetHocr(img);
                        list.Add(str);
                    }
                }
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    pdfDocument.Save(pdfStream, saveOptions);//aspose.SaveFormat.DocX,
                    files.Add(outFileName, pdfStream.ToArray());
                    // Dispose memory
                    pdfStream.Dispose();
                }
                // Dispose memory
                pdfDocument.Dispose();
                Files = files;
                // Save the file into MS document format
                stream.Dispose();
            }
        }
        static string CallBackGetHocr(aspose.XImage img)
        {
            FileStream outputImage = new FileStream(@"D:\Test\"+ DateTime.Now.ToString("HHmmssfff") + ".jpg", FileMode.Create);

            //save output image
            img.Save(outputImage, ImageFormat.Png);
            outputImage.Close();
            return "";
            // Initialize an instance of OcrEngine
            //Aspose.OCR.OcrEngine ocrEngine = new Aspose.OCR.OcrEngine();
            //var ms = new MemoryStream();

            //img.Save(ms, ImageFormat.Jpeg); // put here the image format 
            //ms.Position = 0;


            //// Set the Image property by loading the image from file path location or an instance of MemoryStream 
            //ocrEngine.Image = Aspose.OCR.ImageStream.FromStream(ms, Aspose.OCR.ImageStreamFormat.Jpg);

            //    // Process the image
            //    ocrEngine.Process();
            //string text = ocrEngine.Text.ToString();
            //return text;
        }
        void AddToWord(string fileName, byte[] bytes, aspose.DocSaveOptions.RecognitionMode recognitionMode)
        {
            lock (Files)
            {
                //var files = Files;
                //PdfDocument pdfDocument = new PdfDocument(bytes);
                //string outFileName = Path.GetFileNameWithoutExtension(fileName) + ".docx";
                //pdfDocument.ConvertOptions.SetPdfToDocOptions(true, useFlowRecognitionMode);
                //using (MemoryStream pdfStream = new MemoryStream())
                //{
                //    pdfDocument.SaveToStream(pdfStream, FileFormat.DOCX);
                //    files.Add(outFileName, pdfStream.ToArray());
                //    // Dispose memory
                //    pdfStream.Dispose();
                //}
                //// Dispose memory
                //pdfDocument.Dispose();
                //Files = files;


                aspose.DocSaveOptions saveOptions = new aspose.DocSaveOptions();
                saveOptions.Mode = recognitionMode;
                //saveOptions.RelativeHorizontalProximity = 2.5f;
                saveOptions.RecognizeBullets = true;
                saveOptions.Format = aspose.DocSaveOptions.DocFormat.DocX;

                var files = Files;
                string outFileName = Path.GetFileNameWithoutExtension(fileName) + ".docx";
                MemoryStream stream = new MemoryStream(bytes);
                aspose.Document pdfDocument = new aspose.Document(stream);
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    pdfDocument.Save(pdfStream, saveOptions);//aspose.SaveFormat.DocX,
                    files.Add(outFileName, pdfStream.ToArray());
                    // Dispose memory
                    pdfStream.Dispose();
                }
                // Dispose memory
                pdfDocument.Dispose();
                Files = files;
                // Save the file into MS document format
                stream.Dispose();
            }
        }


        protected void ajaxUploads_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            switch (Request.Cookies["FileType"].Value)
            {
                case "Excel":
                    AddToExcel(e.FileName, e.GetContents());
                    break;
                case "WordPDFLayout":
                    AddToWord(e.FileName, e.GetContents(), aspose.DocSaveOptions.RecognitionMode.Flow);
                    break;
                case "WordTextLayout":
                    AddToWord(e.FileName, e.GetContents(), aspose.DocSaveOptions.RecognitionMode.EnhancedFlow);
                    break;
            }
            e.DeleteTemporaryData();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Files.Count == 1)
            {
                var file = Files.First();
                Response.AppendHeader("content-disposition", "attachment; filename=" + file.Key);
                Response.ContentType = "application/octetstream";
                Response.Clear();
                Response.OutputStream.Write(file.Value, 0, file.Value.Length);
            }
            else
            {
                ZipFile zip = new ZipFile();
                foreach (var stream in Files)
                {
                    zip.AddEntry(stream.Key, stream.Value);
                }

                zip.Dispose();

                Response.AppendHeader("content-disposition", "attachment; filename=" + "result.zip");
                Response.ContentType = "application/octetstream";
                Response.Clear();
                zip.Save(Response.OutputStream);
            }

            Response.Flush();
            Files = null;
            Response.End();
        }

        protected void ddlFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Cookies["FileType"].Value = ddlFileType.SelectedValue;
            //FileType = ddlFileType.SelectedValue;
        }
    }
}