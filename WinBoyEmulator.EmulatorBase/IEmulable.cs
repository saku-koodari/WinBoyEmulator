using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace for defining base of all GameBoy emulators.
namespace WinBoyEmulator.EmulatorBase
{
    /// <summary>
    /// Interface for implementing GameBoy console.
    /// </summary>
    /// <remarks>
    /// You should name a class that implements this as 'Console'.
    /// (Namespace should identify the console.)
    /// </remarks>
    public interface IEmulable
    {
        /// <summary>
        /// Gets or sets the console's width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the console's height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }

        /// <summary>Emulates the consoles's cpu.</summary>
        void EmulateCpu();
    }
}
