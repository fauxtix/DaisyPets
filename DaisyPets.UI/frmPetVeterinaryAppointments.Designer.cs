namespace DaisyPets.UI
{
    partial class frmPetVeterinaryAppointments
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetVeterinaryAppointments));
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            dtpApptDate = new DateTimePicker();
            txtIdPet = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtMotivo = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtDiagnostico = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtTratamento = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtID = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            dgvAppts = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Nome = new DataGridViewTextBoxColumn();
            DataConsulta = new DataGridViewTextBoxColumn();
            Moitvo = new DataGridViewTextBoxColumn();
            Diagnostico = new DataGridViewTextBoxColumn();
            Tratamento = new DataGridViewTextBoxColumn();
            PetId = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtPetName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIdPet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMotivo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDiagnostico).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTratamento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAppts).BeginInit();
            SuspendLayout();
            // 
            // gradientPanel1
            // 
            gradientPanel1.BackColor = Color.Gainsboro;
            gradientPanel1.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel1.Controls.Add(autoLabel2);
            gradientPanel1.Controls.Add(txtPetName);
            gradientPanel1.Controls.Add(dtpApptDate);
            gradientPanel1.Controls.Add(txtIdPet);
            gradientPanel1.Controls.Add(autoLabel6);
            gradientPanel1.Controls.Add(autoLabel5);
            gradientPanel1.Controls.Add(autoLabel4);
            gradientPanel1.Controls.Add(autoLabel3);
            gradientPanel1.Controls.Add(autoLabel1);
            gradientPanel1.Controls.Add(txtMotivo);
            gradientPanel1.Controls.Add(txtDiagnostico);
            gradientPanel1.Controls.Add(txtTratamento);
            gradientPanel1.Controls.Add(txtID);
            gradientPanel1.Location = new Point(22, 34);
            gradientPanel1.Margin = new Padding(3, 4, 3, 4);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(591, 290);
            gradientPanel1.TabIndex = 11;
            // 
            // autoLabel2
            // 
            autoLabel2.BackColor = Color.Gainsboro;
            autoLabel2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel2.ForeColor = SystemColors.ControlText;
            autoLabel2.Location = new Point(18, 43);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(51, 20);
            autoLabel2.TabIndex = 29;
            autoLabel2.Text = "Nome";
            // 
            // txtPetName
            // 
            txtPetName.BeforeTouchSize = new Size(412, 91);
            txtPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetName.Location = new Point(151, 40);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(273, 27);
            txtPetName.TabIndex = 28;
            // 
            // dtpApptDate
            // 
            dtpApptDate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpApptDate.Format = DateTimePickerFormat.Short;
            dtpApptDate.Location = new Point(151, 73);
            dtpApptDate.Name = "dtpApptDate";
            dtpApptDate.Size = new Size(115, 27);
            dtpApptDate.TabIndex = 27;
            // 
            // txtIdPet
            // 
            txtIdPet.BeforeTouchSize = new Size(412, 91);
            txtIdPet.Location = new Point(439, 11);
            txtIdPet.Margin = new Padding(3, 4, 3, 4);
            txtIdPet.Name = "txtIdPet";
            txtIdPet.Size = new Size(61, 23);
            txtIdPet.TabIndex = 26;
            txtIdPet.Visible = false;
            // 
            // autoLabel6
            // 
            autoLabel6.BackColor = Color.Gainsboro;
            autoLabel6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel6.ForeColor = SystemColors.ControlText;
            autoLabel6.Location = new Point(18, 73);
            autoLabel6.Name = "autoLabel6";
            autoLabel6.Size = new Size(41, 20);
            autoLabel6.TabIndex = 23;
            autoLabel6.Text = "Data";
            // 
            // autoLabel5
            // 
            autoLabel5.BackColor = Color.Gainsboro;
            autoLabel5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel5.ForeColor = SystemColors.ControlText;
            autoLabel5.Location = new Point(18, 106);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(58, 20);
            autoLabel5.TabIndex = 22;
            autoLabel5.Text = "Motivo";
            // 
            // autoLabel4
            // 
            autoLabel4.BackColor = Color.Gainsboro;
            autoLabel4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel4.ForeColor = SystemColors.ControlText;
            autoLabel4.Location = new Point(18, 138);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(90, 20);
            autoLabel4.TabIndex = 21;
            autoLabel4.Text = "Diagnóstico";
            // 
            // autoLabel3
            // 
            autoLabel3.BackColor = Color.Gainsboro;
            autoLabel3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel3.ForeColor = SystemColors.ControlText;
            autoLabel3.Location = new Point(18, 193);
            autoLabel3.Name = "autoLabel3";
            autoLabel3.Size = new Size(87, 20);
            autoLabel3.TabIndex = 20;
            autoLabel3.Text = "Tratamento";
            // 
            // autoLabel1
            // 
            autoLabel1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel1.ForeColor = SystemColors.ControlText;
            autoLabel1.Location = new Point(18, 10);
            autoLabel1.Name = "autoLabel1";
            autoLabel1.Size = new Size(22, 20);
            autoLabel1.TabIndex = 18;
            autoLabel1.Text = "Id";
            // 
            // txtMotivo
            // 
            txtMotivo.BeforeTouchSize = new Size(412, 91);
            txtMotivo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMotivo.Location = new Point(151, 103);
            txtMotivo.Margin = new Padding(3, 4, 3, 4);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(412, 27);
            txtMotivo.TabIndex = 1;
            // 
            // txtDiagnostico
            // 
            txtDiagnostico.BeforeTouchSize = new Size(412, 91);
            txtDiagnostico.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtDiagnostico.Location = new Point(151, 135);
            txtDiagnostico.Margin = new Padding(3, 4, 3, 4);
            txtDiagnostico.Multiline = true;
            txtDiagnostico.Name = "txtDiagnostico";
            txtDiagnostico.ScrollBars = ScrollBars.Vertical;
            txtDiagnostico.Size = new Size(412, 47);
            txtDiagnostico.TabIndex = 2;
            // 
            // txtTratamento
            // 
            txtTratamento.BeforeTouchSize = new Size(412, 91);
            txtTratamento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtTratamento.Location = new Point(151, 190);
            txtTratamento.Margin = new Padding(3, 4, 3, 4);
            txtTratamento.Multiline = true;
            txtTratamento.Name = "txtTratamento";
            txtTratamento.ScrollBars = ScrollBars.Vertical;
            txtTratamento.Size = new Size(412, 91);
            txtTratamento.TabIndex = 3;
            // 
            // txtID
            // 
            txtID.BackColor = Color.DarkGray;
            txtID.BeforeTouchSize = new Size(412, 91);
            txtID.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtID.ForeColor = Color.Black;
            txtID.Location = new Point(151, 7);
            txtID.Margin = new Padding(3, 4, 3, 4);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(70, 27);
            txtID.TabIndex = 11;
            txtID.Tag = "13";
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(477, 351);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 15;
            btnClear.Text = "Clear";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Red;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources._678080_shield_error_32;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(282, 351);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.ForestGreen;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.save32;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(154, 351);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "Update";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.FlatStyle = FlatStyle.Flat;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(26, 351);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 12;
            btnInsert.Text = "Add";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = true;
            btnInsert.Click += btnInsert_Click;
            // 
            // dgvAppts
            // 
            dgvAppts.AllowUserToAddRows = false;
            dgvAppts.AllowUserToDeleteRows = false;
            dgvAppts.AllowUserToOrderColumns = true;
            dgvAppts.AllowUserToResizeColumns = false;
            dgvAppts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAppts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAppts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppts.Columns.AddRange(new DataGridViewColumn[] { ID, Nome, DataConsulta, Moitvo, Diagnostico, Tratamento, PetId });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Gray;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvAppts.DefaultCellStyle = dataGridViewCellStyle3;
            dgvAppts.Location = new Point(25, 414);
            dgvAppts.Margin = new Padding(3, 4, 3, 4);
            dgvAppts.Name = "dgvAppts";
            dgvAppts.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvAppts.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvAppts.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAppts.RowTemplate.DividerHeight = 1;
            dgvAppts.RowTemplate.Height = 32;
            dgvAppts.ScrollBars = ScrollBars.Vertical;
            dgvAppts.Size = new Size(910, 146);
            dgvAppts.TabIndex = 16;
            dgvAppts.CellClick += dgvAppts_CellClick;
            dgvAppts.ColumnHeaderMouseClick += dgvAppts_ColumnHeaderMouseClick;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Visible = false;
            // 
            // Nome
            // 
            Nome.DataPropertyName = "Nome";
            Nome.HeaderText = "Nome";
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            Nome.Visible = false;
            Nome.Width = 150;
            // 
            // DataConsulta
            // 
            DataConsulta.DataPropertyName = "DataConsulta";
            DataConsulta.HeaderText = "Data";
            DataConsulta.Name = "DataConsulta";
            DataConsulta.ReadOnly = true;
            DataConsulta.Resizable = DataGridViewTriState.False;
            DataConsulta.Width = 110;
            // 
            // Moitvo
            // 
            Moitvo.DataPropertyName = "Motivo";
            Moitvo.HeaderText = "Motivo";
            Moitvo.Name = "Moitvo";
            Moitvo.ReadOnly = true;
            Moitvo.Resizable = DataGridViewTriState.False;
            Moitvo.Width = 350;
            // 
            // Diagnostico
            // 
            Diagnostico.DataPropertyName = "Diagnostico";
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Diagnostico.DefaultCellStyle = dataGridViewCellStyle2;
            Diagnostico.HeaderText = "Diagnóstico";
            Diagnostico.Name = "Diagnostico";
            Diagnostico.ReadOnly = true;
            Diagnostico.Resizable = DataGridViewTriState.False;
            Diagnostico.Width = 400;
            // 
            // Tratamento
            // 
            Tratamento.DataPropertyName = "Tratamento";
            Tratamento.HeaderText = "Tratamento";
            Tratamento.Name = "Tratamento";
            Tratamento.ReadOnly = true;
            Tratamento.Resizable = DataGridViewTriState.False;
            Tratamento.Visible = false;
            Tratamento.Width = 300;
            // 
            // PetId
            // 
            PetId.DataPropertyName = "IdPet";
            PetId.HeaderText = "IdPet";
            PetId.Name = "PetId";
            PetId.ReadOnly = true;
            PetId.Resizable = DataGridViewTriState.False;
            PetId.Visible = false;
            // 
            // frmPetVeterinaryAppointments
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.Teal;
            CaptionBarHeight = 51;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = (Image)resources.GetObject("captionImage1.Image");
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(36, 36);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(60, 2);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(60, 25);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(200, 24);
            captionLabel2.Text = "Consultas no veterinário";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(947, 552);
            Controls.Add(dgvAppts);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(gradientPanel1);
            ForeColor = Color.Gray;
            Name = "frmPetVeterinaryAppointments";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).EndInit();
            gradientPanel1.ResumeLayout(false);
            gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtPetName).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIdPet).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMotivo).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDiagnostico).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTratamento).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtID).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAppts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtIdPet;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtMotivo;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtDiagnostico;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtTratamento;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtID;
        private DateTimePicker dtpApptDate;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private DataGridView dgvAppts;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn DataConsulta;
        private DataGridViewTextBoxColumn Moitvo;
        private DataGridViewTextBoxColumn Diagnostico;
        private DataGridViewTextBoxColumn Tratamento;
        private DataGridViewTextBoxColumn PetId;
    }
}