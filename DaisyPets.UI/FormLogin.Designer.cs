namespace DaisyPets.UI
{
    partial class FormLogin
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
            panel1 = new Panel();
            label3 = new Label();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            txtUserID = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            btnLogin = new Button();
            linkpass = new LinkLabel();
            btncerrar = new PictureBox();
            btnminimizar = new PictureBox();
            errorProvider1 = new ErrorProvider(components);
            splashControl1 = new Syncfusion.Windows.Forms.Tools.SplashControl();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btncerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnminimizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 100, 182);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(233, 381);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(13, 353);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(59, 19);
            label3.TabIndex = 1;
            label3.Text = "My Pets";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(50, 120);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(113, 30);
            label2.TabIndex = 1;
            label2.Text = "Daisy Pets";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.catsanddogs;
            pictureBox3.Location = new Point(41, 13);
            pictureBox3.Margin = new Padding(4, 3, 4, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(133, 104);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // txtUserID
            // 
            txtUserID.BackColor = Color.FromArgb(39, 57, 80);
            txtUserID.BorderStyle = BorderStyle.None;
            txtUserID.Cursor = Cursors.IBeam;
            txtUserID.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtUserID.ForeColor = Color.Silver;
            txtUserID.Location = new Point(282, 92);
            txtUserID.Margin = new Padding(4, 3, 4, 3);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(260, 20);
            txtUserID.TabIndex = 1;
            txtUserID.Text = "Utilizador";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(39, 57, 80);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPassword.ForeColor = Color.Silver;
            txtPassword.Location = new Point(285, 165);
            txtPassword.Margin = new Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(260, 20);
            txtPassword.TabIndex = 2;
            txtPassword.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(355, 33);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 32);
            label1.TabIndex = 4;
            label1.Text = "LOGIN";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(33, 53, 73);
            btnLogin.FlatAppearance.BorderColor = Color.FromArgb(85, 159, 127);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 20, 20);
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 118, 126);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.LightGray;
            btnLogin.Location = new Point(300, 245);
            btnLogin.Margin = new Padding(4, 3, 4, 3);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(223, 46);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "ACEDER";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // linkpass
            // 
            linkpass.ActiveLinkColor = Color.FromArgb(0, 122, 204);
            linkpass.AutoSize = true;
            linkpass.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            linkpass.LinkColor = Color.DarkGray;
            linkpass.Location = new Point(310, 294);
            linkpass.Margin = new Padding(4, 0, 4, 0);
            linkpass.Name = "linkpass";
            linkpass.Size = new Size(182, 17);
            linkpass.TabIndex = 0;
            linkpass.TabStop = true;
            linkpass.Text = "Esqueceu-se da password?";
            // 
            // btncerrar
            // 
            btncerrar.Cursor = Cursors.Hand;
            btncerrar.Location = new Point(594, 3);
            btncerrar.Margin = new Padding(4, 3, 4, 3);
            btncerrar.Name = "btncerrar";
            btncerrar.Size = new Size(18, 17);
            btncerrar.SizeMode = PictureBoxSizeMode.Zoom;
            btncerrar.TabIndex = 7;
            btncerrar.TabStop = false;
            // 
            // btnminimizar
            // 
            btnminimizar.Cursor = Cursors.Hand;
            btnminimizar.Location = new Point(565, 3);
            btnminimizar.Margin = new Padding(4, 3, 4, 3);
            btnminimizar.Name = "btnminimizar";
            btnminimizar.Size = new Size(18, 17);
            btnminimizar.SizeMode = PictureBoxSizeMode.Zoom;
            btnminimizar.TabIndex = 8;
            btnminimizar.TabStop = false;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // splashControl1
            // 
            splashControl1.HideHostForm = true;
            splashControl1.HostForm = this;
            splashControl1.SplashImage = Properties.Resources.LeaseImg;
            splashControl1.Text = "";
            splashControl1.TimerInterval = 2000;
            splashControl1.TransparentColor = Color.Empty;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(39, 57, 80);
            ClientSize = new Size(634, 381);
            Controls.Add(btnminimizar);
            Controls.Add(btncerrar);
            Controls.Add(linkpass);
            Controls.Add(btnLogin);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUserID);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormLogin";
            Opacity = 0.9D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            TopMost = true;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)btncerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnminimizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Panel panel1;
        private TextBox txtUserID;
        private TextBox txtPassword;
        private Label label1;
        private Button btnLogin;
        private LinkLabel linkpass;
        private PictureBox pictureBox3;
        private PictureBox btncerrar;
        private PictureBox btnminimizar;
        private ErrorProvider errorProvider1;
        private Label label2;
        private Label label3;
        private Syncfusion.Windows.Forms.Tools.SplashControl splashControl1;

    }
}