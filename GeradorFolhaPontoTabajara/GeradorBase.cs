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
    public abstract class GeradorBase : IGerador
    {
        const int SPosicaoInicialLinha = 203;
        const int SPosicaoInicio = 103;
        const int SPosicaoIntervaloInicio = 207;
        const int SPosicaoIntervaloFim = 264;
        const int SPosicaoFim = 316;
        const int SPosicaoAssinatura = 625;


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

        private static readonly Random SRandom = new Random(100);

        private static Point GeraVariacaoPosicaoAleatoria()
        {
            return new Point(
                SRandom.Next(0, 4),
                SRandom.Next(0, 5));
        }

        private void MesclaBitmap(Bitmap bmp, Bitmap part, Point start)
        {
            var posicaoAleatoria = GeraVariacaoPosicaoAleatoria();
            bmp.Merge(part, new Point(posicaoAleatoria.X + start.X, posicaoAleatoria.Y + start.Y), Color.Blue);
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

        protected virtual void DoBeforePreencherTabelaHorarios(Bitmap bmpFolhaPonto)
        {

        }

        protected virtual void DoAfterPreencherTabelaHorarios(Bitmap bmpFolhaPonto)
        {

        }


        private void PreencherTabelaHorarios(Bitmap bmpFolhaPonto)
        {
            this.DoBeforePreencherTabelaHorarios(bmpFolhaPonto);

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
                    var conteudoLinha = this.DoGetConteudoLinha(dia-1);


                    MesclaBitmap(bmpFolhaPonto, conteudoLinha.Inicio, new Point(SPosicaoInicio, linha));
                    MesclaBitmap(bmpFolhaPonto, conteudoLinha.IntervaloInicio, new Point(SPosicaoIntervaloInicio, linha));
                    MesclaBitmap(bmpFolhaPonto, conteudoLinha.IntervaloFim, new Point(SPosicaoIntervaloFim, linha));
                    MesclaBitmap(bmpFolhaPonto, conteudoLinha.Fim, new Point(SPosicaoFim, linha));
                    MesclaBitmap(bmpFolhaPonto, conteudoLinha.Assinatura, new Point(SPosicaoAssinatura, linha));
                }

                if (!areaTabela || !IncrementaLinha(bmpFolhaPonto, ref linha))
                    break;
            }

            this.DoAfterPreencherTabelaHorarios(bmpFolhaPonto);
        }

        protected abstract Padrao DoGetConteudoLinha(int numeroLinha);


        void IGerador.Execute(string pdfSourcePath, string pdfDestinationPath)
        {
            var img = PdfToBitmap(pdfSourcePath);
            PreencherTabelaHorarios(img);
            Save(pdfDestinationPath, img);
        }
    }
}
