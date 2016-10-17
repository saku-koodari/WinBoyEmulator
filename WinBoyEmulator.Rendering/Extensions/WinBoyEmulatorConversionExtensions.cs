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

using SharpDX.Direct2D1;
using Format = SharpDX.DXGI.Format;

using WBEDL = WinBoyEmulator.DebugLevel;
using SDXDL = SharpDX.Direct2D1.DebugLevel;

namespace WinBoyEmulator.Rendering.Extensions
{
    /// <summary>
    /// An extension class for converting WinBoyEmulator (project) classes
    /// to SharpDX compatible
    /// </summary>
    public static class WinBoyEmulatorConversionExtensions
    {
        /// <summary>
        /// Convert <see cref="ThreadType"/> to <see cref="FactoryType"/>.
        /// </summary>
        /// <param name="threadType"></param>
        /// <returns><see cref="FactoryType"/></returns>
        public static FactoryType ToFactoryType(this ThreadType threadType)
        {
            var i = (int)threadType;
            return (FactoryType)i;
        }

        /// <summary>
        /// Convert <see cref="ColorFormat"/> to <see cref="Format"/>.
        /// </summary>
        /// <param name="colorFormat"></param>
        /// <returns><see cref="Format"/></returns>
        public static Format ToSharpDXGIFormat(this ColorFormat colorFormat)
        {
            var i = (int)colorFormat;
            return (Format)i;
        }

        /// <summary>
        /// Convert <see cref="WBEDL"/> to <see cref="SDXDL"/>.
        /// </summary>
        /// <param name="debugLevel"></param>
        /// <returns><see cref="SDXDL"/></returns>
        public static SDXDL ToSharpDXDebugLevel(this WBEDL debugLevel)
        {
            var i = (int)debugLevel;
            return (SDXDL)i;
        }
    }
}
