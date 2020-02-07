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

    class GeradorAleatorio : GeradorBase
    {
        private static readonly Random SRandom = new Random(100);
        private int soma = 0;

        private Padrao[] _conteudosAleatorios = null;

        private static int GerarMinutoAleatorio(int soma, int variacaoMin, int variacaoMax, bool inicioTrabalho)
        {
            if (inicioTrabalho) soma = -soma;
            int minutoNegativo = 0;
            int minutoPostivo = 0;


            if (soma < 0)
            {
                minutoPostivo = -soma + 5;
            }
            else if (soma > 0)
            {
                minutoNegativo = -soma - 5;
            }
            else
            {
                minutoNegativo = -variacaoMin;
                minutoPostivo = variacaoMax;
            }
            return SRandom.Next(minutoNegativo, minutoPostivo);
        }

        private TimeSpan GerarTimespanAleatorio(int hora, int variacaoMin, int variacaoMax, bool inicioTrabalho)
        {
            int minutos = GerarMinutoAleatorio(soma, variacaoMin, variacaoMax, inicioTrabalho);
            soma = inicioTrabalho ? soma - minutos : soma + minutos;
            return new TimeSpan(hora, minutos, 0);
        }

        private Padrao[] GeraConteudosAleatorios(GeradorArgs args)
        {


            var pathPadrao = base.GetPastaImagemAleatoria();

            var result = new List<Padrao>();
            int variacaoMin = args.MargemAtraso.MinimoMinutos;
            int variacaoMax = args.MargemAtraso.MaximoMinutos;
            for (int dia = 1; dia <= 31; dia++)
            {
                result.Add(new Padrao()
                {
                    Inicio = GeradorNumeros.FromTimeSpan(pathPadrao, GerarTimespanAleatorio(9, variacaoMin, variacaoMax, true)),
                    IntervaloInicio = GeradorNumeros.FromTimeSpan(pathPadrao, GerarTimespanAleatorio(12, variacaoMin, variacaoMax, false)),
                    IntervaloFim = GeradorNumeros.FromTimeSpan(pathPadrao, GerarTimespanAleatorio(14, variacaoMin, variacaoMax, true)),
                    Fim = GeradorNumeros.FromTimeSpan(pathPadrao, GerarTimespanAleatorio(19, variacaoMin, variacaoMax, false)),
                    Assinatura = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine(pathPadrao, "Assinatura.png"))
                });
            }

            return result.ToArray();
        }

        protected override void DoBeforePreencherTabelaHorarios(GeradorArgs args, Info info)
        {

            this._conteudosAleatorios = this.GeraConteudosAleatorios(args);
        }

        protected override Padrao DoGetConteudoLinha(GeradorArgs args, Info info, int numeroLinha)
        {
            return this._conteudosAleatorios[numeroLinha];
        }
    }
}
