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

using Log4Any;

namespace WinBoyEmulator.GameBoy.GPU
{
    /// <summary>
    /// This class based heavily on:
    /// https://github.com/Two9A/jsGB/blob/master/js/gpu.js
    /// </summary>
    internal class GPU
    {
        private static readonly object _syncRoot = new object();
        private static volatile GPU _instance;
        private LogWriter _logWriter;

        private int[] _vram;
        private byte[] _oam;
        private int[] _reg; // TODO: rename this
        private byte[][][] _tilemap;
        private ObjectData[] _objectData;
        private ObjectData[] _objectDataSorted;
        private Palette _palette;
        private Screen _screen;

        private byte[] _scanrow;

        // TODO: rename these correctly
        private int _curLine = 0;
        private int _curScan = 0;
        private int _linemode = 0; // TODO: Use enum instead of this.
        private int _modeClocks = 0;

        private int _yscrl = 0;
        private int _xscrl = 0;
        private int _raster = 0;
        private int _ints = 0;

        private bool _isLcdOn = false;
        private bool _isBackgroundOn = false;
        private bool _isObjectOn = false;
        private bool _isWindowOn = false;

        private int _objectSize = 0;

        private int _bgtilebase = 0x0000;
        private int _bgmapbase = 0x1800;
        private int _wintilebase = 0x1800;

        public Screen Screen => _screen;

        public static GPU Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock(_syncRoot)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new GPU();
                }

