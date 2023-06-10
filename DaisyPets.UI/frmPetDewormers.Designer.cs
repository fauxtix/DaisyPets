namespace DaisyPets.UI
{
    partial class frmPetDewormers
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetDewormers));
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            dgvDewormers = new DataGridView();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            cboTipo = new ComboBox();
            dtpProximaAplicacao = new DateTimePicker();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            btnInfo = new Button();
            dtpAplicacao = new DateTimePicker();
            txtId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtMarca = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            Id = new DataGridViewTextBoxColumn();
            NomePet = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            DataAplicacao = new DataGridViewTextBoxColumn();
            DataProximaAplicacao = new DataGridViewTextBoxColumn();
            PetId = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvDewormers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).BeginInit();
            SuspendLayout();
            // 
            // dgvDewormers
            // 
            dgvDewormers.AllowUserToAddRows = false;
            dgvDewormers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDewormers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDewormers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDewormers.Columns.AddRange(new DataGridViewColumn[] { Id, NomePet, Marca, Tipo, DataAplicacao, DataProximaAplicacao, PetId });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvDewormers.DefaultCellStyle = dataGridViewCellStyle5;
            dgvDewormers.Location = new Point(19, 297);
            dgvDewormers.Name = "dgvDewormers";
            dgvDewormers.ReadOnly = true;
            dgvDewormers.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dgvDewormers.RowTemplate.Height = 32;
            dgvDewormers.Size = new Size(780, 214);
            dgvDewormers.TabIndex = 8;
            dgvDewormers.CellClick += dgvDewormers_CellClick;
            dgvDewormers.ColumnHeaderMouseClick += dgvDewormers_ColumnHeaderMouseClick;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(442, 242);
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
            btnDelete.Location = new Point(268, 242);
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
            btnUpdate.Location = new Point(144, 242);
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
            btnInsert.Location = new Point(20, 242);
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
            gradientPanel1.BackColor = Color.WhiteSmoke;
            gradientPanel1.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel1.Controls.Add(autoLabel5);
            gradientPanel1.Controls.Add(cboTipo);
            gradientPanel1.Controls.Add(dtpProximaAplicacao);
            gradientPanel1.Controls.Add(autoLabel2);
            gradientPanel1.Controls.Add(btnInfo);
            gradientPanel1.Controls.Add(dtpAplicacao);
            gradientPanel1.Controls.Add(txtId);
            gradientPanel1.Controls.Add(autoLabel4);
            gradientPanel1.Controls.Add(autoLabel3);
            gradientPanel1.Controls.Add(autoLabel1);
            gradientPanel1.Controls.Add(txtMarca);
            gradientPanel1.Controls.Add(txtPetName);
            gradientPanel1.Location = new Point(19, 15);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(634, 208);
            gradientPanel1.TabIndex = 26;
            // 
            // autoLabel5
            // 
            autoLabel5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel5.Location = new Point(12, 52);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(140, 20);
            autoLabel5.TabIndex = 30;
            autoLabel5.Text = "Tipo desparasitante";
            // 
            // cboTipo
            // 
            cboTipo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboTipo.FormattingEnabled = true;
            cboTipo.Items.AddRange(new object[] { "Interno", "Externo" });
            cboTipo.Location = new Point(190, 49);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(111, 28);
            cboTipo.TabIndex = 29;
            cboTipo.SelectedIndexChanged += cboTipo_SelectedIndexChanged;
            // 
            // dtpProximaAplicacao
            // 
            dtpProximaAplicacao.CalendarFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dtpProximaAplicacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpProximaAplicacao.Format = DateTimePickerFormat.Short;
            dtpProximaAplicacao.Location = new Point(191, 168);
            dtpProximaAplicacao.Name = "dtpProximaAplicacao";
            dtpProximaAplicacao.Size = new Size(110, 27);
            dtpProximaAplicacao.TabIndex = 28;
            // 
            // autoLabel2
            // 
            autoLabel2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel2.Location = new Point(12, 168);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(168, 20);
            autoLabel2.TabIndex = 27;
            autoLabel2.Text = "Data próxima aplicação";
            // 
            // btnInfo
            // 
            btnInfo.BackColor = Color.White;
            btnInfo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInfo.ForeColor = Color.Black;
            btnInfo.Image = Properties.Resources.Info_32;
            btnInfo.ImageAlign = ContentAlignment.MiddleLeft;
            btnInfo.Location = new Point(547, 121);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(82, 40);
            btnInfo.TabIndex = 26;
            btnInfo.Text = "Info";
            btnInfo.TextAlign = ContentAlignment.MiddleRight;
            btnInfo.UseVisualStyleBackColor = false;
            btnInfo.Click += btnInfo_Click;
            // 
            // dtpAplicacao
            // 
            dtpAplicacao.CalendarFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dtpAplicacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpAplicacao.Format = DateTimePickerFormat.Short;
            dtpAplicacao.Location = new Point(191, 93);
            dtpAplicacao.Name = "dtpAplicacao";
            dtpAplicacao.Size = new Size(110, 27);
            dtpAplicacao.TabIndex = 18;
            dtpAplicacao.ValueChanged += dtpAplicacao_ValueChanged;
            // 
            // txtId
            // 
            txtId.BeforeTouchSize = new Size(329, 27);
            txtId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(573, 10);
            txtId.Name = "txtId";
            txtId.Size = new Size(41, 27);
            txtId.TabIndex = 16;
            txtId.Visible = false;
            // 
            // autoLabel4
            // 
            autoLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel4.Location = new Point(12, 93);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(109, 20);
            autoLabel4.TabIndex = 15;
            autoLabel4.Text = "Data aplicação";
            // 
            // autoLabel3
            // 
            autoLabel3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel3.Location = new Point(12, 131);
            autoLabel3.Name = "autoLabel3";
            autoLabel3.Size = new Size(50, 20);
            autoLabel3.TabIndex = 14;
            autoLabel3.Text = "Marca";
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
            // txtMarca
            // 
            txtMarca.BeforeTouchSize = new Size(329, 27);
            txtMarca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMarca.Location = new Point(191, 131);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(329, 27);
            txtMarca.TabIndex = 10;
            // 
            // txtPetName
            // 
            txtPetName.BeforeTouchSize = new Size(329, 27);
            txtPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetName.Location = new Point(191, 10);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(329, 27);
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
            NomePet.Visible = false;
            NomePet.Width = 200;
            // 
            // Marca
            // 
            Marca.DataPropertyName = "Marca";
            Marca.HeaderText = "Desparasitante";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            Marca.Width = 300;
            // 
            // Tipo
            // 
            Tipo.DataPropertyName = "Tipo";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Tipo.DefaultCellStyle = dataGridViewCellStyle2;
            Tipo.HeaderText = "Tipo";
            Tipo.Name = "Tipo";
            Tipo.ReadOnly = true;
            // 
            // DataAplicacao
            // 
            DataAplicacao.DataPropertyName = "DataAplicacao";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataAplicacao.DefaultCellStyle = dataGridViewCellStyle3;
            DataAplicacao.HeaderText = "Data aplicação";
            DataAplicacao.Name = "DataAplicacao";
            DataAplicacao.ReadOnly = true;
            DataAplicacao.Width = 170;
            // 
            // DataProximaAplicacao
            // 
            DataProximaAplicacao.DataPropertyName = "DataProximaAplicacao";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataProximaAplicacao.DefaultCellStyle = dataGridViewCellStyle4;
            DataProximaAplicacao.HeaderText = "Próxima aplicação";
            DataProximaAplicacao.Name = "DataProximaAplicacao";
            DataProximaAplicacao.ReadOnly = true;
            DataProximaAplicacao.Width = 160;
            // 
            // PetId
            // 
            PetId.DataPropertyName = "IdPet";
            PetId.HeaderText = "PetId";
            PetId.Name = "PetId";
            PetId.ReadOnly = true;
            PetId.Visible = false;
            // 
            // frmPetDewormers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            CaptionBarColor = SystemColors.GradientInactiveCaption;
            CaptionBarHeight = 60;
            CaptionButtonColor = Color.DarkGoldenrod;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = (Image)resources.GetObject("captionImage1.Image");
            captionImage1.Location = new Point(6, 8);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(36, 36);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel1.Location = new Point(60, 2);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "DaisyPets";
            captionLabel2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(60, 24);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(300, 32);
            captionLabel2.Text = "Registo de desparasitantes";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(819, 514);
            Controls.Add(gradientPanel1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(dgvDewormers);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPetDewormers";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvDewormers).EndInit();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).EndInit();
            gradientPanel1.ResumeLayout(false);
            gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvDewormers;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtMarca;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtId;
        private DateTimePicker dtpAplicacao;
        private Button btnInfo;
        private DateTimePicker dtpProximaAplicacao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private ComboBox cboTipo;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn NomePet;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn DataAplicacao;
        private DataGridViewTextBoxColumn DataProximaAplicacao;
        private DataGridViewTextBoxColumn PetId;
    }
}