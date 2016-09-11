using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using Format = SharpDX.DXGI.Format;

namespace WinBoyEmulator.Extensions
{
    public static class SharpDXExtensions
    {
        /// <summary>
        /// Initializes WindowRenderTarget. <para />
        /// Uses internally IntPtr.
        /// </summary>
        /// <param name="windoRenderTarget"></param>
        /// <param name="factory"></param>
        /// <param name="renderControl"></param>
        /// <returns></returns>
        public static WindowRenderTarget Create(this WindowRenderTarget windoRenderTarget, Factory factory, Control renderControl)
        {
            var renderTargetProperties = new RenderTargetProperties
            {
                PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Ignore),
                Type = RenderTargetType.Default,
                MinLevel = FeatureLevel.Level_DEFAULT
            };

            var hwndRenderTargetProperties = new HwndRenderTargetProperties
            {
                Hwnd = renderControl.Handle,
                PixelSize = new Size2(renderControl.ClientSize.Width, renderControl.ClientSize.Height),
                PresentOptions = PresentOptions.Immediately
            };

            return new WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
        }
    }
}
