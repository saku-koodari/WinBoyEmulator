using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.Core;
using WinBoyEmulator.EmulatorBase;
using WinBoyEmulator.GameBoy;

namespace WinBoyEmulator.Emulation
{
    /// <summary>
    /// A factory lass (of <see cref="GameBoy"/>), 
    /// that emulates the selected GameBoyConsole.
    /// </summary>
    public class Emulator
    {
        private bool _isEmulatorRunning = false;
        private IEmulable _gameBoyConsole;

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

            InitializeGameBoyConsole();

            // TODO: Consider changing the name to Start.
            EmulateCycle();
        }

        /// <summary>Initializes <see cref="_gameBoyConsole"/> instance by <see cref="GameBoyConsole"/>.</summary>
        private void InitializeGameBoyConsole()
        {
            // Should you uncomment following two lines?
            // Or should they be on IDisposable.Dispose() - method?
            // _gameBoyConsole?.Dispose();
            // _gameBoyConsole = null; // sanity check

            // Because of this, 
            // you can't change the console on the fly.
            // (I mean it recreate instance of _gameBoyConsole
            // depending on GameBoyConsole).
            switch (GameBoyConsole)
            {
                case GameBoyConsole.GameBoy:
                    _gameBoyConsole = new GameBoy.Console();
                    break;
                case GameBoyConsole.GameBoyPocket:
                case GameBoyConsole.GameBoyLight:
                case GameBoyConsole.GameBoyColor:
                    //_gameBoyConsole = new GameBoyColor.Console();
                    // break;
                case GameBoyConsole.GameBoyAdvance:
                    //_gameBoyConsole = new GameBoyAdvance.Console();
                    //break;
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

            // Should this class be disposable?
            // Or how the method should differ apart from Pause(); ?
            // _gameBoyConsole?.Dispose();
            // _gameBoyConsole = null; // Sanity check

            throw new NotImplementedException();
        }

        public void EmulateCycle(int amountOfCycles = 1)
        {
            if (amountOfCycles < 0)
                throw new ArgumentOutOfRangeException(nameof(amountOfCycles), "argument must be zero or bigger.");

            if (!_isEmulatorRunning)
                return;

            for(var i = 0; i < amountOfCycles; i++)
                _gameBoyConsole.EmulateCpu();
        }
    }
}
