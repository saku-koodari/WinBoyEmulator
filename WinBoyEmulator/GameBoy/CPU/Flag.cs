// This file is part of WinBoyEmulator.
// 
// WinBoyEmulator is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     WinBoyEmulator is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with WinBoyEmulator.  If not, see<http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.CPU
{
    /// <summary>Flag for F register.</summary>
    public class Flag
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
