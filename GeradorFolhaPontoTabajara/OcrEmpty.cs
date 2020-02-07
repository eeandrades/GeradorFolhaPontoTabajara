using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    class OcrEmpty : IOcr
    {
        string[] IOcr.ConvertImageToText(Bitmap image)
        {
            return new string[0];
        }
    }
}
