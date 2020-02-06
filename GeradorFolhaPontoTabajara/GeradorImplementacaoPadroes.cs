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
    /// <summary>
    /// Implementar ao gerador utilizando padrões de bitmpas com os intervalos de hora e assinatura
    /// </summary>

    class GeradorImplementacaoPadroes : GeradorBase
    {
        private string[] PastaPadroes { get; }

        public GeradorImplementacaoPadroes()
        {
            var pastaPadroes = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Padroes");
            this.PastaPadroes = System.IO.Directory.GetDirectories(pastaPadroes);
        }

        private static readonly Random SRandom = new Random(100);

        
        private int GerarIndexAleatorioPadrao()
        {
            return SRandom.Next(0, PastaPadroes.Length - 1);
        }

        private static Point GeraVariacaoPosicaoAleatoria()
        {
            return new Point(
                SRandom.Next(0, 4),
                SRandom.Next(0, 5));
        }

        protected override Padrao DoGetConteudoLinha(int numeroLinha)
        {
            int indexPadrao = GerarIndexAleatorioPadrao();
            var pathPadrao = this.PastaPadroes[indexPadrao];

            var path = System.IO.Path.Combine(pathPadrao);
            return new Padrao()
            {
                Inicio = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "inicio.png")),
                IntervaloInicio = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntInicio.png")),
                IntervaloFim = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntFim.png")),
                Fim = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Fim.png")),
                Assinatura = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Assinatura.png"))
            };
        }

    }
}
