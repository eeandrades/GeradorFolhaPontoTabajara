using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Vision.V1;


namespace GeradorFolhaPontoTabajara
{
    
    class OcrGoogleCloudVision : IOcr
    {
        string[] IOcr.ConvertImageToText(Bitmap bmp)
        {

            List<string> result = new List<string>();

            byte[] bmpBytes = ConvertBmpToBytesArray(bmp);

            var client = ImageAnnotatorClient.Create();
            // Load the image file into memory


            var image = Google.Cloud.Vision.V1.Image.FromBytes(bmpBytes);
            // Performs label detection on the image file
            var response = client.DetectText(image);


            return response
                .Where(r => !string.IsNullOrEmpty(r.Description))
                .Select(r => r.Description)
                .ToArray();
        }

        private byte[] ConvertBmpToBytesArray(Bitmap bmp)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));
        }
    }
}
