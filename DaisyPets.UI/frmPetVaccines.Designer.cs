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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            dgvVacinas = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            NomePet = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            DataToma = new DataGridViewTextBoxColumn();
            ProximaTomaEmMeses = new DataGridViewTextBoxColumn();
            DataProximaToma = new DataGridViewTextBoxColumn();
            PetId = new DataGridViewTextBoxColumn();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            btnInfo = new Button();
            dtpToma = new DateTimePicker();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            nupPrxToma = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
            txtMarca = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)dgvVacinas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nupPrxToma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).BeginInit();
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
            dgvVacinas.Columns.AddRange(new DataGridViewColumn[] { Id, NomePet, Marca, DataToma, ProximaTomaEmMeses, DataProximaToma, PetId });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvVacinas.DefaultCellStyle = dataGridViewCellStyle4;
            dgvVacinas.Location = new Point(19, 275);
            dgvVacinas.Name = "dgvVacinas";
            dgvVacinas.ReadOnly = true;
            dgvVacinas.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dgvVacinas.RowTemplate.Height = 32;
            dgvVacinas.Size = new Size(650, 269);
            dgvVacinas.TabIndex = 8;
            dgvVacinas.CellClick += dgvVacinas_CellClick;
            dgvVacinas.ColumnHeaderMouseClick += dgvVacinas_ColumnHeaderMouseClick;
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
            NomePet.Visible = false;
            NomePet.Width = 200;
            // 
            // Marca
            // 
            Marca.DataPropertyName = "Marca";
            Marca.HeaderText = "Vacina";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            Marca.Width = 300;
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
            // PetId
            // 
            PetId.DataPropertyName = "IdPet";
            PetId.HeaderText = "PetId";
            PetId.Name = "PetId";
            PetId.ReadOnly = true;
            PetId.Visible = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(483, 215);
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
            gradientPanel1.Controls.Add(btnInfo);
            gradientPanel1.Controls.Add(dtpToma);
            gradientPanel1.Controls.Add(autoLabel5);
            gradientPanel1.Controls.Add(txtId);
            gradientPanel1.Controls.Add(autoLabel4);
            gradientPanel1.Controls.Add(autoLabel3);
            gradientPanel1.Controls.Add(autoLabel2);
            gradientPanel1.Controls.Add(autoLabel1);
            gradientPanel1.Controls.Add(nupPrxToma);
            gradientPanel1.Controls.Add(txtMarca);
            gradientPanel1.Controls.Add(txtPetName);
            gradientPanel1.Location = new Point(19, 15);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(562, 173);
            gradientPanel1.TabIndex = 26;
            // 
            // btnInfo
            // 
            btnInfo.BackColor = Color.White;
            btnInfo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInfo.ForeColor = Color.Black;
            btnInfo.Image = Properties.Resources.Info_32;
            btnInfo.ImageAlign = ContentAlignment.MiddleLeft;
            btnInfo.Location = new Point(448, 81);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(82, 40);
            btnInfo.TabIndex = 26;
            btnInfo.Text = "Info";
            btnInfo.TextAlign = ContentAlignment.MiddleRight;
            btnInfo.UseVisualStyleBackColor = false;
            btnInfo.Click += btnInfo_Click;
            // 
            // dtpToma
            // 
            dtpToma.CalendarFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToma.Format = DateTimePickerFormat.Short;
            dtpToma.Location = new Point(154, 50);
            dtpToma.Name = "dtpToma";
            dtpToma.Size = new Size(110, 27);
            dtpToma.TabIndex = 18;
            // 
            // autoLabel5
            // 
            autoLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel5.Location = new Point(215, 129);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(50, 20);
            autoLabel5.TabIndex = 17;
            autoLabel5.Text = "meses";
            // 
            // txtId
            // 
            txtId.BeforeTouchSize = new Size(273, 27);
            txtId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(511, 11);
            txtId.Name = "txtId";
            txtId.Size = new Size(41, 27);
            txtId.TabIndex = 16;
            txtId.Visible = false;
            // 
            // autoLabel4
            // 
            autoLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel4.Location = new Point(12, 50);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(101, 20);
            autoLabel4.TabIndex = 15;
            autoLabel4.Text = "Data da toma";
            // 
            // autoLabel3
            // 
            autoLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel3.Location = new Point(12, 88);
            autoLabel3.Name = "autoLabel3";
            autoLabel3.Size = new Size(52, 20);
            autoLabel3.TabIndex = 14;
            autoLabel3.Text = "Vacina";
            // 
            // autoLabel2
            // 
            autoLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel2.Location = new Point(12, 129);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(127, 20);
            autoLabel2.TabIndex = 13;
            autoLabel2.Text = "Próxima toma em";
            // 
            // autoLabel1
            // 
            autoLabel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel1.Location = new Point(12, 13);
            autoLabel1.Name = "autoLabel1";
            autoLabel1.Size = new Size(50, 20);
            autoLabel1.TabIndex = 12;
            autoLabel1.Text = "Nome";
            // 
            // nupPrxToma
            // 
            nupPrxToma.BeforeTouchSize = new Size(53, 27);
            nupPrxToma.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            nupPrxToma.Location = new Point(154, 127);
            nupPrxToma.Name = "nupPrxToma";
            nupPrxToma.Size = new Size(53, 27);
            nupPrxToma.TabIndex = 11;
            nupPrxToma.Value = new decimal(new int[] { 18, 0, 0, 0 });
            // 
            // txtMarca
            // 
            txtMarca.BeforeTouchSize = new Size(273, 27);
            txtMarca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMarca.Location = new Point(154, 88);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(273, 27);
            txtMarca.TabIndex = 10;
            // 
            // txtPetName
            // 
            txtPetName.BeforeTouchSize = new Size(273, 27);
            txtPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetName.Location = new Point(154, 10);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(273, 27);
            txtPetName.TabIndex = 8;
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
            ClientSize = new Size(676, 556);
            Controls.Add(gradientPanel1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(dgvVacinas);
            MaximizeBox = false;
            MinimizeBox = false;
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
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtId;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private DateTimePicker dtpToma;
        private Button btnInfo;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn NomePet;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn DataToma;
        private DataGridViewTextBoxColumn ProximaTomaEmMeses;
        private DataGridViewTextBoxColumn DataProximaToma;
        private DataGridViewTextBoxColumn PetId;
    }
}