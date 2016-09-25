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
using SharpDX.Direct2D1;
using SharpDX.DXGI;

using Direct2D1Factory = SharpDX.Direct2D1.Factory;
using DirectWriteFactory = SharpDX.DirectWrite.Factory;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;

namespace WinBoyEmulator.Rendering.App
{
    public class Direct2DApp : Direct3DApp
    {
        public Direct2D1Factory Factory2D { get; private set; }

        public DirectWriteFactory FactoryDirectWrite { get; private set; }

        public WindowRenderTarget RenderTarget2D { get; private set; }

        public SolidColorBrush SceneColorBrush { get; private set; }

        protected override void Initialize()
        {
            Factory2D = new Direct2D1Factory();
            FactoryDirectWrite = new DirectWriteFactory();

            var properties = new HwndRenderTargetProperties
            {
                Hwnd = DisplayHandle,
                PixelSize = new Size2(Configuration.Instance.Width, Configuration.Instance.Height),
                PresentOptions = PresentOptions.None
            };

            var renderTatgetProperties = new RenderTargetProperties
            {
                PixelFormat = new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)
            };

            RenderTarget2D = new WindowRenderTarget(Factory2D, renderTatgetProperties, properties)
            {
                AntialiasMode = AntialiasMode.PerPrimitive
            };

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, Color.White);

            base.Initialize();
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            RenderTarget2D.BeginDraw();
        }

        protected override void EndDraw()
        {
            RenderTarget2D.EndDraw();
            base.EndDraw();
        }
    }
}
