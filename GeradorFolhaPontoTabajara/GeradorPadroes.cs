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
    /// <summary>
    /// Implementar ao gerador utilizando padrões de bitmpas com os intervalos de hora e assinatura
    /// </summary>

    class GeradorPadroes : GeradorBase
    {
        protected override Padrao DoGetConteudoLinha(GeradorArgs args, Info info, int numeroLinha)
        {
            var path = base.GetPastaImagemAleatoria();
            return new Padrao()
            {
                Inicio = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "inicio.png")),
                IntervaloInicio = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntInicio.png")),
                IntervaloFim = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "IntFim.png")),
                Fim = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Fim.png")),
                Assinatura = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(path, "Assinatura.png"))
            };
        }

    }
}
