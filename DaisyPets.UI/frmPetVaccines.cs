using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.DirectoryServices;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    public partial class frmPetVaccines : MetroForm
    {
        private int IdVacina = 0;
        private int IdPet = 0;
        private int _previousIndex;
        private bool _sortDirection;


        public frmPetVaccines(int petId = 4)
        {
            InitializeComponent();
            IdPet = petId;
            var petData = GetPetData(petId);
            txtPetName.Text = petData.Nome;

            dgvVacinas.AutoGenerateColumns = false;
            if (dgvVacinas.RowCount > 0)
            {
                dgvVacinas.CurrentCell = dgvVacinas.Rows[0].Cells[0];
                dgvVacinas.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvVacinas.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();
        }

        private void FillGrid()
        {
            IEnumerable<VacinaVM> vaccines;

            string url = $"https://localhost:7161/api/Vacinacao/PetVaccines/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        vaccines = response.Content.ReadAsAsync<IEnumerable<VacinaVM>>().Result;
                        if (vaccines != null)
                        {
                            dgvVacinas.DataSource = vaccines?.ToList();
                        }
                        else
                        {
                            dgvVacinas.DataSource = new List<VacinaVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
            }

        }


        private async void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var vaccineData = await GetVaccine(Id);
                if (vaccineData != null)
                {
                    txtId.Text = Id.ToString();
                    dtpToma.Value = Convert.ToDateTime(vaccineData.DataToma);
                    txtMarca.Text = vaccineData.Marca;
                    nupPrxToma.Value = vaccineData.ProximaTomaEmMeses;

                    SetToolbar(OpcoesRegisto.Gravar);
                    SelectRowInGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Visualizar registo");
            }
        }

        private async Task<VacinaDto> GetVaccine(int Id)
        {
            string url = $"https://localhost:7161/api/Vacinacao/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var vaccine = await httpClient.GetFromJsonAsync<VacinaDto>(url);
                return vaccine ?? new VacinaDto();
            }
        }


        private void SelectRowInGrid()
        {
            dgvVacinas.ClearSelection();
            dgvVacinas.CurrentCell = null;

            dgvVacinas.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdVacina)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        private void ClearForm()
        {
            IdVacina = 0;
            dtpToma.Value = DateTime.Now.AddDays(-1);
            txtMarca.Clear();
            nupPrxToma.Value = 1;
            dtpToma.Focus();
            SetToolbar_Clear();
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

        private async void btnInsert_Click(object sender, EventArgs e)
        {

            string url = "https://localhost:7161/api/Vacinacao";

            var validationErrors = ValidateVaccine();

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
                    "Vacinas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            VacinaDto newVaccine = new()
            {
                IdPet = IdPet,
                ProximaTomaEmMeses = (int)nupPrxToma.Value,
                DataToma = dtpToma.Value.ToShortDateString(),
                Marca = txtMarca.Text,
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newVaccine);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    IdVacina = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBox.Show("Registo criado com sucesso", "Daisy Pets - Vacinação");
                    SetToolbar(OpcoesRegisto.Inserir);
                    FillGrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Criação de Pet");
            }

        }

        private List<string> ValidateVaccine()
        {

            try
            {
                VacinaVM vaccineToValidate = new()
                {
                    IdPet = IdPet,
                    ProximaTomaEmMeses = (int)nupPrxToma.Value,
                    DataToma = dtpToma.Value.ToShortDateString(),
                    Marca = txtMarca.Text,
                };

                string url = $"https://localhost:7161/api/Vacinacao/ValidateVaccine";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, vaccineToValidate);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var validationErrors = ValidateVaccine();

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

            DialogResult dr = MessageBoxAdv.Show($"Confirma atualização de registo?",
                "Vacinação - Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (IdVacina == 0)
                IdVacina = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = dgvVacinas.SelectedRows[0].Index;
            var updateVaccine = new VacinaDto()
            {
                IdPet = IdPet,
                Id = IdVacina,
                DataToma = dtpToma.Value.ToShortDateString(),
                Marca = txtMarca.Text,
                ProximaTomaEmMeses = (int)nupPrxToma.Value
            };

            string url = $"https://localhost:7161/api/Vacinacao/{IdVacina}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, updateVaccine);
                    var response = task.Result;

                    task.Wait();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvVacinas.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Atualização de vacina");
            }

        }

        private async Task DeletePetVaccine()
        {
            string url = $"https://localhost:7161/api/Vacinacao/ {IdVacina}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = await httpClient.DeleteFromJsonAsync<VacinaDto>(url);
                    if (task is null)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo,", "Daisy Pets - Vacinas", MessageBoxButtons.OK);
                        return;

                    }

                    FillGrid();
                    if (dgvVacinas.RowCount > 0)
                    {
                        ShowRecord(1);
                    }
                    else
                    {
                        dgvVacinas.DataSource = null;
                        ClearForm();
                    }
                }

                FillGrid();
                dgvVacinas.Rows[0].Selected = true;
                int petId = DataFormat.GetInteger(dgvVacinas.Rows[0].Cells["Id"].Value);
                ShowRecord(petId);


                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar registo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Apagar Pet");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private PetVM GetPetData(int Id)
        {
            PetVM pet;
            string url = $"https://localhost:7161/api/Pets/PetVMById/{Id}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        pet = response.Content.ReadAsAsync<PetVM>().Result;
                        if (pet != null)
                        {
                            return pet;
                        }
                        else
                        {
                            return new PetVM();
                        }
                    }
                    task.Wait();
                    task.Dispose();

                    return new PetVM();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
                return new PetVM();
            }

        }

        private void dgvVacinas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdVacina = DataFormat.GetInteger(dgvVacinas.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(IdVacina);
        }

        private void dgvVacinas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvVacinas.DataSource = SortData(
                (List<PetVM>)dgvVacinas.DataSource, dgvVacinas.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        public List<PetVM> SortData(List<PetVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column).GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column).GetValue(_)).ToList();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar registo de vacinação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeletePetVaccine();

        }
    }
}
