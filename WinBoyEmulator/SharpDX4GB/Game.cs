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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Third party
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.DXGI;

// Self-written
using log4Any;
using WinBoyEmulator.GameBoy;

// Aliasses
using D3D11 = SharpDX.Direct3D11;

namespace WinBoyEmulator.SharpDX4GB
{
    public class Game : IDisposable
    {
        private const int Width = GameBoy.Configuration.Screen.Width;
        private const int Height = GameBoy.Configuration.Screen.Height;

        private LogWriter _logWriter;
        private Emulator _emulator;

        /// <summary>Path of the game.</summary>
        public string GamePath
        {
            get
            {
                return _emulator.GamePath;
            }
            set
            {
                _emulator.GamePath = value;
            }
        }

        public Game() : this("WinBoyEmulator") { }

        public Game(string text)
        {
            _logWriter = new LogWriter( GetType() );
            _emulator = new Emulator();
        }

        /// <summary>Runs a game.</summary>
        public void Run()
        {
            if (string.IsNullOrEmpty(GamePath))
                throw new InvalidOperationException("Missing property GamePath");

            _emulator.StartEmulation();
        }

        /// <summary>Dispose objects.</summary>
        public void Dispose()
        {

        }
    }
}
