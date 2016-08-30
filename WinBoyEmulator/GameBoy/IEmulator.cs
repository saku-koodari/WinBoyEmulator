using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy
{
    /// <summary>Interface IEmulator. class Emulator impments this.</summary>
    public interface IEmulator
    {
        /// <summary>Starts emulation without game inside.</summary>
        void StartEmulation();
        /// <summary>
        /// Starts emulation with game inside.
        /// </summary>
        /// <param name="gamePath">path of the game. File type must be .gb</param>
        void StartEmulation(string gamePath);
    }
}
