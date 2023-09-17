namespace DaisyPets.UI
{
    partial class frmPetCarousel
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
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage1 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetCarousel));
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage2 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            PetCarousel = new Syncfusion.Windows.Forms.Tools.Carousel();
            panel1 = new Panel();
            btnLoadImages = new Syncfusion.WinForms.Controls.SfButton();
            txtFilePath = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnBrowseDocument = new Button();
            txtID = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtPetId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            cboPets = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            dtpPhotoDate = new DateTimePicker();
            autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            dgvGallery = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            IdPet = new DataGridViewTextBoxColumn();
            Data = new DataGridViewTextBoxColumn();
            Imagem = new DataGridViewTextBoxColumn();
            NomePet = new DataGridViewTextBoxColumn();
            openFileDialog1 = new OpenFileDialog();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtFilePath).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPetId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cboPets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGallery).BeginInit();
            SuspendLayout();
            // 
            // PetCarousel
            // 
            PetCarousel.BackColor = Color.WhiteSmoke;
            PetCarousel.CanOverrideStyle = true;
            PetCarousel.HighlightColor = Color.White;
            carouselImage1.ItemImage = (Image)resources.GetObject("carouselImage1.ItemImage");
            carouselImage2.ItemImage = (Image)resources.GetObject("carouselImage2.ItemImage");
            PetCarousel.ImageListCollection.Add(carouselImage1);
            PetCarousel.ImageListCollection.Add(carouselImage2);
            PetCarousel.ImageshadeColor = Color.Black;
            PetCarousel.ImageSlides = false;
            PetCarousel.Location = new Point(0, 0);
            PetCarousel.Name = "PetCarousel";
            PetCarousel.PadX = 1;
            PetCarousel.PadY = 1;
            PetCarousel.Perspective = 4F;
            PetCarousel.RotateAlways = false;
            PetCarousel.ShowImagePreview = true;
            PetCarousel.ShowImageShadow = true;
            PetCarousel.Size = new Size(682, 675);
            PetCarousel.TabIndex = 0;
            PetCarousel.ThemeName = "Metro";
            PetCarousel.TouchTransitionSpeed = 1F;
            PetCarousel.UseOriginalImageinPreview = true;
            PetCarousel.VisualStyle = Syncfusion.Windows.Forms.Tools.CarouselVisualStyle.Metro;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnLoadImages);
            panel1.Controls.Add(txtFilePath);
            panel1.Controls.Add(btnBrowseDocument);
            panel1.Controls.Add(txtID);
            panel1.Controls.Add(txtPetId);
            panel1.Controls.Add(cboPets);
            panel1.Controls.Add(autoLabel5);
            panel1.Controls.Add(dtpPhotoDate);
            panel1.Controls.Add(autoLabel6);
            panel1.Controls.Add(autoLabel2);
            panel1.Controls.Add(dgvGallery);
            panel1.Location = new Point(688, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(411, 672);
            panel1.TabIndex = 1;
            // 
            // btnLoadImages
            // 
            btnLoadImages.AccessibleName = "Button";
            btnLoadImages.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadImages.Location = new Point(31, 151);
            btnLoadImages.Name = "btnLoadImages";
            btnLoadImages.Size = new Size(144, 37);
            btnLoadImages.Style.Image = Properties.Resources.Be_Folder_icon;
            btnLoadImages.TabIndex = 41;
            btnLoadImages.Text = "Add record";
            btnLoadImages.UseVisualStyleBackColor = true;
            btnLoadImages.Click += btnLoadImages_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.BeforeTouchSize = new Size(68, 27);
            txtFilePath.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtFilePath.Location = new Point(246, 94);
            txtFilePath.Margin = new Padding(3, 4, 3, 4);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(142, 27);
            txtFilePath.TabIndex = 40;
            // 
            // btnBrowseDocument
            // 
            btnBrowseDocument.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnBrowseDocument.Location = new Point(101, 94);
            btnBrowseDocument.Name = "btnBrowseDocument";
            btnBrowseDocument.Size = new Size(133, 29);
            btnBrowseDocument.TabIndex = 39;
            btnBrowseDocument.Text = "Select";
            btnBrowseDocument.UseVisualStyleBackColor = true;
            btnBrowseDocument.Click += btnBrowseDocument_Click;
            // 
            // txtID
            // 
            txtID.BeforeTouchSize = new Size(68, 27);
            txtID.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtID.Location = new Point(246, 56);
            txtID.Margin = new Padding(3, 4, 3, 4);
            txtID.Name = "txtID";
            txtID.Size = new Size(68, 27);
            txtID.TabIndex = 38;
            txtID.Visible = false;
            // 
            // txtPetId
            // 
            txtPetId.BeforeTouchSize = new Size(68, 27);
            txtPetId.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtPetId.Location = new Point(320, 56);
            txtPetId.Margin = new Padding(3, 4, 3, 4);
            txtPetId.Name = "txtPetId";
            txtPetId.Size = new Size(68, 27);
            txtPetId.TabIndex = 37;
            txtPetId.Visible = false;
            // 
            // cboPets
            // 
            cboPets.BeforeTouchSize = new Size(266, 28);
            cboPets.FlatStyle = Syncfusion.Windows.Forms.Tools.ComboFlatStyle.System;
            cboPets.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboPets.Location = new Point(101, 23);
            cboPets.Margin = new Padding(3, 4, 3, 4);
            cboPets.Name = "cboPets";
            cboPets.Size = new Size(266, 28);
            cboPets.TabIndex = 36;
            cboPets.Text = "Pet";
            cboPets.SelectedIndexChanged += cboPets_SelectedIndexChanged;
            // 
            // autoLabel5
            // 
            autoLabel5.BackColor = Color.White;
            autoLabel5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel5.ForeColor = SystemColors.ControlText;
            autoLabel5.Location = new Point(31, 99);
            autoLabel5.Name = "autoLabel5";
            autoLabel5.Size = new Size(40, 20);
            autoLabel5.TabIndex = 35;
            autoLabel5.Text = "Foto";
            // 
            // dtpPhotoDate
            // 
            dtpPhotoDate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dtpPhotoDate.Format = DateTimePickerFormat.Short;
            dtpPhotoDate.Location = new Point(101, 58);
            dtpPhotoDate.Name = "dtpPhotoDate";
            dtpPhotoDate.Size = new Size(115, 27);
            dtpPhotoDate.TabIndex = 33;
            // 
            // autoLabel6
            // 
            autoLabel6.BackColor = Color.White;
            autoLabel6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel6.ForeColor = SystemColors.ControlText;
            autoLabel6.Location = new Point(31, 63);
            autoLabel6.Name = "autoLabel6";
            autoLabel6.Size = new Size(41, 20);
            autoLabel6.TabIndex = 32;
            autoLabel6.Text = "Data";
            // 
            // autoLabel2
            // 
            autoLabel2.BackColor = Color.White;
            autoLabel2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            autoLabel2.ForeColor = SystemColors.ControlText;
            autoLabel2.Location = new Point(31, 28);
            autoLabel2.Name = "autoLabel2";
            autoLabel2.Size = new Size(30, 20);
            autoLabel2.TabIndex = 31;
            autoLabel2.Text = "Pet";
            // 
            // dgvGallery
            // 
            dgvGallery.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGallery.Columns.AddRange(new DataGridViewColumn[] { Id, IdPet, Data, Imagem, NomePet });
            dgvGallery.Location = new Point(31, 210);
            dgvGallery.Name = "dgvGallery";
            dgvGallery.RowTemplate.Height = 25;
            dgvGallery.Size = new Size(373, 436);
            dgvGallery.TabIndex = 5;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // IdPet
            // 
            IdPet.DataPropertyName = "IdPet";
            IdPet.HeaderText = "IdPet";
            IdPet.Name = "IdPet";
            IdPet.ReadOnly = true;
            IdPet.Visible = false;
            // 
            // Data
            // 
            Data.DataPropertyName = "Data";
            Data.HeaderText = "Data";
            Data.Name = "Data";
            Data.ReadOnly = true;
            Data.Width = 80;
            // 
            // Imagem
            // 
            Imagem.DataPropertyName = "Imagem";
            Imagem.HeaderText = "Local";
            Imagem.Name = "Imagem";
            Imagem.ReadOnly = true;
            Imagem.Width = 150;
            // 
            // NomePet
            // 
            NomePet.DataPropertyName = "NomePet";
            NomePet.HeaderText = "Pet";
            NomePet.Name = "NomePet";
            NomePet.ReadOnly = true;
            NomePet.Width = 200;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmPetCarousel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 694);
            Controls.Add(panel1);
            Controls.Add(PetCarousel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IconSize = new Size(36, 36);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPetCarousel";
            StartPosition = FormStartPosition.CenterScreen;
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            Text = "Daisy Pets - Carousel";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtFilePath).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtID).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPetId).EndInit();
            ((System.ComponentModel.ISupportInitialize)cboPets).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGallery).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.Carousel PetCarousel;
        private Panel panel1;
        private DataGridView dgvGallery;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DateTimePicker dtpPhotoDate;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv cboPets;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPetId;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtID;
        private Button btnBrowseDocument;
        private OpenFileDialog openFileDialog1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtFilePath;
        private Syncfusion.WinForms.Controls.SfButton btnLoadImages;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn IdPet;
        private DataGridViewTextBoxColumn Data;
        private DataGridViewTextBoxColumn Imagem;
        private DataGridViewTextBoxColumn NomePet;
    }
}