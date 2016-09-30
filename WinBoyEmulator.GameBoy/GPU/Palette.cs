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
    /// <summary>
    /// Class Palette.
    /// </summary>
    internal class Palette
    {
        public int[] Background { get; set; }
        public int[] Object1 { get; set; }
        public int[] Object2 { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="colorsInPalette">Amount of colors in the palette.</param>
        public Palette(int colorsInPalette)
        {
            Background = new int[colorsInPalette];
            Object1 = new int[colorsInPalette];
            Object2 = new int[colorsInPalette];
        }
    }
}
