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

namespace WinBoyEmulator.GameBoy.Memory
{
    internal class Rom
    {
        // TODO: consider combining this class with Bios.cs

        // TODO: Implement this.
        // NOTE: You might need to implement a custom reader.
        // This might contain useful info:
        // https://github.com/visualboyadvance/visualboyadvance/blob/a2868a7eea7d5a6bc07a84ea69f18fafb09dfe4c/src/common/Loader.c
        public void Load(string fileName)
        {
            throw new NotImplementedException("Issue #15");
        }
    }
}
