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

    class GeradorImplementacaoPadroes : GeradorImplementacaoPadroesBase
    {
        protected override Color DoGetCorCaneta()
        {
            return Color.Blue;
        }
    }

    class GeradorImplementacaoPadroesVermelho : GeradorImplementacaoPadroesBase
    {
        protected override Color DoGetCorCaneta()
        {
            return Color.Red;
        }
    }

    class GeradorImplementacaoPadroesPreto : GeradorImplementacaoPadroesBase
    {
        protected override Color DoGetCorCaneta()
        {
            return ColorTranslator.FromHtml("#141414");
        }
    }
}
