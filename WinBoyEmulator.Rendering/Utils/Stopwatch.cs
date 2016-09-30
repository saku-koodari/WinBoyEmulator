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

using OriginalStopwatch = System.Diagnostics.Stopwatch;

namespace WinBoyEmulator.Rendering.Utils
{
    /// <summary>Customized System.Diagnostic.Stopwatch</summary>
    public class Stopwatch : OriginalStopwatch
    {
        private double _lastUpdate;
        
        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        public new void Start()
        {
            base.Start();
            _lastUpdate = 0;
        }

        public double Update()
        {
            var now = Elapsed;
            var updateTime = now - _lastUpdate;
            _lastUpdate = now;

            return updateTime;
        }

        /// <summary>Gets the total elapsed time measured by the current instance.</summary>
        public new double Elapsed => ElapsedMilliseconds * 0.001;
    }
}
