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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DXGI;
//using SharpDX.Direct2D1;
using WinBoyEmulator.GameBoy;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Rectangle = System.Drawing.Rectangle;

namespace WinBoyEmulator.Rendering.Utils
{
    /// <summary>WinBoyEmulator.GameBoy extensions that needs SharpDX.</summary>
    public static class GameBoyExtensions
    {
        // I am making this extensions class,
        // because I don't want add any SharpDX references 
        // to WinBoyEmulator.GameBoy project.
        // (My design is to separate graphic library and GameBoy as much as possible.)

        /// <summary>Convert Screen's Data to SharpDX.Direct2D1.Bitmap</summary>
        public static DataStream ToDataStream(this GameBoy.GPU.Screen screen)
        {
            var stride = screen.Width * sizeof(int);
            // TODO:    Find out whether is work, if canRead and/or canWrite can be set to false
            //          preferable both of them should be false. 
            //          (A quick common sense says for me, that both of them can be false. 
            //          Since we won't read nor write from/to disk...)
            using (var stream = new DataStream(screen.Height * stride, canRead: true, canWrite: true))
            {


                return stream;
            }
        }
    }
}
