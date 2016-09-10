using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Screen = WinBoyEmulator.GameBoy.GPU.Screen;

namespace WinBoyEmulator.GameBoy
{
    public class WinBoyEvents
    {
        /// <summary>
        /// Event handler for drawing Screen to form.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="e"></param>
        public delegate void DrawEventHandler(Screen screen, PaintEventArgs e);
    }
}
