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
using SharpDX;
using SharpDX.Direct2D1;

namespace WinBoyEmulator.Rendering.Utils
{
    /// <summary>WinBoyEmulator.GameBoy extensions that needs SharpDX.</summary>
    public static class GameBoyExtensions
    {
        // This is in an own extension class instead of inside GameBoy.GPU.Screen,
        // because I don't want add any SharpDX references 
        // to WinBoyEmulator.GameBoy project.
        // (My design is to separate graphic library and GameBoy as much as possible.)

        /// <summary>Convert Screen's Data to SharpDX.Direct2D1.Bitmap</summary>
        public static Bitmap ToBitmap(this GameBoy.GPU.Screen screen, WindowRenderTarget windowRenderTarget)
        {
            var size = new Size2(screen.Width, screen.Height);
            var bitmapProperties = new BitmapProperties {  PixelFormat = new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Ignore) };
            var bitmap = new Bitmap(windowRenderTarget, size, bitmapProperties);
            bitmap.CopyFromMemory(screen.Data, screen.Width * GameBoy.GPU.Screen.BufferSize);
            return bitmap;
        }
    }
}
