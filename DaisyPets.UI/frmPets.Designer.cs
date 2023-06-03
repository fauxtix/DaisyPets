﻿namespace DaisyPets.UI
{
    partial class frmPets
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            gdvDados = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nome = new DataGridViewTextBoxColumn();
            Chipado = new DataGridViewCheckBoxColumn();
            Esterilizado = new DataGridViewCheckBoxColumn();
            SituacaoAnimal = new DataGridViewTextBoxColumn();
            EspecieAnimal = new DataGridViewTextBoxColumn();
            RacaAnimal = new DataGridViewTextBoxColumn();
            TamanhoAnimal = new DataGridViewTextBoxColumn();
            HistoryInfo = new DataGridViewButtonColumn();
            btnInsert = new Button();
            panel1 = new Panel();
            chkChipado = new CheckBox();
            txtNumeroChip = new TextBox();
            dtpChip = new DateTimePicker();
            txtMedicacao = new TextBox();
            label5 = new Label();
            nudPeso = new NumericUpDown();
            lblPorte = new Label();
            txtPorte = new TextBox();
            txtTipoCao = new TextBox();
            cboTamanho = new ComboBox();
            label4 = new Label();
            dtpDataNascimento = new DateTimePicker();
            pbPhoto = new PictureBox();
            btnUploadPhoto = new Button();
            txtId = new TextBox();
            chkPadrinho = new CheckBox();
            chkEsterilizado = new CheckBox();
            cboTemperamento = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            cboSituacao = new ComboBox();
            label19 = new Label();
            cboRaca = new ComboBox();
            label18 = new Label();
            label16 = new Label();
            cboEspecies = new ComboBox();
            label15 = new Label();
            txtObservacoes = new TextBox();
            label14 = new Label();
            txtDoencaCronica = new TextBox();
            txtChip = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label9 = new Label();
            txtFoto = new TextBox();
            txtCor = new TextBox();
            label1 = new Label();
            txtIdade = new TextBox();
            txtNome = new TextBox();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            btnUpdate = new Button();
            btnDelete = new Button();
            openFileDialog1 = new OpenFileDialog();
            btnClear = new Button();
            btnGeneratePdf = new Button();
            ((System.ComponentModel.ISupportInitialize)gdvDados).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPhoto).BeginInit();
            SuspendLayout();
            // 
            // gdvDados
            // 
            gdvDados.AllowUserToAddRows = false;
            gdvDados.AllowUserToDeleteRows = false;
            gdvDados.AllowUserToOrderColumns = true;
            gdvDados.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            gdvDados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            gdvDados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gdvDados.ColumnHeadersHeight = 40;
            gdvDados.Columns.AddRange(new DataGridViewColumn[] { Id, Nome, Chipado, Esterilizado, SituacaoAnimal, EspecieAnimal, RacaAnimal, TamanhoAnimal, HistoryInfo });
            gdvDados.Location = new Point(33, 473);
            gdvDados.MultiSelect = false;
            gdvDados.Name = "gdvDados";
            gdvDados.ReadOnly = true;
            gdvDados.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gdvDados.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gdvDados.RowTemplate.Height = 32;
            gdvDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gdvDados.Size = new Size(1155, 221);
            gdvDados.TabIndex = 17;
            gdvDados.CellClick += gdvDados_CellClick;
            gdvDados.CellContentClick += gdvDados_CellContentClick;
            gdvDados.ColumnHeaderMouseClick += gdvDados_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            Id.Width = 50;
            // 
            // Nome
            // 
            Nome.DataPropertyName = "Nome";
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Nome.DefaultCellStyle = dataGridViewCellStyle3;
            Nome.HeaderText = "Nome";
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            Nome.SortMode = DataGridViewColumnSortMode.Programmatic;
            Nome.Width = 200;
            // 
            // Chipado
            // 
            Chipado.DataPropertyName = "Chipado";
            Chipado.HeaderText = "Chip";
            Chipado.Name = "Chipado";
            Chipado.ReadOnly = true;
            Chipado.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Esterilizado
            // 
            Esterilizado.DataPropertyName = "Esterilizado";
            Esterilizado.HeaderText = "Esterilizado";
            Esterilizado.Name = "Esterilizado";
            Esterilizado.ReadOnly = true;
            Esterilizado.Resizable = DataGridViewTriState.True;
            Esterilizado.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // SituacaoAnimal
            // 
            SituacaoAnimal.DataPropertyName = "SituacaoAnimal";
            SituacaoAnimal.HeaderText = "Situação";
            SituacaoAnimal.Name = "SituacaoAnimal";
            SituacaoAnimal.ReadOnly = true;
            SituacaoAnimal.Width = 200;
            // 
            // EspecieAnimal
            // 
            EspecieAnimal.DataPropertyName = "EspecieAnimal";
            EspecieAnimal.HeaderText = "Espécie";
            EspecieAnimal.Name = "EspecieAnimal";
            EspecieAnimal.ReadOnly = true;
            EspecieAnimal.Width = 80;
            // 
            // RacaAnimal
            // 
            RacaAnimal.DataPropertyName = "RacaAnimal";
            RacaAnimal.HeaderText = "Raça";
            RacaAnimal.Name = "RacaAnimal";
            RacaAnimal.ReadOnly = true;
            RacaAnimal.Width = 250;
            // 
            // TamanhoAnimal
            // 
            TamanhoAnimal.DataPropertyName = "TamanhoAnimal";
            TamanhoAnimal.HeaderText = "Tamanho";
            TamanhoAnimal.Name = "TamanhoAnimal";
            TamanhoAnimal.ReadOnly = true;
            TamanhoAnimal.Width = 80;
            // 
            // HistoryInfo
            // 
            HistoryInfo.HeaderText = "";
            HistoryInfo.Name = "HistoryInfo";
            HistoryInfo.ReadOnly = true;
            HistoryInfo.Text = "History";
            HistoryInfo.UseColumnTextForButtonValue = true;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(33, 419);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 18;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(chkChipado);
            panel1.Controls.Add(txtNumeroChip);
            panel1.Controls.Add(dtpChip);
            panel1.Controls.Add(txtMedicacao);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(nudPeso);
            panel1.Controls.Add(lblPorte);
            panel1.Controls.Add(txtPorte);
            panel1.Controls.Add(txtTipoCao);
            panel1.Controls.Add(cboTamanho);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(dtpDataNascimento);
            panel1.Controls.Add(pbPhoto);
            panel1.Controls.Add(btnUploadPhoto);
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(chkPadrinho);
            panel1.Controls.Add(chkEsterilizado);
            panel1.Controls.Add(cboTemperamento);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cboSituacao);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(cboRaca);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(cboEspecies);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(txtObservacoes);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(txtDoencaCronica);
            panel1.Controls.Add(txtChip);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(txtFoto);
            panel1.Controls.Add(txtCor);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtIdade);
            panel1.Controls.Add(txtNome);
            panel1.Location = new Point(28, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(1160, 399);
            panel1.TabIndex = 4;
            // 
            // chkChipado
            // 
            chkChipado.AutoSize = true;
            chkChipado.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            chkChipado.Location = new Point(12, 225);
            chkChipado.Name = "chkChipado";
            chkChipado.Size = new Size(84, 24);
            chkChipado.TabIndex = 10;
            chkChipado.Text = "Chipado";
            chkChipado.UseVisualStyleBackColor = true;
            chkChipado.Click += chkChipado_Click;
            // 
            // txtNumeroChip
            // 
            txtNumeroChip.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtNumeroChip.Location = new Point(312, 223);
            txtNumeroChip.MaxLength = 15;
            txtNumeroChip.Name = "txtNumeroChip";
            txtNumeroChip.Size = new Size(132, 27);
            txtNumeroChip.TabIndex = 12;
            txtNumeroChip.Visible = false;
            // 
            // dtpChip
            // 
            dtpChip.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpChip.Format = DateTimePickerFormat.Short;
            dtpChip.Location = new Point(189, 223);
            dtpChip.Name = "dtpChip";
            dtpChip.Size = new Size(108, 27);
            dtpChip.TabIndex = 11;
            dtpChip.Visible = false;
            // 
            // txtMedicacao
            // 
            txtMedicacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMedicacao.Location = new Point(488, 174);
            txtMedicacao.Multiline = true;
            txtMedicacao.Name = "txtMedicacao";
            txtMedicacao.ScrollBars = ScrollBars.Vertical;
            txtMedicacao.Size = new Size(345, 61);
            txtMedicacao.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(505, 14);
            label5.Name = "label5";
            label5.Size = new Size(47, 20);
            label5.TabIndex = 87;
            label5.Text = "Idade";
            // 
            // nudPeso
            // 
            nudPeso.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            nudPeso.Location = new Point(610, 37);
            nudPeso.Maximum = new decimal(new int[] { 45, 0, 0, 0 });
            nudPeso.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPeso.Name = "nudPeso";
            nudPeso.Size = new Size(55, 27);
            nudPeso.TabIndex = 3;
            nudPeso.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudPeso.ValueChanged += nudPeso_ValueChanged;
            // 
            // lblPorte
            // 
            lblPorte.AutoSize = true;
            lblPorte.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblPorte.Location = new Point(685, 14);
            lblPorte.Name = "lblPorte";
            lblPorte.Size = new Size(69, 20);
            lblPorte.TabIndex = 85;
            lblPorte.Text = "Tamanho";
            // 
            // txtPorte
            // 
            txtPorte.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPorte.Location = new Point(685, 37);
            txtPorte.Name = "txtPorte";
            txtPorte.ReadOnly = true;
            txtPorte.Size = new Size(114, 27);
            txtPorte.TabIndex = 84;
            // 
            // txtTipoCao
            // 
            txtTipoCao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtTipoCao.Location = new Point(505, 36);
            txtTipoCao.Name = "txtTipoCao";
            txtTipoCao.ReadOnly = true;
            txtTipoCao.Size = new Size(85, 27);
            txtTipoCao.TabIndex = 82;
            // 
            // cboTamanho
            // 
            cboTamanho.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboTamanho.FormattingEnabled = true;
            cboTamanho.Location = new Point(256, 37);
            cboTamanho.Name = "cboTamanho";
            cboTamanho.Size = new Size(101, 28);
            cboTamanho.TabIndex = 1;
            cboTamanho.SelectedIndexChanged += cboTamanho_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(257, 14);
            label4.Name = "label4";
            label4.Size = new Size(43, 20);
            label4.TabIndex = 80;
            label4.Text = "Porte";
            // 
            // dtpDataNascimento
            // 
            dtpDataNascimento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpDataNascimento.Format = DateTimePickerFormat.Short;
            dtpDataNascimento.Location = new Point(377, 37);
            dtpDataNascimento.Name = "dtpDataNascimento";
            dtpDataNascimento.Size = new Size(108, 27);
            dtpDataNascimento.TabIndex = 2;
            dtpDataNascimento.ValueChanged += dtpDataNascimento_ValueChanged;
            // 
            // pbPhoto
            // 
            pbPhoto.BorderStyle = BorderStyle.FixedSingle;
            pbPhoto.Image = Properties.Resources.NoImageAvailable;
            pbPhoto.Location = new Point(874, 14);
            pbPhoto.Name = "pbPhoto";
            pbPhoto.Size = new Size(273, 222);
            pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPhoto.TabIndex = 78;
            pbPhoto.TabStop = false;
            // 
            // btnUploadPhoto
            // 
            btnUploadPhoto.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUploadPhoto.Location = new Point(933, 242);
            btnUploadPhoto.Name = "btnUploadPhoto";
            btnUploadPhoto.Size = new Size(128, 31);
            btnUploadPhoto.TabIndex = 16;
            btnUploadPhoto.Text = "Upload photo";
            btnUploadPhoto.UseVisualStyleBackColor = true;
            btnUploadPhoto.Click += btnUploadPhoto_Click;
            // 
            // txtId
            // 
            txtId.Location = new Point(1066, 281);
            txtId.Name = "txtId";
            txtId.Size = new Size(65, 23);
            txtId.TabIndex = 75;
            txtId.Visible = false;
            // 
            // chkPadrinho
            // 
            chkPadrinho.AutoSize = true;
            chkPadrinho.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            chkPadrinho.Location = new Point(747, 106);
            chkPadrinho.Name = "chkPadrinho";
            chkPadrinho.Size = new Size(86, 24);
            chkPadrinho.TabIndex = 14;
            chkPadrinho.Text = "Padrinho";
            chkPadrinho.UseVisualStyleBackColor = true;
            // 
            // chkEsterilizado
            // 
            chkEsterilizado.AutoSize = true;
            chkEsterilizado.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            chkEsterilizado.Location = new Point(626, 106);
            chkEsterilizado.Name = "chkEsterilizado";
            chkEsterilizado.Size = new Size(105, 24);
            chkEsterilizado.TabIndex = 13;
            chkEsterilizado.Text = "Esterilizado";
            chkEsterilizado.UseVisualStyleBackColor = true;
            // 
            // cboTemperamento
            // 
            cboTemperamento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboTemperamento.FormattingEnabled = true;
            cboTemperamento.Location = new Point(189, 174);
            cboTemperamento.Name = "cboTemperamento";
            cboTemperamento.Size = new Size(284, 28);
            cboTemperamento.TabIndex = 8;
            cboTemperamento.SelectedIndexChanged += cboTemperamento_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(189, 151);
            label3.Name = "label3";
            label3.Size = new Size(110, 20);
            label3.TabIndex = 71;
            label3.Text = "Temperamento";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(378, 14);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 70;
            label2.Text = "Dt. Nascimento";
            // 
            // cboSituacao
            // 
            cboSituacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboSituacao.FormattingEnabled = true;
            cboSituacao.Location = new Point(12, 174);
            cboSituacao.Name = "cboSituacao";
            cboSituacao.Size = new Size(165, 28);
            cboSituacao.TabIndex = 7;
            cboSituacao.SelectedIndexChanged += cboSituacao_SelectedIndexChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(12, 151);
            label19.Name = "label19";
            label19.Size = new Size(66, 20);
            label19.TabIndex = 68;
            label19.Text = "Situação";
            // 
            // cboRaca
            // 
            cboRaca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboRaca.FormattingEnabled = true;
            cboRaca.Location = new Point(105, 106);
            cboRaca.Name = "cboRaca";
            cboRaca.Size = new Size(252, 28);
            cboRaca.TabIndex = 6;
            cboRaca.SelectedIndexChanged += cboRaca_SelectedIndexChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(105, 81);
            label18.Name = "label18";
            label18.Size = new Size(41, 20);
            label18.TabIndex = 66;
            label18.Text = "Raça";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(12, 81);
            label16.Name = "label16";
            label16.Size = new Size(59, 20);
            label16.TabIndex = 64;
            label16.Text = "Espécie";
            // 
            // cboEspecies
            // 
            cboEspecies.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboEspecies.FormattingEnabled = true;
            cboEspecies.Location = new Point(12, 106);
            cboEspecies.Name = "cboEspecies";
            cboEspecies.Size = new Size(76, 28);
            cboEspecies.TabIndex = 5;
            cboEspecies.SelectedIndexChanged += cboEspecies_SelectedIndexChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(488, 263);
            label15.Name = "label15";
            label15.Size = new Size(93, 20);
            label15.TabIndex = 62;
            label15.Text = "Observações";
            // 
            // txtObservacoes
            // 
            txtObservacoes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtObservacoes.Location = new Point(488, 286);
            txtObservacoes.Multiline = true;
            txtObservacoes.Name = "txtObservacoes";
            txtObservacoes.ScrollBars = ScrollBars.Vertical;
            txtObservacoes.Size = new Size(345, 110);
            txtObservacoes.TabIndex = 16;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(7, 263);
            label14.Name = "label14";
            label14.Size = new Size(146, 20);
            label14.TabIndex = 60;
            label14.Text = "Doença(s) Crónica(s)";
            // 
            // txtDoencaCronica
            // 
            txtDoencaCronica.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtDoencaCronica.Location = new Point(7, 287);
            txtDoencaCronica.Multiline = true;
            txtDoencaCronica.Name = "txtDoencaCronica";
            txtDoencaCronica.Size = new Size(466, 50);
            txtDoencaCronica.TabIndex = 15;
            // 
            // txtChip
            // 
            txtChip.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtChip.Location = new Point(1028, 310);
            txtChip.Name = "txtChip";
            txtChip.Size = new Size(103, 27);
            txtChip.TabIndex = 57;
            txtChip.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(377, 81);
            label6.Name = "label6";
            label6.Size = new Size(32, 20);
            label6.TabIndex = 56;
            label6.Text = "Cor";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(610, 14);
            label7.Name = "label7";
            label7.Size = new Size(39, 20);
            label7.TabIndex = 55;
            label7.Text = "Peso";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(488, 151);
            label9.Name = "label9";
            label9.Size = new Size(82, 20);
            label9.TabIndex = 54;
            label9.Text = "Medicação";
            // 
            // txtFoto
            // 
            txtFoto.Location = new Point(995, 281);
            txtFoto.Name = "txtFoto";
            txtFoto.Size = new Size(40, 23);
            txtFoto.TabIndex = 51;
            txtFoto.Visible = false;
            // 
            // txtCor
            // 
            txtCor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtCor.Location = new Point(377, 107);
            txtCor.MaxLength = 40;
            txtCor.Name = "txtCor";
            txtCor.Size = new Size(213, 27);
            txtCor.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 49;
            label1.Text = "Nome";
            // 
            // txtIdade
            // 
            txtIdade.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtIdade.Location = new Point(923, 281);
            txtIdade.Name = "txtIdade";
            txtIdade.Size = new Size(49, 27);
            txtIdade.TabIndex = 48;
            txtIdade.TextAlign = HorizontalAlignment.Center;
            txtIdade.Visible = false;
            // 
            // txtNome
            // 
            txtNome.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtNome.Location = new Point(12, 37);
            txtNome.MaxLength = 40;
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(227, 27);
            txtNome.TabIndex = 0;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.ForestGreen;
            btnUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.save32;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(157, 419);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 19;
            btnUpdate.Text = "Update ";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Red;
            btnDelete.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources._678080_shield_error_32;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(281, 419);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "Delete ";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(500, 419);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 21;
            btnClear.Text = "Clear ";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnGeneratePdf
            // 
            btnGeneratePdf.BackColor = Color.WhiteSmoke;
            btnGeneratePdf.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGeneratePdf.ForeColor = Color.Black;
            btnGeneratePdf.Image = Properties.Resources.Adobe_PDF_Document_icon;
            btnGeneratePdf.ImageAlign = ContentAlignment.MiddleLeft;
            btnGeneratePdf.Location = new Point(852, 419);
            btnGeneratePdf.Name = "btnGeneratePdf";
            btnGeneratePdf.Size = new Size(98, 39);
            btnGeneratePdf.TabIndex = 22;
            btnGeneratePdf.Text = "Pdf  ";
            btnGeneratePdf.TextAlign = ContentAlignment.MiddleRight;
            btnGeneratePdf.UseVisualStyleBackColor = false;
            btnGeneratePdf.Click += btnGeneratePdf_Click;
            // 
            // frmPets
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.SteelBlue;
            CaptionBarHeight = 100;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.catsanddogs;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(128, 96);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(160, 12);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Size = new Size(300, 24);
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(160, 50);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(250, 22);
            captionLabel2.Text = "Gestão da base de dados";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1234, 694);
            Controls.Add(btnGeneratePdf);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(panel1);
            Controls.Add(btnInsert);
            Controls.Add(gdvDados);
            Name = "frmPets";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)gdvDados).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPhoto).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView gdvDados;
        private Button btnGetData;
        private Button btnInsert;
        private Panel panel1;
        private CheckBox chkPadrinho;
        private CheckBox chkEsterilizado;
        private ComboBox cboTemperamento;
        private Label label3;
        private Label label2;
        private ComboBox cboSituacao;
        private Label label19;
        private ComboBox cboRaca;
        private Label label18;
        private Label label16;
        private ComboBox cboEspecies;
        private Label label15;
        private TextBox txtObservacoes;
        private Label label14;
        private TextBox txtDoencaCronica;
        private TextBox txtChip;
        private Label label6;
        private Label label7;
        private Label label9;
        private TextBox txtFoto;
        private TextBox txtCor;
        private Label label1;
        private TextBox txtIdade;
        private TextBox txtNome;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Button btnUpdate;
        private Button btnDelete;
        private TextBox txtId;
        private Button btnUploadPhoto;
        private OpenFileDialog openFileDialog1;
        private PictureBox pbPhoto;
        private DateTimePicker dtpDataNascimento;
        private ComboBox cboTamanho;
        private Label label4;
        private TextBox txtTipoCao;
        private Button btnClear;
        private Label lblPorte;
        private TextBox txtPorte;
        private NumericUpDown nudPeso;
        private Label label5;
        private TextBox txtMedicacao;
        private TextBox txtNumeroChip;
        private DateTimePicker dtpChip;
        private CheckBox chkChipado;
        private Button btnGeneratePdf;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewCheckBoxColumn Chipado;
        private DataGridViewCheckBoxColumn Esterilizado;
        private DataGridViewTextBoxColumn SituacaoAnimal;
        private DataGridViewTextBoxColumn EspecieAnimal;
        private DataGridViewTextBoxColumn RacaAnimal;
        private DataGridViewTextBoxColumn TamanhoAnimal;
        private DataGridViewButtonColumn HistoryInfo;
    }
}