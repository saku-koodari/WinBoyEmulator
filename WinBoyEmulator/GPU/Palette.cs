using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GPU
{
    /// <summary>
    /// Class Palette. <para />
    /// Every types can be here safely byte.
    /// Reason is, that we are saving handling here RGB values.
    /// Size of RGB value is one byte.
    /// </summary>
    public class Palette
    {
        public byte[] Background { get; set; }
        public byte[] Object1 { get; set; }
        public byte[] Object2 { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="colorsInPalette">Amount of colors in the palette.</param>
        public Palette(int colorsInPalette)
        {
            Background = new byte[colorsInPalette];
            Object1 = new byte[colorsInPalette];
            Object2 = new byte[colorsInPalette];
        }
    }
}
