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
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadDocument));
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            label1 = new Label();
            lblPath = new Label();
            btnBrowseDocument = new Button();
            btnUploadDocument = new Button();
            openFileDialog1 = new OpenFileDialog();
            lblFile = new Label();
            label6 = new Label();
            panel1 = new Panel();
            label4 = new Label();
            label3 = new Label();
            txtDescription = new TextBox();
            txtTitle = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 185);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "Pasta";
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.ForeColor = Color.Red;
            lblPath.Location = new Point(124, 185);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(82, 15);
            lblPath.TabIndex = 1;
            lblPath.Text = "Directory Path";
            // 
            // btnBrowseDocument
            // 
            btnBrowseDocument.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnBrowseDocument.Location = new Point(34, 252);
            btnBrowseDocument.Name = "btnBrowseDocument";
            btnBrowseDocument.Size = new Size(133, 38);
            btnBrowseDocument.TabIndex = 2;
            btnBrowseDocument.Text = "Browse file";
            btnBrowseDocument.UseVisualStyleBackColor = true;
            btnBrowseDocument.Click += btnBrowseDocument_Click;
            // 
            // btnUploadDocument
            // 
            btnUploadDocument.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUploadDocument.Location = new Point(342, 252);
            btnUploadDocument.Name = "btnUploadDocument";
            btnUploadDocument.Size = new Size(181, 38);
            btnUploadDocument.TabIndex = 3;
            btnUploadDocument.Text = "Upload document";
            btnUploadDocument.UseVisualStyleBackColor = true;
            btnUploadDocument.Click += btnUploadDocument_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.ForeColor = Color.Red;
            lblFile.Location = new Point(124, 213);
            lblFile.Name = "lblFile";
            lblFile.Size = new Size(82, 15);
            lblFile.TabIndex = 9;
            lblFile.Text = "Directory Path";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(34, 213);
            label6.Name = "label6";
            label6.Size = new Size(49, 15);
            label6.TabIndex = 8;
            label6.Text = "Ficheiro";
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(txtTitle);
            panel1.Location = new Point(3, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(557, 153);
            panel1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(34, 58);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 11;
            label4.Text = "Descrição";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(34, 32);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 10;
            label3.Text = "Título";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescription.Location = new Point(138, 58);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(385, 66);
            txtDescription.TabIndex = 9;
            // 
            // txtTitle
            // 
            txtTitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtTitle.Location = new Point(138, 29);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(385, 27);
            txtTitle.TabIndex = 8;
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
            ClientSize = new Size(560, 312);
            Controls.Add(panel1);
            Controls.Add(lblFile);
            Controls.Add(label6);
            Controls.Add(btnUploadDocument);
            Controls.Add(btnBrowseDocument);
            Controls.Add(lblPath);
            Controls.Add(label1);
            Name = "frmUploadDocument";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblPath;
        private Button btnBrowseDocument;
        private Button btnUploadDocument;
        private OpenFileDialog openFileDialog1;
        private Label lblFile;
        private Label label6;
        private Panel panel1;
        private Label label4;
        private Label label3;
        private TextBox txtDescription;
        private TextBox txtTitle;
    }
}