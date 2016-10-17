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

namespace WinBoyEmulator
{
    /// <summary>
    /// General <see cref="WinBoyEmulator"/> configurations. <para />
    /// I want this to be as much OOP as possible,
    /// so I try to avoid static classes.
    /// Configuration class contains configuration data.
    /// That should be available all the time.
    /// <para />
    /// An alternative solution would be use Singleton pattern,
    /// but I find it too complicated for configuration class.
    /// (Since you had to create instance and possible make it thread safe, etc)
    /// So using static class is justifiable in this situation.
    /// </summary>
    public static class Configuration
    {
        public static ThreadType ThreadType = ThreadType.SingleThread;
        public static ColorFormat ColorFormat = ColorFormat.R8G8B8A8_UNorm;
    }
}
