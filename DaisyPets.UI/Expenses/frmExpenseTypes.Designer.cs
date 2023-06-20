namespace DaisyPets.UI.Expenses
{
    partial class frmExpenseTypes
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
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            dgvTipoDespesas = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            CategoriaDespesa = new DataGridViewTextBoxColumn();
            Descricao = new DataGridViewTextBoxColumn();
            IdCategoriaDespesa = new DataGridViewTextBoxColumn();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            panel1 = new Panel();
            txtId = new TextBox();
            lblDescricao = new Label();
            label1 = new Label();
            txtDescricao = new TextBox();
            cboCategories = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvTipoDespesas).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTipoDespesas
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dgvTipoDespesas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvTipoDespesas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTipoDespesas.ColumnHeadersHeight = 32;
            dgvTipoDespesas.Columns.AddRange(new DataGridViewColumn[] { Id, CategoriaDespesa, Descricao, IdCategoriaDespesa });
            dgvTipoDespesas.Location = new Point(21, 221);
            dgvTipoDespesas.Name = "dgvTipoDespesas";
            dgvTipoDespesas.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvTipoDespesas.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTipoDespesas.RowTemplate.DividerHeight = 1;
            dgvTipoDespesas.RowTemplate.Height = 32;
            dgvTipoDespesas.RowTemplate.Resizable = DataGridViewTriState.True;
            dgvTipoDespesas.Size = new Size(651, 338);
            dgvTipoDespesas.TabIndex = 2;
            dgvTipoDespesas.CellClick += dgvTipoDespesas_CellClick;
            dgvTipoDespesas.ColumnHeaderMouseClick += dgvTipoDespesas_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // CategoriaDespesa
            // 
            CategoriaDespesa.DataPropertyName = "CategoriaDespesa";
            CategoriaDespesa.HeaderText = "Categoria";
            CategoriaDespesa.Name = "CategoriaDespesa";
            CategoriaDespesa.ReadOnly = true;
            CategoriaDespesa.Width = 300;
            // 
            // Descricao
            // 
            Descricao.DataPropertyName = "Descricao";
            Descricao.HeaderText = "Descrição";
            Descricao.Name = "Descricao";
            Descricao.ReadOnly = true;
            Descricao.Width = 300;
            // 
            // IdCategoriaDespesa
            // 
            IdCategoriaDespesa.HeaderText = "IdCategoriaDespesa";
            IdCategoriaDespesa.Name = "IdCategoriaDespesa";
            IdCategoriaDespesa.ReadOnly = true;
            IdCategoriaDespesa.Visible = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(470, 159);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 29;
            btnClear.Text = "Clear ";
            btnClear.TextAlign = ContentAlignment.MiddleRight;
            btnClear.UseVisualStyleBackColor = false;
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
            btnDelete.Location = new Point(267, 159);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 28;
            btnDelete.Text = "Delete ";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.UseVisualStyleBackColor = false;
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
            btnUpdate.Location = new Point(143, 159);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 27;
            btnUpdate.Text = "Update ";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = false;
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
            btnInsert.Location = new Point(19, 159);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 26;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(lblDescricao);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtDescricao);
            panel1.Controls.Add(cboCategories);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 130);
            panel1.TabIndex = 30;
            // 
            // txtId
            // 
            txtId.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(9, 3);
            txtId.Name = "txtId";
            txtId.Size = new Size(30, 29);
            txtId.TabIndex = 9;
            txtId.Visible = false;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblDescricao.Location = new Point(32, 78);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(77, 21);
            lblDescricao.TabIndex = 8;
            lblDescricao.Text = "Descrição";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(32, 30);
            label1.Name = "label1";
            label1.Size = new Size(137, 21);
            label1.TabIndex = 7;
            label1.Text = "Categoria despesa";
            // 
            // txtDescricao
            // 
            txtDescricao.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescricao.Location = new Point(201, 75);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(235, 29);
            txtDescricao.TabIndex = 6;
            // 
            // cboCategories
            // 
            cboCategories.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboCategories.FormattingEnabled = true;
            cboCategories.Location = new Point(201, 27);
            cboCategories.Name = "cboCategories";
            cboCategories.Size = new Size(235, 29);
            cboCategories.TabIndex = 5;
            cboCategories.SelectedIndexChanged += cboCategories_SelectedIndexChanged;
            // 
            // frmExpenseTypes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.Khaki;
            CaptionBarHeight = 60;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.iconfinder_71_5027865;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(70, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(70, 26);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(200, 24);
            captionLabel2.Text = "Tipo de Despesas";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(696, 571);
            Controls.Add(panel1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(dgvTipoDespesas);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmExpenseTypes";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvTipoDespesas).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvTipoDespesas;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private Panel panel1;
        private Label lblDescricao;
        private Label label1;
        private TextBox txtDescricao;
        private ComboBox cboCategories;
        private TextBox txtId;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn CategoriaDespesa;
        private DataGridViewTextBoxColumn Descricao;
        private DataGridViewTextBoxColumn IdCategoriaDespesa;
    }
}