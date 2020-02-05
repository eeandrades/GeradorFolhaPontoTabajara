using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public abstract class GeradorBase: IGerador
    {
        private Bitmap PdfToBitmap(string pdfPath)
        {
            PdfDocument documemt = new PdfDocument();
            documemt.LoadFromFile(pdfPath);
            return (Bitmap)documemt.SaveAsImage(0, PdfImageType.Bitmap);
        }

        private PdfDocument BitmapToPdf(Bitmap bitmap)
        {
            PdfDocument document = new PdfDocument();
            var page = document.Pages.Add();
            page.Canvas.DrawImage(PdfImage.FromImage(bitmap), new PointF(0, 0), bitmap.Size);
            return document;
        }

        private void Save(string pdfDestinationPath, Bitmap img)
        {
            string outpuPngFile = pdfDestinationPath + ".png";
            if (System.IO.File.Exists(outpuPngFile))
                System.IO.File.Delete(outpuPngFile);
            img.Save(pdfDestinationPath + ".png", ImageFormat.Png);

            //var newPdf = BitmapToPdf(img);
            //newPdf.SaveToFile(pdfDestinationPath);
        }

        protected abstract void DoPreencher(Bitmap bmpFolhaPonto);

        void IGerador.Execute(string pdfSourcePath, string pdfDestinationPath)
        {
            var img = PdfToBitmap(pdfSourcePath);
            DoPreencher(img);
            Save(pdfDestinationPath, img);
        }
    }
}
