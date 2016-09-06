using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.Configuration
{
    public static class Colors
    {
        public static int Count = Palette.Length;

        public static byte[] Palette = new byte[4] { 255, 192, 96, 0 };
    }
}
