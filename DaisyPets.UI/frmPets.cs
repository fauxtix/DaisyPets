using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Application.ViewModels.Pdfs;
using DaisyPets.UI.ApiServices;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    public partial class frmPets : MetroForm
    {

        private int IdEspecie = 0;
        private int IdRaca = 0;
        private int IdSituacao = 0;
        private int IdTemperamento = 0;
        private int IdTamanho = 0;
        private string IdGenero = "M";

        private int IdPet = 0;

        private bool nonNumberEntered = false;
        private string ApiBaseAddress { get; set; } = string.Empty;
        private string PetsApiEndpoint { get; set; } = string.Empty;
        private string MailMergeApiEndpoint { get; set; } = string.Empty;

        private int _previousIndex;
        private bool _sortDirection;

        private IEnumerable<PesoDto> Pesos = new List<PesoDto>();

        public frmPets()
        {
            InitializeComponent();

            ApiBaseAddress = AccessSettingsService.BaseAddressSetting(); // $"{ReadSetting("ApiBaseAddress")}";
            PetsApiEndpoint = AccessSettingsService.PetsEndpoint(); // $"{ApiBaseAddress}/Pets";
            MailMergeApiEndpoint = AccessSettingsService.MailMergeEndpoint(); // $"{ ApiBaseAddress}/Mailmerge";

            gdvDados.AutoGenerateColumns = false;
            Pesos = GetPesos();

            FillCombo(cboEspecies, "Especie");
            FillCombo(cboRaca, "Raca");
            FillCombo(cboSituacao, "Situacao");
            FillCombo(cboTemperamento, "Temperamento");
            FillCombo(cboTamanho, "Tamanho");

            //FillComboPesos();

            if (gdvDados.RowCount > 0)
            {
                gdvDados.CurrentCell = gdvDados.Rows[0].Cells[0];
                gdvDados.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(gdvDados.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var validationErrors = ValidateData();

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
                    "Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            var newPet = new PetDto()
            {
                Chip = txtChip.Text,
                Chipado = chkChipado.Checked ? 1 : 0,
                DataChip = dtpChip.Value.ToShortDateString(),
                NumeroChip = txtNumeroChip.Text,
                Cor = txtCor.Text,
                DoencaCronica = txtDoencaCronica.Text,
                Esterilizado = chkEsterilizado.Checked ? 1 : 0,
                Foto = txtFoto.Text,
                IdEspecie = IdEspecie,
                DataNascimento = dtpDataNascimento.Value.ToShortDateString(),
                IdPeso = (int)nudPeso.Value,
                IdRaca = IdRaca,
                IdSituacao = IdSituacao,
                IdTamanho = IdTamanho,
                IdTemperamento = IdTemperamento,
                Genero = IdGenero,
                Nome = txtNome.Text,
                Medicacao = txtMedicacao.Text,
                Observacoes = txtObservacoes.Text,
                Padrinho = chkPadrinho.Checked ? 1 : 0
            };

            string url = PetsApiEndpoint;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, newPet);
                    var response = task.Result;

                    var key = response.Content.ReadAsStringAsync().Result;
                    var definition = new { Id = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                    IdPet = Convert.ToInt32(obj?.Id);

                    task.Wait();
                    task.Dispose();

                    MessageBoxAdv.Show("Registo criado com sucesso", "Daisy Pets");
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

            var validationErrors = ValidateData();

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
                "Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            if (IdPet == 0)
                IdPet = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = gdvDados.SelectedRows[0].Index;
            var updatePet = new PetDto()
            {
                Id = IdPet,

                Nome = txtNome.Text,
                Medicacao = txtMedicacao.Text,
                Chip = txtChip.Text,
                Chipado = chkChipado.Checked ? 1 : 0,
                DataChip = dtpChip.Value.ToShortDateString(),
                NumeroChip = txtNumeroChip.Text,
                Cor = txtCor.Text,
                Foto = txtFoto.Text,
                DataNascimento = dtpDataNascimento.Value.ToShortDateString(),
                IdPeso = (int)nudPeso.Value,
                IdEspecie = cboEspecies.SelectedIndex + 1,
                IdRaca = cboRaca.SelectedIndex + 1,
                IdSituacao = cboSituacao.SelectedIndex + 1,
                IdTemperamento = cboTemperamento.SelectedIndex + 1,
                IdTamanho = cboTamanho.SelectedIndex + 1,
                Genero = IdGenero,
                Esterilizado = chkEsterilizado.Checked ? 1 : 0,
                Padrinho = chkPadrinho.Checked ? 1 : 0,

                DoencaCronica = txtDoencaCronica.Text,
                Observacoes = txtObservacoes.Text
            };

            string url = $"{PetsApiEndpoint}/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PutAsJsonAsync<PetDto>(url, updatePet);
                    var response = task.Result;

                    task.Wait();
                }

                FillGrid();
                SetToolbar(OpcoesRegisto.Gravar);


                gdvDados.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de Pet");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar Pet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeletePet();
        }

        private async Task DeletePet()
        {
            string url = $"{PetsApiEndpoint}/{IdPet}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo", "Pets");
                        return;
                    }

                    FillGrid();
                    if (gdvDados.RowCount > 0)
                    {
                        FillGrid();
                        gdvDados.Rows[0].Selected = true;
                        int petId = DataFormat.GetInteger(gdvDados.Rows[0].Cells["Id"].Value);
                        ShowRecord(petId);
                    }
                    else
                    {
                        gdvDados.DataSource = null;
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

        private void RefreshPets()
        {
            FillGrid();
        }


        private void FillGrid()
        {
            string url = $"{PetsApiEndpoint}/AllPetsVM";
            IEnumerable<PetVM> petsGridData;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        petsGridData = response.Content.ReadAsAsync<IEnumerable<PetVM>>().Result;
                        if (petsGridData != null)
                        {
                            gdvDados.DataSource = petsGridData?.ToList();
                        }
                        else
                        {
                            gdvDados.DataSource = new List<PetVM>();
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

        private PetVM GetPetVM(int Id)
        {
            PetVM pet;
            string url = $"{PetsApiEndpoint}/PetVMById/{Id}";
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

        private async Task FillGrid2()
        {
            IEnumerable<PetVM>? pets;
            string url = $"{PetsApiEndpoint}/AllPetsVM";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    pets = await httpClient.GetFromJsonAsync<IEnumerable<PetVM>>(url);
                    gdvDados.DataSource = pets ?? new List<PetVM>();
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
                var petData = await GetPet2(Id);
                if (petData != null)
                {
                    txtId.Text = Id.ToString();
                    txtNome.Text = petData.Nome;
                    txtMedicacao.Text = petData.Medicacao;
                    chkChipado.Checked = petData.Chipado == 1 ? true : false;
                    txtChip.Text = petData.Chip;
                    txtNumeroChip.Text = petData.NumeroChip;
                    dtpChip.Value = petData.Chipado == 1 ? DateTime.Parse(petData.DataChip) : DateTime.Now;
                    txtCor.Text = petData.Cor;
                    txtFoto.Text = petData.Foto;
                    dtpDataNascimento.Value = DateTime.Parse(petData.DataNascimento);
                    nudPeso.Value = petData.IdPeso;
                    txtIdade.Text = petData.DataNascimento;
                    txtNome.Text = petData.Nome;
                    txtDoencaCronica.Text = petData.DoencaCronica;
                    txtObservacoes.Text = petData.Observacoes;

                    txtNumeroChip.Visible = petData.Chipado == 1;
                    dtpChip.Visible = petData.Chipado == 1;

                    foreach (var item in Pesos)
                    {
                        if (petData.IdPeso >= item.De && petData.IdPeso <= item.A)
                        {
                            txtPorte.Text = item.Descricao;
                            break;
                        }
                    }

                    if (DataFormat.IsValidDate(petData.DataNascimento))
                    {
                        var dob = DateTime.Parse(petData.DataNascimento);
                        var now = DateTime.Now;

                        var months = GetMonthDifference(dob, now);
                        var size = petData.IdTamanho;

                        txtTipoCao.Text = GetPetType(size, months);
                    }
                    else
                    {
                        MessageBoxAdv.Show("Data de nascimento inválida. Verifique, p.f.", "Validação");
                    }


                    cboEspecies.SelectedIndex = petData.IdEspecie - 1;
                    cboRaca.SelectedIndex = petData.IdRaca - 1;
                    cboSituacao.SelectedIndex = petData.IdSituacao - 1;
                    cboTemperamento.SelectedIndex = petData.IdTemperamento - 1;
                    cboTamanho.SelectedIndex = petData.IdTamanho - 1;

                    cboGenero.SelectedItem = petData.Genero == "M" ? "Masculino" : "Feminino";

                    chkEsterilizado.Checked = petData.Esterilizado == 1 ? true : false;
                    chkPadrinho.Checked = petData.Padrinho == 1 ? true : false;

                    pbPhoto.ImageLocation = petData.Foto;

                    txtNome.Focus();

                    SetToolbar(OpcoesRegisto.Gravar);
                    //SelectRowInGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Visualizar registo");
            }
        }

        private PetDto GetPet(int Id)
        {
            try
            {
                string url = $"{PetsApiEndpoint}/{Id}";
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var pet = response.Content.ReadAsAsync<PetDto>().Result;
                        task.Wait();
                        task.Dispose();
                        return pet;
                    }
                    return new PetDto();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Pesquisar Pet");
                return new PetDto();
            }
        }

        private async Task<PetDto> GetPet2(int Id)
        {
            string url = $"{PetsApiEndpoint}/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var pet = await httpClient.GetFromJsonAsync<PetDto>(url);
                return pet ?? new PetDto();
            }
        }

        private IEnumerable<PesoDto> GetPesos()
        {
            string url = $"{PetsApiEndpoint}/Pesos";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var pesos = response.Content.ReadAsAsync<IEnumerable<PesoDto>>().Result;
                    if (pesos != null)
                    {
                        return pesos;
                    }
                }
                task.Wait();

                task.Dispose();

                return Enumerable.Empty<PesoDto>();
            }
        }

        private void gdvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdPet = DataFormat.GetInteger(gdvDados.Rows[e.RowIndex].Cells["Id"].Value);
            ShowRecord(IdPet);

        }

        private void FillCombo(ComboBox comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            var lookupEndpoint = AccessSettingsService.LookupTablesEndpoint();
            string url = $"{lookupEndpoint}/GetAllRecords/{dataTable}";
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

        private void ClearForm()
        {
            IdPet = 0;

            txtId.Clear();
            txtChip.Clear();
            chkChipado.Checked = false;
            txtCor.Clear();
            txtDoencaCronica.Clear();
            txtFoto.Clear();
            txtIdade.Clear();
            txtNome.Clear();
            txtMedicacao.Clear();
            txtObservacoes.Clear();
            cboEspecies.SelectedIndex = 0;
            cboRaca.SelectedIndex = -1;
            cboSituacao.SelectedIndex = -1;
            cboTemperamento.SelectedIndex = -1;
            cboTamanho.SelectedIndex = 0;
            cboGenero.SelectedItem = "M";
            chkEsterilizado.Checked = false;
            chkPadrinho.Checked = false;
            dtpDataNascimento.Value = DateTime.Now.AddDays(-1);
            dtpChip.Value = DateTime.Now.AddDays(-1);
            txtNumeroChip.Clear();
            nudPeso.Value = 1;
            pbPhoto.Image = Properties.Resources.NoImageAvailable;

            DisplayWeightDescription();
            DisplayAgeDescription();

            txtNome.Focus();

            SetToolbar_Clear();
        }

        private void cboEspecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEspecies.SelectedItem == null)
                return;

            IdEspecie = DataFormat.GetInteger(((DictionaryEntry)(cboEspecies.SelectedItem)).Key);

        }

        private void cboRaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRaca.SelectedItem == null)
                return;

            IdRaca = DataFormat.GetInteger(((DictionaryEntry)(cboRaca.SelectedItem)).Key);

        }

        private void cboSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSituacao.SelectedItem == null)
                return;

            IdSituacao = DataFormat.GetInteger(((DictionaryEntry)(cboSituacao.SelectedItem)).Key);

        }

        private void cboTemperamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTemperamento.SelectedItem == null)
                return;

            IdTemperamento = DataFormat.GetInteger(((DictionaryEntry)(cboTemperamento.SelectedItem)).Key);

        }

        private void cboTamanho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTamanho.SelectedItem == null)
                return;

            IdTamanho = DataFormat.GetInteger(((DictionaryEntry)(cboTamanho.SelectedItem)).Key);
        }


        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = @"C:\\Users\\User\\OneDrive\\Imagens\\AcordosSaude";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg,*.png)|*.JPG;*.JPEG;*.PNG";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        var photoPath = pbPhoto.ImageLocation = openFileDialog1.FileName;
                        txtFoto.Text = photoPath;
                    }
                }
                else
                {
                    MessageBoxAdv.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show(ex.Message);
            }
        }

        private void dtpDataNascimento_ValueChanged(object sender, EventArgs e)
        {
            DisplayAgeDescription();
        }

        private void DisplayAgeDescription()
        {
            string dogType = "";
            var dob = dtpDataNascimento.Value;
            var now = DateTime.Now;
            if (dob > now)
            {
                MessageBoxAdv.Show("Data inválida");
                return;
            }

            var months = GetMonthDifference(dob, now);
            int size = cboTamanho.SelectedItem is not null ? DataFormat.GetInteger(((DictionaryEntry)(cboTamanho.SelectedItem)).Key) : -1;
            if (size < 1)
            {
                //MessageBoxAdv.Show("Escolha tamanho, p.f.");
                return;
            }

            switch (size)
            {
                case 1: // pequeno
                    if (months > 0 && months <= 8)
                        dogType = "Cahorro";
                    else if (months > 9 && months <= 96)
                        dogType = "Adulto";
                    else
                        dogType = "Sénior";
                    break;
                case 2: // Médio
                    if (months > 0 && months <= 12)
                        dogType = "Cahorro";
                    else if (months > 12 && months <= 84)
                        dogType = "Adulto";
                    else
                        dogType = "Sénior";
                    break;
                case 3: // Grande
                    if (months > 0 && months <= 15)
                        dogType = "Cahorro";
                    else if (months > 15 && months <= 108)
                        dogType = "Adulto";
                    else
                        dogType = "Sénior";
                    break;
            }

            txtTipoCao.Text = dogType;

        }

        private string GetPetType(int size, int months)
        {
            string petType = "";
            switch (size)
            {
                case 1: // pequeno
                    if (months > 0 && months <= 8)
                        petType = "Cahorro";
                    else if (months > 9 && months <= 96)
                        petType = "Adulto";
                    else
                        petType = "Sénior";
                    break;
                case 2: // Médio
                    if (months > 0 && months <= 12)
                        petType = "Cahorro";
                    else if (months > 12 && months <= 84)
                        petType = "Adulto";
                    else
                        petType = "Sénior";
                    break;
                case 3: // Grande
                    if (months > 0 && months <= 15)
                        petType = "Cahorro";
                    else if (months > 15 && months <= 108)
                        petType = "Adulto";
                    else
                        petType = "Sénior";
                    break;
            }

            return petType;

        }
        private int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date > endDate.Date)
            {
                return 0;
            }

            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }


        private void nudPeso_ValueChanged(object sender, EventArgs e)
        {
            DisplayWeightDescription();
        }

        private void DisplayWeightDescription()
        {
            foreach (var item in Pesos)
            {
                if (nudPeso.Value >= item.De && nudPeso.Value <= item.A)
                {
                    txtPorte.Text = item.Descricao;
                    break;
                }
            }
        }

        private void txtPeso_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private List<string> ValidateData()
        {

            try
            {
                PetDto pet2Validate = new()
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

                string url = $"{PetsApiEndpoint}/ValidatePets";
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

        private void SetToolbar(OpcoesRegisto opt)
        {
            if (opt == OpcoesRegisto.Gravar)
            {
                btnUpdate.Enabled = true;
                btnClear.Enabled = true;
                btnDelete.Enabled = true;
                btnGeneratePdf.Enabled = true;

                btnAppts.Enabled = true;
                btnDewormers.Enabled = true;
                btnDocs.Enabled = true;
                btnDogFood.Enabled = true;
                btnVaccines.Enabled = true;


                btnInsert.Enabled = false;
            }
            else if (opt == OpcoesRegisto.Inserir)
            {
                btnClear.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnGeneratePdf.Enabled = false;

                btnAppts.Enabled = false;
                btnDewormers.Enabled = false;
                btnDocs.Enabled = false;
                btnDogFood.Enabled = false;
                btnVaccines.Enabled = false;


                btnInsert.Enabled = true;
            }
        }

        private void SetToolbar_Clear()
        {
            // mostra apenas opções para criar registo ou sair
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnGeneratePdf.Enabled = false;
            btnClear.Enabled = false;

            btnAppts.Enabled = false;
            btnDewormers.Enabled = false;
            btnDocs.Enabled = false;
            btnDogFood.Enabled = false;
            btnVaccines.Enabled = false;
        }

        private void SelectRowInGrid()
        {
            gdvDados.ClearSelection();
            gdvDados.CurrentCell = null;

            gdvDados.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdPet)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        private void chkChipado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkChipado_Click(object sender, EventArgs e)
        {
            txtNumeroChip.Visible = chkChipado.Checked;
            dtpChip.Visible = chkChipado.Checked;

            if (chkChipado.Checked == false)
            {
                txtNumeroChip.Clear();
            }
        }

        private async void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            var petVM = GetPetVM(IdPet);
            var Idade = petVM.DataNascimento;
            var dob = DateTime.Parse(petVM.DataNascimento);
            var now = DateTime.Now;
            string porte = "";
            var months = GetMonthDifference(dob, now);
            var years = months / 12;
            var meses = months % 12;
            var yearsAsString = years == 0 ? "" : years == 1 ? "Um ano" : $"{years} anos";
            var monthsAsString = meses == 0 ? "" : meses == 1 && years == 0 ? "Um mês" : meses == 1 ? " e um mês" : years > 0 ? $" e {meses} meses" : $"{meses} meses";
            var ageAsString = yearsAsString + monthsAsString;

            var descricaoDesparasitado = petVM.Genero == "M" ? "Desparasitado" : "Desparasitada";
            var descricaoEsterilizado = petVM.Genero == "M" ? "Esterilizado" : "Esterilizada";
            var descricaoGenero = petVM.Genero == "M" ? "O" : "A";

            string[] aCampos = new string[] {
                "DescricaoGenero", "Nome", "Raca", "Situacao", "Idade", "Porte", "Temperamento", "GeneroDesparasitado", "GeneroEsterilizado","DoencaCronica", "Foto"
            };


            var photo = petVM.Foto; //.Replace("\\", "/");

            var situacao = petVM.SituacaoAnimal;
            if (situacao.ToLower().Contains("fat"))
                situacao = "em família de adoção temporária";
            else if (situacao.ToLower().Contains("ado"))
            {
                if (petVM.Genero == "F")
                {
                    situacao = "adotada";
                }
            }
            else
            {
                situacao = char.ToLower(situacao[0]) + situacao.Substring(1);
            }


            string[] aDados = new string[]
            {
                descricaoGenero,
                petVM.Nome,
                petVM.RacaAnimal,
                situacao,
                ageAsString,
                petVM.TamanhoAnimal,
                petVM.TemperamentoAnimal,
                descricaoDesparasitado,
                descricaoEsterilizado,
                petVM.DoencaCronica,
                photo
            };


            var mergeModel = new MailMergeModel()
            {
                PetId = IdPet,
                DocumentHeader = "",
                MergeFields = aCampos,
                ValuesFields = aDados,
                WordDocument = "PetInfo.dotx",
                SaveFile = true,
                Referral = true
            };

            var pdfFile = await CreatePetPdf(mergeModel);
            pdfFile = pdfFile.Replace(".docx", ".pdf");
            if (!string.IsNullOrEmpty(pdfFile))
            {
                FormParameters.NomePdf = pdfFile;
                FormParameters.TituloPdf = "Pet Profile";
                frmPdfViewer frmPdf = new frmPdfViewer();
                frmPdf.ShowDialog();

            }
            else
            {
                MessageBoxAdv.Show("Operação terminou com ERRO, tente mais tarde, p.f.,", "Criar Pdf", MessageBoxButtons.OK);
            }

        }

        private async Task<string> CreatePetPdf(MailMergeModel model)
        {
            try
            {
                string url = $"{MailMergeApiEndpoint}/MailMergeDocument";
                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync(url, model);
                    var response = task.Result;

                    task.Wait();
                    task.Dispose();

                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = await response.Content.ReadAsStringAsync();
                        return resultString;
                    }

                    MessageBoxAdv.Show($"Erro na criação de Pdf ({response.ReasonPhrase})", "Daisy Pets");
                    return "";

                }

                //    var response = await _httpClient.PostAsJsonAsync<MailMergeModel>($"{_uri}/MailMergeDocument", model);
                //var resultString = await response.Content.ReadAsStringAsync();
                //return resultString;

            }
            catch
            {
                return "";
            }
        }

        private void gdvDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            gdvDados.DataSource = SortData(
                (List<PetVM>)gdvDados.DataSource, gdvDados.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;
        }

        public List<PetVM> SortData(List<PetVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

        private void gdvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var colIndex = e.ColumnIndex;
            var colName = senderGrid.Columns[colIndex].Name;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (e.RowIndex == -1)
                    return;

                IdPet = DataFormat.GetInteger(gdvDados.Rows[e.RowIndex].Cells["Id"].Value);
                if (colName.ToLower().Contains("appts"))
                {
                    frmPetVeterinaryAppointments fAppts = new frmPetVeterinaryAppointments(IdPet);
                    fAppts.ShowDialog();
                }
                else if (colName.ToLower().Contains("feed"))
                {
                    frmPetRacoes fAppts = new frmPetRacoes(IdPet);
                    fAppts.ShowDialog();
                }
                else if (colName.ToLower().Contains("wormer"))
                {
                    frmPetDewormers fDewormers = new frmPetDewormers(IdPet);
                    fDewormers.ShowDialog();
                }
                else
                {
                    frmPetVaccines fVaccines = new frmPetVaccines(IdPet);
                    fVaccines.ShowDialog();
                }
            }
        }

        private void cboGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGenero.SelectedItem == null)
                return;

            var genero = cboGenero.SelectedItem.ToString();
            IdGenero = genero!.Substring(0, 1);
        }

        private string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "";
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            frmUploadDocument frmUploadDocument = new frmUploadDocument(IdPet);
            DialogResult = frmUploadDocument.ShowDialog();

        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            frmUploadDocument frmUploadDocument = new frmUploadDocument(IdPet);
            DialogResult = frmUploadDocument.ShowDialog();
        }

        private void btnVaccines_Click(object sender, EventArgs e)
        {
            frmPetVaccines fVaccines = new frmPetVaccines(IdPet);
            fVaccines.ShowDialog();

        }

        private void btnAppts_Click(object sender, EventArgs e)
        {
            frmPetVeterinaryAppointments fAppts = new frmPetVeterinaryAppointments(IdPet);
            fAppts.ShowDialog();
        }

        private void btnDogFood_Click(object sender, EventArgs e)
        {
            frmPetRacoes fAppts = new frmPetRacoes(IdPet);
            fAppts.ShowDialog();
        }

        private void btnDewormers_Click(object sender, EventArgs e)
        {
            frmPetDewormers fDewormers = new frmPetDewormers(IdPet);
            fDewormers.ShowDialog();
        }
    }
}

