using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public class GeradorNumeros
    {
        const int horSpace = 2;
        const int vertSpace = 1;

        private static string PathNumeros = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Padroes\");
        private static readonly Random SRandom = new Random(100);
        private static int NumeroPastas = System.IO.Directory.GetDirectories(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Padroes")).Length;

        public static Bitmap FromNumber(int numero, int paddingLeft)
        {
            if (numero < 0)
                throw new InvalidCastException("O número deve ser positivo");


            var arrNumeros = Convert.ToString(numero).PadLeft(paddingLeft, '0').ToArray();


            List<Bitmap> bitmapNumeros = new List<Bitmap>();

            int bmpW = 0;
            int bmpH = 0;


            foreach (var num in arrNumeros)
            {
                var indexPasta = SRandom.Next(1, NumeroPastas);

                var path = System.IO.Path.Combine(PathNumeros+ indexPasta, $"{num}.png");

                var bmpNum = new Bitmap(path);

                bmpW += bmpNum.Width + horSpace;

                bmpH = bmpNum.Height > bmpH ? bmpNum.Height : bmpH;

                bitmapNumeros.Add(bmpNum);
            }

            var result = new Bitmap(bmpW, bmpH + 2 * vertSpace);

            var x = 0;

            foreach (var bmpNum in bitmapNumeros)
            {
                result.Merge(bmpNum, new Point(x, vertSpace), Color.Blue);

                x += bmpNum.Width + horSpace;
            }
            return result;
        }



        public static Bitmap FromTimeSpan(TimeSpan timeSpan)
        {
            var imgHora = FromNumber(timeSpan.Hours, 2);
            var imgMinute = FromNumber(timeSpan.Minutes, 2);

            var doisPontos = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(PathNumeros+"1", "dois-pontos.png"));

            var result = new Bitmap(imgHora.Width + doisPontos.Width + imgMinute.Width + horSpace *3, imgHora.Height);

            var nextPoint = result.Merge(imgHora, new Point(0, 0), Color.Blue);

            result.Merge(doisPontos, new Point(nextPoint.X +  horSpace, 5 * vertSpace), Color.Blue);

            result.Merge(imgMinute, new Point(nextPoint.X +  2* horSpace, nextPoint.Y), Color.Blue);

            return result;
        }
    }
}
