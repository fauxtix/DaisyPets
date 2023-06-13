using DaisyPets.Core.Application.ViewModels.Pdfs;
using Syncfusion.Windows.Forms;

namespace DaisyPets.UI
{
    public partial class frmPdfViewer : MetroForm
    {
        public frmPdfViewer()
        {
            InitializeComponent();
            try
            {
                CaptionLabels[1].Text = FormParameters.TituloPdf;
                pdfViewerControl1.Load(FormParameters.NomePdf);

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show(ex.Message, "Erro ao carregar pdf. Tente mais tarde, p.f.");               
            }
        }

        private void frmPdfViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
