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

using WinBoyEmulator.GameBoy.CPU.Instruction_set;
using WinBoyEmulator.GameBoy.CPU.Instruction_set.Generator;

using MMU = WinBoyEmulator.GameBoy.Memory.Memory;

namespace WinBoyEmulator.GameBoy.CPU
{
    /// <summary>
    /// LR35902 is the processor of the Game Boy. <para />
    /// Inherit Flags (Z,N,H,C, all typeof byte). <para />
    /// implement interface IRegisters.
    /// </summary>
    internal class LR35902 : Flag, IRegisters
    {
        private static readonly object _syncRoot = new object();
        private static volatile LR35902 _instance;

        private bool _isCpuRunning = false;

        // store registers values to these bytes.
        private byte _a, _b, _c, _d, _e, _f, _h, _l;
        private int _sp, _pc;

        #region Registers Accessors
        /// <summary>8-bit register A. Value between 0x8000 - 0x0100.</summary>
        public byte A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }
        /// <summary>8-bit Flag register F. Value between 0x0080 - 0x0000.</summary>
        public byte F
        {
            get
            {
                return _f;
            }
            set
            {
                _f = (byte)(value & 0xF0);
            }
        }
        /// <summary>16-bit register AF. Combined register A with register F.</summary>
        public int AF
        {
            get
            {
                return (_a << 8) | F;
            }
            set
            {
                _a = (byte)(value >> 8);
                _f = (byte)(value & 0xFF);
            }
        }

