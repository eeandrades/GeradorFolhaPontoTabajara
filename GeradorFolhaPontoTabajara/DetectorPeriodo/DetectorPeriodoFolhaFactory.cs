using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    class DetectorPeriodoFolhaFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Type[] ListaTodasImplementacoes()
        {
            return System.Reflection.Assembly
                .GetExecutingAssembly()
                .DefinedTypes.Where(dt => !dt.IsAbstract && dt.ImplementedInterfaces.Contains(typeof(IDetectorPeriodoFolha)))
                .ToArray();
        }

        public static IDetectorPeriodoFolha Create(Type type)
        {
            return (IDetectorPeriodoFolha)type.GetConstructor(new Type[0]).Invoke(new object[0]);
        }
    }
}
