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
//     along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.CPU.Regiser
{

    public class Register : IRegisters
    {
        /// <summary>8-bit register A. Value between 0x8000 - 0x0100.</summary>
        public byte A { get; set; }
        /// <summary>8-bit Flag register F. Value between 0x0080 - 0x0000.</summary>
        public byte F { get; set; }
        /// <summary>16-bit register AF. Combined register A with register F.</summary>
        public ushort AF { get; set; }

        /// <summary>8-bit register B. Value betweem 0x8000 - 0x0100.</summary>
        public byte B { get; set; }
        /// <summary>8-bit register C. Value between 0x0080 - 0x0000.</summary>
        public byte C { get; set; }
        /// <summary>16-bit register BC. Combined register B with register C.</summary>
        public ushort BC { get; set; }

        /// <summary>8-bit register D. Value between 0x8000 - 0x0100.</summary>
        public byte D { get; set; }
        /// <summary>8-bit register E. Value between 0x0080 - 0x0000.</summary>
        public byte E { get; set; }
        /// <summary>16-bit register DE. Combined register D with register E.</summary>
        public ushort DE { get; set; }

        /// <summary>8-bit register H. Value between 0x8000 - 0x0100.</summary>
        public byte H { get; set; }
        /// <summary>8-bit register L. Value between 0x0080 - 0x0000.</summary>
        public byte L { get; set; }
        /// <summary>16-bit register HL. Combined register H with register L.</summary>
        public ushort HL { get; set; }

        /// <summary>16-bit Stack Pointer register</summary>
        public ushort SP { get; set; }

        /// <summary>16-bit Program Counter. Initialize value 0x100.</summary>
        public ushort PC { get; set; } = 0x100;
    }
}