namespace AndroidWifi.Windows
{
    partial class MainForm
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
            this.SsidLabel = new System.Windows.Forms.Label();
            this.Ssid = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.GenerateCode = new System.Windows.Forms.Button();
            this.QrCodePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.QrCodePic)).BeginInit();
            this.SuspendLayout();
            // 
            // SsidLabel
            // 
            this.SsidLabel.AutoSize = true;
            this.SsidLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SsidLabel.Location = new System.Drawing.Point(12, 15);
            this.SsidLabel.Name = "SsidLabel";
            this.SsidLabel.Size = new System.Drawing.Size(99, 24);
            this.SsidLabel.TabIndex = 0;
            this.SsidLabel.Text = "WiFi SSID:";
            // 
            // Ssid
            // 
            this.Ssid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ssid.Location = new System.Drawing.Point(117, 12);
            this.Ssid.Name = "Ssid";
            this.Ssid.Size = new System.Drawing.Size(209, 29);
            this.Ssid.TabIndex = 1;
            this.Ssid.TextChanged += new System.EventHandler(this.Ssid_TextChanged);
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(117, 56);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(209, 29);
            this.Password.TabIndex = 3;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(14, 59);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(97, 24);
            this.PasswordLabel.TabIndex = 2;
            this.PasswordLabel.Text = "Password:";
            // 
            // GenerateCode
            // 
            this.GenerateCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateCode.Location = new System.Drawing.Point(18, 101);
            this.GenerateCode.Name = "GenerateCode";
            this.GenerateCode.Size = new System.Drawing.Size(308, 45);
            this.GenerateCode.TabIndex = 4;
            this.GenerateCode.Text = "Generate QR Code";
            this.GenerateCode.UseVisualStyleBackColor = true;
            this.GenerateCode.Click += new System.EventHandler(this.GenerateCode_Click);
            // 
            // QrCodePic
            // 
            this.QrCodePic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.QrCodePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QrCodePic.Location = new System.Drawing.Point(352, 12);
            this.QrCodePic.Name = "QrCodePic";
            this.QrCodePic.Size = new System.Drawing.Size(440, 440);
            this.QrCodePic.TabIndex = 5;
            this.QrCodePic.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 465);
            this.Controls.Add(this.QrCodePic);
            this.Controls.Add(this.GenerateCode);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Ssid);
            this.Controls.Add(this.SsidLabel);
            this.MinimumSize = new System.Drawing.Size(820, 504);
            this.Name = "MainForm";
            this.Text = "Wifi QR Code Generator";
            ((System.ComponentModel.ISupportInitialize)(this.QrCodePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SsidLabel;
        private System.Windows.Forms.TextBox Ssid;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button GenerateCode;
        private System.Windows.Forms.PictureBox QrCodePic;
    }
}

