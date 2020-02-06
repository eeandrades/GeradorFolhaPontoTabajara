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
        int MaximoMinutos { get; }
        int MinimoMinutos { get; }

        public Atraso(int minimoMinutos, int maximoMinutos)
        {
            this.MinimoMinutos = minimoMinutos;
            this.MaximoMinutos = maximoMinutos;
        }
    }
    public class GeradorArgs
    {
        public GeradorArgs(Color corCaneta, Atraso margemAtraso, string pdfSourcePath, string pdfDestinationPath)
        {
            CorCaneta = corCaneta;
            MargemAtraso = margemAtraso;
            PdfSourcePath = pdfSourcePath;
            PdfDestinationPath = pdfDestinationPath;
        }

        public Color CorCaneta { get; }
        public Atraso MargemAtraso { get; }
        public string PdfSourcePath { get; }
        public string PdfDestinationPath { get; }
    }
}
