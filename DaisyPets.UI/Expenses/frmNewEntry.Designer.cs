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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewEntry));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            labelDescription = new Label();
            txtDescricao = new TextBox();
            labelValue = new Label();
            txtValor = new TextBox();
            labelCategory = new Label();
            cboCategoriasDespesas = new ComboBox();
            cboTipoDespesa = new ComboBox();
            labelTipoDespesa = new Label();
            radioButtonIncome = new RadioButton();
            radioButtonExpenses = new RadioButton();
            dtpDataMovimento = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            txtId = new TextBox();
            btnClose = new Button();
            btnAddEdit = new Button();
            SuspendLayout();
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelDescription.Location = new Point(12, 76);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(74, 20);
            labelDescription.TabIndex = 2;
            labelDescription.Text = "Descrição";
            // 
            // txtDescricao
            // 
            txtDescricao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescricao.Location = new Point(12, 99);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(299, 27);
            txtDescricao.TabIndex = 3;
            // 
            // labelValue
            // 
            labelValue.AutoSize = true;
            labelValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelValue.Location = new Point(12, 181);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(53, 20);
            labelValue.TabIndex = 4;
            labelValue.Text = "* Valor";
            // 
            // txtValor
            // 
            txtValor.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtValor.Location = new Point(12, 203);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(111, 27);
            txtValor.TabIndex = 5;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelCategory.Location = new Point(12, 236);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(142, 20);
            labelCategory.TabIndex = 6;
            labelCategory.Text = "* Categoria despesa";
            // 
            // cboCategoriasDespesas
            // 
            cboCategoriasDespesas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboCategoriasDespesas.FormattingEnabled = true;
            cboCategoriasDespesas.Location = new Point(12, 257);
            cboCategoriasDespesas.Name = "cboCategoriasDespesas";
            cboCategoriasDespesas.Size = new Size(299, 28);
            cboCategoriasDespesas.TabIndex = 7;
            cboCategoriasDespesas.SelectedIndexChanged += cboCategoriasDespesas_SelectedIndexChanged;
            // 
            // cboTipoDespesa
            // 
            cboTipoDespesa.Enabled = false;
            cboTipoDespesa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboTipoDespesa.FormattingEnabled = true;
            cboTipoDespesa.Location = new Point(12, 311);
            cboTipoDespesa.Name = "cboTipoDespesa";
            cboTipoDespesa.Size = new Size(299, 28);
            cboTipoDespesa.TabIndex = 10;
            // 
            // labelTipoDespesa
            // 
            labelTipoDespesa.AutoSize = true;
            labelTipoDespesa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelTipoDespesa.Location = new Point(12, 289);
            labelTipoDespesa.Name = "labelTipoDespesa";
            labelTipoDespesa.Size = new Size(107, 20);
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
            radioButtonIncome.CheckedChanged += radioButtonIncome_CheckedChanged;
            // 
            // radioButtonExpenses
            // 
            radioButtonExpenses.AutoSize = true;
            radioButtonExpenses.Checked = true;
            radioButtonExpenses.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonExpenses.Location = new Point(12, 153);
            radioButtonExpenses.Name = "radioButtonExpenses";
            radioButtonExpenses.Size = new Size(83, 24);
            radioButtonExpenses.TabIndex = 11;
            radioButtonExpenses.TabStop = true;
            radioButtonExpenses.Text = "Despesa";
            radioButtonExpenses.UseVisualStyleBackColor = true;
            radioButtonExpenses.CheckedChanged += radioButtonExpenses_CheckedChanged;
            // 
            // dtpDataMovimento
            // 
            dtpDataMovimento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpDataMovimento.Format = DateTimePickerFormat.Short;
            dtpDataMovimento.Location = new Point(12, 45);
            dtpDataMovimento.Name = "dtpDataMovimento";
            dtpDataMovimento.Size = new Size(127, 27);
            dtpDataMovimento.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 14;
            label1.Text = "Data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 132);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
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
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.ForeColor = Color.Black;
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.ImageAlign = ContentAlignment.MiddleLeft;
            btnClose.Location = new Point(169, 375);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(98, 39);
            btnClose.TabIndex = 23;
            btnClose.Text = "Cancelar";
            btnClose.TextAlign = ContentAlignment.MiddleRight;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnAddEdit
            // 
            btnAddEdit.BackColor = Color.ForestGreen;
            btnAddEdit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddEdit.ForeColor = Color.White;
            btnAddEdit.Image = Properties.Resources.save32;
            btnAddEdit.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddEdit.Location = new Point(25, 375);
            btnAddEdit.Name = "btnAddEdit";
            btnAddEdit.Size = new Size(98, 39);
            btnAddEdit.TabIndex = 22;
            btnAddEdit.Text = "Update ";
            btnAddEdit.TextAlign = ContentAlignment.MiddleRight;
            btnAddEdit.UseVisualStyleBackColor = false;
            btnAddEdit.Click += btnAddEdit_Click;
            // 
            // frmNewEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.Khaki;
            CaptionBarHeight = 55;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.coin_add_icon;
            captionImage1.Location = new Point(4, 6);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(32, 32);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(60, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(60, 26);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(200, 24);
            captionLabel2.Text = "Entrada / Saída de valores";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(328, 426);
            Controls.Add(btnClose);
            Controls.Add(btnAddEdit);
            Controls.Add(txtId);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpDataMovimento);
            Controls.Add(radioButtonIncome);
            Controls.Add(radioButtonExpenses);
            Controls.Add(cboTipoDespesa);
            Controls.Add(labelTipoDespesa);
            Controls.Add(cboCategoriasDespesas);
            Controls.Add(labelCategory);
            Controls.Add(txtValor);
            Controls.Add(labelValue);
            Controls.Add(txtDescricao);
            Controls.Add(labelDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNewEntry";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
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
        private ComboBox cboTipoDespesa;
        private Label labelTipoDespesa;
        private RadioButton radioButtonIncome;
        private RadioButton radioButtonExpenses;
        private DateTimePicker dtpDataMovimento;
        private Label label1;
        private Label label2;
        private TextBox txtId;
        private Button btnClose;
        private Button btnAddEdit;
    }
}