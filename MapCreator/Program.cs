using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace MapCreator
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void LoadMagickImage()
        {
            if (IntPtr.Size == 4)
            {
                Assembly assembly = Assembly.LoadFrom(string.Format("lib/{1}", Application.StartupPath, "Magick.NET-x86.dll"));
            }
        }

    }
}
