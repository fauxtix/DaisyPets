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
    public partial class frmPetRacoes : MetroForm
    {
        private int IdRacao = 0;
        private int IdPet = 0;
        private int _previousIndex;
        private bool _sortDirection;

        public frmPetRacoes(int petId = 0)
        {
            IdPet = petId;
            InitializeComponent();
            var petData = GetPetData(petId);
            if (petData != null)
            {
                txtPetName.Text = petData.Nome;
            }

            dgvRacoes.AutoGenerateColumns = false;
            if (dgvRacoes.RowCount > 0)
            {
                dgvRacoes.CurrentCell = dgvRacoes.Rows[0].Cells[0];
                dgvRacoes.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvRacoes.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            nupQtdDiaria.Value = 200;

            ClearForm();
            FillGrid();

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7161/api/racao";

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
                    "Rações", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            RacaoDto newRacao = new()
            {
                IdPet = IdPet,
                DataCompra = dtpBuyDate.Value.ToShortDateString(),
                Marca = txtMarca.Text,
                QuantidadeDiaria = (int)nupQtdDiaria.Value
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newRacao);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    IdRacao = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets - Rações");
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
                "Rações - Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (IdRacao == 0)
                IdRacao = DataFormat.GetInteger(txtID.Text);

            var selectedRowIndex = dgvRacoes.SelectedRows[0].Index;
            var updateRacao = new RacaoDto()
            {
                Id = int.Parse(txtID.Text),
                IdPet = IdPet,
                DataCompra = dtpBuyDate.Value.ToShortDateString(),
                Marca = txtMarca.Text,
                QuantidadeDiaria = (int)nupQtdDiaria.Value
            };

            string url = $"https://localhost:7161/api/racao/{IdRacao}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync(url, updateRacao);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                dgvRacoes.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de ração");
            }


        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                 "Apagar registo de ração", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeletePetFeed();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private List<string> ValidateAppointment()
        {
            try
            {
                RacaoDto racaoToValidate = new()
                {
                    IdPet = IdPet,
                    DataCompra = dtpBuyDate.Value.ToShortDateString(),
                    Marca = txtMarca.Text,
                    QuantidadeDiaria = (int)nupQtdDiaria.Value,
                };

                string url = $"https://localhost:7161/api/racao/ValidateRacao";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, racaoToValidate);
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

        private void FillGrid()
        {
            IEnumerable<RacaoVM> racoes;

            string url = $"https://localhost:7161/api/racao/PetFeeds/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        racoes = response.Content.ReadAsAsync<IEnumerable<RacaoVM>>().Result;
                        if (racoes != null)
                        {
                            dgvRacoes.DataSource = racoes?.ToList();
                        }
                        else
                        {
                            dgvRacoes.DataSource = new List<RacaoVM>();
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


        private async Task DeletePetFeed()
        {
            string url = $"https://localhost:7161/api/racao/ {IdRacao}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo", "Rações");
                        return;
                    }
                    else
                    {
                        FillGrid();
                        if (dgvRacoes.RowCount > 0)
                        {
                            dgvRacoes.Rows[0].Selected = true;
                            int petId = DataFormat.GetInteger(dgvRacoes.Rows[0].Cells["IdPet"].Value);
                            ShowRecord(petId);
                        }
                        else
                        {
                            dgvRacoes.DataSource = null;
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
                var racaoData = await GetRacao(Id);
                if (racaoData.Id > 0)
                {
                    txtID.Text = racaoData.Id.ToString();
                    txtMarca.Text = racaoData.Marca.ToString();
                    dtpBuyDate.Value = Convert.ToDateTime(racaoData.DataCompra);
                    nupQtdDiaria.Value = (int)racaoData.QuantidadeDiaria;

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


        private async Task<RacaoDto> GetRacao(int Id)
        {
            string url = $"https://localhost:7161/api/racao/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _racao = await httpClient.GetFromJsonAsync<RacaoDto>(url);
                return _racao ?? new RacaoDto();
            }
        }

        private void SelectRowInGrid()
        {
            dgvRacoes.ClearSelection();
            dgvRacoes.CurrentCell = null;

            dgvRacoes.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdRacao)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        private void ClearForm()
        {
            IdRacao = 0;
            dtpBuyDate.Value = DateTime.Now.AddDays(-1);
            txtMarca.Clear();
            nupQtdDiaria.Value = 200;
            dtpBuyDate.Focus();
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

        private void dgvRacoes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvRacoes.DataSource = SortData(
                (List<RacaoVM>)dgvRacoes.DataSource, dgvRacoes.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }
        public List<RacaoVM> SortData(List<RacaoVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

        private void dgvRacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdRacao = DataFormat.GetInteger(dgvRacoes.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(IdRacao);
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

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var file = GetPetFood_InfoPdf();
            if (!string.IsNullOrEmpty(file))
            {
                FormParameters.NomePdf = file;
                FormParameters.TituloPdf = "Info Rações";
                frmPdfViewer frmPdf = new frmPdfViewer();
                frmPdf.ShowDialog();
            }

        }

        private string GetPetFood_InfoPdf()
        {
            string url = $"https://localhost:7161/api/Racao/DogFood_Info_Pdf";
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
