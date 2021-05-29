namespace TwitchLurkerV2
{
    partial class Lurker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lurker));
            this.updateOnlineChannelsTimer = new System.Windows.Forms.Timer(this.components);
            this.lblLurkCount = new System.Windows.Forms.Label();
            this.lblFollowedCount = new System.Windows.Forms.Label();
            this.lblConnected = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSubCount = new System.Windows.Forms.Label();
            this.lblMessageCount = new System.Windows.Forms.Label();
            this.uptime = new System.Windows.Forms.Timer(this.components);
            this.lblUptime = new System.Windows.Forms.Label();
            this.stayOnline = new System.Windows.Forms.Timer(this.components);
            this.checkMessages = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // updateOnlineChannelsTimer
            // 
            this.updateOnlineChannelsTimer.Enabled = true;
            this.updateOnlineChannelsTimer.Tick += new System.EventHandler(this.UpdateOnlineChannelsTimer_Tick);
            // 
            // lblLurkCount
            // 
            this.lblLurkCount.AutoSize = true;
            this.lblLurkCount.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLurkCount.Location = new System.Drawing.Point(12, 98);
            this.lblLurkCount.Name = "lblLurkCount";
            this.lblLurkCount.Size = new System.Drawing.Size(183, 21);
            this.lblLurkCount.TabIndex = 0;
            this.lblLurkCount.Text = "Lurking 0 channels.";
            // 
            // lblFollowedCount
            // 
            this.lblFollowedCount.AutoSize = true;
            this.lblFollowedCount.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFollowedCount.Location = new System.Drawing.Point(12, 40);
            this.lblFollowedCount.Name = "lblFollowedCount";
            this.lblFollowedCount.Size = new System.Drawing.Size(287, 21);
            this.lblFollowedCount.TabIndex = 2;
            this.lblFollowedCount.Text = "Currently following 0 channels.";
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnected.Location = new System.Drawing.Point(14, 196);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(95, 17);
            this.lblConnected.TabIndex = 4;
            this.lblConnected.Text = "Disconnected.";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(335, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(33, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblSubCount
            // 
            this.lblSubCount.AutoSize = true;
            this.lblSubCount.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubCount.Location = new System.Drawing.Point(12, 68);
            this.lblSubCount.Name = "lblSubCount";
            this.lblSubCount.Size = new System.Drawing.Size(322, 21);
            this.lblSubCount.TabIndex = 2;
            this.lblSubCount.Text = "Currently subscribed to 0 channels.";
            // 
            // lblMessageCount
            // 
            this.lblMessageCount.AutoSize = true;
            this.lblMessageCount.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageCount.Location = new System.Drawing.Point(12, 128);
            this.lblMessageCount.Name = "lblMessageCount";
            this.lblMessageCount.Size = new System.Drawing.Size(159, 21);
            this.lblMessageCount.TabIndex = 0;
            this.lblMessageCount.Text = "Sent 0 messages.";
            // 
            // uptime
            // 
            this.uptime.Interval = 1000;
            this.uptime.Tick += new System.EventHandler(this.Uptime_Tick);
            // 
            // lblUptime
            // 
            this.lblUptime.AutoSize = true;
            this.lblUptime.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUptime.Location = new System.Drawing.Point(12, 157);
            this.lblUptime.Name = "lblUptime";
            this.lblUptime.Size = new System.Drawing.Size(315, 21);
            this.lblUptime.TabIndex = 0;
            this.lblUptime.Text = "Uptime: 0 days 0 hours, 0 minutes.";
            // 
            // stayOnline
            // 
            this.stayOnline.Interval = 1000;
            this.stayOnline.Tick += new System.EventHandler(this.StayOnline_Tick);
            // 
            // checkMessages
            // 
            this.checkMessages.AutoSize = true;
            this.checkMessages.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkMessages.Checked = true;
            this.checkMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMessages.Location = new System.Drawing.Point(299, 196);
            this.checkMessages.Name = "checkMessages";
            this.checkMessages.Size = new System.Drawing.Size(69, 17);
            this.checkMessages.TabIndex = 6;
            this.checkMessages.Text = "Message";
            this.checkMessages.UseVisualStyleBackColor = true;
            this.checkMessages.CheckedChanged += new System.EventHandler(this.CheckMessages_CheckedChanged);
            // 
            // Lurker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(372, 225);
            this.ControlBox = false;
            this.Controls.Add(this.checkMessages);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.lblSubCount);
            this.Controls.Add(this.lblFollowedCount);
            this.Controls.Add(this.lblUptime);
            this.Controls.Add(this.lblMessageCount);
            this.Controls.Add(this.lblLurkCount);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(195)))), ((int)(((byte)(13)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Lurker";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Lurker_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateOnlineChannelsTimer;
        private System.Windows.Forms.Label lblLurkCount;
        private System.Windows.Forms.Label lblFollowedCount;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSubCount;
        private System.Windows.Forms.Label lblMessageCount;
        private System.Windows.Forms.Timer uptime;
        private System.Windows.Forms.Label lblUptime;
        private System.Windows.Forms.Timer stayOnline;
        private System.Windows.Forms.CheckBox checkMessages;
    }
}

