using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.CPU
{
    /// <summary>
    /// LR35902 is the processor of the Game Boy. <para />
    /// Inherit Flags (Z,N,H,C, all typeof byte). <para />
    /// implement interface IRegisters.
    /// </summary>
    public class LR35902 : Flags, IRegisters
    {
        // store registers values to these bytes.
        private byte _a, _b, _c, _d, _e, _f, _h, _l;
        private ushort _sp, _pc;

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
        public ushort AF
        {
            get
            {
                return (ushort)((_a << 8) | F);
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
        public ushort BC
        {
            get
            {
                return (ushort)((_b << 8) | _c);
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
        public ushort DE
        {
            get
            {
                return (ushort)((_d << 8) | _e);
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
        public ushort HL
        {
            get
            {
                return (ushort)((_h << 8) | _l);
            }
            set
            {
                _h = (byte)(value >> 8);
                _l = (byte)(value & 0xFF);
            }
        }

        /// <summary>16-bit Stack Pointer register</summary>
        public ushort SP
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
        public ushort PC
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
