using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace MunicipalityApp
{
    public partial class ReportIssue : MaterialForm
    {
        public ReportIssue()
        {
            InitializeComponent();
            //exits program when close icon on top right is selected
            this.FormClosing += new FormClosingEventHandler(Form_Closing);

            materialComboBox1.SelectedIndex = -1; // Ensure no default selection of a category

            //sets the material manager up to customize
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            //base colours
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber700, Primary.Amber900, Primary.Amber500, Accent.Amber400, TextShade.WHITE);

            //Setting up the richTextBox themeatically
            SetHintText();
            richTextBox1.Font = new Font("Roboto", 12);
        }

        MaterialSkinManager ThemeManager = MaterialSkinManager.Instance;

        private void ReportIssue_Load(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize;
        }

        //changes the theme of the app from light to dark when toggling the switch
        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch1.Checked) { 
                ThemeManager.Theme = MaterialSkinManager.Themes.LIGHT;

                //checks if the description is filled in or not during theme toggle
                if (richTextBox1.Text == "Description...")
                {
                    SetHintText(); // Restore the hint text if no user input
                }
                else
                {
                    richTextBox1.ForeColor = Color.Black;
                }
            }
            else { 
                ThemeManager.Theme = MaterialSkinManager.Themes.DARK;

                if (richTextBox1.Text == "Description...")
                {
                    SetHintText(); // Restore the hint text if no user input
                }
                else
                {
                    richTextBox1.ForeColor = Color.White;
                }
            }

            //Setting up the richTextBox themeatically

            richTextBox1.Font = new Font("Roboto", 12);
        }

        //returns the user to the main menu form
        private void materialButton1_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Hide();
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetHintText()
        {
            richTextBox1.Text = "Description..."; // Set the hint text
            richTextBox1.ForeColor = Color.DarkGray; // Set the hint text color
        }

        // Enter Event - Handle when the user clicks to start typing
        private void richTextBox1_Enter_1(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Description..." && richTextBox1.ForeColor == Color.DarkGray)
            {
                richTextBox1.Text = ""; // Clear the hint text

                if (materialSwitch1.Checked)
                {
                    richTextBox1.ForeColor = Color.Black;
                }
                else
                {
                    richTextBox1.ForeColor = Color.White;
                }
            }
        }

        // Leave Event - Handle when the user leaves the textbox
        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                SetHintText(); // Restore the hint text if no user input
            }
        }

        byte[] fileBytes = null;
        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Convert the selected file to byte array, no file type restrictions
                fileBytes = File.ReadAllBytes(filePath);

                // Update label to show the attachment was saved
                label1.Text = "✔️ Attachment saved";
                label1.ForeColor = Color.Green;
            }
            else if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                label1.Text = "";
            }
            else
            {
                label1.Text = "❌ Failed to Save Attachment";
                label1.ForeColor = Color.Red;
            }

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
            try
            {
                string location, category, description, imageBase64;

                // Check location
                if (string.IsNullOrWhiteSpace(materialTextBox1.Text))
                {
                    MessageBox.Show("There is no location for the issue.", "ALERT");
                    return;
                }
                else
                {
                    location = materialTextBox1.Text;
                }

                // Check category
                if (materialComboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("The category for the issue is not selected.", "ALERT");
                    return;
                }
                else
                {
                    category = materialComboBox1.SelectedItem?.ToString();
                }

                // Check description
                if (richTextBox1.Text.Equals("Description...") || string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    MessageBox.Show("There is no description of the issue being reported.", "ALERT");
                    return;
                }
                else
                {
                    description = richTextBox1.Text;
                }

                // Check image
                if (fileBytes == null)
                {
                    MessageBox.Show("No media attachment selected.", "ALERT");
                    return;
                }
                else
                {
                    // Convert byte[] to base64 string
                    imageBase64 = Convert.ToBase64String(fileBytes);
                }

                //save items into a list
                List<string> issueReportData = new List<string>
                {
                    location,
                    category,
                    description,
                    imageBase64
                };

                //show confirmation gamification screen
                ReportIssueConfirmation notifcation = new ReportIssueConfirmation(this);
                notifcation.ShowDialog();

                MainMenu form = new MainMenu();
                form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                //show failure screen
                ReportIssueFailure notifcation = new ReportIssueFailure(this);
                notifcation.ShowDialog();

                // Handles other possible exceptions
                MessageBox.Show($"The unexpected Error:\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        //ends the app when the close icon on the top right is selected
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();  // This will close the entire application
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Description..."; // Set the hint text
            richTextBox1.ForeColor = Color.DarkGray; // Set the hint text color
        }
    }
}
