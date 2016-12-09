using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.Core
{
    // BIOS = Basic Input Output System
    // ROM  = Read-only Memory
    // RAM  = Random-access Memroy
    // VRAM = Video RAM

    /// <summary>
    /// GameBoy's memory 
    /// </summary>
    public abstract class MemoryBase
    {
        private byte[] _bios = null;

        public MemoryBase() { }

        public abstract byte ReadByte(int address);

        public abstract void WriteByte(int address, byte value);

        public byte[] Bios
        {
            get
            {
                return _bios;
            }
            set
            {
                if (_bios != null)
                    throw new InvalidOperationException("Bios is read-only memory.");

                _bios = value;
            }
        }

        public byte[] Rom { get; set; }

        public byte[] VideoRam { get; set; }

        public byte[] ExternalRam { get; set; }

        public byte[] WorkingRam { get; set; }

        public byte[] ZeropageRam { get; set; }

        public void ResetMemory()
        {
            // Bios won't be reseted naturally
            ResetSinglePartOfMemory(Rom);
            ResetSinglePartOfMemory(VideoRam);
            ResetSinglePartOfMemory(ExternalRam);
            ResetSinglePartOfMemory(WorkingRam);
            ResetSinglePartOfMemory(ZeropageRam);
        }

        /// <summary>
        /// Resets the single part of memory.
        /// </summary>
        /// <param name="memoryPart">The memory part.</param>
        private void ResetSinglePartOfMemory(byte[] memoryPart)
        {
            for(var i = 0; i< memoryPart.Length; i++)
                memoryPart[i] = 0;
        }
    }
}
