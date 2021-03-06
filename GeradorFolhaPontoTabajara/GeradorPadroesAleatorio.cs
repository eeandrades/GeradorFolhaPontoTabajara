﻿using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeradorFolhaPontoTabajara
{
    /// <summary>
    /// Implementar ao gerador utilizando padrões de bitmpas com os intervalos de hora e assinatura
    /// </summary>

    class GeradorPadroesAleatorio : GeradorBase
    {


        private List<Padrao> _conteudos = null;

        private List<Padrao> CriaConteudosTabelaHorarioAleatorios()
        {
            var result = new List<Padrao>();

            int numeroPastas = base.PastasImagens.Length;
            Bitmap[] inicio = new Bitmap[numeroPastas];
            Bitmap[] intervaloInicio = new Bitmap[numeroPastas];
            Bitmap[] intervaloFim = new Bitmap[numeroPastas];
            Bitmap[] fim = new Bitmap[numeroPastas];
            Bitmap[] assinatura = new Bitmap[numeroPastas];

            

            //var path = System.IO.Path.Combine(pathPadrao);

            for (int i = 0; i < numeroPastas; i++)
            {
                var path = this.PastasImagens[i];
                inicio[i] = BitmapHelper.LoadIfExists(System.IO.Path.Combine(path, "inicio.png"));
                intervaloInicio[i] = BitmapHelper.LoadIfExists(System.IO.Path.Combine(path, "IntInicio.png"));
                intervaloFim[i] = BitmapHelper.LoadIfExists(System.IO.Path.Combine(path, "IntFim.png"));
                fim[i] = BitmapHelper.LoadIfExists(System.IO.Path.Combine(path, "Fim.png"));
                assinatura[i] = BitmapHelper.LoadIfExists(System.IO.Path.Combine(path, "Assinatura.png"));
            }

            Random rnd = new Random();

            rnd.Shuffle(inicio);

            rnd.Shuffle(intervaloInicio);

            rnd.Shuffle(intervaloFim);

            rnd.Shuffle(fim);

            rnd.Shuffle(assinatura);

            int indexAtual = 0;
            for (int dia = 1; dia <= 31; dia++)
            {
                if (indexAtual == numeroPastas)
                {
                    indexAtual = 0;
                    rnd.Shuffle(inicio);

                    rnd.Shuffle(intervaloInicio);

                    rnd.Shuffle(intervaloFim);

                    rnd.Shuffle(fim);

                    rnd.Shuffle(assinatura);
                }

                result.Add(new Padrao()
                {
                    Inicio = inicio[indexAtual],
                    IntervaloInicio = intervaloInicio[indexAtual],
                    IntervaloFim = intervaloFim[indexAtual],
                    Fim = fim[indexAtual],
                    Assinatura = assinatura[indexAtual]
                });

                indexAtual++;
            }

            return result;
        }

        protected override void DoBeforePreencherTabelaHorarios(GeradorArgs args, Info info)
        {
            this._conteudos = this.CriaConteudosTabelaHorarioAleatorios();
        }

        protected override Padrao DoGetConteudoLinha(GeradorArgs args, Info info, int numeroLinha)
        {
            return this._conteudos[numeroLinha];
        }
    }
}
