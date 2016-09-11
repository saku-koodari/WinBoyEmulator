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
using WinBoyEmulator.SharpDX;

namespace WinBoyEmulator
{
    public partial class MainForm : Form
    {
        private static readonly object _syncRoot = new object();

        private const string _sourceCodeUrl = "https://github.com/saku-kaarakainen/WinBoyEmulator/";
        private LogWriter _logWriter;
        private Game _game;
        public MainForm() { InitializeComponent(); }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _logWriter = new LogWriter(GetType());
            _game = new Game{ GamePath = "C\\temp\\game.gb" };
            _game.Run(); // Check Issues #30 and #31
        }

        #region _Click
        private void _toolStripMenuItemOpen_Click(object sender, EventArgs e) => _openFileDialogMain.ShowDialog();
        private void _toolStripMenuItemSourceCode_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo(_sourceCodeUrl));

        private void _toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Issue #24");
        }

        private void _closeEmulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game.Dispose();
        }

        private void _toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            _game.Dispose();
            Close();
        }
        #endregion

        private void _openFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            _game.GamePath = _openFileDialogMain.FileName;
            _game.Run();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _game.Dispose();
        }
    }
}
