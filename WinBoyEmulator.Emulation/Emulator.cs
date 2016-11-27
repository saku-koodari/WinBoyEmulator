using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.Core;
using WinBoyEmulator.GameBoyConsoles;

namespace WinBoyEmulator.Emulation
{
    /// <summary>
    /// Class that emulates the selected GameBoyConsole.
    /// </summary>
    public class Emulator
    {
        private bool _isEmulatorRunning = false;
        private IEmulatable _gameBoyConsole;

        /// <summary>
        /// Initializes a new instance of the <see cref="Emulator"/> class.
        /// </summary>
        public Emulator()
            // A regulat GameBoy is the default console.
            : this(GameBoyConsole.GameBoy)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Emulator"/> class.
        /// </summary>
        /// <param name="gameBoyConsole">The game boy console.</param>
        public Emulator(GameBoyConsole gameBoyConsole)
        {
            GameBoyConsole = gameBoyConsole;
        }

        /// <summary>
        /// Gets or sets the Game Boy console.
        /// </summary>
        /// <value>
        /// The game boy console.
        /// </value>
        public GameBoyConsole GameBoyConsole { get; set; }

        public byte[] Data { get; set; }

        /// <summary>Starts to emulate selected Game Boy console.</summary>
        public void Start()
        {
            _isEmulatorRunning = true;

            // Because of this, you can't change the console on the fly.
            switch (GameBoyConsole)
            {
                case GameBoyConsole.GameBoy:
                    _gameBoyConsole = new GameBoy();
                    break;
                case GameBoyConsole.GameBoyPocket:
                case GameBoyConsole.GameBoyLight:
                case GameBoyConsole.GameBoyColor:
                case GameBoyConsole.GameBoyAdvance:
                case GameBoyConsole.GameBoyAdvanceSP:
                case GameBoyConsole.GameBoyMicro:
                    throw new NotImplementedException();
                case GameBoyConsole.Unknown:
                default:
                    throw new InvalidOperationException($"Unsupported {nameof(GameBoyConsole)}.");

            }
        }

        /// <summary>Pauses the emulation.</summary>
        public void Pause()
        {
            _isEmulatorRunning = false;
        }

        /// <summary>Stops the emulation.</summary>
        public void Stop()
        {
            _isEmulatorRunning = false;
            throw new NotImplementedException();
        }

        public void EmulateCycle(int amountOfCycles = 1)
        {
            if (!_isEmulatorRunning)
                return;
        }
    }
}
