using DaisyPets.Core.Application.ViewModels.Pdfs;
using DaisyPets.UI.Expenses;
using DaisyPets.UI.Properties;
using Syncfusion.Windows.Forms;

namespace DaisyPets.UI
{
    public partial class frmMain : MetroForm
    {
        private bool _isApplicationExit = false;

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
                    Application.Exit();
                }
            }

        }

        private bool DoLogOff()
        {

            DialogResult dr = MessageBoxAdv.Show("Quer mesmo sair da aplicação?", "HouseRentalSoft",
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
    }
}

