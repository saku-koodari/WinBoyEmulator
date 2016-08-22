using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.CPU
{
    /// <summary>Flag for F register.</summary>
    public class Flags
    {
        /// <summary>
        /// Zero Flag: This bit is set when,
        /// the result of a math operation is zero or two values match,
        /// when using the CP instruction.
        /// </summary>
        public const byte Z = 0x80;

        /// <summary>
        /// Subtract Flag: This bit is set if,
        /// a subtraction was performed in the last math operation.
        /// </summary>
        public const byte N = 0x40;

        /// <summary>
        /// Half Carry Flag: This bit set if,
        /// a carry occurred from the lower nibble in the last math operation.
        /// </summary>
        public const byte H = 0x20;

        /// <summary>
        /// Carry Flag: This bit set if,
        /// a carry occurred fom the last math operation or 
        /// register A is the smaller value when executing the CP instruction.
        /// </summary>
        public const byte C = 0x10;
    }
}
