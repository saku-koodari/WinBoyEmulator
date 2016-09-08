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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinBoyEmulator.GameBoy.CPU;
using WinBoyEmulator.GameBoy.GPU;
using WinBoyEmulator.GameBoy.Memory;

using Timer = System.Timers.Timer;
using MMU = WinBoyEmulator.GameBoy.Memory.Memory;
using Screen = WinBoyEmulator.GameBoy.GPU.Screen;
using static WinBoyEmulator.GameBoy.WinBoyEvents;

namespace WinBoyEmulator.GameBoy
{
    /// <summary>An interface between a From and Game Boy Emulator.</summary>
    public class Emulator : IEmulator
    {
        private Timer _timer;
        private Toolbox _toolbox;
        private Screen _screen;
        private byte[] _game;
        private string _gamePath;

        public event DrawEventHandler DrawEventHandler;

        public string GamePath
        {
            get
            {
                return _gamePath;
            }
            set
            {
                _gamePath = value;
            }
        }

        public  Emulator()
        {
            _game = new byte[0x200];
            _toolbox = new Toolbox();
        }

        private void _readGameFile(string filename)
        {
            using (var reader = new BinaryReader(File.OpenRead(filename)))
            {
                var length = (int)reader.BaseStream.Length;
                _game = reader.ReadBytes(length);
                // Issue #29
            }
        }

        private void _render()
        {
            DrawEventHandler(_screen, new EventArgs());
        }

        private void _gameCycle(object sender, ElapsedEventArgs e)
        {
            // Emulate one cycle
            //LR35902.Instance.EmulateCycle();

            // If the draw flag is set, update the screen
            // update sound (Issue #20)
            // Store key press state (Press and Release)

            _screen = _toolbox.RandomizeScreen();

            _render();
        }

        /// <summary>Starts emulation with game inside.</summary>
        public void StartEmulation()
        {
            if (_timer != null)
                throw new InvalidOperationException($"Trying to access an object:{nameof(_timer)} even though it has been initialized already.");

            // Load game.
            _readGameFile(_gamePath);
            MMU.Instance.Load(_game);

            _timer = new Timer
            {
                Enabled = true,
                Interval = Configuration.Clock.Timer_Interval
            };

            // method tha Elapsed will get, is the main loop
            _timer.Elapsed += _gameCycle;
        }     

        public void StopEmulation()
        {
            _timer.Dispose();
            throw new NotImplementedException("Issue #46");
        }
    }
}
