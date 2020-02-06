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

    class GeradorAleatorio : GeradorBase
    {
        private string[] PastaPadroes { get; }

        public GeradorAleatorio()
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

        private Padrao[] _conteudosAleatorios = null;

        private static int GerarMinutoAleatorio()
        {
            return SRandom.Next(-5, 15);
        }

        private TimeSpan GerarTimespanAleatorio(int hora)
        {
            return new TimeSpan(hora, GerarMinutoAleatorio(), 0);
        }

        private Padrao[] GeraConteudosAleatorios()
        {

            int indexPadrao = GerarIndexAleatorioPadrao();
            var pathPadrao = this.PastaPadroes[indexPadrao];

            var result = new List<Padrao>();
            for (int dia = 1; dia <= 31; dia++)
            {
                result.Add(new Padrao()
                {
                    Inicio = GeradorNumeros.FromTimeSpan(GerarTimespanAleatorio(8)),
                    IntervaloInicio = GeradorNumeros.FromTimeSpan(GerarTimespanAleatorio(12)),
                    IntervaloFim = GeradorNumeros.FromTimeSpan(GerarTimespanAleatorio(14)),
                    Fim = GeradorNumeros.FromTimeSpan(GerarTimespanAleatorio(18)),
                    Assinatura = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(pathPadrao, "Assinatura.png"))
                });
            }

            return result.ToArray();
        }

        protected override void DoBeforePreencherTabelaHorarios(GeradorArgs args, Info info)
        {
            this._conteudosAleatorios = this.GeraConteudosAleatorios();
        }

        protected override Padrao DoGetConteudoLinha(GeradorArgs args, Info info, int numeroLinha)
        {            
            return this._conteudosAleatorios[numeroLinha];
        }
    }
}
