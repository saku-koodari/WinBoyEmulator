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
    public class Instruction : IInstruction
    {    
        /// <summary>Opcode as byte value.</summary>
        public byte Value { get; set; }
        /// <summary>Length in bytes. Either 1 or 2.</summary>
        public int Length { get; set; }
        /// <summary>Duration in cycles.</summary>
        public int Duration { get; set; }
        /// <summary>First part of opcode.<para />Format of opcode is: {0} {1},{2}</summary>
        public string Operand { get; set; }
        /// <summary>Second part of opcode.<para />Format of opcode is: {0} {1},{2}</summary>
        public string Destination { get; set; }
        /// <summary>Third part of opcode.<para />Format of opcode is: {0} {1},{2}</summary>
        public string Source { get; set; }
        /// <summary>Flags that is affected in this instruction.</summary>
        public Flags FlagsAffected { get; set; }
    }
}
