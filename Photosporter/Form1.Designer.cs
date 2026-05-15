namespace PhotoSorter
{
    partial class FrmHome
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.btIndirizzoCartella = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIndirizzo = new System.Windows.Forms.TextBox();
            this.pnNewDirectory = new System.Windows.Forms.Panel();
            this.tbNewDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckNewDirectory = new System.Windows.Forms.CheckBox();
            this.pnOrdinamento = new System.Windows.Forms.Panel();
            this.rdGiorno = new System.Windows.Forms.RadioButton();
            this.rdData = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.pnTypeDate = new System.Windows.Forms.Panel();
            this.rbDataAm = new System.Windows.Forms.RadioButton();
            this.rdDataEur = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.pnDays = new System.Windows.Forms.Panel();
            this.nudGiorni = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ckCancellaCartella = new System.Windows.Forms.CheckBox();
            this.ckCartelleDoppie = new System.Windows.Forms.CheckBox();
            this.btSmorta = new System.Windows.Forms.Button();
            this.pgSpostamentoFile = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCancellaDoppioni = new System.Windows.Forms.CheckBox();
            this.pnCancellaDoppioni = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.trbLivelloSoglia = new System.Windows.Forms.TrackBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnNewDirectory.SuspendLayout();
            this.pnOrdinamento.SuspendLayout();
            this.pnTypeDate.SuspendLayout();
            this.pnDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiorni)).BeginInit();
            this.pnCancellaDoppioni.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLivelloSoglia)).BeginInit();
            this.SuspendLayout();
            // 
            // btIndirizzoCartella
            // 
            this.btIndirizzoCartella.Location = new System.Drawing.Point(25, 60);
            this.btIndirizzoCartella.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btIndirizzoCartella.Name = "btIndirizzoCartella";
            this.btIndirizzoCartella.Size = new System.Drawing.Size(100, 33);
            this.btIndirizzoCartella.TabIndex = 0;
            this.btIndirizzoCartella.Text = "Sfoglia";
            this.btIndirizzoCartella.UseVisualStyleBackColor = true;
            this.btIndirizzoCartella.Click += new System.EventHandler(this.btIndirizzoCartella_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Inserire l\'indirizzo dove dividere i dati:";
            // 
            // tbIndirizzo
            // 
            this.tbIndirizzo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIndirizzo.Location = new System.Drawing.Point(146, 64);
            this.tbIndirizzo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbIndirizzo.Name = "tbIndirizzo";
            this.tbIndirizzo.ReadOnly = true;
            this.tbIndirizzo.Size = new System.Drawing.Size(660, 25);
            this.tbIndirizzo.TabIndex = 2;
            this.tbIndirizzo.TextChanged += new System.EventHandler(this.tbIndirizzo_TextChanged);
            // 
            // pnNewDirectory
            // 
            this.pnNewDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnNewDirectory.Controls.Add(this.tbNewDirectory);
            this.pnNewDirectory.Controls.Add(this.label2);
            this.pnNewDirectory.Location = new System.Drawing.Point(189, 97);
            this.pnNewDirectory.Name = "pnNewDirectory";
            this.pnNewDirectory.Size = new System.Drawing.Size(331, 36);
            this.pnNewDirectory.TabIndex = 3;
            this.pnNewDirectory.Visible = false;
            // 
            // tbNewDirectory
            // 
            this.tbNewDirectory.Location = new System.Drawing.Point(76, 5);
            this.tbNewDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbNewDirectory.Name = "tbNewDirectory";
            this.tbNewDirectory.Size = new System.Drawing.Size(208, 25);
            this.tbNewDirectory.TabIndex = 4;
            this.tbNewDirectory.TextChanged += new System.EventHandler(this.tbNewDirectory_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome:";
            // 
            // ckNewDirectory
            // 
            this.ckNewDirectory.AutoSize = true;
            this.ckNewDirectory.Enabled = false;
            this.ckNewDirectory.Location = new System.Drawing.Point(25, 104);
            this.ckNewDirectory.Name = "ckNewDirectory";
            this.ckNewDirectory.Size = new System.Drawing.Size(146, 23);
            this.ckNewDirectory.TabIndex = 0;
            this.ckNewDirectory.Text = "Crea nuova cartella";
            this.ckNewDirectory.UseVisualStyleBackColor = true;
            this.ckNewDirectory.CheckedChanged += new System.EventHandler(this.ckNewDirectory_CheckedChanged);
            // 
            // pnOrdinamento
            // 
            this.pnOrdinamento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnOrdinamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnOrdinamento.Controls.Add(this.rdGiorno);
            this.pnOrdinamento.Controls.Add(this.rdData);
            this.pnOrdinamento.Controls.Add(this.label3);
            this.pnOrdinamento.Location = new System.Drawing.Point(25, 186);
            this.pnOrdinamento.Name = "pnOrdinamento";
            this.pnOrdinamento.Size = new System.Drawing.Size(180, 118);
            this.pnOrdinamento.TabIndex = 4;
            // 
            // rdGiorno
            // 
            this.rdGiorno.AutoSize = true;
            this.rdGiorno.Location = new System.Drawing.Point(21, 63);
            this.rdGiorno.Name = "rdGiorno";
            this.rdGiorno.Size = new System.Drawing.Size(69, 23);
            this.rdGiorno.TabIndex = 7;
            this.rdGiorno.TabStop = true;
            this.rdGiorno.Text = "Giorno";
            this.rdGiorno.UseVisualStyleBackColor = true;
            this.rdGiorno.CheckedChanged += new System.EventHandler(this.rdGiorno_CheckedChanged_1);
            // 
            // rdData
            // 
            this.rdData.AutoSize = true;
            this.rdData.Location = new System.Drawing.Point(21, 34);
            this.rdData.Name = "rdData";
            this.rdData.Size = new System.Drawing.Size(56, 23);
            this.rdData.TabIndex = 6;
            this.rdData.TabStop = true;
            this.rdData.Text = "Data";
            this.rdData.UseVisualStyleBackColor = true;
            this.rdData.CheckedChanged += new System.EventHandler(this.rdData_CheckedChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ordina:";
            // 
            // pnTypeDate
            // 
            this.pnTypeDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTypeDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnTypeDate.Controls.Add(this.rbDataAm);
            this.pnTypeDate.Controls.Add(this.rdDataEur);
            this.pnTypeDate.Controls.Add(this.label4);
            this.pnTypeDate.Enabled = false;
            this.pnTypeDate.Location = new System.Drawing.Point(226, 186);
            this.pnTypeDate.Name = "pnTypeDate";
            this.pnTypeDate.Size = new System.Drawing.Size(180, 118);
            this.pnTypeDate.TabIndex = 5;
            // 
            // rbDataAm
            // 
            this.rbDataAm.AutoSize = true;
            this.rbDataAm.Location = new System.Drawing.Point(21, 63);
            this.rbDataAm.Name = "rbDataAm";
            this.rbDataAm.Size = new System.Drawing.Size(92, 23);
            this.rbDataAm.TabIndex = 7;
            this.rbDataAm.TabStop = true;
            this.rbDataAm.Text = "Americana";
            this.rbDataAm.UseVisualStyleBackColor = true;
            // 
            // rdDataEur
            // 
            this.rdDataEur.AutoSize = true;
            this.rdDataEur.Location = new System.Drawing.Point(21, 34);
            this.rdDataEur.Name = "rdDataEur";
            this.rdDataEur.Size = new System.Drawing.Size(77, 23);
            this.rdDataEur.TabIndex = 6;
            this.rdDataEur.TabStop = true;
            this.rdDataEur.Text = "Europea";
            this.rdDataEur.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tipo data:";
            // 
            // pnDays
            // 
            this.pnDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnDays.Controls.Add(this.nudGiorni);
            this.pnDays.Controls.Add(this.label5);
            this.pnDays.Enabled = false;
            this.pnDays.Location = new System.Drawing.Point(427, 186);
            this.pnDays.Name = "pnDays";
            this.pnDays.Size = new System.Drawing.Size(180, 118);
            this.pnDays.TabIndex = 6;
            // 
            // nudGiorni
            // 
            this.nudGiorni.Location = new System.Drawing.Point(21, 44);
            this.nudGiorni.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGiorni.Name = "nudGiorni";
            this.nudGiorni.Size = new System.Drawing.Size(120, 25);
            this.nudGiorni.TabIndex = 6;
            this.nudGiorni.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Dal Giorno N:";
            // 
            // ckCancellaCartella
            // 
            this.ckCancellaCartella.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckCancellaCartella.AutoSize = true;
            this.ckCancellaCartella.Location = new System.Drawing.Point(635, 199);
            this.ckCancellaCartella.Name = "ckCancellaCartella";
            this.ckCancellaCartella.Size = new System.Drawing.Size(191, 23);
            this.ckCancellaCartella.TabIndex = 7;
            this.ckCancellaCartella.Text = "Cancella le foto nel origine";
            this.ckCancellaCartella.UseVisualStyleBackColor = true;
            this.ckCancellaCartella.CheckedChanged += new System.EventHandler(this.ckCancellaCartella_CheckedChanged);
            // 
            // ckCartelleDoppie
            // 
            this.ckCartelleDoppie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckCartelleDoppie.AutoSize = true;
            this.ckCartelleDoppie.Location = new System.Drawing.Point(635, 228);
            this.ckCartelleDoppie.Name = "ckCartelleDoppie";
            this.ckCartelleDoppie.Size = new System.Drawing.Size(120, 23);
            this.ckCartelleDoppie.TabIndex = 8;
            this.ckCartelleDoppie.Text = "Cartelle doppie";
            this.ckCartelleDoppie.UseVisualStyleBackColor = true;
            // 
            // btSmorta
            // 
            this.btSmorta.Location = new System.Drawing.Point(25, 420);
            this.btSmorta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btSmorta.Name = "btSmorta";
            this.btSmorta.Size = new System.Drawing.Size(175, 33);
            this.btSmorta.TabIndex = 9;
            this.btSmorta.Text = "Avvia";
            this.btSmorta.UseVisualStyleBackColor = true;
            this.btSmorta.Click += new System.EventHandler(this.btSmorta_Click);
            // 
            // pgSpostamentoFile
            // 
            this.pgSpostamentoFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgSpostamentoFile.Location = new System.Drawing.Point(25, 471);
            this.pgSpostamentoFile.Name = "pgSpostamentoFile";
            this.pgSpostamentoFile.Size = new System.Drawing.Size(801, 31);
            this.pgSpostamentoFile.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "inserire come ordinare:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(630, 175);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "altre opzioni:";
            // 
            // cbCancellaDoppioni
            // 
            this.cbCancellaDoppioni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCancellaDoppioni.AutoSize = true;
            this.cbCancellaDoppioni.Location = new System.Drawing.Point(635, 257);
            this.cbCancellaDoppioni.Name = "cbCancellaDoppioni";
            this.cbCancellaDoppioni.Size = new System.Drawing.Size(137, 23);
            this.cbCancellaDoppioni.TabIndex = 13;
            this.cbCancellaDoppioni.Text = "Cancella doppioni";
            this.cbCancellaDoppioni.UseVisualStyleBackColor = true;
            this.cbCancellaDoppioni.CheckedChanged += new System.EventHandler(this.cbCancellaDoppioni_CheckedChanged);
            // 
            // pnCancellaDoppioni
            // 
            this.pnCancellaDoppioni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnCancellaDoppioni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnCancellaDoppioni.Controls.Add(this.label8);
            this.pnCancellaDoppioni.Controls.Add(this.trbLivelloSoglia);
            this.pnCancellaDoppioni.Enabled = false;
            this.pnCancellaDoppioni.Location = new System.Drawing.Point(25, 322);
            this.pnCancellaDoppioni.Name = "pnCancellaDoppioni";
            this.pnCancellaDoppioni.Size = new System.Drawing.Size(381, 72);
            this.pnCancellaDoppioni.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 19);
            this.label8.TabIndex = 15;
            this.label8.Text = "Livello di tolleranza doppioni:";
            // 
            // trbLivelloSoglia
            // 
            this.trbLivelloSoglia.LargeChange = 3;
            this.trbLivelloSoglia.Location = new System.Drawing.Point(200, 13);
            this.trbLivelloSoglia.Maximum = 3;
            this.trbLivelloSoglia.Minimum = 1;
            this.trbLivelloSoglia.Name = "trbLivelloSoglia";
            this.trbLivelloSoglia.Size = new System.Drawing.Size(167, 45);
            this.trbLivelloSoglia.TabIndex = 0;
            this.trbLivelloSoglia.Value = 1;
            this.trbLivelloSoglia.Scroll += new System.EventHandler(this.trbLivelloSoglia_Scroll);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 519);
            this.Controls.Add(this.pnCancellaDoppioni);
            this.Controls.Add(this.cbCancellaDoppioni);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pgSpostamentoFile);
            this.Controls.Add(this.btSmorta);
            this.Controls.Add(this.ckCartelleDoppie);
            this.Controls.Add(this.ckCancellaCartella);
            this.Controls.Add(this.pnDays);
            this.Controls.Add(this.pnTypeDate);
            this.Controls.Add(this.pnOrdinamento);
            this.Controls.Add(this.ckNewDirectory);
            this.Controls.Add(this.pnNewDirectory);
            this.Controls.Add(this.tbIndirizzo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btIndirizzoCartella);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmHome";
            this.Text = "Photosporter";
            //this.Load += new System.EventHandler(this.FrmHome_Load);
            this.pnNewDirectory.ResumeLayout(false);
            this.pnNewDirectory.PerformLayout();
            this.pnOrdinamento.ResumeLayout(false);
            this.pnOrdinamento.PerformLayout();
            this.pnTypeDate.ResumeLayout(false);
            this.pnTypeDate.PerformLayout();
            this.pnDays.ResumeLayout(false);
            this.pnDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiorni)).EndInit();
            this.pnCancellaDoppioni.ResumeLayout(false);
            this.pnCancellaDoppioni.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLivelloSoglia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btIndirizzoCartella;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIndirizzo;
        private System.Windows.Forms.Panel pnNewDirectory;
        private System.Windows.Forms.TextBox tbNewDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckNewDirectory;
        private System.Windows.Forms.Panel pnOrdinamento;
        private System.Windows.Forms.RadioButton rdGiorno;
        private System.Windows.Forms.RadioButton rdData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnTypeDate;
        private System.Windows.Forms.RadioButton rbDataAm;
        private System.Windows.Forms.RadioButton rdDataEur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnDays;
        private System.Windows.Forms.NumericUpDown nudGiorni;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckCancellaCartella;
        private System.Windows.Forms.CheckBox ckCartelleDoppie;
        private System.Windows.Forms.Button btSmorta;
        private System.Windows.Forms.ProgressBar pgSpostamentoFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbCancellaDoppioni;
        private System.Windows.Forms.Panel pnCancellaDoppioni;
        private System.Windows.Forms.TrackBar trbLivelloSoglia;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label8;
    }
}

