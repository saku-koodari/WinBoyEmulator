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

namespace WinBoyEmulator.Rendering
{
    /// <summary>
    /// TODO: Verify whether this' design has done properly
    /// </summary>
    internal class Configuration
    {
        private static Configuration _instance;

        static Configuration()
        {
            // Initializes instance ASAP.
            _instance = new Configuration();
        }

        /// <summary>Instance of the class Configuration.</summary>
        public static Configuration Instance => _instance;

        private Configuration() { }

        /// <summary>Title of the window.</summary>
        public string Title { get; set; }

        /// <summary>Frames per second</summary>
        public int FPS { get; set; }

        /// <summary>Height of the window</summary>
        public int Width { get; set; }

        /// <summary>Height of the window</summary>
        public int Height { get; set; }

        /// <summary>Wait vertical blanking</summary>
        public bool WaitVerticalBlanking { get; set; }

        /// <summary>Color palette</summary>
        public Color[] ColorPalette { get; set; }
    }
}
