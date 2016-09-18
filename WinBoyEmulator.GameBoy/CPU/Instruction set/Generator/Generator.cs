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

namespace WinBoyEmulator.GameBoy.CPU.Instruction_set.Generator
{
    /// <summary>
    /// Generator
    /// </summary>
    internal static partial class Generator
    {
        // Main partial of this class
        private static Instruction DEFAULT = new Instruction { };

        private static Instruction[] _instructionSet = new Instruction[0x200];

        public  static Instruction[] InstructionSet => _instructionSet;


        static Generator()
        {
            _miscControlInstructions();
            _jumpAndCalls();
            _loadStoreAndMove();
        }

        private static void _setInstruction(byte value, string operand, string destination = null, 
            string source = null, int? length = null, int? duration = null, Flags flagsAffected = default(Flags))
        {
            _instructionSet[value] = new Instruction
            {
                Value = value,
                Operand = operand,
                Destination = destination ?? DEFAULT.Destination,
                Source = source ?? DEFAULT.Source,
                Duration = duration ?? DEFAULT.Duration,
                Length = length ?? DEFAULT.Length,
                FlagsAffected = flagsAffected
            };
        }
    }
}
