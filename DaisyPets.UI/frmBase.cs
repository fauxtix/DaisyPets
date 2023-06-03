using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaisyPets.UI
{
    public partial class frmBase : MetroForm, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public enum DataStatus
        {
            InsertMode,
            NavigationMode,
            EditMode
        }

        public DataStatus sStatus;
        private int _CodGenerico;
        public int CodGenerico
        {

            get { return _CodGenerico; }
            set
            {
                if (_CodGenerico != value)
                {
                    {
                        SendPropertyChanging("CodGenerico");
                        _CodGenerico = value;
                        if (_CodGenerico > 0)
                        {
                            try
                            {
                                // TODO - modo fica sempre em InsertMode, sempre que atribuímos valor a 'CodGenerico'!
                                sStatus = DataStatus.InsertMode;
                                HabilitaDesabilitaControles(true);
                                //CarregaValores();
                                btnCancelar.Visible = true;
                                separadorCancelar.Visible = true;

                            }
                            catch (Exception ex)
                            {
                                //Logger.WriteLog(ex);
                                throw new Exception(ex.Message.ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                sStatus = DataStatus.InsertMode;
                                LimpaControles();
                                HabilitaDesabilitaControles(true);
                            }
                            catch (Exception ex)
                            {
                                //Logger.WriteLog(ex);
                                throw new Exception(ex.Message.ToString());
                            }
                        }

                        SendPropertyChanged("CodGenerico");

                    }
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        //private string sMensagemConfirmacaoExclusao = "";

        //private string sMensagemConfirmacaoRegistoCriadoSucesso = "";
        //private string sMensagemConfirmacaoRegistoAtualizadoSucesso = "";
        //private string sMensagemConfirmacaoRegistoAnuladoSucesso = "";

        //private string sMensagemConfirmacaoRegistoNaoCriado = "";
        //private string sMensagemConfirmacaoRegistoNaoExcluido = "";
        //private string sMensagemConfirmacaoRegistoNaoAtualizado = "";

        //private string sMensagemAvisoSistema = "";
        private readonly string sMensagemErroGeral = "";
        private readonly string sMensagemErroListagem = "";

        public frmBase()
        {
            InitializeComponent();
        }

        private void frmBase_Load(object sender, EventArgs e)
        {
            sStatus = DataStatus.NavigationMode;
            LimpaControles();
            HabilitaDesabilitaControles(false);
            BuildForm();
        }


        #region Métodos

        public virtual void CarregaValores()
        {

        }

        public virtual bool Excluir()
        {
            return false;
        }

        private void HabilitaDesabilitaControles(bool bValue)
        {
            try
            {
                //percorre os controles da tela e os habilita ou desabilita
                foreach (Control ctr in this.Controls)
                {
                    if (ctr is ToolStrip || ctr is DataGridView)
                        continue;
                    if (ctr.Name == "gpPubs")  // testar
                    {
                        ctr.Enabled = true;
                        continue;
                    }

                    if (ctr.Name.Contains("Media"))
                    {
                        if (sStatus == DataStatus.InsertMode)
                        {
                            ctr.Enabled = true;
                        }
                        else
                        {
                            ctr.Enabled = false;
                        }
                        continue;
                    }

                    //if (ctr.Name.ToLower().Equals("pbcover"))
                    //{
                    //    if (sStatus == StatusCadastro.scInserindo)
                    //    {
                    //        ctr.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        ctr.Enabled = false;
                    //    }
                    //    continue;
                    //}

                    if (ctr.Name.Contains("starRating"))
                    {
                        if (sStatus == DataStatus.NavigationMode)
                        {
                            ctr.Enabled = false;
                        }
                        else
                        {
                            ctr.Enabled = true;
                        }
                        continue;
                    }

                    if (ctr.Name.Contains("btnCheckIfExists"))
                    {
                        if (sStatus == DataStatus.NavigationMode)
                        {
                            ctr.Enabled = false;
                        }
                        else if (sStatus == DataStatus.InsertMode)
                        {
                            ctr.Enabled = true;
                        }
                        continue;
                    }

                    ctr.Enabled = bValue;
                }

                //habilitar os botões

                //Novo - será habilitado somente quando for navegação
                btnNovo.Enabled = (sStatus == DataStatus.NavigationMode);

                //Salvar
                btnSalvar.Enabled = (sStatus == DataStatus.EditMode ||
                                     sStatus == DataStatus.InsertMode);
                btnCancelar.Visible = btnSalvar.Enabled;
                separadorCancelar.Visible = btnSalvar.Enabled;

                //Excluir
                btnExcluir.Enabled = (sStatus == DataStatus.EditMode);

                //Localizar
                btnLocalizar.Enabled = (sStatus == DataStatus.NavigationMode ||
                                        sStatus == DataStatus.EditMode ||
                                        sStatus == DataStatus.InsertMode);

                //Fechar
                btnFechar.Enabled = true;
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.StackTrace);
            }
        }

        private void LimpaControles()
        {
            try
            {
                foreach (Control ctr in this.Controls)
                {
                    if (ctr is TextBox)
                    {
                        (ctr as TextBox).Text = "";
                        if (ctr.Name.ToLower() == "txtultlogin" || ctr.Name.ToLower() == "txtaltpassword")
                            (ctr as TextBox).ReadOnly = true;
                        else
                            (ctr as TextBox).ReadOnly = false;
                    }

                    if (ctr is Panel)
                    {
                        foreach (Control ctrl in ctr.Controls)
                        {
                            if (ctrl is TextBox)
                            {
                                (ctrl as TextBox).Text = "";
                            }

                            if (ctrl is Label)
                            {
                                if ((ctrl as Label).Name == "txtCodigo")
                                {
                                    (ctrl as Label).Text = "";
                                }
                            }
                        }
                    }

                    if (ctr is RichTextBox)
                    {
                        (ctr as RichTextBox).Text = "";
                    }


                    if (ctr is ComboBox)
                        (ctr as ComboBox).SelectedIndex = -1;

                    if (ctr is ListBox)
                        (ctr as ListBox).SelectedIndex = -1;

                    if (ctr is RadioButton)
                        (ctr as RadioButton).Checked = false;

                    if (ctr is PictureBox)
                        (ctr as PictureBox).Image = null;

                    if (ctr is CheckBox)
                    {
                        if (ctr.Name.ToLower() == "chkactive")
                            (ctr as CheckBox).Checked = true;
                        else
                            (ctr as CheckBox).Checked = false; ;
                    }

                    if (ctr is CheckedListBox)
                    {
                        foreach (ListControl item in (ctr as CheckedListBox).Items)
                            item.SelectedIndex = -1;
                    }

                    if (ctr is Label)
                    {
                        if ((ctr as Label).Name == "txtCodigo")
                        {
                            (ctr as Label).Text = "";
                        }
                    }
                    if (ctr is DateTimePicker)
                    {
                        (ctr as DateTimePicker).Value = DateTime.Now;
                    }
                }


            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        public virtual bool Localizar()
        {
            return false;
        }

        public virtual bool BuildForm()
        {
            return false;
        }

        public virtual bool Salvar()
        {
            return false;
        }

        public virtual bool Listar()
        {
            return false;
        }

        #endregion


        #region Botões

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxAdv.Show("Confirma operação?", "Apagar registo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (Excluir())
                    {
                        sStatus = DataStatus.NavigationMode;
                        //LimpaControles();
                        HabilitaDesabilitaControles(false);
                        //Logger.WriteTrace(this.Name, sMensagemConfirmacaoRegistoAnuladoSucesso);

//                        InformaUser("Apagar registo", "Registo apagado com sucesso", Notificacoes.TipoNotificacao.Mensagem);
                    }
                    else
                    {
                        //Logger.WriteTrace(this.Name, sMensagemConfirmacaoRegistoNaoExcluido);
//                        InformaUser("Apagar registo", "Registo não foi apagado", Notificacoes.TipoNotificacao.Alerta);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Localizar())
                {
                    sStatus = DataStatus.EditMode;
                    HabilitaDesabilitaControles(true);
                    CarregaValores();

                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            try
            {

                sStatus = DataStatus.InsertMode;
                LimpaControles();
                HabilitaDesabilitaControles(true);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            string sMsgTitulo = "Novo registo";
            string sMsgOperacao = "criado";
            try
            {
                string sMsg = sStatus == DataStatus.InsertMode ? "Confirma criação do registo?" :
                    "Confirma atualização do registo?";

                DialogResult dr = MessageBoxAdv.Show(sMsg, "Confirme operação, p.f.",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                {
                    return;
                }

                if (Salvar())
                {
                    if (sStatus == DataStatus.EditMode)
                    {
                        sMsgTitulo = "Atualizar registo";
                        sMsgOperacao = "atualizado";
                    }

                    sStatus = DataStatus.NavigationMode;
                    //LimpaControles();
                    HabilitaDesabilitaControles(false);

                    //Logger.WriteTrace(this.Name, sMensagemConfirmacaoRegistoAtualizadoSucesso);

                    //InformaUser($"{this.CaptionLabels[1].Text} - {sMsgTitulo}",
                    //    $"Registo {sMsgOperacao} com sucesso",
                    //    Notificacoes.TipoNotificacao.Gravacao);
                }
                else
                {
                    //Logger.WriteTrace(this.Name, sMensagemConfirmacaoRegistoNaoAtualizado);

                    //InformaUser("{this.CaptionLabels[1].Text}  - { sMsgTitulo}",
                    //    "Erro.\r\n\r\nVerifique log, p.f.", Notificacoes.TipoNotificacao.Alerta);
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new ApplicationException(ex.ToString());
            }
        }

        #endregion


        #region Eventos

        private void frmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        #endregion

        private void SendPropertyChanging(string property)
        {
            if (this.PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(property));
            }
        }
        private void SendPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
                btnLocalizar.Enabled = true;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
                sStatus = DataStatus.NavigationMode;
                LimpaControles();
                HabilitaDesabilitaControles(false);
                btnCancelar.Visible = false;
                separadorCancelar.Visible = false;
                btnLocalizar.Enabled = true;
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Listar())
                {
                    sStatus = DataStatus.NavigationMode;
                    LimpaControles();
                    HabilitaDesabilitaControles(false);
                }
                else
                {
                    //InformaUser(sMensagemErroListagem, sMensagemErroGeral, Notificacoes.TipoNotificacao.Alerta);

                    //if (Utilitarios.UsaNotifier())
                    //    MostraNotifier(this.Text, sMensagemErroGeral, Notificacoes.TipoNotificacao.Alerta);
                    //else
                    //    MessageBox.Show(sMensagemErroListagem, sMensagemErroGeral,
                    //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex);
                throw new Exception(ex.Message.ToString());
            }
        }


        private void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
