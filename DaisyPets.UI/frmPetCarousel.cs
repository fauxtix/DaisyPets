using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;

namespace DaisyPets.UI
{
    public partial class frmPetCarousel : SfForm
    {
        public frmPetCarousel()
        {
            InitializeComponent();
            var resCollection = PetCarousel.ImageListCollection;
        }

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            CarouselImage carouselmage = new CarouselImage();
            carouselmage.ItemImage = Image.FromFile(@"C:\Users\User\OneDrive\Imagens\Imagens da Câmara\20230121_170821.jpg");
            PetCarousel.ImageListCollection.Add(carouselmage);
            PetCarousel.ImageSlides = true;
        }
    }
}
