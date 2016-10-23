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

using Log4Any;
using WinBoyEmulator.GameBoy;

namespace WinBoyEmulator
{
    public partial class MainForm : Form
    {
        private const string _sourceCodeUrl = "https://github.com/saku-kaarakainen/WinBoyEmulator/";
        private IVideoRenderer _graphics;
        private LogWriter _logWriter;
        private Emulator _gameBoy;

        /// <summary>
        /// MainForm's constructor. Takes one argument, <see cref="IVideoRenderer">renderer</see> 
        /// </summary>
        /// <param name="renderer">graphics renderer.</param>
        public MainForm(IVideoRenderer renderer)
        {
            InitializeGraphicsAndEmulator(renderer);
            InitializeComponent();
        }

        private void InitializeGraphicsAndEmulator(IVideoRenderer renderer)
        {
            _gameBoy = new Emulator(
                width:          200,
                height:         100,
                colorPalette:   new Color[] {
                    Color.Black,
                    Color.Gray,
                    Color.Silver//,
                   // Color.White
                });

            _graphics = renderer;
            _graphics.Screen = _gameBoy.Screen;
            _graphics.Loop = Loop;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _logWriter = new LogWriter(GetType());
            // Check Issues #30 and #31
            //_emulator = new Emulator { GamePath = "C:\\temp\\game.gb" };
            //_emulator.StartEmulation();
        }
        #region _Click
        private void _toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Issue #24");
        }

        private void _closeEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_emulator.StopEmulation();
        }

        private void _toolStripMenuItemOpen_Click(object sender, EventArgs e) => _openFileDialogMain.ShowDialog();

        private void _toolStripMenuItemClose_Click(object sender, EventArgs e) => Close();

        private void _toolStripMenuItemSourceCode_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo(_sourceCodeUrl));
        #endregion

        private void _openFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            //_emulator.GamePath = _openFileDialogMain.FileName;
            //_emulator.StartEmulation();
        }

        private void Loop()
        {
            // pre-render,
            // emulates one cycle of gameboy
            _gameBoy.EmulateCycle();

            for (var i = 0; i < _gameBoy.Screen.Data.Length; i++)
            {
                // 8 bit (1 byte) for each Red, Blue, Green and alpha. Total 8x4 =  32 bit (4 byte) 
                // ColorFormat.R8G8B8A8_UNorm 

                // First sequence
                _gameBoy.Screen.Data[i++] = 0; // Red
                _gameBoy.Screen.Data[i++] = 0; // Blue
                _gameBoy.Screen.Data[i++] = 0; // Green
                //_gameBoy.Screen.Data[i++] = 1; // Alpha

                if (i >= _gameBoy.Screen.Data.Length)
                    break;

                // Second sequence
                _gameBoy.Screen.Data[i++] = 255; // Red
                _gameBoy.Screen.Data[i++] = 255; // Blue
                _gameBoy.Screen.Data[i++] = 255; // Green
                //_gameBoy.Screen.Data[i++] = 1; // Alpha

                if (i >= _gameBoy.Screen.Data.Length)
                    break;

                // First sequence
                _gameBoy.Screen.Data[i++] = 255; // Red
                _gameBoy.Screen.Data[i++] = 0; // Blue
                _gameBoy.Screen.Data[i++] = 0; // Green
                //_gameBoy.Screen.Data[i++] = 1; // Alpha

                //if (i >= _gameBoy.Screen.Data.Length)
                //    break;

                //// Second sequence
                //_gameBoy.Screen.Data[i++] = 0; // Red
                //_gameBoy.Screen.Data[i++] = 255; // Blue
                //_gameBoy.Screen.Data[i++] = 0; // Green
                //_gameBoy.Screen.Data[i++] = 1; // Alpha
            }

            // Update screen
            _graphics.Update(_gameBoy.Screen);

            // Draws the bitmap
            _graphics.Draw();
        }
    }
}
