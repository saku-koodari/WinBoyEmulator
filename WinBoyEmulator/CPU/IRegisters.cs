using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.CPU
{
    interface IRegisters
    {
        // 8-bit registers
        byte A { get; set; }
        byte B { get; set; }
        byte C { get; set; }
        byte D { get; set; }
        byte E { get; set; }
        byte F { get; set; }
        byte H { get; set; }
        byte L { get; set; }

        // 16-bit registers
        ushort AF { get; set; }
        ushort BC { get; set; }
        ushort DE { get; set; }
        ushort HL { get; set; }

        ushort SP { get; set; }
        ushort PC { get; set; }
    }
}
