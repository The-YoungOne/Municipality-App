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
    public partial class Info : MaterialForm
    {
        public Info()
        {
            InitializeComponent();
            //exits program when close icon on top right is selected
            this.FormClosing += new FormClosingEventHandler(Form_Closing);

            //sets the material manager up to customize
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            //base colours
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber700, Primary.Amber900, Primary.Amber500, Accent.Amber400, TextShade.WHITE);
        }

        //ends the app when the close icon on the top right is selected
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();  // This will close the entire application
        }

        MaterialSkinManager ThemeManager = MaterialSkinManager.Instance;

        private void Info_Load(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize;
        }

        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch1.Checked) { ThemeManager.Theme = MaterialSkinManager.Themes.LIGHT; }
            else { ThemeManager.Theme = MaterialSkinManager.Themes.DARK; }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        // Prevents the form from being able to move anywhere
        //protected override void WndProc(ref Message m)
        //{
        //    const int WM_NCLBUTTONDOWN = 0xA1;
        //    const int HTCAPTION = 0x2;

        //    if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
        //    {
        //        return;
        //    }

        //    base.WndProc(ref m);
        //}
    }
}
