using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalityApp
{
    public partial class ReportIssueConfirmation : Form
    {
        private ReportIssue _reportIssueForm;
        public ReportIssueConfirmation(ReportIssue reportIssueForm)
        {
            InitializeComponent();
            _reportIssueForm = reportIssueForm;
        }

        private void ReportIssueConfirmation_Load(object sender, EventArgs e)
        {

        }

        // Prevents the form from being able to move anywhere
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                return;
            }

            base.WndProc(ref m);
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Close();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Close the ReportIssue form
            if (_reportIssueForm != null)
            {
                _reportIssueForm.Close();
            }

            // Open the MainMenu form
            MainMenu form = new MainMenu();
            form.Show();

            // Close the current confirmation form
            this.Close();
        }
    }
}
