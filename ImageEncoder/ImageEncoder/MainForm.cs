using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;




namespace ImageEncoder
{
    public partial class MainForm : Form
    {
        private const int CharWidth = 7;
        private const int CharHeight = 9;
        private const int MaxCols = 4;
        private const int MaxChars = 12;
        private const int OutputSize = 32;
        private const string LetterFolder = "Letters";

        private string _currnetUser = "";
        private string _sourceImageName, _embeddedImageName;
        private string _embeddedImageLocation, _decodedImageLocation;
        private Bitmap _secretImage, _sourceImage, _embeddedImage, _decodeImage;
        private Watermark _watermark;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "Image Encoder";
            this.usernameTextBox.Text = $"";
            this.systemLabel.Text = $"";
            this.secretPictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 可選：自動縮放
            this.sourcePictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 可選：自動縮放
            this.embededPictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 可選：自動縮放
            this.decodePictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 可選：自動縮放

            _currnetUser = TrimUsername(CallPowershellProcess(@"(Get-WMIObject -ClassName Win32_ComputerSystem | Select-Object -ExpandProperty Username) -split '\\' | Select-Object -Last 1").ToUpper());
            _secretImage = GenerateSecretImage(_currnetUser);
            _sourceImage = GenerateSourceImage();
            //_embeddedImage = EmbedSourceImage();

