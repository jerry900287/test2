using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;

namespace Watermark
{
    public struct WatermarkConfig
    {
        public bool WatermarkSwitch;
        public int ScreenDetectionPeriod;
        public int TextFormMarigin;
        public double TextFormOpacity;
        public int TextFormPeriod;
        public Color TextFormLabelColor;
        public string TextFormLabelFont;
        public float TextFormLabelSize;
    }
    public partial class StartForm : Form
    {
        Screen[] screens;
        WatermarkConfig watermarkConfig = new WatermarkConfig();
        List<TextForm> textFormList = new List<TextForm>();
        TextForm textForm;

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            screens = Screen.AllScreens;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = new Rectangle(0, 0, 100, 100);
            this.ShowInTaskbar = false;
            this.Opacity = 0.0;

            //Thread.Sleep(10000);
            LoadWatermarkConfig();
            CreateTextForm();
        }

        private void LoadWatermarkConfig()
        {

            String registryKeyPath = @"HKEY_CURRENT_USER\SOFTWARE\SelfService\Watermark";
            watermarkConfig.WatermarkSwitch = bool.Parse(Registry.GetValue(registryKeyPath, "WatermarkSwitch", "True")?.ToString() ?? "True");
            watermarkConfig.ScreenDetectionPeriod = Convert.ToInt32(Registry.GetValue(registryKeyPath, "ScreenDetectionPeriod", "3000")?.ToString() ?? "3000");
            watermarkConfig.TextFormMarigin = Convert.ToInt32(Registry.GetValue(registryKeyPath, "TextFormMarigin", "40")?.ToString() ?? "40");
            watermarkConfig.TextFormOpacity = Convert.ToDouble(Registry.GetValue(registryKeyPath, "TextFormOpacity", "0.07")?.ToString() ?? "0.07");
            watermarkConfig.TextFormPeriod = Convert.ToInt32(Registry.GetValue(registryKeyPath, "TextFormPeriod", "120000")?.ToString() ?? "120000");
            watermarkConfig.TextFormLabelColor = ColorTranslator.FromHtml(Registry.GetValue(registryKeyPath, "TextFormPromptLabelColor", "Black")?.ToString() ?? "Black");
            watermarkConfig.TextFormLabelFont = Registry.GetValue(registryKeyPath, "TextFromLabelFont", "Arial")?.ToString() ?? "Arial";
            watermarkConfig.TextFormLabelSize = Convert.ToSingle(Registry.GetValue(registryKeyPath, "TextFormLabelSize", "20")?.ToString() ?? "20");
        }

        public void CreateTextForm()
        {
            if (watermarkConfig.WatermarkSwitch)
            {
                foreach (Screen screen in screens)
                {
                    textForm = new TextForm(screen, watermarkConfig);
                    textForm.Show();
                    textFormList.Add(textForm);
                }
            }
        }
        public void RemoveTextForm()
        {
            foreach (TextForm textForm in textFormList)
            {
                textForm.Hide();
                textForm.Close();
            }
            textFormList.Clear();
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

        private bool isScreenChanged()
        {
            Screen[] currentScreens = Screen.AllScreens;
            if (screens.Length != currentScreens.Length)
            {
                screens = currentScreens;
                return true;
            }
            else
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    if (screens[i] != currentScreens[i])
                    {
                        screens = currentScreens;
                        return true;
                    }
                }
            }
            return false;
        }

        private void screenDetectionTimer_Tick(object sender, EventArgs e)
        {
            if (isScreenChanged())
            {
                RemoveTextForm();
                CreateTextForm();
            }
        }
    }
}