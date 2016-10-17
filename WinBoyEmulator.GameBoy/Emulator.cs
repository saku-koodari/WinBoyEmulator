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

namespace WinBoyEmulator.GameBoy
{
    /// <summary>An interface between a From and Game Boy Emulator.</summary>
    public class Emulator
    {
        private Screen _screen;
        private string _gamePath;

        public Emulator(int width, int height, Color[] colorPalette)
        {
            _screen = new Screen(width, height, colorPalette);
        }

        #region Screen Properties
        public Screen Screen => _screen;

        /// <summary>Width of the game area.</summary>
        public int Width => _screen.Width;

        /// <summary>Height of the game area.</summary>
        public int Height => _screen.Height;

        /// <summary>Contains color GameBoy is using.</summary>
        public Color[] ColorPalette => _screen.ColorPalette;
        #endregion

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

        private void EmulateCpu()
        {
            // throw new NotImplementedException("kräks");
        }

        private void EmulateSound()
        {
           // Console.WriteLine("Sound: bii bii bi bi...");
        }

        private void UpdateScreen()
        {

        }

        /// <summary>Load game to the memory.</summary>
        /// <param name="gamePath">
        /// use this, if you don't want use <see cref="GamePath"/> property. 
        /// (If used, it will override value of <see cref="GamePath"/>)
        /// </param>
        public void LoadGameToMemory(string gamePath = null)
        {

        }

        /// <summary>Emulate one Cycle</summary>
        public void EmulateCycle()
        {
            EmulateCpu();
            EmulateSound();
            UpdateScreen();
        }
    }
}
