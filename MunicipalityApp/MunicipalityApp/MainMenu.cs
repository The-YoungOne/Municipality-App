using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace MunicipalityApp
{
    public partial class MainMenu : MaterialForm
    {
        public MainMenu()
        {
            InitializeComponent();

            //sets the material manager up to customize
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            //base colours
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber700, Primary.Amber900, Primary.Amber500, Accent.Amber400, TextShade.WHITE);
        }

        MaterialSkinManager ThemeManager = MaterialSkinManager.Instance;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //changes the theme of the app from light to dark when toggling the switch
        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if(materialSwitch1.Checked) { ThemeManager.Theme = MaterialSkinManager.Themes.LIGHT; }
            else { ThemeManager.Theme = MaterialSkinManager.Themes.DARK; }
        }

        //opens the form allowing users to report an issue
        private void materialButton1_Click(object sender, EventArgs e)
        {
            ReportIssue form = new ReportIssue();
            form.Show();
            this.Hide();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Info form = new Info();
            form.Show();
            this.Hide();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            ComingSoonForm notifcation = new ComingSoonForm();

            notifcation.ShowDialog();
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

        private void materialButton3_Click(object sender, EventArgs e)
        {
            ComingSoonForm notifcation = new ComingSoonForm();

            notifcation.ShowDialog();
        }
    }
}
