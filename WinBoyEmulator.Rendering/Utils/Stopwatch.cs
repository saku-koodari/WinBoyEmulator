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
