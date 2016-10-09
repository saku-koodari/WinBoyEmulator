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
using System.Threading.Tasks;

namespace WinBoyEmulator.GameBoy.GPU
{
    public class Screen
    {
        /// <summary>
        /// Constructor without parameters. Initializes screen with default values, which are: <para/>
        /// Width = Configuration.Screen.Width; <para />
        /// Height = Configuration.Screen.Height; <para />
        /// ColorCount = Configuration.Screen.Palette.Length; <para />
        /// Data = new int[Width * Height * ColorCount];
        /// </summary>
        public Screen() 
            : this(Configuration.Screen.Width, Configuration.Screen.Height, Configuration.Colors.Palette.Length)
        {

        }

        /// <summary>
        /// Constructor, with three parameters.
        /// </summary>
        /// <param name="width">Amount of pixels in a row in the screen.</param>
        /// <param name="height">Amount of pixels in a column in the screen.</param>
        /// <param name="colorCount">Amount of colors in the palette.</param>
        public Screen(int width, int height, int colorCount)
        {
            var dataSize = width * height * colorCount;
            Width = width;
            Height = Height;
            ColorCount = colorCount;

            //if (dataSize > int.MaxValue)
            //    throw new OutOfMemoryException("Data.Length can't be bigger than int.MaxValue.");

            // Data can't be bigger than 2GB due to CLR limitations
            // https://stackoverflow.com/questions/3944320/maximum-length-of-byte
            // https://stackoverflow.com/questions/4815461/outofmemoryexception-on-declaration-of-large-array
            Data = new byte[dataSize];

            // Fast way to fill 'Data' with zeros.
            Parallel.For(0, Data.Length, i => Data[i] = 0);
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int ColorCount { get; set; }

        // RGB's value range:  #000000   (0) - #FFFFFF   (16777215)
        // RGBA's value range: #00000000 (0) - #FFFFFFFF (4294967295)
        /// <summary>
        /// Flatten screen, where values represent rgb hexadecimal value of the color.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
