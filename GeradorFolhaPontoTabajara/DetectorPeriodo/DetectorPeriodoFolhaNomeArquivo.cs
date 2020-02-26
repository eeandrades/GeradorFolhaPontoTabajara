using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace GeradorFolhaPontoTabajara
{
    /// <summary>
    /// Detecta o periodo da folha pelo nome do arquivo
    /// </summary>
    public class DetectorPeriodoFolhaNomeArquivo : IDetectorPeriodoFolha
    {
        private static string ExtratDateStringFromPath(string path)
        {
            const string datePattern = @"\d{1,2}_\d{1,2}_\d{4}";
            Regex regex = new Regex(datePattern);
            Match match = regex.Match(path);
            return match.Success ? match.Value : string.Empty;
        }

        private static DateTime StringToDatetime(string strDate)
        {
            const Int16 anoPos = 2;
            const Int16 mesPos = 1;
            const Int16 diaPos = 0;

            var arrData = strDate.Split('_')
                .Select(p => Convert.ToInt32(p))
                .ToArray();
            return new DateTime(arrData[anoPos], arrData[mesPos], arrData[diaPos]);
        }

        DateTime IDetectorPeriodoFolha.ObterDataReferencia(GeradorArgs args, Info info)
        {
            var strDate = ExtratDateStringFromPath(args.PdfSourcePath);

            return !string.IsNullOrEmpty(strDate) ? StringToDatetime(strDate) : default;
        }
    }
}
