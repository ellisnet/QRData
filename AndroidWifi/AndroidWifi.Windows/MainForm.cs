using System;
using System.Drawing;
using System.Windows.Forms;
using AndroidWifi.Models;
using QRCoder;

namespace AndroidWifi.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CheckEnabled();
        }

        private void CheckEnabled()
        {
            GenerateCode.Enabled = (!String.IsNullOrWhiteSpace(Ssid.Text)) && (!String.IsNullOrWhiteSpace(Password.Text));
        }

        private void Ssid_TextChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void GenerateCode_Click(object sender, EventArgs e)
        {
            var credentials = new WifiCredentials {Ssid = Ssid.Text.Trim(), Pwd = Password.Text.Trim()};
            string base40 = credentials.ToBase40();

            using (var generator = new QRCodeGenerator())
            using (var codeData = generator.CreateQrCode(base40, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(codeData))
            {
                Bitmap codeImage = qrCode.GetGraphic(20);
                float resizeScale = Math.Min((float)(QrCodePic.Width - 2) / codeImage.Width, (float)(QrCodePic.Height - 2) / codeImage.Height);
                Bitmap resized = new Bitmap(QrCodePic.Width, QrCodePic.Height);
                var graphics = Graphics.FromImage(resized);
                graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, resized.Width, resized.Height));
                graphics.DrawImage(codeImage, ((int)resized.Width - ((int)(codeImage.Width * resizeScale))) / 2,
                    ((int)resized.Height - ((int)(codeImage.Height * resizeScale))) / 2, ((int)(codeImage.Width * resizeScale)), ((int)(codeImage.Height * resizeScale)));
                QrCodePic.Image = resized;
            }
        }
    }
}
