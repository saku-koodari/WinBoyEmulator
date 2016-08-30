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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WinBoyEmulator.GameBoy.CPU;
using WinBoyEmulator.GameBoy.GPU;
using WinBoyEmulator.GameBoy.Memory;

using MMU = WinBoyEmulator.GameBoy.Memory.Memory;

namespace WinBoyEmulator.GameBoy
{
    public class Emulator : IEmulator
    {
        private static readonly object _syncRoot = new object();
        private static volatile Emulator _instance;

        private byte[] _game;
        private bool _isGameBoyOn = false;

        public static Emulator Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_syncRoot)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new Emulator();
                }

                return _instance;
            }
        }

        private Emulator()
        {
            _game = new byte[0x200];
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

        private void _gameCycle()
        {
            while(_isGameBoyOn)
            {
                // Emulate one cycle
                LR35902.Instance.EmulateCycle();

                // If the draw flag is set, update the screen
                // Store key press state (Press and Release)
            }
        }

        /// <summary>Starts emulation without game inside.</summary>
        public void StartEmulation() => StartEmulation(string.Empty);

        /// <summary>
        /// Starts emulation with game inside.
        /// </summary>
        /// <param name="gamePath">path of the game. File type must be .gb</param>
        public void StartEmulation(string gamePath)
        {
            // Load game.
            _readGameFile(gamePath);
            MMU.Instance.Load(_game);

            var thread = new Thread(_gameCycle)
            {
                IsBackground = true,
                Name  = "WinBoyEmulator",
            };
            thread.Start();

        }     
    }
}
