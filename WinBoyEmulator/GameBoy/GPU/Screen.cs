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

namespace WinBoyEmulator.GameBoy.GPU
{
    public class Screen
    {
        private int[,] _data;

        /// <summary>Width of the Screen</summary>
        public static int Width => Configuration.Screen.Width;
        /// <summary>Height of the Screen</summary>
        public static int Height => Configuration.Screen.Height;
        /// <summary>Amount of colors in palette</summary>
        public static int ColorsInPalette => Configuration.Colors.Palette.Length;

        /// <summary>The actual data of the screen.</summary>
        public int[,] Data
        {
            get
            {
                return _data;
            }
            set
            {
                foreach(var i in value)
                {
                    if (i > ColorsInPalette)
                        throw new ArgumentOutOfRangeException(nameof(value), $"value must be between 0 and {ColorsInPalette}");
                }

                _data = value;
            }
        }

        /// <summary>
        /// Constructor without parameters. Initializes screen default values, which are: <para/>
        /// Width = Configuration.Screen.Width; <para />
        /// Height = Configuration.Screen.Height; <para />
        /// Data = new int[Width * Height * Configuration.Colors.Palette.Length];
        /// </summary>
        public Screen()
        {
            Data = new int[Width, Height];
        }
    }
}
