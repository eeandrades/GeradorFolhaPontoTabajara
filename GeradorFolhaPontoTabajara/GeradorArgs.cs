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
        public GeradorArgs(Color corCaneta, Atraso margemAtraso, IDetectorPeriodoFolha detectorPeriodo, string pdfSourcePath, string pdfDestinationPath)
        {
            CorCaneta = corCaneta;
            MargemAtraso = margemAtraso;
            DetectorPeriodo = detectorPeriodo ?? throw new ArgumentNullException(nameof(detectorPeriodo));
            PdfSourcePath = pdfSourcePath;
            PdfDestinationPath = pdfDestinationPath;
        }

        public Color CorCaneta { get; }
        public Atraso MargemAtraso { get; }
        public IDetectorPeriodoFolha DetectorPeriodo { get; }
        public string PdfSourcePath { get; }
        public string PdfDestinationPath { get; }
    }
}
