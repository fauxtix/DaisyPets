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
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage3 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage4 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage5 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage6 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage7 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage8 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage9 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            Syncfusion.Windows.Forms.Tools.CarouselImage carouselImage10 = new Syncfusion.Windows.Forms.Tools.CarouselImage();
            PetCarousel = new Syncfusion.Windows.Forms.Tools.Carousel();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // PetCarousel
            // 
            PetCarousel.BackColor = Color.WhiteSmoke;
            PetCarousel.CanOverrideStyle = true;
            PetCarousel.HighlightColor = Color.White;
            carouselImage1.ItemImage = (Image)resources.GetObject("carouselImage1.ItemImage");
            carouselImage2.ItemImage = (Image)resources.GetObject("carouselImage2.ItemImage");
            carouselImage3.ItemImage = (Image)resources.GetObject("carouselImage3.ItemImage");
            carouselImage4.ItemImage = (Image)resources.GetObject("carouselImage4.ItemImage");
            carouselImage5.ItemImage = (Image)resources.GetObject("carouselImage5.ItemImage");
            carouselImage6.ItemImage = (Image)resources.GetObject("carouselImage6.ItemImage");
            carouselImage7.ItemImage = (Image)resources.GetObject("carouselImage7.ItemImage");
            carouselImage8.ItemImage = (Image)resources.GetObject("carouselImage8.ItemImage");
            carouselImage9.ItemImage = (Image)resources.GetObject("carouselImage9.ItemImage");
            carouselImage10.ItemImage = (Image)resources.GetObject("carouselImage10.ItemImage");
            PetCarousel.ImageListCollection.Add(carouselImage1);
            PetCarousel.ImageListCollection.Add(carouselImage2);
            PetCarousel.ImageListCollection.Add(carouselImage3);
            PetCarousel.ImageListCollection.Add(carouselImage4);
            PetCarousel.ImageListCollection.Add(carouselImage5);
            PetCarousel.ImageListCollection.Add(carouselImage6);
            PetCarousel.ImageListCollection.Add(carouselImage7);
            PetCarousel.ImageListCollection.Add(carouselImage8);
            PetCarousel.ImageListCollection.Add(carouselImage9);
            PetCarousel.ImageListCollection.Add(carouselImage10);
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
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.Carousel PetCarousel;
        private Panel panel1;
    }
}