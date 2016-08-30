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

using log4net;
using log4net.Config;

namespace log4Any
{
    /// <summary>Simple log writer.</summary>
    public class LogWriter
    {
        private static ILog _logger;
        private string _dateTimeFormat;

        static LogWriter()
        {
            // Set up a simple configuration that logs on the console
            BasicConfigurator.Configure();
        }

        /// <summary>
        /// Constructor with one argument.
        /// </summary>
        /// <param name="type">type of class</param>
        public LogWriter(Type type)
        {
            _logger = LogManager.GetLogger(type);

            // Default value
            _dateTimeFormat = "YYYY-mm-dd hh:mm:ss";
        }

        /// <summary>
        /// Constructor with two arguments. <para />
        /// NOTE: if dateTimeFormat is set to empty or null, it will not be printed.
        /// </summary>
        /// <param name="type">type of class</param>
        /// <param name="dateTimeFormat">format of date time.</param>
        public LogWriter(Type type, string dateTimeFormat)
        {
            _logger = LogManager.GetLogger(type);

            _dateTimeFormat = dateTimeFormat;
        }

        private string AppendDateTimeIfExists(string message)
        {
            var msg = new StringBuilder();

            // If there's no _dateTimeFormat,
            // then it's assumed that user doesn't wish to see it.
            if (!string.IsNullOrEmpty(_dateTimeFormat))
            {
                msg.Append("[").Append(DateTime.Now.ToString()).Append("] ");
            }

            return msg.Append(message).ToString();
        }

        /// <summary>
        /// Print Debug message.
        /// </summary>
        /// <param name="message">message</param>
        public void Debug(string message) => _logger.Debug(AppendDateTimeIfExists(message));

        /// <summary>
        /// Prints formatted Debug message <para />
        /// Example: _logWriter.DebugFormat("foo: {0}", bar);
        /// </summary>
        /// <param name="format">format.</param>
        /// <param name="args">arguments</param>
        public void DebugFormat(string format, params object[] args) => _logger.DebugFormat(format, args);
        
        /// <summary>
        /// Print Info message.
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message) => _logger.Info(AppendDateTimeIfExists(message));

        /// <summary>
        /// Prints formatted Info message <para />
        /// Example: _logWriter.InfoFormat("foo: {0}", bar);
        /// </summary>
        /// <param name="format">format.</param>
        /// <param name="args">arguments</param>
        public void InfoFormat(string format, params object[] args) => _logger.InfoFormat(format, args);

        /// <summary>
        /// Prints Warn message.
        /// </summary>
        /// <param name="message">message</param>
        public void Warn(string message) => _logger.Warn(AppendDateTimeIfExists(message));

        /// <summary>
        /// Prints formatted Warn message <para />
        /// Example: _logWriter.WarnFormat("foo: {0}", bar);
        /// </summary>
        /// <param name="format">format.</param>
        /// <param name="args">arguments</param>
        public void WarnFormat(string format, params object[] args) => _logger.WarnFormat(format, args);

        /// <summary>
        /// Prints Error message.
        /// </summary>
        /// <param name="message">message</param>
        public void Error(string message) => _logger.Error(AppendDateTimeIfExists(message));

        /// <summary>
        /// Prints formatted Error message <para />
        /// Example: _logWriter.ErrorFormat("foo: {0}", bar);
        /// </summary>
        /// <param name="format">format.</param>
        /// <param name="args">arguments</param>
        public void ErrorFormat(string format, params object[] args) => _logger.ErrorFormat(format, args);

        /// <summary>
        /// Prints Fatal message. Use this when thing went even worse than ERROR.
        /// </summary>
        /// <param name="message">message</param>
        public void Fatal(string message) => _logger.Fatal(AppendDateTimeIfExists(message));

        /// <summary>
        /// Prints formatted Fatal message <para />
        /// Example: _logWriter.FatalFormat("foo: {0}", bar);
        /// </summary>
        /// <param name="format">format.</param>
        /// <param name="args">arguments</param>
        public void FatalFormat(string format, params object[] args) => _logger.FatalFormat(format, args);
    }
}
