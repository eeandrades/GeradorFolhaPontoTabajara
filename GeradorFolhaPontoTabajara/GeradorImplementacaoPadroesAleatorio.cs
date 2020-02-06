using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    /// <summary>
    /// Implementar ao gerador utilizando padrões de bitmpas com os intervalos de hora e assinatura
    /// </summary>

    class GeradorImplementacaoPadroesAleatorio : GeradorBase
    {
        private string[] PastaPadroes { get; }

        public GeradorImplementacaoPadroesAleatorio()
        {
            var pastaPadroes = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Padroes");
            this.PastaPadroes = System.IO.Directory.GetDirectories(pastaPadroes);
        }

        private const int SPosicaoInicio = 103;
        private const int SPosicaoIntervaloInicio = 207;
        private const int SPosicaoIntervaloFim = 264;
        private const int SPosicaoFim = 316;
        private const int SPosicaoAssinatura = 625;
        private static readonly int[] SCorFundoPadrao = { 0, 16777215 };
        private static int[] SCoresMarcacaoTabela = { -13553359, -16316665, -16579837, -16777216 };
        private static readonly Random SRandom = new Random(100);

        private Color DoGetCorCaneta() => Color.Blue;

        private static bool IncrementaLinha(Bitmap bmp, ref int linhaAtual)
        {
            linhaAtual += 10;
            int count = 0;
            while (true)
            {

                if (bmp.GetPixel(SPosicaoInicio, linhaAtual).ToArgb() == -16777216)
                {
                    break;
                }

                if (count++ > 30)
                    return false;

                linhaAtual++;
            }

            return true;

        }

        private void MesclaBitmap(Bitmap bmp, Bitmap part, Point start)
        {
            var posicaoAleatoria = GeraVariacaoPosicaoAleatoria();
            bmp.Merge(part, new Point(posicaoAleatoria.X + start.X, posicaoAleatoria.Y + start.Y), DoGetCorCaneta());
        }
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

        private Padrao GerarPadrao()
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

        protected override void DoPreencher(Bitmap bmpFolhaPonto)
        {
            int numeroPastas = PastaPadroes.Length;
            Bitmap[] inicio = new Bitmap[numeroPastas];
            Bitmap[] intervaloInicio = new Bitmap[numeroPastas];
            Bitmap[] intervaloFim = new Bitmap[numeroPastas];
            Bitmap[] fim = new Bitmap[numeroPastas];
            Bitmap[] assinatura = new Bitmap[numeroPastas];

            var pathPadrao = this.PastaPadroes[0];

            var path = System.IO.Path.Combine(pathPadrao);

            for (int i = 0; i < numeroPastas; i++)
            {

                pathPadrao = this.PastaPadroes[i];

                path = System.IO.Path.Combine(pathPadrao);

                inicio[i] = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "inicio.png"));
                intervaloInicio[i] = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntInicio.png"));
                intervaloFim[i] = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntFim.png"));
                fim[i] = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Fim.png"));
                assinatura[i] = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Assinatura.png"));

            }
            
            Random rnd = new Random();
            rnd.Shuffle(inicio);

            rnd.Shuffle(intervaloInicio);

            rnd.Shuffle(intervaloFim);

            rnd.Shuffle(fim);

            rnd.Shuffle(assinatura);


            int indexAtual = 0;
            var linha = 203;
            for (int dia = 1; dia <= 31; dia++)
            {
                if (indexAtual == numeroPastas)
                {
                    indexAtual = 0;
                    rnd.Shuffle(inicio);

                    rnd.Shuffle(intervaloInicio);

                    rnd.Shuffle(intervaloFim);

                    rnd.Shuffle(fim);

                    rnd.Shuffle(assinatura);

                }
                int linhaConteudo = linha + 10;

                bool feriado = bmpFolhaPonto.GetPixel(SPosicaoInicio, linhaConteudo).ToArgb() == -4473925;
                var marcacaoTabela = bmpFolhaPonto.GetPixel(28, linhaConteudo);
                bool areaTabela = dia < 29 || SCoresMarcacaoTabela.Contains(marcacaoTabela.ToArgb());

                if (!feriado && areaTabela)
                {
                    var padrao = new Padrao()
                    {
                        Inicio = inicio[indexAtual],
                        IntervaloInicio = intervaloInicio[indexAtual],
                        IntervaloFim = intervaloFim[indexAtual],
                        Fim = fim[indexAtual],
                        Assinatura = assinatura[indexAtual]
                    };

                    MesclaBitmap(bmpFolhaPonto, padrao.Inicio, new Point(SPosicaoInicio, linha));
                    MesclaBitmap(bmpFolhaPonto, padrao.IntervaloInicio, new Point(SPosicaoIntervaloInicio, linha));
                    MesclaBitmap(bmpFolhaPonto, padrao.IntervaloFim, new Point(SPosicaoIntervaloFim, linha));
                    MesclaBitmap(bmpFolhaPonto, padrao.Fim, new Point(SPosicaoFim, linha));
                    MesclaBitmap(bmpFolhaPonto, padrao.Assinatura, new Point(SPosicaoAssinatura, linha));
                }
                indexAtual++;
                if (!areaTabela || !IncrementaLinha(bmpFolhaPonto, ref linha))
                    break;
            }
        }

       
    }
    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
