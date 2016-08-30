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
