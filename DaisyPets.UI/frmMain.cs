using DaisyPets.UI.Properties;
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
    public partial class frmMain : MetroForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        frmPets fPets;
        private void btnPets_Click(object sender, EventArgs e)
        {
            if (fPets == null)
            {
                fPets = new frmPets();
                fPets.MdiParent = this;
                fPets.FormClosed += Pets_FormClosed;
                fPets.Show();
            }
            else
            { fPets.Activate(); }
        }


        private void Pets_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPets.Dispose();
        }

        frmContacto fContactos;

        private void btnContactos_Click(object sender, EventArgs e)
        {
            if (fContactos == null)
            {
                fContactos = new frmContacto();
                fContactos.MdiParent = this;
                fContactos.FormClosed += Contacts_FormClosed;
                fContactos.Show();
            }
            else
            { fPets.Activate(); }
        }

        private void Contacts_FormClosed(object sender, FormClosedEventArgs e)
        {
            fContactos.Dispose();
        }

        frmPdfViewer fPdf;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (fPdf == null)
            {
                fPdf = new frmPdfViewer();
                fPdf.MdiParent = this;
                fPdf.FormClosed += CreatePdfs_FormClosed;
                fPdf.Show();
            }
            else
            { fPdf.Activate(); }
        }

        private void CreatePdfs_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPdf.Dispose();
        }
    }
}

