using DaisyPets.Core.Application.ViewModels.Pdfs;
using Syncfusion.Windows.Forms;

namespace DaisyPets.UI
{
    public partial class frmPdfViewer : MetroForm
    {
        public frmPdfViewer()
        {
            InitializeComponent();
            CaptionLabels[1].Text = FormParameters.TituloPdf;
            pdfViewerControl1.Load(FormParameters.NomePdf);
        }
    }
}
