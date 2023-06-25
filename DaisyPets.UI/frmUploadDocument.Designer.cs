namespace DaisyPets.UI
{
    partial class frmUploadDocument
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
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadDocument));
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            btnBrowseDocument = new Button();
            openFileDialog1 = new OpenFileDialog();
            panel1 = new Panel();
            lblPath = new Label();
            label1 = new Label();
            lblFile = new Label();
            label6 = new Label();
            lblPetName = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            txtPetName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            lblSelect = new Label();
            txtId = new TextBox();
            label4 = new Label();
            label3 = new Label();
            txtDescription = new TextBox();
            txtTitle = new TextBox();
            dgvDocuments = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            CreatedOn = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            PetName = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            DocumentPath = new DataGridViewTextBoxColumn();
            optView = new DataGridViewButtonColumn();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnInsert = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtPetName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).BeginInit();
            SuspendLayout();
            // 
            // btnBrowseDocument
            // 
            btnBrowseDocument.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnBrowseDocument.Location = new Point(135, 4);
            btnBrowseDocument.Name = "btnBrowseDocument";
            btnBrowseDocument.Size = new Size(133, 38);
            btnBrowseDocument.TabIndex = 2;
            btnBrowseDocument.Text = "Select";
            btnBrowseDocument.UseVisualStyleBackColor = true;
            btnBrowseDocument.Click += btnBrowseDocument_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            panel1.Controls.Add(lblPath);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblFile);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(lblPetName);
            panel1.Controls.Add(txtPetName);
            panel1.Controls.Add(lblSelect);
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(txtTitle);
            panel1.Controls.Add(btnBrowseDocument);
            panel1.Location = new Point(3, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(931, 267);
            panel1.TabIndex = 10;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblPath.ForeColor = Color.DimGray;
            lblPath.Location = new Point(135, 43);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(102, 20);
            lblPath.TabIndex = 17;
            lblPath.Text = "Directory Path";
            lblPath.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(21, 43);
            label1.Name = "label1";
            label1.Size = new Size(43, 20);
            label1.TabIndex = 16;
            label1.Text = "Pasta";
            label1.Visible = false;
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblFile.ForeColor = Color.DimGray;
            lblFile.Location = new Point(135, 71);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(102, 20);
            lblFile.TabIndex = 19;
            lblFile.Text = "Directory Path";
            lblFile.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(21, 71);
            label6.Name = "label6";
            label6.Size = new Size(61, 20);
            label6.TabIndex = 18;
            label6.Text = "Ficheiro";
            label6.Visible = false;
            // 
            // lblPetName
            // 
            lblPetName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblPetName.Location = new Point(21, 116);
            lblPetName.Name = "lblPetName";
            lblPetName.Size = new Size(29, 20);
            lblPetName.TabIndex = 15;
            lblPetName.Text = "Pet";
            // 
            // txtPetName
            // 
            txtPetName.BeforeTouchSize = new Size(273, 27);
            txtPetName.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtPetName.Location = new Point(135, 113);
            txtPetName.Name = "txtPetName";
            txtPetName.ReadOnly = true;
            txtPetName.Size = new Size(273, 27);
            txtPetName.TabIndex = 14;
            // 
            // lblSelect
            // 
            lblSelect.AutoSize = true;
            lblSelect.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblSelect.Location = new Point(21, 14);
            lblSelect.Name = "lblSelect";
            lblSelect.Size = new Size(100, 20);
            lblSelect.TabIndex = 13;
            lblSelect.Text = "Selecione pdf";
            // 
            // txtId
            // 
            txtId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtId.Location = new Point(31, 218);
            txtId.Name = "txtId";
            txtId.Size = new Size(64, 27);
            txtId.TabIndex = 12;
            txtId.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(21, 179);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 11;
            label4.Text = "Descrição";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(21, 149);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 10;
            label3.Text = "Título";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescription.Location = new Point(135, 179);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(482, 66);
            txtDescription.TabIndex = 9;
            // 
            // txtTitle
            // 
            txtTitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtTitle.Location = new Point(135, 146);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(482, 27);
            txtTitle.TabIndex = 8;
            // 
            // dgvDocuments
            // 
            dgvDocuments.AllowUserToAddRows = false;
            dgvDocuments.AllowUserToDeleteRows = false;
            dgvDocuments.AllowUserToOrderColumns = true;
            dgvDocuments.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDocuments.ColumnHeadersHeight = 32;
            dgvDocuments.Columns.AddRange(new DataGridViewColumn[] { Id, CreatedOn, Title, PetName, Description, DocumentPath, optView });
            dgvDocuments.Location = new Point(21, 356);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dgvDocuments.RowTemplate.Height = 32;
            dgvDocuments.ScrollBars = ScrollBars.Vertical;
            dgvDocuments.Size = new Size(919, 202);
            dgvDocuments.TabIndex = 11;
            dgvDocuments.CellClick += dgvDocuments_CellClick;
            dgvDocuments.CellContentClick += dgvDocuments_CellContentClick;
            dgvDocuments.ColumnHeaderMouseClick += dgvDocuments_ColumnHeaderMouseClick;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // CreatedOn
            // 
            CreatedOn.DataPropertyName = "CreatedOn";
            CreatedOn.HeaderText = "Criado em";
            CreatedOn.Name = "CreatedOn";
            CreatedOn.ReadOnly = true;
            // 
            // Title
            // 
            Title.DataPropertyName = "Title";
            Title.HeaderText = "Título";
            Title.Name = "Title";
            Title.ReadOnly = true;
            Title.Width = 300;
            // 
            // PetName
            // 
            PetName.DataPropertyName = "PetName";
            PetName.HeaderText = "Pet";
            PetName.Name = "PetName";
            PetName.ReadOnly = true;
            PetName.Visible = false;
            PetName.Width = 200;
            // 
            // Description
            // 
            Description.DataPropertyName = "Description";
            Description.HeaderText = "Descrição";
            Description.Name = "Description";
            Description.ReadOnly = true;
            Description.Width = 400;
            // 
            // DocumentPath
            // 
            DocumentPath.DataPropertyName = "DocumentPath";
            DocumentPath.HeaderText = "Path";
            DocumentPath.Name = "DocumentPath";
            DocumentPath.ReadOnly = true;
            DocumentPath.Visible = false;
            // 
            // optView
            // 
            optView.HeaderText = "Ver";
            optView.Name = "optView";
            optView.ReadOnly = true;
            optView.Text = "...";
            optView.UseColumnTextForButtonValue = true;
            optView.Width = 60;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.ForeColor = Color.Black;
            btnClear.Image = Properties.Resources.edit_clear_32x32;
            btnClear.ImageAlign = ContentAlignment.MiddleLeft;
            btnClear.Location = new Point(522, 302);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(98, 39);
            btnClear.TabIndex = 23;
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
            btnDelete.Location = new Point(287, 302);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 39);
            btnDelete.TabIndex = 22;
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
            btnUpdate.Location = new Point(154, 302);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(98, 39);
            btnUpdate.TabIndex = 21;
            btnUpdate.Text = "Update";
            btnUpdate.TextAlign = ContentAlignment.MiddleRight;
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnInsert
            // 
            btnInsert.BackColor = Color.SteelBlue;
            btnInsert.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnInsert.ForeColor = Color.White;
            btnInsert.Image = Properties.Resources.Clear;
            btnInsert.ImageAlign = ContentAlignment.MiddleLeft;
            btnInsert.Location = new Point(21, 302);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(98, 39);
            btnInsert.TabIndex = 24;
            btnInsert.Text = "Add ";
            btnInsert.TextAlign = ContentAlignment.MiddleRight;
            btnInsert.UseVisualStyleBackColor = false;
            btnInsert.Click += btnUploadDocument_Click;
            // 
            // frmUploadDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            CaptionBarColor = Color.Gainsboro;
            CaptionBarHeight = 60;
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = (Image)resources.GetObject("captionImage1.Image");
            captionImage1.Location = new Point(2, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.Location = new Point(60, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.Location = new Point(60, 26);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Text = "Documentos";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(946, 561);
            Controls.Add(btnInsert);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(dgvDocuments);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmUploadDocument";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtPetName).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnBrowseDocument;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Label label4;
        private Label label3;
        private TextBox txtDescription;
        private TextBox txtTitle;
        private DataGridView dgvDocuments;
        private TextBox txtId;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn CreatedOn;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn PetName;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn DocumentPath;
        private DataGridViewButtonColumn optView;
        private Label lblSelect;
        private Button btnInsert;
        private Label lblPath;
        private Label label1;
        private Label lblFile;
        private Label label6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblPetName;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetName;
    }
}