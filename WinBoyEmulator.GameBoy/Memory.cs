using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinBoyEmulator.Core;
using WinBoyEmulator.EmulatorBase;

namespace WinBoyEmulator.GameBoy
{
    public sealed class Memory : MemoryBase
    {
        // TODO: Should this be in MemoryBase?
        // When the machine starts, program counter is naturally in Bios.
        private bool _isProgramCounterInBios = true;

        /// <summary>Reads the byte from Memory.</summary>
        /// <param name="address">The address.</param>
        /// <returns>The byte found from Memory.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Address must be zero or bigger. or ""</exception>
        /// <exception cref="WinBoyEmulator.Core.UnexpectedException"></exception>
        public override byte ReadByte(int address)
        {
            // Min value
            if (address < 0x000)
                throw new ArgumentOutOfRangeException("Address must be zero or bigger.");

            // Max value
            if (address > 0xF000)
                throw new ArgumentOutOfRangeException($"Attempted to read too big (limit: '{Rom.Length}') address ('{address}').");

            // Doing bitwise AND with 0xF000 ignores tailing bits.
            // This means, rAddress can be 0x0000, 0x1000, 0x2000, ..., or 0xF000.
            var addressBlock = address & 0xF000;

            // Checking in which memory block address (with rAddress) is.
            switch(addressBlock)
            {
                // BIOS (256b) / ROM0
                case 0x0000:
                    // 
                    return _isProgramCounterInBios ? ReadByteFromBios(address) : Rom[address];

                // ROM0
                case 0x1000:
                case 0x2000:
                case 0x3000:

                // ROM1 (unbanked) 16k
                case 0x4000:
                case 0x5000:
                case 0x6000:
                case 0x7000:
                    // Read byte from ROM
                    return Rom[address];

                // Graphics: VRAM (8k)
                case 0x8000:
                case 0x9000:
                    throw new NotImplementedException("Read byte from Video RAM.");

                // External RAM (8k)
                case 0xA000:
                case 0xB000:
                    // TODO: Check why the old version did:
                    // return ExternalRam[address & 0x1FFF];
                    throw new NotImplementedException("Read byte from External RAM.");

                // Working RAM (8k)
                case 0xC000:
                case 0xD000:

                // Working RAM shadow
                case 0xE000:
                case 0xF000:
                    return ReadByteFromWorkingRam(address);
   
                default:
                    // This should never happen since the address should have been already verified.
                    throw new UnexpectedException($"Invalid address block: '{addressBlock}'");
            }
        }

        /// <summary>
        /// Reads the single byte from BIOS.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The byte from the BIOS.</returns>
        private byte ReadByteFromBios(int address)
        {
            if ( !_isProgramCounterInBios )
                return Rom[address];

            if (address < 0x0100)
                return Bios[address];
            else if (true)
            {
                // TODO: PC
                // On the original version, 
                // on this block else if condition statemenet were:
                // else if (CPU.LR35902.Instance.PC == 0x0100)
                // Inside it's block field _isProgramCounterInBios was set to false.
                // After that program threw NotImplementedException,
                // which referenced GitHub project issue #10.
                throw new NotImplementedException("Program Counter is in BIOS.");
            }

            // TODO: What should be done here?
            throw new NotImplementedException("¿Que?");
        }

        /// <summary>
        /// Reads the byte from working RAM.
        /// </summary>
        /// <param name="address">The address of the byte.</param>
        /// <returns>The byte from the address.</returns>
        private byte ReadByteFromWorkingRam(int address)
        {
            // I don't do argument checks here, because
            // I assume it is verified already.

            // This might slows down an emulator a bit.
            // Should it be better to pass addressBlock
            // from ReadByte?
            var addressBlock = address & 0xF000;

            switch(addressBlock)
            {
                case 0xC000:
                case 0xD000:
                    // return WorkingRam[address & 0x1FFF];
                    throw new NotImplementedException("Read byte from Working RAM.");
            }

            // Working RAM shadow
            // Address should be 0xE000 - 0xFFFF here.

            var addressSubBlock = addressBlock & 0x0F00;

            if (addressBlock == 0xE000 || addressSubBlock >= 0xD00)
                // return WorkingRam[address & 0x1FFF];
                throw new NotImplementedException("Working RAM shadow");

            switch(addressSubBlock)
            {
                // Graphics: OAM (Object Attribute Memory)
                // OAM is 160 bytes, remaining byes read as 0
                case 0xE00:
                    // TODO: OAM
                    // Original version had comment:
                    // return address < 0xFEA0 
                    //     ? GPU.Instance.OAM[address & 0xFF]
                    //     : 0;
                    //
                    // It also referenced about
                    // GitHub project Issue #12.
                    throw new NotImplementedException("OAM");

                // Zero-Page
                case 0xF00:
                    if (address >= 0xFF80)
                        // return ZeropageRam[address & 0x7F];
                        throw new NotImplementedException("Zero-Page.");
                    else
                        // In the original version just return 0
                        // and the comment said that the I/O control handling
                        // is unhandled at the moment.
                        throw new NotImplementedException("I/O Control handling.");

                // Again if program goes here, there is something terribly wrong.
                // (Since the address should have been caught by previous' cases.)
                default:
                    throw new UnexpectedException($"Unexpected sub-block: '{addressSubBlock}'.");
            }
        }

        public override void WriteByte(int address, byte value)
        {
            if (address < 0)
                throw new ArgumentOutOfRangeException("Address must be bigger than zero.");

            if (address > 0xFFFF)
                throw new ArgumentOutOfRangeException("Address is outside of memory.");

            if (address < 0x8000)
                // It's not allowed to write to BIOS since it's read-only.
                // That applies to ROM too as it name says so (ROM = Read-only Memory).
                throw new WinBoyEmulatorMemoryAccessViolationException("An attempt to write BIOS or ROM");

            var addressBlock = address & 0xF000;

            switch(addressBlock)
            {
                // Video RAM
                case 0x8000:
                case 0x9000:
                    // Original comments:
                    /*
                        TODO: GPU
                        GPU._vram[addr&0x1FFF] = val;
                        GPU.updatetile(addr & 0x1FFF, vale);
                    */
                    // Also comments referenced about GitHub project issue ¤13.
                    throw new NotImplementedException("GPU");

                // External RAM
                case 0xA000:
                case 0xB000:
                    // TODO: his sounds bizarre, to write to the external RAM.
                    // I mean, doesn't that External RAM means like the game's memory?
                    ExternalRam[address & 0x1FFF] = value;
                    return;

                // Working RAM
                case 0xC000:
                case 0xD000:
                case 0xE000:
                    WorkingRam[address & 0x1FFF] = value;
                    return;

                // Everything else
                case 0xF000:
                    // To prevent nesting we do only break here.
                    break;

                default:
                    throw new UnexpectedException($"Unexptected address block value: '{addressBlock}'.");
            }

            // Everything else

            var addressSubBlock = address & 0x0F00;

            // Echo RAM
            if (addressSubBlock <= 0xD00)
                WorkingRam[address & 0x1FFF] = value;

            // OAM
            else if (addressSubBlock == 0xE00)
            { // As a lazy I just ctrl c+v
                // if((addr&0xFF)<0xA0) GPU._oam[addr&0xFF] = val;
                // GPU.updateoam(addr, val);
                throw new NotImplementedException("Issue #14");
            }

           //  else
                // Zero-page RAM, I/O, ....
                throw new NotImplementedException();
        }
    }
}
