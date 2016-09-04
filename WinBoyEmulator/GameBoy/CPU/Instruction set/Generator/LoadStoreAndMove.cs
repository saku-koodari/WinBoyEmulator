using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU.Instruction_set.Generator
{
    public static partial class Generator
    {
        public static void _loadStoreAndMove()
        {
            _setInstruction(0x31, Operand.LD, "SP", "d16", 3, 12);
        }
    }
}
