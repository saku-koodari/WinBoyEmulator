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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator
{
    /// <summary>
    /// A class that holds the data of the screen.
    /// </summary>
    public class Screen
    {
        public Screen() : this(0, 0, null) { }

        public Screen(int width, int height, Color[] colorPalette)
        {
            Width = width;
            Height = height;
            ColorPalette = colorPalette;

            Data = new byte[width * height * Configuration.ColorFormat.ByteCount()];

            for (var i = 0; i < Data.Length; i++)
                Data[i] = 0;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Color[] ColorPalette { get; set; }

        /// <summary>
        /// Byte array, a buffer, 
        /// that conains the data of the screen.
        /// </summary>
        public byte[] Data { get; set; }

        public Color[] DataAsColor()
        {
            var l = Width * Height;
            var dl = Data.Length / 4; ;
            
            var data = new Color[Width * Height];
            var i = 0;
            var j = 0;

            while(i < Data.Length)
            {
                var red = Data[i++];
                var green = Data[i++];
                var blue = Data[i++];
                var alpha = Data[i++];

                data[j++] = Color.FromArgb(alpha, red, green, blue);
            }

            return data;
        }
    }
}
