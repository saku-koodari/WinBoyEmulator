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

using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;

using D3D11 = SharpDX.Direct3D11;

namespace WinBoyEmulator.SharpDX
{
    public class Game : IDisposable
    {
        public const int FPS = 120;
        public const int Width = GameBoy.Configuration.Screen.Width;
        public const int Height = GameBoy.Configuration.Screen.Height;

        private RenderForm _renderForm;
        private ModeDescription _backBufferDescription;
        private SwapChainDescription _swapChainDescription;
        private D3D11.Device _d3dDevice;
        private D3D11.DeviceContext _d3dDeviceContext;
        private SwapChain _swapChain;

        public string GamePath { get; set; }

        public Game() : this("WinBoyEmulator") { }

        public Game(string text)
        {
            _renderForm = new RenderForm(text);
            _renderForm.ClientSize = new Size(Width, Height);
            // You may need to modify rendering if you set this true.
            _renderForm.AllowUserResizing = false;

            _backBufferDescription = new ModeDescription(Width, Height, new Rational(FPS, 1), Format.R8G8B8A8_UNorm);
            _swapChainDescription = new SwapChainDescription
            {
                ModeDescription = _backBufferDescription,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = _renderForm.Handle,
                IsWindowed = true
            };

            var hw = DriverType.Hardware;
            var flags = D3D11.DeviceCreationFlags.None;

            D3D11.Device.CreateWithSwapChain(hw, flags, _swapChainDescription, out _d3dDevice, out _swapChain);
            _d3dDeviceContext = _d3dDevice.ImmediateContext;
        }

        private void _initializeDeviceResources()
        {

        }

        private void _renderCallBack()
        {

        }

        /// <summary>Runs a game.</summary>
        public void Run() => RenderLoop.Run(_renderForm, _renderCallBack);

        /// <summary>Dispose objects.</summary>
        public void Dispose() => _renderForm.Dispose();
    }
}
