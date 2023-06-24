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
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage11 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPetCarousel));
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage12 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage13 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage14 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage15 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage16 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage17 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage18 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage19 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage20 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            PetCarousel = new Syncfusion.Windows.Forms.Tools.Carousel();
            panel1 = new Panel();
            btnLoadImages = new Syncfusion.WinForms.Controls.SfButton();
            SuspendLayout();
            // 
            // PetCarousel
            // 
            PetCarousel.BackColor = Color.WhiteSmoke;
            PetCarousel.CanOverrideStyle = true;
            PetCarousel.HighlightColor = Color.White;
            carouselImage11.ItemImage = (Image)resources.GetObject("carouselImage11.ItemImage");
            carouselImage12.ItemImage = (Image)resources.GetObject("carouselImage12.ItemImage");
            carouselImage13.ItemImage = (Image)resources.GetObject("carouselImage13.ItemImage");
            carouselImage14.ItemImage = (Image)resources.GetObject("carouselImage14.ItemImage");
            carouselImage15.ItemImage = (Image)resources.GetObject("carouselImage15.ItemImage");
            carouselImage16.ItemImage = (Image)resources.GetObject("carouselImage16.ItemImage");
            carouselImage17.ItemImage = (Image)resources.GetObject("carouselImage17.ItemImage");
            carouselImage18.ItemImage = (Image)resources.GetObject("carouselImage18.ItemImage");
            carouselImage19.ItemImage = (Image)resources.GetObject("carouselImage19.ItemImage");
            carouselImage20.ItemImage = (Image)resources.GetObject("carouselImage20.ItemImage");
            PetCarousel.ImageListCollection.Add(carouselImage11);
            PetCarousel.ImageListCollection.Add(carouselImage12);
            PetCarousel.ImageListCollection.Add(carouselImage13);
            PetCarousel.ImageListCollection.Add(carouselImage14);
            PetCarousel.ImageListCollection.Add(carouselImage15);
            PetCarousel.ImageListCollection.Add(carouselImage16);
            PetCarousel.ImageListCollection.Add(carouselImage17);
            PetCarousel.ImageListCollection.Add(carouselImage18);
            PetCarousel.ImageListCollection.Add(carouselImage19);
            PetCarousel.ImageListCollection.Add(carouselImage20);
            PetCarousel.ImageshadeColor = Color.Black;
            PetCarousel.ImageSlides = true;
            PetCarousel.Location = new Point(0, 0);
            PetCarousel.Name = "PetCarousel";
            PetCarousel.PadX = 1;
            PetCarousel.PadY = 1;
            PetCarousel.Perspective = 4F;
            PetCarousel.RotateAlways = false;
            PetCarousel.ShowImagePreview = true;
            PetCarousel.ShowImageShadow = true;
            PetCarousel.Size = new Size(880, 675);
            PetCarousel.TabIndex = 0;
            PetCarousel.ThemeName = "Metro";
            PetCarousel.TouchTransitionSpeed = 1F;
            PetCarousel.UseOriginalImageinPreview = true;
            PetCarousel.VisualStyle = Syncfusion.Windows.Forms.Tools.CarouselVisualStyle.Metro;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(907, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(170, 83);
            panel1.TabIndex = 1;
            // 
            // btnLoadImages
            // 
            btnLoadImages.AccessibleName = "Button";
            btnLoadImages.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadImages.Location = new Point(921, 132);
            btnLoadImages.Name = "btnLoadImages";
            btnLoadImages.Size = new Size(144, 52);
            btnLoadImages.Style.Image = Properties.Resources.Be_Folder_icon;
            btnLoadImages.TabIndex = 2;
            btnLoadImages.Text = "Load images";
            btnLoadImages.UseVisualStyleBackColor = true;
            btnLoadImages.Click += btnLoadImages_Click;
            // 
            // frmPetCarousel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 694);
            Controls.Add(btnLoadImages);
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
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.Carousel PetCarousel;
        private Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton btnLoadImages;
    }
}