using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.Configuration
{
    public static class Colors
    {
        public static byte[] Palette = new byte[4] 
        {
            (byte)Color.White,
            (byte)Color.Silver,
            (byte)Color.Grey,
            (byte)Color.Black
        };
    }
}
