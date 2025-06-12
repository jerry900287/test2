using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEncoder
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                RunCommandLineMode(args);
            }
            else
            {
                RunGUIMode();
            }
        }
        static void RunCommandLineMode(string[] args)
        {
            try
            {
                Console.WriteLine("Running in command-line mode");

                foreach (var arg in args)
                {
                    Console.WriteLine($"Argument: {arg}");
                }

                // 建立表單但不顯示
                using (var form = new MainForm(args[0]))
                {
                    Console.WriteLine("Lunch MainFrom.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static void RunGUIMode()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // 啟動視窗模式
        }
    }
}
