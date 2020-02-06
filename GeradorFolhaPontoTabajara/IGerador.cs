using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public interface IGerador
    {
        void Execute(GeradorArgs args);
    }
}
