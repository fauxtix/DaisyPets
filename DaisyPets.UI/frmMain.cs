using DaisyPets.Core.Application.ViewModels.Pdfs;
using DaisyPets.UI.Expenses;
using DaisyPets.UI.LookupTables;
using DaisyPets.UI.Properties;
using DaisyPets.UI.Stats;
using Microsoft.Extensions.Logging;
using Syncfusion.Windows.Forms;

namespace DaisyPets.UI
{
    public partial class frmMain : MetroForm
    {

        private bool _isApplicationExit = false;
        private string tableName = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        frmPets? fPets;
        private void btnPets_Click(object sender, EventArgs e)
        {
            if (fPets == null)
            {
                fPets = new frmPets();
                fPets.MdiParent = this;
                fPets.FormClosed += Pets_FormClosed;
                fPets.Show();
            }
            else
            { fPets.Activate(); }
        }


        private void Pets_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPets = null;
        }

        frmPetCarousel? fCarousel;
        private void optGallery_Click(object sender, EventArgs e)
        {
            {
                if (fCarousel == null)
                {
                    fCarousel = new frmPetCarousel();
                    fCarousel.MdiParent = this;
                    fCarousel.FormClosed += Carousel_FormClosed;
                    fCarousel.Show();
                }
                else
                { fCarousel.Activate(); }
            }

        }

        private void Carousel_FormClosed(object sender, FormClosedEventArgs e)
        {
            fCarousel = null;
        }


        frmContacto? fContactos;

        private void btnContactos_Click(object sender, EventArgs e)
        {
            if (fContactos == null)
            {
                fContactos = new frmContacto();
                fContactos.MdiParent = this;
                fContactos.FormClosed += Contacts_FormClosed;
                fContactos.Show();
            }
            else
            { fContactos.Activate(); }
        }

        private void Contacts_FormClosed(object sender, FormClosedEventArgs e)
        {
            fContactos = null;
        }

        frmExpensesMain? fExpenses;
        private void btnExpensesDonations_Click(object sender, EventArgs e)
        {
            if (fExpenses == null)
            {
                fExpenses = new frmExpensesMain();
                fExpenses.MdiParent = this;
                fExpenses.FormClosed += Expenses_FormClosed;
                fExpenses.Show();
            }
            else
            { fExpenses.Activate(); }
        }

        private void Expenses_FormClosed(object sender, FormClosedEventArgs e)
        {
            fExpenses = null;
        }

        frmPdfViewer? fPdf;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (fPdf == null)
            {
                FormParameters.TituloPdf = "Estrutura da base de dados";
                FormParameters.NomePdf = GetDatabase_Info_Pdf();

                fPdf = new frmPdfViewer();
                fPdf.MdiParent = this;
                fPdf.FormClosed += CreatePdfs_FormClosed;
                fPdf.Show();
            }
            else
            { fPdf.Activate(); }
        }

        private void CreatePdfs_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPdf = null;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isApplicationExit)
            {
                if (!DoLogOff())
                    e.Cancel = true;
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }

        }

        private bool DoLogOff()
        {

            DialogResult dr = MessageBoxAdv.Show("Quer mesmo sair da aplicação?", "Daisy Pets",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool retValue = false;
            if (dr == DialogResult.Yes)
            {
                _isApplicationExit = true;
                (new ApplicationContext()).Dispose();
                retValue = true;
            }
            else
                retValue = false;

            return retValue;
        }

        private string GetDatabase_Info_Pdf()
        {
            string url = $"https://localhost:7161/api/mailmerge/DatabaseStructure";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetStringAsync(url);
                    task.Wait();

                    var response = task.Result;
                    task.Dispose();

                    return response;
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no Api ({ex.Message})", "Rações");
                return "";
            }
        }

        private void optCategoriaDespesas_Click(object sender, EventArgs e)
        {
            tableName = "CategoriaDespesa";
            OpenSettingsForm();

        }
        private void optRacas_Click(object sender, EventArgs e)
        {
            tableName = "Raca";
            OpenSettingsForm();
        }

        private void optSituacao_Click(object sender, EventArgs e)
        {
            tableName = "Situacao";
            OpenSettingsForm();
        }

        private void optTemperamento_Click(object sender, EventArgs e)
        {
            tableName = "Temperamento";
            OpenSettingsForm();
        }

        private void OpenSettingsForm()
        {
            frmLookupTablesManager frmManut = new frmLookupTablesManager(tableName);
            var resp = frmManut.ShowDialog();
        }

        private void optTipoDespesa_Click(object sender, EventArgs e)
        {
            frmExpenseTypes fExpenseTypes = new frmExpenseTypes();
            var resp = fExpenseTypes.ShowDialog();
        }

        frmPetStats? fStats;
        private void optStats_Click(object sender, EventArgs e)
        {
            if (fStats == null)
            {
                fStats = new frmPetStats();
                fStats.MdiParent = this;
                fStats.FormClosed  += Stats_FormClosed;
                fStats.Show();
            }
            else
            { fStats.Activate(); }
        }

        private void Stats_FormClosed(object sender, FormClosedEventArgs e)
        {
            fStats = null;
        }

    }
}