                return _instance;
            }
        }


        public GPU()
        {
            // Check Issue #16: array sizes in GPU

            _logWriter = new LogWriter(typeof(GPU));

            // NOTE: if you use 3D array instead of jagged one, you could do this:
            // _tilemap = new byte[0x200, 0x8, 0x8];
            _tilemap = new byte[0x200][][];
            for (var i = 0; i < 0x200; i++)
            {
                _tilemap[i] = new byte[0x8][];
                for (var j = 0; j < 0x8; j++)
                {
                    _tilemap[i][j] = new byte[0x8];
                }
            }

            _vram = new int[0x2000]; //8192
            _oam = new byte[Configuration.Screen.Width];
            _reg = new int[1];

            _scanrow = new byte[Configuration.Screen.Width];
            _palette = new Palette(Configuration.Colors.Palette.Length);
            _objectData = new ObjectData[40];

            _screen = new Screen(Configuration.Screen.Width, Configuration.Screen.Height);
        }

        #region private methods for Checkline()
        // TODO: You might want to move these into the partial class (Checkline.cs, public partial class GPU)
        private void _hBlank()
        {
            if (_modeClocks < 51)
            {
                _logWriter.Warn($"method: _hBlank. ModeClocs under 51. It was {_modeClocks}. Method stopped.");
                return;
            }

            // End of hblank for last scanline; render screen
            if (_curLine == 143)
            {
                _linemode = 1;
                //_canvas.putImageData(GPU._scrn, 0, 0);
                //MMU._if |= 1;
                throw new NotImplementedException("Issue #17");
            }
            else
            {
                _linemode = 2;
            }
            _curLine++;
            _curScan += 640;
            _modeClocks = 0;
        }

        private void _vBlank()
        {
            if (_modeClocks < 114)
            {
                _logWriter.Warn($"method: _vBlank. ModeClocs under 114. It was {_modeClocks}. Method stopped.");
                return;
            }

            _modeClocks = 0;
            _curLine++;

            if (_curLine > 153)
            {
                _curLine = 0;
                _curScan = 0;
                _linemode = 2;
            }
        }

        private void _oamReadMode()
        {
            if (_modeClocks < 20)
            {
                _logWriter.Warn($"method: _oamReadMode. ModeClocs under 20. It was {_modeClocks}. Method stopped.");
                return;
            }

            _modeClocks = 0;
            _linemode = 3;
        }

        private void _vramReadMode()
        {
            var warnMessage = "method: _vramReadMode. {0}. Method stopped.";

            // Render scanline at end of allotted time
            if (_modeClocks < 43)
            {
                _logWriter.WarnFormat(warnMessage, $"_modeClocks under 43. It was {_modeClocks}");
                return;
            }

            _modeClocks = 0;
            _linemode = 0;

            if (!_isLcdOn)
            {
                _logWriter.WarnFormat(warnMessage, $"LCD is off");
                return;
            }

            if (!_isBackgroundOn)
            {
                _logWriter.WarnFormat(warnMessage, $"Background is off");
                return;
            }

            var linebase = _curScan;
            var mapbase = _bgmapbase + ((((_curLine + _yscrl) & 255) >> 3) << 5);
            var y = (_curLine + _yscrl) & 7;
            var x = _xscrl & 7;
            var t = (_xscrl >> 3) & 31;
            var w = 160;

            if (_bgtilebase != 0x0000)
            {
                var tile = _vram[mapbase + t];

                if (tile < 128)
                    tile = (256 + tile);

                var tilerow = _tilemap[tile][y];

                do
                {
                    _scanrow[Configuration.Screen.Width - x] = tilerow[x];
                    throw new NotImplementedException("fix _screen.data");
                    _screen.Data[linebase + 3] = (byte)_palette.Background[tilerow[x]];
                    x++;
                    if (x == 8)
                    {
                        t = (t + 1) & 31;
                        x = 0;
                        tile = _vram[mapbase + t];

                        if (tile < 128)
                        {
                            tile = 256 + tile;
                        }

                        tilerow = _tilemap[tile][y];
                    }
                    linebase += 4;
                } while (--w > 0);
            }
            else
            {
                var tilerow = _tilemap[_vram[mapbase + t]][y];
                do
                {
                    _scanrow[Configuration.Screen.Width - x] = tilerow[x];
                    throw new NotImplementedException("fix _screen.data");
                    _screen.Data[linebase + 3] = (byte)_palette.Background[tilerow[x]];
                    x++;
                    if (x == 8)
                    {
                        t = (t + 1) & 31;
                        x = 0;
                        tilerow = _tilemap[_vram[mapbase + t]][y];
                    }
                    linebase += 4;
                } while (--w > 0);
            }

            if (!_isObjectOn)
            {
                _logWriter.WarnFormat(warnMessage, "object is off");
                return;
            }

            var cnt = 0;
            if (_objectSize > 0)
            {
                for (var i = 0; i < 40; i++)
                {
                    // NOTE: I think i may want to initialize object here. Or something, I don't know.

                    // WTF Check source:
                    // https://github.com/Two9A/jsGB/blob/master/js/gpu.js line:213
                    throw new NotImplementedException("Issue #17");
                }

                // To avoid nested loops.
                return;
            }

            var _linebase = _curScan;
            for (var i = 0; i < 40; i++)
            {
                var objectSorted = _objectDataSorted[i];

                if (objectSorted.Y > _curLine || (objectSorted.Y + 8) <= _curLine)
                {
                    _logWriter.InfoFormat(warnMessage, "_curLine was out of boundaries. " +
                        $"It was {_curLine}. Min:{objectSorted.Y}, Max:{objectSorted.Y}");
                    return;
                }

                var j = objectSorted.yFlip
                    ? 7 - (_curLine - objectSorted.Y)
                    : _curLine - objectSorted.Y;
                var tileRow = _tilemap[objectSorted.Tile][j];
                var palette = objectSorted.Palette ? _palette.Object1 : _palette.Object2;

                linebase = (_curLine * Configuration.Screen.Width + objectSorted.X) * Configuration.Colors.Palette.Length;

                for (var _x = 0; _x < 8; _x++)
                {
                    if (objectSorted.X + _x >= 0 && objectSorted.X + _x < Configuration.Screen.Width
                    && (tileRow[_x] > 0 && (objectSorted.Priority || _scanrow[_x] == 0)))
                    {
                        throw new NotImplementedException("fix _screen.data");
                        _screen.Data[linebase + 3] = (byte)(objectSorted.xFlip ? palette[tileRow[7 - _x]] : palette[tileRow[_x]]);
                    }

                    linebase += 4;
                }

                cnt++;
                if (cnt > 10)
                    break;
            }
        }
        #endregion

        private void _sortObjectDataSorted()
        {
            // Original (javascript version) of sorted object data:
            /**
             *  GPU._objdatasorted.sort(function(a, b){
             *
             *  if (a.x > b.x)
             *      return -1;
             *
             *  if (a.num > b.num)
             *      return -1;
             *  });
             */
            //
            // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort
            // 
            // If I've understood correctly, 
            // object data is sorted primary by X, DESC
            // seconday by num, DESC

            _objectDataSorted = _objectData
                .OrderByDescending(objectData => objectData.X)
                .ThenByDescending(objectData => objectData.Number)
                .ToArray();
        }

        public void Reset()
        {
            var paletteCount = _palette.Background.Length;

            if (paletteCount != _palette.Object1.Length
            || paletteCount != _palette.Object2.Length)
            {
                var fatalMessage = "Atleast one element in _palette has incorrect size.";
                var backgroundLength = $"_palette.Background.Length: {paletteCount}";
                var object1Length = $"_palette.Object1.Length: {_palette.Object1.Length}";
                var object2Length = $"_palette.Object2.Length: {_palette.Object2.Length}";

                _logWriter.Fatal($"{fatalMessage} {backgroundLength}, {object1Length}, {object2Length}");
                throw new InvalidOperationException(fatalMessage);
            }

            for (var i = 0; i < _vram.Length; i++)
            {
                _vram[i] = 0;
            }

            for (var i = 0; i < _oam.Length; i++)
            {
                _oam[i] = 0;
            }

            // Initialize Palette to white
            for (var i = 0; i < paletteCount; i++)
            {
                _palette.Background[i] = 255;
                _palette.Object1[i] = 255;
                _palette.Object2[i] = 255;
            }

            for (var i = 0; i < _tilemap.GetLength(0); i++)
            {
                for (var j = 0; j < _tilemap.GetLength(1); j++)
                {
                    for (var k = 0; k < _tilemap.GetLength(2); k++)
                    {
                        _tilemap[i][j][k] = 0;
                    }
                }
            }

            _logWriter.Debug("GPU.Reset, Initializing Screen");

            // In the source (javascript code) has (re)create canvas at this point.
            /**
                var c = document.getElementById('screen');
                
                if(c && c.getContext)
                {
                    GPU._canvas = c.getContext('2d');
                    if(!GPU._canvas)
                    {
                        throw new Error('GPU: Canvas context could not be created.');
                    }
                    else
                    {
                        if(GPU._canvas.createImageData)
                            GPU._scrn = GPU._canvas.createImageData(160,144);
                        else if(GPU._canvas.getImageData)
                            GPU._scrn = GPU._canvas.getImageData(0,0,160,144);
                        else
                            GPU._scrn = {'width':160, 'height':144, 'data':new Array(160*144*4)};

                        for(i=0; i<GPU._scrn.data.length; i++)
                            GPU._scrn.data[i]=255;

                        GPU._canvas.putImageData(GPU._scrn, 0,0);
                    }
                }
             */

            throw new NotImplementedException("Issue #18");

            _curLine = 0;
            _curScan = 0;
            _linemode = 2;
            _modeClocks = 0;
            _yscrl = 0;
            _xscrl = 0;
            _raster = 0;
            _ints = 0;

            _isLcdOn = false;
            _isBackgroundOn = false;
            _isObjectOn = false;
            _isWindowOn = false;

            _objectSize = 0;

            for (var i = 0; i < _scanrow.Length; i++)
            {
                _scanrow[i] = 0;
            }

            var number = 0;
            foreach (var o in _objectData)
            {
                o.X = -16;
                o.Y = -8;
                o.Tile = 0;
                o.Palette = false;
                o.xFlip = false;
                o.yFlip = false;
                o.Priority = false;
                o.Number = number++;
            }

            // Set to values expected by BIOS, to start
            _bgtilebase = 0x0000;
            _bgmapbase = 0x1800;
            _wintilebase = 0x1800;

            _logWriter.Debug("GPU resetted.");
        }

        public void Checkline()
        {
            throw new NotImplementedException("Issue #17");
            switch (_linemode)
            {
                // In hblank
                case 0:
                    _hBlank();
                    break;
                // In vblank
                case 1:
                    _vBlank();
                    break;
                // In OAM-read mode
                case 2:
                    _oamReadMode();
                    break;
                // In VRAM-read mode
                case 3:
                    _vramReadMode();
                    break;
                default:
                    throw new InvalidOperationException($"_linemode must be 0-3. It was {_linemode}.");
            }
        }

        public void UpdateTile(int address, int value)
        {
            var saddress = address;

            if ((address & 1) > 0)
            {
                saddress--;
                address--;
            }

            var tile = (address >> 4) & 511;
            var y = (address >> 1) & 7;

            for (var x = 0; x < 8; x++)
            {
                var sx = 1 << (7 - x);
                var a = (_vram[saddress] & sx) > 0 ? 0x1 : 0x0;
                var b = (_vram[saddress + 1] & sx) > 0 ? 0x2 : 0x0;

                _tilemap[tile][y][x] = (byte)(a | b);
            }
        }

        public void UpdateOAM(int address, int value)
        {
            address -= 0xFE00;
            var obj = address >> 2;

            if (obj >= 40)
            {
                _sortObjectDataSorted();
                return;
            }

            switch (address & 3)
            {
                case 0:
                    _objectData[obj].Y = value - 16;
                    break;
                case 1:
                    _objectData[obj].X = value - 8;
                    break;
                case 2:
                    _objectData[obj].Tile = _objectSize > 0 ? value & 0xFE : value;
                    break;
                case 3:
                    _objectData[obj].Palette = (value & 0x10) > 0;
                    _objectData[obj].xFlip = (value & 0x20) > 0;
                    _objectData[obj].yFlip = (value & 0x40) > 0;
                    _objectData[obj].Priority = (value & 0x80) > 0;
                    break;

            }

            _sortObjectDataSorted();
        }

        public int ReadByte(int address)
        {
            var gaddress = address - 0xFF40;
            switch (gaddress)
            {
                case 0:
                    return (_isLcdOn ? 0x80 : 0)
                        | ((_bgtilebase == 0x0000) ? 0x10 : 0)
                        | ((_bgmapbase == 0x1C00) ? 0x08 : 0)
                        | (_objectSize > 0 ? 0x04 : 0)
                        | (_isObjectOn ? 0x02 : 0)
                        | (_isBackgroundOn ? 0x01 : 0);
                case 1:
                    return (_curLine == _raster ? 4 : 0) | _linemode;
                case 2:
                    return _yscrl;
                case 3:
                    return _xscrl;
                case 4:
                    return _curLine;
                case 5:
                    return _raster;
                default:
                    return _reg[gaddress];
            }
        }

        private int[] _writeColor(int value)
        {
            var length = Configuration.Colors.Palette.Length;
            var array = new int[length];

            for (var i = 0; i < length; i++)
            {
                // If colors is incorrect in game boy color (any other than regular game boy)
                // This might be the reason. (Because I haven't test this. :D)
                var colorIndex = (value >> (i * 2)) & (length - 1);
                var color = Configuration.Colors.Palette[colorIndex];
               
                // color as int value
                var iColor = color.R | (color.G << 8) | (color.B << 24);

                array[i] = iColor;
            }

            return array;
        }

        public void WriteByte(int address, int value)
        {
            var gaddress = address - 0xFF40;
            _reg[gaddress] = value;

            switch (gaddress)
            {
                case 0:
                    _isLcdOn = (value & 0x80) > 0;
                    _bgtilebase = (value & 0x10) > 0 ? 0x0000 : 0x0800;
                    _bgmapbase = (value & 0x08) > 0 ? 0x1C00 : 0x1800;
                    _objectSize = (value & 0x04) > 0 ? 1 : 0;
                    _isObjectOn = (value & 0x02) > 0;
                    _isBackgroundOn = (value & 0x01) > 0;
                    break;
                case 2:
                    _yscrl = value;
                    break;
                case 3:
                    _xscrl = value;
                    break;
                case 5:
                    _raster = value;
                    break; // TODO: in the source, there was break; missing. Find out whether it was on purpose or not.
                // OAM DMA
                case 6:
                    for (var i = 0; i < Configuration.Screen.Width; i++)
                    {
                        // var val = MMU.rb((value << 8) + i);
                        // _oam[i] = vval;
                        // UpdateOAM(0xFE00 + i, val);
                        throw new NotImplementedException("Issue #19");
                    }
                    break;

                // BG palette mapping
                case 7:
                    _palette.Background = _writeColor(value);
                    break;
                    // Issue #19:
                    //  1. Look the source (javascript) You can see, that 
                    //          there are used obj0 and obj1 instead object1 and object2.
                    //          Find out wheter that is done on purpose or not.
                // OBJ0 palette mapping
                case 8:
                    _palette.Object1 = _writeColor(value);
                    break;

                // OBJ1 palette mapping
                case 9:
                    _palette.Object2 = _writeColor(value);
                    break;
            }
        }
    }
}