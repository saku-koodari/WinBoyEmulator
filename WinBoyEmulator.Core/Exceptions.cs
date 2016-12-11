using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBoyEmulator.Core
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to read or write GameBoy's protected memory.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class WinBoyEmulatorMemoryAccessViolationException : Exception
    {
        /// <summary>
        /// The exception that is thrown when there is an attempt to read or write GameBoy's protected memory.
        /// </summary>
        /// <remarks>
        /// See EmulatorBase's MemoryBase.
        /// </remarks>
        public WinBoyEmulatorMemoryAccessViolationException() : base() { }

        /// <summary>
        /// The exception that is thrown when there is an attempt to read or write GameBoy's protected memory.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <remarks>
        /// See EmulatorBase's MemoryBase.
        /// </remarks>
        public WinBoyEmulatorMemoryAccessViolationException(string message) : base(message) { }
    }

    /// <summary>
    /// The exception that is thrown
    /// when the Address of the emulator's memory is outside the allowable range of the memory.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AddressOutOfRangeException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="AddressOutOfRangeException"/> class.</summary>
        public AddressOutOfRangeException() : base() { }

        public AddressOutOfRangeException(string message) : base(message) { }
    }

    /// <summary>Exception that is thrown when the WinBoyEmulator come across unexception state or situation.</summary>
    public class UnexpectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnexpectedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public UnexpectedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
