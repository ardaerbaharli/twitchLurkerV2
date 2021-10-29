using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TwitchLurkerV2.Core;
using TwitchLurkerV2.Core.Twitch;

namespace TwitchLurkerV2
{
    public partial class Lurker : Form
    {
        public Lurker()
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


        private DateTime startTime;
        private const int messageIntervalMinutes = 4;

        public static bool credentialsInputExitStatus = false;

        private void Lurker_Shown(object sender, EventArgs e)
        {
            this.Hide();
            Configuration.Configurate(this);

            // design
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TabStop = false;

            // Update online channels
            updateOnlineChannelsTimer.Interval = 1000 * 60 * messageIntervalMinutes;

            uptimeTimer.Interval = 1000 * 33;
            uptimeTimer.Start();
            startTime = DateTime.Now;

            // send data between forms
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region Timers
        private void StayOnline_Tick(object sender, EventArgs e)
        {
            if (Main.joinedChannels.Count > 0)
                foreach (var c in Main.joinedChannels.ToList())
                {
                    Main.client.JoinChannel(c);
                }
        }
        private async void UpdateOnlineChannelsTimer_Tick(object sender, EventArgs e)
        {
            await Main.instance.JoinChannels();
        }
        #endregion

        #region Form Events
        private void Uptime_Tick(object sender, EventArgs e)
        {
            TimeSpan uptime = DateTime.Now - startTime;
            lblUptime.Text = string.Format("Uptime: {0} days, {1} hours, {2} minutes.", uptime.Days, uptime.Hours, uptime.Minutes);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void CredentialsInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (credentialsInputExitStatus == true)
            {
                var c = Configuration.GetCredentials();
                Configuration.SetCredentials(c);
            }
        }
        private void CheckMessages_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.SetMessagingStatus(checkMessages.Checked);
        }
        private void checkTargeted_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.SetTargetedStatus(checkTargeted.Checked);
        }

        #endregion

        private void lblEmoteCount_Click(object sender, EventArgs e)
        {

        }
    }
}