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

using SharpDX.Direct2D1;
using WinBoyEmulator.Rendering.App;
using WinBoyEmulator.Rendering.Utils;

namespace WinBoyEmulator.Rendering
{
    /// <summary>Class Renderer. SharpDX class for showing emulator on a form.</summary>
    public class Renderer : Direct2DApp
    {
        private Bitmap _bitmap;
        private GameBoy.Emulator _gameBoy;

        /// <summary>Initializes Configuration values ASAP.</summary>
        static Renderer()
        {
            Configuration.Instance.Title = "WinBoyEmulator";

            // Actually I think is Frames per seconds is just an initial values
            // and the Game's fps could be anything. 
            // Mayber that 60 is minimum fps?
            Configuration.Instance.FPS = 60;
            Configuration.Instance.Width = GameBoy.Emulator.Width;
            Configuration.Instance.Height = GameBoy.Emulator.Height;
            Configuration.Instance.ColorPalette = GameBoy.Emulator.ColorPalette;
        }

        public Renderer()
        {
            _gameBoy = new GameBoy.Emulator();
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Check issues #30 and #31
            // Also WinBoyEmulator/MainForm.cs/MainForm_Load - method.
            // There is comments about this.
            _gameBoy.GamePath = "C:\\temp\\game.gb";
            _gameBoy.LoadGameToMemory();

            _bitmap = Load(RenderTarget2D);
        }

        protected override void Draw(Stopwatch watch)
        {
            // Draw's base method
            base.Draw(watch);

            // emulate one cycle of gameboy
            _gameBoy.EmulateCycle();

            // convert GB's screen to System.Drawign bitmap
            var bitmap = _gameBoy.Screen.ToBitmap(RenderTarget2D);

            // Draw bitmap
            RenderTarget2D.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.Linear);


        }

        /// <summary>Run on a new form.</summary>
        public new void Run() => base.Run();

        /// <summary>Run game on the target form.</summary>
        /// <param name="targetForm">The form where the game will be put.</param>
        public new void Run(Form targetForm) => base.Run(targetForm);

        public Bitmap Load(RenderTarget renderTarget)
        {
            // Loads from file using System.Drawing.Image
            var bitmap = new System.Drawing.Bitmap(Configuration.Instance.Width, Configuration.Instance.Height);
            var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapProperties = new BitmapProperties(new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
            var size = new SharpDX.Size2(bitmap.Width, bitmap.Height);

            // Transform pixels from BGRA to RGBA
            int stride = bitmap.Width * sizeof(int);
            using (var tempStream = new SharpDX.DataStream(bitmap.Height * stride, true, true))
            {
                // Lock System.Drawing.Bitmap
                var bitmapData = bitmap.LockBits(sourceArea, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                // Convert all pixels 
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int offset = bitmapData.Stride * y;
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Not optimized 
                        byte B = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte G = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte R = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte A = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                        int rgba = R | (G << 8) | (B << 16) | (A << 24);
                        tempStream.Write(rgba);
                    }

                }
                bitmap.UnlockBits(bitmapData);
                tempStream.Position = 0;

                return new Bitmap(renderTarget, size, tempStream, stride, bitmapProperties);
            }

        }
    }
}
