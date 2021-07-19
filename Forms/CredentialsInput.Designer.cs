
namespace TwitchLurkerV2
{
    partial class CredentialsInput
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtLurkerToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkAuthToken = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 549);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(163, 54);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(180, 33);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(252, 159);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(91, 42);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // txtLurkerToken
            // 
            this.txtLurkerToken.Location = new System.Drawing.Point(163, 106);
            this.txtLurkerToken.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtLurkerToken.Name = "txtLurkerToken";
            this.txtLurkerToken.Size = new System.Drawing.Size(180, 33);
            this.txtLurkerToken.TabIndex = 2;
            this.txtLurkerToken.TextChanged += new System.EventHandler(this.txtLurkerToken_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 109);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Auth token:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // linkAuthToken
            // 
            this.linkAuthToken.AutoSize = true;
            this.linkAuthToken.Location = new System.Drawing.Point(352, 109);
            this.linkAuthToken.Name = "linkAuthToken";
            this.linkAuthToken.Size = new System.Drawing.Size(18, 21);
            this.linkAuthToken.TabIndex = 4;
            this.linkAuthToken.TabStop = true;
            this.linkAuthToken.Text = "?";
            this.linkAuthToken.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkAuthToken_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Font = new System.Drawing.Font("Gadugi", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(337, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(33, 26);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click_1);
            // 
            // CredentialsInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(376, 230);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.linkAuthToken);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtLurkerToken);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Gadugi", 14.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(195)))), ((int)(((byte)(13)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "CredentialsInput";
            this.Text = "CredentialsInput";
            this.Load += new System.EventHandler(this.CredentialsInput_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtLurkerToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkAuthToken;
        private System.Windows.Forms.Button btnClose;
    }
}