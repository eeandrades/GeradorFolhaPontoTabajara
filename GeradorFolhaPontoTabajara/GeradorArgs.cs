using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public struct Atraso
        {
        public int MaximoMinutos { get; }
        public int MinimoMinutos { get; }

        public Atraso(int minimoMinutos, int maximoMinutos)
        {
            this.MinimoMinutos = minimoMinutos;
            this.MaximoMinutos = maximoMinutos;
        }
    }
    public class GeradorArgs
    {
        public GeradorArgs(Color corCaneta, Atraso margemAtraso, IOcr ocr, string pdfSourcePath, string pdfDestinationPath)
        {
            CorCaneta = corCaneta;
            MargemAtraso = margemAtraso;
            Ocr = ocr ?? throw new ArgumentNullException(nameof(ocr));
            PdfSourcePath = pdfSourcePath;
            PdfDestinationPath = pdfDestinationPath;
        }

        public Color CorCaneta { get; }
        public Atraso MargemAtraso { get; }
        public IOcr Ocr { get; }
        public string PdfSourcePath { get; }
        public string PdfDestinationPath { get; }
    }
}
