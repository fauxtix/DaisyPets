using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Pdfs;
using Newtonsoft.Json;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms;
using System.Net.Http.Json;
using System.Text;
using static DaisyPets.Core.Application.Enums.Common;
using DataFormat = DaisyPets.Core.Application.Formatting.DataFormat;

namespace DaisyPets.UI
{
    public partial class frmUploadDocument : MetroForm
    {
        private int _petId;
        private int DocumentId { get; set; }
        private string documentPath { get; set; }
        private bool fileUploaded { get; set; }

        private DateTime pdfCreationDate { get; set; }

        private int _previousIndex;
        private bool _sortDirection;

        public frmUploadDocument(int PetId = 0)
        {
            InitializeComponent();
            _petId = PetId;
            fileUploaded = false;
            dgvDocuments.AutoGenerateColumns = false;
            if (dgvDocuments.RowCount > 0)
            {
                //dgvDocuments.CurrentCell = dgvDocuments.Rows[0].Cells[0];
                dgvDocuments.Rows[0].Selected = true;
                int firstRowId = Convert.ToInt16(dgvDocuments.Rows[0].Cells[0].Value);
                ShowRecord(firstRowId);
            }

            ClearForm();
            FillGrid();
        }

        private void btnBrowseDocument_Click(object sender, EventArgs e)
        {
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html)|*.pdf; *.docx; *.xlsx; *.html";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = Path.GetDirectoryName(openFileDialog1.FileName);
                        lblPath.Text = path;
                        lblFile.Text = Path.GetFileName(openFileDialog1.FileName);
                        fileUploaded = true;
                        btnUploadDocument.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Selecione ficheiro, p.f..");
                    fileUploaded = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUploadDocument_Click(object sender, EventArgs e)
        {
            // insert endpoint
            string url = $"https://localhost:7161/api/document";

            string filename = Path.GetFileName(openFileDialog1.FileName);
            string fullFilePath = openFileDialog1.FileName;
            if (filename == null || !fileUploaded)
            {
                MessageBoxAdv.Show("Não escolheu documento...", "Daisy Pets documents");
            }
            else
            {
                var _petDocuments = GetGridData();
                if (_petDocuments.Any())
                {
                    // Verificar se documento já foi carregado...
                    var documentAlreadyUploaded = _petDocuments.Select(o => o.DocumentPath == fullFilePath).FirstOrDefault();
                    if (documentAlreadyUploaded)
                    {
                        MessageBoxAdv.Show($"Já carregou ficheiro {filename}. verifique, p.f. ...", "Daisy Pets - Documentos");
                        return;
                    }
                }

                var validationErrors = ValidateDocument();
                if (validationErrors.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var errorMsg in validationErrors)
                    {
                        sb.AppendLine(errorMsg);
                    }
                    MessageBoxAdv.Show(sb.ToString(), "Erro na validação");
                    btnUploadDocument.Enabled = false;
                    return;
                }


                DialogResult dr = MessageBoxAdv.Show($"Confirma criação do documento {filename}?",
                        "Documentos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                    return;

                var pdfFullPath = openFileDialog1.FileName;
                pdfCreationDate = GetPdfCreationDate(pdfFullPath);
                DocumentoDto documento = new DocumentoDto()
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    DocumentPath = pdfFullPath,
                    PetId = _petId,
                    CreatedOn = pdfCreationDate.ToShortDateString(),
                };

                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        var task = httpClient.PostAsJsonAsync(url, documento);
                        var response = task.Result;
                        var key = response.Content.ReadAsStringAsync().Result;
                        var definition = new { Id = 0 };
                        var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                        DocumentId = Convert.ToInt32(obj?.Id);
                        task.Wait();
                        task.Dispose();

                        MessageBoxAdv.Show("Documento criado com sucesso", "Documentos");
                        FillGrid();
                        ClearForm();
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxAdv.Show($"Erro ao criar registo ({ex.Message})", "Daisy Pets - Documentos");
                }

