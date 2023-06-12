using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using Syncfusion.Windows.Forms;
using System.Net.Http.Json;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmExpensesMain : MetroForm
    {
        private int IdExpense = 0;
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
            DespesaDto expenseDto = new DespesaDto()
            {
                DataCriacao = DateTime.Now.Date.ToShortDateString(),
                DataMovimento = DateTime.Now.Date.ToShortDateString(),
                Descricao = "",
                IdCategoriaDespesa = -1,
                IdTipoDespesa = -1,
                Notas = "",
                TipoMovimento = "S",
                ValorPago = 0M
            };

            frmNewEntry fNewEntry = new frmNewEntry(expenseDto);
            var resp = fNewEntry.ShowDialog();
            if (resp == DialogResult.OK)
            {
                FillGrid();
            }
        }

        private async Task<DespesaDto> GeExpense(int Id)
        {
            string url = $"https://localhost:7161/api/despesa/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _expense = await httpClient.GetFromJsonAsync<DespesaDto>(url);
                return _expense ?? new DespesaDto();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            SetToolbar_Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SetToolbar(OpcoesRegisto.Gravar);

        }

        private async void dgvExpenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdExpense = DataFormat.GetInteger(dgvExpenses.Rows[e.RowIndex].Cells["Id"].Value);
            var expenseRecord = await GeExpense(IdExpense);


            frmNewEntry fNewEntry = new frmNewEntry(expenseRecord);
            var resp = fNewEntry.ShowDialog();
            if (resp == DialogResult.OK)
            {
                FillGrid();
            }

        }

        private void dgvExpenses_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvExpenses.DataSource = SortData(
                (List<DespesaVM>)dgvExpenses.DataSource, dgvExpenses.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        private List<DespesaVM> SortData(List<DespesaVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

    }
}

