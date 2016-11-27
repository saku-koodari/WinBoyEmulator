using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinBoyEmulator.Rendering;

namespace WinBoyEmulator.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            // Initialize form with SharpDX (instead of .NET tools)
            using (var renderer = new SharpDX())
            {
                // inject renderer inside to the form.
                // Because it's designed to set renderer values in the Form instead of here. 
                // So we just initialize and run the renderer here.
                var form = new MainForm(renderer);
                renderer.Run(form);
            }
        }
    }
}
