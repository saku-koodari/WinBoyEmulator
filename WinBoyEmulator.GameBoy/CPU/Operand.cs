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
    internal static class Operand
    {
        #region Misc/control instructions (red)
        /// <summary>Misc/control instructions (red)</summary>
        public const string NOP = "NOP";

        /// <summary>Misc/control instructions (red)</summary>
        public const string STOP = "STOP";

        /// <summary>Misc/control instructions (red)</summary>
        public const string HALT = "HALT";

        /// <summary>Misc/control instructions (red)</summary>
        public const string CB = "CB";

        /// <summary>Misc/control instructions (red)</summary>
        public const string DI = "DI";

        /// <summary>Misc/control instructions (red)</summary>
        public const string EI = "EI";
        #endregion

        /// <summaryJump / calls (orange)</summary>
        public const string JR = "JR";

        /// <summary>8 and 16-bit load/store/move instructions (purple)</summary>
        public const string LD = "LD";

        #region 8 & 16 bit arithmetic/logical instructions
        /// <summary>8 & 16 bit arithmetic/logical instructions</summary>
        public const string INC = "INC";

        /// <summary>8 & 16 bit arithmetic/logical instructions</summary>
        public const string DEC = "DEC";

        /// <summary>8 & 16 bit arithmetic/logical instructions</summary>
        public const string ADD = "ADD";
        #endregion

        #region 8 bit arithmetic/logical instructions
        public const string ADC = "ADC";
        public const string SUB = "SUB";
        public const string SBC = "SBC";
        public const string AND = "AND";
        public const string XOR = "XOR";
        public const string OR = "OR";
        public const string CP = "CP";
        public const string DAA = "DAA";
        public const string SCF = "SCF";
        public const string CPL = "CPL";
        public const string CCF = "CCF";
        #endregion

        #region 8 bit rotations/shift bit instructions
        /// <summary>8 bit rotations/shift bit instructions</summary>
        public const string RLCA = "RLCA";

        /// <summary>8 bit rotations/shift bit instructions</summary>
        public const string RRCA = "RRCA";

        /// <summary>8 bit rotations/shift bit instructions</summary>
        public const string RLA = "RLA";

        /// <summary>8 bit rotations/shift bit instructions</summary>
        public const string RRA = "RRA";
        #endregion

        #region 8 bit rotations/shift bit instructions - Prefix CB
        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string RLC = "RLC";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string RRC = "RRC";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string RL = "RL";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string RR = "RR";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string SLA = "SLA";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string SRA = "SRA";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string BIT = "BIT";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string RES = "RES";

        /// <summary>8 bit rotations/shift bit instructions <para/> Prefix CB</summary>
        public const string SET = "SET";
        #endregion
    }
}
