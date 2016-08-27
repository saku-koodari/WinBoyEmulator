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

        byte ReadByte(int address);
        int ReadShort(int address);

        void WriteByte(int address, byte value);
        void WriteShort(int address, int value);
    }
}
