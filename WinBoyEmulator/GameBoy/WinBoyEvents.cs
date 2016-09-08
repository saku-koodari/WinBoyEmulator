using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.GameBoy.GPU;

namespace WinBoyEmulator.GameBoy
{
    public class WinBoyEvents
    {
        public delegate void DrawEventHandler(Screen screen, EventArgs e);
    }
}
