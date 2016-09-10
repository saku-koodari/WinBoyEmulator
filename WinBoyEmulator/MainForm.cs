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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using log4Any;

using WinBoyEmulator.GameBoy;

using Screen = WinBoyEmulator.GameBoy.GPU.Screen;

namespace WinBoyEmulator
{
    public partial class MainForm : Form
    {
        private static readonly object _syncDraw = new object();

        private const string _sourceCodeUrl = "https://github.com/saku-kaarakainen/WinBoyEmulator/";
        private const int gbWidth = GameBoy.Configuration.Screen.Width;
        private const int gbHeight = GameBoy.Configuration.Screen.Height;
        private static int gbcLength = GameBoy.Configuration.Colors.Palette.Length;

        private Rectangle[,] _rectangles;
        private Pixel _pixel;
        private Emulator _emulator;
        private LogWriter _logWriter;

        public MainForm() { InitializeComponent(); }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _logWriter = new LogWriter(GetType());
            _pixel = new Pixel
            {
                Top = _menuStripMain.Top + _menuStripMain.Height,
                Left = _menuStripMain.Left + _menuStripMain.Width,
                Width = 1,
                Height = 1
            };
            _emulator = new Emulator
            {
                // Check issues #30 and #31 
                GamePath = "C:\\temp\\game.gb",
                Graphics = CreateGraphics()
            };
            _emulator.DrawEventHandler += _draw;
            _emulator.StartEmulation();
        }

        private Brush GetBrushById(int colorCode)
        {
            // Not the most flexible solution...
            switch (colorCode)
            {
                case 0: return Brushes.White;
                case 1: return Brushes.Silver;
                case 2: return Brushes.Gray;
                case 3: return Brushes.Black;
                default:
                    var sColor = nameof(colorCode);
                    throw new ArgumentOutOfRangeException(sColor, $"{sColor} must be between 0 and 3.");
            }
        }

        private void _draw(Screen screen, PaintEventArgs e)
        {
            using (var graphics = CreateGraphics())
            {
                for (var i = 0; i < screen.Data.GetLength(0); i++)
                {
                    for (var j = 0; j < screen.Data.GetLength(1); j++)
                    {
                        var brush = GetBrushById(screen.Data[i, j]);
                        var x = _pixel.Top + i;
                        var y = _pixel.Left + j;

                        Console.WriteLine($"x = {x}");
                        Console.WriteLine($"y = {y}");
                        Console.WriteLine($"screen.Data[i, j] = {screen.Data[i, j]}");

                        graphics.FillRectangle(brush, x, y, _pixel.Width, _pixel.Height);
                        //graphics.FillRectangle(brush, _rectangles[i, j]);
                    }
                }
            }
        }

        #region _Click
        private void _toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Issue #24");
        }

        private void _closeEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _emulator.StopEmulation();
        }

        private void _toolStripMenuItemOpen_Click(object sender, EventArgs e) => _openFileDialogMain.ShowDialog();

        private void _toolStripMenuItemClose_Click(object sender, EventArgs e) => Close();

        private void _toolStripMenuItemSourceCode_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo(_sourceCodeUrl));
        #endregion

        private void _openFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            _emulator.GamePath = _openFileDialogMain.FileName;
            _emulator.StartEmulation();
        }
    }
}
