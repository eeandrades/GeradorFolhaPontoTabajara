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
        public static Point Merge(this Bitmap bmp, Bitmap part, Point start, Color cor)
        {
            //var posicaoAleatoria = GeraVariacaoPosicaoAleatoria();

            for (int x = 0; x < part.Width; x++)
                for (int y = 0; y < part.Height; y++)
                {
                    var pixel = part.GetPixel(x, y);
                    var posX = start.X + x;
                    var posY = start.Y + y;

                    if (!STransparentColors.Contains(pixel.ToArgb()) && posX < bmp.Width && posY < bmp.Height)
                        bmp.SetPixel(posX, posY, cor);
                }

            return new Point(start.X + part.Width, start.Y);
        }
    }

    public static class BitmapHelper
    {
        public static Bitmap LoadIfExists(string path)
        {
            if (System.IO.File.Exists(path))
                return (Bitmap) Bitmap.FromFile(path);
            else
                return new Bitmap(0, 0);
        }
    }

}
