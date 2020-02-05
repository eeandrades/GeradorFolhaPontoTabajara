namespace GeradorFolhaPontoTabajara
{
    partial class FrmGeradorFolhaPonto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeradorFolhaPonto));
            this.lblPastaEntrada = new System.Windows.Forms.Label();
            this.lblPastaSaida = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnSelectFolderEntrada = new System.Windows.Forms.Button();
            this.btnSelectFolderSaida = new System.Windows.Forms.Button();
            this.btnAbrirPastaEntrada = new System.Windows.Forms.Button();
            this.btnAbrirPastaSaida = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txbPastaSaida = new System.Windows.Forms.TextBox();
            this.txbPastaEntrada = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.successProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbbImplementacao = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.successProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPastaEntrada
            // 
            this.lblPastaEntrada.AutoSize = true;
            this.lblPastaEntrada.Location = new System.Drawing.Point(28, 22);
            this.lblPastaEntrada.Name = "lblPastaEntrada";
            this.lblPastaEntrada.Size = new System.Drawing.Size(88, 13);
            this.lblPastaEntrada.TabIndex = 0;
            this.lblPastaEntrada.Text = "Pasta de entrada";
            // 
            // lblPastaSaida
            // 
            this.lblPastaSaida.AutoSize = true;
            this.lblPastaSaida.Location = new System.Drawing.Point(28, 75);
            this.lblPastaSaida.Name = "lblPastaSaida";
            this.lblPastaSaida.Size = new System.Drawing.Size(79, 13);
            this.lblPastaSaida.TabIndex = 0;
            this.lblPastaSaida.Text = "Pasta de saída";
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(476, 161);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(75, 23);
            this.btnGerar.TabIndex = 2;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.BtnGerar_Click);
            // 
            // btnSelectFolderEntrada
            // 
            this.btnSelectFolderEntrada.Location = new System.Drawing.Point(476, 36);
            this.btnSelectFolderEntrada.Name = "btnSelectFolderEntrada";
            this.btnSelectFolderEntrada.Size = new System.Drawing.Size(34, 23);
            this.btnSelectFolderEntrada.TabIndex = 3;
            this.btnSelectFolderEntrada.TabStop = false;
            this.btnSelectFolderEntrada.Text = "...";
            this.toolTip1.SetToolTip(this.btnSelectFolderEntrada, "Selecionar Pasta");
            this.btnSelectFolderEntrada.UseVisualStyleBackColor = true;
            this.btnSelectFolderEntrada.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnSelectFolderSaida
            // 
            this.btnSelectFolderSaida.Location = new System.Drawing.Point(476, 88);
            this.btnSelectFolderSaida.Name = "btnSelectFolderSaida";
            this.btnSelectFolderSaida.Size = new System.Drawing.Size(34, 23);
            this.btnSelectFolderSaida.TabIndex = 3;
            this.btnSelectFolderSaida.TabStop = false;
            this.btnSelectFolderSaida.Text = "...";
            this.toolTip1.SetToolTip(this.btnSelectFolderSaida, "Selecionar Pasta");
            this.btnSelectFolderSaida.UseVisualStyleBackColor = true;
            this.btnSelectFolderSaida.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnAbrirPastaEntrada
            // 
            this.btnAbrirPastaEntrada.Location = new System.Drawing.Point(513, 36);
            this.btnAbrirPastaEntrada.Name = "btnAbrirPastaEntrada";
            this.btnAbrirPastaEntrada.Size = new System.Drawing.Size(34, 23);
            this.btnAbrirPastaEntrada.TabIndex = 3;
            this.btnAbrirPastaEntrada.TabStop = false;
            this.btnAbrirPastaEntrada.Text = ">";
            this.toolTip1.SetToolTip(this.btnAbrirPastaEntrada, "Exibir Pasta");
            this.btnAbrirPastaEntrada.UseVisualStyleBackColor = true;
            this.btnAbrirPastaEntrada.Click += new System.EventHandler(this.btnAbrirPasta_Click);
            // 
            // btnAbrirPastaSaida
            // 
            this.btnAbrirPastaSaida.Location = new System.Drawing.Point(513, 88);
            this.btnAbrirPastaSaida.Name = "btnAbrirPastaSaida";
            this.btnAbrirPastaSaida.Size = new System.Drawing.Size(34, 23);
            this.btnAbrirPastaSaida.TabIndex = 3;
            this.btnAbrirPastaSaida.TabStop = false;
            this.btnAbrirPastaSaida.Text = ">";
            this.toolTip1.SetToolTip(this.btnAbrirPastaSaida, "Exibir Pasta");
            this.btnAbrirPastaSaida.UseVisualStyleBackColor = true;
            this.btnAbrirPastaSaida.Click += new System.EventHandler(this.btnAbrirPasta_Click);
            // 
            // txbPastaSaida
            // 
            this.txbPastaSaida.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::GeradorFolhaPontoTabajara.Properties.Settings.Default, "pathSaida", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txbPastaSaida.Location = new System.Drawing.Point(31, 91);
            this.txbPastaSaida.Name = "txbPastaSaida";
            this.txbPastaSaida.Size = new System.Drawing.Size(439, 20);
            this.txbPastaSaida.TabIndex = 1;
            this.txbPastaSaida.Text = global::GeradorFolhaPontoTabajara.Properties.Settings.Default.pathSaida;
            // 
            // txbPastaEntrada
            // 
            this.txbPastaEntrada.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::GeradorFolhaPontoTabajara.Properties.Settings.Default, "pathEntrada", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txbPastaEntrada.Location = new System.Drawing.Point(31, 38);
            this.txbPastaEntrada.Name = "txbPastaEntrada";
            this.txbPastaEntrada.Size = new System.Drawing.Size(439, 20);
            this.txbPastaEntrada.TabIndex = 0;
            this.txbPastaEntrada.Text = global::GeradorFolhaPontoTabajara.Properties.Settings.Default.pathEntrada;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // successProvider
            // 
            this.successProvider.ContainerControl = this;
            this.successProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("successProvider.Icon")));
            // 
            // cbbImplementacao
            // 
            this.cbbImplementacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbImplementacao.FormattingEnabled = true;
            this.cbbImplementacao.Location = new System.Drawing.Point(31, 126);
            this.cbbImplementacao.Name = "cbbImplementacao";
            this.cbbImplementacao.Size = new System.Drawing.Size(516, 21);
            this.cbbImplementacao.TabIndex = 4;
            // 
            // FrmGeradorFolhaPonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 195);
            this.Controls.Add(this.cbbImplementacao);
            this.Controls.Add(this.btnAbrirPastaSaida);
            this.Controls.Add(this.btnSelectFolderSaida);
            this.Controls.Add(this.btnAbrirPastaEntrada);
            this.Controls.Add(this.btnSelectFolderEntrada);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txbPastaSaida);
            this.Controls.Add(this.lblPastaSaida);
            this.Controls.Add(this.txbPastaEntrada);
            this.Controls.Add(this.lblPastaEntrada);
            this.MaximizeBox = false;
            this.Name = "FrmGeradorFolhaPonto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de Folha de Ponto";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.successProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPastaEntrada;
        private System.Windows.Forms.TextBox txbPastaEntrada;
        private System.Windows.Forms.Label lblPastaSaida;
        private System.Windows.Forms.TextBox txbPastaSaida;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnSelectFolderEntrada;
        private System.Windows.Forms.Button btnSelectFolderSaida;
        private System.Windows.Forms.Button btnAbrirPastaEntrada;
        private System.Windows.Forms.Button btnAbrirPastaSaida;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ErrorProvider successProvider;
        private System.Windows.Forms.ComboBox cbbImplementacao;
    }
}

