
using DaisyPets.Core.Application.ViewModels;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Collections;
using System.Text;

namespace DaisyPets.UI
{
    public partial class frmUploadDocument : MetroForm
    {
        private int _petId;
        private int DocumentId { get; set; }
        private bool fileUploaded {  get; set; }
        public frmUploadDocument(int PetId = 0)
        {
            InitializeComponent();
            _petId = PetId;
            fileUploaded = false;
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
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
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
            string url = $"https://localhost:7161/api/document";

            string filename = Path.GetFileName(openFileDialog1.FileName);
            if (filename == null || !fileUploaded)
            {
                MessageBoxAdv.Show("Please select a document.", "Daisy Pets documents");
            }
            else
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


                DialogResult dr = MessageBoxAdv.Show($"Confirma upload do documento {filename}?",
                        "Documentos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                    return;

                DocumentoDto documento = new DocumentoDto()
                {
                    CreatedOn = DateTime.Now.ToShortDateString(),
                    DocumentPath = openFileDialog1.FileName,
                    PetId = _petId,
                    Title = txtTitle.Text,
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
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxAdv.Show($"Erro ao criar registo ({ex.Message})", "Daisy Pets - Documentos");
                }
            }


        }

        private List<string> ValidateDocument()
        {
            string url = $"https://localhost:7161/api/document/ValidateDocument";

            try
            {
                DocumentoDto documentToValidate= new()
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

    }
}
