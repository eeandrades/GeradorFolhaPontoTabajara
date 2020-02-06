using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorFolhaPontoTabajara
{
    public partial class FrmGeradorFolhaPonto : Form
    {
        private GeradorController _controller = new GeradorController();

        private static readonly Color[] SCoresCaneta = { Color.Black, Color.Blue, Color.Red, Color.DarkGray, Color.Green};
        public FrmGeradorFolhaPonto()
        {
            InitializeComponent();

            var pathDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var pathEntrada = System.IO.Path.Combine(pathDocuments, "GFPT");
            var pathSaida = System.IO.Path.Combine(pathDocuments, "GFPT", "Saida");
            TryCreateDirecotry(pathEntrada);
            TryCreateDirecotry(pathSaida);

            this.txbPastaEntrada.Text = pathEntrada;
            this.txbPastaSaida.Text = pathSaida;

            this.btnSelectFolderEntrada.Tag = this.txbPastaEntrada;
            this.btnSelectFolderSaida.Tag = this.txbPastaSaida;

            this.btnAbrirPastaEntrada.Tag = this.txbPastaEntrada;
            this.btnAbrirPastaSaida.Tag = this.txbPastaSaida;

            this.cbbImplementacao.DisplayMember = nameof(Type.FullName);
            this.cbbImplementacao.DataSource = this._controller.ListarGeradores();


            this.cbbCorCaneta.DataSource = SCoresCaneta;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Properties.Settings.Default.Save();
        }

        private static void TryCreateDirecotry(string pathEntrada)
        {
            if (!System.IO.Directory.Exists(pathEntrada))
                System.IO.Directory.CreateDirectory(pathEntrada);
        }

        #region Validações

        private void HandleException(Exception ex, Control control)
        {
            this.errorProvider.SetError(control, ex.Message);
        }

        private void ValidarPath(TextBox txbPath, ref bool error)
        {
            if (!System.IO.Directory.Exists(txbPath.Text))
            {
                this.errorProvider.SetError(txbPath, "A pasta é inválida ou não existe.");
                error = true;
            }
        }

        private bool ValidarPath(TextBox txbPath)
        {
            var error = false;

            this.ValidarPath(txbPath, ref error);

            return !error;
        }
        #endregion
        private void ClearMessages()
        {
            this.successProvider.Clear();
            this.errorProvider.Clear();
        }

        private GeradorArgs CreateArgs()
        {
            return new GeradorArgs(
                (Color)this.cbbCorCaneta.SelectedItem,
                new Atraso(Convert.ToInt32(this.txbAtrasoMinimo.Text), Convert.ToInt32(this.txbAtrasoMaximo.Text)),
            this.txbPastaEntrada.Text,
            this.txbPastaSaida.Text);

        }

        #region eventos
        private void BtnGerar_Click(object sender, EventArgs e)
        {
            ClearMessages();
            var erro = false;

            this.ValidarPath(this.txbPastaEntrada, ref erro);
            this.ValidarPath(this.txbPastaSaida, ref erro);

            if (erro)
                return;
            try
            {
                var count = this._controller.Gerar((Type)this.cbbImplementacao.SelectedItem, this.CreateArgs());

                if (count > 0)
                {
                    this.successProvider.SetError(this.btnGerar, $"{count} folha(s) gerada(s) com sucesso!");
                    this.btnAbrirPastaSaida.PerformClick();
                }
                else
                    this.errorProvider.SetError(this.btnGerar, "A pasta de entrada está vazia. Cetifique-se que as folhas de ponto em formato pdf se encontram nela.");

            }
            catch (Exception ex)
            {
                this.HandleException(ex, this.btnGerar);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            ClearMessages();
            var txb = ((Control)sender).Tag as TextBox;

            using (FolderBrowserDialog fbg = new FolderBrowserDialog())
            {
                fbg.SelectedPath = txb.Text;
                if (fbg.ShowDialog() == DialogResult.OK)
                    txb.Text = fbg.SelectedPath;
            }
        }

        private void btnAbrirPasta_Click(object sender, EventArgs e)
        {
            ClearMessages();
            var txb = ((Control)sender).Tag as TextBox;
            try
            {
                if (ValidarPath(txb))
                    System.Diagnostics.Process.Start(txb.Text);
            }
            catch (Exception ex)
            {
                HandleException(ex, txb); ;
            }
        }
        #endregion
    }
}
