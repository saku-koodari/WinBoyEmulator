using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.GPU
{
    public class ObjectData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Tile { get; set; }
        public bool Palette { get; set; }
        public bool xFlip { get; set; }
        public bool yFlip { get; set; }
        public bool Priority { get; set; }
        public int Number { get; set; }
    }
}
