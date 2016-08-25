using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.Memory
{
    interface IMemory
    {
        void Reset();
        void Load(string fileName);

        byte ReadByte(ushort address);
        ushort ReadShort(ushort address);

        void WriteByte(ushort address, byte value);
        void WriteShort(ushort address, ushort value);
    }
}
