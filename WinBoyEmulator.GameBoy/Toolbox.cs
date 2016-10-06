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

using WinBoyEmulator.GameBoy.GPU;

namespace WinBoyEmulator.GameBoy
{
    internal class Toolbox
    {
        private Screen _screen;
        private Random _random;

        public Screen Screen
        {
            get
            {
                return _screen;
            }
            set
            {
                _screen = value;
            }
        }

        public Toolbox() : this(new Screen())
        {

        }

        public Toolbox(Screen screen)
        {
            _screen = screen;
            _random = new Random();
        }

        /// <summary>Fills Screen with random color values.</summary>
        public Screen RandomizeScreen()
        {
            _random.NextBytes(_screen.Data);

            for(var i = 3; i < _screen.Data.Length; i+= 4)
                _screen.Data[i] = 0xFF;

            return _screen;
        }
    }
}
