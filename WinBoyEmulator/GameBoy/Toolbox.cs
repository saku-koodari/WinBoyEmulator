using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.GameBoy.GPU;

namespace WinBoyEmulator.GameBoy
{
    public class Toolbox
    {
        private Screen _screen;
        private Random _random;

        public Screen Screen
        {
            get
            {
                return _screen;
            }
            set
            {
                _screen = value;
            }
        }

        public Toolbox() : this(default(Screen))
        {

        }

        public Toolbox(Screen screen)
        {
            _screen = screen;
            _random = new Random();
        }

        /// <summary>Fills Screen with random color values.</summary>
        public Screen RandomizeScreen()
        {
            for(var i = 0; i < _screen.Data.Length; i++)
            {
                _screen.Data[i] = _random.Next(Configuration.Colors.Palette.Length - 1);
            }

            return _screen;
        }
    }
}
