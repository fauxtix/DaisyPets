namespace DaisyPets.UI
{
    partial class frmMain
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
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            toolStrip1 = new ToolStrip();
            btnPets = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnContactos = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnFechar = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(48, 48);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnPets, toolStripSeparator1, btnContactos, toolStripSeparator2, toolStripButton3, toolStripSeparator3, btnFechar });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1384, 55);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnPets
            // 
            btnPets.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnPets.Image = Properties.Resources.database_px_png;
            btnPets.ImageTransparentColor = Color.Magenta;
            btnPets.Name = "btnPets";
            btnPets.Size = new Size(87, 52);
            btnPets.Text = "Pets";
            btnPets.Click += btnPets_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 55);
            // 
            // btnContactos
            // 
            btnContactos.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnContactos.Image = Properties.Resources.people_1;
            btnContactos.ImageTransparentColor = Color.Magenta;
            btnContactos.Name = "btnContactos";
            btnContactos.Size = new Size(127, 52);
            btnContactos.Text = "Contactos";
            btnContactos.Click += btnContactos_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 55);
            // 
            // toolStripButton3
            // 
            toolStripButton3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripButton3.Image = Properties.Resources.iconfinder_ic_settings_48px_352095;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(150, 52);
            toolStripButton3.Text = "Configuração";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 55);
            // 
            // btnFechar
            // 
            btnFechar.Image = Properties.Resources.Users_Exit_icon;
            btnFechar.ImageTransparentColor = Color.Magenta;
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(78, 52);
            btnFechar.Text = "Sair";
            btnFechar.Click += btnFechar_Click;
            // 
            // frmMain
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImage = Properties.Resources.catsanddogs;
            BackgroundImageLayout = ImageLayout.Center;
            CaptionBarColor = Color.SteelBlue;
            CaptionBarHeight = 51;
            captionImage1.Image = Properties.Resources.catsanddogs;
            captionImage1.Location = new Point(5, 0);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(60, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Size = new Size(300, 24);
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(60, 24);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(350, 24);
            captionLabel2.Text = "Margarida Luís - 2023";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1384, 874);
            Controls.Add(toolStrip1);
            IsMdiContainer = true;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += frmMain_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnPets;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnContactos;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnFechar;
    }
}