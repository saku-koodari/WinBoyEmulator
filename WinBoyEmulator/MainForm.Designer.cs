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
namespace WinBoyEmulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._menuStripMain = new System.Windows.Forms.MenuStrip();
            this._toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._closeEmulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItemSourceCode = new System.Windows.Forms.ToolStripMenuItem();
            this._openFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this._menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStripMain
            // 
            this._menuStripMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this._menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItemFile,
            this._toolStripMenuItemHelp});
            this._menuStripMain.Location = new System.Drawing.Point(0, 0);
            this._menuStripMain.Name = "_menuStripMain";
            this._menuStripMain.Padding = new System.Windows.Forms.Padding(16, 5, 0, 5);
            this._menuStripMain.Size = new System.Drawing.Size(779, 55);
            this._menuStripMain.TabIndex = 0;
            this._menuStripMain.Text = "menuStrip1";
            // 
            // _toolStripMenuItemFile
            // 
            this._toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItemOpen,
            this._closeEmulatorToolStripMenuItem,
            this._toolStripMenuItemClose});
            this._toolStripMenuItemFile.Name = "_toolStripMenuItemFile";
            this._toolStripMenuItemFile.Size = new System.Drawing.Size(75, 45);
            this._toolStripMenuItemFile.Text = "File";
            // 
            // _toolStripMenuItemOpen
            // 
            this._toolStripMenuItemOpen.Name = "_toolStripMenuItemOpen";
            this._toolStripMenuItemOpen.Size = new System.Drawing.Size(464, 46);
            this._toolStripMenuItemOpen.Text = "Open (Ctrl + O)";
            this._toolStripMenuItemOpen.Click += new System.EventHandler(this._toolStripMenuItemOpen_Click);
            // 
            // _closeEmulatorToolStripMenuItem
            // 
            this._closeEmulatorToolStripMenuItem.Name = "_closeEmulatorToolStripMenuItem";
            this._closeEmulatorToolStripMenuItem.Size = new System.Drawing.Size(464, 46);
            this._closeEmulatorToolStripMenuItem.Text = "Close Emulator (Ctrl + Q)";
            this._closeEmulatorToolStripMenuItem.Click += new System.EventHandler(this._closeEmulatorToolStripMenuItem_Click);
            // 
            // _toolStripMenuItemClose
            // 
            this._toolStripMenuItemClose.Name = "_toolStripMenuItemClose";
            this._toolStripMenuItemClose.Size = new System.Drawing.Size(464, 46);
            this._toolStripMenuItemClose.Text = "Close (Alt + F4)";
            this._toolStripMenuItemClose.Click += new System.EventHandler(this._toolStripMenuItemClose_Click);
            // 
            // _toolStripMenuItemHelp
            // 
            this._toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItemAbout,
            this._toolStripMenuItemSourceCode});
            this._toolStripMenuItemHelp.Name = "_toolStripMenuItemHelp";
            this._toolStripMenuItemHelp.Size = new System.Drawing.Size(92, 45);
            this._toolStripMenuItemHelp.Text = "Help";
            // 
            // _toolStripMenuItemAbout
            // 
            this._toolStripMenuItemAbout.Name = "_toolStripMenuItemAbout";
            this._toolStripMenuItemAbout.Size = new System.Drawing.Size(422, 46);
            this._toolStripMenuItemAbout.Text = "About (F1)";
            this._toolStripMenuItemAbout.Click += new System.EventHandler(this._toolStripMenuItemAbout_Click);
            // 
            // _toolStripMenuItemSourceCode
            // 
            this._toolStripMenuItemSourceCode.Name = "_toolStripMenuItemSourceCode";
            this._toolStripMenuItemSourceCode.Size = new System.Drawing.Size(422, 46);
            this._toolStripMenuItemSourceCode.Text = "Source-code (GitHub)";
            this._toolStripMenuItemSourceCode.Click += new System.EventHandler(this._toolStripMenuItemSourceCode_Click);
            // 
            // _openFileDialogMain
            // 
            this._openFileDialogMain.FileName = "game.gb";
            this._openFileDialogMain.FileOk += new System.ComponentModel.CancelEventHandler(this._openFileDialogMain_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 622);
            this.Controls.Add(this._menuStripMain);
            this.MainMenuStrip = this._menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this._menuStripMain.ResumeLayout(false);
            this._menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemClose;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemSourceCode;
        private System.Windows.Forms.OpenFileDialog _openFileDialogMain;
        private System.Windows.Forms.ToolStripMenuItem _closeEmulatorToolStripMenuItem;
    }
}

