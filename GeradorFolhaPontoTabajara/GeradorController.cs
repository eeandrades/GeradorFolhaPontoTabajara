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

        public int Gerar(Type geradorType, GeradorArgs args)
        {
            var pdfFiles = System.IO.Directory.GetFiles(args.PdfSourcePath, "*.pdf");

            foreach(var pdfFile in pdfFiles)
            {
                IGerador gerador = GeradorFactory.Create(geradorType);
                var pdfOutput = System.IO.Path.Combine(args.PdfDestinationPath, System.IO.Path.GetFileName(pdfFile));
                gerador.Execute(new GeradorArgs(args.CorCaneta, args.MargemAtraso, pdfFile, pdfOutput));
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
