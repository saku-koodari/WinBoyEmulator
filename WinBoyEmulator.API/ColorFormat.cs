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

namespace WinBoyEmulator
{
    /// <summary>Extension class that extens enum ColorFormat</summary>
    public static class ColorFormatExtensions
    {
        public static int ByteCount(this ColorFormat colorFormat)
        {
            switch (colorFormat)
            {
                case ColorFormat.R8G8B8A8_UNorm:
                    // Each color + alpha channel (RGBA) contains 8 bits each. That takes 4 bytes.
                    return 4;
                default:
                    throw new ArgumentException("Only enum value 'R8G8B8A8_UNorm' can be used with ByteCount - method.", nameof(colorFormat));
            }
        }
    }

    /// <summary>Color format that is used in <see cref="WinBoyEmulator"/>.</summary>
    public enum ColorFormat
    {
        // The value is 28 because it is exactly the same value
        // than in SharpDX.DXGI.Format.R8G8B8A8_UNorm
        /// <summary>
        /// A four-component,
        /// 32-bit unsigned-normalized-integer format
        /// that supports 8 bits per channel including alpha.
        /// </summary>
        R8G8B8A8_UNorm = 28
    }
}
