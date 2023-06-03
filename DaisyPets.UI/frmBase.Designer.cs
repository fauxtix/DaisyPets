using System.Runtime.Intrinsics.Arm;

namespace DaisyPets.UI
{
    partial class frmBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBase));
            Syncfusion.Windows.Forms.CaptionImage captionImage1 = new Syncfusion.Windows.Forms.CaptionImage();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel1 = new Syncfusion.Windows.Forms.CaptionLabel();
            Syncfusion.Windows.Forms.CaptionLabel captionLabel2 = new Syncfusion.Windows.Forms.CaptionLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.separadorCancelar = new System.Windows.Forms.ToolStripSeparator();
            this.btnLocalizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnListar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFechar = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbGeral = new System.Windows.Forms.ToolStripProgressBar();
            this.lblMessagem = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblAlert = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Khaki;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.toolStripSeparator5,
            this.btnSalvar,
            this.toolStripSeparator2,
            this.btnCancelar,
            this.separadorCancelar,
            this.btnLocalizar,
            this.toolStripSeparator4,
            this.btnExcluir,
            this.toolStripSeparator1,
            this.btnListar,
            this.toolStripSeparator6,
            this.btnFechar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(730, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(68, 36);
            this.btnNovo.Text = "Novo";
            this.btnNovo.ToolTipText = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.BtnNovo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(83, 36);
            this.btnSalvar.Text = "Gravar";
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::DaisyPets.UI.Properties.Resources.edit_clear_32x32;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 36);
            this.btnCancelar.Text = "Limpar";
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // separadorCancelar
            // 
            this.separadorCancelar.Name = "separadorCancelar";
            this.separadorCancelar.Size = new System.Drawing.Size(6, 39);
            // 
            // btnLocalizar
            // 
            this.btnLocalizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocalizar.CheckOnClick = true;
            this.btnLocalizar.Image = global::DaisyPets.UI.Properties.Resources.shell32210;
            this.btnLocalizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLocalizar.ImageTransparentColor = System.Drawing.Color.White;
            this.btnLocalizar.Name = "btnLocalizar";
            this.btnLocalizar.Size = new System.Drawing.Size(84, 36);
            this.btnLocalizar.Text = "Pesquisar";
            this.btnLocalizar.Click += new System.EventHandler(this.BtnLocalizar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(73, 36);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnListar
            // 
            this.btnListar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnListar.Image = global::DaisyPets.UI.Properties.Resources.print_icon;
            this.btnListar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnListar.ImageTransparentColor = System.Drawing.Color.White;
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(75, 36);
            this.btnListar.Text = "Listar";
            this.btnListar.Click += new System.EventHandler(this.BtnListar_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // btnFechar
            // 
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFechar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(74, 36);
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbGeral,
            this.lblMessagem,
            this.lblAlert});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(730, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbGeral
            // 
            this.pbGeral.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pbGeral.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pbGeral.Name = "pbGeral";
            this.pbGeral.Size = new System.Drawing.Size(100, 16);
            this.pbGeral.Visible = false;
            // 
            // lblMessagem
            // 
            this.lblMessagem.Name = "lblMessagem";
            this.lblMessagem.Size = new System.Drawing.Size(0, 17);
            // 
            // lblAlert
            // 
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(0, 17);
            // 
            // frmBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.CaptionBarColor = System.Drawing.Color.SteelBlue;
            this.CaptionBarHeight = 51;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            captionImage1.BackColor = System.Drawing.Color.Transparent;
            captionImage1.Image = global::DaisyPets.UI.Properties.Resources.iconfinder_Streamline_75_185095;
            captionImage1.Location = new System.Drawing.Point(10, 4);
            captionImage1.Name = "MainCaptionImage";
            captionImage1.Size = new System.Drawing.Size(40, 40);
            this.CaptionImages.Add(captionImage1);
            captionLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            captionLabel1.ForeColor = System.Drawing.Color.White;
            captionLabel1.Location = new System.Drawing.Point(60, 2);
            captionLabel1.Name = "Titulo";
            captionLabel1.Size = new System.Drawing.Size(250, 24);
            captionLabel1.Text = "Configuração";
            captionLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            captionLabel2.ForeColor = System.Drawing.Color.White;
            captionLabel2.Location = new System.Drawing.Point(60, 24);
            captionLabel2.Name = "SubTitulo";
            captionLabel2.Size = new System.Drawing.Size(300, 24);
            this.CaptionLabels.Add(captionLabel1);
            this.CaptionLabels.Add(captionLabel2);
            this.ClientSize = new System.Drawing.Size(730, 350);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBase";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBase_FormClosing);
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBase_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton btnNovo;
        protected System.Windows.Forms.ToolStripButton btnSalvar;
        protected System.Windows.Forms.ToolStripButton btnExcluir;
        protected System.Windows.Forms.ToolStripButton btnLocalizar;
        protected System.Windows.Forms.ToolStripSeparator separadorCancelar;
        protected System.Windows.Forms.ToolStripButton btnFechar;
        protected System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton btnListar;
        protected System.Windows.Forms.ToolStripStatusLabel lblMessagem;
        protected System.Windows.Forms.ToolStripStatusLabel lblAlert;
        protected System.Windows.Forms.ToolStripProgressBar pbGeral;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}