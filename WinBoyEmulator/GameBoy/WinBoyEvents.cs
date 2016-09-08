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
        public delegate void DrawEventHandler(Screen screen, PaintEventArgs e);
    }
}
