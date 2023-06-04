namespace DaisyPets.UI
{
    partial class frmPetVaccines
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            dgvVacinas = new DataGridView();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            nupPrxToma = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
            txtMarca = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            dtpToma = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            Id = new DataGridViewTextBoxColumn();
            NomePet = new DataGridViewTextBoxColumn();
            DataToma = new DataGridViewTextBoxColumn();
            ProximaTomaEmMeses = new DataGridViewTextBoxColumn();
            DataProximaToma = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            PetId = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvVacinas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nupPrxToma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpToma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpToma.Calendar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).BeginInit();
            SuspendLayout();
            // 
            // dgvVacinas
            // 
            dgvVacinas.AllowUserToAddRows = false;
            dgvVacinas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvVacinas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvVacinas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVacinas.Columns.AddRange(new DataGridViewColumn[] { Id, NomePet, DataToma, ProximaTomaEmMeses, DataProximaToma, Marca, PetId });
            dgvVacinas.Location = new Point(19, 275);
            dgvVacinas.Name = "dgvVacinas";
            dgvVacinas.ReadOnly = true;
            dgvVacinas.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dgvVacinas.RowTemplate.Height = 32;
            dgvVacinas.Size = new Size(748, 122);
            dgvVacinas.TabIndex = 8;
            dgvVacinas.CellClick += dgvVacinas_CellClick;
            dgvVacinas.ColumnHeaderMouseClick += dgvVacinas_ColumnHeaderMouseClick;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(513, 215);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 25;
            btnClear.Text = "Clear ";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Red;
            btnDelete.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources._678080_shield_error_32;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(267, 215);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 24;
            btnDelete.Text = "Delete ";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.ForestGreen;
            btnUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.save32;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(143, 215);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 23;
            btnUpdate.Text = "Update ";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(19, 215);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 22;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // gradientPanel1
            // 
            gradientPanel1.BackColor = Color.Linen;
            gradientPanel1.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel1.Controls.Add(autoLabel5);
            gradientPanel1.Controls.Add(txtId);
            gradientPanel1.Controls.Add(autoLabel4);
            gradientPanel1.Controls.Add(autoLabel3);
            gradientPanel1.Controls.Add(autoLabel2);
            gradientPanel1.Controls.Add(autoLabel1);
            gradientPanel1.Controls.Add(nupPrxToma);
            gradientPanel1.Controls.Add(txtMarca);
            gradientPanel1.Controls.Add(dtpToma);
            gradientPanel1.Controls.Add(txtPetName);
            gradientPanel1.Location = new Point(19, 15);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(592, 173);
            gradientPanel1.TabIndex = 26;
            // 
            // autoLabel5
            // 
            autoLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel5.Location = new Point(203, 129);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(50, 20);
            autoLabel5.TabIndex = 17;
            autoLabel5.Text = "meses";
            // 
            // txtId
            // 
            txtId.BeforeTouchSize = new Size(314, 27);
            txtId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(476, 126);
            txtId.Name = "txtId";
            txtId.Size = new Size(89, 27);
            txtId.TabIndex = 16;
            txtId.Visible = false;
            // 
            // autoLabel4
            // 
            autoLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel4.Location = new Point(22, 50);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(101, 20);
            autoLabel4.TabIndex = 15;
            autoLabel4.Text = "Data da toma";
            // 
            // autoLabel3
            // 
            autoLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel3.Location = new Point(22, 88);
            autoLabel3.Name = "autoLabel3";
            autoLabel3.Size = new Size(50, 20);
            autoLabel3.TabIndex = 14;
            autoLabel3.Text = "Marca";
            // 
            // autoLabel2
            // 
            autoLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel2.Location = new Point(22, 129);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(102, 20);
            autoLabel2.TabIndex = 13;
            autoLabel2.Text = "Próxima toma";
            // 
            // autoLabel1
            // 
            autoLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel1.Location = new Point(22, 13);
            autoLabel1.Name = "autoLabel1";
            autoLabel1.Size = new Size(50, 20);
            autoLabel1.TabIndex = 12;
            autoLabel1.Text = "Nome";
            // 
            // nupPrxToma
            // 
            nupPrxToma.BeforeTouchSize = new Size(53, 27);
            nupPrxToma.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            nupPrxToma.Location = new Point(142, 127);
            nupPrxToma.Name = "nupPrxToma";
            nupPrxToma.Size = new Size(53, 27);
            nupPrxToma.TabIndex = 11;
            // 
            // txtMarca
            // 
            txtMarca.BeforeTouchSize = new Size(314, 27);
            txtMarca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMarca.Location = new Point(142, 88);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(314, 27);
            txtMarca.TabIndex = 10;
            // 
            // dtpToma
            // 
            dtpToma.BorderColor = Color.Empty;
            dtpToma.BorderStyle = BorderStyle.FixedSingle;
            // 
            // 
            // 
            dtpToma.Calendar.AllowMultipleSelection = false;
            dtpToma.Calendar.BorderColor = Color.FromArgb(209, 211, 212);
            dtpToma.Calendar.BottomHeight = 30;
            dtpToma.Calendar.Culture = new System.Globalization.CultureInfo("pt-PT");
            dtpToma.Calendar.DayNamesFont = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dtpToma.Calendar.DayNamesHeight = 137;
            dtpToma.Calendar.DaysFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.Calendar.Dock = DockStyle.Fill;
            dtpToma.Calendar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.Calendar.HeaderFont = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dtpToma.Calendar.HeaderHeight = 38;
            dtpToma.Calendar.Iso8601CalenderFormat = false;
            dtpToma.Calendar.Location = new Point(0, 0);
            dtpToma.Calendar.MetroColor = Color.FromArgb(22, 165, 220);
            dtpToma.Calendar.Name = "monthCalendar";
            // 
            // 
            // 
            dtpToma.Calendar.NoneButton.AutoSize = true;
            dtpToma.Calendar.NoneButton.Location = new Point(190, 0);
            dtpToma.Calendar.NoneButton.Size = new Size(72, 30);
            dtpToma.Calendar.NoneButton.Text = "None";
            dtpToma.Calendar.Size = new Size(262, 249);
            dtpToma.Calendar.SizeToFit = true;
            dtpToma.Calendar.TabIndex = 0;
            // 
            // 
            // 
            dtpToma.Calendar.TodayButton.AutoSize = true;
            dtpToma.Calendar.TodayButton.Location = new Point(0, 0);
            dtpToma.Calendar.TodayButton.Size = new Size(190, 30);
            dtpToma.Calendar.TodayButton.Text = "Today";
            dtpToma.Calendar.WeekFont = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.CalendarSize = new Size(189, 176);
            dtpToma.Checked = false;
            dtpToma.Culture = new System.Globalization.CultureInfo("pt-PT");
            dtpToma.DropDownImage = null;
            dtpToma.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.Format = DateTimePickerFormat.Short;
            dtpToma.Location = new Point(144, 50);
            dtpToma.MetroColor = Color.FromArgb(22, 165, 220);
            dtpToma.MinValue = new DateTime(0L);
            dtpToma.Name = "dtpToma";
            dtpToma.Size = new Size(125, 32);
            dtpToma.TabIndex = 9;
            dtpToma.Value = new DateTime(2023, 6, 3, 16, 56, 18, 840);
            // 
            // txtPetName
            // 
            txtPetName.BeforeTouchSize = new Size(314, 27);
            txtPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetName.Location = new Point(142, 10);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(314, 27);
            txtPetName.TabIndex = 8;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // NomePet
            // 
            NomePet.DataPropertyName = "NomePet";
            NomePet.HeaderText = "Nome";
            NomePet.Name = "NomePet";
            NomePet.ReadOnly = true;
            NomePet.Width = 200;
            // 
            // DataToma
            // 
            DataToma.DataPropertyName = "DataToma";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataToma.DefaultCellStyle = dataGridViewCellStyle2;
            DataToma.HeaderText = "Data";
            DataToma.Name = "DataToma";
            DataToma.ReadOnly = true;
            // 
            // ProximaTomaEmMeses
            // 
            ProximaTomaEmMeses.DataPropertyName = "ProximaTomaEmMeses";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProximaTomaEmMeses.DefaultCellStyle = dataGridViewCellStyle3;
            ProximaTomaEmMeses.HeaderText = "Prx. Toma";
            ProximaTomaEmMeses.Name = "ProximaTomaEmMeses";
            ProximaTomaEmMeses.ReadOnly = true;
            // 
            // DataProximaToma
            // 
            DataProximaToma.DataPropertyName = "DataProximaToma";
            DataProximaToma.HeaderText = "Próxima";
            DataProximaToma.Name = "DataProximaToma";
            DataProximaToma.ReadOnly = true;
            // 
            // Marca
            // 
            Marca.DataPropertyName = "Marca";
            Marca.HeaderText = "Marca";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            Marca.Width = 300;
            // 
            // PetId
            // 
            PetId.DataPropertyName = "IdPet";
            PetId.HeaderText = "PetId";
            PetId.Name = "PetId";
            PetId.ReadOnly = true;
            PetId.Visible = false;
            // 
            // frmPetVaccines
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SeaShell;
            CaptionBarColor = Color.Peru;
            CaptionBarHeight = 51;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.syringe;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(36, 36);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(60, 2);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "DaisyPets";
            captionLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(60, 24);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(300, 26);
            captionLabel2.Text = "Registos de vacinação";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(780, 409);
            Controls.Add(gradientPanel1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(dgvVacinas);
            Name = "frmPetVaccines";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvVacinas).EndInit();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).EndInit();
            gradientPanel1.ResumeLayout(false);
            gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).EndInit();
            ((System.ComponentModel.ISupportInitialize)nupPrxToma).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpToma.Calendar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpToma).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvVacinas;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.NumericUpDownExt nupPrxToma;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtMarca;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dtpToma;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtId;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn NomePet;
        private DataGridViewTextBoxColumn DataToma;
        private DataGridViewTextBoxColumn ProximaTomaEmMeses;
        private DataGridViewTextBoxColumn DataProximaToma;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn PetId;
    }
}