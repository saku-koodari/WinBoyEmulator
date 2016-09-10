using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator
{
    public class Pixel
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Pixel() /* : this(0, 0, 1, 1) */ { }

        public Pixel(int top, int left, int width, int height)
        {
            Top = top;
            Left = left;
            Width = width;
            Height = height;
        }

        public Rectangle ToRectangle() => new Rectangle(Top, Left, Width, Height);
    }

}