                ClearForm();
            }
        }

        private async void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var documentData = await GetDocument(Id);
                if (documentData.Id > 0)
                {
                    txtId.Text = documentData.Id.ToString();
                    txtTitle.Text = documentData.Title?.ToString();
                    txtDescription.Text = documentData?.Description?.ToString();
                    lblPath.Text = Path.GetDirectoryName(documentData.DocumentPath);
                    lblFile.Text = Path.GetFileName(documentData.DocumentPath);

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

        private void SelectRowInGrid()
        {
            dgvDocuments.ClearSelection();
            dgvDocuments.CurrentCell = null;

            dgvDocuments.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == DocumentId)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }


        private async Task<DocumentoDto> GetDocument(int Id)
        {
            string url = $"https://localhost:7161/api/document/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _document = await httpClient.GetFromJsonAsync<DocumentoDto>(url);
                return _document ?? new DocumentoDto();
            }
        }

        private void FillGrid()
        {
            IEnumerable<DocumentoVM>? documents = GetGridData();
            if (documents != null && documents.Any())
            {
                dgvDocuments.DataSource = documents?.ToList();
            }
            else
            {
                dgvDocuments.DataSource = documents?.ToList();
            }
        }

        private IEnumerable<DocumentoVM> GetGridData()
        {
            IEnumerable<DocumentoVM> documents;
            string url = $"https://localhost:7161/api/document/AllDocumentsVM/{_petId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        documents = response.Content.ReadAsAsync<IEnumerable<DocumentoVM>>().Result;
                        if (documents != null)
                        {
                            return documents;
                        }
                        else
                        {
                            return new List<DocumentoVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
                return new List<DocumentoVM>();
            }

            return new List<DocumentoVM>();
        }

        private void ClearForm()
        {
            DocumentId = 0;
            txtTitle.Clear();
            txtDescription.Clear();
            txtId.Clear();
            SetToolbar_Clear();
        }

        private void SetToolbar(OpcoesRegisto opt)
        {
            if (opt == OpcoesRegisto.Gravar)
            {
                btnUpdate.Enabled = true;
                btnClear.Enabled = true;
                btnDelete.Enabled = true;
                btnBrowseDocument.Visible = false;
                btnUploadDocument.Visible = false;

            }
            else if (opt == OpcoesRegisto.Inserir)
            {
                btnClear.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                btnBrowseDocument.Visible = true;
                btnUploadDocument.Visible = true;

            }
        }

        private void SetToolbar_Clear()
        {
            // mostra apenas opções para criar registo ou sair
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
            btnBrowseDocument.Visible = true;
            btnUploadDocument.Visible = true;

        }

        private List<string> ValidateDocument()
        {
            string url = $"https://localhost:7161/api/document/ValidateDocument";

            try
            {
                DocumentoDto documentToValidate = new()
                {
                    DocumentPath = openFileDialog1.FileName,
                    Title = txtTitle.Text,
                };

                using (HttpClient httpClient = new HttpClient())
                {

                    var task = httpClient.PostAsJsonAsync<DocumentoDto>(url, documentToValidate);
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



        private void dgvDocuments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvDocuments.DataSource = SortData(
                (List<DocumentoVM>)dgvDocuments.DataSource, dgvDocuments.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        public List<DocumentoVM> SortData(List<DocumentoVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

        private void dgvDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DocumentId = DataFormat.GetInteger(dgvDocuments.Rows[e.RowIndex].Cells["Id"].Value);
            documentPath = DataFormat.GetString(dgvDocuments.Rows[e.RowIndex].Cells["DocumentPath"].Value);
            ShowRecord(DocumentId);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var validationErrors = ValidateDocument();

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
                "Documentos - Pets", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;
            if (DocumentId == 0)
                DocumentId = DataFormat.GetInteger(txtId.Text);

            var selectedRowIndex = dgvDocuments.SelectedRows[0].Index;
            var updateAppt = new DocumentoDto()
            {
                Id = int.Parse(txtId.Text),
                PetId = _petId,
                DocumentPath = documentPath,
                Description = txtDescription.Text,
                Title = txtTitle.Text,
            };

            string url = $"https://localhost:7161/api/Document/{DocumentId}";
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


                dgvDocuments.Rows[selectedRowIndex].Selected = true;
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Atualização de documento");
            }
        }

        private async Task DeleteDocument()
        {
            string url = $"https://localhost:7161/api/Document/{DocumentId}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo", "Documentos");
                        return;
                    }
                    else
                    {
                        FillGrid();
                        if (dgvDocuments.RowCount > 0)
                        {
                            dgvDocuments.Rows[0].Selected = true;
                            var _documentId = DataFormat.GetInteger(dgvDocuments.Rows[0].Cells["Id"].Value);
                            ShowRecord(_documentId);
                        }
                        else
                        {
                            dgvDocuments.DataSource = null;
                            ClearForm();
                        }
                    }
                    response.Dispose();
                }

                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar documento", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Apagar documento");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var colIndex = e.ColumnIndex;
            var colName = senderGrid.Columns[colIndex].Name;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (e.RowIndex == -1)
                    return;

                DocumentId = DataFormat.GetInteger(dgvDocuments.Rows[e.RowIndex].Cells["Id"].Value);
                var documentName = dgvDocuments.Rows[e.RowIndex].Cells["DocumentPath"].Value.ToString();
                if (colName.ToLower().Contains("view"))
                {
                    FormParameters.NomePdf = documentName;
                    FormParameters.TituloPdf = "Documentos";
                    frmPdfViewer frmPdf = new frmPdfViewer();
                    frmPdf.ShowDialog();
                }
            }
        }

        private DateTime GetPdfCreationDate(string pdfFile)
        {
            DateTime creationDate;
            using (PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdfFile))
            {
                creationDate = loadedDocument.DocumentInformation.CreationDate;
            }
            return creationDate;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                "Apagar Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            await DeleteDocument();
        }
    }
}
