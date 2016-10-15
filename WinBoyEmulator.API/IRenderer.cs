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
using System.Windows.Forms;

namespace WinBoyEmulator
{
    /// <summary>
    /// An interface which purpose is to keep a different
    /// video renderers compatible with WinBoyEmulator.
    /// </summary>
    public interface IVideoRenderer
    {
        /// <summary>
        /// Buffer contains the data of the screen in a bit array. <para />
        /// Buffer's format is a four-component, 32-bit unsigned-normalized-integer format
        /// that supports 8 bits for each color channel and 8-bit alpha.
        /// </summary>
        byte[] Buffer { get; set; }

        /// <summary>Actions that is done during the loop.</summary>
        Action Loop { get; set; }

        /// <summary>
        /// Method that starts the Game.
        /// </summary>
        /// <param name="targetForm">The form where the game is drawn.</param>
        void Run(Form targetForm);

        /// <summary>
        /// Updates buffer.
        /// </summary>
        /// <param name="updatedBuffer">You can use with this argument or without
        /// (the you must use Property <see cref="Buffer"/>).</param>
        void Update(byte[] updatedBuffer = null);

        /// <summary>Draws buffer to the target form.</summary>
        void Draw();
    }

}
