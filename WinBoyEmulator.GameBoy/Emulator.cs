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

using WinBoyEmulator.GameBoy.CPU;
using WinBoyEmulator.GameBoy.GPU;
using WinBoyEmulator.GameBoy.Memory;

using MMU = WinBoyEmulator.GameBoy.Memory.Memory;
using Screen = WinBoyEmulator.GameBoy.GPU.Screen;

namespace WinBoyEmulator.GameBoy
{
    /// <summary>An interface between a From and Game Boy Emulator.</summary>
    public class Emulator
    {
        // TODO: Release memory of GPU.Screen.Data (it's IDisposable)
        // TODO2: It would be better, instances here (instead of `singleton`.Instance.*)

        private Toolbox _toolbox;
        private Screen _screen;
        private byte[] _game;
        private string _gamePath;

        /// <summary>Width of the game area.</summary>
        public static int Width => Configuration.Screen.Width;

        /// <summary>Height of the game area.</summary>
        public static int Height => Configuration.Screen.Height;

        /// <summary>Contains color GameBoy is using.</summary>
        public static Color[] ColorPalette => Configuration.Colors.Palette;

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

        /// <summary>
        /// Screen object.
        /// </summary>
        public Screen Screen => _screen = _toolbox.RandomizeScreen();

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

        /// <summary>Emulate one Cycle</summary>
        public void EmulateCycle()
        {
            // Emulate one cycle
            //LR35902.Instance.EmulateCycle();

            // If the draw flag is set, update the screen
            // update sound (Issue #20)
            // Store key press state (Press and Release)
        }

        /// <summary>Load game to the memory.</summary>
        /// <param name="gamePath">
        /// use this, if you don't want use GamePath property. 
        /// (If used, it will override value of GamePath)
        /// </param>
        public void LoadGameToMemory(string gamePath = null)
        {
            if ( !string.IsNullOrEmpty(gamePath) )
                _gamePath = gamePath;

            if (string.IsNullOrEmpty(_gamePath))
                throw new InvalidOperationException($"Property '{nameof(GamePath)}' or argument {nameof(gamePath)} is either null or empty");
            
            // Load the game.
            _readGameFile(_gamePath);

            // Load the game to the memory
            MMU.Instance.Load(_game);
        }     
    }
}
