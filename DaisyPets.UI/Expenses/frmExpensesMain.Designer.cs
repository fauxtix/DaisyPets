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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
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
            autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            filterFromDateTime = new DateTimePicker();
            filterToDateTime = new DateTimePicker();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            radioButtonAll = new RadioButton();
            radioButtonExpenses = new RadioButton();
            radioButtonIncome = new RadioButton();
            autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            comboBoxCategories = new ComboBox();
            labelSummary = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            labelVisibleSummary = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvExpenses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkBoxFilterDateTime).BeginInit();
            SuspendLayout();
            // 
            // dgvExpenses
            // 
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.AllowUserToDeleteRows = false;
            dgvExpenses.AllowUserToOrderColumns = true;
            dgvExpenses.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvExpenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvExpenses.ColumnHeadersHeight = 40;
            dgvExpenses.Columns.AddRange(new DataGridViewColumn[] { Id, DataMovimento, ValorPago, Descricao, DescricaoCategoriaDespesa, DescricaoTipoDespesa, TipoMovimento });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvExpenses.DefaultCellStyle = dataGridViewCellStyle5;
            dgvExpenses.Location = new Point(22, 12);
            dgvExpenses.Name = "dgvExpenses";
            dgvExpenses.ReadOnly = true;
            dgvExpenses.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvExpenses.RowTemplate.Height = 25;
            dgvExpenses.Size = new Size(885, 409);
            dgvExpenses.TabIndex = 0;
            dgvExpenses.CellDoubleClick += dgvExpenses_CellDoubleClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // DataMovimento
            // 
            DataMovimento.DataPropertyName = "DataMovimento";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataMovimento.DefaultCellStyle = dataGridViewCellStyle2;
            DataMovimento.HeaderText = "Data";
            DataMovimento.Name = "DataMovimento";
            DataMovimento.ReadOnly = true;
            // 
            // ValorPago
            // 
            ValorPago.DataPropertyName = "ValorPago";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ValorPago.DefaultCellStyle = dataGridViewCellStyle3;
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
            Descricao.Width = 200;
            // 
            // DescricaoCategoriaDespesa
            // 
            DescricaoCategoriaDespesa.DataPropertyName = "DescricaoCategoriaDespesa";
            DescricaoCategoriaDespesa.HeaderText = "Categoria";
            DescricaoCategoriaDespesa.Name = "DescricaoCategoriaDespesa";
            DescricaoCategoriaDespesa.ReadOnly = true;
            DescricaoCategoriaDespesa.Width = 150;
            // 
            // DescricaoTipoDespesa
            // 
            DescricaoTipoDespesa.DataPropertyName = "DescricaoTipoDespesa";
            DescricaoTipoDespesa.HeaderText = "Tipo despesa";
            DescricaoTipoDespesa.Name = "DescricaoTipoDespesa";
            DescricaoTipoDespesa.ReadOnly = true;
            DescricaoTipoDespesa.Width = 150;
            // 
            // TipoMovimento
            // 
            TipoMovimento.DataPropertyName = "TipoMovimento";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TipoMovimento.DefaultCellStyle = dataGridViewCellStyle4;
            TipoMovimento.HeaderText = "Tipo Movimento";
            TipoMovimento.Name = "TipoMovimento";
            TipoMovimento.ReadOnly = true;
            TipoMovimento.Width = 150;
            // 
            // lblFilter
            // 
            lblFilter.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblFilter.Location = new Point(938, 12);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(45, 17);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Filtros";
            // 
            // checkBoxFilterDateTime
            // 
            checkBoxFilterDateTime.BeforeTouchSize = new Size(123, 21);
            checkBoxFilterDateTime.Location = new Point(938, 47);
            checkBoxFilterDateTime.Name = "checkBoxFilterDateTime";
            checkBoxFilterDateTime.Size = new Size(123, 21);
            checkBoxFilterDateTime.TabIndex = 2;
            checkBoxFilterDateTime.Text = "Filter date range.";
            // 
            // autoLabel1
            // 
            autoLabel1.Location = new Point(938, 83);
            autoLabel1.Name = "autoLabel1";
            autoLabel1.Size = new Size(62, 15);
            autoLabel1.TabIndex = 3;
            autoLabel1.Text = "A partir de";
            // 
            // filterFromDateTime
            // 
            filterFromDateTime.CalendarFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            filterFromDateTime.Enabled = false;
            filterFromDateTime.Location = new Point(938, 101);
            filterFromDateTime.Name = "filterFromDateTime";
            filterFromDateTime.Size = new Size(222, 23);
            filterFromDateTime.TabIndex = 0;
            filterFromDateTime.Value = new DateTime(2023, 6, 11, 0, 0, 0, 0);
            // 
            // filterToDateTime
            // 
            filterToDateTime.CalendarFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            filterToDateTime.Enabled = false;
            filterToDateTime.Location = new Point(938, 158);
            filterToDateTime.Name = "filterToDateTime";
            filterToDateTime.Size = new Size(222, 23);
            filterToDateTime.TabIndex = 4;
            filterToDateTime.Value = new DateTime(2099, 12, 31, 0, 0, 0, 0);
            // 
            // autoLabel2
            // 
            autoLabel2.Location = new Point(938, 140);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(25, 15);
            autoLabel2.TabIndex = 5;
            autoLabel2.Text = "Até";
            // 
            // autoLabel3
            // 
            autoLabel3.Location = new Point(938, 198);
            autoLabel3.Name = "autoLabel3";
            autoLabel3.Size = new Size(78, 15);
            autoLabel3.TabIndex = 6;
            autoLabel3.Text = "Filtra por tipo";
            // 
            // radioButtonAll
            // 
            radioButtonAll.AutoSize = true;
            radioButtonAll.Checked = true;
            radioButtonAll.Location = new Point(938, 226);
            radioButtonAll.Name = "radioButtonAll";
            radioButtonAll.Size = new Size(56, 19);
            radioButtonAll.TabIndex = 7;
            radioButtonAll.TabStop = true;
            radioButtonAll.Text = "Todos";
            radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonExpenses
            // 
            radioButtonExpenses.AutoSize = true;
            radioButtonExpenses.Location = new Point(1080, 226);
            radioButtonExpenses.Name = "radioButtonExpenses";
            radioButtonExpenses.Size = new Size(73, 19);
            radioButtonExpenses.TabIndex = 8;
            radioButtonExpenses.Text = "Despesas";
            radioButtonExpenses.UseVisualStyleBackColor = true;
            // 
            // radioButtonIncome
            // 
            radioButtonIncome.AutoSize = true;
            radioButtonIncome.Location = new Point(996, 226);
            radioButtonIncome.Name = "radioButtonIncome";
            radioButtonIncome.Size = new Size(78, 19);
            radioButtonIncome.TabIndex = 9;
            radioButtonIncome.Text = "Donativos";
            radioButtonIncome.UseVisualStyleBackColor = true;
            // 
            // autoLabel4
            // 
            autoLabel4.Location = new Point(938, 260);
            autoLabel4.Name = "autoLabel4";
            autoLabel4.Size = new Size(108, 15);
            autoLabel4.TabIndex = 10;
            autoLabel4.Text = "Filtra por Categoria";
            // 
            // comboBoxCategories
            // 
            comboBoxCategories.FormattingEnabled = true;
            comboBoxCategories.Location = new Point(939, 282);
            comboBoxCategories.Name = "comboBoxCategories";
            comboBoxCategories.Size = new Size(221, 23);
            comboBoxCategories.TabIndex = 11;
            // 
            // labelSummary
            // 
            labelSummary.Location = new Point(22, 527);
            labelSummary.Name = "labelSummary";
            labelSummary.Size = new Size(58, 15);
            labelSummary.TabIndex = 14;
            labelSummary.Text = "Summary";
            // 
            // labelVisibleSummary
            // 
            labelVisibleSummary.Location = new Point(22, 554);
            labelVisibleSummary.Name = "labelVisibleSummary";
            labelVisibleSummary.Size = new Size(97, 15);
            labelVisibleSummary.TabIndex = 15;
            labelVisibleSummary.Text = "FilteredSummary";
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(444, 450);
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
            btnDelete.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources._678080_shield_error_32;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.Location = new Point(270, 450);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 28;
            btnDelete.Text = "Delete ";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.ForestGreen;
            btnUpdate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.save32;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(146, 450);
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
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(22, 450);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 26;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnInsert_Click;
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
            captionLabel2.Text = "Despesas / Donativos";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1184, 579);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(labelVisibleSummary);
            Controls.Add(labelSummary);
            Controls.Add(comboBoxCategories);
            Controls.Add(autoLabel4);
            Controls.Add(radioButtonIncome);
            Controls.Add(radioButtonExpenses);
            Controls.Add(radioButtonAll);
            Controls.Add(autoLabel3);
            Controls.Add(filterToDateTime);
            Controls.Add(autoLabel2);
            Controls.Add(filterFromDateTime);
            Controls.Add(autoLabel1);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvExpenses;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblFilter;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv checkBoxFilterDateTime;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private DateTimePicker filterFromDateTime;
        private DateTimePicker filterToDateTime;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private RadioButton radioButtonAll;
        private RadioButton radioButtonExpenses;
        private RadioButton radioButtonIncome;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private ComboBox comboBoxCategories;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelSummary;
        private Syncfusion.Windows.Forms.Tools.AutoLabel labelVisibleSummary;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn DataMovimento;
        private DataGridViewTextBoxColumn ValorPago;
        private DataGridViewTextBoxColumn Descricao;
        private DataGridViewTextBoxColumn DescricaoCategoriaDespesa;
        private DataGridViewTextBoxColumn DescricaoTipoDespesa;
        private DataGridViewTextBoxColumn TipoMovimento;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnInsert;
    }
}