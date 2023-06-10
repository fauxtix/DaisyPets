using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Data;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    public partial class frmPetVeterinaryAppointments : MetroForm
    {
        private int IdAppt = 0;
        private int IdPet = 0;
        private int _previousIndex;
        private bool _sortDirection;

        public frmPetVeterinaryAppointments(int petId = 0)
        {
            IdPet = petId;
            InitializeComponent();

            var petData = GetPetData(petId);
            if (petData != null)
            {
                txtPetName.Text = petData.Nome;
            }

            dgvAppts.AutoGenerateColumns = false;
            if (dgvAppts.RowCount > 0)
            {
                dgvAppts.CurrentCell = dgvAppts.Rows[0].Cells[0];
                dgvAppts.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvAppts.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7161/api/Consulta";

            var validationErrors = ValidateAppointment();

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
                    "Consultas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            ConsultaVeterinarioDto newAppt = new()
            {
                IdPet = IdPet,
                DataConsulta = dtpApptDate.Value.ToShortDateString(),
                Diagnostico = txtDiagnostico.Text,
                Motivo = txtMotivo.Text,
                Tratamento = txtTratamento.Text
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newAppt);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    IdAppt = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets - Consultas");
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

        private List<string> ValidateAppointment()
        {
            try
            {
                ConsultaVeterinarioDto apptToValidate = new()
                {
                    IdPet = IdPet,
                    DataConsulta = dtpApptDate.Value.ToShortDateString(),
                    Diagnostico = txtDiagnostico.Text,
                    Motivo = txtMotivo.Text,
                    Tratamento = txtTratamento.Text
                };

                string url = $"https://localhost:7161/api/consulta/ValidateAppointment";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, apptToValidate);
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
            var validationErrors = ValidateAppointment();

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
                "Consultas - Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (IdAppt == 0)
                IdAppt = DataFormat.GetInteger(txtID.Text);

            var selectedRowIndex = dgvAppts.SelectedRows[0].Index;
            var updateAppt = new ConsultaVeterinarioDto()
            {
                Id = int.Parse(txtID.Text),
                IdPet = IdPet,
                DataConsulta = dtpApptDate.Value.ToShortDateString(),
                Diagnostico = txtDiagnostico.Text,
                Motivo = txtMotivo.Text,
                Tratamento = txtTratamento.Text
            };

            string url = $"https://localhost:7161/api/Consulta/{IdAppt}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, updateAppt);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvAppts.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de consulta");
            }

        }

        private void FillGrid()
        {
            IEnumerable<ConsultaVeterinarioVM> appts;

            string url = $"https://localhost:7161/api/Consulta/PetAppointments/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        appts = response.Content.ReadAsAsync<IEnumerable<ConsultaVeterinarioVM>>().Result;
                        if (appts != null)
                        {
                            dgvAppts.DataSource = appts?.ToList();
                        }
                        else
                        {
                            dgvAppts.DataSource = new List<ConsultaVeterinarioVM>();
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

        private void SelectRowInGrid()
        {
            dgvAppts.ClearSelection();
            dgvAppts.CurrentCell = null;

            dgvAppts.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdAppt)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }


        private void ClearForm()
        {
            IdAppt = 0;
            dtpApptDate.Value = DateTime.Now.AddDays(-1);
            txtMotivo.Clear();
            txtDiagnostico.Clear();
            txtTratamento.Clear();
            dtpApptDate.Focus();
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

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar registo de consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeletePetAppt();

        }

        private async Task DeletePetAppt()
        {
            string url = $"https://localhost:7161/api/Consulta//{IdAppt}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo", "Consultas no veterinário");
                        return;
                    }
                    else
                    {
                        FillGrid();
                        if (dgvAppts.RowCount > 0)
                        {
                            dgvAppts.Rows[0].Selected = true;
                            int petId = DataFormat.GetInteger(dgvAppts.Rows[0].Cells["IdPet"].Value);
                            ShowRecord(petId);
                        }
                        else
                        {
                            dgvAppts.DataSource = null;
                            ClearForm();
                        }
                    }
                    response.Dispose();
                }

                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar registo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Apagar Pet");
            }

        }

        private async void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var apptData = await GetAppt(Id);
                if (apptData.Id > 0)
                {
                    txtID.Text = apptData.Id.ToString();
                    txtDiagnostico.Text = apptData.Diagnostico.ToString();
                    txtMotivo.Text = apptData.Motivo.ToString();
                    txtTratamento.Text = apptData.Tratamento.ToString();
                    dtpApptDate.Value = Convert.ToDateTime(apptData.DataConsulta);

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
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Visualizar registo");
            }
        }

        private async Task<ConsultaVeterinarioDto> GetAppt(int Id)
        {
            string url = $"https://localhost:7161/api/Consulta/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var appt = await httpClient.GetFromJsonAsync<ConsultaVeterinarioDto>(url);
                return appt ?? new ConsultaVeterinarioDto();
            }
        }

        private void dgvAppts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvAppts.DataSource = SortData(
                (List<ConsultaVeterinarioVM>)dgvAppts.DataSource, dgvAppts.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }
        public List<ConsultaVeterinarioVM> SortData(List<ConsultaVeterinarioVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
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

        private void dgvAppts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdAppt = DataFormat.GetInteger(dgvAppts.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(IdAppt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}

