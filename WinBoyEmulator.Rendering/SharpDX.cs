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
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using Format = SharpDX.DXGI.Format;
using Color = System.Drawing.Color;

using WinBoyEmulator;
using WinBoyEmulator.Rendering.Extensions;
using WinBoyEmulator.Rendering.Utils;

namespace WinBoyEmulator.Rendering
{
    /// <summary>Class for showing an emulator on a form. Disposable.</summary>
    public class SharpDX : IDisposable, IVideoRenderer
    {
        private static Format _format;
        private static int _numberOfBytes;
        private Log4Any.LogWriter _logWriter;
        private Form _form;
        private Factory _factory;
        private WindowRenderTarget _windowRenderTarget;
        private RawRectangleF _drawRectangle;
        private Bitmap _bitmap;

        /// <summary>
        /// Uses format <see cref="Format.R8G8B8A8_UNorm"/> and alpha is ignored.<para/>
        /// (Used <see cref="Format.B8G8R8A8_UNorm" /> previously
        /// </summary>
        private PixelFormat NewPixelFormat => new PixelFormat(_format, AlphaMode.Ignore);

        // TODO: This is never assigned. Use it somehow.
        private bool _isFormClosed;

        static SharpDX()
        {
            _numberOfBytes = Configuration.ColorFormat.ByteCount();
            _format = Configuration.ColorFormat.ToSharpDXGIFormat();
        }

        /// <summary>
        /// Constructor of <see cref="Render"/>.
        /// Creates singlethreaded video renderer with SharpDX which uses information as debug level.
        /// Doesn't initialize buffer. <para />
        /// Class uses interface <see cref="IDisposable"/>
        /// </summary>
        public SharpDX() : this(ThreadType.SingleThread, DebugLevel.Information) { }

        /// <summary>
        /// Constructor of <see cref="Render"/>.
        /// Doesn't initialize buffer. <para />
        /// Class uses interface <see cref="IDisposable"/>
        /// </summary>
        public SharpDX(ThreadType threadType, DebugLevel debugLevel)
        {
            _logWriter = new Log4Any.LogWriter(GetType());
            _factory = new Factory(threadType.ToFactoryType(), debugLevel.ToSharpDXDebugLevel());
        }

        public Screen Screen { get; set; }

        public Action Loop { get; set; }

        private Size2 NewSize => new Size2(Screen.Width, Screen.Height);

        private void CreateRenderTargets()
        {
            _windowRenderTarget = new WindowRenderTarget(_factory, new RenderTargetProperties {
                PixelFormat = NewPixelFormat,
                Type = RenderTargetType.Default,
                MinLevel = FeatureLevel.Level_DEFAULT
            }, new HwndRenderTargetProperties { Hwnd = _form.Handle, PixelSize = NewSize, PresentOptions = PresentOptions.Immediately})
            {
                DotsPerInch = new Size2F(96.0f, 96.0f),
                AntialiasMode = AntialiasMode.Aliased,
            };

            // CreateRenderTargets
            var _screenRenderTarget = new BitmapRenderTarget(_windowRenderTarget, 
                CompatibleRenderTargetOptions.None, new Size2F(Screen.Width, Screen.Height), NewSize, null);
            
            // RecalculateDrawRectangle
            _drawRectangle = new RawRectangleF(0, 0, _form.ClientSize.Width, _form.ClientSize.Height);
        }

        private void CreateBitmap()
        {
            _bitmap = new Bitmap(_windowRenderTarget, NewSize, new BitmapProperties { PixelFormat = NewPixelFormat });
            _bitmap.CopyFromMemory(Screen.Data);
        }

        private void SizeChanged(object sender, EventArgs e)
        {
            // If you uncomment next line, screen won't be sctretched if window size changes.
            // _windowRenderTarget?.Resize(NewClientSize);
        }

        /// <summary>
        /// Updates buffer.
        /// </summary>
        /// <param name="screen">
        ///     You can use with this argument or without
        ///     (the you must use Property <see cref="Screen"/>).
        /// </param>
        public void Update(Screen screen = null)
        {
            if (screen != null)
                Screen = screen;

            // Copy gameboy screen's data to bitmap
            _bitmap.CopyFromMemory(Screen.Data, Screen.Width * _numberOfBytes);
        }

        /// <summary>
        /// Draws the bitmap to the target form.
        /// </summary>
        public void Draw()
        {
            // Draw bitmap
            _windowRenderTarget.BeginDraw();
            _windowRenderTarget.DrawBitmap(_bitmap, _drawRectangle, 1.0f, BitmapInterpolationMode.NearestNeighbor);
            _windowRenderTarget.EndDraw();
        }

        /// <summary>
        /// Runs the game. This method handles form closing.
        /// // TODO: Add property LoopAction add add possibility to all this method without arguments.
        /// </summary>
        /// <param name="targetForm">the form where graphics will drawn.</param>
        /// <param name="loopAction">Add your game logic here.</param>
        public void Run(Form targetForm)
        {
            if (targetForm == null)
                throw new ArgumentNullException(nameof(targetForm));

            if (Loop == null)
                throw new InvalidOperationException($"Property '{nameof(Loop)}' is null. Have you initialized it?");

            // Before run
            _form = targetForm;
            _form.SizeChanged += SizeChanged;

            CreateRenderTargets();
            CreateBitmap();

            // Run
            RenderLoop.Run(targetForm, () =>
            {
                // Check wheter is it time to finish.
                // a.k.a. bool form close flag is set to true.
                if (_isFormClosed)
                    return;

                Loop();

                // TODO: Check whether we need to close form or not.
            });

            // After run
            _form.SizeChanged -= SizeChanged;
            #region Dispose();
            // Dispose is not necessary needed here, since
            // this class uses interface IDisposable (user can dispose when (s)he wants).
            // Dispose();
            #endregion
        }

        /// <summary>Dispose objects used by Renderer.</summary>
        public void Dispose()
        {
            _bitmap?.Dispose();
            _windowRenderTarget?.Dispose();
        }
    }
}
