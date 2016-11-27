using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.Core
{
    /// <summary>Enum for select GameBoy console.</summary>
    public enum GameBoyConsole
    {
        /// <summary>The unknown Game Boy console.</summary>
        Unknown,

        /// <summary>The regular Game Boy console.</summary>
        GameBoy,

        /// <summary>The game Boy pocket.</summary>
        GameBoyPocket,

        /// <summary>Game boy light.</summary>
        GameBoyLight,

        /// <summary>Game Goy Color.</summary>
        GameBoyColor,

        /// <summary>Game Boy Advance.</summary>
        GameBoyAdvance,

        /// <summary>Game Boy Advance SP</summary>
        GameBoyAdvanceSP,

        /// <summary>Game Boy micro.</summary>
        GameBoyMicro
    }
}
