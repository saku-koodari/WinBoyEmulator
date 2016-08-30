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

using WinBoyEmulator.GameBoy;

namespace WinBoyEmulator
{
    public partial class MainForm : Form
    {
        private const string _sourceCodeUrl = "https://github.com/saku-kaarakainen/WinBoyEmulator/";

        public MainForm() { InitializeComponent(); }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // This due to Issue #30
            // Check #31
            Emulator.Instance.StartEmulation("C:\\temp\\game.gb");
        }

        #region _Click
        private void _toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Issue #24");
        }

        private void _toolStripMenuItemOpen_Click(object sender, EventArgs e) => _openFileDialogMain.ShowDialog();
        
        private void _toolStripMenuItemClose_Click(object sender, EventArgs e) => Close();

        private void _toolStripMenuItemSourceCode_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo(_sourceCodeUrl));
        #endregion

        private void _openFileDialogMain_FileOk(object sender, CancelEventArgs e)
        {
            Emulator.Instance.StartEmulation(_openFileDialogMain.FileName);
        }
    }
}
