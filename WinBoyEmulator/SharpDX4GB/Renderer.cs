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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// SharpDX library
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

// Self written
using log4Any;
using WinBoyEmulator.Extensions;
using Configuration = WinBoyEmulator.GameBoy.Configuration;

// Aliasses
using Format = SharpDX.DXGI.Format;

namespace WinBoyEmulator.SharpDX4GB
{
    public sealed class Renderer : IDisposable
    {
        #region Constants
        private const int Width = Configuration.Screen.Width;
        private const int Height = Configuration.Screen.Height;
        private static int ColorCount = Configuration.Colors.Palette.Length;
        // TODO: Change values from GameBoy.Configuration.Screen.Width and see what will happen.
        // (I am not sure, but this might brake due to that...)
        // Don't ask why. There is no reason :D
        // (Actually there is: CrystalBoy.Emulator.Rendering.SharpDX.Direct2DRenderer.cs line 45.)
        private const int BorderWidth = Width + 96;
        private const int BorderHeight = Height + 80;
        private const float DotWidth = 96.0f;
        private const float DotHeight = 96.0f;
        #endregion

        private LogWriter _logWriter;
        private Factory _factory;
        private WindowRenderTarget _windowRenderTarget;
        private BitmapRenderTarget _compositeRenderTarget;
        private BitmapRenderTarget _screenRenderTarget;
        private Bitmap _borderBitmap;
        private Bitmap _screenBitmap1;
        private Bitmap _screenBitmap2;
        private RawColor4 _clearColor;
        private RawRectangleF _drawRectangle;
        private readonly byte[] _borderBuffer;
        private readonly byte[] _screenBuffer;

        private Control _renderControl;
        private SynchronizationContext _synchronizationContext;

        #region public bool BorderVisible { get; set; }
        private volatile bool _borderVisible;
        public bool BorderVisible
        {
            get
            {
                return _borderVisible;
            }
            set
            {
                if(_borderVisible != (_borderVisible = value))
                {
                    OnBorderVisibilityChanged();
                }
            }
        }
        private void OnBorderVisibilityChanged()
        {

        }
        #endregion

        public Renderer(Control renderControl, int width, int height, int colorCount)
        {
            #region Base
            _logWriter = new LogWriter( GetType() );

            if (renderControl == null)
                throw new ArgumentNullException(nameof(renderControl));

            _renderControl = renderControl;

            if (renderControl.InvokeRequired)
            {
                var func = (Func<SynchronizationContext>)(() => SynchronizationContext.Current);

                _synchronizationContext = (SynchronizationContext)renderControl.Invoke(func);
            }
            else
            {
                _synchronizationContext = SynchronizationContext.Current;
            }
            #endregion

            _borderBuffer = new byte[BorderWidth * BorderHeight * ColorCount]; 
            _screenBuffer = new byte[Width * Height * ColorCount];

            // RGBA value, pure black
            _clearColor = new RawColor4(0, 0, 0, 0);
            _factory = new Factory(FactoryType.SingleThreaded, DebugLevel.Information);
            ResetRendering();
            _renderControl.FindForm().SizeChanged += OnSizeChanged;
        }

        private void CreateBitmapsAndRenderingTargets()
        {
            // Bitmap creation
            var borderSize = new Size2(BorderWidth, BorderHeight);
            var screenSize = new Size2(Width, Height);
            var borderBitmapProperties = new BitmapProperties { PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied) };
            var screenBitmapProperties = new BitmapProperties { PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Ignore) };

            // TODO: Is it right to use color count?
            _borderBitmap = new Bitmap(_windowRenderTarget, borderSize, borderBitmapProperties);
            _borderBitmap.CopyFromMemory(_borderBuffer, BorderWidth * ColorCount);
            _screenBitmap1 = new Bitmap(_windowRenderTarget, screenSize, screenBitmapProperties);
            _screenBitmap1.CopyFromMemory(_screenBuffer, Width * ColorCount);
            _screenBitmap2 = new Bitmap(_windowRenderTarget, screenSize, screenBitmapProperties);
            _screenBitmap2.CopyFromBitmap(_screenBitmap1);

            // RenderingTargetCreation
            _windowRenderTarget.Create(_factory, _renderControl);
            _windowRenderTarget.DotsPerInch = new Size2F(DotWidth, DotHeight);
            _windowRenderTarget.AntialiasMode = AntialiasMode.Aliased;

            if (BorderVisible)
                CreateCompositeRenderTarget();

            var size2f = new Size2F(Width, Height);

            _screenRenderTarget = new BitmapRenderTarget(_windowRenderTarget, CompatibleRenderTargetOptions.None, size2f, screenSize, null);

            RecalculateDrawRectangle();
        }

        private void RecalculateDrawRectangle()
        {
            throw new NotImplementedException();
        }

        private void CreateCompositeRenderTarget()
        {
            throw new NotImplementedException();
        }

        private void ResetRendering()
        {
            DisposeBitmapsAndRenderingTargets();
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DisposeBitmapsAndRenderingTargets()
        {
            _borderBitmap.DisposeIfNotNull();
            _screenBitmap1.DisposeIfNotNull();
            _screenBitmap2.DisposeIfNotNull();
            _compositeRenderTarget.DisposeIfNotNull();
            _windowRenderTarget.DisposeIfNotNull();
        }

        public void Dispose()
        {
            DisposeBitmapsAndRenderingTargets();

            _factory.DisposeIfNotNull();
            _renderControl.FindForm().SizeChanged -= OnSizeChanged;
        }
    }
}
