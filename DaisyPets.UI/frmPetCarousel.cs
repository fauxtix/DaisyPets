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
    }
}
