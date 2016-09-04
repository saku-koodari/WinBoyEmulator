using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU
{
    public struct Flags
    {
        byte? Z { get; set; }
        byte? H { get; set; }
        byte? N { get; set; }
        byte? C { get; set; }

        public Flags(byte? z, byte? n, byte? h, byte? c)
        {
            Z = z;
            N = n;
            H = h;
            C = c;
        }
    }
}
