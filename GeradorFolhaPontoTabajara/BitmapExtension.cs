using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public static class BitmapExtension
    {
        private static readonly int[] STransparentColors = { 0, 16777215 };
        public static void Merge(this Bitmap bmp, Bitmap part, Point start, Color cor)
        {
            //var posicaoAleatoria = GeraVariacaoPosicaoAleatoria();

            for (int x = 0; x < part.Width; x++)
                for (int y = 0; y < part.Height; y++)
                {
                    var pixel = part.GetPixel(x, y);
                    if (!STransparentColors.Contains(pixel.ToArgb()))
                        bmp.SetPixel(start.X + x, start.Y + y, cor);
                }
        }
    }
}
