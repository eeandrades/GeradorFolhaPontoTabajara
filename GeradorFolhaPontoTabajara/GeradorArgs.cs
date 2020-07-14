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
        public GeradorArgs(Color corCaneta, Atraso margemAtraso, IDetectorPeriodoFolha detectorPeriodo, bool assinar, string pdfSourcePath, string pdfDestinationPath)
        {
            this.CorCaneta = corCaneta;
            this.MargemAtraso = margemAtraso;
            this.DetectorPeriodo = detectorPeriodo ?? throw new ArgumentNullException(nameof(detectorPeriodo));
            this.PdfSourcePath = pdfSourcePath;
            this.PdfDestinationPath = pdfDestinationPath;
            this.Assinar = assinar;
        }

        public bool Assinar { get; set; }
        public Color CorCaneta { get; }
        public Atraso MargemAtraso { get; }
        public IDetectorPeriodoFolha DetectorPeriodo { get; }
        public string PdfSourcePath { get; }
        public string PdfDestinationPath { get; }
    }
}
