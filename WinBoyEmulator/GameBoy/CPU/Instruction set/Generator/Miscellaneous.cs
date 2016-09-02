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
    public partial class Generator
    {
        private void _miscControlInstructions()
        {
            _instructionSet[0x00] = new Opcode { Mnemonic = Operand.NOP };
            _instructionSet[0x10] = new Opcode
            {
                Mnemonic = $"{Operand.STOP} 0",
                LengthInByte = 2
            };
            _instructionSet[0x76] = new Opcode { Mnemonic = Operand.HALT };
            _instructionSet[0xCB] = new Opcode { Mnemonic = Operand.CB };
            _instructionSet[0xF3] = new Opcode { Mnemonic = Operand.DI };
            _instructionSet[0xFB] = new Opcode { Mnemonic = Operand.EI };
        }
    }
}
