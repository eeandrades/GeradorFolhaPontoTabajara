using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public interface ICalendario
    {
        DateTime UltimoDiaUtil(int ano, int mes);
    }
}
