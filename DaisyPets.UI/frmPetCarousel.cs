using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.UI.ApiServices;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System.Collections;
using System.Net.Http.Json;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI
{
    public partial class frmPetCarousel : SfForm
    {
        private int PhotoId = 0;
        private int PetId = 0;
        private int _previousIndex;
        private bool _sortDirection;
        private CarouselImageCollection resCollection;
        private string PhotoGalleryApiEndpoint { get; set; } = string.Empty;
        private string PetsApiEndpoint { get; set; } = string.Empty;
        IEnumerable<GaleriaFotosVM> Photos { get; set; }


        public frmPetCarousel()
        {
            InitializeComponent();
            PhotoGalleryApiEndpoint = AccessSettingsService.PhotoGalleryEndpoint;
            PetsApiEndpoint = AccessSettingsService.PetsEndpoint;
            dgvGallery.AutoGenerateColumns = false;
            FillCombo();

            PetCarousel.ImageListCollection.Clear();
            FillGrid();
            ShowCarousel();
        }

        private async void FillCombo()
        {
            var pets = await GetPets();
            if (pets != null)
            {
                foreach (var entry in pets)
                {
                    cboPets.Items.Add(new DictionaryEntry(entry.Id, entry.Nome));
                }
                cboPets.ValueMember = "Key";
                cboPets.DisplayMember = "Value";
                cboPets.SelectedIndex = 0;
            }
        }
        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            AddUpdate(OpcoesRegisto.Inserir);
        }

        private void AddUpdate(OpcoesRegisto opcao)
        {
            string url = PhotoGalleryApiEndpoint;

            string sMsg1 = "";
            if (opcao == OpcoesRegisto.Inserir)
            {
                sMsg1 = "Criação";
            }
            else
            {
                sMsg1 = "Atualização";
            }

            var photoPath = txtFilePath.Text;
            var photoDate = dtpPhotoDate.Text;

            //var validationErrors = ValidationErrors();
            //if (validationErrors.Count() > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var errorMsg in validationErrors)
            //    {
            //        sb.AppendLine(errorMsg);
            //    }
            //    MessageBoxAdv.Show(sb.ToString(), "Erro na validação");
            //    return;
            //}

            DialogResult dr = MessageBoxAdv.Show($"Confirma {sMsg1} de registo?",
                "Galeria", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            GaleriaFotosDto photo = new();
            if (opcao == OpcoesRegisto.Gravar)
            {
                photo.Id = int.Parse(txtID.Text);
            }
            photo.Data = photoDate;
            photo.Imagem = photoPath;
            photo.IdPet = PetId;

            try
            {
                if (opcao == OpcoesRegisto.Inserir)
                {
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            var task = httpClient.PostAsJsonAsync(url, photo);
                            var response = task.Result;
                            var key = response.Content.ReadAsStringAsync().Result;

                            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                            {
                                MessageBoxAdv.Show(key, "Galeria de imagens - Validação");
                                return;
                            }

                            var definition = new { Id = 0 };
                            var obj = JsonConvert.DeserializeAnonymousType(key, definition);
                            PhotoId = Convert.ToInt32(obj?.Id);
                            task.Wait();
                            task.Dispose();

                            MessageBoxAdv.Show("Registo criado com sucesso", "Galeria");
                            FillGrid();
                            ShowCarousel();
                        }
                    }
                    catch (Exception ex)
                    {
                        var exc = ex.Message;
                        MessageBoxAdv.Show($"Erro ao criar foto ({exc})", "Galeria");
                    }
                }
                else // update
                {

                    url = $"{url}/{PhotoId}";


                    var keyId = int.Parse(txtID.Text);

                    var selectedRowIndex = dgvGallery.SelectedRows[0].Index;

                    using (HttpClient httpClient = new HttpClient())
                    {
                        var task = httpClient.PutAsJsonAsync(url, photo);
                        var response = task.Result;

                        task.Wait();
                        task.Dispose();
                    }

                    dgvGallery.Rows[selectedRowIndex].Selected = true;
                    MessageBoxAdv.Show("Operação terminada com sucesso,", "Atualização de dados", MessageBoxButtons.OK);
                }

                FillGrid();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }


        }
        private void FillGrid()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = $"{PhotoGalleryApiEndpoint}/AllPhotosVM";

                var task = httpClient.GetAsync(url);
                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    Photos = response.Content.ReadAsAsync<IEnumerable<GaleriaFotosVM>>().Result;
                    if (Photos != null)
                    {
                        dgvGallery.DataSource = Photos?.ToList();
                    }
                    else
                    {
                        dgvGallery.DataSource = new List<GaleriaFotosVM>();
                    }
                }
                task.Wait();
                task.Dispose();
            }
        }

        private void ShowCarousel()
        {
            if (Photos != null)
            {
                foreach (var photo in Photos)
                {
                    CarouselImage carouselImage = new CarouselImage();
                    carouselImage.ItemImage = Image.FromFile(photo.Imagem);
                    PetCarousel.ImageListCollection.Add(carouselImage);
                }
                PetCarousel.ImageSlides = true;
            }
        }

        private PetVM GetCarouselImages()
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

        private List<string> ValidationErrors()
        {
            try
            {
                GaleriaFotosDto photo2Validate = new()
                {
                    Data = dtpPhotoDate.Value.ToShortDateString(),
                    Imagem = txtFilePath.Text,
                    IdPet = DataFormat.GetInteger(((DictionaryEntry)(cboPets.SelectedItem)).Key)
                };

                string url = $"{PhotoGalleryApiEndpoint}/ValidatePhoto";
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, photo2Validate);
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

        private void cboPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPets.SelectedItem == null)
            {
                return;
            }

            PetId = DataFormat.GetInteger(((DictionaryEntry)(cboPets.SelectedItem)).Key);
            txtPetId.Text = PetId.ToString();
        }

        private async Task<IEnumerable<PetVM>> GetPets()
        {
            string url = $"{PetsApiEndpoint}/AllPetsVM";
            using (HttpClient httpClient = new HttpClient())
            {
                var pets = await httpClient.GetFromJsonAsync<IEnumerable<PetVM>>(url);
                return pets ?? Enumerable.Empty<PetVM>();
            }

        }

        private void btnBrowseDocument_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select photo to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = openFileDialog1.FileName;
                        txtFilePath.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Selecione ficheiro, p.f..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
