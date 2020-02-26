using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public interface IOcr
    {
        string[] ConvertImageToText(Bitmap image);
    }
}
