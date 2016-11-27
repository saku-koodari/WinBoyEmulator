using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinBoyEmulator.Core;
using WinBoyEmulator.Emulation;

namespace WinBoyEmulator.Forms
{
    public partial class MainForm : Form
    {
        private IVideoRenderer _graphics;
        private Emulator _emulator;
        public MainForm(IVideoRenderer renderer)
        {
            _graphics = renderer;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // initialize emulator
            _emulator = new Emulator();

            // select console to emulate
            // you should add ability to choose console by User.
            // A regular GameBoy is the default option 
            // (since in the first version it should be the only working one.)
            _emulator.GameBoyConsole = GameBoyConsole.GameBoy;

            // you should
            _emulator.Start();

            _graphics.Loop = Loop;

            // 1. you might want consider implment pause and stop also.
            // 2. how about window resize?
            // 3. Configuration form (change buttons, edit colors?)
            // 4. About Form
        }

        private void Loop()
        {
            // if game _emulator isn't started, following line doesn't do anything.
            _emulator.EmulateCycle();
            _graphics.Update(_emulator.Data);
            _graphics.Draw();
        }

        /*
         With _emulator.Pause() you can modify running speed of the console. 
         
         preferrably way to change console:
            _emulator.Stop();
            _emulator.GameBoyConsole = [the new console];
            _emulator.Start();
         
         */
    }
}
