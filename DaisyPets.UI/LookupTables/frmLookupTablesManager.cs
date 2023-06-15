using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.UI.ApiServices;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Net.Http.Json;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.LookupTables
{
    public partial class frmLookupTablesManager : MetroForm
    {
        protected int TableId = 0;
        protected string _tableName = string.Empty;
        protected string _apiEndPoint = string.Empty;
        protected string tableToCheck = string.Empty;
        protected string tableDescription = string.Empty;
        protected string fieldToCheck = string.Empty;
        public frmLookupTablesManager(string tableName = "")
        {
            InitializeComponent();
            _tableName = tableName;
            dgvLookupTables.AutoGenerateColumns = false;

            switch (_tableName.ToLower())
            {
                case "categoriadespesa":
                    tableToCheck = "Despesa";
                    fieldToCheck = "IdCategoriaDespesa";
                    tableDescription = "Categoria de despesas";
                    break;
                case "raca":
                    tableToCheck = "pets";
                    fieldToCheck = "IdRaca";
                    tableDescription = "Raças";
                    break;
                case "situacao":
                    tableToCheck = "pets";
                    fieldToCheck = "IdSituacao";
                    tableDescription = "Situação";
                    break;
                case "temperamento":
                    tableToCheck = "pets";
                    fieldToCheck = "IdTemperamento";
                    tableDescription = "Temperamento";
                    break;
            }

            lblTableName.Text = tableDescription;

            _apiEndPoint = AccessSettingsService.LookupTablesEndpoint();

            if (dgvLookupTables.RowCount > 0)
            {
                dgvLookupTables.CurrentCell = dgvLookupTables.Rows[0].Cells[0];
                dgvLookupTables.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvLookupTables.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();

        }


        private async void ShowRecord(int Id)
        {
            if (Id < 1)
                return;

            var tableData = await GetData(Id);
            if (tableData is not null)
            {
                txtId.Text = tableData.Id.ToString();
                txtDescricao.Text = tableData?.Descricao;

                SetToolbar(OpcoesRegisto.Gravar);
                //SelectRowInGrid();
            }

        }
        private void FillGrid()
        {
            string url = $"{_apiEndPoint}/GetAllRecords/{_tableName}";
            IEnumerable<LookupTableVM> gridData;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        gridData = response.Content.ReadAsAsync<IEnumerable<LookupTableVM>>().Result;
                        if (gridData != null)
                        {
                            dgvLookupTables.DataSource = gridData?.ToList();
                        }
                        else
                        {
                            dgvLookupTables.DataSource = new List<LookupTableVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha - lookup tables manager");
            }
        }

        private async Task<LookupTableVM> GetData(int Id)
        {
            string url = $"{_apiEndPoint}/{Id}/{_tableName}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var output = await httpClient.GetFromJsonAsync<LookupTableVM>(url);
                    return output ?? new LookupTableVM();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro ao ler registo {Id} - ({ex.Message})", "Manutenção de tabelas");
                return new LookupTableVM();
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

        private void SelectRowInGrid()
        {
            dgvLookupTables.ClearSelection();
            dgvLookupTables.CurrentCell = null;

            dgvLookupTables.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == TableId)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        private void ClearForm()
        {
            TableId = 0;
            txtDescricao.Clear();
            txtId.Clear();

            txtDescricao.Focus();
            SetToolbar_Clear();
        }

        private void dgvLookupTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            TableId = DataFormat.GetInteger(dgvLookupTables.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(TableId);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string verifyEndpoint = $"{_apiEndPoint}/CheckRecordExist/{txtDescricao.Text}/{_tableName}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(verifyEndpoint);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var descriptionExists = response.Content.ReadAsAsync<bool>().Result;
                    if (descriptionExists)
                    {
                        MessageBoxAdv.Show($"Descrição {txtDescricao.Text} já existe", "Novo registo");
                        return;
                    }
                }
                task.Wait();
                task.Dispose();
            }

            DialogResult dr = MessageBoxAdv.Show($"Confirma criação de registo?",
                _tableName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            var newRecord = new LookupTableVM()
            {
                Descricao = txtDescricao.Text,
                Tabela = _tableName
            };

            string url = _apiEndPoint;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newRecord);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    TableId = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Manutenção de tabelas");
                    SetToolbar(OpcoesRegisto.Inserir);
                    FillGrid();
                    ClearForm();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Criação de registo");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string url = $"{_apiEndPoint}/{TableId}";
            DialogResult dr = MessageBoxAdv.Show($"Confirma atualização de registo?",
                _tableName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            if (TableId == 0)
                TableId = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = dgvLookupTables.SelectedRows[0].Index;
            var updateRecord = new LookupTableVM()
            {
                Id = TableId,
                Descricao = txtDescricao.Text,
                Tabela = _tableName
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync<LookupTableVM>(url, updateRecord);
                    var response = task.Result;

                    task.Wait();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvLookupTables.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de Pet");
            }

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsFkInUse())
            {
                MessageBoxAdv.Show($"A tabela '{tableToCheck}' tem registos que usam '{txtDescricao.Text}'", "Pedido cancelado");
                return;
            }

            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeleteRecord();

        }

        private async Task DeleteRecord()
        {
            // TODO - verificar se há registos usados noutras tabelas

            string url = $"{_apiEndPoint}/{TableId}/{_tableName}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo", $"{_tableName}");
                        return;
                    }

                    FillGrid();
                    if (dgvLookupTables.RowCount > 0)
                    {
                        ShowRecord(1);
                    }
                    else
                    {
                        dgvLookupTables.DataSource = null;
                        ClearForm();
                    }
                }

                FillGrid();
                dgvLookupTables.Rows[0].Selected = true;
                int petId = DataFormat.GetInteger(dgvLookupTables.Rows[0].Cells["Id"].Value);
                ShowRecord(petId);


                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar registo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Apagar registo");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private bool IsFkInUse()
        {
            string verifyEndpoint = $"{_apiEndPoint}/CheckFkInUse/{TableId}/{fieldToCheck}/{tableToCheck}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(verifyEndpoint);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var FKInuse = response.Content.ReadAsAsync<bool>().Result;
                    if (FKInuse)
                    {
                        return true;
                    }

                    task.Wait();
                    task.Dispose();

                    return false;
                }

                return true;
            }
        }
    }
}
