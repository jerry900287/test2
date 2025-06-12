using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watermark
{
    public partial class TextForm : Form
    {
        Screen screen;
        WatermarkConfig watermarkConfig;
        List<Label> labelList = new List<Label>();
        List<List<Point>> labelPositionList = new List<List<Point>>();
        string username = CallPowershellProcess(@"Get-WMIObject -ClassName Win32_ComputerSystem | Select-Object -ExpandProperty Username").ToUpper();
        string hostname = Environment.MachineName;
        Random random = new Random(Guid.NewGuid().GetHashCode());
        int randomNumber = 0;
        int adjustedScreenWidth = 0;
        int adjustedScreenHeight = 0;


        //////// Cursor Penetration ////////
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);
        ////////////////////////////////////////

        public TextForm(Screen _screen, WatermarkConfig _watermarkConfig)
        {
            screen = _screen;
            watermarkConfig = _watermarkConfig;
            InitializeComponent();
        }

        private void TextForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = screen.Bounds;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            this.Opacity = watermarkConfig.TextFormOpacity;

            this.dateTimeTimer.Enabled = true;
            this.dateTimeTimer.Interval = 1000;
            this.labelPositionChangeTimer.Enabled = true;
            this.labelPositionChangeTimer.Interval = watermarkConfig.TextFormPeriod;

            /*Parameter*/
            username = "Jerry"; 
            string imageEncoderPath = AppDomain.CurrentDomain.BaseDirectory + "ImageEncoder.exe";
            string embeddedWatermarkPath = AppDomain.CurrentDomain.BaseDirectory + "EmbeddedWatermark.png";

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = imageEncoderPath;
            psi.Arguments = "encode"; // 可省略
            //psi.WorkingDirectory = @"C:\Path\To\"; // 可省略
            psi.UseShellExecute = false;
            psi.CreateNoWindow = false; // 設 true 則隱藏視窗

            Process proc = Process.Start(psi);
            proc.WaitForExit(); // 如果你希望等程式執行完再繼續

            if (File.Exists(embeddedWatermarkPath))
            {
                // 設定背景圖片
                this.BackgroundImage = Image.FromFile(embeddedWatermarkPath);
                // 設定背景圖片為平鋪（Tile）布局
                this.BackgroundImageLayout = ImageLayout.Tile;
            }
            else
            {
                Console.WriteLine($"The {embeddedWatermarkPath} is not exist.");
            }
            /*--------------------------*/

            adjustedScreenWidth = screen.Bounds.Width - (2 * watermarkConfig.TextFormMarigin);
            adjustedScreenHeight = screen.Bounds.Height - (2 * watermarkConfig.TextFormMarigin);
            LabelPositionInitial();
            randomNumber = random.Next(0, labelPositionList.Count());
            LabelInitial();

            SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);


        }

        private void LabelPositionInitial()
        {
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 7), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 7) });
            labelPositionList.Add(new List<Point>() { new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 1), new Point(adjustedScreenWidth / 8 * 5, adjustedScreenHeight / 8 * 3), new Point(adjustedScreenWidth / 8 * 3, adjustedScreenHeight / 8 * 5), new Point(adjustedScreenWidth / 8 * 1, adjustedScreenHeight / 8 * 7), new Point(adjustedScreenWidth / 8 * 7, adjustedScreenHeight / 8 * 7) });
        }

        private void LabelInitial()
        {
            labelList.Add(this.label1);
            labelList.Add(this.label2);
            labelList.Add(this.label3);
            labelList.Add(this.label4);
            labelList.Add(this.label5);
            foreach (var label in labelList.Select((value, index) => new { value, index }))
            {
                label.value.Font = new Font(watermarkConfig.TextFormLabelFont, watermarkConfig.TextFormLabelSize);
                label.value.ForeColor = watermarkConfig.TextFormLabelColor;
                label.value.Text = hostname + "\n" + username + "\n" + DateTime.Now.ToString("ddd, MM dd yyyy,\nhh:mm:ss tt", new CultureInfo("en-US"));
                label.value.Location = new Point(labelPositionList[randomNumber][label.index].X - (label.value.Width / 2) + watermarkConfig.TextFormMarigin, labelPositionList[randomNumber][label.index].Y - (label.value.Height / 2) + watermarkConfig.TextFormMarigin);
            }

        }

        private void dateTimeTimer_Tick(object sender, EventArgs e)
        {
            foreach (Label label in labelList)
            {
                label.Text = hostname + "\n" + username + "\n" + DateTime.Now.ToString("ddd, MM dd yyyy,\nhh:mm:ss tt", new CultureInfo("en-US"));
            }
        }

        private void labelPositionChangeTimer_Tick(object sender, EventArgs e)
        {
            randomNumber = random.Next(0, labelPositionList.Count());
            foreach (var label in labelList.Select((value, index) => new {value, index}))
            {
                label.value.Location = new Point(labelPositionList[randomNumber][label.index].X - (label.value.Width / 2) + watermarkConfig.TextFormMarigin, labelPositionList[randomNumber][label.index].Y - (label.value.Height / 2) + watermarkConfig.TextFormMarigin);
            }
        }

        static private string CallPowershellProcess(string commandLine)
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


        // Disappear in Alt+Tab
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;     // Enable WS_EX_TOOLWINDO
                cp.ExStyle &= ~0x20;    // Disable WS_EX_APPWINDOW
                return cp;
            }
        }

        private void TextForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        
    }
}
