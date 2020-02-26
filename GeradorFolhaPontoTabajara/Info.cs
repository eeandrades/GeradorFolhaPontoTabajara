using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public class Info
    {
        public Info(Bitmap folhaPonto, int numeroTotalDias, int diasUteis, int ultimoDiaUtil)
        {
            FolhaPonto = folhaPonto;
            NumeroTotalDias = numeroTotalDias;
            DiasUteis = diasUteis;
            UltimoDiaUtil = ultimoDiaUtil;
        }

        public Bitmap FolhaPonto { get; }
        public int NumeroTotalDias { get; }
        public int DiasUteis { get; }
        public int UltimoDiaUtil { get; }
    }
}
