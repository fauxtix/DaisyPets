namespace DaisyPets.UI
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPets));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            gdvDados = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nome = new DataGridViewTextBoxColumn();
            EspecieAnimal = new DataGridViewTextBoxColumn();
            Chipado = new DataGridViewCheckBoxColumn();
            Esterilizado = new DataGridViewCheckBoxColumn();
            SituacaoAnimal = new DataGridViewTextBoxColumn();
            TemperamentoAnimal = new DataGridViewTextBoxColumn();
            RacaAnimal = new DataGridViewTextBoxColumn();
            TamanhoAnimal = new DataGridViewTextBoxColumn();
            btnInsert = new Button();
            panel1 = new Panel();
            cboGenero = new ComboBox();
            label8 = new Label();
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
            btnVaccines = new Button();
            panel2 = new Panel();
            btnDocs = new Button();
            btnDewormers = new Button();
            btnDogFood = new Button();
            btnAppts = new Button();
            ((System.ComponentModel.ISupportInitialize)gdvDados).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPhoto).BeginInit();
            panel2.SuspendLayout();
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
            gdvDados.Columns.AddRange(new DataGridViewColumn[] { Id, Nome, EspecieAnimal, Chipado, Esterilizado, SituacaoAnimal, TemperamentoAnimal, RacaAnimal, TamanhoAnimal });
            gdvDados.Location = new Point(28, 472);
            gdvDados.MultiSelect = false;
            gdvDados.Name = "gdvDados";
            gdvDados.ReadOnly = true;
            gdvDados.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gdvDados.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            gdvDados.RowTemplate.Height = 32;
            gdvDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gdvDados.Size = new Size(1145, 210);
            gdvDados.TabIndex = 17;
            gdvDados.CellClick += gdvDados_CellClick;
            gdvDados.CellContentClick += gdvDados_CellContentClick;
            gdvDados.ColumnHeaderMouseClick += gdvDados_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            Id.DefaultCellStyle = dataGridViewCellStyle3;
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            Id.Width = 60;
            // 
            // Nome
            // 
            Nome.DataPropertyName = "Nome";
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Nome.DefaultCellStyle = dataGridViewCellStyle4;
            Nome.HeaderText = "Nome";
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            Nome.SortMode = DataGridViewColumnSortMode.Programmatic;
            Nome.Width = 200;
            // 
            // EspecieAnimal
            // 
            EspecieAnimal.DataPropertyName = "EspecieAnimal";
            EspecieAnimal.HeaderText = "Espécie";
            EspecieAnimal.Name = "EspecieAnimal";
            EspecieAnimal.ReadOnly = true;
            EspecieAnimal.Visible = false;
            EspecieAnimal.Width = 80;
            // 
            // Chipado
            // 
            Chipado.DataPropertyName = "Chipado";
            Chipado.HeaderText = "Chip";
            Chipado.Name = "Chipado";
            Chipado.ReadOnly = true;
            Chipado.SortMode = DataGridViewColumnSortMode.Automatic;
            Chipado.Width = 60;
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
            // TemperamentoAnimal
            // 
            TemperamentoAnimal.DataPropertyName = "TemperamentoAnimal";
            TemperamentoAnimal.HeaderText = "Temperamento";
            TemperamentoAnimal.Name = "TemperamentoAnimal";
            TemperamentoAnimal.ReadOnly = true;
            TemperamentoAnimal.Width = 200;
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
            TamanhoAnimal.Width = 90;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.FlatStyle = FlatStyle.Flat;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(33, 419);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(108, 39);
            btnInsert.TabIndex = 18;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(cboGenero);
            panel1.Controls.Add(label8);
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
            panel1.Size = new Size(1145, 392);
            panel1.TabIndex = 4;
            // 
            // cboGenero
            // 
            cboGenero.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboGenero.FormattingEnabled = true;
            cboGenero.Items.AddRange(new object[] { "Macho", "Fêmea" });
            cboGenero.Location = new Point(106, 106);
            cboGenero.Name = "cboGenero";
            cboGenero.Size = new Size(111, 28);
            cboGenero.TabIndex = 6;
            cboGenero.SelectedIndexChanged += cboGenero_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(106, 81);
            label8.Name = "label8";
            label8.Size = new Size(57, 20);
            label8.TabIndex = 89;
            label8.Text = "Género";
            // 
            // chkChipado
            // 
            chkChipado.AutoSize = true;
            chkChipado.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            chkChipado.Location = new Point(12, 225);
            chkChipado.Name = "chkChipado";
            chkChipado.Size = new Size(84, 24);
            chkChipado.TabIndex = 12;
            chkChipado.Text = "Chipado";
            chkChipado.UseVisualStyleBackColor = true;
            chkChipado.Click += chkChipado_Click;
            // 
            // txtNumeroChip
            // 
            txtNumeroChip.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtNumeroChip.Location = new Point(235, 221);
            txtNumeroChip.MaxLength = 15;
            txtNumeroChip.Name = "txtNumeroChip";
            txtNumeroChip.Size = new Size(235, 27);
            txtNumeroChip.TabIndex = 14;
            txtNumeroChip.Visible = false;
            // 
            // dtpChip
            // 
            dtpChip.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpChip.Format = DateTimePickerFormat.Short;
            dtpChip.Location = new Point(109, 221);
            dtpChip.Name = "dtpChip";
            dtpChip.Size = new Size(108, 27);
            dtpChip.TabIndex = 13;
            dtpChip.Visible = false;
            // 
            // txtMedicacao
            // 
            txtMedicacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMedicacao.Location = new Point(488, 174);
            txtMedicacao.Multiline = true;
            txtMedicacao.Name = "txtMedicacao";
            txtMedicacao.ScrollBars = ScrollBars.Vertical;
            txtMedicacao.Size = new Size(358, 61);
            txtMedicacao.TabIndex = 11;
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
            lblPorte.Visible = false;
            // 
            // txtPorte
            // 
            txtPorte.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPorte.Location = new Point(685, 37);
            txtPorte.Name = "txtPorte";
            txtPorte.ReadOnly = true;
            txtPorte.Size = new Size(114, 27);
            txtPorte.TabIndex = 84;
            txtPorte.Visible = false;
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
            pbPhoto.Location = new Point(883, 14);
            pbPhoto.Name = "pbPhoto";
            pbPhoto.Size = new Size(247, 188);
            pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPhoto.TabIndex = 78;
            pbPhoto.TabStop = false;
            // 
            // btnUploadPhoto
            // 
            btnUploadPhoto.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUploadPhoto.Location = new Point(948, 208);
            btnUploadPhoto.Name = "btnUploadPhoto";
            btnUploadPhoto.Size = new Size(128, 31);
            btnUploadPhoto.TabIndex = 16;
            btnUploadPhoto.Text = "Upload photo";
            btnUploadPhoto.UseVisualStyleBackColor = true;
            btnUploadPhoto.Click += btnUploadPhoto_Click;
            // 
            // txtId
            // 
            txtId.Location = new Point(1011, 314);
            txtId.Name = "txtId";
            txtId.Size = new Size(65, 23);
            txtId.TabIndex = 75;
            txtId.Visible = false;
            // 
            // chkPadrinho
            // 
            chkPadrinho.AutoSize = true;
            chkPadrinho.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            chkPadrinho.Location = new Point(726, 124);
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
            chkEsterilizado.Location = new Point(726, 94);
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
            cboTemperamento.Location = new Point(235, 174);
            cboTemperamento.Name = "cboTemperamento";
            cboTemperamento.Size = new Size(235, 28);
            cboTemperamento.TabIndex = 10;
            cboTemperamento.SelectedIndexChanged += cboTemperamento_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(235, 151);
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
            cboSituacao.Size = new Size(205, 28);
            cboSituacao.TabIndex = 9;
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
            cboRaca.Location = new Point(235, 105);
            cboRaca.Name = "cboRaca";
            cboRaca.Size = new Size(235, 28);
            cboRaca.TabIndex = 7;
            cboRaca.SelectedIndexChanged += cboRaca_SelectedIndexChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(238, 81);
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
            txtObservacoes.Size = new Size(358, 94);
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
            txtChip.Location = new Point(973, 343);
            txtChip.Name = "txtChip";
            txtChip.Size = new Size(103, 27);
            txtChip.TabIndex = 57;
            txtChip.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(488, 81);
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
            txtFoto.Location = new Point(940, 314);
            txtFoto.Name = "txtFoto";
            txtFoto.Size = new Size(40, 23);
            txtFoto.TabIndex = 51;
            txtFoto.Visible = false;
            // 
            // txtCor
            // 
            txtCor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtCor.Location = new Point(488, 106);
            txtCor.MaxLength = 40;
            txtCor.Name = "txtCor";
            txtCor.Size = new Size(213, 27);
            txtCor.TabIndex = 8;
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
            txtIdade.Location = new Point(868, 314);
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
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.save32;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(157, 419);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(108, 39);
            btnUpdate.TabIndex = 19;
            btnUpdate.Text = "Update ";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Red;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources._678080_shield_error_32;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(281, 419);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(108, 39);
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
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(461, 419);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(108, 39);
            btnClear.TabIndex = 21;
            btnClear.Text = "Clear ";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnGeneratePdf
            // 
            btnGeneratePdf.BackColor = Color.WhiteSmoke;
            btnGeneratePdf.FlatStyle = FlatStyle.Flat;
            btnGeneratePdf.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGeneratePdf.ForeColor = Color.Black;
            btnGeneratePdf.Image = Properties.Resources.print_pdf2;
            btnGeneratePdf.ImageAlign = ContentAlignment.MiddleLeft;
            btnGeneratePdf.Location = new Point(19, 403);
            btnGeneratePdf.Name = "btnGeneratePdf";
            btnGeneratePdf.Size = new Size(131, 53);
            btnGeneratePdf.TabIndex = 22;
            btnGeneratePdf.Text = "Pdf  ";
            btnGeneratePdf.TextAlign = ContentAlignment.MiddleRight;
            btnGeneratePdf.UseVisualStyleBackColor = false;
            btnGeneratePdf.Click += btnGeneratePdf_Click;
            // 
            // btnVaccines
            // 
            btnVaccines.BackColor = Color.White;
            btnVaccines.BackgroundImageLayout = ImageLayout.None;
            btnVaccines.Enabled = false;
            btnVaccines.FlatStyle = FlatStyle.Flat;
            btnVaccines.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnVaccines.ForeColor = Color.Black;
            btnVaccines.Image = (Image)resources.GetObject("btnVaccines.Image");
            btnVaccines.ImageAlign = ContentAlignment.MiddleRight;
            btnVaccines.Location = new Point(19, 137);
            btnVaccines.Name = "btnVaccines";
            btnVaccines.Size = new Size(131, 48);
            btnVaccines.TabIndex = 24;
            btnVaccines.Text = "Vacinas";
            btnVaccines.TextAlign = ContentAlignment.TopLeft;
            btnVaccines.UseVisualStyleBackColor = false;
            btnVaccines.Click += btnVaccines_Click;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnDocs);
            panel2.Controls.Add(btnGeneratePdf);
            panel2.Controls.Add(btnDewormers);
            panel2.Controls.Add(btnDogFood);
            panel2.Controls.Add(btnAppts);
            panel2.Controls.Add(btnVaccines);
            panel2.Location = new Point(1183, 8);
            panel2.Name = "panel2";
            panel2.Size = new Size(175, 464);
            panel2.TabIndex = 25;
            // 
            // btnDocs
            // 
            btnDocs.BackColor = Color.White;
            btnDocs.BackgroundImageLayout = ImageLayout.None;
            btnDocs.Enabled = false;
            btnDocs.FlatStyle = FlatStyle.Flat;
            btnDocs.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDocs.ForeColor = Color.Black;
            btnDocs.Image = (Image)resources.GetObject("btnDocs.Image");
            btnDocs.ImageAlign = ContentAlignment.BottomRight;
            btnDocs.Location = new Point(19, 13);
            btnDocs.Name = "btnDocs";
            btnDocs.Size = new Size(131, 69);
            btnDocs.TabIndex = 28;
            btnDocs.Text = "Documentos";
            btnDocs.TextAlign = ContentAlignment.TopLeft;
            btnDocs.UseVisualStyleBackColor = false;
            btnDocs.Click += btnDocs_Click;
            // 
            // btnDewormers
            // 
            btnDewormers.BackColor = Color.White;
            btnDewormers.BackgroundImageLayout = ImageLayout.None;
            btnDewormers.Enabled = false;
            btnDewormers.FlatStyle = FlatStyle.Flat;
            btnDewormers.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDewormers.ForeColor = Color.Black;
            btnDewormers.Image = (Image)resources.GetObject("btnDewormers.Image");
            btnDewormers.ImageAlign = ContentAlignment.BottomRight;
            btnDewormers.Location = new Point(19, 299);
            btnDewormers.Name = "btnDewormers";
            btnDewormers.Size = new Size(131, 64);
            btnDewormers.TabIndex = 27;
            btnDewormers.Text = "Desparasitantes";
            btnDewormers.TextAlign = ContentAlignment.TopLeft;
            btnDewormers.UseVisualStyleBackColor = false;
            btnDewormers.Click += btnDewormers_Click;
            // 
            // btnDogFood
            // 
            btnDogFood.BackColor = Color.White;
            btnDogFood.BackgroundImageLayout = ImageLayout.None;
            btnDogFood.Enabled = false;
            btnDogFood.FlatStyle = FlatStyle.Flat;
            btnDogFood.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnDogFood.ForeColor = Color.Black;
            btnDogFood.Image = (Image)resources.GetObject("btnDogFood.Image");
            btnDogFood.ImageAlign = ContentAlignment.MiddleRight;
            btnDogFood.Location = new Point(19, 245);
            btnDogFood.Name = "btnDogFood";
            btnDogFood.Size = new Size(131, 48);
            btnDogFood.TabIndex = 26;
            btnDogFood.Text = "Rações";
            btnDogFood.TextAlign = ContentAlignment.TopLeft;
            btnDogFood.UseVisualStyleBackColor = false;
            btnDogFood.Click += btnDogFood_Click;
            // 
            // btnAppts
            // 
            btnAppts.BackColor = Color.White;
            btnAppts.BackgroundImageLayout = ImageLayout.None;
            btnAppts.Enabled = false;
            btnAppts.FlatStyle = FlatStyle.Flat;
            btnAppts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnAppts.ForeColor = Color.Black;
            btnAppts.Image = (Image)resources.GetObject("btnAppts.Image");
            btnAppts.ImageAlign = ContentAlignment.MiddleRight;
            btnAppts.Location = new Point(19, 191);
            btnAppts.Name = "btnAppts";
            btnAppts.Size = new Size(131, 48);
            btnAppts.TabIndex = 25;
            btnAppts.Text = "Consultas";
            btnAppts.TextAlign = ContentAlignment.TopLeft;
            btnAppts.UseVisualStyleBackColor = false;
            btnAppts.Click += btnAppts_Click;
            // 
            // frmPets
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.SeaShell;
            CaptionBarHeight = 100;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.catsanddogs;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(128, 96);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(160, 12);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Size = new Size(300, 24);
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(160, 50);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(250, 22);
            captionLabel2.Text = "Gestão da base de dados";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1359, 694);
            Controls.Add(panel2);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(panel1);
            Controls.Add(btnInsert);
            Controls.Add(gdvDados);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPets";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)gdvDados).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPhoto).EndInit();
            panel2.ResumeLayout(false);
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
        private ComboBox cboGenero;
        private Label label8;
        private Button btnVaccines;
        private Panel panel2;
        private Button btnAppts;
        private Button btnDewormers;
        private Button btnDogFood;
        private Button btnDocs;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn EspecieAnimal;
        private DataGridViewCheckBoxColumn Chipado;
        private DataGridViewCheckBoxColumn Esterilizado;
        private DataGridViewTextBoxColumn SituacaoAnimal;
        private DataGridViewTextBoxColumn TemperamentoAnimal;
        private DataGridViewTextBoxColumn RacaAnimal;
        private DataGridViewTextBoxColumn TamanhoAnimal;
    }
}