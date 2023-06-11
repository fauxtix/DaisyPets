namespace DaisyPets.UI.Expenses
{
    partial class frmNewEntry
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
            labelDescription = new Label();
            txtDescricao = new TextBox();
            labelValue = new Label();
            txtValor = new TextBox();
            labelCategory = new Label();
            cboCategoriasDespesas = new ComboBox();
            btnAddEdit = new Button();
            cboTipoDespesa = new ComboBox();
            labelTipoDespesa = new Label();
            radioButtonIncome = new RadioButton();
            radioButtonExpenses = new RadioButton();
            dtpDataMovimento = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            txtId = new TextBox();
            SuspendLayout();
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Location = new Point(12, 79);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(58, 15);
            labelDescription.TabIndex = 2;
            labelDescription.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(12, 99);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(299, 23);
            txtDescricao.TabIndex = 3;
            // 
            // labelValue
            // 
            labelValue.AutoSize = true;
            labelValue.Location = new Point(12, 184);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(41, 15);
            labelValue.TabIndex = 4;
            labelValue.Text = "* Valor";
            // 
            // txtValor
            // 
            txtValor.Location = new Point(12, 203);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(111, 23);
            txtValor.TabIndex = 5;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Location = new Point(12, 239);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(111, 15);
            labelCategory.TabIndex = 6;
            labelCategory.Text = "* Categoria despesa";
            // 
            // cboCategoriasDespesas
            // 
            cboCategoriasDespesas.FormattingEnabled = true;
            cboCategoriasDespesas.Location = new Point(12, 257);
            cboCategoriasDespesas.Name = "cboCategoriasDespesas";
            cboCategoriasDespesas.Size = new Size(299, 23);
            cboCategoriasDespesas.TabIndex = 7;
            // 
            // btnAddEdit
            // 
            btnAddEdit.Location = new Point(12, 342);
            btnAddEdit.Name = "btnAddEdit";
            btnAddEdit.Size = new Size(299, 78);
            btnAddEdit.TabIndex = 8;
            btnAddEdit.UseVisualStyleBackColor = true;
            btnAddEdit.Click += buttonAddNewEntry_Click;
            // 
            // cboTipoDespesa
            // 
            cboTipoDespesa.FormattingEnabled = true;
            cboTipoDespesa.Location = new Point(12, 311);
            cboTipoDespesa.Name = "cboTipoDespesa";
            cboTipoDespesa.Size = new Size(299, 23);
            cboTipoDespesa.TabIndex = 10;
            // 
            // labelTipoDespesa
            // 
            labelTipoDespesa.AutoSize = true;
            labelTipoDespesa.Location = new Point(12, 292);
            labelTipoDespesa.Name = "labelTipoDespesa";
            labelTipoDespesa.Size = new Size(83, 15);
            labelTipoDespesa.TabIndex = 9;
            labelTipoDespesa.Text = "* Tipo despesa";
            // 
            // radioButtonIncome
            // 
            radioButtonIncome.AutoSize = true;
            radioButtonIncome.Location = new Point(104, 156);
            radioButtonIncome.Name = "radioButtonIncome";
            radioButtonIncome.Size = new Size(73, 19);
            radioButtonIncome.TabIndex = 12;
            radioButtonIncome.Text = "Donativo";
            radioButtonIncome.UseVisualStyleBackColor = true;
            // 
            // radioButtonExpenses
            // 
            radioButtonExpenses.AutoSize = true;
            radioButtonExpenses.Checked = true;
            radioButtonExpenses.Location = new Point(12, 153);
            radioButtonExpenses.Name = "radioButtonExpenses";
            radioButtonExpenses.Size = new Size(68, 19);
            radioButtonExpenses.TabIndex = 11;
            radioButtonExpenses.TabStop = true;
            radioButtonExpenses.Text = "Despesa";
            radioButtonExpenses.UseVisualStyleBackColor = true;
            // 
            // dtpDataMovimento
            // 
            dtpDataMovimento.Format = DateTimePickerFormat.Short;
            dtpDataMovimento.Location = new Point(12, 45);
            dtpDataMovimento.Name = "dtpDataMovimento";
            dtpDataMovimento.Size = new Size(127, 23);
            dtpDataMovimento.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 14;
            label1.Text = "Data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 135);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 15;
            label2.Text = "Tipo";
            // 
            // txtId
            // 
            txtId.Location = new Point(212, 203);
            txtId.Name = "txtId";
            txtId.Size = new Size(75, 23);
            txtId.TabIndex = 16;
            txtId.Visible = false;
            // 
            // frmNewEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 432);
            Controls.Add(txtId);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpDataMovimento);
            Controls.Add(radioButtonIncome);
            Controls.Add(radioButtonExpenses);
            Controls.Add(cboTipoDespesa);
            Controls.Add(labelTipoDespesa);
            Controls.Add(btnAddEdit);
            Controls.Add(cboCategoriasDespesas);
            Controls.Add(labelCategory);
            Controls.Add(txtValor);
            Controls.Add(labelValue);
            Controls.Add(txtDescricao);
            Controls.Add(labelDescription);
            Name = "frmNewEntry";
            Text = "Entrada de despesas / donativos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelDescription;
        private TextBox txtDescricao;
        private Label labelValue;
        private TextBox txtValor;
        private Label labelCategory;
        private ComboBox cboCategoriasDespesas;
        private Button btnAddEdit;
        private ComboBox cboTipoDespesa;
        private Label labelTipoDespesa;
        private RadioButton radioButtonIncome;
        private RadioButton radioButtonExpenses;
        private DateTimePicker dtpDataMovimento;
        private Label label1;
        private Label label2;
        private TextBox txtId;
    }
}