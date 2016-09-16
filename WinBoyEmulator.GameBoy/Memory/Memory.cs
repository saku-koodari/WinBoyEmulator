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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Log4Any;

namespace WinBoyEmulator.GameBoy.Memory
{
    public class Memory : Bios, IMemory
    {
        private static readonly object _syncRoot = new object();
        private static volatile Memory _instance;

        private LogWriter _logWriter;
        private Rom _romObject { get; set; }

        private static byte[] _bios;
        private byte[] _rom;
        private byte[] _eram;
        private byte[] _wram;
        private byte[] _zram;

        private bool _isInBios;

        public static Memory Instance
        {
            get
            {
                if(_instance != null)
                {
                    return _instance;
                }

                lock(_syncRoot)
                {
                    if(_instance != null)
                    {
                        return _instance;
                    }

                    _instance = new Memory();
                }

                return _instance;
            }
        }

        static Memory() { _bios = BiosCode; }

        public Memory()
        {
            _romObject = new Rom();

            // Since running bios is always the first thing you will do.
            _isInBios = true;

            _rom = new byte[0x100];
            _eram = new byte[0x100];
            _wram = new byte[0x2000];
            _zram = new byte[0x7F];

            _logWriter = new LogWriter(typeof(Memory));
        }

        public void Load(byte[] game) => _rom = game;

        /// <summary>Resets memory.</summary>
        public void Reset()
        {
            for(var i = 0; i< _wram.Length; i++)
            {
                _wram[i] = 0;
            }

            for (var i = 0; i < _eram.Length; i++)
            {
                _eram[i] = 0;
            }

            for (var i = 0; i < _zram.Length; i++)
            {
                _zram[i] = 0;
            }
        }

        public byte ReadByte(int address)
        {
            var aAddress = address & 0xF000;

            switch (aAddress)
            {
                // BIOS (256b) / ROM0
                case 0x000:
                    if (_isInBios)
                    {
                        // Check if address is at the bios
                        if (address < 0x0100)
                        {
                            return _bios[address];
                        }
                        // Check if Program Counter is at BIOS
                        else if (CPU.LR35902.Instance.PC == 0x0100)
                        {
                            _isInBios = false;
                            throw new NotImplementedException("Issue #10");
                        }
                    }

                    return _rom[address];

                // ROM0
                case 0x1000:
                case 0x2000:
                case 0x3000:
                // Rom1 (unbanked) 16k
                case 0x4000:
                case 0x5000:
                case 0x6000:
                case 0x7000:
                    return _rom[address];

                // Graphics: VRAM (8k)
                case 0x8000:
                case 0x9000:
                    // return GPU._vram[addr & 0x1FFF];
                    throw new NotImplementedException("Issue #11");

                // External RAM (8k)
                case 0xA000:
                case 0xB000:
                    return _eram[address & 0x1FFF];

                // Working RAM (8k)
                case 0xC000:
                case 0xD000:
                    return _wram[address & 0x1FFF];

                // Working RAM shadow 
                case 0xE000:
                case 0xF000:
                    {
                        // Make bAddress variable. It's divided by 0x100 the avoid horrible long switch case
                        // Now it's just `bAddress >= 0xD` :)
                        var bAddress = (aAddress & 0x0F00) / 0x100;

                        // Check if address is in RAM shadow
                        if (aAddress == 0xE000 || bAddress >= 0xD) // 0xE000 = 0xE000 & 0xF000
                            return _wram[address & 0x1FFF];

                        switch (bAddress)
                        {
                            // Graphics: Object Attribute Memory
                            // OAM is 160 bytes, remaining bytes read as 0
                            case 0xE:
                                // return address < 0xFEA0 ? GPU.Instance.OAM[address & 0xFF] : 0;
                                throw new NotImplementedException("Issue #12");

                            // Zero-page
                            case 0xF:
                                // else: I/O control handling. Currently unhandled
                                // Why you can't return 0 without casting (if return type is byte)? Stupid imo.
                                return address >= 0xFF80 ? _zram[address & 0x7F] : (byte)0;

                            // error blaa blaa blaa
                            default:
                                var fatalMessage = $"argument must be between 0x000 - 0xFFFF (0 - 65535).";
                                _logWriter.FatalFormat("{0} address: {1}, aAddress: {2}.", fatalMessage, address, aAddress);
                                throw new InvalidOperationException(fatalMessage);
                        }
                    }

                // Check for bad address
                // This should never happen
                default:
                    {
                        var fatalMessage = $"argument must be between 0x000 - 0xFFFF (0 - 65535).";
                        _logWriter.FatalFormat("{0} address: {1}, aAddress: {2}.", fatalMessage, address, aAddress);
                        throw new ArgumentOutOfRangeException(nameof(address), fatalMessage);
                    }
            }
        }

        /// <summary>
        /// Read 16-bit word.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int ReadShort(int address) => (ReadByte(address) + ReadByte(address + 1) << 8);

        public void WriteByte(int address, byte value)
        {
            var warnMessage = "(method:WriteByte. Address='" + address + "'is at the '{0}'. Nothing has been written.";

            switch (address & 0xF000)
            {
                // ROM bank 0
                case 0x000:
                    _logWriter.WarnFormat(warnMessage, "ROM bank 0");

                    if (_isInBios && address < 0x0100)
                    {
                        return;
                    }

                    break;

                // ROM bank 0
                case 0x1000:
                case 0x2000:
                case 0x3000:
                    _logWriter.WarnFormat(warnMessage, "ROM bank 0");
                    break;

                // ROM bank 1
                case 0x4000:
                case 0x5000:
                case 0x6000:
                case 0x7000:
                    _logWriter.WarnFormat(warnMessage, "ROM bank 1");
                    break;

                // VRAM
                case 0x8000:
                case 0x9000:
                    // TODO: GPU
                    // GPU._vram[addr&0x1FFF] = val;
                    // GPU.updatetile(addr & 0x1FFF, val);
                    throw new NotImplementedException("Issue #13");

                // External RAM
                case 0xA000:
                case 0xB000:
                    _eram[address & 0x1FFF] = value;
                    break;

                // Work RAM and echo
                case 0xC000:
                case 0xD000:
                case 0xE000:
                    _wram[address & 0x1FFF] = value;
                    break;

                // Everything else
                case 0xF000:
                    {
                        var bAddress = (address & 0x0F00) / 100;

                        // Echo RAM
                        if (bAddress <= 0xD)
                        {
                            _wram[address & 0x1FFF] = value;
                            break;
                        }
                        // OAM
                        else if (bAddress == 0xE)
                        {
                            // if((addr&0xFF)<0xA0) GPU._oam[addr&0xFF] = val;
                            // GPU.updateoam(addr, val);
                            throw new NotImplementedException("Issue #14");
                        }
                        // Zero-page RAM, I/O
                        else if (bAddress == 0xF)
                        {
                            if (address > 0xFF7F)
                            {
                                _zram[address & 0x7F] = value;
                            }
                            else
                            {
                                switch (address & 0xF0)
                                {
                                    // I Wonder why this is here...
                                }

                                _logWriter.WarnFormat(warnMessage, "Zero-page RAM, I/O");
                            }
                        }

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(address), $"address ({address}) was not in the memory");
            }
        }
        public void WriteShort(int address, int value)
        {
            WriteByte(address, (byte)(value & 255));
            WriteByte((address + 1), (byte)(value >> 8));
        }
    }
}
