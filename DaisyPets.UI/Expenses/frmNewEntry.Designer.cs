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
            btnClose = new Button();
            btnAddEdit = new Button();
            panel1 = new Panel();
            txtNotas = new TextBox();
            label3 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            label1 = new Label();
            dtpDataMovimento = new DateTimePicker();
            radioButtonIncome = new RadioButton();
            radioButtonExpenses = new RadioButton();
            cboTipoDespesa = new ComboBox();
            labelTipoDespesa = new Label();
            cboCategoriasDespesas = new ComboBox();
            labelCategory = new Label();
            txtValor = new TextBox();
            labelValue = new Label();
            txtDescricao = new TextBox();
            labelDescription = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.ForeColor = Color.Black;
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.ImageAlign = ContentAlignment.MiddleLeft;
            btnClose.Location = new Point(318, 449);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(106, 39);
            btnClose.TabIndex = 23;
            btnClose.Text = "Cancelar";
            btnClose.TextAlign = ContentAlignment.MiddleRight;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnAddEdit
            // 
            btnAddEdit.BackColor = Color.ForestGreen;
            btnAddEdit.FlatStyle = FlatStyle.Flat;
            btnAddEdit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddEdit.ForeColor = Color.White;
            btnAddEdit.Image = Properties.Resources.save32;
            btnAddEdit.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddEdit.Location = new Point(195, 449);
            btnAddEdit.Name = "btnAddEdit";
            btnAddEdit.Size = new Size(106, 39);
            btnAddEdit.TabIndex = 22;
            btnAddEdit.Text = "Update ";
            btnAddEdit.TextAlign = ContentAlignment.MiddleRight;
            btnAddEdit.UseVisualStyleBackColor = false;
            btnAddEdit.Click += btnAddEdit_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(txtNotas);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dtpDataMovimento);
            panel1.Controls.Add(radioButtonIncome);
            panel1.Controls.Add(radioButtonExpenses);
            panel1.Controls.Add(cboTipoDespesa);
            panel1.Controls.Add(labelTipoDespesa);
            panel1.Controls.Add(cboCategoriasDespesas);
            panel1.Controls.Add(labelCategory);
            panel1.Controls.Add(txtValor);
            panel1.Controls.Add(labelValue);
            panel1.Controls.Add(txtDescricao);
            panel1.Controls.Add(labelDescription);
            panel1.Location = new Point(4, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 421);
            panel1.TabIndex = 24;
            // 
            // txtNotas
            // 
            txtNotas.BackColor = Color.White;
            txtNotas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtNotas.Location = new Point(17, 332);
            txtNotas.Multiline = true;
            txtNotas.Name = "txtNotas";
            txtNotas.ScrollBars = ScrollBars.Vertical;
            txtNotas.Size = new Size(395, 79);
            txtNotas.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(17, 309);
            label3.Name = "label3";
            label3.Size = new Size(48, 20);
            label3.TabIndex = 40;
            label3.Text = "Notas";
            // 
            // txtId
            // 
            txtId.Location = new Point(211, 154);
            txtId.Name = "txtId";
            txtId.Size = new Size(75, 23);
            txtId.TabIndex = 39;
            txtId.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(203, 9);
            label2.Name = "label2";
            label2.Size = new Size(39, 20);
            label2.TabIndex = 38;
            label2.Text = "Tipo";
            label2.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(17, 8);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 37;
            label1.Text = "Data";
            // 
            // dtpDataMovimento
            // 
            dtpDataMovimento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpDataMovimento.Format = DateTimePickerFormat.Short;
            dtpDataMovimento.Location = new Point(17, 31);
            dtpDataMovimento.Name = "dtpDataMovimento";
            dtpDataMovimento.Size = new Size(127, 27);
            dtpDataMovimento.TabIndex = 0;
            // 
            // radioButtonIncome
            // 
            radioButtonIncome.AutoSize = true;
            radioButtonIncome.Location = new Point(295, 33);
            radioButtonIncome.Name = "radioButtonIncome";
            radioButtonIncome.Size = new Size(73, 19);
            radioButtonIncome.TabIndex = 35;
            radioButtonIncome.Text = "Donativo";
            radioButtonIncome.UseVisualStyleBackColor = true;
            radioButtonIncome.Visible = false;
            // 
            // radioButtonExpenses
            // 
            radioButtonExpenses.AutoSize = true;
            radioButtonExpenses.Checked = true;
            radioButtonExpenses.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonExpenses.Location = new Point(203, 30);
            radioButtonExpenses.Name = "radioButtonExpenses";
            radioButtonExpenses.Size = new Size(83, 24);
            radioButtonExpenses.TabIndex = 34;
            radioButtonExpenses.TabStop = true;
            radioButtonExpenses.Text = "Despesa";
            radioButtonExpenses.UseVisualStyleBackColor = true;
            radioButtonExpenses.Visible = false;
            // 
            // cboTipoDespesa
            // 
            cboTipoDespesa.BackColor = Color.Khaki;
            cboTipoDespesa.Enabled = false;
            cboTipoDespesa.FlatStyle = FlatStyle.System;
            cboTipoDespesa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboTipoDespesa.ForeColor = Color.Black;
            cboTipoDespesa.FormattingEnabled = true;
            cboTipoDespesa.Location = new Point(17, 272);
            cboTipoDespesa.Name = "cboTipoDespesa";
            cboTipoDespesa.Size = new Size(299, 28);
            cboTipoDespesa.TabIndex = 4;
            cboTipoDespesa.SelectedIndexChanged += cboTipoDespesa_SelectedIndexChanged;
            // 
            // labelTipoDespesa
            // 
            labelTipoDespesa.AutoSize = true;
            labelTipoDespesa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelTipoDespesa.Location = new Point(17, 249);
            labelTipoDespesa.Name = "labelTipoDespesa";
            labelTipoDespesa.Size = new Size(107, 20);
            labelTipoDespesa.TabIndex = 32;
            labelTipoDespesa.Text = "* Tipo despesa";
            // 
            // cboCategoriasDespesas
            // 
            cboCategoriasDespesas.BackColor = Color.Khaki;
            cboCategoriasDespesas.FlatStyle = FlatStyle.System;
            cboCategoriasDespesas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboCategoriasDespesas.ForeColor = Color.Black;
            cboCategoriasDespesas.FormattingEnabled = true;
            cboCategoriasDespesas.Location = new Point(17, 212);
            cboCategoriasDespesas.Name = "cboCategoriasDespesas";
            cboCategoriasDespesas.Size = new Size(299, 28);
            cboCategoriasDespesas.TabIndex = 3;
            cboCategoriasDespesas.SelectedIndexChanged += cboCategoriasDespesas_SelectedIndexChanged;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelCategory.Location = new Point(17, 189);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(142, 20);
            labelCategory.TabIndex = 30;
            labelCategory.Text = "* Categoria despesa";
            // 
            // txtValor
            // 
            txtValor.BackColor = Color.White;
            txtValor.BorderStyle = BorderStyle.FixedSingle;
            txtValor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtValor.Location = new Point(17, 153);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(88, 27);
            txtValor.TabIndex = 2;
            txtValor.TextAlign = HorizontalAlignment.Right;
            // 
            // labelValue
            // 
            labelValue.AutoSize = true;
            labelValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelValue.Location = new Point(17, 130);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(53, 20);
            labelValue.TabIndex = 28;
            labelValue.Text = "* Valor";
            // 
            // txtDescricao
            // 
            txtDescricao.BackColor = Color.White;
            txtDescricao.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtDescricao.Location = new Point(17, 94);
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(395, 27);
            txtDescricao.TabIndex = 1;
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelDescription.Location = new Point(17, 68);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(84, 20);
            labelDescription.TabIndex = 26;
            labelDescription.Text = "* Descrição";
            // 
            // frmNewEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            CaptionBarColor = Color.SeaShell;
            CaptionBarHeight = 55;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.coin_add_icon;
            captionImage1.Location = new Point(4, 6);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(32, 32);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(50, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(50, 26);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(200, 24);
            captionLabel2.Text = "Despesas";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(444, 500);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(btnAddEdit);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNewEntry";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnClose;
        private Button btnAddEdit;
        private Panel panel1;
        private TextBox txtNotas;
        private Label label3;
        private TextBox txtId;
        private Label label2;
        private Label label1;
        private DateTimePicker dtpDataMovimento;
        private RadioButton radioButtonIncome;
        private RadioButton radioButtonExpenses;
        private ComboBox cboTipoDespesa;
        private Label labelTipoDespesa;
        private ComboBox cboCategoriasDespesas;
        private Label labelCategory;
        private TextBox txtValor;
        private Label labelValue;
        private TextBox txtDescricao;
        private Label labelDescription;
    }
}