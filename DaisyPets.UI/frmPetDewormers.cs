using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Pdfs;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    public partial class frmPetDewormers : MetroForm
    {
        private int IdDesparasitante = 0;
        private int IdPet = 0;
        private string IdTipo = "I";
        private int _previousIndex;
        private bool _sortDirection;


        public frmPetDewormers(int petId = 0)
        {
            InitializeComponent();
            IdPet = petId;
            var petData = GetPetData(petId);
            txtPetName.Text = petData.Nome;

            dgvDewormers.AutoGenerateColumns = false;
            if (dgvDewormers.RowCount > 0)
            {
                dgvDewormers.CurrentCell = dgvDewormers.Rows[0].Cells[0];
                dgvDewormers.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvDewormers.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();
        }

        private void FillGrid()
        {
            IEnumerable<DesparasitanteVM> dewormers;

            string url = $"https://localhost:7161/api/desparasitante/PetDewormers/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dewormers = response.Content.ReadAsAsync<IEnumerable<DesparasitanteVM>>().Result;
                        if (dewormers != null)
                        {
                            dgvDewormers.DataSource = dewormers?.ToList();
                        }
                        else
                        {
                            dgvDewormers.DataSource = new List<DesparasitanteVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
            }

        }


        private async void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var dewormerData = await GetDewormer(Id);
                if (dewormerData.Id > 0)
                {
                    txtId.Text = Id.ToString();
                    cboTipo.SelectedItem = dewormerData.Tipo;
                    dtpAplicacao.Value = Convert.ToDateTime(dewormerData.DataAplicacao);
                    txtMarca.Text = dewormerData.Marca;
                    dtpProximaAplicacao.Value = Convert.ToDateTime(dewormerData.DataProximaAplicacao);

                    cboTipo.SelectedItem = dewormerData.Tipo == "I" ? "Interno" : "Externo";

                    SetToolbar(OpcoesRegisto.Gravar);
//                    SelectRowInGrid();
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

        private async Task<Core.Application.ViewModels.DesparasitanteDto> GetDewormer(int Id)
        {
            string url = $"https://localhost:7161/api/Desparasitante/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var dewormer = await httpClient.GetFromJsonAsync<Core.Application.ViewModels.DesparasitanteDto>(url);
                return dewormer ?? new Core.Application.ViewModels.DesparasitanteDto();
            }
        }


        private void SelectRowInGrid()
        {
            dgvDewormers.ClearSelection();
            dgvDewormers.CurrentCell = null;

            dgvDewormers.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdDesparasitante)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        private void ClearForm()
        {
            IdDesparasitante = 0;
            dtpAplicacao.Value = DateTime.Now.AddDays(-1);
            dtpProximaAplicacao.Value = DateTime.Now.AddDays(-1);
            txtMarca.Clear();
            cboTipo.SelectedItem = null;
            cboTipo.Focus();
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

        private void btnInsert_Click(object sender, EventArgs e)
        {

            string url = "https://localhost:7161/api/Desparasitante";

            var validationErrors = ValidateDewormer();

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
                    "Desparasitantes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            Core.Application.ViewModels.DesparasitanteDto newDewormer = new()
            {
                IdPet = IdPet,
                Tipo = IdTipo,
                DataAplicacao = dtpAplicacao.Value.ToShortDateString(),
                DataProximaAplicacao = dtpAplicacao.Value.AddMonths(3).ToShortDateString(),
                Marca = txtMarca.Text,
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newDewormer);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    IdDesparasitante = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets - Desparasitantes");
                    SetToolbar(OpcoesRegisto.Inserir);
                    FillGrid();
                    ClearForm();

                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Criação de Pet");
            }

        }

        private List<string> ValidateDewormer()
        {

            try
            {
                Core.Application.ViewModels.DesparasitanteDto dewormerToValidate = new()
                {
                    IdPet = IdPet,
                    Tipo = IdTipo,
                    DataAplicacao = dtpAplicacao.Value.ToShortDateString(),
                    DataProximaAplicacao = dtpProximaAplicacao.Value.ToShortDateString(),
                    Marca = txtMarca.Text,
                };

                string url = $"https://localhost:7161/api/Desparasitante/ValidateDesparasitantes";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, dewormerToValidate);
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
            var validationErrors = ValidateDewormer();

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
                "Desparasitantes - Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (IdDesparasitante == 0)
                IdDesparasitante = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = dgvDewormers.SelectedRows[0].Index;
            Core.Application.ViewModels.DesparasitanteDto dewormerDto = new()
            {
                Id = IdDesparasitante,
                IdPet = IdPet,
                Tipo = IdTipo,
                DataAplicacao = dtpAplicacao.Value.ToShortDateString(),
                DataProximaAplicacao = dtpProximaAplicacao.Value.ToShortDateString(),
                Marca = txtMarca.Text,
            };

            string url = $"https://localhost:7161/api/Desparasitante/{IdDesparasitante}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, dewormerDto);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvDewormers.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de desparasitante");
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
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
                return new PetVM();
            }
        }


        private void dgvDewormers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdDesparasitante = DataFormat.GetInteger(dgvDewormers.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(IdDesparasitante);
        }

        private void dgvDewormers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvDewormers.DataSource = SortData(
                (List<DesparasitanteVM>)dgvDewormers.DataSource, dgvDewormers.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        public List<DesparasitanteVM> SortData(List<DesparasitanteVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column).GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column).GetValue(_)).ToList();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeletePetDewormer();

        }

        private async Task DeletePetDewormer()
        {
            string url = $"https://localhost:7161/api/Desparasitante/{IdDesparasitante}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {

                        if (dgvDewormers.RowCount > 0)
                        {
                            ShowRecord(1);
                        }
                        else
                        {
                            dgvDewormers.DataSource = null;
                            ClearForm();
                        }

                    }

                    response.Dispose();
                    FillGrid();

                    if (dgvDewormers.RowCount > 0)
                    {
                        dgvDewormers.Rows[0].Selected = true;
                        int petId = DataFormat.GetInteger(dgvDewormers.Rows[0].Cells["PetId"].Value);
                        ShowRecord(petId);
                    }
                    else
                    {
                        dgvDewormers.DataSource = null;
                        ClearForm();
                    }
                }

                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar registo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Apagar Pet");
            }

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var file = GetDewordmers_InfoPdf();
            if (!string.IsNullOrEmpty(file))
            {
                FormParameters.NomePdf = file;
                FormParameters.TituloPdf = "Info Desparasitantes";
                frmPdfViewer frmPdf = new frmPdfViewer();
                frmPdf.ShowDialog();
            }
        }

        private string GetDewordmers_InfoPdf()
        {
            string url = $"https://localhost:7161/api/Desparasitante/Desparasitante_Info_Pdf";
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
                MessageBoxAdv.Show($"Erro no Api ({ex.Message})", "Desparasitantes");
                return "";
            }
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipo.SelectedItem == null)
                return;

            var _tipo = cboTipo.SelectedItem.ToString();
            IdTipo = _tipo!.Substring(0, 1);

        }

        private void dtpAplicacao_ValueChanged(object sender, EventArgs e)
        {
            var dateSelected = dtpAplicacao.Value;
            var isValidDate = DataFormat.IsValidDate(dateSelected);
            if (isValidDate)
            {
                if (dateSelected.Date > DateTime.Now.Date)
                {
                    MessageBoxAdv.Show("Data da aplicação inválida", "Desparasitantes");
                    return;
                }

                dtpProximaAplicacao.Value = dtpAplicacao.Value.AddMonths(3);
            }
        }
    }
}
