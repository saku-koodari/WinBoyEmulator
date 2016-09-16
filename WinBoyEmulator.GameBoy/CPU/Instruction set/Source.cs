using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU.Instruction_set
{
    /// <summary>
    /// This class is used to determinite source of opcode <para/>
    /// Use this with Instruction.Source
    /// </summary>
    public static class Source
    {
        /// <summary>8-bit data.</summary>
        public const string d8 = "d8";

        /// <summary>Immediate 16-bit data.</summary>
        public const string d16 = "d16";

        /// <summary>
        /// 8-bit unsigned data, which are added to 0xFF00 in certain instructions <para/>
        /// Replacement for missing IN and OUT instructions.
        /// </summary>
        public const string a8 = "a8";

        /// <summary>16-bit address</summary>
        public const string a16 = "a16";

        /// <summary>8-bit signed data, which are added to program counter.</summary>
        public const string r8 = "r8";
    }
}
