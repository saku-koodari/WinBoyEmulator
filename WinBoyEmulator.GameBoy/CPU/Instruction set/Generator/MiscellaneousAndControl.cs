using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU.Instruction_set.Generator
{
    /// <summary>
    /// Miscellaneous / 
    /// Control instructions
    /// </summary>
    public static partial class Generator
    {
        private static void _miscControlInstructions()
        {
            _setInstruction(0x00, Operand.NOP);
            _setInstruction(0x10, Operand.STOP, destination:"0", length: 2);
            _setInstruction(0x76, Operand.HALT);
            _setInstruction(0x76, Operand.CB);
            _setInstruction(0xF3, Operand.DI);
            _setInstruction(0xFB, Operand.EI);
        }
    }
}