            ApplyForm();
        }
        public MainForm(string arg)
        {
            InitializeComponent();
             
            _embeddedImageLocation = AppDomain.CurrentDomain.BaseDirectory + "EmbeddedWatermark.png";
            _decodedImageLocation = AppDomain.CurrentDomain.BaseDirectory + "DecodedWatermark.png";
            _currnetUser = TrimUsername(CallPowershellProcess(@"(Get-WMIObject -ClassName Win32_ComputerSystem | Select-Object -ExpandProperty Username) -split '\\' | Select-Object -Last 1").ToUpper());
            _secretImage = GenerateSecretImage(_currnetUser);
            _sourceImage = GenerateSourceImage();

            if (arg.ToLower().Equals("encode"))
            {
                _embeddedImage = EmbedSourceImage();
                _embeddedImage.Save(_embeddedImageLocation, ImageFormat.Png);
            }

            if (arg.ToLower().Equals("decode"))
            {
                var fileBytes = File.ReadAllBytes(_embeddedImageLocation);
                using (var ms = new MemoryStream(fileBytes))
                {
                    using (var tempImg = Image.FromStream(ms))
                    {
                        _embeddedImage = new Bitmap(tempImg);  // 建立複本，不依賴 stream
                    }
                }
                _decodeImage = DecodeSourceImage();
                _decodeImage.Save(_decodedImageLocation, ImageFormat.Png);
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            string input = this.usernameTextBox.Text;
            _secretImage = GenerateSecretImage(TrimUsername(input));
            ApplyForm();
        }
        private void loadDefaultButton_Click(object sender, EventArgs e)
        {
            _sourceImage = GenerateSourceImage();
            ApplyForm();
        }
        private void loadSourceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.png;*.gif;*.bmp;*.webp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _sourceImageName = ofd.FileName;

                //pictureBox.Image = Image.FromFile(_sourceImageName);
                var fileBytes = File.ReadAllBytes(_sourceImageName);
                using (var ms = new MemoryStream(fileBytes))
                {
                    using (var tempImg = Image.FromStream(ms))
                    {
                        _sourceImage = new Bitmap(tempImg);  // 建立複本，不依賴 stream
                    }
                }
            }
            ApplyForm();
        }
        private void loadEmbededImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.png;*.gif;*.bmp;*.webp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _embeddedImageName = ofd.FileName;

                //pictureBox.Image = Image.FromFile(_embeddedImageName);
                var fileBytes = File.ReadAllBytes(_embeddedImageName);
                using (var ms = new MemoryStream(fileBytes))
                {
                    using (var tempImg = Image.FromStream(ms))
                    {
                        _embeddedImage = new Bitmap(tempImg);  // 建立複本，不依賴 stream
                    }
                }
            }
            ApplyForm();
        }
        private void saveEmbededImageButton_Click(object sender, EventArgs e)
        {
            if (_embeddedImage == null)
            {
                MessageBox.Show("No embeded image loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif";
            sfd.Title = "Save Image";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Determine the format based on the file extension
                    ImageFormat format = ImageFormat.Png;
                    string ext = Path.GetExtension(sfd.FileName).ToLower();

                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        default:
                            MessageBox.Show("Unsupported image format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    _embeddedImage.Save(sfd.FileName, format);
                    MessageBox.Show("Image saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void saveDecodedImageButton_Click(object sender, EventArgs e)
        {
            if (_decodeImage == null)
            {
                MessageBox.Show("No embeded image loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif";
            sfd.Title = "Save Image";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Determine the format based on the file extension
                    ImageFormat format = ImageFormat.Png;
                    string ext = Path.GetExtension(sfd.FileName).ToLower();

                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        default:
                            MessageBox.Show("Unsupported image format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    _decodeImage.Save(sfd.FileName, format);
                    MessageBox.Show("Image saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void embedImageButton_Click(object sender, EventArgs e)
        {
            _embeddedImage = EmbedSourceImage();
            ApplyForm();
        }
        private void decodeImageButton_Click(object sender, EventArgs e)
        {
            _decodeImage = DecodeSourceImage();
            ApplyForm();
        }
        private void ApplyForm() 
        {
            this.usernameTextBox.Text = _currnetUser;
            this.secretPictureBox.Image = _secretImage;
            this.sourcePictureBox.Image = _sourceImage;
            this.embededPictureBox.Image = _embeddedImage;
            this.decodePictureBox.Image = _decodeImage;
        }
        private string TrimUsername(string username)
        {
            string output = username.ToUpper();
            output = new string(output.Where(c => c >= 'A' && c <= 'Z' || c == '_').ToArray());

            this.systemLabel.Text = $"Output: {output}";
            if (output.Length > MaxChars)
            {
                output = output.Substring(0, MaxChars);
                this.systemLabel.Text = $"Over the limit length: {MaxChars}. Output: {output}";
            }
            return output;
        }
        private Bitmap GenerateSecretImage_Old(string input) 
        {
            int width = 32; // Parameters
            int height = 32; // Parameters

            // 取得資訊
            string domain = Environment.UserDomainName;
            string user = Environment.UserName;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            domain = "QWER";
            user = "HJOP";
            date = "ZXCV";

            // 建立白底圖
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            // 設定字體
            Font font = new Font("Arial", 16, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);

            // 行距計算
            float lineHeight = (int)g.MeasureString("A", font).Height * 0.8f;
            float y = 1.0f;

            // 畫文字
            g.DrawString(domain, font, brush, new PointF(1, y));
            y += lineHeight;
            g.DrawString(user, font, brush, new PointF(1, y));
            y += lineHeight;
            g.DrawString(date, font, brush, new PointF(1, y));
            y += lineHeight;
            g.DrawString(time, font, brush, new PointF(1, y));

            // 畫 1 pixel 黑色外框
            Pen pen = new Pen(Color.Black, 1);
            g.DrawRectangle(pen, 0, 0, width - 1, height - 1);

            return bmp;
        }
        private Bitmap GenerateSecretImage(string input)
        {
            // 建立 32x32 畫布
            Bitmap bmp = new Bitmap(OutputSize, OutputSize);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            for (int i = 0; i < input.Length; i++)
            {
                char letter = input[i];
                int row = i / MaxCols;
                int col = i % MaxCols;

                int x = col * CharWidth;
                int y = row * CharHeight;

                string imagePath = Path.Combine(AppContext.BaseDirectory, LetterFolder, $"{letter}.png");
                if (!File.Exists(imagePath))
                {
                    Debug.WriteLine($"Error, cannot find the image：{imagePath}");
                    continue;
                }

                Bitmap letterImg = new Bitmap(imagePath);
                g.DrawImage(letterImg, x, y, CharWidth, CharHeight);
            }
            return bmp;
        }

        private Bitmap GenerateSourceImage()
        {
            int width = 500;
            int height = 500;

            Bitmap bmp = new Bitmap(width, height);
            Color bgColor = SystemColors.Control;
            Color borderColor = Color.Black;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 畫邊框（最外圈像素）
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        bmp.SetPixel(x, y, borderColor);
                    else
                        bmp.SetPixel(x, y, bgColor);
                }
            }

            return bmp;
        }

        private Bitmap EmbedSourceImage()
        {
            _watermark = new Watermark(BitmapToBytes(_secretImage));
            //_watermark = new Watermark();
            Bitmap bmp = BytesToBitmap(_watermark.EmbedWatermark(BitmapToBytes(_sourceImage), 1f));
            return bmp;
        }
        private Bitmap DecodeSourceImage()
        {
            _watermark = new Watermark();
            var result = _watermark.RetrieveWatermark(BitmapToBytes(_embeddedImage));
            return BytesToBitmap(result.RecoveredWatermark);
        }
        private static byte[] BitmapToBytes(Bitmap bmp)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png); // 可改為 Jpeg、Bmp 等
                return ms.ToArray();
            }
        } 
        private static Bitmap BytesToBitmap(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (var original = new Bitmap(ms))
                {
                    return new Bitmap(original); // 複製一份，避免依賴 ms
                }
            }
        }
        private static string CallPowershellProcess(string commandLine)
        {
            string result = string.Empty;
            string arguments = string.Empty;
            string filename = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                arguments = commandLine;
                process.StartInfo.FileName = filename;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.StartInfo.Arguments = arguments;
                process.Start();
                result = process.StandardOutput.ReadToEnd().Trim();
            }
            return result;
        }
    }
}
