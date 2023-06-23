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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            toolStrip1 = new ToolStrip();
            btnPets = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnExpensesDonations = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnContactos = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            optGallery = new ToolStripButton();
            toolStripSeparator10 = new ToolStripSeparator();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            optCategoriaDespesas = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            optRacas = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            optSituacao = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            optTemperamento = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            optTipoDespesa = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator9 = new ToolStripSeparator();
            btnFechar = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(48, 48);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnPets, toolStripSeparator1, btnExpensesDonations, toolStripSeparator4, btnContactos, toolStripSeparator3, optGallery, toolStripSeparator10, toolStripDropDownButton1, toolStripSeparator2, toolStripButton3, toolStripSeparator9, btnFechar });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1384, 55);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnPets
            // 
            btnPets.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnPets.Image = (Image)resources.GetObject("btnPets.Image");
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
            // btnExpensesDonations
            // 
            btnExpensesDonations.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnExpensesDonations.Image = Properties.Resources.iconfinder_71_5027865;
            btnExpensesDonations.ImageTransparentColor = Color.Magenta;
            btnExpensesDonations.Name = "btnExpensesDonations";
            btnExpensesDonations.Size = new Size(123, 52);
            btnExpensesDonations.Text = "Despesas";
            btnExpensesDonations.Click += btnExpensesDonations_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 55);
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
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 55);
            // 
            // optGallery
            // 
            optGallery.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optGallery.Image = (Image)resources.GetObject("optGallery.Image");
            optGallery.ImageTransparentColor = Color.Magenta;
            optGallery.Name = "optGallery";
            optGallery.Size = new Size(189, 52);
            optGallery.Text = "Galeria de imagens";
            optGallery.Click += optGallery_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(6, 55);
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { optCategoriaDespesas, toolStripSeparator5, optRacas, toolStripSeparator6, optSituacao, toolStripSeparator7, optTemperamento, toolStripSeparator8, optTipoDespesa });
            toolStripDropDownButton1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripDropDownButton1.Image = Properties.Resources.iconfinder_ic_settings_48px_352095;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(159, 52);
            toolStripDropDownButton1.Text = "Configuração";
            // 
            // optCategoriaDespesas
            // 
            optCategoriaDespesas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optCategoriaDespesas.Image = Properties.Resources.tool_box_icon;
            optCategoriaDespesas.Name = "optCategoriaDespesas";
            optCategoriaDespesas.Size = new Size(241, 54);
            optCategoriaDespesas.Text = "Categoria Despesas";
            optCategoriaDespesas.Click += optCategoriaDespesas_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(238, 6);
            // 
            // optRacas
            // 
            optRacas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optRacas.Image = Properties.Resources.tool_box_icon;
            optRacas.Name = "optRacas";
            optRacas.Size = new Size(241, 54);
            optRacas.Text = "Raças";
            optRacas.Click += optRacas_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(238, 6);
            // 
            // optSituacao
            // 
            optSituacao.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optSituacao.Image = Properties.Resources.tool_box_icon;
            optSituacao.Name = "optSituacao";
            optSituacao.Size = new Size(241, 54);
            optSituacao.Text = "Situação";
            optSituacao.Click += optSituacao_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(238, 6);
            // 
            // optTemperamento
            // 
            optTemperamento.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optTemperamento.Image = Properties.Resources.tool_box_icon;
            optTemperamento.Name = "optTemperamento";
            optTemperamento.Size = new Size(241, 54);
            optTemperamento.Text = "Temperamento";
            optTemperamento.Click += optTemperamento_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(238, 6);
            // 
            // optTipoDespesa
            // 
            optTipoDespesa.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            optTipoDespesa.Image = Properties.Resources.tool_box_icon;
            optTipoDespesa.Name = "optTipoDespesa";
            optTipoDespesa.Size = new Size(241, 54);
            optTipoDespesa.Text = "Tipo de Despesa";
            optTipoDespesa.Click += optTipoDespesa_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 55);
            // 
            // toolStripButton3
            // 
            toolStripButton3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(158, 52);
            toolStripButton3.Text = "Base de dados";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(6, 55);
            // 
            // btnFechar
            // 
            btnFechar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnFechar.Image = Properties.Resources.Users_Exit_icon;
            btnFechar.ImageTransparentColor = Color.Magenta;
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(86, 52);
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
            CaptionBarHeight = 60;
            captionImage1.Image = Properties.Resources.catsanddogs;
            captionImage1.Location = new Point(5, 6);
            captionImage1.Name = "CaptionImage1";
            captionImage1.Size = new Size(48, 48);
            CaptionImages.Add(captionImage1);
            captionLabel1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            captionLabel1.ForeColor = Color.White;
            captionLabel1.Location = new Point(70, 4);
            captionLabel1.Name = "CaptionLabel1";
            captionLabel1.Size = new Size(300, 24);
            captionLabel1.Text = "Daisy Pets";
            captionLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            captionLabel2.ForeColor = Color.White;
            captionLabel2.Location = new Point(70, 28);
            captionLabel2.Name = "CaptionLabel2";
            captionLabel2.Size = new Size(350, 24);
            captionLabel2.Text = "Margarida Luís - 2023";
            CaptionLabels.Add(captionLabel1);
            CaptionLabels.Add(captionLabel2);
            ClientSize = new Size(1384, 865);
            Controls.Add(toolStrip1);
            IsMdiContainer = true;
            MaximizeBox = false;
            MinimizeBox = false;
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
        private ToolStripButton btnExpensesDonations;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem optCategoriaDespesas;
        private ToolStripMenuItem optRacas;
        private ToolStripMenuItem optSituacao;
        private ToolStripMenuItem optTemperamento;
        private ToolStripMenuItem optTipoDespesa;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton optGallery;
    }
}