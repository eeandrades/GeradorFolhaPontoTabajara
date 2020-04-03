using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GeradorFolhaPontoTabajara
{
    public abstract class GeradorBase : IGerador
    {
        const int SPosicaoInicialLinha = 203;
        const int SPosicaoInicio = 103;
        const int SPosicaoIntervaloInicio = 207;
        const int SPosicaoIntervaloFim = 258;
        const int SPosicaoFim = 316;
        const int SPosicaoAssinatura = 625;
        private const int SPosicaoTopData = 995;
        private const int SPosicaoLeftDataDia = 76;
        private const int SPosicaoLeftDataMes = 106;
        private const int SPosicaoLeftDataAno = 136;

        const int SPosicaoAssinaturaTop = 62;
        const int SPosicaoAssinaturaLeft = 400;
        const int SPosicaoAssinaturaWidth = 270;

        protected string[] PastasImagens { get; }


        public GeradorBase()
        {
            var pastaPadroes = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Padroes");
            this.PastasImagens = System.IO.Directory.GetDirectories(pastaPadroes);
        }

        private int GerarIndexAleatorioPadrao()
        {
            return SRandom.Next(0, PastasImagens.Length - 1);
        }

        protected string GetPastaImagemAleatoria()
        {
            var indexAleatorio = GerarIndexAleatorioPadrao();
            return this.PastasImagens[indexAleatorio];
        }


        private Bitmap PdfToBitmap(string pdfPath)
        {
            PdfDocument documemt = new PdfDocument();
            documemt.LoadFromFile(pdfPath);
            return (Bitmap)documemt.SaveAsImage(0, PdfImageType.Bitmap);
        }

        private PdfDocument BitmapToPdf(Bitmap bitmap)
        {
            PdfDocument doc = new PdfDocument();
            PdfImage pdfimage = PdfImage.FromImage(bitmap);

            PdfUnitConvertor uinit = new PdfUnitConvertor();
            SizeF pageSize = uinit.ConvertFromPixels(bitmap.Size, PdfGraphicsUnit.Point);
            PdfPageBase page = doc.Pages.Add(pageSize, new PdfMargins(0f));

            page.Canvas.DrawImage(pdfimage, new PointF(0, 0));

            return doc;
        }

        private void Save(string pdfDestinationPath, Bitmap img)
        {
            var newPdf = BitmapToPdf(img);
            newPdf.SaveToFile(pdfDestinationPath);
        }

        private static readonly Random SRandom = new Random(100);

        private static Point GeraVariacaoPosicaoAleatoria(int minX, int minY, int maxX, int maxY)
        {
            return new Point(
                SRandom.Next(minX, maxX),
                SRandom.Next(minY, maxY));
        }

        private static Point GeraVariacaoPosicaoAleatoria()
        {
            return GeraVariacaoPosicaoAleatoria(0, 0, 1, 1);
        }



        private void MesclaBitmap(Bitmap bmp, Bitmap part, Point start, Color corCaneta)
        {
            var posicaoAleatoria = GeraVariacaoPosicaoAleatoria();
            bmp.Merge(part, new Point(posicaoAleatoria.X + start.X, posicaoAleatoria.Y + start.Y), corCaneta);
        }

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

        protected virtual void DoBeforePreencherTabelaHorarios(GeradorArgs args, Info info)
        {

        }

        protected virtual void DoAfterPreencherTabelaHorarios(GeradorArgs args, Info info)
        {

        }

        private Info GetInformacoes(Bitmap bmpFolhaPonto, out IEnumerable<int> linhasConteudos)
        {
            int qtdDias = 0;
            int ultimoDiaUtil = 1;
            var linhasValidas = new List<int>();

            var linha = SPosicaoInicialLinha;
            int[] SCoresMarcacaoTabela = { -13553359, -16316665, -16579837, -16777216 };

            for (int dia = 1; dia <= 31; dia++)
            {
                int linhaConteudo = linha + 10;

                bool feriado = bmpFolhaPonto.GetPixel(SPosicaoInicio, linhaConteudo).ToArgb() == -4473925;
                var marcacaoTabela = bmpFolhaPonto.GetPixel(28, linhaConteudo);
                bool areaTabela = dia < 29 || SCoresMarcacaoTabela.Contains(marcacaoTabela.ToArgb());

                if (!feriado && areaTabela)
                {
                    linhasValidas.Add(linha);
                    ultimoDiaUtil = dia;
                }

                if (!areaTabela || !IncrementaLinha(bmpFolhaPonto, ref linha))
                    break;
                qtdDias++;
            }

            linhasConteudos = linhasValidas;

            return new Info(bmpFolhaPonto, qtdDias, linhasValidas.Count, ultimoDiaUtil);
        }


        private Info PreencherTabelaHorarios(GeradorArgs args, Bitmap bmpFolhaPonto)
        {
            var info = GetInformacoes(bmpFolhaPonto, out var linhasConteudo);

            this.DoBeforePreencherTabelaHorarios(args, info);

            int indexLinha = 0;
            Random rnd = new Random();

            foreach (int linha in linhasConteudo)
            {
                var conteudoLinha = this.DoGetConteudoLinha(args, info, indexLinha++);
                MesclaBitmap(bmpFolhaPonto, conteudoLinha.Inicio, new Point(SPosicaoInicio, linha + 1), args.CorCaneta);
                MesclaBitmap(bmpFolhaPonto, conteudoLinha.IntervaloInicio, new Point(SPosicaoIntervaloInicio, linha + 1), args.CorCaneta);
                MesclaBitmap(bmpFolhaPonto, conteudoLinha.IntervaloFim, new Point(SPosicaoIntervaloFim, linha + 1), args.CorCaneta);
                MesclaBitmap(bmpFolhaPonto, conteudoLinha.Fim, new Point(SPosicaoFim, linha + 1), args.CorCaneta);
                MesclaBitmap(bmpFolhaPonto, conteudoLinha.Assinatura, new Point(rnd.Next(SPosicaoAssinatura - 5, SPosicaoAssinatura + 5) - 35, linha - 4), args.CorCaneta);
            }


            this.DoAfterPreencherTabelaHorarios(args, info);

            return info;
        }

        protected abstract Padrao DoGetConteudoLinha(GeradorArgs args, Info info, int numeroLinha);

        private static int GetPosicaoTopDataAssinatura(Bitmap bmpFolhaPonto)
        {
            int y = bmpFolhaPonto.Height - 1;
            while (true)
            {
                var pixel = bmpFolhaPonto.GetPixel(SPosicaoInicio, y);
                if (pixel.ToArgb() != -1 || y == 100)
                    break;
                y--;
            }

            return y - 23;
        }

        private void PreencherData(GeradorArgs args, Info info, Bitmap bmpFolhaPonto)
        {
            var delta = GeraVariacaoPosicaoAleatoria(0, -3, 2, 1);
            var pathImagem = GetPastaImagemAleatoria();

            var date = args.DetectorPeriodo.ObterDataReferencia(args, info);

            if (date != default)
            {
                var dia = GeradorNumeros.FromNumber(pathImagem, info.UltimoDiaUtil, 2);
                var mes = GeradorNumeros.FromNumber(pathImagem, date.Month, 2);
                var ano = GeradorNumeros.FromNumber(pathImagem, date.Year, 4);

                var y = GetPosicaoTopDataAssinatura(bmpFolhaPonto) - dia.Height + delta.Y;

                bmpFolhaPonto.Merge(dia, new Point(SPosicaoLeftDataDia + delta.X, y), args.CorCaneta);
                bmpFolhaPonto.Merge(mes, new Point(SPosicaoLeftDataMes + delta.X, y), args.CorCaneta);
                bmpFolhaPonto.Merge(ano, new Point(SPosicaoLeftDataAno + delta.X, y), args.CorCaneta);
            }
        }

        private Bitmap GetAssinatura()
        {
            var pathImagem = GetPastaImagemAleatoria();
            return System.IO.File.Exists(System.IO.Path.Combine(pathImagem, "assinatura-completa.png")) ?
                new Bitmap(System.IO.Path.Combine(pathImagem, "assinatura-completa.png")) :
                new Bitmap(System.IO.Path.Combine(pathImagem, "assinatura.png"));
        }

        private Bitmap PreencherAssinatura(GeradorArgs args, Bitmap bmpFolhaPonto)
        {
            var delta = GeraVariacaoPosicaoAleatoria(-20, -5, 20, 0);
            delta.Y = -5;
            var assinatura = this.GetAssinatura();
            var xCentralizado = SPosicaoAssinaturaLeft + (SPosicaoAssinaturaWidth - assinatura.Width) / 2;
            var y = GetPosicaoTopDataAssinatura(bmpFolhaPonto) - assinatura.Height;
            bmpFolhaPonto.Merge(assinatura, new Point(xCentralizado + delta.X, y + delta.Y), args.CorCaneta);
            AddNoise(bmpFolhaPonto, new Random().Next(10, 40));
            return RotateBitmap2(bmpFolhaPonto, new Random().Next(-3, 3));
        }

        public static Bitmap AddNoise(Bitmap OriginalImage, int Amount)
        {
            Random TempRandom = new Random();
            for (int x = 0; x < OriginalImage.Width; ++x)
            {
                for (int y = 0; y < OriginalImage.Height; ++y)
                {
                    Amount = new Random().Next(10, 40);
                    Color CurrentPixel = OriginalImage.GetPixel(x, y);
                    int R = CurrentPixel.R + TempRandom.Next(-Amount, Amount + 1);
                    int G = CurrentPixel.G + TempRandom.Next(-Amount, Amount + 1);
                    int B = CurrentPixel.B + TempRandom.Next(-Amount, Amount + 1);
                    R = R > 255 ? 255 : R;
                    R = R < 0 ? 0 : R;
                    G = G > 255 ? 255 : G;
                    G = G < 0 ? 0 : G;
                    B = B > 255 ? 255 : B;
                    B = B < 0 ? 0 : B;
                    Color TempValue = Color.FromArgb(R, G, B);
                    OriginalImage.SetPixel(x, y, TempValue);
                }
            }
            return OriginalImage;
        }


        private Bitmap RotateBitmap2(Bitmap bm, float angle)
        {
            angle = angle / 3;
            // Make a Matrix to represent rotation
            // by this angle.
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(angle);

            // Rotate the image's corners to see how big
            // it will be after rotation.
            PointF[] points =
            {
        new PointF(0, 0),
        new PointF(bm.Width, 0),
        new PointF(bm.Width, bm.Height),
        new PointF(0, bm.Height),
    };
            rotate_at_origin.TransformPoints(points);
            float xmin, xmax, ymin, ymax;
            GetPointBounds(points, out xmin, out xmax,
                out ymin, out ymax);

            // Make a bitmap to hold the rotated result.
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);

            Bitmap result = new Bitmap(wid, hgt);

            // Create the real rotation transformation.
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle,
                new PointF(wid / 2f, hgt / 2f));

            // Draw the image onto the new bitmap rotated.
            using (Graphics gr = Graphics.FromImage(result))
            {
                // Use smooth image interpolation.
                gr.InterpolationMode = InterpolationMode.High;

                // Clear with the color in the image's upper left corner.
                gr.Clear(Color.White);

                //// For debugging. (It's easier to see the background.)
                //gr.Clear(Color.LightBlue);

                // Set up the transformation to rotate.
                gr.Transform = rotate_at_center;

                // Draw the image centered on the bitmap.
                int x = (wid - bm.Width) / 2;
                int y = (hgt - bm.Height) / 2;
                gr.DrawImage(bm, x, y);
            }

            // Return the result bitmap.
            return result;
        }

        private void GetPointBounds(PointF[] points,
    out float xmin, out float xmax,
    out float ymin, out float ymax)
        {
            xmin = points[0].X;
            xmax = xmin;
            ymin = points[0].Y;
            ymax = ymin;
            foreach (PointF point in points)
            {
                if (xmin > point.X) xmin = point.X;
                if (xmax < point.X) xmax = point.X;
                if (ymin > point.Y) ymin = point.Y;
                if (ymax < point.Y) ymax = point.Y;
            }
        }

        void IGerador.Execute(GeradorArgs args)
        {
            var img = PdfToBitmap(args.PdfSourcePath);
            var info = this.PreencherTabelaHorarios(args, img);
            this.PreencherData(args, info, img);
            img = this.PreencherAssinatura(args, img);
            this.Save(args.PdfDestinationPath, img);
        }
    }
}
