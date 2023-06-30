using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Domain;
using DaisyPets.UI.ApiServices;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmExpenseTypes : MetroForm
    {
        protected TipoDespesaDto SelectedTipoDespesa = new();
        protected IEnumerable<LookupTableVM>? CategoriesList { get; set; }

        protected TipoDespesaVM TipoDespesa { get; set; }
        protected int CategoryID { get; set; }
        protected int TipoDespesaId { get; set; }

        private string ExpenseTypesApiEndpoint { get; set; } = string.Empty;
        private string LookupTablesApiEndpoint { get; set; } = string.Empty;

        private int _previousIndex;
        private bool _sortDirection;


        public frmExpenseTypes()
        {
            InitializeComponent();
            dgvTipoDespesas.AutoGenerateColumns = false;
            ExpenseTypesApiEndpoint = AccessSettingsService.TipoDespesaEndpoint;
            LookupTablesApiEndpoint = AccessSettingsService.LookupTablesEndpoint;

            FillCombo(cboCategories, "CategoriaDespesa");

            if (dgvTipoDespesas.RowCount > 0)
            {
                //dgvTipoDespesas.CurrentCell = dgvTipoDespesas.Rows[0].Cells[0];
                dgvTipoDespesas.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvTipoDespesas.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();

        }

        private IEnumerable<TipoDespesaVM> GetExpenseTypesVM()
        {
            string url = $"{ExpenseTypesApiEndpoint}/AllTipoDespesasVM";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var expenseTypes = response.Content.ReadAsAsync<IEnumerable<TipoDespesaVM>>().Result;
                    expenseTypes = expenseTypes.OrderBy(o => o.CategoriaDespesa);
                    if (expenseTypes != null)
                    {
                        return expenseTypes;
                    }
                }
                task.Wait();

                task.Dispose();

                return Enumerable.Empty<TipoDespesaVM>();
            }

        }

        private IEnumerable<TipoDespesaDto> GetExpenseTypes()
        {
            string url = $"{ExpenseTypesApiEndpoint}/AllTipoDespesas";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var expenseTypes = response.Content.ReadAsAsync<IEnumerable<TipoDespesaDto>>().Result;
                    if (expenseTypes != null)
                    {
                        return expenseTypes;
                    }
                }
                task.Wait();

                task.Dispose();

                return Enumerable.Empty<TipoDespesaDto>();
            }
        }

        private TipoDespesaDto GetExpenseType(int Id)
        {
            string url = $"{ExpenseTypesApiEndpoint}/TipoDespesaById/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var expenseType = response.Content.ReadAsAsync<TipoDespesaDto>().Result;
                    if (expenseType != null)
                    {
                        return expenseType;
                    }
                }
                task.Wait();

                task.Dispose();

                return new TipoDespesaDto();
            }
        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategories.SelectedItem == null)
            {
                return;
            }

            CategoryID = DataFormat.GetInteger(((DictionaryEntry)(cboCategories.SelectedItem)).Key);
        }

        private void FillCombo(ComboBox comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            string url = $"{LookupTablesApiEndpoint}/GetAllRecords/{dataTable}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<CategoriaDespesa>>(url);
                var response = task.Result;

                task.Wait();
                task.Dispose();

                if (response != null)
                {
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string url = ExpenseTypesApiEndpoint;

            var validationErrors = ValidateExpenseType();

            if (validationErrors.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var errorMsg in validationErrors)
                {
                    sb.AppendLine(errorMsg);
                }
                MessageBoxAdv.Show(sb.ToString(), "Erro na validação");
                return;
            }

            DialogResult dr = MessageBoxAdv.Show($"Confirma criação de registo?",
                    "Tipo de Despesa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            TipoDespesaDto expenseTypeDto = new()
            {
                Id = TipoDespesaId,
                Descricao = txtDescricao.Text,
                IdCategoriaDespesa = CategoryID
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, expenseTypeDto);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    TipoDespesaId = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets - Tipo de despesas");
                    SetToolbar(OpcoesRegisto.Inserir);
                    FillGrid();
                    ClearForm();

                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Criação de Tipo de despesa");
            }

        }

        private void FillGrid()
        {
            IEnumerable<TipoDespesaVM> expenseTypes;
            try
            {
                expenseTypes = GetExpenseTypesVM();

                if (expenseTypes != null)
                {
                    dgvTipoDespesas.DataSource = expenseTypes?.ToList();
                }
                else
                {
                    dgvTipoDespesas.DataSource = new List<TipoDespesaVM>();
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API tipo de despesas ({ex.Message})", "FillGrid");
            }
        }

        private List<string> ValidateExpenseType()
        {

            try
            {
                TipoDespesaDto expenseTypeToValidate = new()
                {
                    Descricao = txtDescricao.Text,
                    IdCategoriaDespesa = CategoryID
                };

                string url = $"{ExpenseTypesApiEndpoint}/ValidateExpenseType";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, expenseTypeToValidate);
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
                }
            }
            catch (Exception ex)
            {
                var ddd = ex.Message;
                throw;
            }
        }

        private void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var expenseTypeData = GetExpenseType(Id);
                if (expenseTypeData.Id > 0)
                {
                    txtId.Text = Id.ToString();
                    txtDescricao.Text = expenseTypeData.Descricao;
                    CategoryID = expenseTypeData.IdCategoriaDespesa;
                    cboCategories.SelectedIndex = expenseTypeData.IdCategoriaDespesa - 1;

                    SetToolbar(OpcoesRegisto.Gravar);
                    SelectRowInGrid();
                }
                else
                {
                    SetToolbar(OpcoesRegisto.Inserir);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Visualizar tipo de despesa");
            }
        }

        private void SelectRowInGrid()
        {
            dgvTipoDespesas.ClearSelection();
            dgvTipoDespesas.CurrentCell = null;

            dgvTipoDespesas.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == TipoDespesaId)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
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
        private void ClearForm()
        {
            TipoDespesaId = 0;
            CategoryID = -1;
            txtDescricao.Clear();
            txtDescricao.Focus();
            SetToolbar_Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var validationErrors = ValidateExpenseType();

            if (validationErrors.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var errorMsg in validationErrors)
                {
                    sb.AppendLine(errorMsg);
                }
                MessageBoxAdv.Show(sb.ToString(), "Erro na validação - Tipo de despesa");
                return;
            }

            DialogResult dr = MessageBoxAdv.Show($"Confirma atualização de registo?",
                "Tipo de Pespesa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (TipoDespesaId == 0)
                TipoDespesaId = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = dgvTipoDespesas.SelectedRows[0].Index;
            TipoDespesaDto expenseTypeToUpdate = new()
            {
                Id = TipoDespesaId,
                Descricao = txtDescricao.Text,
                IdCategoriaDespesa = CategoryID
            };

            string url = $"{ExpenseTypesApiEndpoint}/{TipoDespesaId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, expenseTypeToUpdate);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvTipoDespesas.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de tipo de despesa");
            }


        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar Tipo de despesa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeleteExpenseType();
        }



        private async Task DeleteExpenseType()
        {
            string url = $"{ExpenseTypesApiEndpoint}/{TipoDespesaId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        MessageBoxAdv.Show("Tipo de despesa em uso na tabela de Despesas... Verifique, p.f.", "Apagar tipo de despesa");
                        return;
                    }

                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        if (dgvTipoDespesas.RowCount > 0)
                        {
                            ShowRecord(1);
                        }
                        else
                        {
                            dgvTipoDespesas.DataSource = null;
                            ClearForm();
                        }
                    }

                    response.Dispose();
                    FillGrid();
                }

                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar tipo de despesa", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Apagar Tipo de despesa");
            }

        }

        private void dgvTipoDespesas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            TipoDespesaId = DataFormat.GetInteger(dgvTipoDespesas.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(TipoDespesaId);
        }

        private void dgvTipoDespesas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvTipoDespesas.DataSource = SortData(
                (List<TipoDespesaVM>)dgvTipoDespesas.DataSource, dgvTipoDespesas.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        public List<TipoDespesaVM> SortData(List<TipoDespesaVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
