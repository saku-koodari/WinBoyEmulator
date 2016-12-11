using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace for defining base of all GameBoy emulators.
namespace WinBoyEmulator.EmulatorBase
{
    // BIOS = Basic Input Output System
    // ROM  = Read-only Memory
    // RAM  = Random-access Memroy
    // VRAM = Video RAM

    /// <summary>
    /// A base class for GameBoy's memory.
    /// </summary>
    /// <remarks>
    /// You should name the class that inherits this as 'Memory'
    /// (the classes should be identify by its namespace).
    /// </remarks>
    public abstract class MemoryBase
    {
        private byte[] _bios = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBase"/> class.
        /// </summary>
        public MemoryBase() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBase"/> class.
        /// </summary>
        /// <param name="bios">The bios.</param>
        public MemoryBase(byte[] bios)
        {
            _bios = bios;
        }

        /// <summary>
        /// Reads the byte from Memory.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>byte found from Memory.</returns>
        public abstract byte ReadByte(int address);

        /// <summary>
        /// Writes the byte to the memory.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public abstract void WriteByte(int address, byte value);

        // TODO: Should BIOS be abstract?
        // (Because each console might have different values in bios.)
        /// <summary>
        /// Return byte array which contains loaded BIOS.
        /// You should set this only if bios doesn't exists (or the emulable machine changes).
        /// </summary>
        /// <value>
        /// The bios.
        /// </value>
        /// <exception cref="System.InvalidOperationException">Bios is read-only memory.</exception>
        public byte[] Bios // { get; protected set; }
        {
            get
            {
                return _bios;
            }
            set
            {
                // TODO: Create an own WinBoyEmulator Exception project/namespace.
                // In this point you could throw WinBoyEmulatorMemoryAccessViolation

                // Check if bios is already written.
                if (_bios != null)
                    throw new InvalidOperationException("BIOS is already written.");

                // In this point _bios == null (author's sanity check)
                _bios = value;
            }
        }
        public byte[] Rom { get; set; }

        public byte[] VideoRam { get; set; }

        public byte[] ExternalRam { get; set; }

        public byte[] WorkingRam { get; set; }

        public byte[] ZeropageRam { get; set; }

        /// <summary>Resets the memory (apart from BIOS).</summary>
        public void Reset()
        {
            // Bios won't be reseted naturally
            ResetSinglePartOfMemory(Rom);
            ResetSinglePartOfMemory(VideoRam);
            ResetSinglePartOfMemory(ExternalRam);
            ResetSinglePartOfMemory(WorkingRam);
            ResetSinglePartOfMemory(ZeropageRam);
        }

        /// <summary>
        /// Resets the single part of memory.
        /// </summary>
        /// <param name="memoryPart">The memory part.</param>
        private void ResetSinglePartOfMemory(byte[] memoryPart)
        {
            for(var i = 0; i < memoryPart.Length; i++)
                memoryPart[i] = 0;
        }
    }
}
