namespace DaisyPets.UI.Stats
{
    partial class frmPetStats
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetStats));
            dgvVaccines = new DataGridView();
            NomePet = new DataGridViewTextBoxColumn();
            Marca = new DataGridViewTextBoxColumn();
            DataToma = new DataGridViewTextBoxColumn();
            ProximaTomaEmMeses = new DataGridViewTextBoxColumn();
            DataProximaToma = new DataGridViewTextBoxColumn();
            DiasParaProximaToma = new DataGridViewTextBoxColumn();
            dgvDewormers = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            DataAplicacao = new DataGridViewTextBoxColumn();
            DataProximaAplicacao = new DataGridViewTextBoxColumn();
            DiasParaProximaAplicacao = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVaccines).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDewormers).BeginInit();
            SuspendLayout();
            // 
            // dgvVaccines
            // 
            dgvVaccines.AllowUserToAddRows = false;
            dgvVaccines.AllowUserToDeleteRows = false;
            dgvVaccines.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            dgvVaccines.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvVaccines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvVaccines.ColumnHeadersHeight = 32;
            dgvVaccines.Columns.AddRange(new DataGridViewColumn[] { NomePet, Marca, DataToma, ProximaTomaEmMeses, DataProximaToma, DiasParaProximaToma });
            dgvVaccines.Location = new Point(25, 39);
            dgvVaccines.MultiSelect = false;
            dgvVaccines.Name = "dgvVaccines";
            dgvVaccines.ReadOnly = true;
            dgvVaccines.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvVaccines.RowTemplate.DividerHeight = 1;
            dgvVaccines.RowTemplate.Height = 32;
            dgvVaccines.Size = new Size(978, 192);
            dgvVaccines.TabIndex = 0;
            dgvVaccines.ColumnHeaderMouseClick += dgvVaccines_ColumnHeaderMouseClick;
            dgvVaccines.RowPrePaint += dgvVaccines_RowPrePaint;
            // 
            // NomePet
            // 
            NomePet.DataPropertyName = "NomePet";
            NomePet.HeaderText = "Pet";
            NomePet.Name = "NomePet";
            NomePet.ReadOnly = true;
            NomePet.Width = 230;
            // 
            // Marca
            // 
            Marca.DataPropertyName = "Marca";
            Marca.HeaderText = "Marca";
            Marca.Name = "Marca";
            Marca.ReadOnly = true;
            Marca.Width = 400;
            // 
            // DataToma
            // 
            DataToma.DataPropertyName = "DataToma";
            DataToma.HeaderText = "Toma";
            DataToma.Name = "DataToma";
            DataToma.ReadOnly = true;
            DataToma.Width = 120;
            // 
            // ProximaTomaEmMeses
            // 
            ProximaTomaEmMeses.DataPropertyName = "ProximaTomaEmMeses";
            ProximaTomaEmMeses.HeaderText = "Meses";
            ProximaTomaEmMeses.Name = "ProximaTomaEmMeses";
            ProximaTomaEmMeses.ReadOnly = true;
            ProximaTomaEmMeses.Visible = false;
            // 
            // DataProximaToma
            // 
            DataProximaToma.DataPropertyName = "DataProximaToma";
            DataProximaToma.HeaderText = "Próxima";
            DataProximaToma.Name = "DataProximaToma";
            DataProximaToma.ReadOnly = true;
            DataProximaToma.Width = 120;
            // 
            // DiasParaProximaToma
            // 
            DiasParaProximaToma.DataPropertyName = "DiasParaProximaToma";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(64, 64, 64);
            DiasParaProximaToma.DefaultCellStyle = dataGridViewCellStyle3;
            DiasParaProximaToma.HeaderText = "Dias";
            DiasParaProximaToma.Name = "DiasParaProximaToma";
            DiasParaProximaToma.ReadOnly = true;
            DiasParaProximaToma.Width = 50;
            // 
            // dgvDewormers
            // 
            dgvDewormers.AllowUserToAddRows = false;
            dgvDewormers.AllowUserToDeleteRows = false;
            dgvDewormers.AllowUserToResizeColumns = false;
            dataGridViewCellStyle4.BackColor = Color.LightGray;
            dgvDewormers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvDewormers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvDewormers.ColumnHeadersHeight = 32;
            dgvDewormers.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, DataAplicacao, DataProximaAplicacao, DiasParaProximaAplicacao });
            dgvDewormers.Location = new Point(25, 276);
            dgvDewormers.MultiSelect = false;
            dgvDewormers.Name = "dgvDewormers";
            dgvDewormers.ReadOnly = true;
            dgvDewormers.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dgvDewormers.RowTemplate.DividerHeight = 1;
            dgvDewormers.RowTemplate.Height = 32;
            dgvDewormers.Size = new Size(978, 201);
            dgvDewormers.TabIndex = 1;
            dgvDewormers.ColumnHeaderMouseClick += dgvDewormers_ColumnHeaderMouseClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "NomePet";
            dataGridViewTextBoxColumn1.HeaderText = "Pet";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 230;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "Marca";
            dataGridViewTextBoxColumn2.HeaderText = "Marca";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 400;
            // 
            // DataAplicacao
            // 
            DataAplicacao.DataPropertyName = "DataAplicacao";
            DataAplicacao.HeaderText = "Aplicação";
            DataAplicacao.Name = "DataAplicacao";
            DataAplicacao.ReadOnly = true;
            DataAplicacao.Width = 120;
            // 
            // DataProximaAplicacao
            // 
            DataProximaAplicacao.DataPropertyName = "DataProximaAplicacao";
            DataProximaAplicacao.HeaderText = "Próxima";
            DataProximaAplicacao.Name = "DataProximaAplicacao";
            DataProximaAplicacao.ReadOnly = true;
            DataProximaAplicacao.Width = 120;
            // 
            // DiasParaProximaAplicacao
            // 
            DiasParaProximaAplicacao.DataPropertyName = "DiasParaProximaAplicacao";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(64, 64, 64);
            DiasParaProximaAplicacao.DefaultCellStyle = dataGridViewCellStyle6;
            DiasParaProximaAplicacao.HeaderText = "Dias";
            DiasParaProximaAplicacao.Name = "DiasParaProximaAplicacao";
            DiasParaProximaAplicacao.ReadOnly = true;
            DiasParaProximaAplicacao.Width = 50;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(25, 9);
            label1.Name = "label1";
            label1.Size = new Size(76, 25);
            label1.TabIndex = 2;
            label1.Text = "Vacinas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(25, 245);
            label2.Name = "label2";
            label2.Size = new Size(148, 25);
            label2.TabIndex = 3;
            label2.Text = "Desparasitantes";
            // 
            // frmPetStats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1026, 533);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvDewormers);
            Controls.Add(dgvVaccines);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IconSize = new Size(32, 32);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPetStats";
            StartPosition = FormStartPosition.CenterScreen;
            Style.BackColor = Color.WhiteSmoke;
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            Style.TitleBar.BackColor = Color.Khaki;
            Text = "Vacinas e desparasitantes";
            ((System.ComponentModel.ISupportInitialize)dgvVaccines).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDewormers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvVaccines;
        private DataGridView dgvDewormers;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn NomePet;
        private DataGridViewTextBoxColumn Marca;
        private DataGridViewTextBoxColumn DataToma;
        private DataGridViewTextBoxColumn ProximaTomaEmMeses;
        private DataGridViewTextBoxColumn DataProximaToma;
        private DataGridViewTextBoxColumn DiasParaProximaToma;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn DataAplicacao;
        private DataGridViewTextBoxColumn DataProximaAplicacao;
        private DataGridViewTextBoxColumn DiasParaProximaAplicacao;
    }
}