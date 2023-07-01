namespace DaisyPets.UI.LookupTables
{
    partial class frmLookupTablesManager
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
            panel1 = new Panel();
            txtId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            label1 = new Label();
            txtDescricao = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            dgvLookupTables = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Descricao = new DataGridViewTextBoxColumn();
            lblTableName = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDescricao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLookupTables).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtDescricao);
            panel1.Location = new Point(40, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(490, 52);
            panel1.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.BeforeTouchSize = new Size(347, 29);
            txtId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(461, 14);
            txtId.Name = "txtId";
            txtId.Size = new Size(26, 27);
            txtId.TabIndex = 2;
            txtId.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 17);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 0;
            label1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.BeforeTouchSize = new Size(347, 29);
            txtDescricao.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescricao.Location = new Point(111, 14);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(347, 29);
            txtDescricao.TabIndex = 1;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(432, 141);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 39;
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
            btnDelete.Location = new Point(288, 141);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 38;
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
            btnUpdate.Location = new Point(164, 141);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 37;
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
            btnInsert.Location = new Point(40, 141);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 36;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // dgvLookupTables
            // 
            dgvLookupTables.AllowUserToAddRows = false;
            dgvLookupTables.AllowUserToDeleteRows = false;
            dgvLookupTables.AllowUserToOrderColumns = true;
            dgvLookupTables.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dgvLookupTables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvLookupTables.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvLookupTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvLookupTables.ColumnHeadersHeight = 40;
            dgvLookupTables.Columns.AddRange(new DataGridViewColumn[] { Id, Descricao });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvLookupTables.DefaultCellStyle = dataGridViewCellStyle3;
            dgvLookupTables.Location = new Point(40, 217);
            dgvLookupTables.MultiSelect = false;
            dgvLookupTables.Name = "dgvLookupTables";
            dgvLookupTables.ReadOnly = true;
            dgvLookupTables.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvLookupTables.RowHeadersWidth = 45;
            dgvLookupTables.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvLookupTables.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvLookupTables.RowTemplate.DividerHeight = 1;
            dgvLookupTables.RowTemplate.Height = 28;
            dgvLookupTables.RowTemplate.ReadOnly = true;
            dgvLookupTables.RowTemplate.Resizable = DataGridViewTriState.True;
            dgvLookupTables.ScrollBars = ScrollBars.Vertical;
            dgvLookupTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLookupTables.Size = new Size(545, 263);
            dgvLookupTables.TabIndex = 40;
            dgvLookupTables.CellClick += dgvLookupTables_CellClick;
            dgvLookupTables.ColumnHeaderMouseClick += dgvLookupTables_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // Descricao
            // 
            Descricao.DataPropertyName = "Descricao";
            Descricao.HeaderText = "Descrição";
            Descricao.Name = "Descricao";
            Descricao.ReadOnly = true;
            Descricao.Width = 480;
            // 
            // lblTableName
            // 
            lblTableName.AutoSize = true;
            lblTableName.BorderStyle = BorderStyle.FixedSingle;
            lblTableName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTableName.Location = new Point(254, 18);
            lblTableName.Name = "lblTableName";
            lblTableName.Size = new Size(97, 27);
            lblTableName.TabIndex = 41;
            lblTableName.Text = "Descrição";
            lblTableName.TextAlign = ContentAlignment.TopCenter;
            // 
            // frmLookupTablesManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            CaptionBarColor = Color.Khaki;
            CaptionBarHeight = 60;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.database_px_png;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(70, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(70, 28);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(300, 24);
            captionLabel2.Text = "Manutenção de tabelas auxiliares";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(624, 492);
            Controls.Add(lblTableName);
            Controls.Add(dgvLookupTables);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLookupTablesManager";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtId).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDescricao).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLookupTables).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtDescricao;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private DataGridView dgvLookupTables;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtId;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Descricao;
        private Label lblTableName;
    }
}