        /// <summary>8-bit register B. Value betweem 0x8000 - 0x0100.</summary>
        public byte B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }
        /// <summary>8-bit register C. Value between 0x0080 - 0x0000.</summary>
        public byte C
        {
            get
            {
                return _c;
            }
            set
            {
                _c = value;
            }
        }
        /// <summary>16-bit register BC. Combined register B with register C.</summary>
        public int BC
        {
            get
            {
                return (_b << 8) | _c;
            }
            set
            {
                _b = (byte)(value >> 8);
                _c = (byte)(value & 0xFF);
            }
        }

        /// <summary>8-bit register D. Value between 0x8000 - 0x0100.</summary>
        public byte D
        {
            get
            {
                return _d;
            }
            set
            {
                _d = value;
            }
        }
        /// <summary>8-bit register E. Value between 0x0080 - 0x0000.</summary>
        public byte E
        {
            get
            {
                return _e;
            }
            set
            {
                _e = value;
            }
        }
        /// <summary>16-bit register DE. Combined register D with register E.</summary>
        public int DE
        {
            get
            {
                return (_d << 8) | _e;
            }
            set
            {
                _d = (byte)(value >> 8);
                _e = (byte)(value & 0xFF);
            }
        }

        /// <summary>8-bit register H. Value between 0x8000 - 0x0100.</summary>
        public byte H
        {
            get
            {
                return _h;
            }
            set
            {
                _h = value;
            }
        }
        /// <summary>8-bit register L. Value between 0x0080 - 0x0000.</summary>
        public byte L
        {
            get
            {
                return _l;
            }
            set
            {
                _l = value;
            }
        }
        /// <summary>16-bit register HL. Combined register H with register L.</summary>
        public int HL
        {
            get
            {
                return (_h << 8) | _l;
            }
            set
            {
                _h = (byte)(value >> 8);
                _l = (byte)(value & 0xFF);
            }
        }

        /// <summary>16-bit Stack Pointer register</summary>
        public int SP
        {
            get
            {
                return _sp;
            }
            set
            {
                _sp = value;
            }
        }

        /// <summary>16-bit Program Counter. Initialize value 0x100.</summary>
        public int PC
        {
            get
            {
                return _pc;
            }
            set
            {
                _pc = value;
            }
        }
        #endregion

        // #region Time related

        public int LengthInBytes { get; set; }
        public int DurationInCycles { get; set; }

        // #endregion

        // I am using different methods for setting 8-bit and 16-bit value.
        // Reason is that, then I don't have to do any extra casting.

        /// <summary>
        /// Set byte to 8-bit register.
        /// </summary>
        /// <param name="register">Register. Use a const string of a static class Register.</param>
        /// <param name="value">8-bit (byte) value</param>
        private void _setByteToRegister(string register, byte value)
        {
            switch(register)
            {
                case Register.A: A = value; break;
                case Register.B: B = value; break;
                case Register.C: C = value; break;
                case Register.D: D = value; break;
                case Register.E: E = value; break;
                case Register.F: F = value; break;
                case Register.H: H = value; break;
                case Register.L: L = value; break;
                default:
                    throw new ArgumentException("Register doesn't match with 16-bit register", nameof(register));
            }
        }

        /// <summary>
        /// Set value to 16-bit register.
        /// </summary>
        /// <param name="register">Register. Use a const string of a static class Register.</param>
        /// <param name="value"></param>
        private void _setValueToRegister(string register, int value)
        {
            switch(register)
            {
                case Register.AF: AF = value; break;
                case Register.BC: BC = value; break;
                case Register.DE: DE = value; break;
                case Register.HL: HL = value; break;
                case Register.PC: PC = value; break;
                case Register.SP: SP = value; break;
                default:
                    throw new ArgumentException("Register doesn't match with 16-bit register", nameof(register));
            }
        }

        private byte _getByteFromRegister(string register)
        {
            switch (register)
            {
                case Register.A: return A;
                case Register.B: return B;
                case Register.C: return C;
                case Register.D: return D;
                case Register.E: return E;
                case Register.F: return F;
                case Register.H: return H;
                case Register.L: return L;
                default:
                    throw new ArgumentException("Register doesn't match with 8-bit register", nameof(register));
            }
        }

        private int _getValueFromRegister(string register)
        {
            switch (register)
            {
                case Register.AF: return AF;
                case Register.BC: return BC;
                case Register.DE: return DE;
                case Register.HL: return HL;
                case Register.PC: return PC;
                case Register.SP: return SP;
                default:
                    throw new ArgumentException("Register doesn't match with 8-bit register", nameof(register));
            }
        }

        private void _executeOperand(Instruction opcode)
        {
            if (opcode == null)
                throw new ArgumentNullException(nameof(opcode), "Current instruction doesn't exists.");

            // Duration and length is set every time.
            DurationInCycles = opcode.Duration;
            LengthInBytes = opcode.Length;

            switch(opcode.Operand)
            {
                // Misc/Control
                case Operand.NOP: /* No Operation */ break;

                // Load/Store/Move
                case Operand.LD: _ld(opcode); break;

                default:
                    throw new NotImplementedException();
            }
        }

        /*
         0xE0: LDH (a8),A
         0xF0: LDH A,(a8)
         0xEA: LD (a16),A
         0xFA: LD A,(a16)
        */

        /* var opcode = new Instruction
        {
            Value = 0x31,
            Length = 3
            Duration = 12,
            Operand = "LD" // Operand.LD
            Destination = "SP",
            Source = "d16",
            FlagsAffected = default(Flags),
        }; */
        /// <summary>Load/Store/Move Operation.</summary>
        private void _ld(Instruction opcode)
        {
            // LD A,(C)         has alternative mnemonic LD A, ($FF00 + C)
            // LD C,(A)         has alternative mnemonic LD($FF00 + C), A
            // LDH A, (a8)      has alternative mnemonic LD A, ($FF00 + a8)
            // LDH(a8),A        has alternative mnemonic LD($FF00 + a8),A
            // LD A,(HL +)      has alternative mnemonic LD A,(HLI)or LDI A,(HL)
            // LD(HL +), A      has alternative mnemonic LD(HLI), A or LDI(HL), A
            // LD A, (HL -)     has alternative mnemonic LD A, (HLD)or LDD A, (HL)
            // LD(HL -), A      has alternative mnemonic LD(HLD), A or LDD(HL), A
            // LD HL, SP + r8   has alternative mnemonic LDHL SP, r8

            switch(opcode.Source)
            {
                case Source.d8:
                case Source.d16: 
                    // When opcode.Value is: 0x01, 0x11, 0x21, 0x31
                    // How about redesign, to not to cast here?
                    SP = MMU.Instance.ReadShort(PC);
                    PC += 2;
                    break;
                // case Source.a8: // a8 is used only with LDH
                case Source.a16:
                case Source.r8:
                    // Only situation: 0xF8: LD HL,SP+r8
                    throw new NotImplementedException();
                default:
                    // For example if destination is any Register.
                    throw new NotImplementedException();
            }
        }

        public static LR35902 Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_syncRoot)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new LR35902();
                }

                return _instance;
            }
        }

        public void EmulateCycle()
        {
            // fetch operand
            var opcode = MMU.Instance.ReadByte(PC++);

            // decode operand
            var instruction = Generator.InstructionSet[opcode];

            // execute operand
            _executeOperand(instruction);
        }

        public void Reset()
        {
            // Since we have combined registers, we can use them.

            AF = 0x0000;
            BC = 0x0000;
            DE = 0x0000;
            HL = 0x0000;
            SP = 0x0000;
            PC = 0x0000;
        }
    }
}
