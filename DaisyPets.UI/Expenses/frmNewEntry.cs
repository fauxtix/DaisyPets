using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Syncfusion.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DaisyPets.Core.Application.Enums.Common;

namespace DaisyPets.UI.Expenses
{
    public partial class frmNewEntry : MetroForm
    {
        private int expenseId = 0;
        public frmNewEntry(DespesaDto expense)
        {
            InitializeComponent();

            FillCombo(cboCategoriasDespesas, "CategoriaDespesa");
            FillCombo(cboTipoDespesa, "TipoDespesa");

            expenseId = expense.Id;
            if (expenseId == 0)
            {
                ClearForm();
            }
            else
            {
                ShowRecord(expenseId);
            }
        }


        private async Task<DespesaDto> GetExpense(int Id)
        {
            string url = $"https://localhost:7161/api/despesa/{Id}";
            using (HttpClient httpClient = new HttpClient())
            {
                var _expense = await httpClient.GetFromJsonAsync<DespesaDto>(url);
                return _expense ?? new DespesaDto();
            }
        }


        private async void ShowRecord(int Id)
        {

            if (Id < 1)
                return;

            try
            {
                var expenseData = await GetExpense(Id);
                if (expenseData.Id > 0)
                {
                    txtId.Text = Id.ToString();
                    dtpDataMovimento.Value = Convert.ToDateTime(expenseData.DataMovimento);
                    txtDescricao.Text = expenseData.Descricao;
                    if (expenseData.TipoMovimento.ToLower() == "s")
                    {
                        radioButtonExpenses.Checked = true;
                    }
                    else
                    {
                        radioButtonIncome.Checked = true;

                    }
                    txtValor.Text = expenseData.ValorPago.ToString();

                    SetToolbar(OpcoesRegisto.Gravar);
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

        private void buttonAddNewEntry_Click(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            txtDescricao.Clear();
            txtValor.Clear();
            cboCategoriasDespesas.SelectedIndex = -1;
            cboTipoDespesa.SelectedIndex = -1;
            dtpDataMovimento.Value = DateTime.Now;
        }

        private void FillCombo(ComboBox comboBox, string dataTable)
        {
            comboBox.Items.Clear();
            string url = $"https://localhost:7161/api/LookupTables/GetAllRecords/{dataTable}";
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

        private void SetToolbar(OpcoesRegisto opt)
        {
            if (opt == OpcoesRegisto.Gravar)
            {
                btnAddEdit.Text = "Gravar registo";
            }
            else if (opt == OpcoesRegisto.Inserir)
            {
                btnAddEdit.Text = "Criar registo";
            }
        }

    }
}
