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
            InitializeComponent();
            InitializeGraphicsAndEmulator(renderer);
        }

        private void InitializeGraphicsAndEmulator(IVideoRenderer renderer)
        {
            _gameBoy = new Emulator();
            _graphics = renderer;
            _graphics.Loop = Loop;
            _graphics.Buffer = new byte[_gameBoy.Width 
                * _gameBoy.Height 
                * _gameBoy.ColorPalette.Length];
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

        private readonly static Random r = new Random();
        private void Loop()
        {
            // pre-render,
            // emulates one cycle of gameboy
            _gameBoy.EmulateCycle();

            r.NextBytes(_gameBoy.Screen.Data);

            _graphics.Buffer = _gameBoy.Screen.Data;

            // Update renderer's buffer. 
            _graphics.Update();

            // Draws the bitmap
            _graphics.Draw();
        }
    }
}
