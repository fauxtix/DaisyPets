using DaisyPets.Core.Application.ViewModels;
using DaisyPets.UI.ApiServices;
using Syncfusion.Windows.Forms;
using Syncfusion.WinForms.Controls;
using System.Configuration;
using System.Data;

namespace DaisyPets.UI.Stats
{
    public partial class frmPetStats : SfForm
    {
        IEnumerable<VacinaVM> Vaccines { get; set; }
        IEnumerable<DesparasitanteVM> Dewormers { get; set; }

        private string VaccinesApiEndpoint { get; set; } = string.Empty;
        private string DewormersApiEndpoint { get; set; } = string.Empty;

        private int daysToAlertVaccines = 0;
        private int daysToAlertDewormers = 0;

        private int _previousIndex;
        private bool _sortDirection;

        public frmPetStats()
        {
            InitializeComponent();
            daysToAlertVaccines = int.Parse(ConfigurationManager.AppSettings["DiasAvisoVacinas"]);
            daysToAlertDewormers = int.Parse(ConfigurationManager.AppSettings["DiasAvisoDesparasitantes"]);

            dgvVaccines.AutoGenerateColumns = false;
            dgvDewormers.AutoGenerateColumns = false;

            VaccinesApiEndpoint = AccessSettingsService.VacinacoesEndpoint;
            DewormersApiEndpoint = AccessSettingsService.DesparasitantesEndpoint;

            Vaccines = GetVaccines();
            Dewormers = GetDewormers();

            dgvVaccines.DataSource = Vaccines;
            dgvDewormers.DataSource = Dewormers;
        }

        private IEnumerable<VacinaVM> GetVaccines()
        {
            string url = $"{VaccinesApiEndpoint}/AllVacinasVM";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var output = response.Content.ReadAsAsync<IEnumerable<VacinaVM>>().Result;
                        if (output != null)
                        {
                            output = output.Where(o => o.DataProximaToma > DateTime.Now).OrderBy(c => c.DataProximaToma);
                            return output.ToList();
                        }
                        else
                        {
                            return Enumerable.Empty<VacinaVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();

                    return Enumerable.Empty<VacinaVM>();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
                return Enumerable.Empty<VacinaVM>();
            }

        }

        private void dgvVaccines_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DateTime result;

            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dgvVaccines.Rows[e.RowIndex];
                var prxToma = row.Cells[4].Value.ToString();
                if (DateTime.TryParse(prxToma, out result))
                {
                    TimeSpan ts = result - DateTime.Now;
                    var days = Convert.ToInt16(ts.TotalDays);
                    var colResult = row.Cells["Alerta"];
                    colResult.ReadOnly = true;
                    colResult.Value = days;
                    colResult.Style.BackColor = Color.Yellow;
                    colResult.Style.ForeColor = Color.Black;
                    colResult.ReadOnly = false;
                }
            }
        }

        private IEnumerable<DesparasitanteVM> GetDewormers()
        {
            string url = $"{DewormersApiEndpoint}/AllWormersVM";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var output = response.Content.ReadAsAsync<IEnumerable<DesparasitanteVM>>().Result;
                        if (output != null)
                        {
                            output = output.Where(o => DateTime.Parse(o.DataProximaAplicacao) > DateTime.Now);
                            return output.ToList();
                        }
                        else
                        {
                            return Enumerable.Empty<DesparasitanteVM>();
                        }
                    }
                    task.Wait();
                    task.Dispose();

                    return Enumerable.Empty<DesparasitanteVM>();
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show($"Erro no API {ex.Message}", "Preenchimento de grelha");
                return Enumerable.Empty<DesparasitanteVM>();
            }

        }


        private void dgvVaccines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvVaccines.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                dgvVaccines.UpdateCellValue(e.ColumnIndex, e.RowIndex);
                dgvVaccines.Rows[e.RowIndex].Cells[5].ReadOnly = true;
            }
        }

        private void dgvVaccines_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void dgvVaccines_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DateTime result;

            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dgvVaccines.Rows[e.RowIndex];
                var prxToma = row.Cells[4].Value.ToString();
                if (DateTime.TryParse(prxToma, out result))
                {
                    TimeSpan ts = result - DateTime.Now;
                    var days = Convert.ToInt16(ts.TotalDays);
                    var colResult = row.Cells["Alerta"];
                    colResult.ReadOnly = true;
                    colResult.Value = days;
                    colResult.Style.BackColor = Color.Yellow;
                    colResult.Style.ForeColor = Color.Black;
                    colResult.ReadOnly = false;
                }
            }
        }

        private void dgvVaccines_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var _days = Convert.ToInt32(dgvVaccines.Rows[e.RowIndex].Cells["DiasParaProximaToma"].Value);
            var orangeAlert = daysToAlertVaccines;
            var redAlert = daysToAlertVaccines / 2;

            if (_days <= redAlert)
            {
                dgvVaccines.Rows[e.RowIndex].Cells["DiasParaProximaToma"].Style.BackColor = Color.Red;
                dgvVaccines.Rows[e.RowIndex].Cells["DiasParaProximaToma"].Style.ForeColor = Color.White;
            }
            else if (_days > redAlert && _days <= orangeAlert)
            {
                dgvVaccines.Rows[e.RowIndex].Cells["DiasParaProximaToma"].Style.BackColor = Color.Orange;
                dgvVaccines.Rows[e.RowIndex].Cells["DiasParaProximaToma"].Style.ForeColor = Color.Black;
            }

        }

        public List<VacinaVM> SortVaccines(List<VacinaVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }
        public List<DesparasitanteVM> SortDewormers(List<DesparasitanteVM> list, string column, bool ascending)
        {
            return ascending ?
                list.OrderBy(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList() :
                list.OrderByDescending(_ => _.GetType().GetProperty(column)?.GetValue(_)).ToList();
        }

        private void dgvVaccines_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvVaccines.DataSource = SortVaccines(
                (List<VacinaVM>)dgvVaccines.DataSource, dgvVaccines.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        private void dgvDewormers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == _previousIndex)
                _sortDirection ^= true; // toggle direction

            dgvDewormers.DataSource = SortDewormers(
                (List<DesparasitanteVM>)dgvDewormers.DataSource, dgvDewormers.Columns[e.ColumnIndex].Name, _sortDirection);

            _previousIndex = e.ColumnIndex;

        }

        private void dgvDewormers_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            var _days = Convert.ToInt32(dgvDewormers.Rows[e.RowIndex].Cells["DiasParaProximaAplicacao"].Value);
            var orangeAlert = daysToAlertDewormers;
            var redAlert = daysToAlertDewormers / 2;
            if (_days <= redAlert)
            {
                dgvDewormers.Rows[e.RowIndex].Cells["DiasParaProximaAplicacao"].Style.BackColor = Color.Red;
                dgvDewormers.Rows[e.RowIndex].Cells["DiasParaProximaAplicacao"].Style.ForeColor = Color.White;
            }
            else if (_days > redAlert && _days <= orangeAlert)
            {
                dgvDewormers.Rows[e.RowIndex].Cells["DiasParaProximaAplicacao"].Style.BackColor = Color.Orange;
                dgvDewormers.Rows[e.RowIndex].Cells["DiasParaProximaAplicacao"].Style.ForeColor = Color.Black;
            }

        }
    }
}
