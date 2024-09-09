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
    public partial class ReportIssueFailure : Form
    {
        private ReportIssue _reportIssueForm;
        public ReportIssueFailure(ReportIssue reportIssueForm)
        {
            InitializeComponent();
            _reportIssueForm = reportIssueForm;
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
