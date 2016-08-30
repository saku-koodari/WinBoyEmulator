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
using System.Linq;
using System.Text;
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

        public static Emulator Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_instance)
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
            // private empty constructor.
            // Just to prevent object creation outside this class.
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
            MMU.Instance.Load(gamePath);
        }
             
    }
}
