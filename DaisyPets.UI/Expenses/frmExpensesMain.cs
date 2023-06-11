using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using Syncfusion.Windows.Forms;
using System.Collections;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmExpensesMain : MetroForm
    {
        private int IdExpense = 0;
        private string IdTipoMovimento = "S";
        private int IdCategoriaDespesa = 0;
        private int IdTipoDespesa = 0;
        private int _previousIndex;
        private bool _sortDirection;

        public frmExpensesMain()
        {
            InitializeComponent();
            dgvExpenses.AutoGenerateColumns = false;
            FillGrid();
        }

        private IEnumerable<DespesaVM> GetExpenses()
        {
            IEnumerable<DespesaVM> expenses;
            string url = $"https://localhost:7161/api/Despesa/AllVMAsync";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        expenses = response.Content.ReadAsAsync<IEnumerable<DespesaVM>>().Result;
                        if (expenses != null)
                        {
                            return expenses;
                        }
                        else
                        {
                            return Enumerable.Empty<DespesaVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();

                    return Enumerable.Empty<DespesaVM>();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API de despesas({ex.Message})", "Preenchimento de grelha");
                return Enumerable.Empty<DespesaVM>();
            }
        }

        private void FillGrid()
        {
            IEnumerable<DespesaVM> expenses = GetExpenses();
            dgvExpenses.DataSource = expenses?.ToList();
            if (dgvExpenses.RowCount > 0)
            {
                //dgvExpenses.CurrentCell = dgvExpenses.Rows[0].Cells[0];
                dgvExpenses.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvExpenses.Rows[0].Cells[0].Value);
            }

        }

        private void SetToolbar(OpcoesRegisto opt)
        {
            if (opt == OpcoesRegisto.Gravar)
            {
                btnUpdate.Enabled = true;
                btnClear.Enabled = true;
                btnDelete.Enabled = true;
                btnInsert.Enabled = false;
            }
            else if (opt == OpcoesRegisto.Inserir)
            {
                btnClear.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnInsert.Enabled = true;
            }
        }

        private void SetToolbar_Clear()
        {
            // mostra apenas opções para criar registo ou sair
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private List<string> ValidateData()
        {

            try
            {
                DespesaDto expenset2Validate = new()
                {
                    Nome = txtNome.Text,
                    Chipado = chkChipado.Checked ? 1 : 0,
                    NumeroChip = txtNumeroChip.Text,
                    DataChip = dtpChip.Value.ToShortDateString(),
                    IdEspecie = cboEspecies.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboEspecies.SelectedItem)).Key) : -1,
                    IdRaca = cboRaca.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboRaca.SelectedItem)).Key) : -1,
                    IdSituacao = cboSituacao.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboSituacao.SelectedItem)).Key) : -1,
                    DataNascimento = dtpDataNascimento.Text,
                    IdTamanho = cboTamanho.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboTamanho.SelectedItem)).Key) : -1,
                    IdTemperamento = cboTemperamento.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboTemperamento.SelectedItem)).Key) : -1
                };

                string url = $"https://localhost:7161/api/Pets/ValidatePets";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync<PetDto>(url, pet2Validate);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();

                    if (response.IsSuccessStatusCode == false)
                    {
                        var errors = response.Content.ReadAsAsync<List<string>>().Result;
                        if (errors.Count() > 0)
                        {
                            return errors;
                        }

                        else

                            return new List<string>();
                    }

                    return new List<string>();

                    //using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, pet2Validate))
                    //{
                    //    var isSuccess = response.IsSuccessStatusCode;

                    //    var errors = response.Content.ReadAsAsync<List<string>>().Result;
                    //    return errors ?? new List<string>();
                    //}

                }
            }
            catch (Exception ex)
            {
                var ddd = ex.Message;
                throw;
            }
        }

    }
}
