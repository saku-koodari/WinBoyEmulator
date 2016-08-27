using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GPU
{
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Amount of pixels in a row in the screen.</param>
        /// <param name="height">Amount of pixels in a column in the screen.</param>
        /// <param name="colorsInPalette">amount of colors in palette</param>
        public Screen(int width, int height, int colorsInPalette)
        {
            Width = width;
            Height = Height;
            Data = new int[width * height * colorsInPalette];
        }
    }
}
