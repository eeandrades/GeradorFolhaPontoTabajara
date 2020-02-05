using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    public static class GeradorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Type[] ListaTodasImplementacoes()
        {
            return System.Reflection.Assembly
                .GetExecutingAssembly()
                .DefinedTypes.Where(dt => !dt.IsAbstract &&  dt.ImplementedInterfaces.Contains(typeof(IGerador)))
                .ToArray();
        }

        public static IGerador Create(Type type)
        {
           return (IGerador) type.GetConstructor(new Type[0]).Invoke(new object[0]);
        }
    }
}
