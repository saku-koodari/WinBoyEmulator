using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.Core
{
    public interface IEmulable
    {
        int Width { get; set; }
        int Height { get; set; }
        void EmulateCpu();
    }
}
