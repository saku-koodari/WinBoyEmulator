using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.EmulatorBase;

namespace WinBoyEmulator.GameBoy
{
    public partial class Console : IEmulable
    {
        /// <summary>The Memory Management Unit</summary>
        private Memory _mmu;

        public Console()
        {
            // Basic dimensions for GameBoy
            Width = 160;
            Height = 140;

            _mmu = new Memory
            {
                Bios = _bios, // size = 256
                ExternalRam = new byte[8192],
                WorkingRam  = new byte[8192],
                ZeropageRam = new byte[128]
            };

            // Resets the memory (fills the memory with zeroes).
            _mmu.Reset();
        }

        public int Width { get; set; }
        public int Height { get; set; }

        private void InitializeMemoryUnit(byte[] memoryUnit)
        {
            for(var i = 0;  i < memoryUnit.Length; i++)
                memoryUnit[i] = 0;
        }

        public void EmulateCpu()
        {
            // Step 1 - Read byte from memory
            // Step 2 - Decode instrucion by fetched byte
            // Step 3 - Execute instruction
        }
    }


}
