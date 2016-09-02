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

namespace WinBoyEmulator.GameBoy.CPU.Instruction_set
{
    /// <summary>
    /// Default values, that is used here, is for NOP
    /// If this is too heavy, alter this class to struct (it's more efficient)
    /// </summary>
    public class Opcode : IOpcode
    {
        public string Mnemonic { get; set; }
        /// <summary>Default value is 1.</summary>
        public int LengthInByte { get; set; } = 0x1;
        /// <summary>Default value is 4.</summary>
        public int DurationInCycles { get; set; } = 0x4;
        /// <summary>Zero is default value, which means no flag affected.</summary>
        public byte FlagsAffected { get; set; } = 0x0;
    }
}
