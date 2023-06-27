using DaisyPets.UI.Properties;

namespace DaisyPets.UI
{
    partial class Configuration
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            groupBox1 = new GroupBox();
            bSearchFolder = new Button();
            txtPasta = new TextBox();
            label3 = new Label();
            label2 = new Label();
            btnBackup = new Button();
            toolTip1 = new ToolTip(components);
            btnRestore = new Button();
            fbBackup = new FolderBrowserDialog();
            label4 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label5 = new Label();
            bSearchFile = new Button();
            txtBackupFile = new TextBox();
            label6 = new Label();
            ofdRestore = new OpenFileDialog();
            btnClose = new Button();
            imageList1 = new ImageList(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Lavender;
            groupBox1.Controls.Add(bSearchFolder);
            groupBox1.Controls.Add(txtPasta);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnBackup);
            groupBox1.Location = new Point(12, 43);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(676, 153);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // bSearchFolder
            // 
            bSearchFolder.AutoSize = true;
            bSearchFolder.Image = Resources.Be_Folder_icon;
            bSearchFolder.Location = new Point(607, 13);
            bSearchFolder.Margin = new Padding(4, 3, 4, 3);
            bSearchFolder.Name = "bSearchFolder";
            bSearchFolder.Size = new Size(50, 44);
            bSearchFolder.TabIndex = 10;
            bSearchFolder.UseVisualStyleBackColor = true;
            bSearchFolder.Click += bSearchFolder_Click;
            // 
            // txtPasta
            // 
            txtPasta.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPasta.Location = new Point(71, 18);
            txtPasta.Margin = new Padding(4, 3, 4, 3);
            txtPasta.Multiline = true;
            txtPasta.Name = "txtPasta";
            txtPasta.Size = new Size(510, 43);
            txtPasta.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(68, 70);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(304, 15);
            label3.TabIndex = 8;
            label3.Text = "Se não selecionar pasta, backup será feito no disco local.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(20, 18);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(38, 17);
            label2.TabIndex = 8;
            label2.Text = "Local";
            // 
            // btnBackup
            // 
            btnBackup.BackColor = Color.Transparent;
            btnBackup.Enabled = false;
            btnBackup.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnBackup.ForeColor = Color.Black;
            btnBackup.ImageAlign = ContentAlignment.MiddleLeft;
            btnBackup.Location = new Point(537, 91);
            btnBackup.Margin = new Padding(4, 3, 4, 3);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(120, 55);
            btnBackup.TabIndex = 7;
            btnBackup.Text = "&Backup";
            btnBackup.TextAlign = ContentAlignment.MiddleRight;
            toolTip1.SetToolTip(btnBackup, "Clique para efetuar backup à base de dados");
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += btnBackup_Click;
            // 
            // btnRestore
            // 
            btnRestore.BackColor = Color.Gray;
            btnRestore.Enabled = false;
            btnRestore.FlatStyle = FlatStyle.Popup;
            btnRestore.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnRestore.ForeColor = Color.White;
            btnRestore.Image = Properties.Resources.if_Database_Upload_32_32;
            btnRestore.ImageAlign = ContentAlignment.MiddleLeft;
            btnRestore.Location = new Point(545, 91);
            btnRestore.Margin = new Padding(4, 3, 4, 3);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(120, 55);
            btnRestore.TabIndex = 7;
            btnRestore.Text = "&Restore";
            btnRestore.TextAlign = ContentAlignment.MiddleRight;
            toolTip1.SetToolTip(btnRestore, "Clique para efetuar backup à base de dados");
            btnRestore.UseVisualStyleBackColor = false;
            btnRestore.Click += btnRestore_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(14, 10);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(186, 25);
            label4.TabIndex = 9;
            label4.Text = "Cópia de segurança";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(6, 215);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(273, 25);
            label1.TabIndex = 11;
            label1.Text = "Restaurar cópia de segurança";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Silver;
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(bSearchFile);
            groupBox2.Controls.Add(txtBackupFile);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(btnRestore);
            groupBox2.Location = new Point(4, 247);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(676, 153);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(85, 67);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(340, 15);
            label5.TabIndex = 11;
            label5.Text = "Indicar local a partir do qual pretende restaurar a base de dados";
            // 
            // bSearchFile
            // 
            bSearchFile.AutoSize = true;
            bSearchFile.Image = Properties.Resources.Be_Folder_icon;
            bSearchFile.Location = new Point(607, 15);
            bSearchFile.Margin = new Padding(4, 3, 4, 3);
            bSearchFile.Name = "bSearchFile";
            bSearchFile.Size = new Size(50, 44);
            bSearchFile.TabIndex = 10;
            bSearchFile.UseVisualStyleBackColor = true;
            bSearchFile.Click += bSearchFile_Click;
            // 
            // txtBackupFile
            // 
            txtBackupFile.Enabled = false;
            txtBackupFile.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtBackupFile.Location = new Point(79, 15);
            txtBackupFile.Margin = new Padding(4, 3, 4, 3);
            txtBackupFile.Multiline = true;
            txtBackupFile.Name = "txtBackupFile";
            txtBackupFile.Size = new Size(510, 43);
            txtBackupFile.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(7, 18);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(53, 17);
            label6.TabIndex = 8;
            label6.Text = "Ficheiro";
            // 
            // ofdRestore
            // 
            ofdRestore.FileName = "openFileDialog1";
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.Menu;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Image = Properties.Resources.Clear;
            btnClose.ImageAlign = ContentAlignment.MiddleLeft;
            btnClose.Location = new Point(589, 415);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(79, 55);
            btnClose.TabIndex = 7;
            btnClose.Text = "&Sair";
            btnClose.TextAlign = ContentAlignment.MiddleRight;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click_1;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "BackupR.jpg");
            // 
            // Configuration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            BorderThickness = 5;
            CaptionBarColor = Color.SteelBlue;
            CaptionBarHeight = 51;
            CaptionFont = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionImage1.BackColor = Color.Transparent;
            captionImage1.Image = Properties.Resources.database_protect_shield_512;
            captionImage1.Location = new Point(10, 4);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(40, 40);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Italic, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(60, 2);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Size = new Size(300, 24);
            captionLabel1.Text = "Painel do Administrador";
            captionLabel2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(60, 24);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(250, 24);
            captionLabel2.Text = "Backup / Restore da base de dados";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(700, 487);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(label4);
            Controls.Add(btnClose);
            Controls.Add(groupBox1);
            DropShadow = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Configuration";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += Configuration_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private Button btnBackup;
        private ToolTip toolTip1;
        private Button bSearchFolder;
        private TextBox txtPasta;
        private Label label2;
        private FolderBrowserDialog fbBackup;
        private Label label3;
        private Label label4;
        private Label label1;
        private GroupBox groupBox2;
        private Button bSearchFile;
        private TextBox txtBackupFile;
        private Label label6;
        private Button btnRestore;
        private Button btnClose;
        private OpenFileDialog ofdRestore;
        private ImageList imageList1;
        private Label label5;
    }
}