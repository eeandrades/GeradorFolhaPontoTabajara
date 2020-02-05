using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    class GeradorController
    {
        public GeradorController()
        {
        }

        public int Gerar(Type geradorType, string inputPath, string outputPath)
        {
            var pdfFiles = System.IO.Directory.GetFiles(inputPath, "*.pdf");

            foreach(var pdfFile in pdfFiles)
            {
                IGerador gerador = GeradorFactory.Create(geradorType);
                var pdfOutput = System.IO.Path.Combine(outputPath, System.IO.Path.GetFileName(pdfFile));
                gerador.Execute(pdfFile, pdfOutput);
            }

            return pdfFiles.Length;
        }

        public Type[] ListarGeradores()
        {
            return GeradorFactory.ListaTodasImplementacoes();
        }

        public IGerador CriarGerardor(Type tipoGerador)
        {
            return GeradorFactory.Create(tipoGerador);
        }
    }
}
