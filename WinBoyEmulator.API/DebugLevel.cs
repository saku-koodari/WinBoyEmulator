using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator
{
    /// <summary>Debug level that is used in WinBoyEmulator</summary>
    public enum DebugLevel
    {
        /// <summary>
        /// No debugging. This should be used only for good reason. 
        /// Prefer error level on production. So there's a critical bug in the code,
        /// you are able to trace it faster.
        /// <summary>
        None = 0,

        /// <summary>
        /// Level that is used in errors.
        /// For example if system throws an exception.
        /// (unless throwing exception is the purpose.)
        /// </summary>
        Error = 1,
        
        /// <summary>
        /// Level that is used for warning.
        /// Use it on a situation where system should not crash / throw but
        /// situation should not happen or there might be a risk.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Level that tells useful information.
        /// Use it for showing good-to-know information.
        /// </summary>
        Information = 3,

        /// <summary>
        /// Level that prints debug values.
        /// A good for printing data that Visual Studio is unable to show in its debugger.
        /// </summary>
        Debug = 4,

        /// <summary>
        /// Level for tracing
        /// </summary>
        Trace = 5
    }
}
