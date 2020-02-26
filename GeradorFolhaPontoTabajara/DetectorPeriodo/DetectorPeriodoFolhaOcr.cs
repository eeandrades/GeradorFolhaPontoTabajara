using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public class DetectorPeriodoFolhaOcr : IDetectorPeriodoFolha
    {

        public DateTime ObterDataReferencia(GeradorArgs args, Info info)
        {
            IOcr ocr = new OcrGoogleCloudVision();

            var bmpFolhaPonto = info.FolhaPonto;

            var imgData = bmpFolhaPonto.Clone(new Rectangle(509, 68, 60, 12), bmpFolhaPonto.PixelFormat);

            var strData = ocr.ConvertImageToText(imgData).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(strData))
            {
                if (DateTime.TryParseExact(strData.Trim(), new string[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy" }, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                else
                    throw new ArgumentException($"Erro ao obter data do documento. Data inválida. {strData}");
            }
            return default;
        }
    }
}
