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

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Device = SharpDX.Direct3D11.Device;

namespace WinBoyEmulator.Rendering.App
{
    public class Direct3DApp : BaseClass
    {
        private Device _device;
        private SwapChain _swapChain;
        private Texture2D _backBuffer;
        private RenderTargetView _backBufferView;

        /// <summary>Gets the device.</summary>
        public Device Device => _device;

        /// <summary>Gets the back Buffer</summary>
        public Texture2D BackBuffer => _backBuffer;

        protected override void Initialize()
        {
            var modeDescription = new ModeDescription(
                width: Configuration.Instance.Width,
                height: Configuration.Instance.Height,
                refreshRate: new Rational(Configuration.Instance.FPS, 1),
                format: Format.R8G8B8A8_UNorm
            );

            // TODO: Should this values makes configurable?
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 1,
                ModeDescription = modeDescription,
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                // TODO: Document why Discard is used¿
                SwapEffect = SwapEffect.Discard,
                // TODO: Document below?
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(
                // If you need to debug, 
                // you could set DriverType to Reference.
                DriverType.Hardware,
                DeviceCreationFlags.BgraSupport,
                new[] { FeatureLevel.Level_10_0 },
                swapChainDescription,
                out _device,
                out _swapChain);

            // Ignore all window events
            _swapChain.GetParent<Factory>()
                .MakeWindowAssociation(DisplayHandle, 
                // TODO: Should this be configurable?
                WindowAssociationFlags.IgnoreAll);

            // TODO: If you have more than one buffer, you need to handle this' 2nd. argument (0).
            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            _backBufferView = new RenderTargetView(_device, _backBuffer);
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();

            var viewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = Configuration.Instance.Width,
                Height = Configuration.Instance.Height
            };

            Device.ImmediateContext.Rasterizer
                .SetViewport(viewport);
            Device.ImmediateContext.OutputMerger
                .SetTargets(_backBufferView);

        }

        protected override void EndDraw()
        {
            //base.EndDraw();

            var syncInterval = Configuration.Instance.WaitVerticalBlanking ? 1 : 0;

            _swapChain.Present(syncInterval, PresentFlags.None);
        }
    }
}
