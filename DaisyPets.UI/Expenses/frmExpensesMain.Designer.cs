namespace DaisyPets.UI.Expenses
{
    partial class frmExpensesMain
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpensesMain));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            dgvExpenses = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            DataMovimento = new DataGridViewTextBoxColumn();
            ValorPago = new DataGridViewTextBoxColumn();
            Descricao = new DataGridViewTextBoxColumn();
            DescricaoCategoriaDespesa = new DataGridViewTextBoxColumn();
            DescricaoTipoDespesa = new DataGridViewTextBoxColumn();
            TipoMovimento = new DataGridViewTextBoxColumn();
            lblFilter = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            checkBoxFilterDateTime = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            labelFilterFrom = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            filterFromDateTime = new DateTimePicker();
            filterToDateTime = new DateTimePicker();
            labelFilterTo = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            panel1 = new Panel();
            button1 = new Button();
            lblTotalFilteredExpenses = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            lblTotalExpenses = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            labelVisibleSummary = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            labelSummary = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            labelFilterByCategory = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            cboCategoryTypes = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkBoxFilterDateTime).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvExpenses
            // 
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AllowUserToDeleteRows = false;
            dgvExpenses.AllowUserToOrderColumns = true;
            dgvExpenses.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dgvExpenses.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvExpenses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvExpenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvExpenses.ColumnHeadersHeight = 40;
            dgvExpenses.Columns.AddRange(new DataGridViewColumn[] { Id, DataMovimento, ValorPago, Descricao, DescricaoCategoriaDespesa, DescricaoTipoDespesa, TipoMovimento });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Window;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgvExpenses.DefaultCellStyle = dataGridViewCellStyle7;
            dgvExpenses.Location = new Point(22, 12);
            dgvExpenses.MultiSelect = false;
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.ReadOnly = true;
            dgvExpenses.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvExpenses.RowHeadersWidth = 45;
            dgvExpenses.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvExpenses.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvExpenses.RowTemplate.DividerHeight = 1;
            dgvExpenses.RowTemplate.Height = 28;
            dgvExpenses.RowTemplate.ReadOnly = true;
            dgvExpenses.RowTemplate.Resizable = DataGridViewTriState.True;
            dgvExpenses.ScrollBars = ScrollBars.Vertical;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.Size = new Size(1069, 519);
            dgvExpenses.TabIndex = 0;
            dgvExpenses.CellClick += dgvExpenses_CellClick;
            dgvExpenses.CellDoubleClick += dgvExpenses_CellDoubleClick;
            dgvExpenses.ColumnHeaderMouseClick += dgvExpenses_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            Id.DefaultCellStyle = dataGridViewCellStyle3;
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 60;
            // 
            // DataMovimento
            // 
            DataMovimento.DataPropertyName = "DataMovimento";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataMovimento.DefaultCellStyle = dataGridViewCellStyle4;
            DataMovimento.HeaderText = "Data";
            DataMovimento.Name = "DataMovimento";
            DataMovimento.ReadOnly = true;
            // 
            // ValorPago
            // 
            ValorPago.DataPropertyName = "ValorPago";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            ValorPago.DefaultCellStyle = dataGridViewCellStyle5;
            ValorPago.HeaderText = "Valor";
            ValorPago.Name = "ValorPago";
            ValorPago.ReadOnly = true;
            ValorPago.Width = 80;
            // 
            // Descricao
            // 
            Descricao.DataPropertyName = "Descricao";
            Descricao.HeaderText = "Descricao";
            Descricao.Name = "Descricao";
            Descricao.ReadOnly = true;
            Descricao.Width = 300;
            // 
            // DescricaoCategoriaDespesa
            // 
            DescricaoCategoriaDespesa.DataPropertyName = "DescricaoCategoriaDespesa";
            DescricaoCategoriaDespesa.HeaderText = "Categoria";
            DescricaoCategoriaDespesa.Name = "DescricaoCategoriaDespesa";
            DescricaoCategoriaDespesa.ReadOnly = true;
            DescricaoCategoriaDespesa.Width = 220;
            // 
            // DescricaoTipoDespesa
            // 
            DescricaoTipoDespesa.DataPropertyName = "DescricaoTipoDespesa";
            DescricaoTipoDespesa.HeaderText = "Tipo despesa";
            DescricaoTipoDespesa.Name = "DescricaoTipoDespesa";
            DescricaoTipoDespesa.ReadOnly = true;
            DescricaoTipoDespesa.Width = 240;
            // 
            // TipoMovimento
            // 
            TipoMovimento.DataPropertyName = "TipoMovimento";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TipoMovimento.DefaultCellStyle = dataGridViewCellStyle6;
            TipoMovimento.HeaderText = "Tipo Movimento";
            TipoMovimento.Name = "TipoMovimento";
            TipoMovimento.ReadOnly = true;
            TipoMovimento.Visible = false;
            TipoMovimento.Width = 150;
            // 
            // lblFilter
            // 
            lblFilter.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblFilter.Location = new Point(1115, 12);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(53, 20);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Filtros";
            // 
            // checkBoxFilterDateTime
            // 
            checkBoxFilterDateTime.BeforeTouchSize = new Size(123, 21);
            checkBoxFilterDateTime.Location = new Point(1115, 47);
            checkBoxFilterDateTime.Name = "checkBoxFilterDateTime";
            checkBoxFilterDateTime.Size = new Size(123, 21);
            checkBoxFilterDateTime.TabIndex = 2;
            checkBoxFilterDateTime.Text = "Filter date range.";
            checkBoxFilterDateTime.CheckStateChanged += checkBoxFilterDateTime_CheckStateChanged;
            // 
            // labelFilterFrom
            // 
            labelFilterFrom.Location = new Point(1115, 83);
            labelFilterFrom.Name = "labelFilterFrom";
            labelFilterFrom.Size = new Size(62, 15);
            labelFilterFrom.TabIndex = 3;
            labelFilterFrom.Text = "A partir de";
            // 
            // filterFromDateTime
            // 
            filterFromDateTime.CalendarFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            filterFromDateTime.Enabled = false;
            filterFromDateTime.Location = new Point(1115, 101);
            filterFromDateTime.Name = "filterFromDateTime";
            filterFromDateTime.Size = new Size(222, 23);
            filterFromDateTime.TabIndex = 0;
            filterFromDateTime.Value = new DateTime(2023, 6, 1, 0, 0, 0, 0);
            filterFromDateTime.ValueChanged += filterFromDateTime_ValueChanged;
            // 
            // filterToDateTime
            // 
            filterToDateTime.CalendarFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            filterToDateTime.Enabled = false;
            filterToDateTime.Location = new Point(1115, 158);
            filterToDateTime.Name = "filterToDateTime";
            filterToDateTime.Size = new Size(222, 23);
            filterToDateTime.TabIndex = 4;
            filterToDateTime.Value = new DateTime(2023, 6, 30, 0, 0, 0, 0);
            filterToDateTime.ValueChanged += filterToDateTime_ValueChanged;
            // 
            // labelFilterTo
            // 
            labelFilterTo.Location = new Point(1115, 140);
            labelFilterTo.Name = "labelFilterTo";
            labelFilterTo.Size = new Size(25, 15);
            labelFilterTo.TabIndex = 5;
            labelFilterTo.Text = "Até";
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(lblTotalFilteredExpenses);
            panel1.Controls.Add(lblTotalExpenses);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(btnInsert);
            panel1.Controls.Add(labelVisibleSummary);
            panel1.Controls.Add(labelSummary);
            panel1.Location = new Point(22, 549);
            panel1.Name = "panel1";
            panel1.Size = new Size(1069, 143);
            panel1.TabIndex = 12;
            // 
            // button1
            // 
            button1.BackColor = Color.LightSalmon;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.Black;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(897, 12);
            button1.Name = "button1";
            button1.Size = new Size(150, 39);
            button1.TabIndex = 38;
            button1.Text = "Calculadora";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // lblTotalFilteredExpenses
            // 
            lblTotalFilteredExpenses.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalFilteredExpenses.Location = new Point(157, 116);
            lblTotalFilteredExpenses.Name = "lblTotalFilteredExpenses";
            lblTotalFilteredExpenses.Size = new Size(0, 20);
            lblTotalFilteredExpenses.TabIndex = 37;
            // 
            // lblTotalExpenses
            // 
            lblTotalExpenses.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalExpenses.Location = new Point(157, 89);
            lblTotalExpenses.Name = "lblTotalExpenses";
            lblTotalExpenses.Size = new Size(0, 20);
            lblTotalExpenses.TabIndex = 36;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(339, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 35;
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
            btnDelete.Location = new Point(137, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 34;
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
            btnUpdate.Location = new Point(614, 12);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 33;
            btnUpdate.Text = "Update ";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Visible = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(16, 12);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 32;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
            // 
            // labelVisibleSummary
            // 
            labelVisibleSummary.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelVisibleSummary.Location = new Point(16, 116);
            labelVisibleSummary.Name = "labelVisibleSummary";
            labelVisibleSummary.Size = new Size(78, 20);
            labelVisibleSummary.TabIndex = 31;
            labelVisibleSummary.Text = "Total filtro";
            // 
            // labelSummary
            // 
            labelSummary.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelSummary.Location = new Point(16, 89);
            labelSummary.Name = "labelSummary";
            labelSummary.Size = new Size(127, 20);
            labelSummary.TabIndex = 30;
            labelSummary.Text = "Total de despesas";
            // 
            // labelFilterByCategory
            // 
            labelFilterByCategory.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelFilterByCategory.Location = new Point(1115, 235);
            labelFilterByCategory.Name = "labelFilterByCategory";
            labelFilterByCategory.Size = new Size(75, 20);
            labelFilterByCategory.TabIndex = 13;
            labelFilterByCategory.Text = "Categoria";
            // 
            // cboCategoryTypes
            // 
            cboCategoryTypes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboCategoryTypes.FormattingEnabled = true;
            cboCategoryTypes.Location = new Point(1115, 259);
            cboCategoryTypes.Name = "cboCategoryTypes";
            cboCategoryTypes.Size = new Size(227, 28);
            cboCategoryTypes.TabIndex = 14;
            cboCategoryTypes.SelectedIndexChanged += cboCategoryTypes_SelectedIndexChanged;
            // 
            // frmExpensesMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.SteelBlue;
            CaptionBarHeight = 60;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.iconfinder_pay_credit_card_expenditure_bill_payment_method_banking_4288566;
            captionImage1.Location = new Point(4, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(60, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(60, 28);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(200, 24);
            captionLabel2.Text = "Gestão de Despesas";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1350, 696);
            Controls.Add(cboCategoryTypes);
            Controls.Add(labelFilterByCategory);
            Controls.Add(panel1);
            Controls.Add(filterToDateTime);
            Controls.Add(labelFilterTo);
            Controls.Add(filterFromDateTime);
            Controls.Add(labelFilterFrom);
            Controls.Add(checkBoxFilterDateTime);
            Controls.Add(lblFilter);
            Controls.Add(dgvExpenses);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmExpensesMain";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).EndInit();
            ((System.ComponentModel.ISupportInitialize)checkBoxFilterDateTime).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvExpenses;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblFilter;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxFilterDateTime;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelFilterFrom;
        private DateTimePicker filterFromDateTime;
        private DateTimePicker filterToDateTime;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelFilterTo;
        private Panel panel1;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelVisibleSummary;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelSummary;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelFilterByCategory;
        private ComboBox cboCategoryTypes;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblTotalFilteredExpenses;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblTotalExpenses;
        private Button button1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn DataMovimento;
        private DataGridViewTextBoxColumn ValorPago;
        private DataGridViewTextBoxColumn Descricao;
        private DataGridViewTextBoxColumn DescricaoCategoriaDespesa;
        private DataGridViewTextBoxColumn DescricaoTipoDespesa;
        private DataGridViewTextBoxColumn TipoMovimento;
    }
}