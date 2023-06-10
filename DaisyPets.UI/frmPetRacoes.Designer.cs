namespace DaisyPets.UI
{
    partial class frmPetRacoes
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetRacoes));
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            nupQtdDiaria = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            dtpBuyDate = new DateTimePicker();
            txtIdPet = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtMarca = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtID = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            dgvRacoes = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            NomePet = new DataGridViewTextBoxColumn();
            DataCompra = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            QuantidadeDiaria = new DataGridViewTextBoxColumn();
            PetId = new DataGridViewTextBoxColumn();
            btnInfo = new Button();
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).BeginInit();
            gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nupQtdDiaria).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIdPet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRacoes).BeginInit();
            SuspendLayout();
            // 
            // gradientPanel1
            // 
            gradientPanel1.BackColor = Color.Gainsboro;
            gradientPanel1.BorderStyle = BorderStyle.FixedSingle;
            gradientPanel1.Controls.Add(btnInfo);
            gradientPanel1.Controls.Add(autoLabel4);
            gradientPanel1.Controls.Add(nupQtdDiaria);
            gradientPanel1.Controls.Add(autoLabel2);
            gradientPanel1.Controls.Add(txtPetName);
            gradientPanel1.Controls.Add(dtpBuyDate);
            gradientPanel1.Controls.Add(txtIdPet);
            gradientPanel1.Controls.Add(autoLabel6);
            gradientPanel1.Controls.Add(autoLabel5);
            gradientPanel1.Controls.Add(autoLabel1);
            gradientPanel1.Controls.Add(txtMarca);
            gradientPanel1.Controls.Add(txtID);
            gradientPanel1.Location = new Point(24, 38);
            gradientPanel1.Margin = new Padding(3, 4, 3, 4);
            gradientPanel1.Name = "gradientPanel1";
            gradientPanel1.Size = new Size(557, 181);
            gradientPanel1.TabIndex = 12;
            // 
            // autoLabel4
            // 
            autoLabel4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            autoLabel4.Location = new Point(18, 136);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(129, 20);
            autoLabel4.TabIndex = 31;
            autoLabel4.Text = "Quantidade diária";
            // 
            // nupQtdDiaria
            // 
            nupQtdDiaria.BeforeTouchSize = new Size(53, 27);
            nupQtdDiaria.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            nupQtdDiaria.Location = new Point(155, 134);
            nupQtdDiaria.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            nupQtdDiaria.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nupQtdDiaria.Name = "nupQtdDiaria";
            nupQtdDiaria.Size = new Size(53, 27);
            nupQtdDiaria.TabIndex = 30;
            nupQtdDiaria.Value = new decimal(new int[] { 1, 0, 0, 0 });
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
            txtPetName.BeforeTouchSize = new Size(70, 27);
            txtPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetName.Location = new Point(155, 37);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(273, 27);
            txtPetName.TabIndex = 28;
            // 
            // dtpBuyDate
            // 
            dtpBuyDate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpBuyDate.Format = DateTimePickerFormat.Short;
            dtpBuyDate.Location = new Point(155, 70);
            dtpBuyDate.Name = "dtpBuyDate";
            dtpBuyDate.Size = new Size(115, 27);
            dtpBuyDate.TabIndex = 27;
            // 
            // txtIdPet
            // 
            txtIdPet.BeforeTouchSize = new Size(70, 27);
            txtIdPet.Location = new Point(367, 7);
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
            autoLabel6.Size = new Size(97, 20);
            autoLabel6.TabIndex = 23;
            autoLabel6.Text = "Data compra";
            // 
            // autoLabel5
            // 
            autoLabel5.BackColor = Color.Gainsboro;
            autoLabel5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel5.ForeColor = SystemColors.ControlText;
            autoLabel5.Location = new Point(18, 106);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(52, 20);
            autoLabel5.TabIndex = 22;
            autoLabel5.Text = "Marca";
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
            // txtMarca
            // 
            txtMarca.BeforeTouchSize = new Size(70, 27);
            txtMarca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtMarca.Location = new Point(155, 100);
            txtMarca.Margin = new Padding(3, 4, 3, 4);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(273, 27);
            txtMarca.TabIndex = 1;
            // 
            // txtID
            // 
            txtID.BackColor = Color.DarkGray;
            txtID.BeforeTouchSize = new Size(70, 27);
            txtID.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtID.ForeColor = Color.Black;
            txtID.Location = new Point(155, 4);
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
            btnClear.Location = new Point(505, 246);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 19;
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
            btnDelete.Location = new Point(281, 246);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 18;
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
            btnUpdate.Location = new Point(153, 246);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 17;
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
            btnInsert.Location = new Point(25, 246);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 16;
            btnInsert.Text = "Add";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = true;
            btnInsert.Click += btnInsert_Click;
            // 
            // dgvRacoes
            // 
            dgvRacoes.AllowUserToAddRows = false;
            dgvRacoes.AllowUserToDeleteRows = false;
            dgvRacoes.AllowUserToOrderColumns = true;
            dgvRacoes.AllowUserToResizeColumns = false;
            dgvRacoes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvRacoes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvRacoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRacoes.Columns.AddRange(new DataGridViewColumn[] { ID, NomePet, DataCompra, Marca, QuantidadeDiaria, PetId });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Gray;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvRacoes.DefaultCellStyle = dataGridViewCellStyle5;
            dgvRacoes.Location = new Point(24, 309);
            dgvRacoes.Margin = new Padding(3, 4, 3, 4);
            dgvRacoes.Name = "dgvRacoes";
            dgvRacoes.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvRacoes.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvRacoes.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRacoes.RowTemplate.DividerHeight = 1;
            dgvRacoes.RowTemplate.Height = 32;
            dgvRacoes.ScrollBars = ScrollBars.Vertical;
            dgvRacoes.Size = new Size(579, 134);
            dgvRacoes.TabIndex = 20;
            dgvRacoes.CellClick += dgvRacoes_CellClick;
            dgvRacoes.ColumnHeaderMouseClick += dgvRacoes_ColumnHeaderMouseClick;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Visible = false;
            // 
            // NomePet
            // 
            NomePet.DataPropertyName = "NomePet";
            NomePet.HeaderText = "Nome";
            NomePet.Name = "NomePet";
            NomePet.ReadOnly = true;
            NomePet.Visible = false;
            NomePet.Width = 150;
            // 
            // DataCompra
            // 
            DataCompra.DataPropertyName = "DataCompra";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataCompra.DefaultCellStyle = dataGridViewCellStyle2;
            DataCompra.HeaderText = "Data";
            DataCompra.Name = "DataCompra";
            DataCompra.ReadOnly = true;
            DataCompra.Resizable = DataGridViewTriState.False;
            // 
            // Marca
            // 
            Marca.DataPropertyName = "Marca";
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            Marca.DefaultCellStyle = dataGridViewCellStyle3;
            Marca.HeaderText = "Marca";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            Marca.Resizable = DataGridViewTriState.False;
            Marca.Width = 310;
            // 
            // QuantidadeDiaria
            // 
            QuantidadeDiaria.DataPropertyName = "QuantidadeDiaria";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            QuantidadeDiaria.DefaultCellStyle = dataGridViewCellStyle4;
            QuantidadeDiaria.HeaderText = "Qtd. Diária";
            QuantidadeDiaria.Name = "QuantidadeDiaria";
            QuantidadeDiaria.ReadOnly = true;
            QuantidadeDiaria.Resizable = DataGridViewTriState.False;
            QuantidadeDiaria.Width = 120;
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
            // btnInfo
            // 
            btnInfo.BackColor = Color.White;
            btnInfo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInfo.ForeColor = Color.Black;
            btnInfo.Image = Properties.Resources.Info_32;
            btnInfo.ImageAlign = ContentAlignment.MiddleLeft;
            btnInfo.Location = new Point(460, 93);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(82, 40);
            btnInfo.TabIndex = 32;
            btnInfo.Text = "Info";
            btnInfo.TextAlign = ContentAlignment.MiddleRight;
            btnInfo.UseVisualStyleBackColor = false;
            btnInfo.Click += btnInfo_Click;
            // 
            // frmPetRacoes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.MistyRose;
            CaptionBarHeight = 51;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = (Image)resources.GetObject("captionImage1.Image");
            captionImage1.Location = new Point(8, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel1.Location = new Point(70, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(70, 24);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Text = "Rações";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(627, 443);
            Controls.Add(dgvRacoes);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(gradientPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPetRacoes";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)gradientPanel1).EndInit();
            gradientPanel1.ResumeLayout(false);
            gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nupQtdDiaria).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPetName).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIdPet).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtMarca).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtID).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRacoes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
        private DateTimePicker dtpBuyDate;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtIdPet;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtMarca;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtID;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private DataGridView dgvRacoes;
        private DataGridViewTextBoxColumn DataConsulta;
        private DataGridViewTextBoxColumn Moitvo;
        private DataGridViewTextBoxColumn Diagnostico;
        private DataGridViewTextBoxColumn Tratamento;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.NumericUpDownExt nupQtdDiaria;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn NomePet;
        private DataGridViewTextBoxColumn DataCompra;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn QuantidadeDiaria;
        private DataGridViewTextBoxColumn PetId;
        private Button btnInfo;
    }
}