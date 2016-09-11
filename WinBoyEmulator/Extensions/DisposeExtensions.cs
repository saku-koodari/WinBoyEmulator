using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4Any;

namespace WinBoyEmulator.Extensions
{
    public static class DisposeExtensions
    {
        private static LogWriter _logWriter;

        static DisposeExtensions()
        {
            _logWriter = new LogWriter( typeof(DisposeExtensions) );
        }

        public static void DisposeIfNotNull(this IDisposable disposableObject)
        {
            if (disposableObject == null)
            {
                _logWriter.Debug("disposable object already null.");
                return;
            }

            disposableObject.Dispose();
            disposableObject = null;
        }
    }
}
