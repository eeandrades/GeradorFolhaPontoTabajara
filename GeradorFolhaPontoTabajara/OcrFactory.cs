using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    class OcrFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Type[] ListaTodasImplementacoes()
        {
            return System.Reflection.Assembly
                .GetExecutingAssembly()
                .DefinedTypes.Where(dt => !dt.IsAbstract && dt.ImplementedInterfaces.Contains(typeof(IOcr)))
                .ToArray();
        }

        public static IOcr Create(Type type)
        {
            return (IOcr)type.GetConstructor(new Type[0]).Invoke(new object[0]);
        }
    }
}
