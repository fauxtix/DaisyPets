using DaisyPets.Core.Application.Formatting;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms;
using System.Net.Http.Json;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.LookupTables
{
    public partial class frmTemperamento : frmBase
    {
        string url = "https://localhost:7161/api/LookupTables";

        public frmTemperamento()
        {
            InitializeComponent();
        }

        private void frmTemperamento_Load(object sender, EventArgs e)
        {
            CaptionLabels[1].Text = "Temperamentos";
            sStatus = DataStatus.EditMode;
            FillGrid();

            int iFirstId = GetFirstId("Temperamento");
            if (iFirstId > 0)
            {
                CodGenerico = iFirstId;
                CarregaValores();
            }
        }
        private void FillGrid()
        {

            var data = _TipoPropriedadeSvc.Query().ToList();

            dgTipoPropriedade.AutoGenerateColumns = false;
            dgTipoPropriedade.DataSource = DataService.ToDataTable(data);
            lblMessagem.Text = data.Count().ToString() + " registos";
        }

        private void DgTipoPropriedade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();

            if (e.RowIndex == -1)
                return;

            CodGenerico = DataFormat.GetInteger(dgTipoPropriedade.Rows[e.RowIndex].Cells["Id"].Value);
            sStatus = DataStatus.EditMode;
            CarregaValores();

            errorProvider1.Clear();
        }

        public override void CarregaValores()
        {
            if (CodGenerico > 0)
            {
                try
                {
                    TipoPropriedade data = _TipoPropriedadeSvc.Query_ById(CodGenerico);
                    this.txtCodigo.Text = Convert.ToString(data.Id);
                    this.txtDescricao.Text = data.Descricao;

                    SelectRowInGrid();

                    errorProvider1.Clear();
                    _dirtyTracker.MarkAsClean();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        private void SelectRowInGrid()
        {
            dgTipoPropriedade.ClearSelection();
            dgTipoPropriedade.CurrentCell = null;

            dgTipoPropriedade.Rows
                .OfType<DataGridViewRow>()
                .Where(x => (int)x.Cells["Id"].Value == CodGenerico)
                .ToArray<DataGridViewRow>()[0]
                .Selected = true;
        }

        //public override bool Localizar()
        //{
        //    try
        //    {
        //        bool bLocaliza = false;
        //        using (FrmPesquisaTipoPropriedade frmPesquisa = CompositionRoot.Resolve<FrmPesquisaTipoPropriedade>())
        //        {
        //            if (frmPesquisa.ShowDialog() == DialogResult.OK)
        //            {
        //                bLocaliza = (frmPesquisa.sCdCodigo != "");
        //                if (bLocaliza)
        //                    CodGenerico = int.Parse(frmPesquisa.sCdCodigo);
        //            }
        //        }

        //        return bLocaliza;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        public override bool Salvar()
        {

            string sCod;
            if (!string.IsNullOrEmpty(txtCodigo.Text))
                sCod = txtCodigo.Text;
            else
                sCod = "0";

            if (string.IsNullOrEmpty(txtDescricao.Text))
                errorProvider1.SetError(txtDescricao, "Campo requerido.");
            else
            {

                try
                {
                    if (sStatus == DataStatus.InsertMode)
                    {
                        LookupTableVM temperamento = new LookupTableVM
                        {
                            Descricao = txtDescricao.Text,
                            Tabela = "Temperamento"
                        };

                        using (HttpClient httpClient = new HttpClient())
                        {
                            var task = httpClient.PostAsJsonAsync(url, temperamento);
                            var response = task.Result;

                            var key = response.Content.ReadAsStringAsync().Result;
                            var definition = new { Id = 0 };
                            CodGenerico = JsonConvert.DeserializeObject<int>(key);

                            task.Wait();
                            task.Dispose();

                            MessageBox.Show("Registo criado com sucesso", "Daisy Pets");
                            FillGrid();
                        }
                    }
                    else if (sStatus == DataStatus.EditMode)
                    {
                        url += $"/{CodGenerico}";
                        LookupTableVM temperamento = new LookupTableVM
                        {
                            Id = CodGenerico,
                            Descricao = txtDescricao.Text,
                            Tabela = "Temperamento"
                        };

                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                var task = httpClient.PutAsJsonAsync(url, temperamento);
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
                            MessageBox.Show($"Erro no API {ex.Message}", "Atualização de Pet");
                        }
                    }

                    FillGrid();

                    CarregaValores();
                    m_DataUpdated = true;
                    return true;
                }
                catch (Exception exc)
                {

                    throw new ApplicationException(exc.Message);
                }
            }
            return false;
        }


        public override async bool Excluir()
        {

            DialogResult dr = MessageBoxAdv.Show($"Confirma operação?",
                    "Apagar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return false;

            await Delete()

            int Codigo = Convert.ToInt32(txtCodigo.Text);
            string sAlert = "Apagar registo";
            LookupTableVM table = new()
            {
                Id = DataFormat.GetInteger(txtCodigo.Text),
                Descricao = txtDescricao.Text,
                Tabela = "Temperamento"
            };
            try
            {
                TipoPropriedade tPropriedade = new TipoPropriedade { Id = Codigo };
                _TipoPropriedadeSvc.Delete(tPropriedade);
                m_DataUpdated = true;
                FillGrid();
                return true;
            }
            catch (Exception exc)
            {

                throw new ApplicationException(exc.Message);
            }
        }

        public override bool Listar()
        {
            try
            {
                //frmPreviewFormatosGenerico frm = new frmPreviewFormatosGenerico();
                //frm.ShowDialog();
                return true;
            }
            catch (Exception exc)
            {

                throw new ApplicationException(exc.Message);
            }
        }

        private void frmTemperamentos_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    if (_dirtyTracker.IsDirty)
                    {
                        if (MessageBoxAdv.Show("Dados foram alterados\r\n\r\nSai sem gravar?",
                            "Fechar formulário",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                        else
                            e.Cancel = false;
                    }
                    else
                    {
                        if (m_DataUpdated)
                            DialogResult = DialogResult.OK;
                        else
                            DialogResult = DialogResult.Cancel;

                    }
                    break;
            }
        }

        private int GetFirstId(string tableName)
        {
            url += $"GetFirstId/{{tableName}";
            using (HttpClient httpClient = new HttpClient())
            {
                var task = httpClient.GetAsync(url);
                var response = task.Result;
                task.Wait();

                task.Dispose();
                if (response.IsSuccessStatusCode)
                {
                    var firstId = response.Content.ReadAsAsync<int>().Result;
                    return firstId;
                }
                else return 0;
            }
        }

        private async Task Delete( )
        {
            string url = $"https://localhost:7161/api/Pets/{CodGenerico}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var task = await httpClient.DeleteFromJsonAsync<PetDto>(url);
                    if (task is null)
                    {
                        MessageBoxAdv.Show("Erro ao apagar registo,", "Daisy Pets", MessageBoxButtons.OK);
                        return;

                    }
                    FillGrid();
                }

                FillGrid();
                MessageBoxAdv.Show("Operação terminada com sucesso,", "Apagar registo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no API {ex.Message}", "Apagar Pet");
            }

        }

    }
}
