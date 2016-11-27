using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.Core;

namespace WinBoyEmulator.GameBoyConsoles
{
    public class GameBoy : BaseConsole, IEmulatable
    {
        public GameBoy()
        {
            Width = 160;
            Height = 140;
        }

        public void EmulateCpu()
        {
            // Step 1 - Read byte from memory
            // Step 2 - Decode instrucion by fetched byte
            // Step 3 - Execute instruction
        }

        public void EmulateSound()
        {

        }
    }

    public class GameBoyColor
    {

    }
}
