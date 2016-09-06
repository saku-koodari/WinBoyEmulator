using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static WinBoyEmulator.GameBoy.Configuration.Color;

namespace WinBoyEmulator.GameBoy.Configuration
{
    public static class Colors
    {
        public static int Count = Palette.Length;

        public static byte[] Palette = new byte[4] { White, Silver, Grey, Black };
    }
}
