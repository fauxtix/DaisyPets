using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System.Collections;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Properties
{
    public partial class frmContacto : MetroForm
    {
        int iTipoContacto = 0;
        int IdContacto = 0;

        private int _previousIndex;
        private bool _sortDirection;

        public frmContacto()
        {

            InitializeComponent();
            gdvContacts.AutoGenerateColumns = false;
            FillCombo(cboTipoContacto, "TipoContacto");

            ClearForm();
            RefreshContacts();

            if (gdvContacts.RowCount > 0)
            {
                gdvContacts.Rows[0].Selected = true;
                var arg = new DataGridViewCellEventArgs(0, 0);
                gdvContacts_CellClick(gdvContacts, arg);
            }

        }

        private void frmContacto_Load(object sender, EventArgs e)
        {
        }

        private void ClearForm()
        {
            IdContacto = 0;

            txtID.Text = string.Empty;
            txtID.Clear();
            txtLocalidade.Clear();
            txtMorada.Clear();
            txtNome.Clear();
            txtEMail.Clear();
            txtContacto.Clear();
            txtNotas.Clear();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnInsert.Enabled = true;
            cboTipoContacto.SelectedIndex = 0;
            iTipoContacto = 0;

            SetToolbar_Clear();
        }

        private void FillGrid()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "https://localhost:7161/api/Contacts/AllContactsVM";

                var task = httpClient.GetAsync(url);
                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var contacts = response.Content.ReadAsAsync<IEnumerable<ContactoVM>>().Result;
                    if (contacts != null)
                    {
                        gdvContacts.DataSource = contacts?.ToList();
                    }
                    else
                    {
                        gdvContacts.DataSource = new List<ContactoVM>();
                    }
                }
                task.Wait();
                task.Dispose();
            }

        }

        private void FillCombo(ComboBoxAdv comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            string url = $"https://localhost:7161/api/LookupTables/GetAllRecords/{dataTable}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetFromJsonAsync<IEnumerable<TipoContacto>>(url);
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
            AddUpdate(OpcoesRegisto.Inserir);
            SetToolbar(OpcoesRegisto.Inserir);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AddUpdate(OpcoesRegisto.Gravar);
            SetToolbar(OpcoesRegisto.Gravar);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar Contacto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeleteContact();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void AddUpdate(OpcoesRegisto opcao)
        {
            string url = $"https://localhost:7161/api/Contacts";

            string sMsg1 = "";
            if (opcao == OpcoesRegisto.Inserir)
            {
                sMsg1 = "Criação";
            }
            else
            {
                sMsg1 = "Atualização";
            }

            var validationErrors = ValidationErrors();
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


            DialogResult dr = MessageBoxAdv.Show($"Confirma {sMsg1} de registo?",
                "Contactos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            ContactoVM contacto = new ContactoVM();

            if (opcao == OpcoesRegisto.Gravar)
            {
                contacto.Id = int.Parse(txtID.Text);
            }

            contacto.Nome = txtNome.Text;
            contacto.Morada = txtMorada.Text;
            contacto.Movel = txtContacto.Text;
            contacto.Localidade = txtLocalidade.Text;
            contacto.eMail = txtEMail.Text;
            contacto.Notas = txtNotas.Text;
            contacto.IdTipoContacto = iTipoContacto;

            try
            {
                if (opcao == OpcoesRegisto.Inserir)
                {
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            var task = httpClient.PostAsJsonAsync(url, contacto);
                            var response = task.Result;
                            var key = response.Content.ReadAsStringAsync().Result;
                            var definition = new { Id = 0 };
                            var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                            IdContacto = Convert.ToInt32(obj?.Id);
                            task.Wait();
                            task.Dispose();

                            MessageBoxAdv.Show("Registo criado com sucesso", "Contactos");
                            SetToolbar(OpcoesRegisto.Inserir);
                            FillGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        var exc = ex.Message;
                        MessageBoxAdv.Show($"Erro ao criar contacto ({exc})", "Contactos");
                    }
                }
                else // update
                {
                    if (IdContacto == 0)
                        IdContacto = DataFormat.GetInteger(txtID.Text);

                    url = $"https://localhost:7161/api/Contacts/{IdContacto}";


                    var keyId = int.Parse(txtID.Text);

                    var selectedRowIndex = gdvContacts.SelectedRows[0].Index;

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var task = httpClient.PutAsJsonAsync(url, contacto);
                        var response = task.Result;

                        task.Wait();
                        task.Dispose();
                    }

                    gdvContacts.Rows[selectedRowIndex].Selected = true;
                    MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
                }

                RefreshContacts();
                //                ClearForm();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        private void ShowRecord(int Id)
        {
            if (Id < 1)
                return;

            ContactoVM contacto = GetContactById(Id);

            txtID.Text = contacto.Id.ToString();
            txtNome.Text = contacto.Nome;
            txtMorada.Text = contacto.Morada;
            txtLocalidade.Text = contacto.Localidade;
            txtContacto.Text = contacto.Movel;
            txtEMail.Text = contacto.eMail;
            txtNotas.Text = contacto.Notas;
            txtIdContacto.Text = contacto.IdTipoContacto.ToString();
            cboTipoContacto.SelectedIndex = contacto.IdTipoContacto - 1;

            SetToolbar(OpcoesRegisto.Gravar);
            SelectRowInGrid();
        }


        private List<string> ValidationErrors()
        {

            try
            {
                ContactoVM contacto2Validate = new()
                {
                    Nome = txtNome.Text,
                    Morada = txtMorada.Text,
                    Localidade = txtLocalidade.Text,
                    Notas = txtNotas.Text,
                    Movel = txtContacto.Text,
                    eMail = txtEMail.Text,
                    IdTipoContacto = iTipoContacto = DataFormat.GetInteger(((DictionaryEntry)(cboTipoContacto.SelectedItem)).Key)
                };

                string url = $"https://localhost:7161/api/Contacts/ValidateContacts";
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync<ContactoVM>(url, contacto2Validate);
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


        private ContactoVM GetContactById(int Id)
        {
            string url = $"https://localhost:7161/api/Contacts/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var pet = response.Content.ReadAsAsync<ContactoVM>().Result;
                    if (pet != null)
                    {
                        return pet;
                    }
                }
                task.Wait();

                task.Dispose();

                return new ContactoVM();
            }

        }

        private IEnumerable<ContactoVM> GetContacts()
        {
            string url = $"https://localhost:7161/api/Contacts/AllContactsVM";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                if (response.IsSuccessStatusCode)
                {
                    var contacts = response.Content.ReadAsAsync<IEnumerable<ContactoVM>>().Result;
                    if (contacts != null)
                    {
                        return contacts;
                    }
                }
                task.Wait();

                task.Dispose();

                return Enumerable.Empty<ContactoVM>();
            }

        }

        private void cboTipoContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoContacto.SelectedItem == null)
            {
                return;
            }

            iTipoContacto = DataFormat.GetInteger(((DictionaryEntry)(cboTipoContacto.SelectedItem)).Key);
            txtIdContacto.Text = iTipoContacto.ToString();
        }

        private void gdvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            IdContacto = DataFormat.GetInteger(gdvContacts.Rows[e.RowIndex].Cells["Id"].Value);

            ShowRecord(IdContacto);
        }

        private void RefreshContacts()
        {
            FillGrid();
        }

        private async Task DeleteContact()
        {
            string url = $"https://localhost:7161/api/Contacts/{IdContacto}";
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
                    RefreshContacts();
                    MessageBoxAdv.Show("Registo apagado com sucesso");

                    if (gdvContacts.RowCount > 0)
                    {
                        IdContacto = DataFormat.GetInteger(gdvContacts.Rows[0].Cells["Id"].Value);

                        gdvContacts.Rows[0].Selected = true;
                        ShowRecord(IdContacto);
                    }
                    else
                    {
                        gdvContacts.DataSource = null;
                        ClearForm();
                    }
                }
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gdvContacts.Refresh();
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
            gdvContacts.ClearSelection();
            gdvContacts.CurrentCell = null;

            gdvContacts.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == IdContacto)
                .ToArray()[0]
                .Selected = true;
        }

        private void gdvContacts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            gdvContacts.DataSource = SortData(
                (List<ContactoVM>)gdvContacts.DataSource, gdvContacts.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;
        }

        private List<ContactoVM> SortData(List<ContactoVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

    }
}
