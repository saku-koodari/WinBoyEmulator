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
//     along with Foobar.  If not, see<http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.GPU
{
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Amount of pixels in a row in the screen.</param>
        /// <param name="height">Amount of pixels in a column in the screen.</param>
        /// <param name="colorsInPalette">amount of colors in palette</param>
        public Screen(int width, int height, int colorsInPalette)
        {
            Width = width;
            Height = Height;
            Data = new int[width * height * colorsInPalette];
        }
    }
}
