using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Domain;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmNewEntry : MetroForm
    {
        private int expenseId = 0;
        private string tipoMovimento = string.Empty;
        private string IdTipoMovimento = "S";
        private int IdCategoriaDespesa = -1;
        private int IdTipoDespesa = -1;

        private IEnumerable<TipoDespesa>? ExpenseTypes { get; set; }

        public frmNewEntry(DespesaDto expense)
        {
            InitializeComponent();

            FillCombo(cboCategoriasDespesas, "CategoriaDespesa");
            FillCombo(cboTipoDespesa, "TipoDespesa");

            ExpenseTypes = GetTipoDespesas();

            expenseId = expense.Id;
            if (expenseId == 0)
            {
                ClearForm();
                btnAddEdit.Text = "Add";
            }
            else
            {
                btnAddEdit.Text = "Update";
                ShowRecord(expense);
            }
        }


        private async Task<DespesaDto> GetExpense(int Id)
        {
            string url = $"https://localhost:7161/api/despesa/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _expense = await httpClient.GetFromJsonAsync<DespesaDto>(url);
                return _expense ?? new DespesaDto();
            }
        }


        private void ShowRecord(DespesaDto expenseData)
        {
            try
            {
                if (expenseData.Id > 0)
                {
                    txtId.Text = expenseData.Id.ToString();
                    dtpDataMovimento.Value = Convert.ToDateTime(expenseData.DataMovimento);
                    txtDescricao.Text = expenseData.Descricao;

                    if (expenseData.TipoMovimento.ToLower() == "s")
                    {
                        radioButtonExpenses.Checked = true;
                    }
                    else
                    {
                        radioButtonIncome.Checked = true;

                    }
                    txtValor.Text = expenseData.ValorPago.ToString();
                    string categoryDescription = GetDescricaoCategoria(expenseData.IdCategoriaDespesa);
                    string expenseTypeDescription = GetDescricaoTipoDespesa(expenseData.IdTipoDespesa, expenseData.IdCategoriaDespesa);

                    var idxCategory = cboCategoriasDespesas.FindStringExact(categoryDescription);

                    // Ao mudar o index, segunda combo fica automaticamente filtrada...
                    cboCategoriasDespesas.SelectedIndex = idxCategory;

                    var idxExpenseType = cboTipoDespesa.FindStringExact(expenseTypeDescription);
                    cboTipoDespesa.SelectedIndex = idxExpenseType;

                    txtNotas.Text = expenseData.Notas;

                    SetToolbar(OpcoesRegisto.Gravar);
                }
                else
                {
                    SetToolbar(OpcoesRegisto.Inserir);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Visualizar registo");
            }
        }

        private void buttonAddNewEntry_Click(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            txtDescricao.Clear();
            txtValor.Clear();
            cboCategoriasDespesas.SelectedIndex = -1;
            cboTipoDespesa.SelectedIndex = -1;
            dtpDataMovimento.Value = DateTime.Now;
        }

        private IEnumerable<TipoDespesa>? GetTipoDespesas()
        {
            string url = $"https://localhost:7161/api/despesa/TipoDespesas";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<TipoDespesa>?>(url);
                var _expenses = task.Result;

                task.Wait();

                if (_expenses is null)
                {
                    MessageBoxAdv.Show("Erro ao carregar tipo de despesas", "Daist Pets - Despesas");
                }

                return _expenses;
            }

        }
        private void FillCombo(ComboBox comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            string url = $"https://localhost:7161/api/LookupTables/GetAllRecords/{dataTable}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<LookupTableVM>>(url);
                var response = task.Result;

                task.Wait();

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

        private void SetToolbar(OpcoesRegisto opt)
        {
            if (opt == OpcoesRegisto.Gravar)
            {
                btnAddEdit.Text = "Gravar";
            }
            else if (opt == OpcoesRegisto.Inserir)
            {
                btnAddEdit.Text = "Criar";
            }
        }

        private void btnAddEdit_Click(object sender, EventArgs e)
        {
            if (expenseId == 0)
            {
                // Criar
                InsertRecord();
            }
            else
            {
                // Gravar
                UpdateRecord();
            }
        }

        private void InsertRecord()
        {
            var validationErrors = ValidateExpense();

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
                    "Despesas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            var newExpense = GetFormData();

            string url = "https://localhost:7161/api/despesa";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newExpense);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    expenseId = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets - despesas");
                    DialogResult = DialogResult.OK;
                    Dispose();
                    Close();

                    //SetToolbar(OpcoesRegisto.Inserir);
                    //ClearForm();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Criação de Despesa");
                DialogResult = DialogResult.Cancel;
                Dispose();
                Close();
            }

        }

        private void UpdateRecord()
        {
            var validationErrors = ValidateExpense();

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

            DialogResult dr = MessageBoxAdv.Show($"Confirma atualização do registo?",
                    "Despesas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            var updatedExpense = GetFormData();

            if (expenseId == 0)
                expenseId = DataFormat.GetInteger(txtId.Text);

            string url = $"https://localhost:7161/api/Despesa/{expenseId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, updatedExpense);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();
                }

                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
                DialogResult = DialogResult.OK;
                Dispose();
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de despesa");
                DialogResult = DialogResult.Cancel;
                Dispose();
                Close();
            }
        }

        private void radioButtonExpenses_CheckedChanged(object sender, EventArgs e)
        {
            tipoMovimento = radioButtonExpenses.Checked ? "S" : "E";
        }

        private void radioButtonIncome_CheckedChanged(object sender, EventArgs e)
        {
            tipoMovimento = radioButtonExpenses.Checked ? "E" : "S";
        }

        private List<string> ValidateExpense()
        {
            try
            {
                DespesaDto expenseToValidate = GetFormData();
                string url = $"https://localhost:7161/api/despesa/ValidateExpense";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, expenseToValidate);
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
                var errMsg = ex.Message;
                return new List<string> { errMsg };
            }
        }

        private DespesaDto GetFormData()
        {
            var formData = new DespesaDto()
            {
                Id = expenseId,
                DataCriacao = DateTime.Now.Date.ToShortDateString(),
                DataMovimento = DateTime.Now.Date.ToShortDateString(),
                Descricao = txtDescricao.Text,
                IdCategoriaDespesa = cboCategoriasDespesas.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboCategoriasDespesas.SelectedItem)).Key) : -1,
                IdTipoDespesa = cboTipoDespesa.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboTipoDespesa.SelectedItem)).Key) : -1,
                Notas = txtNotas.Text,
                TipoMovimento = radioButtonExpenses.Checked ? "S" : "E",
                ValorPago = decimal.Parse(txtValor.Text)
            };

            return formData;
        }

        private void cboCategoriasDespesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoriasDespesas.SelectedItem == null)
                return;

            IdCategoriaDespesa = DataFormat.GetInteger(((DictionaryEntry)(cboCategoriasDespesas.SelectedItem)).Key);
            var tipoDespesas = ExpenseTypes?.Where(o => o.IdCategoriaDespesa == IdCategoriaDespesa).ToList();

            if (tipoDespesas is not null && tipoDespesas.Any())
            {
                cboTipoDespesa.Enabled = true;
                cboTipoDespesa.Items.Clear();
                foreach (var item in tipoDespesas)
                {
                    cboTipoDespesa.Items.Add(new DictionaryEntry(item.Id, item.Descricao));
                }

                cboTipoDespesa.ValueMember = "Key";
                cboTipoDespesa.DisplayMember = "Value";
                cboTipoDespesa.SelectedIndex = 0;
            }

        }

        private string GetDescricaoCategoria(int categoryId)
        {
            string url = $"https://localhost:7161/api/despesa/DescricaoCategoriaDespesa/{categoryId}";
            using (HttpClient httpClient = new HttpClient())
            {

                var task = httpClient.GetAsync(url);
                var response = task.Result;

                task.Wait();
                task.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var category = response.Content.ReadAsAsync<LookupTableVM>().Result;
                    return category.Descricao ?? "";
                }

                return "";
            }
        }

        private string GetDescricaoTipoDespesa(int expenseTypeId, int categoryId)
        {
            string url = $"https://localhost:7161/api/despesa/TipoDespesa_ByCategoriaDespesa/{categoryId}";
            using (HttpClient httpClient = new HttpClient())
            {

                var task = httpClient.GetAsync(url);
                var response = task.Result;

                task.Wait();
                task.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var expenseTypes = response.Content.ReadAsAsync<IEnumerable<TipoDespesa>>().Result;
                    if (expenseTypes.Any())
                    {
                        var tipoDespesa = expenseTypes.SingleOrDefault(o => o.Id == expenseTypeId);
                        return tipoDespesa.Descricao;
                    }
                    return "";
                }
            }

            return "";
        }


        private void cboTipoDespesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoDespesa.SelectedItem == null)
                return;

            IdTipoDespesa = DataFormat.GetInteger(((DictionaryEntry)(cboTipoDespesa.SelectedItem)).Key);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

    }
}
