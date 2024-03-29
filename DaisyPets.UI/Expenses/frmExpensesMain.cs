﻿using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.UI.ApiServices;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Net.Http.Json;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmExpensesMain : MetroForm
    {
        private int ExpenseId = 0;
        private int ExpenseCategoryId;
        private string ExpensesApiEndpoint { get; }
        private string LookupTablesApiEndpoint { get; }

        private decimal totalExpenses { get; set; }
        private decimal totalFilteredExpenses { get; set; }
        private int _previousIndex;
        private bool _sortDirection;
        private IEnumerable<DespesaVM> listOfExpenses { get; set; }

        private DateTime dStart;
        private DateTime dEnd;

        public frmExpensesMain()
        {
            InitializeComponent();
            dgvExpenses.AutoGenerateColumns = false;
            totalFilteredExpenses = 0M;
            ExpensesApiEndpoint = AccessSettingsService.DespesasEndpoint;
            LookupTablesApiEndpoint = AccessSettingsService.LookupTablesEndpoint;

            listOfExpenses = GetExpenses();

            FillCombo(cboCategoryTypes, "CategoriaDespesa");

            FillGrid();
            ShowTotals();
            ClearForm();
        }


        private void FillGrid()
        {
            listOfExpenses = GetExpenses();
            dgvExpenses.DataSource = listOfExpenses?.ToList();
            if (dgvExpenses.RowCount > 0)
            {
                dgvExpenses.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvExpenses.Rows[0].Cells[0].Value);
            }
        }

        private void FillCombo(ComboBox comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            string url = $"{LookupTablesApiEndpoint}/GetAllRecords/{dataTable}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>(url);
                var response = task.Result;

                task.Wait();

                if (response != null)
                {
                    cboCategoryTypes.Items.Add(new DictionaryEntry(0, " == Todas =="));
                    response = response.OrderBy(o => o.Descricao).ToList();
                    foreach (var entry in response)
                    {
                        comboBox.Items.Add(new DictionaryEntry(entry.Id, entry.Descricao));
                    }
                    comboBox.ValueMember = "Key";
                    comboBox.DisplayMember = "Value";
                    comboBox.SelectedIndex = 0;
                }
            }
        }


        private IEnumerable<DespesaVM> GetExpenses()
        {
            string url = $"{ExpensesApiEndpoint}/AllVMAsync";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var output = response.Content.ReadAsAsync<IEnumerable<DespesaVM>>().Result;
                        if (output != null)
                        {
                            return output;
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
                ShowTotals();
            }
        }

        private async Task<DespesaDto> GeExpense(int Id)
        {
            string url = $"{ExpensesApiEndpoint}/{Id}";
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

            ExpenseId = DataFormat.GetInteger(dgvExpenses.Rows[e.RowIndex].Cells["Id"].Value);
            var expenseRecord = await GeExpense(ExpenseId);


            frmNewEntry fNewEntry = new frmNewEntry(expenseRecord);
            var resp = fNewEntry.ShowDialog();
            if (resp == DialogResult.OK)
            {
                FillGrid();
                ShowTotals();
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

        private void checkBoxFilterDateTime_CheckStateChanged(object sender, EventArgs e)
        {
            var dStart = filterFromDateTime.Value;

            cboCategoryTypes.Enabled = checkBoxFilterDateTime.Checked;

            labelFilterFrom.Enabled = checkBoxFilterDateTime.Checked;
            labelFilterTo.Enabled = checkBoxFilterDateTime.Checked;
            filterFromDateTime.Enabled = checkBoxFilterDateTime.Checked;
            filterToDateTime.Enabled = checkBoxFilterDateTime.Checked;
            if (checkBoxFilterDateTime.Checked == false)
            {
                cboCategoryTypes.Enabled = true;
                FillGrid(); // all expenses
            }
            else
            {
                cboCategoryTypes.Enabled = false;

            }
        }

        private void filterFromDateTime_ValueChanged(object sender, EventArgs e)
        {
        }

        private void filterToDateTime_ValueChanged(object sender, EventArgs e)
        {
            var fromDate = filterFromDateTime.Value;
            var toDate = filterToDateTime.Value;
            listOfExpenses = listOfExpenses.Where(o => DateTime.Parse(o.DataMovimento) >= fromDate && DateTime.Parse(o.DataMovimento) <= toDate);
            if (listOfExpenses.Any())
            {
                dgvExpenses.DataSource = listOfExpenses?.ToList();
            }
        }

        private void dgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            ExpenseId = DataFormat.GetInteger(dgvExpenses.Rows[e.RowIndex].Cells["Id"].Value);
            SetToolbar(OpcoesRegisto.Gravar);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.Rows.Count == 0)
            {
                MessageBoxAdv.Show("Sem registos para apagar...", "Apagar despesa");
                SetToolbar_Clear();
                return;
            }
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                $"Apagar registo de despesa ({ExpenseId})", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeleteExpense();

        }

        private async Task DeleteExpense()
        {
            string url = $"{ExpensesApiEndpoint}/{ExpenseId}";
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    MessageBoxAdv.Show("Erro ao apagar registo", "Contactos");
                    return;
                }
                else
                {
                    FillGrid();

                    MessageBoxAdv.Show("Registo apagado com sucesso");
                    ShowTotals();

                    if (dgvExpenses.RowCount > 0)
                    {
                        ExpenseId = DataFormat.GetInteger(dgvExpenses.Rows[0].Cells["Id"].Value);

                        dgvExpenses.Rows[0].Selected = true;
                    }
                    else
                    {
                        dgvExpenses.DataSource = null;
                        ClearForm();
                    }
                }
            }

        }

        private void cboCategoryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoryTypes.SelectedItem == null)
                return;

            ExpenseCategoryId = DataFormat.GetInteger(((DictionaryEntry)(cboCategoryTypes.SelectedItem)).Key);
            var filteredExpenses = listOfExpenses.Where(x => x.IdCategoriaDespesa == ExpenseCategoryId);

            if (ExpenseCategoryId == 0)
            {
                dgvExpenses.DataSource = listOfExpenses;
                lblTotalFilteredExpenses.Text = "0";
            }
            else
            {
                dgvExpenses.DataSource = filteredExpenses?.ToList();
            }

            ShowTotals();
        }

        private void ShowTotals()
        {
            totalExpenses = listOfExpenses.Sum(o => o.ValorPago);
            lblTotalExpenses.Text = String.Format("({0:N2})", totalExpenses);
            totalFilteredExpenses = listOfExpenses.Where(o => o.IdCategoriaDespesa == ExpenseCategoryId).Sum(x => x.ValorPago);
            lblTotalFilteredExpenses.Text = String.Format("({0:N2})", totalFilteredExpenses);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCalculator frmCalculator = new frmCalculator();
            var resp = frmCalculator.ShowDialog();
        }
    }
}

