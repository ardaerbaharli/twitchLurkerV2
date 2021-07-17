using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLurkerV2.Core;

namespace TwitchLurkerV2
{
    public partial class CredentialsInput : Form
    {
        public CredentialsInput()
        {
            InitializeComponent();
        }

        #region Move the window by form
        private bool mouseDown;
        private Point lastLocation;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        private void CredentialsInput_Load(object sender, EventArgs e)
        {
            //Configuration.SetPaths();
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.TabStop = false;

            // send data between forms
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            this.Hide();
            string username = txtUsername.Text;
            string lurkerToken = txtLurkerToken.Text;
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(lurkerToken))
            {
                MessageBox.Show(this, "Please fill the text boxes!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Lurker.credentialsInputExitStatus = true;               
                string content = $"username: {txtUsername.Text}" +
                                 $"\nlurkerToken: {txtLurkerToken.Text}";
                File.WriteAllText(Configuration.CredentialsPath, content);
                this.Close();
            }
        }

        private void LinkAuthToken_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitchtokengenerator.com");
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
