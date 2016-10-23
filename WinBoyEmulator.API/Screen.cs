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
        public byte[] _data;

        public Screen() : this(0, 0, new Color[0]) { }

        public Screen(int width, int height, Color[] colorPalette)
        {
            Width = width;
            Height = height;
            ColorPalette = colorPalette;

            _data = new byte[width * height * Configuration.ColorFormat.ByteCount()];

            for (var i = 0; i < _data.Length; i++)
                _data[i] = 0;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Color[] ColorPalette { get; set; }

        /// <summary>
        /// Byte array, a buffer, 
        /// that conains the data of the screen.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}
